﻿using System;
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
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();

            txtSpPre.Text = Properties.Settings.Default.sp_pre;
            txtDtPre.Text = Properties.Settings.Default.dt_pre;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.sp_pre = txtSpPre.Text;
            Properties.Settings.Default.dt_pre = txtDtPre.Text;
            Properties.Settings.Default.Save();
        }
    }
}
