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
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            CheckRole();
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
                flowLayoutPanelSideBar.Width -= 10;
                if (flowLayoutPanelSideBar.Width <= 67)
                {
                    sidebarExpand = false;
                    timerSideBar.Stop();
                }
            }
            else
            {
                flowLayoutPanelSideBar.Width += 10;
                if (flowLayoutPanelSideBar.Width >= 264)
                {
                    sidebarExpand = true;
                    timerSideBar.Stop();
                }
            }
        }

        private void pictureBoxSideBar_Click(object sender, EventArgs e)
        {
            timerSideBar.Start();
        }
    }
}
