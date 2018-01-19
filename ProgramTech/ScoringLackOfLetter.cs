using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramTech
{
    public class ScoringLackOfLetter : ScoringHandlerException
    {
        public ScoringLackOfLetter() : base("There is no letter in scoring file")
        {

        }

        public ScoringLackOfLetter(char letter) : base(String.Format("There is no letter {0} in scoring file", letter))
        {

        }

    }
}