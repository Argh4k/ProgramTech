using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramTech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgramTech.Tests
{
    [TestClass()]
    public class WordServiceTests
    {
        static List<Word> testWords;
        static readonly Language tableName = ProgramTech.Language.EN;
        [ClassInitialize()]
        public static void intialize(TestContext tc)
        {
            DatabaseController.getInstance().removeTable(tableName.ToString());
            DatabaseController.getInstance().addTable(tableName.ToString());
            Word.setScoringHandler(new ScoringHandler("../../ScoringHandlerDaoTests.xml"));
            testWords = new List<Word>(new Word[] { new Word("Daddy"), new Word("Mom"), new Word("Dnow"), new Word("Grandpa") });
            using (var testDao = new WordDAO())
            {
                foreach (Word word in testWords)
                {
                    testDao.save(word, tableName.ToString());
                }
            }
        }
        

        [TestMethod()]
        public void getAllTest()
        { 
            List<Word> result = WordService.getInstance().getAll(tableName, 1000);
            CollectionAssert.AreEquivalent(testWords, result);
        }

        [TestMethod()]
        public void getAllAsyncTest()
        {
            List<Word> result = WordService.getInstance().getAll(tableName, 1000, true);
            CollectionAssert.AreEquivalent(testWords, result);
        }

        [TestMethod()]
        public void getByFirstCharacterTest()
        {
            List<Word> expected = testWords.Where(x => {return x.Content.First() == 'd'; }).ToList();
            List<Word> result = WordService.getInstance().getByFirstCharacter(tableName, 'd', 1000);
            CollectionAssert.AreEquivalent(expected, result);

        }

        [TestMethod()]
        public void getByFirstCharacterTestAsync()
        {
            List<Word> expected = testWords.Where(x => { return x.Content.First() == 'd'; }).ToList();
            List<Word> result = WordService.getInstance().getByFirstCharacter(tableName, 'd', 1000, true);
            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod()]
        public void getBtFistCharacterAndLengthTest()
        {
            List<Word> expected = testWords.Where(x => { return x.Content.First() == 'd' && x.Length == 5; }).ToList();
            List<Word> result = WordService.getInstance().getByFirstCharacterAndLength(tableName, 'd', 5);
            CollectionAssert.AreEquivalent(expected, result);
        }

        [TestMethod()]
        public void getBtFistCharacterAndLengthTestAsync()
        {
            List<Word> expected = testWords.Where(x => { return x.Content.First() == 'd' && x.Length == 5; }).ToList();
            List<Word> result = WordService.getInstance().getByFirstCharacterAndLength(tableName, 'd', 5, true);
            CollectionAssert.AreEquivalent(expected, result);
        }


        [TestMethod()]
        public void saveListTest()
        {
            if (DatabaseController.getInstance().checkTableExists(tableName.ToString()))
            {
                DatabaseController.getInstance().removeTable(tableName.ToString());       
            }
            DatabaseController.getInstance().addTable(tableName.ToString());
            WordService.getInstance().saveList(testWords, tableName);
            List<Word> result = WordService.getInstance().getAll(tableName, 1000);
            CollectionAssert.AreEquivalent(testWords, result);
        }

        [TestMethod()]
        public void saveListTestAsync()
        {
            if (DatabaseController.getInstance().checkTableExists(tableName.ToString()))
            {
                DatabaseController.getInstance().removeTable(tableName.ToString());
            }
            DatabaseController.getInstance().addTable(tableName.ToString());
            WordService.getInstance().saveList(testWords, tableName, true);
            List<Word> result = WordService.getInstance().getAll(tableName, 1000);
            CollectionAssert.AreEquivalent(testWords, result);
        }

        [TestMethod()]
        public void saveTest()
        {
            if (DatabaseController.getInstance().checkTableExists(tableName.ToString()))
            {
                DatabaseController.getInstance().removeTable(tableName.ToString());
            }
            DatabaseController.getInstance().addTable(tableName.ToString());
            foreach (Word word in testWords)
            {
                WordService.getInstance().save(word, tableName);
            }
            List<Word> result = WordService.getInstance().getAll(tableName, 1000);
            CollectionAssert.AreEquivalent(testWords, result);
        }

        [TestMethod()]
        public void saveTestAsync()
        {
            if (DatabaseController.getInstance().checkTableExists(tableName.ToString()))
            {
                DatabaseController.getInstance().removeTable(tableName.ToString());
            }
            DatabaseController.getInstance().addTable(tableName.ToString());
            foreach (Word word in testWords)
            {
                WordService.getInstance().save(word, tableName, true);
            }
            List<Word> result = WordService.getInstance().getAll(tableName, 1000);
            CollectionAssert.AreEquivalent(testWords, result);
        }

        [TestMethod()]
        public void savePolishLetter()
        {
            if (DatabaseController.getInstance().checkTableExists(tableName.ToString()))
            {
                DatabaseController.getInstance().removeTable(tableName.ToString());
            }
            DatabaseController.getInstance().addTable(tableName.ToString());
            Assert.IsTrue(WordService.getInstance().save(new Word("ćma"), tableName));
            List<Word> result = WordService.getInstance().getAll(tableName, 1000);
            List<Word> expected = new List<Word>(new Word[] { new Word("ćma") });
            CollectionAssert.AreEquivalent(expected, result);
        }

    }
}