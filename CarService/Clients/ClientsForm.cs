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

namespace CarService.Clients
{
    public partial class ClientsForm : Form
    {
        private MySqlConnection connection;
        ClientCardForm clientCardForm;
        MainForm mainForm;
        public ClientsForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            connection = new DBConnection().GetConnectionString();
            UpdateTable();
        }

        private void UpdateTable()
        {
            dataGridViewClients.Rows.Clear();

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                string query = "SELECT * FROM clients";
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    dataGridViewClients.Rows.Add(row.ItemArray[0], row.ItemArray[1], row.ItemArray[2], row.ItemArray[3]);
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
                //comboBoxSearch.SelectedIndex = -1;
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            //Hide();
            clientCardForm = new ClientCardForm(true, false);
            clientCardForm.FormClosed += ClientCardForm_FormClosed;
            clientCardForm.MdiParent = mainForm;
            clientCardForm.Dock = DockStyle.Fill;
            clientCardForm.Show();
        }

        private void ClientCardForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            clientCardForm = null;
        }

        private void pictureBoxUpdate_Click(object sender, EventArgs e)
        {
            UpdateTable();
        }

        private void dataGridViewClients_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 0 && e.RowIndex >= 0)
            {
                try
                {
                    string id = dataGridViewClients.Rows[e.RowIndex].Cells[0].Value.ToString();
                    clientCardForm = new ClientCardForm(false, false, id);
                    clientCardForm.FormClosed += ClientCardForm_FormClosed;
                    clientCardForm.MdiParent = mainForm;
                    clientCardForm.Dock = DockStyle.Fill;
                    clientCardForm.Show();
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

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            string query = @"
            SELECT ID, FirstName, LastName, Phone 
            FROM Clients 
            WHERE FirstName LIKE @SearchText OR LastName LIKE @SearchText OR Phone LIKE @SearchText";

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@SearchText", "%" + textBoxSearch.Text + "%");

                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                dataGridViewClients.Rows.Clear();
                foreach (DataRow row in dataTable.Rows)
                {
                    dataGridViewClients.Rows.Add(row.ItemArray[0], row.ItemArray[1], row.ItemArray[2], row.ItemArray[3]);
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
