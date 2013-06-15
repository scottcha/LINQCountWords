using System;
using System.Collections.Generic;
using System.Linq;

namespace CountWordsInString
{
    public class CountWordsInString
    {
        public static List<String> CountWordsLinq(String s)
        {
            var words = s.Split(' ');

            var wordCounts = words.GroupBy(x => x).Select(x => new { Name = x.Key, Count = x.Count() }).OrderByDescending(x => x.Count);
            
            var countedWords = wordCounts.Select(x => x.Name).ToList();

            return ExtractTopTen(countedWords);
        }

        

        public static List<String> CountWordsDictionary(String s1)
        {
            var wordDictionary = StringToWordDictionary(s1);

            var countedWords = wordDictionary.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
            return ExtractTopTen(countedWords);
        }

        private static int CompareKVPByCount(KeyValuePair<string, int> a, KeyValuePair<string, int> b)
        {
            if (a.Value == b.Value)
            {
                return 0;
            }
            else if (a.Value < b.Value)
            {
                return 1;
            }
            else
            {
                return -1;
            }

        }

        public static List<String> CountWordsDictionaryNoLinq(String s1)
        {
            var wordDictionary = StringToWordDictionary(s1);

            var kvpList = wordDictionary.ToList();
            kvpList.Sort(CompareKVPByCount);

            
            return ExtractTopTen(kvpList);
        }

        private static Dictionary<string, int> StringToWordDictionary(string s1)
        {
            var words = s1.Split(' ');

            var wordDictionary = new Dictionary<string, int>();

            foreach (string s in words)
            {
                if (wordDictionary.ContainsKey(s))
                {
                    wordDictionary[s]++;
                }
                else
                {
                    wordDictionary.Add(s, 1);
                }
            }
            return wordDictionary;
        }

        private static List<string> ExtractTopTen(List<string> countedWords)
        {
            if (countedWords.Count <= 10)
            {
                return countedWords;
            }
            else
            {
                return countedWords.GetRange(0, 10);
            }
        }

        private static List<string> ExtractTopTen(List<KeyValuePair<string,int>> countedWords)
        {
            var topTen = new List<string>();
            for (int i = 0; i < countedWords.Count && i < 10; i++)
            {
                topTen.Add(countedWords[i].Key);
            }
            return topTen;

        }
    }
}
