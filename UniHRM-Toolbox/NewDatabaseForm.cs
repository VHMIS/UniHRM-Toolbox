using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ToolLib;

namespace UniHRM_Toolbox
{
    public delegate void eventOnComplete(string text);

    public partial class NewDatabaseForm : Form
    {
        public event eventOnComplete onCompleted;

        public NewDatabaseForm()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            ToolLib.Database.dt_pre = cbDt.Checked ? Properties.Settings.Default.dt_pre : "";
            ToolLib.Database.sp_pre = Properties.Settings.Default.sp_pre;

            string text = ToolLib.Database.fromString(txtDescription.Text);

            if (this.onCompleted != null)
            {
                this.onCompleted(text);
            }
        }
    }
}
