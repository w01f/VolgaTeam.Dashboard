namespace AdSalesBrowser
{
	partial class FormVideoDownloadComplete
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
			this.buttonXOpenFile = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOpenFolder = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.laTitle = new System.Windows.Forms.Label();
			this.buttonXAddVideoToSlide = new DevComponents.DotNetBar.ButtonX();
			this.SuspendLayout();
			// 
			// buttonXOpenFile
			// 
			this.buttonXOpenFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXOpenFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpenFile.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOpenFile.Location = new System.Drawing.Point(25, 77);
			this.buttonXOpenFile.Name = "buttonXOpenFile";
			this.buttonXOpenFile.Size = new System.Drawing.Size(94, 32);
			this.buttonXOpenFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOpenFile.TabIndex = 0;
			this.buttonXOpenFile.Text = "Open File";
			this.buttonXOpenFile.Click += new System.EventHandler(this.buttonXOpenFile_Click);
			// 
			// buttonXOpenFolder
			// 
			this.buttonXOpenFolder.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpenFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXOpenFolder.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpenFolder.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOpenFolder.Location = new System.Drawing.Point(134, 77);
			this.buttonXOpenFolder.Name = "buttonXOpenFolder";
			this.buttonXOpenFolder.Size = new System.Drawing.Size(94, 32);
			this.buttonXOpenFolder.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOpenFolder.TabIndex = 1;
			this.buttonXOpenFolder.Text = "Go to File";
			this.buttonXOpenFolder.Click += new System.EventHandler(this.buttonXOpenFolder_Click);
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(352, 77);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(94, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 2;
			this.buttonXCancel.Text = "Cancel";
			// 
			// laTitle
			// 
			this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laTitle.BackColor = System.Drawing.Color.White;
			this.laTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.ForeColor = System.Drawing.Color.Black;
			this.laTitle.Location = new System.Drawing.Point(12, 9);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(447, 52);
			this.laTitle.TabIndex = 3;
			this.laTitle.Text = "{0} is READY!";
			// 
			// buttonXAddVideoToSlide
			// 
			this.buttonXAddVideoToSlide.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXAddVideoToSlide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXAddVideoToSlide.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXAddVideoToSlide.DialogResult = System.Windows.Forms.DialogResult.Abort;
			this.buttonXAddVideoToSlide.Location = new System.Drawing.Point(243, 77);
			this.buttonXAddVideoToSlide.Name = "buttonXAddVideoToSlide";
			this.buttonXAddVideoToSlide.Size = new System.Drawing.Size(94, 32);
			this.buttonXAddVideoToSlide.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXAddVideoToSlide.TabIndex = 4;
			this.buttonXAddVideoToSlide.Text = "Add to Slide";
			// 
			// FormVideoDownloadComplete
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(471, 121);
			this.Controls.Add(this.buttonXAddVideoToSlide);
			this.Controls.Add(this.laTitle);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXOpenFolder);
			this.Controls.Add(this.buttonXOpenFile);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormVideoDownloadComplete";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Download Complete";
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonXOpenFile;
		private DevComponents.DotNetBar.ButtonX buttonXOpenFolder;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private System.Windows.Forms.Label laTitle;
		private DevComponents.DotNetBar.ButtonX buttonXAddVideoToSlide;
	}
}