using CarService.Cars;
using CarService.Clients;
using CarService.Orders;
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

namespace CarService.Employees
{
    public partial class EmployeesForm : Form
    {
        private MySqlConnection connection;
        EmployeeCardForm employeeCardForm;
        MainForm mainForm;
        public EmployeesForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            connection = new DBConnection().GetConnectionString();
            UpdateTable();
        }

        private void UpdateTable()
        {
            dataGridViewEmployees.Rows.Clear();

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                string query = @"SELECT Employees.ID, Employees.FirstName, Employees.LastName, Employees.Position, 
                        Employees.Username FROM Employees";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    dataGridViewEmployees.Rows.Add(row.ItemArray[0], row.ItemArray[1], row.ItemArray[2], row.ItemArray[3], row.ItemArray[4]);
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
            employeeCardForm = new EmployeeCardForm(true, false);
            employeeCardForm.FormClosed += EmployeeCardForm_FormClosed;
            employeeCardForm.MdiParent = mainForm;
            employeeCardForm.Dock = DockStyle.Fill;
            employeeCardForm.Show();
        }

        private void EmployeeCardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            employeeCardForm = null;
        }

        private void pictureBoxUpdate_Click(object sender, EventArgs e)
        {
            UpdateTable();
        }

        private void dataGridViewEmployees_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                try
                {
                    string id = dataGridViewEmployees.Rows[e.RowIndex].Cells[0].Value.ToString();
                    employeeCardForm = new EmployeeCardForm(false, false, id);
                    employeeCardForm.FormClosed += EmployeeCardForm_FormClosed;
                    employeeCardForm.MdiParent = mainForm;
                    employeeCardForm.Dock = DockStyle.Fill;
                    employeeCardForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string query = @"SELECT Employees.ID, Employees.FirstName, Employees.LastName, Employees.Position, 
                    Employees.Username
                    FROM Employees
                    WHERE CONVERT(Employees.FirstName USING utf8mb4) LIKE @SearchText 
                    OR CONVERT(Employees.LastName USING utf8mb4) LIKE @SearchText 
                    OR CONVERT(Employees.Position USING utf8mb4) LIKE @SearchText 
                    OR CONVERT(Employees.Username USING utf8mb4) LIKE @SearchText 
                    OR CONVERT(CONCAT(Employees.FirstName, ' ', Employees.LastName) USING utf8mb4) LIKE @SearchText";
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@SearchText", "%" + textBoxSearch.Text + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridViewEmployees.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    dataGridViewEmployees.Rows.Add(row.ItemArray[0], row.ItemArray[1], row.ItemArray[2], row.ItemArray[3], row.ItemArray[4]);
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
