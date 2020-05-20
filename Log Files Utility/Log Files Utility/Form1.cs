using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

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

        private void chooseFolderBackupButton_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            folderPathBackups.Text = folderBrowserDialog1.SelectedPath;
        }

        private void folderPathBackups_KeyDown(object sender, KeyEventArgs e)
        {
            Console.WriteLine("folderPathBackups_KeyDown. key pressed: " + e.KeyCode.ToString());
            if (e.KeyCode == Keys.Enter && !chooseFolderTimerRunning)
            {
                Console.WriteLine("Enter Key pressed");
                chooseLogsFolderTimer.Stop();
                if (processFolderUpdate(folderPathBackups.Text) && backup.isFolderValid())
                {
                    Console.WriteLine("enabling search fields");
                    enableSearchFields();
                }
            }
        }

        private bool processFolderUpdate(string folderPath)
        {
            DialogResult dialogResult = DialogResult.None;
            bool b = true;

            if (backup.getFolder() != folderPath && !backup.isFolderStringEmpty() && backup.isNewDirSet())
            {
                dialogResult = MessageBox.Show("The path has changed, we will need to delete any current backed up \n" +
                    "log files first.\nCan we continue?", "Delete Current Logs", MessageBoxButtons.YesNo);
            }
            if (dialogResult == DialogResult.No)
            {
                b = false;
            }
            else if (dialogResult == DialogResult.Yes)
            {
                b = true;
                backup.deleteBackup();
            }

            if (b)
            {
                backup.setFolder(folderPath);

                if (backup.isFolderValid())
                {
                    startStopBackup.Enabled = true;
                }
                else
                {
                    startStopBackup.Enabled = false;

                    if (!backup.isFolderStringEmpty())
                    {
                        chooseFolderTimerRunning = true;
                        MessageBox.Show("Chosen Folder Path does not exist.\nPlease choose again.",
                            "Invalid Folder Path",
                            MessageBoxButtons.OK);
                        chooseLogsFolderTimer2.Start();
                    }
                }
            }
            return b;
        }

        private void startStopBackup_Click(object sender, EventArgs e)
        {
            if (processFolderUpdate(folderPathBackups.Text))
            {
                startStopBackupRun();
            }
        }

        private void startStopBackupRun()
        {
            if (startStopBackup.Text == "Start" && backup.isFolderValid())
            {
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

        private void zipBackupsButton_Click(object sender, EventArgs e)
        {
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = ".zip";
            saveFileDialog1.Filter = "Zip Files (*.zip)|*.zip";
            saveFileDialog1.ShowDialog();
            zippingThread = new Thread(() => zipFiles(saveFileDialog1.FileName));
            zipBackupsButton.Enabled = false;
            zipBackupsButton.Text = "Zipping Up";
            zippingThread.Start();
            zippingTimer.Start();
        }

        private void zipFiles(string destFile)
        {
            try
            {
                backup.zipUpFiles(destFile);
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
                    zipBackupsButton.Text = "Zip Backups";
                }
            }
        }

        private void deleteBackupsButton_Click(object sender, EventArgs e)
        {
            if (backup.deleteBackup())
            {
                zipBackupsButton.Enabled = false;
                deleteBackupsButton.Enabled = false;
            }
        }

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

        private void chooseLogsFolderTimer_Tick(object sender, EventArgs e)
        {
            chooseLogsFolderTimer.Stop();
            if (processFolderUpdate(folderPathBackups.Text) && backup.isFolderValid())
            {
                enableSearchFields();
            }
        }

        private void chooseLogsFolderTimer2_Tick(object sender, EventArgs e)
        {
            chooseLogsFolderTimer2.Stop();
            chooseFolderTimerRunning = false;
        }

        private void folderPathBackups_TextChanged(object sender, EventArgs e)
        {
            chooseLogsFolderTimer.Stop();
            chooseLogsFolderTimer.Start();
        }

        /*--------------------------------------------------------------------------*/

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
            searchLogsButton.Enabled = false;
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
    }
}