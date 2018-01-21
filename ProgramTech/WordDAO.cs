using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;

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
            List<Word> toReturn = new List<Word>(); ;
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


        public bool save(Word word, string language)
        {
            string query = string.Format("INSERT INTO {0} VALUES ('{1}', '{2}', '{3}', '{4}');", language, word.Content, word.Score, word.Content.First(), word.Length);
            using (var command = new SqlCommand(query, connection))
                try
                {
                    command.ExecuteNonQuery();
                } catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            return true;
        }

        public bool saveBulk(DataTable wordsTable, string language)
        {
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