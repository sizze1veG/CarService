using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    internal class DBConnection
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public DBConnection()
        {
            server = "localhost";
            database = "carservice";
            uid = "locAdmin";
            password = "locAdmin";
        }

        public MySqlConnection GetConnectionString()
        {
            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};charset=utf8mb4";
            connection = new MySqlConnection(connectionString);
            return connection;
        }
    }
}
