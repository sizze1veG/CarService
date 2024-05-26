using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.IO;


namespace CarService.Reports
{
    public partial class ReportsForm : Form
    {
        private MySqlConnection connection;
        public ReportsForm()
        {
            InitializeComponent();
            connection = new DBConnection().GetConnectionString();
            LoadReports();
        }
        private void LoadReports()
        {
            DataTable reportsTable = new DataTable();
            reportsTable.Columns.Add("НазваниеОтчета");
            reportsTable.Columns.Add("Описание");

            dataGridViewReports.Rows.Add("Список клиентов", "Список всех клиентов");
            dataGridViewReports.Rows.Add("Список автомобилей", "Список всех автомобилей");
            //dataGridViewReports.Rows.Add("Список сотрудников", "Список всех сотрудников");
            dataGridViewReports.Rows.Add("Список заказов", "Список всех заказов");
            dataGridViewReports.Rows.Add("Список услуг", "Список всех услуг");
            dataGridViewReports.Rows.Add("Заказы по статусу", "Список заказов, отфильтрованных по статусу");
            //dataGridViewReports.Rows.Add("Заказы по клиенту", "Список заказов, отфильтрованных по клиенту");
            //dataGridViewReports.Rows.Add("Заказы по автомобилю", "Список заказов, отфильтрованных по автомобилю");
            dataGridViewReports.Rows.Add("Заказы по дате", "Список заказов, отфильтрованных по дате");
            //dataGridViewReports.Rows.Add("Услуги по заказу", "Список услуг, отфильтрованных по заказу");
            //dataGridViewReports.Rows.Add("Доход по услугам за период", "Доход по услугам за указанный период");
            //dataGridViewReports.Rows.Add("Доход по сотрудникам за период", "Доход по сотрудникам за указанный период");
            dataGridViewReports.Rows.Add("Клиенты с максимальным числом заказов", "Клиенты с наибольшим количеством заказов");
            dataGridViewReports.Rows.Add("Самые популярные услуги", "Наиболее популярные услуги");
            //dataGridViewReports.Rows.Add("Доход по месяцам", "Доход по месяцам");

           //dataGridViewReports.DataSource = reportsTable;
        }

        private void dataGridViewReports_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex ==0)
            {
                switch (dataGridViewReports.Rows[e.RowIndex].Cells[0].Value.ToString())
                {
                    case "Список клиентов":
                        GenerateClientListReport();
                        break;
                    case "Список автомобилей":
                        GenerateCarListReport();
                        break;
                    //case "Список сотрудников":
                    //    GenerateEmployeeListReport();
                    //    break;
                    case "Список заказов":
                        GenerateOrderListReport();
                        break;
                    case "Список услуг":
                        GenerateServiceListReport();
                        break;
                    case "Заказы по статусу":
                        GenerateOrdersByStatusReport();
                        break;
                    //case "Заказы по клиенту":
                    //    GenerateOrdersByClientReport();
                    //    break;
                    //case "Заказы по автомобилю":
                    //    GenerateOrdersByCarReport();
                    //    break;
                    case "Заказы по дате":
                        GenerateOrdersByDateReport();
                        break;
                    //case "Услуги по заказу":
                    //    GenerateServicesByOrderReport();
                    //    break;
                    //case "Доход по услугам за период":
                    //    GenerateRevenueByServicesReport();
                    //    break;
                    //case "Доход по сотрудникам за период":
                    //    GenerateRevenueByEmployeesReport();
                    //    break;
                    case "Клиенты с максимальным числом заказов":
                        GenerateTopClientsReport();
                        break;
                    case "Самые популярные услуги":
                        GeneratePopularServicesReport();
                        break;
                    //case "Доход по месяцам":
                    //    GenerateMonthlyRevenueReport();
                    //    break;
                    default:
                        MessageBox.Show("Отчет не реализован.");
                        break;
                }
            }

