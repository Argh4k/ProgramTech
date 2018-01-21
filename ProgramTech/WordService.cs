using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ProgramTech
{
    public class WordService : IDisposable
    {
        ///Instead of using static variable it would be better to implement this as singleton with dispensable interface
        WordDAO staticDao;

        private static WordService instance;

        private WordService()
        {
            staticDao = new WordDAO();
        }

        public static WordService getInstance()
        {
            if(instance == null)
            {
                instance = new WordService();
            }
            return instance;
        }

        public List<Word> getAll(Language language, int maxLength, bool async = false)
        {
            if(async)
            {
                using (var localDao = new WordDAO())
                {
                    return localDao.findAll(language.ToString(), maxLength);
                }
            } else
            {
                return staticDao.findAll(language.ToString(), maxLength);
            }
        }

        public List<Word> getByFirstCharacter(Language language, char character, int maxLength, bool async = false)
        {
            if (async)
            {
                using (var localDao = new WordDAO())
                {
                    return localDao.findyByFirstCharacter(language.ToString(), character, maxLength);
                }
            }
            else
            {
                return staticDao.findyByFirstCharacter(language.ToString(), character, maxLength);
            }
        }

        public List<Word> getByFirstCharacterAndLength(Language language, char character, int length, bool async = false)
        {
            if (async)
            {
                using (var localDao = new WordDAO())
                {
                    return localDao.findyByFirstCharacterAndFixedLength(language.ToString(), character, length);
                }
            }
            else
            {
                return staticDao.findyByFirstCharacterAndFixedLength(language.ToString(), character, length);
            }
        }

        public bool save(Word word, Language language, bool async = false)
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

    
        public bool saveList(List<Word> words, Language language, bool async = false)
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
                    staticDao.saveBulk(wordTable, language.ToString());
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

        public void Dispose()
        {
            staticDao.Dispose();
        }
    }
}