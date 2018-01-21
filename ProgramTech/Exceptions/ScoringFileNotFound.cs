using System;

namespace ProgramTech
{
    namespace Exceptions
    {
        public class ScoringFileNotFound : ScoringHandlerException
        {
            public ScoringFileNotFound() : base("File with scoring not found")
            {

            }

            public ScoringFileNotFound(string filepath) : base("File with scoring not found at " + filepath)
            {

            }

            public ScoringFileNotFound(string filepath, Exception inner) : base("File with scoring not found at " + filepath, inner)
            {

            }
        }
    }
    
}