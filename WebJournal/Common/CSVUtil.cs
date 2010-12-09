using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace WebJournal.Common
{
    /// <summary>
    /// CSVUtil is a helper class handling csv files.
    /// </summary>
    public class CSVUtil
    {
        private CSVUtil()
        {
        }
        //write a new file, existed file will be overwritten
        public static void WriteCSV(string filePathName, List<String[]> ls)
        {
            WriteCSV(filePathName, false, ls);
        }
        //write a file, existed file will be overwritten if append = false
        public static void WriteCSV(string filePathName, bool append, List<String[]> ls)
        {
            StreamWriter fileWriter = new StreamWriter(filePathName, append, Encoding.Default);
            foreach (String[] strArr in ls)
            {
                fileWriter.WriteLine(String.Join(",", strArr));
            }
            fileWriter.Flush();
            fileWriter.Close();

        }
        public static List<String[]> ReadCSV(string filePathName)
        {
            List<String[]> ls = new List<String[]>();
            StreamReader fileReader = new StreamReader(filePathName);
            string strLine = "";
            while (strLine != null)
            {
                strLine = fileReader.ReadLine();
                if (strLine != null && strLine.Length > 0)
                {
                    ls.Add(strLine.Split(','));
                }
            }
            fileReader.Close();
            return ls;
        }

    }
}
