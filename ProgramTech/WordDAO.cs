using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ProgramTech
{
    public class WordDAO
    {
        public static List<Word> findAll(Language language)
        {
            List<Word> toReturn = new List<Word>(); ;
            var connection = DatabaseController.getSqlConnection();
            string query = string.Format("SELECT * FROM {0}", language.ToString());
            using (var command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["word"].ToString());
                        toReturn.Add(new Word(reader["word"].ToString()));
                    }
                }
            }
            return toReturn; 
        }

        public static List<Word> findyByFirstCharacter(Language language, char character)
        {
            List<Word> toReturn = new List<Word>(); ;
            var connection = DatabaseController.getSqlConnection();
            string query = string.Format("SELECT * FROM {0} WHERE first_letter = '{1}' ORDER BY SCORE DESC", language.ToString());
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


        public static bool save(Word word, string language)
        {
            var connection = DatabaseController.getSqlConnection();
            string query = string.Format("INSERT INTO {0} VALUES ('{1}', '{2}', '{3}', '{4}');", language, word.Content, word.Score, word.Content.First(), word.Length);
            using (var command = new SqlCommand(query, connection))
                command.ExecuteNonQuery();
            return true;
        }
            
    }
}