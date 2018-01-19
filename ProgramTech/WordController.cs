using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ProgramTech
{
    public class WordController
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(WordController));
        public bool addDictionaryFromFile(Language language, string path)
        {
            List<Word> words = new List<Word>();
            if(!File.Exists(path))
            {
                return false;
            } else
            {
                foreach(string wordstring in File.ReadLines(path))
                {
                    if(Word.isVaild(wordstring))
                    {
                        words.Add(new Word(wordstring));
                    } else
                    {
                        //Wanted to do this, this way but it takes too much time
                        log.Info(String.Format("{0} was not added to database, because it is not forged only from letters", wordstring));
                    }
                }
            }
            return WordService.getInstance().saveList(words, language);
        }

        public List<Word> downloadDictionary(Language language)
        {
            throw new System.NotImplementedException();
        }
    }
}