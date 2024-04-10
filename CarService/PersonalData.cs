using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarService
{
    public class PersonalData
    {
        private MySqlConnection connection;
        public static int ID {  get; private set; }
        public static string FirstName { get; private set; }
        public static string LastName { get; set; }
        public static string Position { get; private set; }
        public static string Login { get; private set; }
        public static int RoleID { get; private set; }

        public bool SetUserData(string username, string password)
        {
            connection = new DBConnection().GetConnectionString();

            connection.Open();

            string query = "SELECT * FROM employees WHERE Username = @UserName";
            using (MySqlCommand cmd = new MySqlCommand(query, connection))
            {
                cmd.Parameters.AddWithValue("@UserName", username);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                        ID = (int)reader["ID"];
                        FirstName = reader["FirstName"].ToString();
                        LastName = reader["LastName"].ToString();
                        Position = reader["position"].ToString();
                        Login = reader["Username"].ToString();
                        RoleID = (int)reader["RoleID"];
                        connection.Close();
                        return true;
                    }
                }
                connection.Close();
                return false;
            } 
        }
    }
}
