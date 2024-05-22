using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarService.Services
{
    public partial class ServiceCardForm : Form
    {
        private MySqlConnection connection;
        private int ID = -1;
        public ServiceCardForm(bool isNew, bool isReadOnly, string id = "-1")
        {
            InitializeComponent();
            connection = new DBConnection().GetConnectionString();

            if (isReadOnly)
            {
                textBoxName.Enabled = false;
                textBoxDescription.Enabled = false;
                textBoxDescription.Enabled = false;
            }
            if (isNew)
            {
                buttonAdd.Visible = true;
                buttonAdd.Enabled = true;
                buttonDelete.Visible = false;
                buttonDelete.Enabled = false;
                buttonUpdate.Visible = false;
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
                }
                else
                {
                    buttonDelete.Visible = true;
                    buttonDelete.Enabled = true;
                    buttonUpdate.Visible = true;
                }

                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"SELECT Services.ID, Services.ServiceName, Services.Description, Services.Price
                                    FROM Services WHERE Services.ID = @ServiceID LIMIT 1";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@ServiceID", ID);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBoxName.Text = reader.GetString("ServiceName");
                            textBoxDescription.Text = reader.GetString("Description");
                            textBoxPrice.Text = reader.GetDecimal("Price").ToString();
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
            if (textBoxName.Text.Length == 0)
            {
                MessageBox.Show("Неверно введено название.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
                textBoxName.Focus();
                return false;
            }
            if (textBoxDescription.Text.Length == 0)
            {
                MessageBox.Show("Неверно введена описание.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
                textBoxDescription.Focus();
                return false;
            }
            if (Convert.ToDecimal(textBoxPrice.Text) < 0)
            {
                MessageBox.Show("Неверно введена цена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
                textBoxPrice.Focus();
                return false;
            }
            return true;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    // Запрос для добавления нового сервиса
                    string query = "INSERT INTO Services (ServiceName, Description, Price) VALUES (@ServiceName, @Description, @Price)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    // Параметры запроса, связывание с текстовыми полями на форме
                    cmd.Parameters.AddWithValue("@ServiceName", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@Description", textBoxDescription.Text);
                    cmd.Parameters.AddWithValue("@Price", decimal.Parse(textBoxPrice.Text));

                    // Выполнение запроса
                    int result = cmd.ExecuteNonQuery();

                    // Проверка результата выполнения запроса
                    if (result > 0)
                    {
                        MessageBox.Show("Услуга добавлена.", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close(); // Закрытие формы после успешного добавления
                    }
                    else
                    {
                        MessageBox.Show("Услуга не добавлена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string query = "DELETE FROM Services WHERE id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", ID);
                object result = cmd.ExecuteNonQuery();
                if (result != null)
                {
                    MessageBox.Show("Услуга удалена.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                    string query = @"UPDATE Services SET ServiceName = @ServiceName, Description = @Description, Price = @Price WHERE ID = @ServiceID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@ServiceID", ID);
                    cmd.Parameters.AddWithValue("@ServiceName", textBoxName.Text);
                    cmd.Parameters.AddWithValue("@Description", textBoxDescription.Text);
                    cmd.Parameters.AddWithValue("@Price", decimal.Parse(textBoxPrice.Text));

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Услуга обновлена.", "Обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Услуга не обновлена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
