using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using System.Linq;
using System.Text;
using BlobLoader.Tools;
using License;

namespace BlobLoader
{
    public partial class FormMain : Form
    {
        private readonly List<BlobFile> _blobFiles = new List<BlobFile>();      // lista obiektów opisujących pliki do ładowania
        private readonly Operaty _dbOperaty = new Operaty();
        private readonly KdokRodzDict _dbKdokRodz = new KdokRodzDict();

        private readonly OracleConnection _oracleConnection = new OracleConnection();

        public FormMain()
        {
            InitializeComponent();

            buttonLoad.Enabled = false;
            buttonVerify.Enabled = false;
            buttonFilter.Enabled = false;
            buttonDisconnect.Enabled = false;
            buttonLoadCustom.Enabled = false;

            dateTimePickerDataD.CustomFormat = "yyyy-MM-dd HH:mm:ss";
        }

        // Ładowanie głównego okna aplikacji
        private void FormMain_Load(object sender, EventArgs e)
        {
            // ------------------------------------------------------------------------------------
            // wczytaj parametry programu z pliku konfiguracyjnego
            
            textBoxHost.Text = IniFile.ReadIni("Database", "textBoxHost");
            textBoxDb.Text = IniFile.ReadIni("Database", "textBoxDb");
            textBoxUser.Text = SecureText.UnProtect(IniFile.ReadIni("Database", "textBoxUser"));
            textBoxPass.Text = SecureText.UnProtect(IniFile.ReadIni("Database", "textBoxPass"));

            textBoxUserId.Text = IniFile.ReadIni("Params", "textBoxUserId");
            dateTimePickerDataD.Text = IniFile.ReadIni("Params", "dateTimePickerDataD");

            textBoxDokId.Text = IniFile.ReadIni("Params", "textBoxDokId");
            textBoxIdRodzDok.Text = IniFile.ReadIni("Params", "textBoxIdRodzDok");
            comboBoxWl.Text = IniFile.ReadIni("Params", "comboBoxWl");

            // wyświetl tytuł aplikacji
            Text = Application.ProductName + " " + Application.ProductVersion;
        }

        //Operacje uruchamiane podczas zamykania głównego okna aplikacji
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            _oracleConnection.Close();       // zamknij połączenie z bazą przy wyjściu z programu
        }

        //Obsługa nawiązania połączenia z bazą danych Oracle
        private void ButtonConnect_Click(object sender, EventArgs e)
        {
            try
            {
                _oracleConnection.ConnectionString = $"Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(" +
                                                     $"HOST={textBoxHost.Text})(" +
                                                     $"PORT={1521}))(CONNECT_DATA=(" +
                                                     $"SERVICE_NAME={textBoxDb.Text})));" +
                                                     $"User Id={textBoxUser.Text};" +
                                                     $"Password={textBoxPass.Text};";
                _oracleConnection.Open();

                // przełączenie aktywności przycisków w zależności od statusu połączenia
                buttonLoad.Enabled = true;
                buttonVerify.Enabled = true;
                buttonConnect.Enabled = false;
                buttonDisconnect.Enabled = true;
                buttonLoadCustom.Enabled = true;

                toolStripStatusLabel.Text = "Połączono z bazą: " + textBoxDb.Text;
            }
            catch (Exception exception)
            {
                toolStripStatusLabel.Text = exception.Message;
            }
        }

        // Obsługa rozłączenia z bazą danych
        private void ButtonDisconnect_Click(object sender, EventArgs e)
        {
            _oracleConnection.Close(); // zamknięcie połączenia z bazą danych

            // przełączenie aktywności przycisków w zależności od statusu połączenia
            buttonConnect.Enabled = true;
            buttonDisconnect.Enabled = false;
            buttonLoad.Enabled = false;
            buttonVerify.Enabled = false;
            buttonLoadCustom.Enabled = false;

            toolStripStatusLabel.Text = "Rozłączono z bazą";
        }

        // obsługa wyboru folderu ze skanami
        private void ButtonSelectDirectoryBlobs_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog
            {
                SelectedPath = IniFile.ReadIni("Files", "RecentFolder") // jako domyślny wybierz ostatnio wybrany folder
            };

