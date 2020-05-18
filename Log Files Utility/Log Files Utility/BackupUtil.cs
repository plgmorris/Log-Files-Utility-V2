using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using Ionic.Zip;

namespace Log_Files_Utility
{
    class BackupUtil
    {
        private string folder;
        private string searchPattern;
        private string newDir;
        private bool runBackup;
        private int timeout; // in seconds

        public BackupUtil()
        {
            folder = "";
            timeout = 30;

        }

        public void setFolder(string s)
        {
            folder = s;
        }

        public string getFolder()
        {
            return folder;
        }

        public void setSearchPattern(string s)
        {
            searchPattern = s;
        }

        public void stopBackup()
        {
            runBackup = false;
        }

        public bool isFolderValid()
        {
            if (Directory.Exists(folder))
            {
                return true;
            }
            else return false;
        }

        public bool isFolderStringEmpty()
        {
            if (folder == "") return true;
            else return false;
        }

        public bool isNewDirSet()
        {
            if (newDir != "" && newDir != null) return true;
            else return false;
        }

        public void startBackup(int t)
        {
            timeout = t;
            runBackup = true;
            DateTime dateTime;
            dateTime = DateTime.Now;
            newDir = folder + "\\" + dateTime.Year + dateTime.Month + dateTime.Day;

            do
            {
                dateTime = DateTime.Now;
                string[] files = System.IO.Directory.GetFiles(folder, "*" + searchPattern + "*");

                //MessageBox.Show("Date: " + dateTime.ToShortDateString(), "Files", MessageBoxButtons.OK);

                if (!Directory.Exists(newDir))
                {
                    Directory.CreateDirectory(newDir);
                }

                foreach (string file in files)
                {
                    string hour = dateTime.Hour.ToString();
                    if (hour.Length == 1) hour = "0" + dateTime.Hour.ToString();
                    string minute = dateTime.Minute.ToString();
                    if (minute.Length == 1) minute = "0" + dateTime.Minute.ToString();
                    string second = dateTime.Second.ToString();
                    if (second.Length == 1) second = "0" + dateTime.Second.ToString();

                    StringBuilder newFile = new StringBuilder(newDir + file.Substring(file.LastIndexOf('\\')));
                    string fileExtension = file.Substring(file.LastIndexOf('.'));
                    newFile.Remove(newFile.ToString().LastIndexOf('.'), fileExtension.Length);
                    newFile.Append(" " + hour + minute + second + fileExtension);
                    //Console.WriteLine("New File: " + newFile.ToString());

                    File.Move(file, newFile.ToString());
                }
                for (int i = 0; i < timeout; i++)
                {
                    Thread.Sleep(1000);
                    if (!runBackup) i = timeout;
                }
            }
            while (runBackup);
        }

        public void zipUpFiles(string destination)
        {
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    zip.AddDirectory(newDir);
                    zip.Save(destination);
                }
            }
            catch (DllNotFoundException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool deleteBackup()
        {
            bool b = false;
            if (Directory.Exists(newDir))
            {
                try
                {
                    Directory.Delete(newDir, true);
                    newDir = "";
                    b = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error deleting Backup Log Files from folder: \n" +
                        newDir, "Error", MessageBoxButtons.OK);
                }
            }
            else MessageBox.Show("Apparently the backed up logs folder doesn't exist: \n" +
                        newDir, "Error", MessageBoxButtons.OK);

            return b;
        }
    }
}
