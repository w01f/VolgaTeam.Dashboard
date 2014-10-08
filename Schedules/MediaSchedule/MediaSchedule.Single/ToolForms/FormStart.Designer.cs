namespace NewBizWiz.MediaSchedule.Single
{
    partial class FormStart
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
			this.laTitle = new System.Windows.Forms.Label();
			this.buttonXNew = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOpen = new DevComponents.DotNetBar.ButtonX();
			this.buttonXExit = new DevComponents.DotNetBar.ButtonX();
			this.SuspendLayout();
			// 
			// laTitle
			// 
			this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laTitle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.Location = new System.Drawing.Point(8, 9);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(431, 73);
			this.laTitle.TabIndex = 0;
			this.laTitle.Text = "Do you want to Create a NEW PowerPoint  {0} Schedule, or Open a Saved {0} Schedul" +
    "e?";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonXNew
			// 
			this.buttonXNew.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXNew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXNew.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXNew.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.buttonXNew.Location = new System.Drawing.Point(14, 90);
			this.buttonXNew.Name = "buttonXNew";
			this.buttonXNew.Size = new System.Drawing.Size(125, 33);
			this.buttonXNew.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXNew.TabIndex = 1;
			this.buttonXNew.Text = "New Schedule";
			this.buttonXNew.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXOpen
			// 
			this.buttonXOpen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXOpen.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpen.DialogResult = System.Windows.Forms.DialogResult.No;
			this.buttonXOpen.Location = new System.Drawing.Point(161, 90);
			this.buttonXOpen.Name = "buttonXOpen";
			this.buttonXOpen.Size = new System.Drawing.Size(125, 33);
			this.buttonXOpen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOpen.TabIndex = 2;
			this.buttonXOpen.Text = "Open Schedule";
			this.buttonXOpen.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXExit
			// 
			this.buttonXExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXExit.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXExit.Location = new System.Drawing.Point(308, 90);
			this.buttonXExit.Name = "buttonXExit";
			this.buttonXExit.Size = new System.Drawing.Size(125, 33);
			this.buttonXExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXExit.TabIndex = 3;
			this.buttonXExit.Text = "CANCEL && EXIT";
			this.buttonXExit.TextColor = System.Drawing.Color.Red;
			// 
			// FormStart
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(446, 135);
			this.Controls.Add(this.buttonXExit);
			this.Controls.Add(this.buttonXOpen);
			this.Controls.Add(this.buttonXNew);
			this.Controls.Add(this.laTitle);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormStart";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SellerPoint for {0}";
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label laTitle;
        private DevComponents.DotNetBar.ButtonX buttonXNew;
        private DevComponents.DotNetBar.ButtonX buttonXExit;
        public DevComponents.DotNetBar.ButtonX buttonXOpen;
    }
}