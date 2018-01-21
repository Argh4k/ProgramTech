using System;

namespace ProgramTech
{
    namespace Exceptions
    {
        public class WordControllerException : Exception
        {
            public WordControllerException()
            {

            }

            public WordControllerException(string message) : base(message)
            {

            }

            public WordControllerException(string message, Exception inner) : base(message, inner)
            {

            }
        }
    }
    
}