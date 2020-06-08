using System;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using Ionic.Zip;
using System.Collections;
using System.Collections.Generic;

namespace Log_Files_Utility
{
    class BackupUtil
    {
        private string folder;
        private string searchPattern;
        private ICollection<string> newDir;
        private bool runBackup;
        private int timeout; // in seconds

        public BackupUtil()
        {
            folder = "";
            timeout = 30;
            newDir = new HashSet<string>();
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

        public bool isFolderStringEmpty()
        {
            if (folder == "") return true;
            else return false;
        }

        public bool isNewDirSet()
        {
            return newDir.Count > 0;
        }

        public void startBackup(int t)
        {
            timeout = t;
            runBackup = true;
            DateTime dateTime;
            
            do
            {
                dateTime = DateTime.Now;
                string dir = folder + "\\" + dateTime.ToString("yyyyMMdd");
                //string dir2 = folder + "\\" + dateTime.Year + dateTime.Month + dateTime.Day;
                string[] files = System.IO.Directory.GetFiles(folder, "*" + searchPattern + "*");

                //MessageBox.Show("Date: " + dateTime.ToShortDateString(), "Files", MessageBoxButtons.OK);

                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                if (!newDir.Contains(dir))
                {
                    newDir.Add(dir);
                }

                foreach (string file in files)
                {
                    StringBuilder newFile = new StringBuilder(dir + file.Substring(file.LastIndexOf('\\')));
                    string fileExtension = file.Substring(file.LastIndexOf('.'));
                    newFile.Remove(newFile.ToString().LastIndexOf('.'), fileExtension.Length);
                    newFile.Append(dateTime.ToString("HHmmss") + fileExtension);
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

        public static void zipUpFiles(string destination, string dir)
        {
            string[] allFiles = Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    foreach (string item in allFiles)
                    {
                        Console.WriteLine(item.Substring(dir.Length));
                        if (item.EndsWith(".log") || item.EndsWith(".txt"))
                        {
                            Console.WriteLine("Adding file to Zip File: " + item);
                            ZipEntry e = zip.AddFile(item);
                            e.FileName = item.Substring(dir.Length + 1);
                        }
                    }
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

        public void deleteBackup()
        {
            if (newDir.Count > 0)
            {
                Console.WriteLine(newDir.Count);
                foreach (string dir2 in newDir)
                {
                    Console.WriteLine(dir2);
                    if (Directory.Exists(dir2))
                    {
                        try
                        {
                            Directory.Delete(dir2, true);
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Error deleting Backup Log Files from folder: \n" +
                                dir2, "Error", MessageBoxButtons.OK);
                        }
                    }
                }

                newDir.Clear();
                Console.WriteLine("backup directories deleted. Directory Length: " + newDir.Count);
            }
        }
    }
}
