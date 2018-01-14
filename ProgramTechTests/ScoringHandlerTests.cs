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
        [ExpectedException(typeof(KeyNotFoundException))]
        public void scoreWordBadLetter()
        {
            string word = "de";
            testHandler.scoreWord(word);
        }
    }
}