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

        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
