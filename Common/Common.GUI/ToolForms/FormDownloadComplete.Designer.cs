namespace Asa.Common.GUI.ToolForms
{
	partial class FormDownloadComplete
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
			this.SuspendLayout();
			// 
			// buttonXOpenFile
			// 
			this.buttonXOpenFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOpenFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXOpenFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOpenFile.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOpenFile.Location = new System.Drawing.Point(10, 77);
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
			this.buttonXOpenFolder.Location = new System.Drawing.Point(150, 77);
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
			this.buttonXCancel.Location = new System.Drawing.Point(290, 77);
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
			this.laTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.Location = new System.Drawing.Point(12, 9);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(371, 52);
			this.laTitle.TabIndex = 3;
			this.laTitle.Text = "{0} is READY!";
			// 
			// FormDownloadComplete
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(395, 121);
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
			this.Name = "FormDownloadComplete";
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
	}
}