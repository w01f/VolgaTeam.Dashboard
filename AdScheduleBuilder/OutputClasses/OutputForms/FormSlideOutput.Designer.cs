namespace AdScheduleBuilder.OutputClasses.OutputForms
{
    partial class FormSlideOutput
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
            this.pbLogo = new System.Windows.Forms.PictureBox();
            this.laTitle = new System.Windows.Forms.Label();
            this.buttonXShow = new DevComponents.DotNetBar.ButtonX();
            this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // pbLogo
            // 
            this.pbLogo.Image = global::AdScheduleBuilder.Properties.Resources.PowerPoint;
            this.pbLogo.Location = new System.Drawing.Point(21, 6);
            this.pbLogo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(64, 67);
            this.pbLogo.TabIndex = 0;
            this.pbLogo.TabStop = false;
            // 
            // laTitle
            // 
            this.laTitle.AutoSize = true;
            this.laTitle.Font = new System.Drawing.Font("Tahoma", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTitle.Location = new System.Drawing.Point(107, 9);
            this.laTitle.Name = "laTitle";
            this.laTitle.Size = new System.Drawing.Size(256, 46);
            this.laTitle.TabIndex = 1;
            this.laTitle.Text = "Your Slides have been added\r\nto the Active Presentation:";
            // 
            // buttonXShow
            // 
            this.buttonXShow.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXShow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXShow.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXShow.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonXShow.Location = new System.Drawing.Point(21, 92);
            this.buttonXShow.Name = "buttonXShow";
            this.buttonXShow.Size = new System.Drawing.Size(175, 32);
            this.buttonXShow.TabIndex = 9;
            this.buttonXShow.Text = "Stay Here";
            this.buttonXShow.TextColor = System.Drawing.Color.Black;
            // 
            // buttonXOK
            // 
            this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonXOK.Location = new System.Drawing.Point(202, 92);
            this.buttonXOK.Name = "buttonXOK";
            this.buttonXOK.Size = new System.Drawing.Size(175, 32);
            this.buttonXOK.TabIndex = 10;
            this.buttonXOK.Text = "Open AdSchedule";
            this.buttonXOK.TextColor = System.Drawing.Color.Black;
            // 
            // FormSlideOutput
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(399, 136);
            this.Controls.Add(this.buttonXOK);
            this.Controls.Add(this.buttonXShow);
            this.Controls.Add(this.laTitle);
            this.Controls.Add(this.pbLogo);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormSlideOutput";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Slide Output Success!";
            this.TopMost = true;
            this.Shown += new System.EventHandler(this.FormSlideOutput_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label laTitle;
        private DevComponents.DotNetBar.ButtonX buttonXShow;
        private DevComponents.DotNetBar.ButtonX buttonXOK;
    }
}