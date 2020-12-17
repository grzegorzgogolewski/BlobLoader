namespace BlobLoader
{
    partial class FormMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.textBoxHost = new System.Windows.Forms.TextBox();
            this.textBoxDb = new System.Windows.Forms.TextBox();
            this.textBoxUser = new System.Windows.Forms.TextBox();
            this.textBoxPass = new System.Windows.Forms.TextBox();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonLoad = new System.Windows.Forms.Button();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonDisconnect = new System.Windows.Forms.Button();
            this.buttonSelectDirectoryBlobs = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuStripDataGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItemCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.buttonVerify = new System.Windows.Forms.Button();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.loadBlobBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBoxConnection = new System.Windows.Forms.GroupBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.labelUser = new System.Windows.Forms.Label();
            this.labelDatabase = new System.Windows.Forms.Label();
            this.labelHost = new System.Windows.Forms.Label();
            this.buttonFilter = new System.Windows.Forms.Button();
            this.verifyBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBoxParams = new System.Windows.Forms.GroupBox();
            this.labelDataD = new System.Windows.Forms.Label();
            this.dateTimePickerDataD = new System.Windows.Forms.DateTimePicker();
            this.textBoxUserId = new System.Windows.Forms.TextBox();
            this.labelUserId = new System.Windows.Forms.Label();
            this.checkBoxCustomDict = new System.Windows.Forms.CheckBox();
            this.textBoxIdRodzDok = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxDokId = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonLoadCustom = new System.Windows.Forms.Button();
            this.loadBlobCustomBackgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelWl = new System.Windows.Forms.Label();
            this.comboBoxWl = new System.Windows.Forms.ComboBox();
            this.groupBoxKontrola = new System.Windows.Forms.GroupBox();
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.contextMenuStripDataGrid.SuspendLayout();
            this.groupBoxConnection.SuspendLayout();
            this.groupBoxParams.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBoxKontrola.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxHost
            // 
            this.textBoxHost.Location = new System.Drawing.Point(83, 19);
            this.textBoxHost.Name = "textBoxHost";
            this.textBoxHost.Size = new System.Drawing.Size(115, 20);
            this.textBoxHost.TabIndex = 0;
            this.textBoxHost.TextChanged += new System.EventHandler(this.Control_TextChanged);
            // 
            // textBoxDb
            // 
            this.textBoxDb.Location = new System.Drawing.Point(83, 45);
            this.textBoxDb.Name = "textBoxDb";
            this.textBoxDb.Size = new System.Drawing.Size(115, 20);
            this.textBoxDb.TabIndex = 1;
            this.textBoxDb.TextChanged += new System.EventHandler(this.Control_TextChanged);
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(83, 71);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(115, 20);
            this.textBoxUser.TabIndex = 2;
            this.textBoxUser.TextChanged += new System.EventHandler(this.Control_TextChanged);
            // 
            // textBoxPass
            // 
            this.textBoxPass.Location = new System.Drawing.Point(83, 97);
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.PasswordChar = '*';
            this.textBoxPass.Size = new System.Drawing.Size(115, 20);
            this.textBoxPass.TabIndex = 3;
            this.textBoxPass.TextChanged += new System.EventHandler(this.Control_TextChanged);
            // 
            // buttonConnect
            // 
            this.buttonConnect.Location = new System.Drawing.Point(6, 123);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.Size = new System.Drawing.Size(90, 25);
            this.buttonConnect.TabIndex = 4;
            this.buttonConnect.Text = "Połącz";
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.ButtonConnect_Click);
            // 
            // buttonLoad
            // 
            this.buttonLoad.Location = new System.Drawing.Point(12, 373);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(204, 25);
            this.buttonLoad.TabIndex = 5;
            this.buttonLoad.Text = "Ładuj";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.ButtonLoad_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 572);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1292, 22);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            this.statusStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip_ItemClicked);
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.AutoSize = false;
            this.toolStripStatusLabel.BackColor = System.Drawing.SystemColors.Control;
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(1250, 17);
            this.toolStripStatusLabel.Text = "Gotowy";
            this.toolStripStatusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(108, 123);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(90, 25);
            this.buttonDisconnect.TabIndex = 7;
            this.buttonDisconnect.Text = "Rozłącz";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.ButtonDisconnect_Click);
            // 
            // buttonSelectDirectoryBlobs
            // 
            this.buttonSelectDirectoryBlobs.Location = new System.Drawing.Point(12, 176);
            this.buttonSelectDirectoryBlobs.Name = "buttonSelectDirectoryBlobs";
            this.buttonSelectDirectoryBlobs.Size = new System.Drawing.Size(204, 25);
            this.buttonSelectDirectoryBlobs.TabIndex = 8;
            this.buttonSelectDirectoryBlobs.Text = "Wskaż katalog ze skanami";
            this.buttonSelectDirectoryBlobs.UseVisualStyleBackColor = true;
            this.buttonSelectDirectoryBlobs.Click += new System.EventHandler(this.ButtonSelectDirectoryBlobs_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            this.dataGridView.ContextMenuStrip = this.contextMenuStripDataGrid;
            this.dataGridView.Location = new System.Drawing.Point(230, 17);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(1050, 526);
            this.dataGridView.TabIndex = 10;
            // 
            // contextMenuStripDataGrid
            // 
            this.contextMenuStripDataGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemCopy});
            this.contextMenuStripDataGrid.Name = "contextMenuStripDataGrid";
            this.contextMenuStripDataGrid.Size = new System.Drawing.Size(109, 26);
            // 
            // toolStripMenuItemCopy
            // 
            this.toolStripMenuItemCopy.Name = "toolStripMenuItemCopy";
            this.toolStripMenuItemCopy.Size = new System.Drawing.Size(108, 22);
            this.toolStripMenuItemCopy.Text = "Kopiuj";
            this.toolStripMenuItemCopy.Click += new System.EventHandler(this.ToolStripMenuItemCopy_Click);
            // 
            // buttonVerify
            // 
            this.buttonVerify.Location = new System.Drawing.Point(6, 45);
            this.buttonVerify.Name = "buttonVerify";
            this.buttonVerify.Size = new System.Drawing.Size(80, 25);
            this.buttonVerify.TabIndex = 11;
            this.buttonVerify.Text = "Weryfikuj";
            this.buttonVerify.UseVisualStyleBackColor = true;
            this.buttonVerify.Click += new System.EventHandler(this.ButtonVerify_Click);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 549);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1292, 23);
            this.progressBar.Step = 1;
            this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar.TabIndex = 12;
            // 
            // loadBlobBackgroundWorker
            // 
            this.loadBlobBackgroundWorker.WorkerReportsProgress = true;
            this.loadBlobBackgroundWorker.WorkerSupportsCancellation = true;
            this.loadBlobBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.LoadBlobBackgroundWorkerOnDoWork);
            this.loadBlobBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.LoadBlobBackgroundWorkerOnProgressChanged);
            this.loadBlobBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.LoadBlobBackgroundWorkerOnRunWorkerCompleted);
            // 
            // groupBoxConnection
            // 
            this.groupBoxConnection.Controls.Add(this.labelPassword);
            this.groupBoxConnection.Controls.Add(this.labelUser);
            this.groupBoxConnection.Controls.Add(this.labelDatabase);
            this.groupBoxConnection.Controls.Add(this.labelHost);
            this.groupBoxConnection.Controls.Add(this.textBoxHost);
            this.groupBoxConnection.Controls.Add(this.buttonDisconnect);
            this.groupBoxConnection.Controls.Add(this.textBoxDb);
            this.groupBoxConnection.Controls.Add(this.textBoxUser);
            this.groupBoxConnection.Controls.Add(this.textBoxPass);
            this.groupBoxConnection.Controls.Add(this.buttonConnect);
            this.groupBoxConnection.Location = new System.Drawing.Point(12, 12);
            this.groupBoxConnection.Name = "groupBoxConnection";
            this.groupBoxConnection.Size = new System.Drawing.Size(204, 158);
            this.groupBoxConnection.TabIndex = 13;
            this.groupBoxConnection.TabStop = false;
            this.groupBoxConnection.Text = "Parametry połączenia";
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(6, 100);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(39, 13);
            this.labelPassword.TabIndex = 16;
            this.labelPassword.Text = "Hasło:";
            // 
            // labelUser
            // 
            this.labelUser.AutoSize = true;
            this.labelUser.Location = new System.Drawing.Point(6, 74);
            this.labelUser.Name = "labelUser";
            this.labelUser.Size = new System.Drawing.Size(65, 13);
            this.labelUser.TabIndex = 15;
            this.labelUser.Text = "Użytkownik:";
            // 
            // labelDatabase
            // 
            this.labelDatabase.AutoSize = true;
            this.labelDatabase.Location = new System.Drawing.Point(6, 48);
            this.labelDatabase.Name = "labelDatabase";
            this.labelDatabase.Size = new System.Drawing.Size(34, 13);
            this.labelDatabase.TabIndex = 14;
            this.labelDatabase.Text = "Baza:";
            // 
            // labelHost
            // 
            this.labelHost.AutoSize = true;
            this.labelHost.Location = new System.Drawing.Point(6, 22);
            this.labelHost.Name = "labelHost";
            this.labelHost.Size = new System.Drawing.Size(32, 13);
            this.labelHost.TabIndex = 0;
            this.labelHost.Text = "Host:";
            // 
            // buttonFilter
            // 
            this.buttonFilter.Location = new System.Drawing.Point(118, 45);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(80, 25);
            this.buttonFilter.TabIndex = 14;
            this.buttonFilter.Text = "Filtruj";
            this.buttonFilter.UseVisualStyleBackColor = true;
            this.buttonFilter.Click += new System.EventHandler(this.ButtonFilter_Click);
            // 
            // verifyBackgroundWorker
            // 
            this.verifyBackgroundWorker.WorkerReportsProgress = true;
            this.verifyBackgroundWorker.WorkerSupportsCancellation = true;
            this.verifyBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.VerifyBackgroundWorker_DoWork);
            this.verifyBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.VerifyBackgroundWorker_ProgressChanged);
            this.verifyBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.VerifyBackgroundWorker_RunWorkerCompleted);
            // 
            // groupBoxParams
            // 
            this.groupBoxParams.Controls.Add(this.labelDataD);
            this.groupBoxParams.Controls.Add(this.dateTimePickerDataD);
            this.groupBoxParams.Controls.Add(this.textBoxUserId);
            this.groupBoxParams.Controls.Add(this.labelUserId);
            this.groupBoxParams.Location = new System.Drawing.Point(12, 293);
            this.groupBoxParams.Name = "groupBoxParams";
            this.groupBoxParams.Size = new System.Drawing.Size(204, 74);
            this.groupBoxParams.TabIndex = 15;
            this.groupBoxParams.TabStop = false;
            this.groupBoxParams.Text = "Ustawienia";
            // 
            // labelDataD
            // 
            this.labelDataD.AutoSize = true;
            this.labelDataD.Location = new System.Drawing.Point(6, 51);
            this.labelDataD.Name = "labelDataD";
            this.labelDataD.Size = new System.Drawing.Size(43, 13);
            this.labelDataD.TabIndex = 19;
            this.labelDataD.Text = "data_d:";
            // 
            // dateTimePickerDataD
            // 
            this.dateTimePickerDataD.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePickerDataD.Location = new System.Drawing.Point(62, 45);
            this.dateTimePickerDataD.Name = "dateTimePickerDataD";
            this.dateTimePickerDataD.Size = new System.Drawing.Size(136, 20);
            this.dateTimePickerDataD.TabIndex = 18;
            this.dateTimePickerDataD.ValueChanged += new System.EventHandler(this.Control_TextChanged);
            // 
            // textBoxUserId
            // 
            this.textBoxUserId.Location = new System.Drawing.Point(62, 19);
            this.textBoxUserId.Name = "textBoxUserId";
            this.textBoxUserId.Size = new System.Drawing.Size(136, 20);
            this.textBoxUserId.TabIndex = 17;
            this.textBoxUserId.TextChanged += new System.EventHandler(this.Control_TextChanged);
            // 
            // labelUserId
            // 
            this.labelUserId.AutoSize = true;
            this.labelUserId.Location = new System.Drawing.Point(6, 22);
            this.labelUserId.Name = "labelUserId";
            this.labelUserId.Size = new System.Drawing.Size(44, 13);
            this.labelUserId.TabIndex = 17;
            this.labelUserId.Text = "user_id:";
            // 
            // checkBoxCustomDict
            // 
            this.checkBoxCustomDict.Location = new System.Drawing.Point(6, 19);
            this.checkBoxCustomDict.Name = "checkBoxCustomDict";
            this.checkBoxCustomDict.Size = new System.Drawing.Size(146, 20);
            this.checkBoxCustomDict.TabIndex = 20;
            this.checkBoxCustomDict.Text = "Niestandardowy słownik";
            this.checkBoxCustomDict.UseVisualStyleBackColor = true;
            this.checkBoxCustomDict.CheckedChanged += new System.EventHandler(this.CheckBoxCustomDict_CheckedChanged);
            // 
            // textBoxIdRodzDok
            // 
            this.textBoxIdRodzDok.Location = new System.Drawing.Point(83, 45);
            this.textBoxIdRodzDok.Name = "textBoxIdRodzDok";
            this.textBoxIdRodzDok.Size = new System.Drawing.Size(115, 20);
            this.textBoxIdRodzDok.TabIndex = 22;
            this.textBoxIdRodzDok.TextChanged += new System.EventHandler(this.Control_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 23;
            this.label2.Text = "id_rodz_dok:";
            // 
            // textBoxDokId
            // 
            this.textBoxDokId.Location = new System.Drawing.Point(83, 19);
            this.textBoxDokId.Name = "textBoxDokId";
            this.textBoxDokId.Size = new System.Drawing.Size(115, 20);
            this.textBoxDokId.TabIndex = 20;
            this.textBoxDokId.TextChanged += new System.EventHandler(this.Control_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 21;
            this.label1.Text = "dok_id:";
            // 
            // buttonLoadCustom
            // 
            this.buttonLoadCustom.Location = new System.Drawing.Point(12, 512);
            this.buttonLoadCustom.Name = "buttonLoadCustom";
            this.buttonLoadCustom.Size = new System.Drawing.Size(204, 25);
            this.buttonLoadCustom.TabIndex = 16;
            this.buttonLoadCustom.Text = "Ładuj custom";
            this.buttonLoadCustom.UseVisualStyleBackColor = true;
            this.buttonLoadCustom.Click += new System.EventHandler(this.ButtonLoadCustom_Click);
            // 
            // loadBlobCustomBackgroundWorker
            // 
            this.loadBlobCustomBackgroundWorker.WorkerReportsProgress = true;
            this.loadBlobCustomBackgroundWorker.WorkerSupportsCancellation = true;
            this.loadBlobCustomBackgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.LoadBlobCustomBackgroundWorker_DoWork);
            this.loadBlobCustomBackgroundWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.LoadBlobCustomBackgroundWorker_ProgressChanged);
            this.loadBlobCustomBackgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.LoadBlobCustomBackgroundWorker_RunWorkerCompleted);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelWl);
            this.groupBox1.Controls.Add(this.comboBoxWl);
            this.groupBox1.Controls.Add(this.textBoxDokId);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.textBoxIdRodzDok);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(12, 404);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(204, 102);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ustawienia custom";
            // 
            // labelWl
            // 
            this.labelWl.AutoSize = true;
            this.labelWl.Location = new System.Drawing.Point(57, 75);
            this.labelWl.Name = "labelWl";
            this.labelWl.Size = new System.Drawing.Size(20, 13);
            this.labelWl.TabIndex = 25;
            this.labelWl.Text = "wl:";
            // 
            // comboBoxWl
            // 
            this.comboBoxWl.FormattingEnabled = true;
            this.comboBoxWl.Items.AddRange(new object[] {
            "dok.rast. - inny materiał",
            "dzzgl - dokument przychodzący",
            "mapa - mapa",
            "operat - operat geodezyjny",
            "zglzm - zgłoszenie zmiany"});
            this.comboBoxWl.Location = new System.Drawing.Point(83, 72);
            this.comboBoxWl.Name = "comboBoxWl";
            this.comboBoxWl.Size = new System.Drawing.Size(115, 21);
            this.comboBoxWl.TabIndex = 24;
            this.comboBoxWl.SelectedIndexChanged += new System.EventHandler(this.Control_TextChanged);
            // 
            // groupBoxKontrola
            // 
            this.groupBoxKontrola.Controls.Add(this.checkBoxCustomDict);
            this.groupBoxKontrola.Controls.Add(this.buttonVerify);
            this.groupBoxKontrola.Controls.Add(this.buttonFilter);
            this.groupBoxKontrola.Location = new System.Drawing.Point(12, 207);
            this.groupBoxKontrola.Name = "groupBoxKontrola";
            this.groupBoxKontrola.Size = new System.Drawing.Size(204, 80);
            this.groupBoxKontrola.TabIndex = 21;
            this.groupBoxKontrola.TabStop = false;
            this.groupBoxKontrola.Text = "Weryfikacja";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1292, 594);
            this.Controls.Add(this.groupBoxKontrola);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.buttonLoadCustom);
            this.Controls.Add(this.buttonLoad);
            this.Controls.Add(this.groupBoxParams);
            this.Controls.Add(this.groupBoxConnection);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonSelectDirectoryBlobs);
            this.Controls.Add(this.statusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1300, 625);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.Shown += new System.EventHandler(this.FormMain_Shown);
            this.Resize += new System.EventHandler(this.FormMain_Resize);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.contextMenuStripDataGrid.ResumeLayout(false);
            this.groupBoxConnection.ResumeLayout(false);
            this.groupBoxConnection.PerformLayout();
            this.groupBoxParams.ResumeLayout(false);
            this.groupBoxParams.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBoxKontrola.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxHost;
        private System.Windows.Forms.TextBox textBoxDb;
        private System.Windows.Forms.TextBox textBoxUser;
        private System.Windows.Forms.TextBox textBoxPass;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.Button buttonDisconnect;
        private System.Windows.Forms.Button buttonSelectDirectoryBlobs;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.Button buttonVerify;
        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.GroupBox groupBoxConnection;
        private System.Windows.Forms.Label labelHost;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label labelUser;
        private System.Windows.Forms.Label labelDatabase;
        private System.Windows.Forms.Button buttonFilter;
        private System.ComponentModel.BackgroundWorker loadBlobBackgroundWorker;
        private System.ComponentModel.BackgroundWorker verifyBackgroundWorker;
        private System.Windows.Forms.GroupBox groupBoxParams;
        private System.Windows.Forms.TextBox textBoxUserId;
        private System.Windows.Forms.Label labelUserId;
        private System.Windows.Forms.DateTimePicker dateTimePickerDataD;
        private System.Windows.Forms.Label labelDataD;
        private System.Windows.Forms.TextBox textBoxIdRodzDok;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxDokId;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonLoadCustom;
        private System.ComponentModel.BackgroundWorker loadBlobCustomBackgroundWorker;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripDataGrid;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCopy;
        private System.Windows.Forms.CheckBox checkBoxCustomDict;
        private System.Windows.Forms.GroupBox groupBoxKontrola;
        private System.Windows.Forms.ComboBox comboBoxWl;
        private System.Windows.Forms.Label labelWl;
    }
}

