using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GanzAdmin.ServerAdministration
{
    public abstract partial class ucAdminTask : UserControl
    {
        protected DateTime LastRun;
        protected TimeSpan RunInterval;
        protected DateTime NextRun;

        public ucAdminTask()
        {
            InitializeComponent();
        }

        public bool CheckIn()
        {
            this.lblTimeLeft.Text = (this.NextRun - DateTime.Now).ToString(@"hh\:mm\:ss");
            return this.NextRun < DateTime.Now;
        }

        public void Run()
        {
            this.RunInternal();
            this.LastRun = DateTime.Now;
            this.lblLastRun.Text = this.LastRun.ToString("yyyy.MM.dd. hh:mm:ss");
            this.NextRun = this.LastRun + this.RunInterval;
        }

        protected abstract void RunInternal();

        private void btnRunNow_Click(object sender, EventArgs e)
        {
            this.NextRun = DateTime.Now;
        }
    }
}
