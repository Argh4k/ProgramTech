using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ProgramTech
{
    public class WordService
    {
        ///Instead of using static variable it would be better to implement this as singleton with dispensable interface
        static WordDAO staticDao;
        static WordService()
        {
            staticDao = new WordDAO();
        }

        public static List<Word> getAll(Language language, int maxLength, bool async = false)
        {
            if(async)
            {
                using (var localDao = new WordDAO())
                {
                    return localDao.findAll(language, maxLength);
                }
            } else
            {
                return staticDao.findAll(language, maxLength);
            }
        }

        public static List<Word> getByFirstCharacter(Language language, char character, int maxLength, bool async = false)
        {
            if (async)
            {
                using (var localDao = new WordDAO())
                {
                    return localDao.findyByFirstCharacter(language, character, maxLength);
                }
            }
            else
            {
                return staticDao.findyByFirstCharacter(language, character, maxLength);
            }
        }

        public static bool save(Word word, Language language, bool async = false)
        {
            if (async)
            {
                using (var localDao = new WordDAO())
                {
                    return localDao.save(word, language.ToString());
                }
            }
            else
            {
                return staticDao.save(word, language.ToString());
            }
        }

    
        public static bool saveList(List<Word> words, Language language, bool async = false)
        {
            DatabaseController.addTable(language.ToString());
            DataTable wordTable = toDataTable(words);
            if (async)
            {
                using (var localDao = new WordDAO())
                {
                    foreach (Word word in words)
                    {
                        localDao.saveBulk(wordTable, language.ToString());
                    }
                }
            }
            else
            {
                //foreach (Word word in words)
                //{
                    staticDao.saveBulk(wordTable, language.ToString());
                //}
            }
            return true;
        }

        private static DataTable toDataTable(IList<Word> words)
        {
            DataTable dt = new DataTable();
            dt.Clear();
            dt.Columns.Add("word", typeof(string));
            dt.Columns.Add("score", typeof(int));
            dt.Columns.Add("first_letter", typeof(char));
            dt.Columns.Add("length", typeof(int));
            DataRow dr;
            foreach (Word word in words)
            {
                dr = dt.NewRow();
                dr["word"] = word.Content;
                dr["score"] = word.Score;
                dr["first_letter"] = word.Content.First();
                dr["length"] = word.Length;
                dt.Rows.Add(dr);
            }
            return dt;
        }
    }
}