using MySql.Data.MySqlClient;
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
                    command.CommandText = @"INSERT INTO users (UserName, UserPassword) VALUES (@name1, @password1);";
                    command.Parameters.AddWithValue("@name1", "banana");
                    command.Parameters.AddWithValue("@quantity1", "password");
                    int rowCount = await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
