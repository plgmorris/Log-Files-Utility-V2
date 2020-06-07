using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

namespace Log_Files_Utility
{
    public partial class Form1 : Form
    {
        private BackupUtil backup;
        private Thread backupUtilStartThread;
        private Thread searchFilesThread;
        private Thread zippingThread;
        private LogFileUtil logFileUtil;
        bool chooseFolderTimerRunning;

        public Form1()
        {
            InitializeComponent();

            backup = new BackupUtil();
            backupUtilStartThread = null;
            logFileUtil = new LogFileUtil();
            chooseFolderTimerRunning = false;
        }

        /* Choose Folder and Validate ------------------------------------------------ */

        private void chooseFolderBackupButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("inside chooseFolderBackupButton_Click");
            folderBrowserDialog1.ShowDialog();
            Console.WriteLine("folder chosen");
            if (validateFolder(folderBrowserDialog1.SelectedPath))
            {
                folderPathBackups.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void folderPathBackups_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine("folderPathBackups_KeyDown. key pressed: " + e.KeyCode.ToString());
            if (e.KeyCode == Keys.Enter && !chooseFolderTimerRunning)
            {
                chooseLogsFolderTimer.Stop();
                validateFolder(folderPathBackups.Text);
            }
        }

        private void folderPathBackups_TextChanged(object sender, EventArgs e)
        {
            chooseLogsFolderTimer.Stop();
            chooseLogsFolderTimer.Start();
        }

        private void chooseLogsFolderTimer_Tick(object sender, EventArgs e)
        {
            chooseLogsFolderTimer.Stop();
            validateFolder(folderPathBackups.Text);
        }

        private bool validateFolder(string folder)
        {
            if (LogFileUtil.isFolderValid(folder))
            {
                DialogResult dialogResult = DialogResult.Yes;
                //if (backup.isNewDirSet() && backup.getFolder() != folder && !backup.isFolderStringEmpty())
                //{
                //    dialogResult = MessageBox.Show("The path has changed, we will need to delete any current backed up \n" +
                //    "log files first.\nCan we continue?", "Delete Current Logs", MessageBoxButtons.YesNo);
                //}
                if (dialogResult == DialogResult.Yes)
                {
                    chooseFolderTimerRunning = true;
                    chooseLogsFolderTimer2.Start();
                    openFolder.Enabled = true;
                    if (backupSearchPattern.TextLength > 0)
                    {
                        startStopBackup.Enabled = true;
                    }
                    searchTermTextBox.Enabled = true;
                    zipBackupsButton.Enabled = true;
                    return true;
                }
                return false;
            }
            else
            {
                openFolder.Enabled = false;
                startStopBackup.Enabled = false;
                searchTermTextBox.Enabled = false;
                zipBackupsButton.Enabled = false;
                return false;
            }
        }

        /* ---------------------------------------------------------------------------- */

        private void startStopBackup_Click(object sender, EventArgs e)
        {
            if (startStopBackup.Text == "Start")
            {
                backup.setFolder(folderPathBackups.Text);
                backup.setSearchPattern(backupSearchPattern.Text);
                backupUtilStartThread = new Thread(() => backup.startBackup(30));
                backupUtilStartThread.Start();

                backupSearchPattern.Enabled = false;
                folderPathBackups.Enabled = false;
                deleteBackupsButton.Enabled = false;
                chooseFolderBackupButton.Enabled = false;
                zipBackupsButton.Enabled = true;
                startStopBackup.Text = "Stop";
            }
            else if (startStopBackup.Text == "Stop")
            {
                backup.stopBackup();
                startStopBackup.Text = "Stopping";
                startStopBackup.Enabled = false;
                while (backupUtilStartThread.IsAlive) { }
                startStopBackup.Text = "Start";
                startStopBackup.Enabled = true;
                backupSearchPattern.Enabled = true;
                folderPathBackups.Enabled = true;
                if (backup.isNewDirSet()) deleteBackupsButton.Enabled = true;
                chooseFolderBackupButton.Enabled = true;
            }
        }

        private void openFolder_Click(object sender, EventArgs e)
        {
            bool s;
            try
            {
                s = LogFileUtil.openFolder(folderPathBackups.Text);
                if (s == false)
                {
                    MessageBox.Show("Folder does not exist. Please enter a new folder", "Folder Doesn't Exist", MessageBoxButtons.OK);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("An error occurred while opening folder.", "Error Occurred", MessageBoxButtons.OK);
            }
        }

        /* Zipping section ------------------------------------------------------------- */

        private void zipBackupsButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = ".zip";
            saveFileDialog1.Filter = "Zip Files (*.zip)|*.zip";
            saveFileDialog1.ShowDialog();
            zippingThread = new Thread(() => zipFiles(saveFileDialog1.FileName, folderPathBackups.Text));
            zipBackupsButton.Enabled = false;
            zipBackupsButton.Text = "Zipping Up";
            zippingThread.Start();
            zippingTimer.Start();
        }

        private void zipFiles(string destFile, string dir)
        {
            try
            {
                BackupUtil.zipUpFiles(destFile, dir);
            }
            catch (DllNotFoundException ex)
            {
                MessageBox.Show("Could Not Load DotNetZip.dll. Did you forget to copy it?",
                    "DLL Not Found Exception", MessageBoxButtons.OK);
                Console.WriteLine("DLL Could Not be loaded / found. \n" + ex.Message +
                    "\n" + ex.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error occurred while zipping up files.",
                    "Exception", MessageBoxButtons.OK);
                Console.WriteLine("Error occurred while trying to zip up files to " + destFile + ". \n" + ex.Message +
                    "\n" + ex.ToString());
            }
        }

