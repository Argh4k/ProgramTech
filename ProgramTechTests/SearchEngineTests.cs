using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace ProgramTech.Tests
{
    [TestClass()]
    public class SearchEngineTests
    {
        static ProgramTech.Language language = ProgramTech.Language.EN;

        [TestInitialize()]
        public void intialize()
        {
            DatabaseController.getInstance().removeTable(language.ToString());

        }

        [ClassCleanup()]
        public static void cleanup()
        {
            DatabaseController.getInstance().removeTable(language.ToString());
        }

        [TestMethod()]
        public void searchTest()
        {
            Word.setScoringHandler(new ScoringHandler("../../ScoringHandlerDaoTests.xml"));
            int resLength = 2;
            string filePath = "../../SearchEngineTest.txt";
            WordController wc = new WordController();
            wc.addDictionaryFromFile(language, filePath);
            List<Char> characters = new List<char>(new char[] { 'a', 'b', 'c', 'd', 'e' });
            SearchEngine sc = new SearchEngine(resLength);
            List<Word> expected = new List<Word>(new Word[] { new Word("abcde"), new Word("bcde") });
            List<Word> result = sc.search(characters, language);
            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod()] 
        public void searchTestMaxLength()
        {
            Word.setScoringHandler(new ScoringHandler("../../ScoringHandlerDaoTests.xml"));
            int resLength = 2;
            string filePath = "../../SearchEngineTest.txt";
            WordController wc = new WordController();
            wc.addDictionaryFromFile(language, filePath);
            List<Char> characters = new List<char>(new char[] { 'a', 'b', 'c', 'd', 'e' });
            SearchEngine sc = new SearchEngine(resLength);
            List<Word> expected = new List<Word>(new Word[] { new Word("cde"), new Word("ced") });
            List<Word> result = sc.search(characters, language, 3);
            CollectionAssert.AreEquivalent(expected, result);
        }
    }
}