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

namespace CarService.Orders
{
    public partial class OrderCardForm : Form
    {
        private MySqlConnection connection;
        private int ID = -1;
        public OrderCardForm(bool isNew, bool isReadOnly, string id = "-1")
        {
            InitializeComponent();
            connection = new DBConnection().GetConnectionString();

            if (isReadOnly)
            {
                dateTimePickerOrderDate.Enabled = false;
                comboBoxStatus.Enabled = false;
                comboBoxCar.Enabled = false;
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
                SearchCars();
            }
            else
            {
                ID = Convert.ToInt32(id);
                buttonAdd.Visible = false;
                buttonAdd.Enabled = false;
                SearchClients();
                SearchCars();
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

                    //// Заполнение комбобокса с номерами автомобилей
                    //string queryCars = @"SELECT ID, LicensePlate FROM Cars";
                    //MySqlCommand commandCars = new MySqlCommand(queryCars, connection);
                    //using (MySqlDataReader readerCars = commandCars.ExecuteReader())
                    //{
                    //    while (readerCars.Read())
                    //    {
                    //        comboBoxCar.Items.Add(readerCars.GetString("LicensePlate"));
                    //    }
                    //}

                    //// Заполнение комбобокса с именами клиентов
                    //string queryClients = @"SELECT ID, CONCAT(FirstName, ' ', LastName) AS ClientName FROM Clients";
                    //MySqlCommand commandClients = new MySqlCommand(queryClients, connection);
                    //using (MySqlDataReader readerClients = commandClients.ExecuteReader())
                    //{
                    //    while (readerClients.Read())
                    //    {
                    //        comboBoxClient.Items.Add(new { ID = readerClients.GetInt32("ID"), ClientName = readerClients.GetString("ClientName") });
                    //    }
                    //}

                    // Заполнение формы данными заказа
                    string queryOrder = @"SELECT Orders.ID, Orders.OrderDate, Orders.Status, Orders.ClientID, Orders.CarID, 
                        CONCAT(Clients.LastName, ' ', Clients.FirstName) AS ClientName, Cars.LicensePlate 
                        FROM Orders 
                        INNER JOIN Clients ON Orders.ClientID = Clients.ID 
                        INNER JOIN Cars ON Orders.CarID = Cars.ID 
                        WHERE Orders.ID = @OrderID LIMIT 1";
                    MySqlCommand commandOrder = new MySqlCommand(queryOrder, connection);
                    commandOrder.Parameters.AddWithValue("@OrderID", ID);

                    using (MySqlDataReader readerOrder = commandOrder.ExecuteReader())
                    {
                        if (readerOrder.Read())
                        {
                            dateTimePickerOrderDate.Value = readerOrder.GetDateTime("OrderDate");
                            comboBoxStatus.SelectedItem = readerOrder.GetString("Status");
                            string clientName = readerOrder.GetString("ClientName");
                            comboBoxClient.SelectedItem = clientName;
                            string licensePlate = readerOrder.GetString("LicensePlate");
                            comboBoxCar.SelectedItem = licensePlate;

                            //int clientID = readerOrder.GetInt32("ClientID");
                            //int carID = readerOrder.GetInt32("CarID");

                            //// Выбор нужного клиента в комбобоксе
                            //foreach (var item in comboBoxClient.Items)
                            //{
                            //    if (((dynamic)item).ID == clientID)
                            //    {
                            //        comboBoxClient.SelectedItem = item;
                            //        break;
                            //    }
                            //}

                            //// Выбор нужного автомобиля в комбобоксе
                            //foreach (var item in comboBoxCar.Items)
                            //{
                            //    if (((dynamic)item).ID == carID)
                            //    {
                            //        comboBoxCar.SelectedItem = item;
                            //        break;
                            //    }
                            //}
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

        private void SearchCars()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query = @"SELECT LicensePlate FROM Cars";

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string licensePlate = reader.GetString("LicensePlate");
                            comboBoxCar.Items.Add(licensePlate);
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

        private int GetCarId()
        {
            string carPlate = comboBoxCar.SelectedItem.ToString();

            try
            {
                string query = "SELECT id FROM Cars WHERE LicensePlate = @carPlate";
                using (MySqlCommand cmd = new MySqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@carPlate", carPlate);
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    object result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        MessageBox.Show("Авто не найдено.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private bool CheckInput()
        {
            if (dateTimePickerOrderDate.Value > DateTime.Now)
            {
                MessageBox.Show("Неверно выбрана дата.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
                dateTimePickerOrderDate.Focus();
                return false;
            }
            if (comboBoxStatus.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите статус.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
                comboBoxClient.Focus();
                return false;
            }
            if (comboBoxCar.SelectedIndex == -1)
            {
                MessageBox.Show("Выберите авто.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                connection.Close();
                comboBoxCar.Focus();
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

                    string query = @"INSERT INTO Orders (OrderDate, Status, ClientID, CarID) VALUES (@OrderDate, @Status, @ClientID, @CarID)";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@OrderDate", dateTimePickerOrderDate.Value);
                    cmd.Parameters.AddWithValue("@Status", comboBoxStatus.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ClientID", GetClientId());
                    cmd.Parameters.AddWithValue("@CarID", GetCarId());

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Заказ добавлен.", "Добавление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Заказ не добавлен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                string query = "DELETE FROM Orders WHERE id = @Id";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Id", ID);
                object result = cmd.ExecuteNonQuery();
                if (result != null)
                {
                    MessageBox.Show("Заказ удален.", "Удаление", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                    string query = @"UPDATE Orders SET OrderDate = @OrderDate, Status = @Status, ClientID = @ClientID, CarID = @CarID WHERE ID = @OrderID";
                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.Parameters.AddWithValue("@OrderID", ID);
                    cmd.Parameters.AddWithValue("@OrderDate", dateTimePickerOrderDate.Value);
                    cmd.Parameters.AddWithValue("@Status", comboBoxStatus.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@ClientID", GetClientId());
                    cmd.Parameters.AddWithValue("@CarID", GetCarId());

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        MessageBox.Show("Заказ обновлен.", "Обновление", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Заказ не обновлен.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
