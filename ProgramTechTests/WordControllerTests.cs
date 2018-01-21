using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
namespace ProgramTech.Tests
{
    [TestClass()]
    public class WordControllerTests
    {
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

        WordController wc = new WordController();
        static ProgramTech.Language language = ProgramTech.Language.EN;
        [TestMethod()]
        public void addDictionaryFromFileTest()
        {
            Word.setScoringHandler(new ScoringHandler("../../ScoringHandlerDaoTests.xml"));
            string filePath = "../../addDictionaryFromFileTest.txt";
            List<Word> expected = new List<Word>(new Word[] { new Word("abandon"), new Word("adult"), new Word("belief"), new Word("frustration"), new Word("garage") });
            wc.addDictionaryFromFile(language, filePath);
            List<Word> result;
            result = WordService.getInstance().getAll(language, 100);
            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod()]
        public void downloadDictionaryTest()
        {
            Word.setScoringHandler(new ScoringHandler("../../ScoringHandlerDaoTests.xml"));
            string file = "../../addDictionaryFromWebTest.txt";
            List<Word> expeected;
            List<Word> result;
            wc.addDictionaryFromFile(language, file);           
            expeected = WordService.getInstance().getAll(language, 100);
            foreach(Word w in expeected)
            {
                Console.WriteLine(w.Content);
            }
            DatabaseController.getInstance().removeTable(language.ToString());
            wc.downloadDictionary(language, "http://www.mieliestronk.com/corncob_lowercase.txt");
            result = WordService.getInstance().getAll(language, 100);
            CollectionAssert.AreEquivalent(expeected, result);
        }


        [TestMethod()]
        [ExpectedException(typeof(Exceptions.WordControllerFileNotFoundException))]
        public void FileExceptionTest()
        {
            int i = 0;
            string fileName = "abcdfgh";
            while (File.Exists(fileName))
            {
                fileName += i;
                i++;
            }
            wc.addDictionaryFromFile(language, fileName);
        }
        

        [TestMethod()]
        [ExpectedException(typeof(Exceptions.WordControllerWebException))]
        public void WebExceptionTest()
        {
            string addres = "http://ww.fgjagadj.pl";
            wc.downloadDictionary(language, addres);
        }
    }
}