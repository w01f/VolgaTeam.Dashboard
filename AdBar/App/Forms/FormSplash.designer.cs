namespace Asa.Bar.App.Forms
{
	partial class FormSplash
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
			this.pictureBoxLogo = new System.Windows.Forms.PictureBox();
			this.panelMain = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
			this.panelMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// laTitle
			// 
			this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laTitle.BackColor = System.Drawing.Color.Transparent;
			this.laTitle.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.ForeColor = System.Drawing.Color.White;
			this.laTitle.Location = new System.Drawing.Point(12, 84);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(351, 46);
			this.laTitle.TabIndex = 3;
			this.laTitle.Text = "Connecting...";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pictureBoxLogo
			// 
			this.pictureBoxLogo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pictureBoxLogo.BackColor = System.Drawing.Color.Transparent;
			this.pictureBoxLogo.ForeColor = System.Drawing.Color.Black;
			this.pictureBoxLogo.Image = global::Asa.Bar.App.Properties.Resources.SplashLogo;
			this.pictureBoxLogo.Location = new System.Drawing.Point(12, 11);
			this.pictureBoxLogo.Name = "pictureBoxLogo";
			this.pictureBoxLogo.Size = new System.Drawing.Size(351, 68);
			this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pictureBoxLogo.TabIndex = 4;
			this.pictureBoxLogo.TabStop = false;
			// 
			// panelMain
			// 
			this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(10)))), ((int)(((byte)(96)))), ((int)(((byte)(13)))));
			this.panelMain.Controls.Add(this.pictureBoxLogo);
			this.panelMain.Controls.Add(this.laTitle);
			this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelMain.Location = new System.Drawing.Point(1, 1);
			this.panelMain.Name = "panelMain";
			this.panelMain.Size = new System.Drawing.Size(375, 140);
			this.panelMain.TabIndex = 5;
			// 
			// FormSplash
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(377, 142);
			this.ControlBox = false;
			this.Controls.Add(this.panelMain);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSplash";
			this.Padding = new System.Windows.Forms.Padding(1);
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
			this.panelMain.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pictureBoxLogo;
		public System.Windows.Forms.Label laTitle;
		private System.Windows.Forms.Panel panelMain;
	}
}

