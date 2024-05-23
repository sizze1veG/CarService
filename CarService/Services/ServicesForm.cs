using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace CarService.Services
{
    public partial class ServicesForm : Form
    {
        private MySqlConnection connection;
        ServiceCardForm serviceCardForm;
        MainForm mainForm;
        public ServicesForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            connection = new DBConnection().GetConnectionString();
            UpdateTable();
        }

        private void UpdateTable()
        {
            dataGridViewServices.Rows.Clear();

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query = @"SELECT ID, ServiceName, Description, Price FROM Services";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    dataGridViewServices.Rows.Add(row.ItemArray[0], row.ItemArray[1], row.ItemArray[2], row.ItemArray[3]);
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
            serviceCardForm = new ServiceCardForm(true, false);
            serviceCardForm.FormClosed += ServiceCardForm_FormClosed;
            serviceCardForm.MdiParent = mainForm;
            serviceCardForm.Dock = DockStyle.Fill;
            serviceCardForm.Show();
        }

        private void ServiceCardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            serviceCardForm = null;
        }

        private void pictureBoxUpdate_Click(object sender, EventArgs e)
        {
            UpdateTable();
        }

        private void dataGridViewServices_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                try
                {
                    string id = dataGridViewServices.Rows[e.RowIndex].Cells[0].Value.ToString();
                    serviceCardForm = new ServiceCardForm(false, false, id);
                    serviceCardForm.FormClosed += ServiceCardForm_FormClosed;
                    serviceCardForm.MdiParent = mainForm;
                    serviceCardForm.Dock = DockStyle.Fill;
                    serviceCardForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string query = @"SELECT ID, ServiceName, Description, Price 
                    FROM Services 
                    WHERE CONVERT(ServiceName USING utf8mb4) LIKE @SearchText 
                    OR CONVERT(Description USING utf8mb4) LIKE @SearchText 
                    OR CONVERT(Price USING utf8mb4) LIKE @SearchText";
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@SearchText", "%" + textBoxSearch.Text + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridViewServices.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    dataGridViewServices.Rows.Add(row.ItemArray[0], row.ItemArray[1], row.ItemArray[2], row.ItemArray[3]);
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
