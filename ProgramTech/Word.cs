using System;
using System.Linq;

namespace ProgramTech
{
    public class Word
    {
        private char firstLetter;
        private int length;
        private int score;
        private string content;
        private static ScoringHandler scoringHandler;

        public Word(string _content)
        {
            this.content = _content.ToLower();
            this.length = this.content.Length;
            this.score = scoringHandler.scoreWord(this.content);
            this.firstLetter = this.content.First();
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

        public override bool Equals(Object obj)
        {
            // Check for null values and compare run-time types.
            if (obj == null || GetType() != obj.GetType())
                return false;

            Word w = (Word)obj;
            return this.content == w.Content;
        }
        public override int GetHashCode()
        {
            return content.GetHashCode();
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