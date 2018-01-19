using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramTech
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