using System;

namespace ProgramTech
{
    namespace Exceptions
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
   
}