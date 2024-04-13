using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    internal class MySqlCreate
    {
        static async Task Main(string[] args)
        {
            var builder = new MySqlConnectionStringBuilder
            {
                Server = "localhost_3306",
                Database = "carservice",
                UserID = "locAdmin",
                Password = "locAdmin",
                SslMode = MySqlSslMode.Required,
            };

            using (var conn = new MySqlConnection(builder.ConnectionString))
            {
                await conn.OpenAsync();

                using (var command = conn.CreateCommand())
                {
                    //command.CommandText = "DROP TABLE IF EXISTS inventory;";
                    //await command.ExecuteNonQueryAsync();

                    //Console.WriteLine("Finished dropping table (if existed)");

                    //command.CommandText = "CREATE TABLE inventory (id serial PRIMARY KEY, name VARCHAR(50), quantity INTEGER);";
                    //await command.ExecuteNonQueryAsync();
                    //Console.WriteLine("Finished creating table");

                    command.CommandText = @"INSERT INTO users (UserName, UserPassword) VALUES (@name1, @password1);";
                    command.Parameters.AddWithValue("@name1", "banana");
                    command.Parameters.AddWithValue("@quantity1", "password");
                    //command.Parameters.AddWithValue("@name2", "orange");
                    //command.Parameters.AddWithValue("@quantity2", 154);
                    //command.Parameters.AddWithValue("@name3", "apple");
                    //command.Parameters.AddWithValue("@quantity3", 100);

                    int rowCount = await command.ExecuteNonQueryAsync();
                    //Console.WriteLine(String.Format("Number of rows inserted={0}", rowCount));
                }
            }
        }
    }
}
