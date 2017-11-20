using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RoundAndRound
{
    public class FileParser
    {
        private static char[] emptyChars = { ' ', '\t', '\r', '\n' };

        public StringStore lineStore = new StringStore();
        public StringStore stopStore = new StringStore();

        public List<List<int>> lineStops = new List<List<int>>();

        public void Load(string fileName)
        {
            string[] lines = File.ReadAllLines(fileName);

            List<int> currentStops = new List<int>();
            foreach (string line in lines)
            {
                if (IsEmpty(line)) continue;

                if (line[0] == '#') // начало на линия
                {
                    // запазваме името
                    // би трябвало да се проверява дали вече я има...
                    lineStore.GetIdx(line.Substring(1));

                    currentStops = new List<int>();
                    lineStops.Add(currentStops);
                }
                else
                {
                    string stop = line.Substring(5).TrimEnd(emptyChars);
                    int stopIdx = stopStore.GetIdx(stop);

                    currentStops.Add(stopIdx);
                }
            }
        }

        private bool IsEmpty(string str)
        {
            bool empty = true;
            
            for (int i = 0; i < str.Length; i++)
            {
                if (!emptyChars.Contains(str[i]))
                {
                    empty = false;
                    break;
                }
            }

            return empty;
        }
    }
}
