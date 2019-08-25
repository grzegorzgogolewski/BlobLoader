using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using IniParser;
using IniParser.Model;

namespace BlobLoader
{
    public partial class FormMain : Form
    {
        readonly List<BlobFile> _blobFiles = new List<BlobFile>();      // lista obiektów opisujących pliki do ładowania
        readonly Operaty _dbOperaty = new Operaty();
        readonly KdokRodzDict _dbKdokRodz = new KdokRodzDict();

        public FormMain()
        {
            InitializeComponent();

            buttonLoad.Enabled = false;
            buttonVerify.Enabled = false;
            buttonFilter.Enabled = false;
            buttonDisconnect.Enabled = false;
        }

        // Ładowanie głównego okna aplikacji
        private void FormMain_Load(object sender, EventArgs e)
        {
            // ------------------------------------------------------------------------------------
            // zapamiętaj parametry połączenia w pliku konfiguracyjnym
            textBoxHost.Text = ReadIni("Database", "Host");
            textBoxDb.Text = ReadIni("Database", "Database");
            textBoxUser.Text = SecureText.UnProtect(ReadIni("Database", "User"));
            textBoxPass.Text = SecureText.UnProtect(ReadIni("Database", "Pass"));

            // wyświetl tytuł aplikacji
            Text = Application.ProductName + @" " + Application.ProductVersion;
        }

        //Operacje uruchamiane podczas zamykania głównego okna aplikacji
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            oracleConnection.Close();       // zamknij połączenie z bazą przy wyjściu z programu
        }

