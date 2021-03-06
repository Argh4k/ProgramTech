﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace ProgramTech.Tests
{
    [TestClass()]
    public class WordDAOTests
    {
        static List<Word> testWords;
        static WordDAO testDao;
        static readonly string tableName = "EN";
        [ClassInitialize()]
        public static void intialize(TestContext tc)
        {
            DatabaseController.getInstance().removeTable(tableName);
            DatabaseController.getInstance().addTable(tableName);
            Word.setScoringHandler(new ScoringHandler("../../ScoringHandlerDaoTests.xml"));
            testWords = new List<Word>(new Word[] { new Word("Daddy"), new Word("Mom"), new Word("Dnow"), new Word("Grandpa") });
            testDao = new WordDAO();
            foreach (Word word in testWords)
            {
                testDao.save(word, tableName);
            }
        }

        [ClassCleanup()]
        public static void cleanup()
        {
            testDao.Dispose();
            DatabaseController.getInstance().removeTable(tableName);
        }

        [TestMethod()]
        public void findAllTest()
        {
            List<Word> resultWordsAll = testDao.findAll(tableName, 7);
            CollectionAssert.AreEquivalent(testWords, resultWordsAll);
        }

        [TestMethod()] 
        public void findAllLengthTest()
        {
            List<Word> expectedWords = testWords.Where(x => { return x.Length <= 4; }).ToList();
            List<Word> resultWords = testDao.findAll(tableName, 4);
            CollectionAssert.AreEquivalent(expectedWords, resultWords);
        }

        [TestMethod()]
        public void findyByFirstCharacterTest()
        {
            List<Word> expectedWords = testWords.Where(x => { return x.Content.First() == 'd'; }).ToList();
            List<Word> resultWords = testDao.findyByFirstCharacter(tableName, 'd', 10);
            CollectionAssert.AreEquivalent(expectedWords, resultWords);
        }

        [TestMethod()]
        public void findyByFirstCharacterAndFixedLengthTest()
        {
            List<Word> expectedWords = testWords.Where(x => { return x.Content.First() == 'd' && x.Length == 5; }).ToList();
            List<Word> resultWords = testDao.findyByFirstCharacterAndFixedLength(tableName, 'd', 5);
            CollectionAssert.AreEquivalent(expectedWords, resultWords);
        }
        


    }
}