namespace AdSalesBrowser
{
	partial class FormUrlDetails
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
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCopy = new DevComponents.DotNetBar.ButtonX();
			this.buttonXEmail = new DevComponents.DotNetBar.ButtonX();
			this.pbTitleLogo = new System.Windows.Forms.PictureBox();
			this.laTitleText = new System.Windows.Forms.Label();
			this.laWebAddressTitle = new System.Windows.Forms.Label();
			this.laWebAddressValue = new System.Windows.Forms.Label();
			((System.ComponentModel.ISupportInitialize)(this.pbTitleLogo)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(290, 157);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(94, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 5;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Green;
			// 
			// buttonXCopy
			// 
			this.buttonXCopy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCopy.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCopy.Location = new System.Drawing.Point(150, 157);
			this.buttonXCopy.Name = "buttonXCopy";
			this.buttonXCopy.Size = new System.Drawing.Size(94, 32);
			this.buttonXCopy.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCopy.TabIndex = 4;
			this.buttonXCopy.Text = "Copy URL";
			this.buttonXCopy.TextColor = System.Drawing.Color.Green;
			this.buttonXCopy.Click += new System.EventHandler(this.buttonXCopy_Click);
			// 
			// buttonXEmail
			// 
			this.buttonXEmail.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXEmail.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXEmail.Location = new System.Drawing.Point(10, 157);
			this.buttonXEmail.Name = "buttonXEmail";
			this.buttonXEmail.Size = new System.Drawing.Size(94, 32);
			this.buttonXEmail.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXEmail.TabIndex = 3;
			this.buttonXEmail.Text = "Email URL";
			this.buttonXEmail.TextColor = System.Drawing.Color.Green;
			this.buttonXEmail.Click += new System.EventHandler(this.buttonXEmail_Click);
			// 
			// pbTitleLogo
			// 
			this.pbTitleLogo.BackColor = System.Drawing.Color.White;
			this.pbTitleLogo.ForeColor = System.Drawing.Color.Black;
			this.pbTitleLogo.Image = global::AdSalesBrowser.Properties.Resources.UrlDetailsForm;
			this.pbTitleLogo.Location = new System.Drawing.Point(10, 12);
			this.pbTitleLogo.Name = "pbTitleLogo";
			this.pbTitleLogo.Size = new System.Drawing.Size(48, 48);
			this.pbTitleLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
			this.pbTitleLogo.TabIndex = 6;
			this.pbTitleLogo.TabStop = false;
			// 
			// laTitleText
			// 
			this.laTitleText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laTitleText.BackColor = System.Drawing.Color.White;
			this.laTitleText.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitleText.ForeColor = System.Drawing.Color.Black;
			this.laTitleText.Location = new System.Drawing.Point(81, 12);
			this.laTitleText.Name = "laTitleText";
			this.laTitleText.Size = new System.Drawing.Size(303, 48);
			this.laTitleText.TabIndex = 7;
			this.laTitleText.Text = "URL Details";
			this.laTitleText.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laWebAddressTitle
			// 
			this.laWebAddressTitle.AutoSize = true;
			this.laWebAddressTitle.BackColor = System.Drawing.Color.White;
			this.laWebAddressTitle.ForeColor = System.Drawing.Color.Black;
			this.laWebAddressTitle.Location = new System.Drawing.Point(10, 86);
			this.laWebAddressTitle.Name = "laWebAddressTitle";
			this.laWebAddressTitle.Size = new System.Drawing.Size(90, 16);
			this.laWebAddressTitle.TabIndex = 8;
			this.laWebAddressTitle.Text = "Web Address:";
			// 
			// laWebAddressValue
			// 
			this.laWebAddressValue.BackColor = System.Drawing.Color.White;
			this.laWebAddressValue.ForeColor = System.Drawing.Color.Black;
			this.laWebAddressValue.Location = new System.Drawing.Point(10, 106);
			this.laWebAddressValue.Name = "laWebAddressValue";
			this.laWebAddressValue.Size = new System.Drawing.Size(374, 47);
			this.laWebAddressValue.TabIndex = 9;
			// 
			// FormUrlDetails
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(395, 201);
			this.Controls.Add(this.laWebAddressValue);
			this.Controls.Add(this.laWebAddressTitle);
			this.Controls.Add(this.laTitleText);
			this.Controls.Add(this.pbTitleLogo);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXCopy);
			this.Controls.Add(this.buttonXEmail);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormUrlDetails";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "URL Details";
			this.Shown += new System.EventHandler(this.FormUrlDetails_Shown);
			((System.ComponentModel.ISupportInitialize)(this.pbTitleLogo)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXCopy;
		private DevComponents.DotNetBar.ButtonX buttonXEmail;
		private System.Windows.Forms.PictureBox pbTitleLogo;
		private System.Windows.Forms.Label laTitleText;
		private System.Windows.Forms.Label laWebAddressTitle;
		private System.Windows.Forms.Label laWebAddressValue;
	}
}