            DialogResult result = fbd.ShowDialog(); // uruchom okno wyboru folderu

            if (result == DialogResult.OK)
            {
                IniFile.SaveIni("Files", "RecentFolder", fbd.SelectedPath); // jeśli wybrano folder zapisz go do pliku konfiguracyjnego jako ostatnio wybrany
                
                //todo zmienić w zależności od tego co jest ładowane
                string[] fileNames = Directory.GetFiles(fbd.SelectedPath, "*.*", SearchOption.AllDirectories); // wybierz wszystkie pliki włącznie z podfolderami

                fileNames = fileNames.Where(val => !val.EndsWith(".XML", StringComparison.OrdinalIgnoreCase)).ToArray();    //  pomiń pliki XML
                fileNames = fileNames.Where(val => !val.EndsWith(".WKT", StringComparison.OrdinalIgnoreCase)).ToArray();    //  pomiń pliki WKT
                //fileNames = fileNames.Where(val => !val.EndsWith(".BAK", StringComparison.OrdinalIgnoreCase)).ToArray();    //  pomiń pliki BAK

                //fileNames = fileNames.Where(val => val.Contains("ZARYSY_KATASTRALNE")).ToArray();    //  tylko pliki zawierające w nazwie

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

                int filesSize = _blobFiles.Aggregate(0, (current, bFile) => current + bFile.FileSize);

                toolStripStatusLabel.Text = $"Liczba wczytanych plików: {_blobFiles.Count}, o rozmiarze: {filesSize / 1024} MB";

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
            OracleTransaction transaction = _oracleConnection.BeginTransaction();    // transakcja ładowania plików

            OracleCommand command = _oracleConnection.CreateCommand();   // polecenie dla połączenia

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

                int filesSizeAll = _blobFiles.Aggregate(0, (current, bFile) => current + bFile.FileSize);

                // dla każdego wskazanego pliku
                foreach (BlobFile blobFile in _blobFiles)
                {
                    fileCounter++;      // licznik załadowanych plików

                    string fileFullName = blobFile.FullFileName;        // pełna nazwa pliku włączenie ze ścieżką
                    string fileShortName = blobFile.FileName;           // tylko nazwa pliku
                    int fileSize = blobFile.FileSize;                   // rozmiar pliku

                    filesSize += fileSize;      // zsumuj wielkość plików

                    toolStripStatusLabel.Text = $"Ładowanie pliku {fileCounter}/{_blobFiles.Count} [{filesSize / 1024}/{filesSizeAll / 1024} MB]: {fileFullName} [{Math.Round(fileSize / 1024.0, 2) } MB]";

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
                    command.CommandText = $"INSERT INTO blob.kdok_pliki (id_file, pieczec_pliku, typ_pliku, data, data_d) VALUES({idFileSq}, {idFileSq}, '{fileShortName}', :blobDataParameter, to_date('{dateTimePickerDataD.Text}', 'YYYY-MM-DD HH24:MI:SS'))";

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
                    int userId = Convert.ToInt32(textBoxUserId.Text);

                    command.CommandText = "INSERT INTO KDOK_WSK(ID_DOK, WL, ID_GR, ID_FILE, PATH, ID_RODZ_DOK, OPIS, USER_ID, DATA_D, USERM_ID, DATA_M) " +
                                          $"VALUES({idDokSq}, 'operat', {idGr}, {idFileSq}, '{path}', {idRodzDok}, '', {userId}, to_date('{dateTimePickerDataD.Text}', 'YYYY-MM-DD HH24:MI:SS'), {userId}, to_date('{dateTimePickerDataD.Text}', 'YYYY-MM-DD HH24:MI:SS'))";

                    command.ExecuteNonQuery();

                    // ----------------------------------------------------------------------------

                    int percentage = (fileCounter * 100)/ dataGridView.Rows.Count;      // oblicz procentowe zaawansowanie ładowania
                    loadBlobBackgroundWorker.ReportProgress(percentage);                // zaraportuj zaawansowanie

                }

                transaction.Commit();       // zatwierdzenie transakcji ładowania

                toolStripStatusLabel.Text = $"Załadowano {dataGridView.Rows.Count} plików o łącznym rozmiarze {filesSize / 1024} MB";
            }
            catch (Exception exception)
            {
                transaction.Rollback();     // w przypadku wystąpienia błędu wycofaj transakcje

                MessageBox.Show(exception.Message, "Bład!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                blobDataParameter.Dispose();
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

            MessageBox.Show("Koniec");
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

            toolStripStatusLabel.Text = $"Liczba błędów: {_blobFiles.Count}";
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

            toolStripStatusLabel.Text = "Pobieranie listy operatów...";

            _dbOperaty.Clear(); // wyczyść listę obiektów

            OracleCommand command = _oracleConnection.CreateCommand();
            command.CommandText = "SELECT idop, sygnatura, idmaterialu FROM ewid4.osr_operat ORDER BY idop";
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

            toolStripStatusLabel.Text = "Pobieranie rodzajów dokumentów...";

            _dbKdokRodz.Clear(); // wyczyść listę obiektów

            command = _oracleConnection.CreateCommand();
            command.CommandText = "SELECT id_rodz_dok, opis, prefix, nazdok_id, gml_val FROM ewid4.kdok_rodz ORDER BY id_rodz_dok";
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

                    _dbKdokRodz.Add(kdokRodz);
                }

                if (checkBoxCustomDict.Checked)
                {
                    List<string> dictionaryList = File.ReadLines("KdokRodzCustom.slo", Encoding.UTF8).ToList();

                    for (int i = 1; i < dictionaryList.Count; i++)
                    {
                        string[] dictionaryItem = dictionaryList[i].Split(';');

                        _dbKdokRodz.Add(new KdokRodz {IdRodzDok = int.Parse(dictionaryItem[0]), Opis = "plik danych inny", Prefix = dictionaryItem[1], NazdokId = dictionaryItem[2], GmlVal = "inny"});
                    }
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

            toolStripStatusLabel.Text = "Sprawdzanie numerów operatów..";

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
                        MessageBox.Show("Więcej niż jeden operat o nazwie: " + blobFile.IdMaterialu);
                        break;
                }
            }