        //Obsługa nawiązania połączenia z bazą danych Oracle
        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                oracleConnection.ConnectionString = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST={textBoxHost.Text})(PORT={1521}))(CONNECT_DATA=(SERVICE_NAME={textBoxDb.Text})));User Id={textBoxUser.Text};Password={textBoxPass.Text};";
                oracleConnection.Open();

                // przełączenie aktywności przycisków w zależności od statusu połączenia
                buttonLoad.Enabled = true;
                buttonVerify.Enabled = true;
                buttonConnect.Enabled = false;
                buttonDisconnect.Enabled = true;

                // zapisanie parametrów połączenia do pliku konfiguracyjnego
                SaveIni("Database", "Host", textBoxHost.Text);
                SaveIni("Database", "Database", textBoxDb.Text);
                SaveIni("Database", "User", SecureText.Protect(textBoxUser.Text));
                SaveIni("Database", "Pass", SecureText.Protect(textBoxPass.Text)); // hasło dodatkowo jest kodowane

                toolStripStatusLabel.Text = @"Połączono z bazą: " + textBoxDb.Text;
            }
            catch (Exception exception)
            {
                toolStripStatusLabel.Text = exception.Message;
            }

        }

        // Obsługa rozłączenia z bazą danych
        private void ButtonDisconnect_Click(object sender, EventArgs e)
        {
            oracleConnection.Close(); // zamknięcie połączenia z bazą danych

            // przełączenie aktywności przycisków w zależności od statusu połączenia
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
            buttonLoad.Enabled = false;
            buttonVerify.Enabled = false;

            toolStripStatusLabel.Text = @"Rozłączono z bazą";
        }

        // obsługa wyboru folderu ze skanami
        private void ButtonSelectDirectoryBlobs_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                SelectedPath = ReadIni("Files", "RecentFolder") // jako domyślny wybierz ostatnio wybrany folder
            };

            DialogResult result = fbd.ShowDialog(); // uruchom okno wyboru folderu

            if (result == DialogResult.OK)
            {
                SaveIni("Files", "RecentFolder", fbd.SelectedPath); // jeśli wybrano folder zapisz go do pliku konfiguracyjnego jako ostatnio wybrany

                string[] fileNames = Directory.GetFiles(fbd.SelectedPath, "*.*", SearchOption.AllDirectories); // wybierz wszystkie pliki włącznie z podfolderami
                
                Array.Sort(fileNames, new NaturalStringComparer()); // sortowanie nazw plików tak aby cyfry było w kolejności

                dataGridView.DataSource = null;
                _blobFiles.Clear(); // wyczyść listę obiektów

                // utwórz obiekt dla każdego pliku, dokonaj jego analizy i dodaj do listy
                foreach (string file in fileNames)
                {
                    BlobFile blobFile = new BlobFile(file, _blobFiles.Count + 1);
                    _blobFiles.Add(blobFile);
                }

                dataGridView.DataSource = _blobFiles; // przypisz listę obiektów do widoku tabeli

                dataGridView.Columns[3].DefaultCellStyle.Alignment = DataGridViewContentAlignment.BottomRight; // justyfikacja prawa dla rozmiaru pliku

                dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);   // automatyczna szerokość dla każdej kolumny

                toolStripStatusLabel.Text = $@"Liczba wczytanych plików: {_blobFiles.Count}";

            }
        }

        // obsługa zdarzenia ładowania plików
        private void ButtonLoad_Click(object sender, EventArgs e)
        {
            buttonLoad.Enabled = false;     // wyłącz przycisk ładowania plików

            loadBlobBackgroundWorker.RunWorkerAsync();      // uruchom wątek ładowania plików
        }

        // Wątek ładowania plików do blob
        private void LoadBlobBackgroundWorkerOnDoWork(object sender, DoWorkEventArgs e)
        {
            OracleTransaction transaction = oracleConnection.BeginTransaction();    // transakcja ładowania plików

            OracleCommand command = oracleConnection.CreateCommand();   // polecenie dla połączenia

            // parametr dla polecenia ładowania pliku do blob - binarna zawartość pliku
            OracleParameter blobDataParameter = new OracleParameter
            {
                OracleDbType = OracleDbType.Blob,
                ParameterName = "blobDataParameter"
            };

            try
            {
                int fileCounter = 0;    // licznik plików załadowanych. potrzebny do statusu i obliczenia procent ładowania
                int filesSize = 0;      // łączny rozmiar wszystkich załadowanych plików

                // dla każdego wskazanego pliku
                foreach (BlobFile blobFile in _blobFiles)
                {
                    fileCounter++;      // licznik załadowanych plików

                    string fileFullName = blobFile.FullFileName;        // pełna nazwa pliku włączenie ze ścieżką
                    string fileShortName = blobFile.FileName;           // tylko nazwa pliku
                    int fileSize = blobFile.FileSize;                   // rozmiar pliku
                     
                    filesSize += fileSize;      // zsumuj wielkość plików

                    toolStripStatusLabel.Text = $@"Ładowanie pliku {fileCounter}/{dataGridView.Rows.Count}: {fileFullName}";

                    // ----------------------------------------------------------------------------
                    // pobierz zawartość pliku
                    // ----------------------------------------------------------------------------
                    FileStream fs = new FileStream(fileFullName, FileMode.Open, FileAccess.Read);
                    byte[] blobDataValue = new byte[fs.Length];
                    fs.Read(blobDataValue, 0, Convert.ToInt32(fs.Length));
                    fs.Close();
                    // ----------------------------------------------------------------------------

                    // ----------------------------------------------------------------------------
                    // ładowanie pliku do BLOB
                    // ----------------------------------------------------------------------------
                    command.CommandText = "SELECT blob.kdok_plikisq.nextval FROM dual";     // pobranie sekwencji dla ładowanego rekordu BLOB
                    int idFileSq = int.Parse(command.ExecuteScalar().ToString());

                    // polecenie dodania pliku do blob
                    command.CommandText = $"INSERT INTO blob.kdok_pliki (id_file, pieczec_pliku, typ_pliku, data) VALUES({idFileSq}, {idFileSq}, '{fileShortName}', :blobDataParameter)";

                    command.Parameters.Clear();                     // przygotowanie parametru polecenia
                    command.Parameters.Add(blobDataParameter);      // którym jest plik binarny
                    blobDataParameter.Value = blobDataValue;        // ze skanem

                    command.ExecuteNonQuery();                      // wykonaj polecenie
                    // ----------------------------------------------------------------------------

                    // ----------------------------------------------------------------------------
                    // ładowanie rekordu do KDOK_WSK
                    // ----------------------------------------------------------------------------
                    command.CommandText = "SELECT ewid4.kdok_wsksq.nextval FROM dual";     // pobranie sekwencji dla ładowanego rekordu kdok_wsk
                    int idDokSq = int.Parse(command.ExecuteScalar().ToString());

                    int idGr = blobFile.IdOp;
                    string path = blobFile.FullFileName;
                    int idRodzDok = blobFile.PrefixId;
                    int userId = 696;

                    command.CommandText = $"INSERT INTO KDOK_WSK(ID_DOK, WL, ID_GR, ID_FILE, PATH, ID_RODZ_DOK, OPIS, USER_ID, DATA_D) VALUES({idDokSq}, 'operat', {idGr}, {idFileSq}, '{path}', {idRodzDok}, '', {userId}, SYSDATE)";
                    command.ExecuteNonQuery();

                    // ----------------------------------------------------------------------------

                    int percentage = (fileCounter * 100)/ dataGridView.Rows.Count;      // oblicz procentowe zaawansowanie ładowania
                    loadBlobBackgroundWorker.ReportProgress(percentage);                // zaraportuj zaawansowanie
                }

                transaction.Commit();       // zatwierdzenie transakcji ładowania

                toolStripStatusLabel.Text = $@"Załadowano {dataGridView.Rows.Count} plików o łącznym rozmiarze {filesSize / 1024} MB";
            }
            catch (Exception exception)
            {
                transaction.Rollback();     // w przypadku wystąpienia błędu wycofaj transakcje
                toolStripStatusLabel.Text = exception.Message;
            }
            finally
            {
                command.Dispose();
                transaction.Dispose();
            }
            
        }

        private void LoadBlobBackgroundWorkerOnProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void LoadBlobBackgroundWorkerOnRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonLoad.Enabled = true;
        }

        private static void SaveIni(string section, string key, string value)
        {
            if (!File.Exists("configuration.ini")) File.Create("configuration.ini").Dispose();

            FileIniDataParser parser = new FileIniDataParser();
            IniData iniFile = parser.ReadFile("configuration.ini");
            iniFile[section][key] = value;

            parser.WriteFile("configuration.ini", iniFile);
        }

        private static string ReadIni(string section, string key)
        {
            if (!File.Exists("configuration.ini")) File.Create("configuration.ini").Dispose();

            FileIniDataParser parser = new FileIniDataParser();
            IniData iniFile = parser.ReadFile("configuration.ini");
            return iniFile[section][key];
        }

        // usunięcie z listy poprawnych wartości i pozostawienie tylko błędnych
        private void ButtonFilter_Click(object sender, EventArgs e)
        {
            dataGridView.DataSource = null;     // usuń wszystkie rekordy z widoku

            // usuń wartości ze statusem "OK"
            for (int i = 0; i < _blobFiles.Count; i++)
            {
                if (_blobFiles[i].Status == "OK")
                {
                    _blobFiles.RemoveAt(i);
                    i--;
                }
            }

            dataGridView.DataSource = _blobFiles;       // przypisz ponownie listę obiektów z plikami

            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);       // automatyczna szerokość dla każdej kolumny

            toolStripStatusLabel.Text = $@"Liczba błędów: {_blobFiles.Count}";
        }

        private void ButtonVerify_Click(object sender, EventArgs e)
        {
            buttonFilter.Enabled = false;
            buttonLoad.Enabled = false;
            buttonSelectDirectoryBlobs.Enabled = false;
            buttonVerify.Enabled = false;

            verifyBackgroundWorker.RunWorkerAsync();
        }

        private void VerifyBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // ------------------------------------------------------------------------------------
            // pobierz listę operatów z bazy Ewid
            // ------------------------------------------------------------------------------------

            toolStripStatusLabel.Text = @"Pobieranie listy operatów...";

            _dbOperaty.Clear(); // wyczyść listę obiektów

            OracleCommand command = oracleConnection.CreateCommand();
            command.CommandText = @"SELECT idop, sygnatura, idmaterialu FROM ewid4.osr_operat ORDER BY idop";
            OracleDataReader reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    Operat operat = new Operat
                    {
                        IdOp = reader.GetInt32(0),
                        NrOperatu = reader.GetValue(1).ToString(),
                        IdMaterialu = reader.GetValue(2).ToString()
                    };

                    _dbOperaty.Add(reader.GetInt32(0), operat);
                }
            }
            finally
            {
                reader.Close();
                command.Dispose();
            }

            // ------------------------------------------------------------------------------------
            // pobierz listę rodzajów dokumentów z bazy Ewid
            // ------------------------------------------------------------------------------------

            toolStripStatusLabel.Text = @"Pobieranie rodzajów dokumentów...";

            _dbKdokRodz.Clear(); // wyczyść listę obiektów

            command = oracleConnection.CreateCommand();
            command.CommandText = @"SELECT id_rodz_dok, opis, prefix, nazdok_id, gml_val FROM ewid4.kdok_rodz ORDER BY id_rodz_dok";
            reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    KdokRodz kdokRodz = new KdokRodz
                    {
                        IdRodzDok = reader.GetInt32(0),
                        Opis = reader.GetValue(1).ToString(),
                        Prefix = reader.GetValue(2).ToString(),
                        NazdokId = reader.GetValue(3).ToString(),
                        GmlVal = reader.GetValue(4).ToString()
                    };

                    _dbKdokRodz.Add(reader.GetInt32(0), kdokRodz);
                }
            }
            finally
            {
                reader.Close();
                command.Dispose();
            }

            // ------------------------------------------------------------------------------------
            // Sprawdź czy operaty z folderów występują w bazie
            // ------------------------------------------------------------------------------------

            toolStripStatusLabel.Text = @"Sprawdzanie numerów operatów..";

            int errorCount = 0;

            foreach (BlobFile blobFile in _blobFiles)
            {
                switch (_dbOperaty.GetOperatCount(blobFile.IdMaterialu))
                {
                    case 0:
                        errorCount++;
                        blobFile.IdOp = 0;
                        blobFile.Status = "brak operatu w bazie";
                        break;
                    case 1:
                        blobFile.IdOp = _dbOperaty.GetIdOp(blobFile.IdMaterialu);
                        blobFile.Status = "OK";
                        break;
                    default:
                        errorCount++;
                        blobFile.Status = "wiele takich samych operatów";
                        MessageBox.Show(@"Więcej niż jeden operat o nazwie: " + blobFile.IdMaterialu);
                        break;
                }

            }

            // ------------------------------------------------------------------------------------
            // Sprawdź czy pliki mają właściwie prefiksy
            // ------------------------------------------------------------------------------------

            toolStripStatusLabel.Text = @"Pobieranie prefiksów...";

            foreach (BlobFile blobFile in _blobFiles)
            {
                blobFile.PrefixId = _dbKdokRodz.GetIdRodzDok(blobFile.FileName);
                blobFile.Prefix = _dbKdokRodz.GetOpis(blobFile.FileName);

                if (blobFile.PrefixId == 0) // jeśli nie odnaleziono prefiksu
                {
                    if (blobFile.Status == "OK" || blobFile.Status == null)    // jeśli dla tego pliku nie było wcześniej błędu
                    {
                        errorCount++;
                        blobFile.Status = "błąd prefiksu";
                    }
                    else
                    {
                        blobFile.Status += ", " + "błąd prefiksu";
                    }
                }
                else if (blobFile.Status == null)   // jeśli odnaleziono prefix a status jest nieuzupełniony
                {
                    blobFile.Status = "OK";
                }

            }

            //toolStripStatusLabel.Text = @"Odświeżanie listy...";
            //dataGridView.Refresh();

            // ------------------------------------------------------------------------------------
            // Zaznacz kolorami wiersze w zależności od statusu
            // ------------------------------------------------------------------------------------

            toolStripStatusLabel.Text = $@"Liczba błędów: {errorCount} / {_blobFiles.Count}";

        }

        private void VerifyBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {

        }

        private void VerifyBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            bool isError = false;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[8].Value.ToString() != "OK")
                {
                    isError = true;
                    row.DefaultCellStyle.BackColor = Color.LightPink;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.LightGreen;
                }
            }

            dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);   // automatyczna szerokość dla każdej kolumny

            if (isError) // jeśli są błędy 
            {
                buttonFilter.Enabled = true;
                buttonLoad.Enabled = false;
            }
            else // jeśli nie ma błędów
            {
                buttonFilter.Enabled = false;
                buttonLoad.Enabled = true;
            }

            buttonSelectDirectoryBlobs.Enabled = true;
            buttonVerify.Enabled = true;


        }
    }
}
