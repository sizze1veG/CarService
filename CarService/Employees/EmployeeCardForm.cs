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
    }
}
