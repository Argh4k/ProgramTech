using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace ProgramTech
{
    public class WordDAO : IDisposable
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(WordDAO));
        SqlConnection connection;

        public WordDAO()
        {
            connection = DatabaseController.getInstance().getSqlConnection();
        }

        public List<Word> findAll(string language, int maxLength)
        {
            List<Word> toReturn = new List<Word>();
            string query = string.Format("SELECT * FROM {0} WHERE length <= {1} ORDER BY SCORE DESC", language, maxLength);
            using (var command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        toReturn.Add(new Word(reader["word"].ToString()));
                    }
                }
            }
            return toReturn; 
        }

        public List<Word> findyByFirstCharacter(string language, char character, int maxLength)
        {
            List<Word> toReturn = new List<Word>(); ;
            string query = string.Format("SELECT * FROM {0} WHERE first_letter = '{1}' AND length <= {2} ORDER BY SCORE DESC", language, character, maxLength);
            using (var command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        toReturn.Add(new Word(reader["word"].ToString()));
                    }
                }
            }
            return toReturn;
        }

        public List<Word> findyByFirstCharacterAndFixedLength(string language, char character, int length)
        {
            List<Word> toReturn = new List<Word>(); ;
            string query = string.Format("SELECT * FROM {0} WHERE first_letter = '{1}' AND length = {2} ORDER BY SCORE DESC", language, character, length);
            using (var command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        toReturn.Add(new Word(reader["word"].ToString()));
                    }
                }
            }
            return toReturn;
        }

        public bool save(Word word, string language)
        {
            string query = string.Format("INSERT INTO {0} VALUES (@cont, @scr, @first, @length);", language);
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.Add("@cont", SqlDbType.NVarChar, 50).Value = word.Content;
                command.Parameters.Add("@scr", SqlDbType.Int).Value = word.Score;
                command.Parameters.Add("@first", SqlDbType.NChar).Value = word.Content.First();
                command.Parameters.Add("@length", SqlDbType.Int).Value = word.Length;

                try
                {
                    command.ExecuteNonQuery();
                }
                catch (SqlException ex)
                {
                    log.Info(ex.Message);
                    return false;
                }
            }
            return true;
        }

        public bool saveBulk(DataTable wordsTable, string language)
        {
            connection = DatabaseController.getInstance().getSqlConnection();
            try
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    bulkCopy.DestinationTableName = language;
                    bulkCopy.WriteToServer(wordsTable);
                }
            } catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }

        

        public void Dispose()
        {
            connection.Close();
        }
    }
}