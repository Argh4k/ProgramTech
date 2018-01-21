using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramTech
{
    namespace Exceptions
    {
        public abstract class ScoringHandlerException : Exception
        {
            public ScoringHandlerException()
            {

            }

            public ScoringHandlerException(string message) : base(message)
            {

            }

            public ScoringHandlerException(string message, Exception inner) : base(message, inner)
            {

            }
        }
    }
   
}