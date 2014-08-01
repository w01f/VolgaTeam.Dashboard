namespace NewBizWiz.CommonGUI.Floater
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
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.buttonXHide = new DevComponents.DotNetBar.ButtonX();
			this.buttonXBack = new DevComponents.DotNetBar.ButtonX();
			this.SuspendLayout();
			// 
			// superTooltip
			// 
			this.superTooltip.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// buttonXHide
			// 
			this.buttonXHide.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXHide.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXHide.FocusCuesEnabled = false;
			this.buttonXHide.Image = global::NewBizWiz.CommonGUI.Properties.Resources.FloaterHide;
			this.buttonXHide.Location = new System.Drawing.Point(261, 0);
			this.buttonXHide.Name = "buttonXHide";
			this.buttonXHide.Size = new System.Drawing.Size(68, 106);
			this.buttonXHide.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.superTooltip.SetSuperTooltip(this.buttonXHide, new DevComponents.DotNetBar.SuperTooltipInfo("Hide", "", "Hide Application", null, null, DevComponents.DotNetBar.eTooltipColor.Gray));
			this.buttonXHide.TabIndex = 1;
			this.buttonXHide.Click += new System.EventHandler(this.buttonItemHide_Click);
			// 
			// buttonXBack
			// 
			this.buttonXBack.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXBack.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXBack.FocusCuesEnabled = false;
			this.buttonXBack.Location = new System.Drawing.Point(0, 0);
			this.buttonXBack.Name = "buttonXBack";
			this.buttonXBack.Size = new System.Drawing.Size(255, 106);
			this.buttonXBack.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXBack.TabIndex = 0;
			this.buttonXBack.Click += new System.EventHandler(this.buttonItemBack_Click);
			// 
			// FormFloater
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(329, 106);
			this.Controls.Add(this.buttonXHide);
			this.Controls.Add(this.buttonXBack);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormFloater";
			this.Opacity = 0.85D;
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "adSALESapps.com";
			this.TopMost = true;
			this.Shown += new System.EventHandler(this.FormFloater_Shown);
			this.ResumeLayout(false);

        }

        #endregion

		public DevComponents.DotNetBar.SuperTooltip superTooltip;
		private DevComponents.DotNetBar.ButtonX buttonXBack;
		private DevComponents.DotNetBar.ButtonX buttonXHide;

    }
}