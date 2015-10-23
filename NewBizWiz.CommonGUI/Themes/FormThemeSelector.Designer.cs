﻿namespace Asa.CommonGUI.Themes
{
	partial class FormThemeSelector
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
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnButtons = new System.Windows.Forms.Panel();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.pnHeader = new System.Windows.Forms.Panel();
			this.laThemeName = new System.Windows.Forms.Label();
			this.laSlideSize = new System.Windows.Forms.Label();
			this.pnButtons.SuspendLayout();
			this.pnHeader.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.BackColor = System.Drawing.Color.Transparent;
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.ForeColor = System.Drawing.Color.Black;
			this.pnMain.Location = new System.Drawing.Point(0, 40);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(894, 408);
			this.pnMain.TabIndex = 3;
			// 
			// pnButtons
			// 
			this.pnButtons.BackColor = System.Drawing.Color.Transparent;
			this.pnButtons.Controls.Add(this.buttonXOK);
			this.pnButtons.Controls.Add(this.buttonXCancel);
			this.pnButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnButtons.ForeColor = System.Drawing.Color.Black;
			this.pnButtons.Location = new System.Drawing.Point(0, 448);
			this.pnButtons.Name = "pnButtons";
			this.pnButtons.Size = new System.Drawing.Size(894, 46);
			this.pnButtons.TabIndex = 4;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(613, 7);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(122, 32);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 4;
			this.buttonXOK.Text = "Select";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(760, 7);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(122, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 5;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// pnHeader
			// 
			this.pnHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(211)))), ((int)(((byte)(211)))));
			this.pnHeader.Controls.Add(this.laThemeName);
			this.pnHeader.Controls.Add(this.laSlideSize);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeader.ForeColor = System.Drawing.Color.Black;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(894, 40);
			this.pnHeader.TabIndex = 6;
			// 
			// laThemeName
			// 
			this.laThemeName.BackColor = System.Drawing.Color.White;
			this.laThemeName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.laThemeName.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laThemeName.ForeColor = System.Drawing.Color.Black;
			this.laThemeName.Location = new System.Drawing.Point(333, 0);
			this.laThemeName.Name = "laThemeName";
			this.laThemeName.Size = new System.Drawing.Size(561, 40);
			this.laThemeName.TabIndex = 3;
			this.laThemeName.Text = "Slide Name";
			this.laThemeName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// laSlideSize
			// 
			this.laSlideSize.BackColor = System.Drawing.Color.White;
			this.laSlideSize.Dock = System.Windows.Forms.DockStyle.Left;
			this.laSlideSize.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laSlideSize.ForeColor = System.Drawing.Color.Black;
			this.laSlideSize.Location = new System.Drawing.Point(0, 0);
			this.laSlideSize.Name = "laSlideSize";
			this.laSlideSize.Size = new System.Drawing.Size(333, 40);
			this.laSlideSize.TabIndex = 2;
			this.laSlideSize.Text = "Slide Size: {0}";
			this.laSlideSize.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// FormThemeSelector
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(894, 494);
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.pnHeader);
			this.Controls.Add(this.pnButtons);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormThemeSelector";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Theme";
			this.TopMost = true;
			this.pnButtons.ResumeLayout(false);
			this.pnHeader.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnMain;
		private System.Windows.Forms.Panel pnButtons;
		private DevComponents.DotNetBar.ButtonX buttonXOK;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private System.Windows.Forms.Panel pnHeader;
		private System.Windows.Forms.Label laThemeName;
		private System.Windows.Forms.Label laSlideSize;
	}
}