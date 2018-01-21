using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using System.Xml;
using System.Collections;

namespace ProgramTech
{
    public class ScoringHandler
    {
        private Dictionary<char, int> charScores = new Dictionary<char, int>();
        // Use default values
        public ScoringHandler() : this("ScoringHandler.xml")
        {

        }

        public ScoringHandler(string filePath)
        {
            try
            {
                foreach (XElement xe in XElement.Load(filePath).Elements("letter"))
                {
                    charScores.Add(xe.Element("name").Value.First(), Int32.Parse(xe.Element("value").Value));
                }
            }catch(FileNotFoundException ex)
            {
                throw new Exceptions.ScoringFileNotFound(filePath);
            }
            catch(XmlException ex)
            {
                throw new Exceptions.ScoringFileBadFormatted(ex.Message);
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
                    throw new Exceptions.ScoringLackOfLetter(ch);
                }
            }
            return value;
        }
    }
}