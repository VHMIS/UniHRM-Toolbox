namespace UniHRM_Toolbox
{
    partial class SettingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblSpPre = new System.Windows.Forms.Label();
            this.txtSpPre = new System.Windows.Forms.TextBox();
            this.btnUpdate = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblSpPre
            // 
            this.lblSpPre.AutoSize = true;
            this.lblSpPre.Location = new System.Drawing.Point(12, 9);
            this.lblSpPre.Name = "lblSpPre";
            this.lblSpPre.Size = new System.Drawing.Size(119, 13);
            this.lblSpPre.TabIndex = 0;
            this.lblSpPre.Text = "Stored Procedure Prefix";
            // 
            // txtSpPre
            // 
            this.txtSpPre.Location = new System.Drawing.Point(13, 26);
            this.txtSpPre.Name = "txtSpPre";
            this.txtSpPre.Size = new System.Drawing.Size(285, 20);
            this.txtSpPre.TabIndex = 1;
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(12, 297);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(75, 23);
            this.btnUpdate.TabIndex = 2;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.UseVisualStyleBackColor = true;
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 332);
            this.Controls.Add(this.btnUpdate);
            this.Controls.Add(this.txtSpPre);
            this.Controls.Add(this.lblSpPre);
            this.Name = "SettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setting";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblSpPre;
        private System.Windows.Forms.TextBox txtSpPre;
        private System.Windows.Forms.Button btnUpdate;
    }
}