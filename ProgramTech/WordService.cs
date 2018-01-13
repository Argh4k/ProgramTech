using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProgramTech
{
    public class WordService
    {
        public static List<Word> getAll(Language language)
        {
            throw new System.NotImplementedException();
        }

        public static List<Word> getByFirstCharacter(string language, string character)
        {
            throw new System.NotImplementedException();
        }

        public static bool save(string word)
        {
            throw new System.NotImplementedException();
        }

        public static bool saveList(List<Word> words, Language language)
        {
            DatabaseController.addTable(language.ToString());
            foreach(Word word in words)
            {
                WordDAO.save(word, language.ToString());
            }
            DatabaseController.closeConnection();
            return true;
        }
    }
}