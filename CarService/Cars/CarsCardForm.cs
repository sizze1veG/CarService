using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarService.Cars
{
    public partial class CarsCardForm : Form
    {
        private MySqlConnection connection;
        private int ID = -1;
        public CarsCardForm(bool isNew, bool isReadOnly, string id = "-1")
        {
            InitializeComponent();
            connection = new DBConnection().GetConnectionString();

            if (isReadOnly)
            {
                textBoxBrand.Enabled = false;
                textBoxModel.Enabled = false;
                textBoxYear.Enabled = false;
                maskedTextBoxLicensePlate.Enabled = false;
                comboBoxClient.Enabled = false;
            }
            if (isNew)
            {
                buttonAdd.Visible = true;
                buttonAdd.Enabled = true;
                buttonDelete.Visible = false;
                buttonDelete.Enabled = false;
                buttonUpdate.Visible = false;
                SearchClients();
            }
            else
            {
                ID = Convert.ToInt32(id);
                buttonAdd.Visible = false;
                buttonAdd.Enabled = false;
                SearchClients();
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
                    string query = @"SELECT Cars.ID, Cars.Brand, Cars.Model, Cars.Year, Cars.LicensePlate, Cars.ClientID, 
                                    CONCAT(Clients.LastName, ' ', Clients.FirstName) AS ClientName FROM Cars 
                                    INNER JOIN Clients ON Cars.ClientID = Clients.ID 
                                    WHERE Cars.ID = @CarID LIMIT 1";

                    MySqlCommand command = new MySqlCommand(query, connection);
                    command.Parameters.AddWithValue("@CarID", ID);

                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            textBoxBrand.Text = reader.GetString("Brand");
                            textBoxModel.Text = reader.GetString("Model");
                            textBoxYear.Text = reader.GetInt32("Year").ToString();
                            maskedTextBoxLicensePlate.Text = reader.GetString("LicensePlate");

                            // Заполнение комбобокса с клиентами и установка выбранного клиента
                            string clientName = reader.GetString("ClientName");
                            //int clientId = reader.GetInt32("ClientID");

                            //if (!comboBoxClient.Items.Contains(clientName))
                            //{
                            //comboBoxClient.Items.Add(clientName);
                            //}
                            //SearchClients();
                            comboBoxClient.SelectedItem = clientName;

                            // Сохранение ID клиента, если это необходимо для дальнейших операций
                            //comboBoxClient.Tag = clientId;
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

        private void SearchClients()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query = @"SELECT CONCAT(LastName, ' ', FirstName) AS FullName FROM Clients";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string fullName = reader.GetString("FullName");
                            comboBoxClient.Items.Add(fullName);
                        }
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

        private int GetClientId()
        {
            string ClientName = comboBoxClient.SelectedItem.ToString();

            try
            {
                string query = "SELECT id FROM Clients WHERE CONCAT(LastName, ' ', FirstName) = @ClientName";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@ClientName", ClientName);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("Клиент не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return -1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return -1;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void maskedTextBoxLicensePlate_MaskInputRejected(object sender, KeyPressEventArgs e)
        {
            if (maskedTextBoxLicensePlate.SelectionStart == 0 ||
            maskedTextBoxLicensePlate.SelectionStart == 4 ||
            maskedTextBoxLicensePlate.SelectionStart == 5)
            {
                if (!char.IsLetter(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
            // Разрешить только цифры для остальных позиций
            else if (maskedTextBoxLicensePlate.SelectionStart == 1 ||
                     maskedTextBoxLicensePlate.SelectionStart == 2 ||
                     maskedTextBoxLicensePlate.SelectionStart == 3 ||
                     maskedTextBoxLicensePlate.SelectionStart == 6 ||
                     maskedTextBoxLicensePlate.SelectionStart == 7)
            {
                if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
                {
                    e.Handled = true;
                }
            }
        }

        private bool CheckInput()
        {
            if (textBoxBrand.Text.Length == 0)
            {
                MessageBox.Show("Неверно введена марка.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
                textBoxBrand.Focus();
                return false;
            }
            if (textBoxModel.Text.Length == 0)
            {
                MessageBox.Show("Неверно введена модель.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
                textBoxModel.Focus();
                return false;
            }
            if (Convert.ToInt32(textBoxYear.Text) < 1900 || Convert.ToInt32(textBoxYear.Text) > DateTime.Now.Year)
            {
                MessageBox.Show("Неверно введен год.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
                textBoxYear.Focus();
                return false;
            }
            if (comboBoxClient.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите клиента.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
                comboBoxClient.Focus();
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

                    // Запрос для добавления новой машины
                    string query = "INSERT INTO Cars (Brand, Model, Year, LicensePlate, ClientID) VALUES (@Brand, @Model, @Year, @LicensePlate, @ClientID)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    // Параметры запроса, связывание с текстовыми полями на форме
                    cmd.Parameters.AddWithValue("@Brand", textBoxBrand.Text);
                    cmd.Parameters.AddWithValue("@Model", textBoxModel.Text);
                    cmd.Parameters.AddWithValue("@Year", int.Parse(textBoxYear.Text));
                    cmd.Parameters.AddWithValue("@LicensePlate", maskedTextBoxLicensePlate.Text);
                    cmd.Parameters.AddWithValue("@ClientID", (GetClientId()));

                    // Выполнение запроса
                    int result = cmd.ExecuteNonQuery();

                    // Проверка результата выполнения запроса
                    if (result > 0)
                    {
                        MessageBox.Show("Машина добавлена.", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close(); // Закрытие формы после успешного добавления
                    }
                    else
                    {
                        MessageBox.Show("Машина не добавлена.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (MySql.Data.MySqlClient.MySqlException ex)
                {
                    MessageBox.Show("Такой запись уже существует", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void textBoxYear_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                e.Handled = true;
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                string query = "DELETE FROM Cars WHERE id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", ID);
                object result = cmd.ExecuteNonQuery();
                if (result != null)
                {
                    MessageBox.Show("Автомобиль удален.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Close();
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

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (CheckInput())
            {
                try
                {
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    string query = @"UPDATE Cars 
                                    SET Brand = @Brand, Model = @Model, Year = @Year, LicensePlate = @LicensePlate, ClientID = @ClientID 
                                    WHERE ID = @ID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);

                    cmd.Parameters.AddWithValue("@ID", ID); // carID должен быть идентификатором автомобиля, который нужно обновить
                    cmd.Parameters.AddWithValue("@Brand", textBoxBrand.Text);
                    cmd.Parameters.AddWithValue("@Model", textBoxModel.Text);
                    cmd.Parameters.AddWithValue("@Year", int.Parse(textBoxYear.Text));
                    cmd.Parameters.AddWithValue("@LicensePlate", maskedTextBoxLicensePlate.Text);
                    cmd.Parameters.AddWithValue("@ClientID", GetClientId()); // предполагается, что comboBoxClient содержит идентификаторы клиентов

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Автомобиль обновлен.", "Редактирование", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Автомобиль не обновлен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
