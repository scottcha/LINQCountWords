using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
            var countedWords = wordCounts.Select(x => x.Name).Take(10).ToList();
            return ExtractTopTen(countedWords);
        }

        /// <summary>
        /// Returns top 10 most frequently occuring words (naive definition of word) which occur in a string using LINQ as lookup instead of list
        /// </summary>
        /// <param name="s">string to parse</param>
        /// <returns>Top ten most frequently occuring words</returns>
        public static List<String> CountWordsLinqLookup(String s)
        {
            var words = s.Split(' ');
            var wordCounts = words.ToLookup(x => x).Select(x => new { Name = x.Key, Count = x.Count() }).OrderByDescending(x => x.Count);
            var countedWords = wordCounts.Select(x => x.Name).Take(10);
            return ExtractTopTen(countedWords);
        }

        /// <summary>
        /// Returns top 10 most frequently occuring words (naive definition of word) which occur in a string using LINQ
        /// Force evaluation of every LINQ command by using ToList; this is only for performance analysis and should not be used
        /// in production code
        /// </summary>
        /// <param name="s">string to parse</param>
        /// <returns>Top ten most frequently occuring words</returns>
        public static List<String> CountWordsLinqForceEvaluate(String s)
        {
            FileStream fs;
            const string fileName = "forceEvaluateValues.txt";
            try
            {
                fs = new FileStream(fileName, FileMode.Truncate);
            }
            catch (FileNotFoundException)
            {
                fs = new FileStream(fileName, FileMode.Create);
            }
            var streamWriter = new StreamWriter(fs);
            
            var words = s.Split(' ');
            var sw = Stopwatch.StartNew();
            var op1 = words.GroupBy(x => x).ToList();
            sw.Stop();
            streamWriter.WriteLine("GroupBy: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();
            sw = Stopwatch.StartNew();
            var op2 = op1.Select(x => new { Name = x.Key, Count = x.Count() }).ToList();
            sw.Stop();
            streamWriter.WriteLine("Select Key & value: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();
            sw = Stopwatch.StartNew();
            var op3 = op2.OrderByDescending(x => x.Count).ToList();
            sw.Stop();
            streamWriter.WriteLine("OrderByDescending: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();
            sw = Stopwatch.StartNew();
            var op4 = op3.Select(x => x.Name).ToList();
            sw.Stop();
            streamWriter.WriteLine("Select Name: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();
            sw = Stopwatch.StartNew();
            var countedWords = op4.Take(10).ToList();
            sw.Stop();
            streamWriter.WriteLine("Take 10: " + sw.Elapsed.TotalMilliseconds);
            streamWriter.Close();
            fs.Close();
            return ExtractTopTen(countedWords);
        }

        /// <summary>
        /// Returns top 10 most frequently occuring words (naive definition of word) which occur in a string using LINQ
        /// Force evaluation of every LINQ command by using ToList; this is only for performance analysis and should not be used
        /// in production code
        /// </summary>
        /// <param name="s">string to parse</param>
        /// <returns>Top ten most frequently occuring words</returns>
        public static List<String> CountWordsLinqLookupForceEvaluate(String s)
        {
            FileStream fs;
            const string fileName = "forceEvaluateLinqLookupValues.txt";
            try
            {
                fs = new FileStream(fileName, FileMode.Truncate);
            }
            catch (FileNotFoundException)
            {
                fs = new FileStream(fileName, FileMode.Create);
            }
            var streamWriter = new StreamWriter(fs);

            var words = s.Split(' ');
            var sw = Stopwatch.StartNew();
            var op1 = words.ToLookup(x => x);
            sw.Stop();
            streamWriter.WriteLine("GroupBy: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();
            sw = Stopwatch.StartNew();
            var op2 = op1.Select(x => new { Name = x.Key, Count = x.Count() }).ToList();
            sw.Stop();
            streamWriter.WriteLine("Select Key & value: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();
            sw = Stopwatch.StartNew();
            var op3 = op2.OrderByDescending(x => x.Count).ToList();
            sw.Stop();
            streamWriter.WriteLine("OrderByDescending: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();
            sw = Stopwatch.StartNew();
            var op4 = op3.Select(x => x.Name).ToList();
            sw.Stop();
            streamWriter.WriteLine("Select Name: " + sw.Elapsed.TotalMilliseconds);
            sw.Reset();
            sw = Stopwatch.StartNew();
            var countedWords = op4.Take(10).ToList();
            sw.Stop();
            streamWriter.WriteLine("Take 10: " + sw.Elapsed.TotalMilliseconds);
            streamWriter.Close();
            fs.Close();
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

            var countedWords = wordDictionary.OrderByDescending(x => x.Value).Select(x => x.Key).Take(10).ToList();
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
        /// Returns the top 10 values from an IEnumerable or a subset if there are fewer than 10 values
        /// </summary>
        /// <param name="countedWordsEnum">Sorted IEnumerable of words</param>
        /// <returns>List containing up to 10 values from beginning of list</returns>
        private static List<string> ExtractTopTen(IEnumerable<string> countedWordsEnum)
        {
            return ExtractTopTen(countedWordsEnum.ToList());
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
