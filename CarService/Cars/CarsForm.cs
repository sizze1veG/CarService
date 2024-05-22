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

namespace CarService.Cars
{
    public partial class CarsForm : Form
    {
        private MySqlConnection connection;
        CarsCardForm carsCardForm;
        ClientCardForm clientCardForm;
        MainForm mainForm;
        public CarsForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            connection = new DBConnection().GetConnectionString();
            UpdateTable();
        }

        private void UpdateTable()
        {
            dataGridViewCars.Rows.Clear();

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                string query = @"SELECT cars.id, cars.brand, cars.model, cars.year, cars.licenseplate, CONCAT(Clients.FirstName, ' ', Clients.LastName) AS ClientName FROM cars
                    LEFT JOIN clients ON cars.clientid = clients.id";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    dataGridViewCars.Rows.Add(row.ItemArray[0], row.ItemArray[1], row.ItemArray[2], row.ItemArray[3], row.ItemArray[4], row.ItemArray[5]);
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
            carsCardForm = new CarsCardForm(true, false);
            carsCardForm.FormClosed += CarsCardForm_FormClosed;
            carsCardForm.MdiParent = mainForm;
            carsCardForm.Dock = DockStyle.Fill;
            carsCardForm.Show();
        }

        private void CarsCardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            carsCardForm = null;
        }

        private void pictureBoxUpdate_Click(object sender, EventArgs e)
        {
            UpdateTable();
        }

        private void dataGridViewCars_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                try
                {
                    string id = dataGridViewCars.Rows[e.RowIndex].Cells[0].Value.ToString();
                    carsCardForm = new CarsCardForm(false, false, id);
                    carsCardForm.FormClosed += CarsCardForm_FormClosed;
                    carsCardForm.MdiParent = mainForm;
                    carsCardForm.Dock = DockStyle.Fill;
                    carsCardForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            if (e.ColumnIndex == 5 && e.RowIndex >= 0)
            {
                try
                {
                    string CarId = dataGridViewCars.Rows[e.RowIndex].Cells[0].Value.ToString();
                    if (connection.State == ConnectionState.Closed)
                        connection.Open();
                    string clientIdQuery = "SELECT ClientID FROM Cars WHERE id = @CarId";
                    MySqlCommand clientIdCmd = new MySqlCommand(clientIdQuery, connection);
                    clientIdCmd.Parameters.AddWithValue("@CarId", CarId);
                    object clientIdResult = clientIdCmd.ExecuteScalar();

                    if (clientIdResult != null)
                    {
                        string clientId = clientIdResult.ToString();
                        clientCardForm = new ClientCardForm(false, false, clientId);
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
        }

        private void ClientCardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            clientCardForm = null;
        }

        private void textBoxSearch_TextChanged_1(object sender, EventArgs e)
        {
            string query = @"SELECT Cars.ID, Cars.Brand, Cars.Model, Cars.Year, Cars.LicensePlate, CONCAT(Clients.FirstName, ' ', Clients.LastName) AS ClientName
                            FROM Cars INNER JOIN Clients ON Cars.ClientID = Clients.ID
                            WHERE Cars.Brand LIKE @SearchText 
                            OR Cars.Model LIKE @SearchText 
                            OR Cars.Year LIKE @SearchText
                            OR Cars.LicensePlate LIKE @SearchText 
                            OR Clients.FirstName LIKE @SearchText 
                            OR Clients.LastName LIKE @SearchText";

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@SearchText", "%" + textBoxSearch.Text + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridViewCars.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    dataGridViewCars.Rows.Add(row.ItemArray[0], row.ItemArray[1], row.ItemArray[2], row.ItemArray[3], row.ItemArray[4], row.ItemArray[5]);
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
