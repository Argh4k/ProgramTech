using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramTech
{
    public class Word
    {
        private char firstLetter;
        private string id;
        private int length;
        private int score;
        private string content;
        private static ScoringHandler scoringHandler;

        public Word(string content)
        {
            this.content = content.ToLower();
            this.length = content.Length;
            this.score = scoringHandler.scoreWord(this.content);
            this.firstLetter = content.First();
        }

        public string Content
        {
            get
            {
                return content;
            }

            set
            {
            }
        }

        public int Length
        {
            get
            {
                return length;
            }
        }

        public int Score
        {
            get
            {
                return score;
            }

        }

        static public Boolean isVaild(string word)
        {
            foreach(char c in word)
            {
                if(!Char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }

        static public void setScoringHandler(ScoringHandler sc)
        {
            scoringHandler = sc;
        }

        
    }
}