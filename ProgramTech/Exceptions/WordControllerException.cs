using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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