using CarService.Cars;
using CarService.Clients;
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
    public partial class OrderForm : Form
    {
        private MySqlConnection connection;
        CarsCardForm carsCardForm;
        ClientCardForm clientCardForm;
        OrderCardForm orderCardForm;
        MainForm mainForm;
        public OrderForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            connection = new DBConnection().GetConnectionString();
            UpdateTable();
        }

        private void UpdateTable()
        {
            dataGridViewOrders.Rows.Clear();

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                string query = @"SELECT Orders.ID, Orders.OrderDate, Orders.Status, 
                                CONCAT(Clients.FirstName, ' ', Clients.LastName) AS ClientName, 
                                Cars.LicensePlate FROM Orders
                                LEFT JOIN Clients ON Orders.ClientID = Clients.ID
                                LEFT JOIN Cars ON Orders.CarID = Cars.ID";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    dataGridViewOrders.Rows.Add(row.ItemArray[0], ((DateTime)row.ItemArray[1]).ToShortDateString(), row.ItemArray[2], row.ItemArray[3], row.ItemArray[4], "Детали");
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

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            orderCardForm = new OrderCardForm(true, false);
            orderCardForm.FormClosed += OrderCardForm_FormClosed;
            orderCardForm.MdiParent = mainForm;
            orderCardForm.Dock = DockStyle.Fill;
            orderCardForm.Show();
        }

        private void OrderCardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            orderCardForm = null;
        }

        private void pictureBoxUpdate_Click(object sender, EventArgs e)
        {
            UpdateTable();
        }

        private void dataGridViewOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                try
                {
                    string id = dataGridViewOrders.Rows[e.RowIndex].Cells[0].Value.ToString();
                    orderCardForm = new OrderCardForm(false, false, id);
                    orderCardForm.FormClosed += OrderCardForm_FormClosed;
                    orderCardForm.MdiParent = mainForm;
                    orderCardForm.Dock = DockStyle.Fill;
                    orderCardForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (e.ColumnIndex == 3 && e.RowIndex >= 0)
            {
                try
                {
                    string orderId = dataGridViewOrders.Rows[e.RowIndex].Cells[0].Value.ToString();
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    string clientIdQuery = "SELECT ClientID FROM Orders WHERE id = @orderId";
                    MySqlCommand clientIdCmd = new MySqlCommand(clientIdQuery, connection);
                    clientIdCmd.Parameters.AddWithValue("@orderId", orderId);
                    object clientIdResult = clientIdCmd.ExecuteScalar();

                    if (clientIdResult != null)
                    {
                        string clientId = clientIdResult.ToString();
                        clientCardForm = new ClientCardForm(false, true, clientId);
                        clientCardForm.FormClosed += ClientCardForm_FormClosed;
                        clientCardForm.MdiParent = mainForm;
                        clientCardForm.Dock = DockStyle.Fill;
                        clientCardForm.Show();
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
            if (e.ColumnIndex == 4 && e.RowIndex >= 0)
            {
                try
                {
                    string orderId = dataGridViewOrders.Rows[e.RowIndex].Cells[0].Value.ToString();
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    string carIdQuery = "SELECT CarID FROM Orders WHERE id = @orderId";
                    MySqlCommand carIdCmd = new MySqlCommand(carIdQuery, connection);
                    carIdCmd.Parameters.AddWithValue("@orderId", orderId);
                    object carIdResult = carIdCmd.ExecuteScalar();

                    if (carIdResult != null)
                    {
                        string carId = carIdResult.ToString();
                        carsCardForm = new CarsCardForm(false, true, carId);
                        carsCardForm.FormClosed += ClientCardForm_FormClosed;
                        carsCardForm.MdiParent = mainForm;
                        carsCardForm.Dock = DockStyle.Fill;
                        carsCardForm.Show();
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
            if (e.ColumnIndex == 5 && e.RowIndex >= 0)
            {
                try
                {
                    string orderId = dataGridViewOrders.Rows[e.RowIndex].Cells[0].Value.ToString();
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    OrderDetailsForm detailsForm = new OrderDetailsForm(Convert.ToInt32(orderId));
                    detailsForm.ShowDialog();
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

        private void ClientCardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            clientCardForm = null;
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string query = @"SELECT Orders.ID, Orders.OrderDate, Orders.Status, 
                            CONCAT(Clients.FirstName, ' ', Clients.LastName) AS ClientName, 
                            Cars.LicensePlate FROM Orders 
                            LEFT JOIN Clients ON Orders.ClientID = Clients.ID
                            LEFT JOIN Cars ON Orders.CarID = Cars.ID
                            WHERE CONVERT(Orders.OrderDate USING utf8mb4) LIKE @SearchText 
                            OR CONVERT(Orders.Status USING utf8mb4) LIKE @SearchText 
                            OR CONVERT(Cars.LicensePlate USING utf8mb4) LIKE @SearchText 
                            OR CONVERT(Clients.FirstName USING utf8mb4) LIKE @SearchText 
                            OR CONVERT(Clients.LastName USING utf8mb4) LIKE @SearchText 
                            OR CONVERT(CONCAT(Clients.FirstName, ' ', Clients.LastName) USING utf8mb4) LIKE @SearchText";
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@SearchText", "%" + textBoxSearch.Text + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridViewOrders.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    dataGridViewOrders.Rows.Add(row.ItemArray[0], ((DateTime)row.ItemArray[1]).ToShortDateString(), row.ItemArray[2], row.ItemArray[3], row.ItemArray[4], "Детали");
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

