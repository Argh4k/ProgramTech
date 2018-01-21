using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramTech
{
    namespace Exceptions
    {
        public class WordControllerFileNotFoundException : WordControllerException
        {
            public WordControllerFileNotFoundException() : base("File with words not found")
            {

            }

            public WordControllerFileNotFoundException(string filepath) : base("File with words not found at " + filepath)
            {

            }

            public WordControllerFileNotFoundException(string filepath, Exception inner) : base("File with words not found at " + filepath, inner)
            {

            }
        }
    }
    
}