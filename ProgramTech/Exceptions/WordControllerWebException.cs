using System;

namespace ProgramTech
{
    namespace Exceptions
    {
        public class WordControllerWebException : WordControllerException
        {
            public WordControllerWebException() : base("File with words not found")
            {

            }

            public WordControllerWebException(string addres) : base(String.Format("{0} could not find appropiate file under this addres", addres))
            {

            }

            public WordControllerWebException(string addres, Exception inner) : base(String.Format("{0} could not find appropiate file under this addres", addres), inner)
            {

            }
        }
    }
    
}