            // ------------------------------------------------------------------------------------
            // Sprawdź czy pliki mają właściwie prefiksy
            // ------------------------------------------------------------------------------------

            toolStripStatusLabel.Text = "Pobieranie prefiksów...";

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
                        blobFile.Status += ", błąd prefiksu";
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

            toolStripStatusLabel.Text = $"Liczba błędów: {errorCount} / {_blobFiles.Count}";
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

        private void ButtonLoadCustom_Click(object sender, EventArgs e)
        {
            buttonLoadCustom.Enabled = false;     // wyłącz przycisk ładowania plików

            loadBlobCustomBackgroundWorker.RunWorkerAsync();
        }

        private void LoadBlobCustomBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            OracleTransaction transaction = _oracleConnection.BeginTransaction();    // transakcja ładowania plików

            OracleCommand command = _oracleConnection.CreateCommand();   // polecenie dla połączenia

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

                int filesSizeAll = _blobFiles.Aggregate(0, (current, bFile) => current + bFile.FileSize);

                // dla każdego wskazanego pliku
                foreach (BlobFile blobFile in _blobFiles)
                {
                    fileCounter++;      // licznik załadowanych plików

                    string fileFullName = blobFile.FullFileName;        // pełna nazwa pliku włączenie ze ścieżką
                    string fileShortName = blobFile.FileName;           // tylko nazwa pliku
                    int fileSize = blobFile.FileSize;                   // rozmiar pliku

                    filesSize += fileSize;      // zsumuj wielkość plików

                    toolStripStatusLabel.Text = $"Ładowanie pliku {fileCounter}/{_blobFiles.Count} [{filesSize / 1024}/{filesSizeAll / 1024} MB]: {fileFullName} [{Math.Round(fileSize / 1024.0, 2) } MB]";

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
                    command.CommandText = $"INSERT INTO blob.kdok_pliki (id_file, pieczec_pliku, typ_pliku, data, data_d) VALUES({idFileSq}, {idFileSq}, '{fileShortName}', :blobDataParameter, to_date('{dateTimePickerDataD.Text}', 'YYYY-MM-DD HH24:MI:SS'))";

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

                    int idGr = Convert.ToInt32(textBoxDokId.Text);
                    string path = blobFile.FullFileName;
                    
                    int idRodzDok = Convert.ToInt32(textBoxIdRodzDok.Text);

                    if (idRodzDok == 0)
                    {
                        idRodzDok = blobFile.PrefixId;
                    }

                    int userId = Convert.ToInt32(textBoxUserId.Text);

                    string wl = string.Empty;
                    
                    Invoke(new MethodInvoker(() => wl = comboBoxWl.Text));

                    wl = wl.Substring(0, wl.IndexOf(" -", StringComparison.Ordinal));

                    command.CommandText = "INSERT INTO KDOK_WSK(ID_DOK, WL, ID_GR, ID_FILE, PATH, ID_RODZ_DOK, OPIS, USER_ID, DATA_D, USERM_ID, DATA_M) " +
                                          $"VALUES({idDokSq}, '{wl}', {idGr}, {idFileSq}, '{path}', {idRodzDok}, '', {userId}, to_date('{dateTimePickerDataD.Text}', 'YYYY-MM-DD HH24:MI:SS'), {userId}, to_date('{dateTimePickerDataD.Text}', 'YYYY-MM-DD HH24:MI:SS'))";

                    command.ExecuteNonQuery();

                    // ----------------------------------------------------------------------------

                    int percentage = (fileCounter * 100)/ dataGridView.Rows.Count;      // oblicz procentowe zaawansowanie ładowania
                    loadBlobCustomBackgroundWorker.ReportProgress(percentage);                // zaraportuj zaawansowanie
                }

