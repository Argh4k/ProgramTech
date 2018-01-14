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
            return WordDAO.findAll(language);
        }

        public static List<Word> getByFirstCharacter(Language language, char character)
        {
            return WordDAO.findyByFirstCharacter(language, character);
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