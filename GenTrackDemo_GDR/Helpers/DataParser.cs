using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GenTrackDemo.Helpers
{
    public class DataParser
    {
        private string stringToParse;
        private List<string> lines;
        private List<List<string>> outputResults = new List<List<string>>();
        private List<string> outPutErrors = new List<string>();
        private string header = "";
        int i900 = 0;
        int i300 = 0;
        int i200 = 0;
        int i100 = 0;

        public DataParser()
        {
        }


        public DataParser(string inputCsv)
        {
            EvaluateDataSet(inputCsv);
        }

        public void EvaluateDataSet(string inputCsv)
        {
            CleanUp(inputCsv);

            string[] arylines = inputCsv.Split(Convert.ToChar("\n"));
            foreach(string line in arylines)
            {
                string newLine = ValidateLine(line).ToString().Trim();
                if(newLine.Length > 0)
                {
                    lines.Add(line);
                }
            }

            if ((i100 > 0) && (i200 > 0) && (i300 > 0) && (i900 > 0))
            {
                if ((i100 == 1) && (i900 == 1))
                {
                    ProcessValidLines();
                    ValidateOutputResults();
                    CreateOutputFiles();
                }
                else
                {
                    Console.WriteLine("Too many Header or Footer elements");
                    throw (new Exception("Too many Header or Footer elements"));
                }
            }
            else
            {
                if (i100 == 0)
                {
                    Console.WriteLine("No Header");
                    throw (new Exception("No Header"));
                }

                if (i200 == 0)
                {
                    Console.WriteLine("No 200");
                    throw (new Exception("No 200"));
                }

                if (i300 == 0)
                {
                    Console.WriteLine("No 300");
                    throw (new Exception("No 300"));
                }


                if (i900 == 0)
                {
                    Console.WriteLine("No Footer");
                    throw (new Exception("No Footer"));
                }

                Console.WriteLine("Missing data element");
                throw (new Exception("Missing data element"));
            }
           

        }

        private void ValidateOutputResults()
        {
            List<int> indexToDelete = new List<int>();
            foreach (List<string> s in outputResults)
            {
                for(int i = 0; i < s.Count; i++)
                {
                    if (s[i].StartsWith("200")) //We have a valid start line
                    {
                        if (s.Count == 1) // We only have 1 row in data bundle, we're expecting 300's after this 200
                            throw (new Exception("No 300"));
                        try 
                        {
                            for (int n = 1; n < s.Count; n++)
                                if (!s[i + 1].StartsWith("300")) //Its an invalid line sequence as all subsequent lines must start with "300"
                                {
                                    indexToDelete.Add(n);
                                }
                        }
                        catch { }
                    }
                }
            }

            //remove error documents
            indexToDelete.Sort((a, b) => b.CompareTo(a));
            foreach(int item in indexToDelete)
            {
                indexToDelete.Remove(item);
            }
        }

        private void CreateOutputFiles()
        {
            char sep = Convert.ToChar(",");
            string fileName = "";
            foreach(List<string> s in outputResults)
            {
                fileName = s[0].Split(sep)[1] + ".csv";
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(header);
                foreach(string line in s)
                {
                    sb.AppendLine(line);
                }
                sb.AppendLine("900");

                using (StreamWriter sw = new StreamWriter(fileName))
                {
                    sw.WriteLine(sb.ToString());
                    Console.WriteLine("File Created Ok");
                }
            }

        }

        private void ProcessValidLines()
        {
            List<string> newDoc = new List<string>();

            for (int i = 0; i < lines.Count; i++)
            {
                if (lines[i].StartsWith("200"))
                {
                    if (newDoc.Count > 0)
                    {
                        outputResults.Add(newDoc);
                        newDoc = new List<string>();
                    }
                    newDoc.Add(lines[i]);
                }

                if (lines[i].StartsWith("300"))
                {
                    newDoc.Add(lines[i]);
                }

                if (i == lines.Count-1)
                {
                    outputResults.Add(newDoc);
                }
            }

        }

        private void CleanUp(string inputCsv)
        {
            //cleanup incase we're reusing
            stringToParse = inputCsv;
            lines = new List<string>();
            outputResults = new List<List<string>>();

            i900 = 0;
            i300 = 0;
            i200 = 0;
            i100 = 0;
        }

        private string ValidateLine(string line)
        {
            string result = "";
            char sep = Convert.ToChar(",");

            string[] row = line.Split(sep);
            switch (row[0])
            {
                case "100":
                    i100 += 1;
                    header = line;
                    result = "";
                    break;
                case "200":
                    i200 += 1;
                    result = line;
                    break;
                case "300":
                    i300 += 1;
                    result = line;
                    break;

                case "900":
                    i900 += 1;
                    result = "";
                    break;

                default:
                    result = "";
                    break;
            }

            result = result.Replace("\t", "").Replace("\r", "").Replace("\n", "").Replace(" ", "").Trim();

            return result;
        }
    }
}
