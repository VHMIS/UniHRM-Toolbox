using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniHRM_Toolbox
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void menuItemExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void newCatalogueMenuItem_Click(object sender, EventArgs e)
        {
            using (CatalogueForm frmCatalogue = new CatalogueForm())
            {
                frmCatalogue.onCompleted += frmCatalogue_onComplete;
                frmCatalogue.ShowDialog();
            }
        }

        private void frmCatalogue_onComplete(string text)
        {
            this.txtDisplay.Enabled = true;
            this.txtDisplay.Text = text;
        }

        private void menuItemSetting_Click(object sender, EventArgs e)
        {
            using (SettingForm frmSetting = new SettingForm())
            {
                frmSetting.ShowDialog();
            }
        }
    }
}
