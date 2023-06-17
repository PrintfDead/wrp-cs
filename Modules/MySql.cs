using MySql.Data.MySqlClient;
using System;

namespace WashingtonRP.Modules
{
    public class MySql
    {
        public static MySqlConnection connection;
        private MySql()
        {

        }
        public static void Init()
        {
            try
            {
                connection = new MySqlConnection("server=localhost; database=washington; user=root; password=;");
                connection.Open();

                Console.WriteLine(">>> Connected to database");
            }
            catch (Exception e)
            {
                Console.WriteLine(">>> Failed to connect to database. Reason: " + e.Message);
            }
        }
        public static MySqlDataReader Reader(string query)
        {
            //connection.Close();
            //Init();
            return new MySqlCommand(query, connection).ExecuteReader();
        }
        public static MySqlCommand Query(string query)
        {
            return new MySqlCommand(query, connection);
        }
    }
}
