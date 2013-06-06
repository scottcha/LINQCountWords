using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CountWordsInString;

namespace Tests
{
    [TestClass]
    public class CountWordsTests
    {
        private String _longString;
        private String _s1 = "one three three three four four four two two four eight eight six six six six six six seven seven seven seven seven seven five five five five five seven eight eight eight eight eight eight nine nine nine nine nine nine nine nine nine ten ten ten ten ten ten ten ten ten ten eleven eleven eleven eleven eleven eleven eleven eleven eleven eleven eleven";

        [TestInitialize()]
        public void Initialize()
        {
            var fs = new FileStream("..\\..\\prideandprejudice.txt", FileMode.Open);
            var sr = new StreamReader(fs);
            _longString = sr.ReadToEnd();
            sr.Close();
            fs.Close();

        }

        [TestMethod]
        public void BasicTestLinq()
        {
            
            
            var results = CountWordsInString.CountWordsInString.CountWordsLinq(_s1);
            Assert.AreEqual(results.Count, 10, "Expected 4 have: " + results.Count);
        }

        [TestMethod]
        public void LongStringTestLinq()
        {
            var results = CountWordsInString.CountWordsInString.CountWordsLinq(_longString);
            
            
            Assert.AreEqual(results.Count, 10, "Expected 10 have: " + results.Count);
        }

        [TestMethod]
        public void BasicTestDictionary()
        {
            

            var results = CountWordsInString.CountWordsInString.CountWordsDictionary(_s1);
            Assert.AreEqual(results.Count, 10, "Expected 4 have: " + results.Count);
        }

        [TestMethod]
        public void LongStringTestDictionary()
        {
            var results = CountWordsInString.CountWordsInString.CountWordsDictionary(_longString);


            Assert.AreEqual(results.Count, 10, "Expected 10 have: " + results.Count);
        }

        [TestMethod]
        public void BasicTestDictionaryNoLinq()
        {
            var results = CountWordsInString.CountWordsInString.CountWordsDictionaryNoLinq(_s1);
            Assert.AreEqual(results.Count, 10, "Expected 4 have: " + results.Count);
        }

        [TestMethod]
        public void LongStringTestDictionaryNoLinq()
        {
            var results = CountWordsInString.CountWordsInString.CountWordsDictionaryNoLinq(_longString);
            Assert.AreEqual(results.Count, 10, "Expected 10 have: " + results.Count);
        }

        [TestMethod]
        public void CompareResults()
        {
            var resultsDict = CountWordsInString.CountWordsInString.CountWordsDictionary(_s1);
            var resultsLinq = CountWordsInString.CountWordsInString.CountWordsLinq(_s1);
            for (int i = 0; i < resultsDict.Count; i++)
            {
                Assert.AreEqual(resultsDict[i], resultsLinq[i], "Items should be equal but are not for short string");
            }

            resultsDict = CountWordsInString.CountWordsInString.CountWordsDictionary(_longString);
            resultsLinq = CountWordsInString.CountWordsInString.CountWordsLinq(_longString);
            for (int i = 0; i < resultsDict.Count; i++)
            {
                Assert.AreEqual(resultsDict[i], resultsLinq[i], "Items should be equal but are not for long string");
            }

            resultsDict = CountWordsInString.CountWordsInString.CountWordsDictionaryNoLinq(_longString);
            resultsLinq = CountWordsInString.CountWordsInString.CountWordsLinq(_longString);
            for (int i = 0; i < resultsDict.Count; i++)
            {
                Assert.AreEqual(resultsDict[i], resultsLinq[i], "NoLinq: Items should be equal but are not for long string");
            }
        }

    }
}