        private void zippingTimer_Tick(object sender, EventArgs e)
        {
            if (zippingThread != null)
            {
                if (!zippingThread.IsAlive)
                {
                    zippingTimer.Stop();
                    zipBackupsButton.Enabled = true;
                    zipBackupsButton.Text = "Zip Up Logs";
                }
            }
        }

        private void deleteBackupsButton_Click(object sender, EventArgs e)
        {
            backup.deleteBackup();
            deleteBackupsButton.Enabled = false;
        }

        private void chooseLogsFolderTimer2_Tick(object sender, EventArgs e)
        {
            chooseLogsFolderTimer2.Stop();
            chooseFolderTimerRunning = false;
        }

        /* Search Log Files  ----------------------------------------------------------- */

        private void enableSearchFields()
        {
            searchTermTextBox.Enabled = true;
        }

        private void searchTextBox_TextChanged(object sender, EventArgs e)
        {
            if (searchTermTextBox.TextLength > 0)
            {
                searchLogsButton.Enabled = true;
            }
            else if (searchTermTextBox.TextLength == 0)
            {
                searchLogsButton.Enabled = false;
            }
        }

        private void searchTermTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine("searchTermTextBox_KeyUp. Key pressed: " + e.KeyCode.ToString());
            if (e.KeyCode == Keys.Enter && searchTermTextBox.TextLength > 0)
            {
                startSearchButtonClicked();
            }
        }

        private void searchLogsButton_Click(object sender, EventArgs e)
        {
            startSearchButtonClicked();
        }

        private void startSearchButtonClicked()
        {
            searchLogsButton.Enabled = false;
            searchTermTextBox.Enabled = false;
            openFileButton.Enabled = false;
            occurancesTree.Enabled = false;
            occurancesTree.Nodes.Clear();
            occurancesTree.Nodes.Add("Searching");
            logFileUtil.setSearchTerms(searchTermTextBox.Text, folderPathBackups.Text);
            searchFilesThread = new Thread(() => startSearch());
            searchFilesThread.Start();
            searchingTimer.Start();
        }

        private void startSearch()
        {
            logFileUtil.startSearch();
        }

        private void searchingTimer_Tick(object sender, EventArgs e)
        {
            if (searchFilesThread != null)
            {
                if (!searchFilesThread.IsAlive)
                {
                    searchingTimer.Stop();
                    searchTermTextBox.Enabled = true;
                    if (searchTermTextBox.TextLength > 0)
                    {
                        searchLogsButton.Enabled = true;
                    }

                    showOccurances();
                }
            }
        }

        private void occurancesTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            openFileButton.Enabled = true;
        }

        private void showOccurances()
        {
            occurancesTree.Nodes.Clear();
            ICollection<MyFile> results = logFileUtil.getOccurances();
            foreach (MyFile item in results)
            {
                TreeNode nodes = new TreeNode(logFileUtil.getRelName(item.getFileName()));
                IDictionary<int, string> valuePairs = item.getOccurances();
                foreach (int key in valuePairs.Keys)
                {
                    string value = "";
                    valuePairs.TryGetValue(key, out value);
                    TreeNode n = new TreeNode();
                    n.Tag = key;
                    n.Text = "Line " + key.ToString() + ", " + value;
                    nodes.Nodes.Add(n);
                    Console.WriteLine("Added occurance to Nodes: " + value);
                }
                occurancesTree.Nodes.Add(nodes);
            }
            if (occurancesTree.Nodes.Count > 0)
            {
                occurancesTree.Enabled = true;
            }
            else if (occurancesTree.Nodes.Count == 0)
            {
                occurancesTree.Nodes.Add("No Occurances Found");
            }
        }

        private void openFileButton_Click(object sender, EventArgs e)
        {
            openFile();
        }

        private void occurancesTree_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeNode n = occurancesTree.SelectedNode;
            if (n.Level == 1)
            {
                openFile();
            }
        }

        private void openFile()
        {
            string fileName = "";
            string lineNumber = "";
            TreeNode n = occurancesTree.SelectedNode;
            if (n.Level == 0)
            {
                fileName = logFileUtil.getFullName(n.Text);
            }
            else if (n.Level == 1)
            {
                fileName = logFileUtil.getFullName(n.Parent.Text);
                lineNumber = n.Tag.ToString();
            }

            try
            {
                LogFileUtil.openFile(fileName, lineNumber);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error occured while openning file\n" + fileName);
                Console.WriteLine("Error occured while openning file\n" + fileName +
                    "\n" + ex.ToString());
            }
        }

        /* Closing the App  -------------------------------------------------------------------------------- */

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (backup.isNewDirSet())
            {
                DialogResult d = MessageBox.Show("There may be backup files, can we delete them?\n" +
                    "If you select No, please remember to delete manually later",
                    "Delete backed up logs?",
                    MessageBoxButtons.YesNo);

                if (d == DialogResult.Yes) backup.deleteBackup();
            }
            backup.stopBackup();
        }
    }
}