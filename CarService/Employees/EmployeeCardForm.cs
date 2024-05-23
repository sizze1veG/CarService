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

namespace CarService.Employees
{
    public partial class EmployeeCardForm : Form
    {
        private MySqlConnection connection;
        private int ID = -1;
        public EmployeeCardForm(bool isNew, bool isReadOnly, string id = "-1")
        {
            InitializeComponent();
            connection = new DBConnection().GetConnectionString();

            if (isReadOnly)
            {
                textBoxFirstName.Enabled = false;
                textBoxLastName.Enabled = false;
                comboBoxPosition.Enabled = false;
                textBoxUsername.Enabled = false;
                textBoxPassword.Visible = false;
                textBoxConfPass.Visible = false;
            }
            if (isNew)
            {
                buttonAdd.Visible = true;
                buttonAdd.Enabled = true;
                buttonDelete.Visible = false;
                buttonDelete.Enabled = false;
                buttonUpdate.Visible = false;
                textBoxPassword.Visible = true;
                textBoxConfPass.Visible = true;
            }
            else
            {
                ID = Convert.ToInt32(id);
                buttonAdd.Visible = false;
                buttonAdd.Enabled = false;
                if (isReadOnly)
                {
                    buttonAdd.Visible = false;
                    buttonDelete.Visible = false;
                    buttonUpdate.Visible = false;
                    textBoxPassword.Visible = false;
                    textBoxConfPass.Visible = false;
                }
                else
                {
                    buttonDelete.Visible = true;
                    buttonDelete.Enabled = true;
                    buttonUpdate.Visible = true;
                    textBoxPassword.Visible = true;
                    textBoxConfPass.Visible = true;
                }

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    string query = @"SELECT Employees.ID, Employees.FirstName, Employees.LastName, Employees.Position, 
                        Employees.Username
                        FROM Employees 
                        WHERE Employees.ID = @EmployeeID LIMIT 1";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@EmployeeID", ID);

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
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private bool CheckInput()
        {
            if (textBoxFirstName.Text.Length == 0)
            {
                MessageBox.Show("Неверно введено имя.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
                textBoxFirstName.Focus();
                return false;
            }
            if (textBoxLastName.Text.Length == 0)
            {
                MessageBox.Show("Неверно введена фамилия.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
                textBoxLastName.Focus();
                return false;
            }
            if (textBoxUsername.Text.Length == 0)
            {
                MessageBox.Show("Неверно введен логин.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
                textBoxUsername.Focus();
                return false;
            }
            if (textBoxPassword.Text.Length != 0 && !CheckPassword())
            {
                MessageBox.Show("Неверно введен пароль.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
                textBoxPassword.Focus();
                return false;
            }
            if (comboBoxPosition.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите должность.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
                comboBoxPosition.Focus();
                return false;
            }
            return true;
        }

        private bool CheckLogin()
        {
            string userLogin = textBoxUsername.Text;

            if (connection.State == ConnectionState.Closed)
                connection.Open();
            string checkQuery = "SELECT COUNT(*) FROM employees WHERE Username = @UserName";
            MySqlCommand checkCmd = new MySqlCommand(checkQuery, connection);
            checkCmd.Parameters.AddWithValue("@UserName", userLogin);
            int userCount = Convert.ToInt32(checkCmd.ExecuteScalar());

            if (userCount > 0)
            {
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

        private bool CheckPassword() => textBoxPassword.Text.Length >= 5 && textBoxPassword.Text == textBoxConfPass.Text;

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
                textBoxPassword.PasswordChar = '\0';
                textBoxConfPass.PasswordChar = '\0';
            }
            else
            {
                textBoxPassword.PasswordChar = '•';
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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                if (!CheckLogin())
                {
                    MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connection.Close();
                    textBoxUsername.Focus();
                    return;
                }
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    // Запрос для добавления новой машины
                    string query = "INSERT INTO employees (FirstName, LastName, Position, Username, Password, RoleID)" +
                        " VALUES (@FirstName, @LastName, @Position, @Username, @Password, @RoleID)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@FirstName", textBoxFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", textBoxLastName.Text);
                    cmd.Parameters.AddWithValue("@Position", comboBoxPosition.Text);
                    cmd.Parameters.AddWithValue("@Username", textBoxUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", HashPassword(textBoxPassword.Text));
                    cmd.Parameters.AddWithValue("@RoleID", comboBoxPosition.SelectedIndex + 2);

                    // Выполнение запроса
                    int result = cmd.ExecuteNonQuery();

                    // Проверка результата выполнения запроса
                    if (result > 0)
                    {
                        MessageBox.Show("Сотрудник добавлен.", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close(); // Закрытие формы после успешного добавления
                    }
                    else
                    {
                        MessageBox.Show("Сотрудник не добавлен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show("Такая запись уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                if (ID == PersonalData.ID)
                {
                    MessageBox.Show("Вы не можете удалить свою учетную запись.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                string query = "DELETE FROM Employees WHERE id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", ID);
                object result = cmd.ExecuteNonQuery();
                if (result != null)
                {
                    MessageBox.Show("Запись удалена.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
                }
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show("Запись невозможно удалить из-за связи с другим объектом.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"UPDATE Employees 
                    SET FirstName = @FirstName, LastName = @LastName, Position = @Position, Username = @Username, Password = @Password, RoleID = @RoleID
                    WHERE ID = @ID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@ID", ID); // ID должен быть идентификатором сотрудника, который нужно обновить
                    cmd.Parameters.AddWithValue("@FirstName", textBoxFirstName.Text);
                    cmd.Parameters.AddWithValue("@LastName", textBoxLastName.Text);
                    cmd.Parameters.AddWithValue("@Position", comboBoxPosition.Text);
                    cmd.Parameters.AddWithValue("@Username", textBoxUsername.Text);
                    cmd.Parameters.AddWithValue("@Password", HashPassword(textBoxPassword.Text));
                    cmd.Parameters.AddWithValue("@RoleID", comboBoxPosition.SelectedIndex + 2);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Сотрудник обновлен.", "Редактирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Сотрудник не обновлен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