            //if (e.ColumnIndex == 0 && e.RowIndex == 0)
        }

        private void buttonGenerateReport_Click(object sender, EventArgs e)
        {
            if (dataGridViewReports.SelectedRows.Count == 0)
            {
                MessageBox.Show("Пожалуйста, выберите отчет для генерации.");
                return;
            }
            string selectedReport = dataGridViewReports.SelectedCells[0].Value.ToString();
            //string selectedReport = dataGridViewReports.SelectedRows[0].Cells[0].Value.ToString();

            switch (selectedReport)
            {
                case "Список клиентов":
                    GenerateClientListReport();
                    break;
                case "Список автомобилей":
                    GenerateCarListReport();
                    break;
                //case "Список сотрудников":
                //    GenerateEmployeeListReport();
                //    break;
                case "Список заказов":
                    GenerateOrderListReport();
                    break;
                case "Список услуг":
                    GenerateServiceListReport();
                    break;
                case "Заказы по статусу":
                    GenerateOrdersByStatusReport();
                    break;
                //case "Заказы по клиенту":
                //    GenerateOrdersByClientReport();
                //    break;
                //case "Заказы по автомобилю":
                //    GenerateOrdersByCarReport();
                //    break;
                case "Заказы по дате":
                    GenerateOrdersByDateReport();
                    break;
                //case "Услуги по заказу":
                //    GenerateServicesByOrderReport();
                //    break;
                //case "Доход по услугам за период":
                //    GenerateRevenueByServicesReport();
                //    break;
                //case "Доход по сотрудникам за период":
                //    GenerateRevenueByEmployeesReport();
                //    break;
                case "Клиенты с максимальным числом заказов":
                    GenerateTopClientsReport();
                    break;
                case "Самые популярные услуги":
                    GeneratePopularServicesReport();
                    break;
                //case "Доход по месяцам":
                //    GenerateMonthlyRevenueReport();
                //    break;
                default:
                    MessageBox.Show("Отчет не реализован.");
                    break;
            }
        }

        private void GenerateClientListReport()
        {
            DataTable dataTable = new DataTable();
            using (MySqlCommand command = new MySqlCommand("SELECT * FROM Clients", connection))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            CreatePdfReport("Отчет_Список_Клиентов.pdf", "Список клиентов", dataTable);
        }

        private void GenerateCarListReport()
        {
            DataTable dataTable = new DataTable();
            using (MySqlCommand command = new MySqlCommand("SELECT * FROM Cars", connection))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            CreatePdfReport("Отчет_Список_Автомобилей.pdf", "Список автомобилей", dataTable);
        }

        //private void GenerateEmployeeListReport()
        //{
        //    DataTable dataTable = new DataTable();
        //    using (MySqlCommand command = new MySqlCommand("SELECT * FROM Employees", connection))
        //    {
        //        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
        //        adapter.Fill(dataTable);
        //    }
        //    CreatePdfReport("Отчет_Список_Сотрудников.pdf", "Список сотрудников", dataTable);
        //}

        private void GenerateOrderListReport()
        {
            DataTable dataTable = new DataTable();
            using (MySqlCommand command = new MySqlCommand("SELECT * FROM Orders", connection))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            CreatePdfReport("Отчет_Список_Заказов.pdf", "Список заказов", dataTable);
        }

        private void GenerateServiceListReport()
        {
            DataTable dataTable = new DataTable();
            using (MySqlCommand command = new MySqlCommand("SELECT * FROM Services", connection))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            CreatePdfReport("Отчет_Список_Услуг.pdf", "Список услуг", dataTable);
        }

        private void GenerateOrdersByStatusReport()
        {
            try
            {
                string status = comboBoxStatus.SelectedItem.ToString();

                DataTable dataTable = new DataTable();
                using (MySqlCommand command = new MySqlCommand("SELECT * FROM Orders WHERE Status = @Status", connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                    adapter.Fill(dataTable);
                }
                CreatePdfReport("Отчет_Заказы_По_Статусу.pdf", $"Заказы по статусу: {status}", dataTable);
            }
            catch (Exception)
            {
                MessageBox.Show($"Выберите статус", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        //private void GenerateOrdersByClientReport()
        //{
        //    // Assuming you have a ComboBox or other UI element to select the client
        //    int clientId = (int)comboBoxClient.SelectedValue;

        //    DataTable dataTable = new DataTable();
        //    using (MySqlCommand command = new MySqlCommand("SELECT * FROM Orders WHERE ClientID = @ClientID", connection))
        //    {
        //        command.Parameters.AddWithValue("@ClientID", clientId);
        //        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
        //        adapter.Fill(dataTable);
        //    }
        //    CreatePdfReport("Отчет_Заказы_По_Клиенту.pdf", $"Заказы по клиенту ID: {clientId}", dataTable);
        //}

        //private void GenerateOrdersByCarReport()
        //{
        //    // Assuming you have a ComboBox or other UI element to select the car
        //    int carId = (int)comboBoxCar.SelectedValue;

        //    DataTable dataTable = new DataTable();
        //    using (MySqlCommand command = new MySqlCommand("SELECT * FROM Orders WHERE CarID = @CarID", connection))
        //    {
        //        command.Parameters.AddWithValue("@CarID", carId);
        //        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
        //        adapter.Fill(dataTable);
        //    }
        //    CreatePdfReport("Отчет_Заказы_По_Автомобилю.pdf", $"Заказы по автомобилю ID: {carId}", dataTable);
        //}

        private void GenerateOrdersByDateReport()
        {
            // Assuming you have DateTimePickers or other UI elements to select the date range
            DateTime startDate = dateTimePickerStartDate.Value;
            DateTime endDate = dateTimePickerEndDate.Value;

            DataTable dataTable = new DataTable();
            using (MySqlCommand command = new MySqlCommand("SELECT * FROM Orders WHERE OrderDate BETWEEN @StartDate AND @EndDate", connection))
            {
                command.Parameters.AddWithValue("@StartDate", startDate);
                command.Parameters.AddWithValue("@EndDate", endDate);
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            CreatePdfReport("Отчет_Заказы_По_Дате.pdf", $"Заказы с {startDate:dd.MM.yyyy} по {endDate:dd.MM.yyyy}", dataTable);
        }

        //private void GenerateServicesByOrderReport()
        //{
        //    // Assuming you have a ComboBox or other UI element to select the order
        //    int orderId = (int)comboBoxOrder.SelectedValue;

        //    DataTable dataTable = new DataTable();
        //    using (MySqlCommand command = new MySqlCommand("SELECT * FROM OrderDetails WHERE OrderID = @OrderID", connection))
        //    {
        //        command.Parameters.AddWithValue("@OrderID", orderId);
        //        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
        //        adapter.Fill(dataTable);
        //    }
        //    CreatePdfReport("Отчет_Услуги_По_Заказу.pdf", $"Услуги по заказу ID: {orderId}", dataTable);
        //}

        //private void GenerateRevenueByServicesReport()
        //{
        //    // Assuming you have DateTimePickers or other UI elements to select the date range
        //    DateTime startDate = dateTimePickerStartDate.Value;
        //    DateTime endDate = dateTimePickerEndDate.Value;

        //    DataTable dataTable = new DataTable();
        //    using (MySqlCommand command = new MySqlCommand("SELECT ServiceName, SUM(Price * Quantity) AS TotalRevenue FROM OrderDetails INNER JOIN Services ON OrderDetails.ServiceID = Services.ID WHERE OrderDate BETWEEN @StartDate AND @EndDate GROUP BY ServiceName", connection))
        //    {
        //        command.Parameters.AddWithValue("@StartDate", startDate);
        //        command.Parameters.AddWithValue("@EndDate", endDate);
        //        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
        //        adapter.Fill(dataTable);
        //    }
        //    CreatePdfReport("Отчет_Доход_По_Услугам.pdf", $"Доход по услугам с {startDate:dd.MM.yyyy} по {endDate:dd.MM.yyyy}", dataTable);
        //}

        //private void GenerateRevenueByEmployeesReport()
        //{
        //    // Assuming you have DateTimePickers or other UI elements to select the date range
        //    DateTime startDate = dateTimePickerStartDate.Value;
        //    DateTime endDate = dateTimePickerEndDate.Value;

        //    DataTable dataTable = new DataTable();
        //    using (MySqlCommand command = new MySqlCommand("SELECT EmployeeName, SUM(Price * Quantity) AS TotalRevenue FROM OrderDetails INNER JOIN Employees ON OrderDetails.EmployeeID = Employees.ID WHERE OrderDate BETWEEN @StartDate AND @EndDate GROUP BY EmployeeName", connection))
        //    {
        //        command.Parameters.AddWithValue("@StartDate", startDate);
        //        command.Parameters.AddWithValue("@EndDate", endDate);
        //        MySqlDataAdapter adapter = new MySqlDataAdapter(command);
        //        adapter.Fill(dataTable);
        //    }
        //    CreatePdfReport("Отчет_Доход_По_Сотрудникам.pdf", $"Доход по сотрудникам с {startDate:dd.MM.yyyy} по {endDate:dd.MM.yyyy}", dataTable);
        //}

        private void GenerateTopClientsReport()
        {
            DataTable dataTable = new DataTable();
            using (MySqlCommand command = new MySqlCommand("SELECT ClientID, COUNT(*) AS OrderCount FROM Orders GROUP BY ClientID ORDER BY OrderCount DESC LIMIT 10", connection))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            CreatePdfReport("Отчет_Топ_Клиенты.pdf", "Клиенты с максимальным числом заказов", dataTable);
        }

        private void GeneratePopularServicesReport()
        {
            DataTable dataTable = new DataTable();
            using (MySqlCommand command = new MySqlCommand("SELECT ServiceID, COUNT(*) AS UsageCount FROM OrderDetails GROUP BY ServiceID ORDER BY UsageCount DESC LIMIT 10", connection))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            CreatePdfReport("Отчет_Популярные_Услуги.pdf", "Самые популярные услуги", dataTable);
        }

        private void GenerateMonthlyRevenueReport()
        {
            DataTable dataTable = new DataTable();
            using (MySqlCommand command = new MySqlCommand("SELECT DATE_FORMAT(OrderDate, '%Y-%m') AS Month, SUM(Price * Quantity) AS TotalRevenue FROM OrderDetails GROUP BY Month ORDER BY Month", connection))
            {
                MySqlDataAdapter adapter = new MySqlDataAdapter(command);
                adapter.Fill(dataTable);
            }
            CreatePdfReport("Отчет_Доход_По_Месяцам.pdf", "Доход по месяцам", dataTable);
        }

        private void CreatePdfReport(string fileName, string reportTitle, DataTable dataTable)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "PDF файлы (*.pdf)|*.pdf",
                FileName = fileName
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var doc = new iTextSharp.text.Document())
                {
                    iTextSharp.text.pdf.PdfWriter.GetInstance(doc, new FileStream(saveFileDialog.FileName, FileMode.Create));
                    doc.Open();

                    string arialFontPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Fonts), "arial.ttf");
                    BaseFont baseFont = BaseFont.CreateFont(arialFontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                    var titleFont = new iTextSharp.text.Font(baseFont, 16, iTextSharp.text.Font.BOLD);
                    var boldFont = new iTextSharp.text.Font(baseFont, 12, iTextSharp.text.Font.BOLD);
                    var regularFont = new iTextSharp.text.Font(baseFont, 12);

                    doc.Add(new iTextSharp.text.Paragraph(reportTitle, titleFont));
                    doc.Add(new iTextSharp.text.Paragraph("\n"));

                    PdfPTable table = new PdfPTable(dataTable.Columns.Count);
                    foreach (DataColumn column in dataTable.Columns)
                    {
                        table.AddCell(new PdfPCell(new Phrase(column.ColumnName, boldFont)));
                    }

                    foreach (DataRow row in dataTable.Rows)
                    {
                        foreach (var cell in row.ItemArray)
                        {
                            table.AddCell(new PdfPCell(new Phrase(cell.ToString(), regularFont)));
                        }
                    }

                    doc.Add(table);
                    doc.Close();
                }

                MessageBox.Show("PDF отчет успешно создан.", "Создание PDF", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}