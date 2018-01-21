using System;

namespace ProgramTech
{
    namespace Exceptions
    {
        public class ScoringFileBadFormatted : ScoringHandlerException
        {
            public ScoringFileBadFormatted() : base("Scoring file is badly formatted")
            {

            }

            public ScoringFileBadFormatted(string additionalInformation) : base("Scoring file is badly formated - " + additionalInformation)
            {

            }

            public ScoringFileBadFormatted(string additionalInformation, Exception inner) : base("Scoring file is badly formated - " + additionalInformation, inner)
            {

            }
        }
    }
    
}