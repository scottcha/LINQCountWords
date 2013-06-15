using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CountWordsInString;

namespace Tests
{
    [TestClass]
    public class WikipediaTests
    {
        private const int NumValues = 10;
       

        [TestMethod]
        public void BasicTest()
        {
            String testString = WikipediaTestHelper.GetRandomTestString();
            var sw = Stopwatch.StartNew();
            Assert.IsTrue(testString.Length > 0);
            sw.Stop();
            Debug.WriteLine("Time: " + sw.Elapsed.TotalMilliseconds);
        }

        [TestMethod]
        public void TestAllToCsv()
        {
            FileStream fs;
            const string fileName = "wikipediaTestValues.csv";
            try
            {
                fs = new FileStream(fileName, FileMode.Truncate);
            }
            catch (FileNotFoundException e)
            {
                fs = new FileStream(fileName, FileMode.Create);
            }
            var streamWriter = new StreamWriter(fs);
            streamWriter.WriteLine("length, linq, dictionary, dictionaryNoLinq");
            //generate values for each
            for (int i = 0; i < NumValues; i++)
            {
                var valueToTest = WikipediaTestHelper.GetRandomTestString();
                var length = valueToTest.Split(' ').Length;
                var sw = Stopwatch.StartNew();
                var result = CountWordsInString.CountWordsInString.CountWordsLinq(valueToTest);
                sw.Stop();
                var linqValue = sw.Elapsed.TotalMilliseconds;
                sw.Reset();

                sw = Stopwatch.StartNew();
                result = CountWordsInString.CountWordsInString.CountWordsDictionary(valueToTest);
                sw.Stop();
                var dictValue = sw.Elapsed.TotalMilliseconds;
                sw.Reset();

                sw = Stopwatch.StartNew();
                result = CountWordsInString.CountWordsInString.CountWordsDictionaryNoLinq(valueToTest);
                sw.Stop();
                var dictNoLinqValue = sw.Elapsed.TotalMilliseconds;
                sw.Reset();

            
                streamWriter.WriteLine(length + ", " + linqValue + ", " + dictValue + ", " + dictNoLinqValue);
            }
            streamWriter.Close();
            fs.Close();

        }
    }
}
