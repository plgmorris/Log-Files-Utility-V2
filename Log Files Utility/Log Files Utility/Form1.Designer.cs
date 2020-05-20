namespace Log_Files_Utility
{
    partial class Form1
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.backupSearchPattern = new System.Windows.Forms.TextBox();
            this.deleteBackupsButton = new System.Windows.Forms.Button();
            this.zipBackupsButton = new System.Windows.Forms.Button();
            this.startStopBackup = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.chooseFolderBackupButton = new System.Windows.Forms.Button();
            this.folderPathBackups = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.chooseLogsFolderTimer = new System.Windows.Forms.Timer(this.components);
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dummyAcceptButton = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.openFileButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.occurancesTree = new System.Windows.Forms.TreeView();
            this.searchLogsButton = new System.Windows.Forms.Button();
            this.searchTermTextBox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.searchingTimer = new System.Windows.Forms.Timer(this.components);
            this.chooseLogsFolderTimer2 = new System.Windows.Forms.Timer(this.components);
            this.zippingTimer = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.backupSearchPattern);
            this.groupBox1.Controls.Add(this.deleteBackupsButton);
            this.groupBox1.Controls.Add(this.zipBackupsButton);
            this.groupBox1.Controls.Add(this.startStopBackup);
            this.groupBox1.Location = new System.Drawing.Point(12, 98);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(487, 87);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Log Files Backup";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 52);
            this.label2.TabIndex = 7;
            this.label2.Text = "Input Search Pattern for File Names\r\nto backup. Default is \'.1 .log\'\r\nThis is sui" +
    "table for all our DS logs\r\nsuch as InDigoX and InQuire.";
            // 
            // backupSearchPattern
            // 
            this.backupSearchPattern.AcceptsReturn = true;
            this.backupSearchPattern.Location = new System.Drawing.Point(187, 26);
            this.backupSearchPattern.Name = "backupSearchPattern";
            this.backupSearchPattern.Size = new System.Drawing.Size(92, 20);
            this.backupSearchPattern.TabIndex = 6;
            this.backupSearchPattern.Text = ".1 .log";
            // 
            // deleteBackupsButton
            // 
            this.deleteBackupsButton.Enabled = false;
            this.deleteBackupsButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.deleteBackupsButton.Location = new System.Drawing.Point(386, 24);
            this.deleteBackupsButton.Name = "deleteBackupsButton";
            this.deleteBackupsButton.Size = new System.Drawing.Size(92, 23);
            this.deleteBackupsButton.TabIndex = 2;
            this.deleteBackupsButton.Text = "Delete Backups";
            this.deleteBackupsButton.UseVisualStyleBackColor = true;
            this.deleteBackupsButton.Click += new System.EventHandler(this.deleteBackupsButton_Click);
            // 
            // zipBackupsButton
            // 
            this.zipBackupsButton.Enabled = false;
            this.zipBackupsButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.zipBackupsButton.Location = new System.Drawing.Point(386, 53);
            this.zipBackupsButton.Name = "zipBackupsButton";
            this.zipBackupsButton.Size = new System.Drawing.Size(92, 23);
            this.zipBackupsButton.TabIndex = 1;
            this.zipBackupsButton.Tag = "zipLogs";
            this.zipBackupsButton.Text = "Zip Backpus";
            this.zipBackupsButton.UseVisualStyleBackColor = true;
            this.zipBackupsButton.Click += new System.EventHandler(this.zipBackupsButton_Click);
            // 
            // startStopBackup
            // 
            this.startStopBackup.Enabled = false;
            this.startStopBackup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.startStopBackup.Location = new System.Drawing.Point(288, 24);
            this.startStopBackup.Name = "startStopBackup";
            this.startStopBackup.Size = new System.Drawing.Size(92, 23);
            this.startStopBackup.TabIndex = 0;
            this.startStopBackup.Tag = "";
            this.startStopBackup.Text = "Start";
            this.startStopBackup.UseVisualStyleBackColor = true;
            this.startStopBackup.Click += new System.EventHandler(this.startStopBackup_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(209, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Choose a folder where the logs are located";
            // 
            // chooseFolderBackupButton
            // 
            this.chooseFolderBackupButton.Location = new System.Drawing.Point(287, 39);
            this.chooseFolderBackupButton.Name = "chooseFolderBackupButton";
            this.chooseFolderBackupButton.Size = new System.Drawing.Size(93, 23);
            this.chooseFolderBackupButton.TabIndex = 4;
            this.chooseFolderBackupButton.Text = "Choose Folder";
            this.chooseFolderBackupButton.UseVisualStyleBackColor = true;
            this.chooseFolderBackupButton.Click += new System.EventHandler(this.chooseFolderBackupButton_Click);
            // 
            // folderPathBackups
            // 
            this.folderPathBackups.AcceptsReturn = true;
            this.folderPathBackups.Location = new System.Drawing.Point(6, 41);
            this.folderPathBackups.Name = "folderPathBackups";
            this.folderPathBackups.Size = new System.Drawing.Size(261, 20);
            this.folderPathBackups.TabIndex = 3;
            this.folderPathBackups.TextChanged += new System.EventHandler(this.folderPathBackups_TextChanged);
            this.folderPathBackups.KeyUp += new System.Windows.Forms.KeyEventHandler(this.folderPathBackups_KeyDown);
            // 
            // chooseLogsFolderTimer
            // 
            this.chooseLogsFolderTimer.Enabled = true;
            this.chooseLogsFolderTimer.Interval = 2500;
            this.chooseLogsFolderTimer.Tick += new System.EventHandler(this.chooseLogsFolderTimer_Tick);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.folderPathBackups);
            this.groupBox2.Controls.Add(this.chooseFolderBackupButton);
            this.groupBox2.Location = new System.Drawing.Point(12, 13);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(487, 81);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Choose Logs Folder";
            // 
            // dummyAcceptButton
            // 
            this.dummyAcceptButton.Enabled = false;
            this.dummyAcceptButton.Location = new System.Drawing.Point(299, -1);
            this.dummyAcceptButton.Name = "dummyAcceptButton";
            this.dummyAcceptButton.Size = new System.Drawing.Size(122, 23);
            this.dummyAcceptButton.TabIndex = 3;
            this.dummyAcceptButton.Text = "dummyAcceptButton";
            this.dummyAcceptButton.UseVisualStyleBackColor = true;
            this.dummyAcceptButton.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.openFileButton);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.occurancesTree);
            this.groupBox3.Controls.Add(this.searchLogsButton);
            this.groupBox3.Controls.Add(this.searchTermTextBox);
            this.groupBox3.Controls.Add(this.label4);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Location = new System.Drawing.Point(12, 192);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(487, 248);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Search log files for string pattern";
            // 
            // openFileButton
            // 
            this.openFileButton.Enabled = false;
            this.openFileButton.Location = new System.Drawing.Point(12, 214);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(75, 23);
            this.openFileButton.TabIndex = 10;
            this.openFileButton.Text = "Open File";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 160);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(214, 39);
            this.label5.TabIndex = 9;
            this.label5.Text = "Now select a file on the right and click open\r\nto view in notepad++ or the files\r" +
    "\ndefault program.";
            // 
            // occurancesTree
            // 
            this.occurancesTree.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.occurancesTree.Enabled = false;
            this.occurancesTree.Location = new System.Drawing.Point(242, 19);
            this.occurancesTree.Name = "occurancesTree";
            this.occurancesTree.Size = new System.Drawing.Size(235, 222);
            this.occurancesTree.TabIndex = 8;
            this.occurancesTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.occurancesTree_NodeMouseClick);
            this.occurancesTree.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.occurancesTree_NodeMouseDoubleClick);
            // 
            // searchLogsButton
            // 
            this.searchLogsButton.Enabled = false;
            this.searchLogsButton.Location = new System.Drawing.Point(12, 121);
            this.searchLogsButton.Name = "searchLogsButton";
            this.searchLogsButton.Size = new System.Drawing.Size(75, 23);
            this.searchLogsButton.TabIndex = 7;
            this.searchLogsButton.Text = "Search";
            this.searchLogsButton.UseVisualStyleBackColor = true;
            this.searchLogsButton.Click += new System.EventHandler(this.searchLogsButton_Click);
            // 
            // searchTermTextBox
            // 
            this.searchTermTextBox.AcceptsReturn = true;
            this.searchTermTextBox.Enabled = false;
            this.searchTermTextBox.Location = new System.Drawing.Point(93, 85);
            this.searchTermTextBox.Name = "searchTermTextBox";
            this.searchTermTextBox.Size = new System.Drawing.Size(107, 20);
            this.searchTermTextBox.TabIndex = 5;
            this.searchTermTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            this.searchTermTextBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.searchTermTextBox_KeyUp);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 88);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Search Pattern";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(227, 52);
            this.label3.TabIndex = 1;
            this.label3.Text = "Please choose a folder where the logs are first.\r\nThen enter your search term bel" +
    "ow\r\nsuch as AE Title or IP Address.\r\nThis wil also search Subfolders.";
            // 
            // searchingTimer
            // 
            this.searchingTimer.Enabled = true;
            this.searchingTimer.Interval = 2000;
            this.searchingTimer.Tick += new System.EventHandler(this.searchingTimer_Tick);
            // 
            // chooseLogsFolderTimer2
            // 
            this.chooseLogsFolderTimer2.Interval = 500;
            this.chooseLogsFolderTimer2.Tick += new System.EventHandler(this.chooseLogsFolderTimer2_Tick);
            // 
            // zippingTimer
            // 
            this.zippingTimer.Interval = 500;
            this.zippingTimer.Tick += new System.EventHandler(this.zippingTimer_Tick);
            // 
            // Form1
            // 
            this.AcceptButton = this.dummyAcceptButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(510, 446);
            this.Controls.Add(this.dummyAcceptButton);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.MinimumSize = new System.Drawing.Size(526, 455);
            this.Name = "Form1";
            this.Text = "Log File Utility";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button deleteBackupsButton;
        private System.Windows.Forms.Button zipBackupsButton;
        private System.Windows.Forms.Button startStopBackup;
        private System.Windows.Forms.Button chooseFolderBackupButton;
        private System.Windows.Forms.TextBox folderPathBackups;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox backupSearchPattern;
        private System.Windows.Forms.Timer chooseLogsFolderTimer;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox searchTermTextBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button searchLogsButton;
        private System.Windows.Forms.Timer searchingTimer;
        private System.Windows.Forms.TreeView occurancesTree;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button dummyAcceptButton;
        private System.Windows.Forms.Timer chooseLogsFolderTimer2;
        private System.Windows.Forms.Timer zippingTimer;
    }
}