                transaction.Commit();       // zatwierdzenie transakcji ładowania

                toolStripStatusLabel.Text = $"Załadowano {dataGridView.Rows.Count} plików o łącznym rozmiarze {filesSize / 1024} MB";
            }
            catch (Exception exception)
            {
                transaction.Rollback();     // w przypadku wystąpienia błędu wycofaj transakcje
                toolStripStatusLabel.Text = exception.Message + "\n" + command.CommandText;
            }
            finally
            {
                blobDataParameter.Dispose();
                command.Dispose();
                transaction.Dispose();
            }
        }

        private void LoadBlobCustomBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar.Value = e.ProgressPercentage;
        }

        private void LoadBlobCustomBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            buttonLoadCustom.Enabled = true;     // wyłącz przycisk ładowania plików

            MessageBox.Show("Koniec");
        }

        private void ToolStripMenuItemCopy_Click(object sender, EventArgs e)
        {
            if (dataGridView.GetCellCount(DataGridViewElementStates.Selected) > 0)
            {
                    // Add the selection to the clipboard.
                    Clipboard.SetDataObject(dataGridView.GetClipboardContent() ?? throw new InvalidOperationException());
            }
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            MyLicense license = LicenseHandler.ReadLicenseFile(out LicenseStatus licStatus, out string validationMsg);

            switch (licStatus)
            {
                case LicenseStatus.Undefined:

                    MessageBox.Show("Brak pliku z licencją!\nIdentyfikator komputera: " + LicenseHandler.GenerateUid("BlobLoader"), Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Application.Exit();
                    break;

                case LicenseStatus.Valid:

                    toolStripStatusLabel.Text = $"Licencja typu: '{license.Type}', ważna do: {license.LicenseEnd}";
                    break;

                case LicenseStatus.Invalid:
                case LicenseStatus.Cracked:
                case LicenseStatus.Expired:

                    MessageBox.Show(validationMsg, Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Application.Exit();
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void Control_TextChanged(object sender, EventArgs e)
        {
            string controlName = ((Control) sender).Name;
            string controlVal = ((Control) sender).Text;

            switch (controlName)
            {
                case "textBoxHost":
                case "textBoxDb":
                    IniFile.SaveIni("Database", controlName, controlVal);
                break;

                case "textBoxUser":
                case "textBoxPass":
                    IniFile.SaveIni("Database", controlName, SecureText.Protect(controlVal));
                    break;

                default:
                    IniFile.SaveIni("Params", controlName, controlVal);
                    break;
            }
        }

        private void CheckBoxCustomDict_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void FormMain_Resize(object sender, EventArgs e)
        {
            toolStripStatusLabel.Size = new Size(((Form) sender).Size.Width - 50, 23);
        }
    }
}

