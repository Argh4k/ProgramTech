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

        public Word(string content)
        {
            this.content = content;
            this.length = content.Length;
            this.score = 5;
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
    }
}