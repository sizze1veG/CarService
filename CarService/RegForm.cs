using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CarService
{
    public partial class RegForm : Form
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public RegForm()
        {
            InitializeComponent();
            server = "localhost";
            database = "carservice";
            uid = "locAdmin";
            password = "locAdmin";

            string connectionString = $"SERVER={server};DATABASE={database};UID={uid};PASSWORD={password};charset=utf8mb4";
            connection = new MySqlConnection(connectionString);  
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonReg_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                if (!CheckFirstName())
                {
                    MessageBox.Show("Неверно введено имя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                    textBoxFrstName.Focus();
                    return;
                }
                else if (!CheckLastName())
                {
                    MessageBox.Show("Неверно введена фамилия.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                    textBoxLastName.Focus();
                    return;
                }
                else if (!CheckPosition()) 
                {
                    MessageBox.Show("Не выбрана должность.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                    comboBoxPosition.Focus();
                    return;
                }
                else if (textBoxLogin.Text.Length < 5)
                {
                    MessageBox.Show("Логин слишком короткий. Минимальная длина логина 5 символов",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                    textBoxLogin.Focus();
                    return;
                }
                else if (!CheckLogin())
                {
                    MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBoxLogin.Focus();
                    return;
                }
                else if (!CheckPassword())
                {
                    MessageBox.Show("Пароли не совпадают или пароль слишком короткий. Минимальная длина пароля 5 символов",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                    textBoxPassword.Focus();
                    return;
                }
                else
                {
                    string query = "INSERT INTO employees (FirstName, LastName, Position, Username, Password, RoleID)" +
                        " VALUES (@FirstName, @LastName, @Position, @Username, @Password, @RoleID)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@FirstName", textBoxFrstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", textBoxLastName.Text);
                    cmd.Parameters.AddWithValue("@Position", comboBoxPosition.Text);
                    cmd.Parameters.AddWithValue("@Username", textBoxLogin.Text);
                    cmd.Parameters.AddWithValue("@Password", HashPassword(textBoxPassword.Text));
                    cmd.Parameters.AddWithValue("@RoleID", comboBoxPosition.SelectedIndex + 2);
                    cmd.ExecuteNonQuery();

                    connection.Close();

                    MessageBox.Show("Аккаунт успешно зарегистрирован.", "Регистрация",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    ClearAllFields();
                }    
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при открытии соединения: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
            }

        }

        private void labelLogIn_Click(object sender, EventArgs e)
        {
            new LogInForm().Show();
            Hide();
        }

        private void labelClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        #region check input data

        private void textBoxFrstName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBoxLastName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private bool CheckFirstName() => textBoxFrstName.Text.Length > 2;

        private bool CheckLastName() => textBoxLastName.Text.Length > 2;

        private bool CheckPosition() => comboBoxPosition.SelectedIndex != -1;

        private bool CheckLogin()
        {
            string userLogin = textBoxLogin.Text;

            string checkQuery = "SELECT COUNT(*) FROM employees WHERE Username = @UserName";
            MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection);
            checkCmd.Parameters.AddWithValue("@UserName", userLogin);
            int userCount = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (userCount > 0)
            {
                connection.Close();
                return false;
            }
            return true;
        }

        private void textBoxLogin_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 33 || e.KeyChar > 126) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private bool CheckPassword() => textBoxPassword.Text.Length >= 5 && textBoxPassword.Text == textBoxConfirmPassword.Text;

        private void textBoxPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 33 || e.KeyChar > 126) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void textBoxConfirmPassword_KeyPress(object sender, KeyPressEventArgs e)
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
                textBoxPassword.PasswordChar = '\0';
                textBoxConfirmPassword.PasswordChar = '\0';
            }
            else
            {
                textBoxPassword.PasswordChar = '•';
                textBoxConfirmPassword.PasswordChar = '•';
            }
        }

        #endregion

        private void ClearAllFields()
        {
            textBoxFrstName.Clear();
            textBoxLastName.Clear();
            comboBoxPosition.SelectedIndex = -1;
            textBoxLogin.Clear();
            textBoxPassword.Clear();
            textBoxConfirmPassword.Clear();
        }
    }
}
