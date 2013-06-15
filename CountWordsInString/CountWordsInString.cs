using System;
using System.Collections.Generic;
using System.Linq;

namespace CountWordsInString
{
    public class CountWordsInString
    {
        /// <summary>
        /// Returns top 10 most frequently occuring words (naive definition of word) which occur in a string using LINQ
        /// </summary>
        /// <param name="s">string to parse</param>
        /// <returns>Top ten most frequently occuring words</returns>
        public static List<String> CountWordsLinq(String s)
        {
            var words = s.Split(' ');
            var wordCounts = words.GroupBy(x => x).Select(x => new { Name = x.Key, Count = x.Count() }).OrderByDescending(x => x.Count);  
            var countedWords = wordCounts.Select(x => x.Name).ToList();
            return ExtractTopTen(countedWords);
        }

        /// <summary>
        /// Returns top 10 most frequently occuring words (naive definition of word) which occur in a string using dictionary to count the words and LINQ to sort them
        /// </summary>
        /// <param name="s1">string to parse</param>
        /// <returns>Top ten most frequently occuring words</returns>
        public static List<String> CountWordsDictionary(String s1)
        {
            var wordDictionary = StringToWordDictionary(s1);

            var countedWords = wordDictionary.OrderByDescending(x => x.Value).Select(x => x.Key).ToList();
            return ExtractTopTen(countedWords);
        }

        /// <summary>
        /// Delegate method to compare KeyValuePairs for sorting
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Returns top 10 most frequently occuring words (naive definition of word) which occur in a string using dictionary and sorting using collections.sort
        /// </summary>
        /// <param name="s1">string to parse</param>
        /// <returns>Top ten most frequently occuring words</returns>
        public static List<String> CountWordsDictionaryNoLinq(String s1)
        {
            var wordDictionary = StringToWordDictionary(s1);

            var kvpList = wordDictionary.ToList();
            kvpList.Sort(CompareKVPByCount);

            
            return ExtractTopTen(kvpList);
        }

        /// <summary>
        /// Creates a dictionary of words for a string with key being the word and value the # of times the word occurs in the string
        /// </summary>
        /// <param name="s1">String to parse</param>
        /// <returns>Dictionary containing the word as key and wordcount as value</returns>
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

        /// <summary>
        /// Returns the top 10 values from a list or a subset if there are fewer than 10 values
        /// </summary>
        /// <param name="countedWords">Sorted list of words</param>
        /// <returns>List containing up to 10 values from beginning of list</returns>
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

        /// <summary>
        /// Returns the top 10 values from a list of KeyValuePairs or subset if there are fewer than 10 values
        /// </summary>
        /// <param name="countedWords">Sorted List of KeyValuePairs</param>
        /// <returns>List containing only the keys from the first 10 key value pairs</returns>
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
