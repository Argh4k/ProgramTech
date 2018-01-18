using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ProgramTech
{
    public class WordController
    {
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
                    }
                }
            }
            return WordService.saveList(words, language);
        }

        public List<Word> downloadDictionary(Language language)
        {
            throw new System.NotImplementedException();
        }
    }
}