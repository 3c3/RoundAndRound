using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundAndRound
{
    public class StringStore
    {
        private List<string> strings = new List<string>();
        private Dictionary<string, int> lookup = new Dictionary<string, int>();
        private int wordIdx = 0;

        public int Count
        {
            get { return strings.Count; }
        }

        public int GetIdx(string lookupString)
        {
            int result;
            if (lookup.TryGetValue(lookupString, out result))
            {
                return result;
            }
            else
            {
                lookup.Add(lookupString, wordIdx);
                strings.Add(lookupString);
                wordIdx++;
                return wordIdx - 1;
            }
        }

        public string GetString(int idx)
        {
            if (idx >= wordIdx) return null;
            return strings[idx];
        }

        /// <summary>
        /// Returns all the elements in the store
        /// </summary>
        /// <returns></returns>
        public string GetAll()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < strings.Count; i++)
                builder.AppendFormat("{0}\t{1}\n", i, strings[i]);
            return builder.ToString();
        }
    }
}
