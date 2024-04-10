using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace CarService
{
    public partial class LogInForm : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;
        public LogInForm()
        {
            InitializeComponent();
            //server = "localhost";
            //database = "carservice";
            //uid = "locAdmin";
            //password = "locAdmin";

            //string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};charset=utf8mb4";
            //connection = new MySqlConnection(connectionString);

            connection = new DBConnection().GetConnectionString();
        }

        private void buttonLog_Click(object sender, EventArgs e)
        {
            string authUserName = textBoxLogin.Text;
            string authPassword = textBoxPassword.Text;

            if (string.IsNullOrWhiteSpace(authUserName) || string.IsNullOrWhiteSpace(authPassword))
            {
                MessageBox.Show("Пожалуйста, введите имя пользователя и пароль для аутентификации.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                connection.Open();

                string query = "SELECT Password FROM employees WHERE Username = @UserName";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@UserName", authUserName);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    string hashedPasswordFromDatabase = result.ToString();
                    if (VerifyHashedPassword(hashedPasswordFromDatabase, authPassword))
                    {
                        var account = new PersonalData();
                        if (account.SetUserData(authUserName, hashedPasswordFromDatabase))
                        {
                            MessageBox.Show("Пользователь авторизирован.", "Авторизация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Hide();
                            new MainForm().ShowDialog();
                        }                        
                    }
                    else
                    {
                        MessageBox.Show("Ошибка аутентификации. Неправильное имя пользователя или пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка аутентификации. Пользователь с таким именем не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                    connection.Close();
            }

        }

        private void labelCreateAccount_Click(object sender, EventArgs e)
        {
            new RegForm().Show();
            Hide();
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x31) || (src[0] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        public static bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPassword.Checked)
            {
                textBoxPassword.PasswordChar = '\0';
            }
            else
            {
                textBoxPassword.PasswordChar = '•';
            }
        }

        private void textBoxLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 33 || e.KeyChar > 126) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 33 || e.KeyChar > 126) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }
    }
}
