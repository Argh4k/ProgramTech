using System;

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