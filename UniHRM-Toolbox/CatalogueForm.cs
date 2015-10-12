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
    public partial class CatalogueForm : Form
    {
        public CatalogueForm()
        {
            InitializeComponent();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            ToolLib.Catalogue.build();
        }
    }
}
