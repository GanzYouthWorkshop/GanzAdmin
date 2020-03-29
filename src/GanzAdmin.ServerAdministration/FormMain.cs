using GanzAdmin.ServerAdministration.Tasks;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GanzAdmin.ServerAdministration
{
    public partial class FormMain : Form
    {
        private TimeSpan m_UpTime;

        public FormMain()
        {
            InitializeComponent();

            this.flowLayoutPanel1.Controls.Add(new UpdateDnsTask("NsUpdate", new TimeSpan(1, 0, 0, 0), "3t2wAdMqEp"));
        }

        private void tmrTimer_Tick(object sender, EventArgs e)
        {
            this.m_UpTime += new TimeSpan(0, 0, 1);
            this.label2.Text = this.m_UpTime.ToString(@"dd\.hh\:mm\:ss");
            foreach(ucAdminTask task in this.flowLayoutPanel1.Controls)
            {
                if(task.CheckIn())
                {
                    task.Run();
                }
            }
        }
    }
}
