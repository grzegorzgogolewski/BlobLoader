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
            this.oracleConnection = new Oracle.ManagedDataAccess.Client.OracleConnection();
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
            this.statusStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.groupBoxConnection.SuspendLayout();
            this.SuspendLayout();
            // 
            // oracleConnection
            // 
            this.oracleConnection.Credential = null;
            // 
            // textBoxHost
            // 
            this.textBoxHost.Location = new System.Drawing.Point(77, 19);
            this.textBoxHost.Name = "textBoxHost";
            this.textBoxHost.Size = new System.Drawing.Size(115, 20);
            this.textBoxHost.TabIndex = 0;
            this.textBoxHost.Text = "127.0.0.1";
            // 
            // textBoxDb
            // 
            this.textBoxDb.Location = new System.Drawing.Point(77, 45);
            this.textBoxDb.Name = "textBoxDb";
            this.textBoxDb.Size = new System.Drawing.Size(115, 20);
            this.textBoxDb.TabIndex = 1;
            this.textBoxDb.Text = "ORCL";
            // 
            // textBoxUser
            // 
            this.textBoxUser.Location = new System.Drawing.Point(77, 71);
            this.textBoxUser.Name = "textBoxUser";
            this.textBoxUser.Size = new System.Drawing.Size(115, 20);
            this.textBoxUser.TabIndex = 2;
            this.textBoxUser.Text = "ewid4";
            // 
            // textBoxPass
            // 
            this.textBoxPass.Location = new System.Drawing.Point(77, 97);
            this.textBoxPass.Name = "textBoxPass";
            this.textBoxPass.PasswordChar = '*';
            this.textBoxPass.Size = new System.Drawing.Size(115, 20);
            this.textBoxPass.TabIndex = 3;
            this.textBoxPass.Text = "ewid4";
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
            this.buttonLoad.Location = new System.Drawing.Point(18, 275);
            this.buttonLoad.Name = "buttonLoad";
            this.buttonLoad.Size = new System.Drawing.Size(186, 25);
            this.buttonLoad.TabIndex = 5;
            this.buttonLoad.Text = "Ładuj";
            this.buttonLoad.UseVisualStyleBackColor = true;
            this.buttonLoad.Click += new System.EventHandler(this.ButtonLoad_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 547);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(1492, 22);
            this.statusStrip.TabIndex = 6;
            this.statusStrip.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(48, 17);
            this.toolStripStatusLabel.Text = "Gotowy";
            // 
            // buttonDisconnect
            // 
            this.buttonDisconnect.Location = new System.Drawing.Point(102, 123);
            this.buttonDisconnect.Name = "buttonDisconnect";
            this.buttonDisconnect.Size = new System.Drawing.Size(90, 25);
            this.buttonDisconnect.TabIndex = 7;
            this.buttonDisconnect.Text = "Rozłącz";
            this.buttonDisconnect.UseVisualStyleBackColor = true;
            this.buttonDisconnect.Click += new System.EventHandler(this.ButtonDisconnect_Click);
            // 
            // buttonSelectDirectoryBlobs
            // 
            this.buttonSelectDirectoryBlobs.Location = new System.Drawing.Point(18, 188);
            this.buttonSelectDirectoryBlobs.Name = "buttonSelectDirectoryBlobs";
            this.buttonSelectDirectoryBlobs.Size = new System.Drawing.Size(186, 25);
            this.buttonSelectDirectoryBlobs.TabIndex = 8;
            this.buttonSelectDirectoryBlobs.Text = "Wskaż katalog ze skanami";
            this.buttonSelectDirectoryBlobs.UseVisualStyleBackColor = true;
            this.buttonSelectDirectoryBlobs.Click += new System.EventHandler(this.ButtonSelectDirectoryBlobs_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.Location = new System.Drawing.Point(222, 17);
            this.dataGridView.MultiSelect = false;
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(1258, 501);
            this.dataGridView.TabIndex = 10;
            // 
            // buttonVerify
            // 
            this.buttonVerify.Location = new System.Drawing.Point(18, 230);
            this.buttonVerify.Name = "buttonVerify";
            this.buttonVerify.Size = new System.Drawing.Size(90, 25);
            this.buttonVerify.TabIndex = 11;
            this.buttonVerify.Text = "Weryfikuj";
            this.buttonVerify.UseVisualStyleBackColor = true;
            this.buttonVerify.Click += new System.EventHandler(this.ButtonVerify_Click);
            // 
            // progressBar
            // 
            this.progressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar.Location = new System.Drawing.Point(0, 524);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(1492, 23);
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
            this.buttonFilter.Location = new System.Drawing.Point(114, 230);
            this.buttonFilter.Name = "buttonFilter";
            this.buttonFilter.Size = new System.Drawing.Size(90, 25);
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
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1492, 569);
            this.Controls.Add(this.buttonFilter);
            this.Controls.Add(this.groupBoxConnection);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.buttonVerify);
            this.Controls.Add(this.dataGridView);
            this.Controls.Add(this.buttonSelectDirectoryBlobs);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.buttonLoad);
            this.Name = "FormMain";
            this.Text = "FormMain";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.groupBoxConnection.ResumeLayout(false);
            this.groupBoxConnection.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Oracle.ManagedDataAccess.Client.OracleConnection oracleConnection;
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
    }
}

