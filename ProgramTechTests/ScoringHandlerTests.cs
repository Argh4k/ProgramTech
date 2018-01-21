using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgramTech;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProgramTech.Tests
{
    [TestClass()]
    public class ScoringHandlerTests
    {
        ScoringHandler testHandler = new ScoringHandler("../../ScoringHandlerTestCase.xml");

        [TestMethod()]
        public void scoreWordTest()
        {
            string word = "abcd";
            Assert.AreEqual(14, testHandler.scoreWord(word));
        }

        [TestMethod()]
        public void scoreWordEmpty()
        {
            string word = "";
            Assert.AreEqual(0, testHandler.scoreWord(word));
        }

        [TestMethod()]
        public void scoreWordNotLetter()
        {
            string word = "_";
            Assert.AreEqual(0, testHandler.scoreWord(word));
        }

        [TestMethod()]
        [ExpectedException(typeof(Exceptions.ScoringLackOfLetter))]
        public void scoreWordBadLetter()
        {
            string word = "de";
            testHandler.scoreWord(word);
        }

        [TestMethod()]
        [ExpectedException(typeof(Exceptions.ScoringFileNotFound))]
        public void scoreFileNotFound()
        {
            int i = 0;
            string fileName = "abcdfgh";
            while(File.Exists(fileName))
            {
                fileName += i;
                i++;
            }
            ScoringHandler sc = new ScoringHandler(fileName);
        }

        [TestMethod]
        [ExpectedException(typeof(Exceptions.ScoringFileBadFormatted))]
        public void scoreFileBadFormatted()
        {
            ScoringHandler sc = new ScoringHandler("../../ScoringHandlerBadFormatted.xml");
        }
    }
}