namespace UniHRM_Toolbox
{
    partial class MainForm
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
            this.menu = new System.Windows.Forms.MenuStrip();
            this.uniHRMToolboxMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.txtDisplay = new System.Windows.Forms.TextBox();
            this.catalogueMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.newCatalogueMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu.SuspendLayout();
            this.SuspendLayout();
            // 
            // menu
            // 
            this.menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.uniHRMToolboxMenu,
            this.catalogueMenu});
            this.menu.Location = new System.Drawing.Point(0, 0);
            this.menu.Name = "menu";
            this.menu.Size = new System.Drawing.Size(725, 24);
            this.menu.TabIndex = 0;
            this.menu.Text = "menu";
            // 
            // uniHRMToolboxMenu
            // 
            this.uniHRMToolboxMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItemExit});
            this.uniHRMToolboxMenu.Name = "uniHRMToolboxMenu";
            this.uniHRMToolboxMenu.Size = new System.Drawing.Size(97, 20);
            this.uniHRMToolboxMenu.Text = "UniHRM Toolbox";
            // 
            // menuItemExit
            // 
            this.menuItemExit.Name = "menuItemExit";
            this.menuItemExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.menuItemExit.Size = new System.Drawing.Size(132, 22);
            this.menuItemExit.Text = "Exit";
            this.menuItemExit.Click += new System.EventHandler(this.menuItemExit_Click);
            // 
            // txtDisplay
            // 
            this.txtDisplay.Location = new System.Drawing.Point(12, 27);
            this.txtDisplay.Multiline = true;
            this.txtDisplay.Name = "txtDisplay";
            this.txtDisplay.Size = new System.Drawing.Size(701, 294);
            this.txtDisplay.TabIndex = 1;
            // 
            // catalogueMenu
            // 
            this.catalogueMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCatalogueMenuItem});
            this.catalogueMenu.Name = "catalogueMenu";
            this.catalogueMenu.Size = new System.Drawing.Size(68, 20);
            this.catalogueMenu.Text = "Catalogue";
            // 
            // newCatalogueMenuItem
            // 
            this.newCatalogueMenuItem.Name = "newCatalogueMenuItem";
            this.newCatalogueMenuItem.Size = new System.Drawing.Size(152, 22);
            this.newCatalogueMenuItem.Text = "New Catalogue";
            this.newCatalogueMenuItem.Click += new System.EventHandler(this.newCatalogueMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 333);
            this.Controls.Add(this.txtDisplay);
            this.Controls.Add(this.menu);
            this.MainMenuStrip = this.menu;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UniHRM Toolbox";
            this.menu.ResumeLayout(false);
            this.menu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menu;
        private System.Windows.Forms.ToolStripMenuItem uniHRMToolboxMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItemExit;
        private System.Windows.Forms.TextBox txtDisplay;
        private System.Windows.Forms.ToolStripMenuItem catalogueMenu;
        private System.Windows.Forms.ToolStripMenuItem newCatalogueMenuItem;
    }
}

