using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;


namespace ProgramTech
{
    public class DatabaseController
    {
        static string databaseFileName = "Dictionary.mdf";
        static SqlConnection connection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Dictionary.mdf;Initial Catalog=Dictionary;Integrated Security=True");
        static Boolean isConnectionOpen = false;
        public static SqlConnection getSqlConnection()
        {
            string databaseName = System.IO.Path.GetFileNameWithoutExtension(databaseFileName);
            if(!isConnectionOpen)
            {
                try
                {
                    connection.Open();
                    isConnectionOpen = true;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return connection;

        }

        public static void closeConnection()
        {
            connection.Close();
            isConnectionOpen = false;
        }



        public static void addTable(string tableName)
        {
            var connection = getSqlConnection();
            string checkQuery = string.Format("SELECT db_id('{0}')", tableName);
            using (var command = new SqlCommand(checkQuery, connection))
            {
                //If table does not exist, add one
                if (command.ExecuteScalar() != DBNull.Value)
                {
                    string query = string.Format("CREATE TABLE {0}(word varchar(50), score int, first_letter char(1), length int);", tableName);
                    using (var command2 = new SqlCommand(query, connection))
                        command2.ExecuteNonQuery();
                }
            }
                
            closeConnection();
        }

    }

}