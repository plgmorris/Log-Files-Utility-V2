using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Log_Files_Utility
{
    class LogFileUtil
    {
        private List<MyFile> matchingFiles;
        private string searchPattern;
        private string directory;

        public LogFileUtil()
        {
            matchingFiles = new List<MyFile>();
        }

        public void setSearchTerms(string s, string d)
        {
            searchPattern = s;
            directory = d;
        }

        public string getDirectory()
        {
            return directory;
        }

        public void startSearch()
        {
            matchingFiles.Clear();
            IEnumerable<string> files = Directory.EnumerateFiles(directory);
            string[] directories = Directory.GetDirectories(directory);

            foreach (string dir in directories)
            {
                files = files.Concat(Directory.EnumerateFiles(dir));
            }
            foreach (string file in files)
            {
                string ext = file.Substring(file.LastIndexOf('.'));
                if (ext == ".log" || ext == ".txt")
                {
                    Console.WriteLine("Adding file: " + file);
                    matchingFiles.Add(new MyFile(file, searchPattern));
                }
            }
        }

        public ICollection<MyFile> getOccurances()
        {
            ICollection<MyFile> result = new List<MyFile>();
            foreach (MyFile file in matchingFiles)
            {
                if (file.isMatch())
                {
                    result.Add(file);
                }
            }

            return result;
        }

        public string getRelName(string name)
        {
            return name.Replace(directory, "");
        }

        public string getFullName(string name)
        {
            return directory + name;
        }

        public static void openFile(string fileName, string lineNumber)
        {
            if (lineNumber == null)
            {
                lineNumber = "";
            }
            Process p = new Process();
            string fileExt = fileName.Substring(fileName.LastIndexOf('.'));

            try
            {
                if ((fileExt == ".log" || fileExt == ".txt") && isNotepadPPInstalled() && lineNumber != "")
                {
                    p.StartInfo.FileName = "notepad++.exe";
                    p.StartInfo.Arguments = "\"" + fileName + "\" -n" + lineNumber;
                }
                else if ((fileExt == ".log" || fileExt == ".txt") && isNotepadPPInstalled())
                {
                    p.StartInfo.FileName = "notepad++.exe";
                    p.StartInfo.Arguments = "\"" + fileName + "\"";
                }
                else
                {
                    p.StartInfo.FileName = fileName;
                }
                Console.WriteLine("Opening File {0} on line {1}", fileName, lineNumber);
                p.Start();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static bool isNotepadPPInstalled()
        {
            string registry_key = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    if (subkey_name == "Notepad++")
                    {
                        Console.WriteLine("Notepad++ is installed");
                        return true;
                    }
                }
            }
            registry_key = @"SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey(registry_key))
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    if (subkey_name == "Notepad++")
                    {
                        Console.WriteLine("Notepad++ is installed");
                        return true;
                    }
                }
            }
            Console.WriteLine("Notepad++ is NOT installed");
            return false;
        }
    }
}
