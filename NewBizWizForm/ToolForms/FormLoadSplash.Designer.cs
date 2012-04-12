namespace NewBizWizForm.ToolForms
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
            this.prbProgress = new System.Windows.Forms.ProgressBar();
            this.laTopCaption = new System.Windows.Forms.Label();
            this.laBottomCaption = new System.Windows.Forms.Label();
            this.pbLogo = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
            this.SuspendLayout();
            // 
            // prbProgress
            // 
            this.prbProgress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.prbProgress.Location = new System.Drawing.Point(12, 121);
            this.prbProgress.Maximum = 12;
            this.prbProgress.Name = "prbProgress";
            this.prbProgress.Size = new System.Drawing.Size(349, 23);
            this.prbProgress.Step = 1;
            this.prbProgress.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.prbProgress.TabIndex = 3;
            this.prbProgress.UseWaitCursor = true;
            // 
            // laTopCaption
            // 
            this.laTopCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.laTopCaption.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.laTopCaption.ForeColor = System.Drawing.Color.White;
            this.laTopCaption.Location = new System.Drawing.Point(113, 9);
            this.laTopCaption.Name = "laTopCaption";
            this.laTopCaption.Size = new System.Drawing.Size(248, 63);
            this.laTopCaption.TabIndex = 5;
            this.laTopCaption.Text = "PowerPoint is Loading Your\r\nDashboard...";
            // 
            // laBottomCaption
            // 
            this.laBottomCaption.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.laBottomCaption.Font = new System.Drawing.Font("Arial", 8F);
            this.laBottomCaption.ForeColor = System.Drawing.Color.White;
            this.laBottomCaption.Location = new System.Drawing.Point(114, 72);
            this.laBottomCaption.Name = "laBottomCaption";
            this.laBottomCaption.Size = new System.Drawing.Size(247, 46);
            this.laBottomCaption.TabIndex = 6;
            this.laBottomCaption.Text = "This may take a few seconds, depending on\r\nthe speed of your PC";
            // 
            // pbLogo
            // 
            this.pbLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pbLogo.Image = global::NewBizWizForm.Properties.Resources.PowerPoint07;
            this.pbLogo.Location = new System.Drawing.Point(12, 6);
            this.pbLogo.Name = "pbLogo";
            this.pbLogo.Size = new System.Drawing.Size(95, 95);
            this.pbLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbLogo.TabIndex = 4;
            this.pbLogo.TabStop = false;
            // 
            // FormLoadSplash
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.ClientSize = new System.Drawing.Size(373, 156);
            this.ControlBox = false;
            this.Controls.Add(this.laBottomCaption);
            this.Controls.Add(this.laTopCaption);
            this.Controls.Add(this.pbLogo);
            this.Controls.Add(this.prbProgress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormLoadSplash";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ProgressForm";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        internal System.Windows.Forms.ProgressBar prbProgress;
        private System.Windows.Forms.PictureBox pbLogo;
        private System.Windows.Forms.Label laTopCaption;
        private System.Windows.Forms.Label laBottomCaption;
    }
}