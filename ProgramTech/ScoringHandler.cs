using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace ProgramTech
{
    public class ScoringHandler
    {
        private Dictionary<char, int> charScores = new Dictionary<char, int>();
        // Use default values
        public ScoringHandler()
        {

        }

        public ScoringHandler(string filePath)
        {
           foreach(XElement xe in XElement.Load(filePath).Elements("letter"))
            {
                charScores.Add(xe.Element("name").Value.First(), Int32.Parse(xe.Element("value").Value));
            }
        }


        public int scoreWord(string word)
        {
            int value = 0;
            foreach(char ch in word)
            {
                try
                {
                    if(char.IsLetter(ch))
                    {
                        value += charScores[ch];
                    }
                  
                } 
                catch(KeyNotFoundException ex)
                {
                    Console.WriteLine("Could not find {0} in letters", ch);
                    throw ex;
                }
            }
            return value;
        }
    }
}