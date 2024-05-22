using CarService.Cars;
using CarService.Clients;
using CarService.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarService
{
    public partial class MainForm : Form
    {
        ClientsForm clientsForm;
        CarsForm carsForm;
        OrderForm orderForm;
        public MainForm()
        {
            InitializeComponent();
            WindowState = FormWindowState.Maximized;
            nightControlBox1.Visible = false;
            mdiProp();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckRole();           
        }

        private void mdiProp()
        {
            this.SetBevel(false);
            Controls.OfType<MdiClient>().FirstOrDefault().BackColor = Color.FromArgb(232, 234, 237);
        }
        private void CheckRole()
        {
            int roleID = PersonalData.RoleID;
            switch (roleID)
            {
                case 2:
                    panelServices.Dispose();
                    panelReporst.Dispose();
                    panelEmployees.Dispose();
                    panelAboutAProgramm.Dispose();
                    break;
                case 3:
                    panelServices.Dispose();
                    panelClients.Dispose();
                    panelCars.Dispose();
                    panelReporst.Dispose();
                    panelEmployees.Dispose();
                    panelAboutAProgramm.Dispose();
                    break;
                case 4:
                    panelServices.Dispose();
                    panelCars.Dispose();
                    panelEmployees.Dispose();
                    panelAboutAProgramm.Dispose();
                    break;
                default:
                    break;
            }
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Хотите сменить аккаунт?","Выход", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                new LogInForm().Show();
                Hide();
            }
            else if (result == DialogResult.No)
            {
                Application.Exit();
            }
        }

        private bool sidebarExpand = true;

        private void timerSideBar_Tick(object sender, EventArgs e)
        {
            if (sidebarExpand)
            {
                flowLayoutPanelSideBar.Width -= 50;
                if (flowLayoutPanelSideBar.Width <= 67)
                {
                    sidebarExpand = false;
                    timerSideBar.Stop();
                    panelAboutAProgramm.Width = Width;
                    panelAccount.Width = Width;
                    panelCars.Width = Width;
                    panelClients.Width = Width;
                    panelEmployees.Width = Width;
                    panelExit.Width = Width;
                    panelOrders.Width = Width;
                    panelServices.Width = Width;
                }
            }
            else
            {
                flowLayoutPanelSideBar.Width += 50;
                if (flowLayoutPanelSideBar.Width >= 264)
                {
                    sidebarExpand = true;
                    timerSideBar.Stop();
                    panelAboutAProgramm.Width = Width;
                    panelAccount.Width = Width;
                    panelCars.Width = Width;
                    panelClients.Width = Width;
                    panelEmployees.Width = Width;
                    panelExit.Width = Width;
                    panelOrders.Width = Width;
                    panelServices.Width = Width;
                }
            }
        }

        private void pictureBoxSideBar_Click(object sender, EventArgs e)
        {
            timerSideBar.Start();
        }

        private void buttonClients_Click(object sender, EventArgs e)
        {
            if (clientsForm == null)
            {
                clientsForm = new ClientsForm(this);
                clientsForm.FormClosed += ClientsForm_FormClosed;
                clientsForm.MdiParent = this;
                clientsForm.Dock = DockStyle.Fill;
                clientsForm.Show();
            }
            else
            {
                clientsForm.Activate();
            }
        }

        private void ClientsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            clientsForm = null;
        }

        private void buttonCars_Click(object sender, EventArgs e)
        {
            if (carsForm == null)
            {
                carsForm = new CarsForm(this);
                carsForm.FormClosed += CarsForm_FormClosed;
                carsForm.MdiParent = this;
                carsForm.Dock = DockStyle.Fill;
                carsForm.Show();
            }
            else
            {
                carsForm.Activate();
            }
        }

        private void CarsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            carsForm = null;
        }

        private void buttonOrders_Click(object sender, EventArgs e)
        {
            if (orderForm == null)
            {
                orderForm = new OrderForm(this);
                orderForm.FormClosed += OrderForm_FormClosed;
                orderForm.MdiParent = this;
                orderForm.Dock = DockStyle.Fill;
                orderForm.Show();
            }
            else
            {
                orderForm.Activate();
            }
        }

        private void OrderForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            orderForm = null;
        }
    }
}
