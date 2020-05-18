using System;
using System.Collections.Generic;
using System.IO;

namespace Log_Files_Utility
{
    class MyFile
    {
        private string fileName;
        private IDictionary<int, string> occurances;
        private string searchTerm;

        public MyFile(string f, string s)
        {
            fileName = f;
            searchTerm = s;
            occurances = new Dictionary<int, string>();
            searchFile();
        }

        public string getFileName()
        {
            return fileName;
        }

        public IDictionary<int, string> getOccurances()
        {
            return new Dictionary<int, string>(occurances);
        }

        private void searchFile()
        {
            StreamReader reader = null;
            int lineCounter = 1;
            try
            {
                reader = new StreamReader(fileName);
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line.Contains(searchTerm))
                    {
                        occurances.Add(lineCounter, line);
                        Console.WriteLine("Added Occurance. Line {0}. {1}", lineCounter, line);
                    }
                    lineCounter++;
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File Not Found Error: \n" + e.Message + "\n" + e.ToString());
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Directory Not Found Error: \n" + e.Message + "\n" + e.ToString());
            }
            catch (IOException e)
            {
                Console.WriteLine("IO Error: \n" + e.Message + "\n" + e.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: \n" + e.Message + "\n" + e.ToString());
            }
            finally
            {
                try
                {
                    reader.Close();
                }
                catch { }
            }
        }

        public bool isMatch()
        {
            return (occurances.Count > 0);
        }
    }
}
