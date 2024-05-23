using System;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace CarService.Orders
{
    public partial class OrderDetailsForm : Form
    {
        private int orderId;
        private MySqlConnection connection;
        public OrderDetailsForm(int orderId)
        {
            InitializeComponent();
            this.orderId = orderId;
            connection = new DBConnection().GetConnectionString();

            LoadOrderDetails();
            CalculateTotalPrice();
        }

        private void LoadOrderDetails()
        {
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query = @"SELECT Services.ServiceName, OrderDetails.Quantity, OrderDetails.Price
                             FROM OrderDetails
                             INNER JOIN Services ON OrderDetails.ServiceID = Services.ID
                             WHERE OrderDetails.OrderID = @OrderID";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@OrderID", orderId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        dataGridViewOrderDetails.Rows.Add(reader.GetString("ServiceName"), reader.GetInt32("Quantity"), reader.GetDecimal("Price"));
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

        private void CalculateTotalPrice()
        {
            decimal totalPrice = 0;
            foreach (DataGridViewRow row in dataGridViewOrderDetails.Rows)
            {
                totalPrice += Convert.ToDecimal(row.Cells["Price"].Value);
            }
            labelTotalPrice.Text = $"Итоговая стоимость: {totalPrice:C2}";
        }

        private void buttonGeneratePdf_Click(object sender, EventArgs e)
        {
            GeneratePdf();
        }

        private (int clientId, int carId) GetClientAndCarIds(int orderId)
        {
            int clientId = -1;
            int carId = -1;

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query = "SELECT ClientID, CarID FROM Orders WHERE ID = @OrderId";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@OrderId", orderId);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        clientId = reader.GetInt32("ClientID");
                        carId = reader.GetInt32("CarID");
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

            return (clientId, carId);
        }

        private string GetClientName(int clientId)
        {
            string clientName = "";

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query = "SELECT CONCAT(FirstName, ' ', LastName) AS FullName FROM Clients WHERE ID = @ClientId";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@ClientId", clientId);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    clientName = result.ToString();
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

            return clientName;
        }

        private string GetCarInfo(int carId)
        {
            string carInfo = "";

            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();

                string query = "SELECT CONCAT(Brand, ' ', Model, ' ', LicensePlate) AS CarInfo FROM Cars WHERE ID = @CarId";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@CarId", carId);

                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    carInfo = result.ToString();
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

            return carInfo;
        }


        private void GeneratePdf()
        {
            (int clientId, int carId) = GetClientAndCarIds(orderId);
            string clientName = GetClientName(clientId);
            string carInfo = GetCarInfo(carId);


            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF файлы (*.pdf)|*.pdf",
                FileName = "Квитанция.pdf"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var doc = new iTextSharp.text.Document())
                {
                    PdfWriter.GetInstance(doc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                    doc.Open();

                    string arialFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                    BaseFont baseFont = BaseFont.CreateFont(arialFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    var titleFont = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);
                    var boldFont = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.BOLD);
                    var regularFont = new iTextSharp.text.Font(baseFont, 12);

                    doc.Add(new Paragraph("Автосервис \"AutoCare Center\"", titleFont));
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph($"Клиент: {clientName}", boldFont));
                    doc.Add(new Paragraph($"Автомобиль: {carInfo}", boldFont));
                    doc.Add(new Paragraph("\n"));

                    PdfPTable table = new PdfPTable(3);
                    table.AddCell(new PdfPCell(new Phrase("Услуга", boldFont)));
                    table.AddCell(new PdfPCell(new Phrase("Количество", boldFont)));
                    table.AddCell(new PdfPCell(new Phrase("Цена", boldFont)));

                    foreach (DataGridViewRow row in dataGridViewOrderDetails.Rows)
                    {                        table.AddCell(new PdfPCell(new Phrase(row.Cells["Service"].Value.ToString(), regularFont)));
                        table.AddCell(new PdfPCell(new Phrase(row.Cells["Quantity"].Value.ToString(), regularFont)));
                        table.AddCell(new PdfPCell(new Phrase(row.Cells["Price"].Value.ToString(), regularFont)));
                    }

                    doc.Add(table);

                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph(labelTotalPrice.Text, boldFont));
                    doc.Add(new Paragraph("\n\n"));
                    doc.Add(new Paragraph("Подпись клиента: ___________________________", regularFont));
                    doc.Add(new Paragraph("\n"));
                    doc.Add(new Paragraph("Подпись ген. директора: _______________________", regularFont));

                    doc.Close();
                }

                MessageBox.Show("PDF отчет успешно создан.", "Создание PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
