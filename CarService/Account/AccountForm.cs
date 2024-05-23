using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace CarService.Account
{
    public partial class AccountForm : Form
    {
        private MySqlConnection connection;
        public AccountForm()
        {
            InitializeComponent();
            connection = new DBConnection().GetConnectionString();
            textBoxFirstName.Enabled = false;
            textBoxLastName.Enabled = false;
            comboBoxPosition.Enabled = false;
            textBoxUsername.Enabled = false;
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                string query = @"SELECT Employees.ID, Employees.FirstName, Employees.LastName, Employees.Position, 
                        Employees.Username
                        FROM Employees 
                        WHERE Employees.ID = @EmployeeID LIMIT 1";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@EmployeeID", PersonalData.ID);

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        textBoxFirstName.Text = reader.GetString("FirstName");
                        textBoxLastName.Text = reader.GetString("LastName");
                        if (reader.GetString("Position") == "Administrator")
                        {
                            comboBoxPosition.Enabled = false;
                        }
                        comboBoxPosition.SelectedItem = reader.GetString("Position");
                        textBoxUsername.Text = reader.GetString("Username");
                    }
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool CheckInput()
        {
            if (textBoxOldPassword.Text.Length == 0)
            {
                MessageBox.Show("Неверно введен пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
                textBoxOldPassword.Focus();
                return false;
            }
            else
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = "SELECT Password FROM employees WHERE Username = @UserName";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@UserName", PersonalData.Login);

                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        string hashedPasswordFromDatabase = result.ToString();
                        if (!VerifyHashedPassword(hashedPasswordFromDatabase, textBoxOldPassword.Text))
                        {
                            MessageBox.Show("Неверно введён старый пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (!CheckPassword())
            {
                MessageBox.Show("Неверно введен пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
                textBoxNewPassword.Focus();
                return false;
            }
            return true;
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

        private bool CheckPassword() => textBoxNewPassword.Text.Length >= 5 && textBoxNewPassword.Text == textBoxConfPass.Text;

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 33 || e.KeyChar > 126) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBoxConfPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 33 || e.KeyChar > 126) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void checkBoxShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxShowPassword.Checked)
            {
                textBoxOldPassword.PasswordChar = '\0';
                textBoxNewPassword.PasswordChar = '\0';
                textBoxConfPass.PasswordChar = '\0';
            }
            else
            {
                textBoxOldPassword.PasswordChar = '•';
                textBoxNewPassword.PasswordChar = '•';
                textBoxConfPass.PasswordChar = '•';
            }
        }

        public static string HashPassword(string password)
        {
            byte[] salt;
            byte[] buffer2;
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, 0x10, 0x3e8))
            {
                salt = bytes.Salt;
                buffer2 = bytes.GetBytes(0x20);
            }
            byte[] dst = new byte[0x31];
            Buffer.BlockCopy(salt, 0, dst, 1, 0x10);
            Buffer.BlockCopy(buffer2, 0, dst, 0x11, 0x20);
            return Convert.ToBase64String(dst);
        }

        private void buttonChangePassword_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"UPDATE Employees SET Password = @Password WHERE ID = @ID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@ID", PersonalData.ID); // ID должен быть идентификатором сотрудника, который нужно обновить
                    cmd.Parameters.AddWithValue("@Password", HashPassword(textBoxNewPassword.Text));

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Пароль обновлен.", "Редактирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Пароль не обновлен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
        }
    }
}
