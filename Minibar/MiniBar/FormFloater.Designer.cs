namespace NewBizWiz.MiniBar
{
    partial class FormFloater
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
            this.buttonXSync = new DevComponents.DotNetBar.ButtonX();
            this.buttonXMinibar = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // buttonXSync
            // 
            this.buttonXSync.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXSync.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXSync.Image = global::NewBizWiz.MiniBar.Properties.Resources.Sync;
            this.buttonXSync.ImageFixedSize = new System.Drawing.Size(64, 48);
            this.buttonXSync.Location = new System.Drawing.Point(87, 3);
            this.buttonXSync.Name = "buttonXSync";
            this.buttonXSync.Size = new System.Drawing.Size(77, 55);
            this.buttonXSync.TabIndex = 1;
            this.buttonXSync.Click += new System.EventHandler(this.buttonXSync_Click);
            // 
            // buttonXMinibar
            // 
            this.buttonXMinibar.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXMinibar.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXMinibar.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonXMinibar.ImageFixedSize = new System.Drawing.Size(64, 48);
            this.buttonXMinibar.Location = new System.Drawing.Point(4, 3);
            this.buttonXMinibar.Name = "buttonXMinibar";
            this.buttonXMinibar.Size = new System.Drawing.Size(77, 55);
            this.buttonXMinibar.TabIndex = 2;
            this.buttonXMinibar.Text = "Minibar";
            this.buttonXMinibar.TextColor = System.Drawing.Color.Black;
            this.buttonXMinibar.Click += new System.EventHandler(this.buttonXMinibar_Click);
            // 
            // FormFloater
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(169, 61);
            this.Controls.Add(this.buttonXMinibar);
            this.Controls.Add(this.buttonXSync);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormFloater";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "adSALESapps";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormFloater_FormClosed);
            this.Shown += new System.EventHandler(this.FormFloater_Shown);
            this.LocationChanged += new System.EventHandler(this.FormFloater_LocationChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX buttonXSync;
        private DevComponents.DotNetBar.ButtonX buttonXMinibar;
    }
}