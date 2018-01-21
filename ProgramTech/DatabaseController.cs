using System;
using System.Data;
using System.Data.SqlClient;


namespace ProgramTech
{
    public class DatabaseController : IDisposable
    {
        private static DatabaseController instance;
        private string databaseFileName = "Dictionary.mdf";
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(DatabaseController));
        private SqlConnection connection;

        private DatabaseController()
        {
            connection = getSqlConnection();
        }

        public static DatabaseController getInstance()
        {
            if(instance == null)
            {
                instance = new DatabaseController();
            }
            return instance;
        }

        public SqlConnection getSqlConnection()
        {
            string databaseName = System.IO.Path.GetFileNameWithoutExtension(databaseFileName);
            var sqlConnection = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=|DataDirectory|Dictionary.mdf;Initial Catalog=Dictionary;Integrated Security=True");
            sqlConnection.Open();
            return sqlConnection;

        }

        public void addTable(string tableName)
        {
           
            if(!checkTableExists(tableName))
            {
                log.Info(String.Format("Creating database {0}", tableName));
                string query = string.Format("CREATE TABLE {0}(word varchar(50), score int, first_letter char(1), length int);", tableName);
                using (var command2 = new SqlCommand(query, connection))
                    command2.ExecuteNonQuery();
            }
        }

        public void removeTable(string tableName)
        {
            if(checkTableExists(tableName))
            using (SqlCommand cmd = new SqlCommand(String.Format("DROP TABLE {0}", tableName), connection)) 
            {
                cmd.ExecuteNonQuery();
            }
        }

        public bool checkTableExists(string tableName)
        {
            int exists = 0;
            using (SqlCommand cmd = new SqlCommand(@"IF EXISTS(
                 SELECT 1 FROM INFORMATION_SCHEMA.TABLES 
                WHERE TABLE_NAME = @table) 
                 SELECT 1 ELSE SELECT 0", connection))
            {
                cmd.Parameters.Add("@table", SqlDbType.NVarChar).Value = tableName;
                exists = (int)cmd.ExecuteScalar();
            }
            return exists == 1;
        }

        public void Dispose()
        {
            connection.Close();
        }
    }

}