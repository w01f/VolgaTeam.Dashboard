namespace NewBizWiz.Dashboard.ToolForms
{
    partial class FormLoadSplash
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
			this.laTopCaption = new System.Windows.Forms.Label();
			this.laBottomCaption = new System.Windows.Forms.Label();
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.circularProgress1 = new DevComponents.DotNetBar.Controls.CircularProgress();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// laTopCaption
			// 
			this.laTopCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laTopCaption.BackColor = System.Drawing.Color.White;
			this.laTopCaption.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTopCaption.ForeColor = System.Drawing.Color.Black;
			this.laTopCaption.Location = new System.Drawing.Point(113, 12);
			this.laTopCaption.Name = "laTopCaption";
			this.laTopCaption.Size = new System.Drawing.Size(256, 60);
			this.laTopCaption.TabIndex = 5;
			this.laTopCaption.Text = "PowerPoint is Loading Your\r\nDashboard...";
			// 
			// laBottomCaption
			// 
			this.laBottomCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laBottomCaption.BackColor = System.Drawing.Color.White;
			this.laBottomCaption.Font = new System.Drawing.Font("Arial", 8F);
			this.laBottomCaption.ForeColor = System.Drawing.Color.Black;
			this.laBottomCaption.Location = new System.Drawing.Point(114, 72);
			this.laBottomCaption.Name = "laBottomCaption";
			this.laBottomCaption.Size = new System.Drawing.Size(255, 35);
			this.laBottomCaption.TabIndex = 6;
			this.laBottomCaption.Text = "This may take a few seconds, depending on\r\nthe speed of your PC";
			// 
			// pbLogo
			// 
			this.pbLogo.BackColor = System.Drawing.Color.White;
			this.pbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
			this.pbLogo.ForeColor = System.Drawing.Color.Black;
			this.pbLogo.Image = global::NewBizWiz.Dashboard.Properties.Resources.Output;
			this.pbLogo.Location = new System.Drawing.Point(12, 23);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(72, 72);
			this.pbLogo.TabIndex = 4;
			this.pbLogo.TabStop = false;
			// 
			// circularProgress1
			// 
			this.circularProgress1.BackColor = System.Drawing.Color.White;
			// 
			// 
			// 
			this.circularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.circularProgress1.FocusCuesEnabled = false;
			this.circularProgress1.Location = new System.Drawing.Point(366, 24);
			this.circularProgress1.Name = "circularProgress1";
			this.circularProgress1.ProgressBarType = DevComponents.DotNetBar.eCircularProgressType.Dot;
			this.circularProgress1.ProgressColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.circularProgress1.Size = new System.Drawing.Size(88, 70);
			this.circularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP;
			this.circularProgress1.TabIndex = 7;
			// 
			// FormLoadSplash
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(466, 119);
			this.ControlBox = false;
			this.Controls.Add(this.circularProgress1);
			this.Controls.Add(this.pbLogo);
			this.Controls.Add(this.laTopCaption);
			this.Controls.Add(this.laBottomCaption);
			this.DoubleBuffered = true;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "FormLoadSplash";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label laTopCaption;
        private System.Windows.Forms.Label laBottomCaption;
        private DevComponents.DotNetBar.Controls.CircularProgress circularProgress1;
    }
}