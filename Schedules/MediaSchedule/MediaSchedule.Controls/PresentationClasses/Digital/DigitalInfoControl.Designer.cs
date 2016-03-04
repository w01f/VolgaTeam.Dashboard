namespace Asa.Media.Controls.PresentationClasses.Digital
{
	partial class DigitalInfoControl
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
			this.components = new System.ComponentModel.Container();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.pnControls = new System.Windows.Forms.Panel();
			this.memoEditInfo = new DevExpress.XtraEditors.MemoEdit();
			this.checkEditEnable = new DevExpress.XtraEditors.CheckEdit();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.laTitle = new System.Windows.Forms.Label();
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.pnHeader = new System.Windows.Forms.Panel();
			this.pnBottom = new System.Windows.Forms.Panel();
			this.buttonXRefreshInfoCase3 = new DevComponents.DotNetBar.ButtonX();
			this.buttonXRefreshInfoCase2 = new DevComponents.DotNetBar.ButtonX();
			this.buttonXRefreshInfoCase1 = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.pnControls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditInfo.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditEnable.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.pnHeader.SuspendLayout();
			this.pnBottom.SuspendLayout();
			this.SuspendLayout();
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDisabled.Options.UseFont = true;
			this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDown.Options.UseFont = true;
			this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
			this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceFocused.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceReadOnly.Options.UseFont = true;
			// 
			// pnControls
			// 
			this.pnControls.BackColor = System.Drawing.Color.Transparent;
			this.pnControls.Controls.Add(this.memoEditInfo);
			this.pnControls.Controls.Add(this.checkEditEnable);
			this.pnControls.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnControls.Location = new System.Drawing.Point(0, 75);
			this.pnControls.Name = "pnControls";
			this.pnControls.Size = new System.Drawing.Size(270, 288);
			this.pnControls.TabIndex = 103;
			// 
			// memoEditInfo
			// 
			this.memoEditInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditInfo.Enabled = false;
			this.memoEditInfo.Location = new System.Drawing.Point(31, 6);
			this.memoEditInfo.Name = "memoEditInfo";
			this.memoEditInfo.Properties.AllowFocused = false;
			this.memoEditInfo.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.memoEditInfo.Properties.Appearance.Options.UseBackColor = true;
			this.memoEditInfo.Size = new System.Drawing.Size(232, 275);
			this.memoEditInfo.StyleController = this.styleController;
			this.memoEditInfo.TabIndex = 13;
			this.memoEditInfo.EditValueChanged += new System.EventHandler(this.memoEdit_EditValueChanged);
			// 
			// checkEditEnable
			// 
			this.checkEditEnable.Location = new System.Drawing.Point(5, 4);
			this.checkEditEnable.Name = "checkEditEnable";
			this.checkEditEnable.Properties.Caption = "checkEdit1";
			this.checkEditEnable.Size = new System.Drawing.Size(19, 20);
			this.checkEditEnable.StyleController = this.styleController;
			this.checkEditEnable.TabIndex = 8;
			this.checkEditEnable.TabStop = false;
			this.checkEditEnable.CheckedChanged += new System.EventHandler(this.checkEditEnable_CheckedChanged);
			// 
			// superTooltip
			// 
			this.superTooltip.DefaultTooltipSettings = new DevComponents.DotNetBar.SuperTooltipInfo("", "", "", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// laTitle
			// 
			this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laTitle.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.ForeColor = System.Drawing.Color.Gray;
			this.laTitle.Location = new System.Drawing.Point(97, 9);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(164, 48);
			this.laTitle.TabIndex = 104;
			this.laTitle.Text = "Do you want to show basic digital product info on this slide?";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pbLogo
			// 
			this.pbLogo.Image = global::Asa.Media.Controls.Properties.Resources.DigitalInfoLogo;
			this.pbLogo.Location = new System.Drawing.Point(9, 9);
			this.pbLogo.Name = "pbLogo";
			this.pbLogo.Size = new System.Drawing.Size(79, 48);
			this.pbLogo.TabIndex = 105;
			this.pbLogo.TabStop = false;
			// 
			// pnHeader
			// 
			this.pnHeader.Controls.Add(this.pbLogo);
			this.pnHeader.Controls.Add(this.laTitle);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(270, 75);
			this.pnHeader.TabIndex = 106;
			// 
			// pnBottom
			// 
			this.pnBottom.Controls.Add(this.buttonXRefreshInfoCase3);
			this.pnBottom.Controls.Add(this.buttonXRefreshInfoCase2);
			this.pnBottom.Controls.Add(this.buttonXRefreshInfoCase1);
			this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottom.Enabled = false;
			this.pnBottom.Location = new System.Drawing.Point(0, 363);
			this.pnBottom.Name = "pnBottom";
			this.pnBottom.Size = new System.Drawing.Size(270, 171);
			this.pnBottom.TabIndex = 107;
			// 
			// buttonXRefreshInfoCase3
			// 
			this.buttonXRefreshInfoCase3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXRefreshInfoCase3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXRefreshInfoCase3.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXRefreshInfoCase3.Location = new System.Drawing.Point(9, 121);
			this.buttonXRefreshInfoCase3.Name = "buttonXRefreshInfoCase3";
			this.buttonXRefreshInfoCase3.Size = new System.Drawing.Size(254, 32);
			this.buttonXRefreshInfoCase3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXRefreshInfoCase3.TabIndex = 2;
			this.buttonXRefreshInfoCase3.Text = "Refresh Products Impressions and CPM";
			this.buttonXRefreshInfoCase3.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.buttonXRefreshInfoCase3.Click += new System.EventHandler(this.buttonXRefreshInfoCase3_Click);
			// 
			// buttonXRefreshInfoCase2
			// 
			this.buttonXRefreshInfoCase2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXRefreshInfoCase2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXRefreshInfoCase2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXRefreshInfoCase2.Location = new System.Drawing.Point(9, 68);
			this.buttonXRefreshInfoCase2.Name = "buttonXRefreshInfoCase2";
			this.buttonXRefreshInfoCase2.Size = new System.Drawing.Size(254, 32);
			this.buttonXRefreshInfoCase2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXRefreshInfoCase2.TabIndex = 1;
			this.buttonXRefreshInfoCase2.Text = "Refresh Products and Impressions";
			this.buttonXRefreshInfoCase2.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.buttonXRefreshInfoCase2.Click += new System.EventHandler(this.buttonXRefreshInfoCase2_Click);
			// 
			// buttonXRefreshInfoCase1
			// 
			this.buttonXRefreshInfoCase1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXRefreshInfoCase1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXRefreshInfoCase1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXRefreshInfoCase1.Location = new System.Drawing.Point(9, 15);
			this.buttonXRefreshInfoCase1.Name = "buttonXRefreshInfoCase1";
			this.buttonXRefreshInfoCase1.Size = new System.Drawing.Size(254, 32);
			this.buttonXRefreshInfoCase1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXRefreshInfoCase1.TabIndex = 0;
			this.buttonXRefreshInfoCase1.Text = "Refresh Digital Products";
			this.buttonXRefreshInfoCase1.TextColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
			this.buttonXRefreshInfoCase1.Click += new System.EventHandler(this.buttonXRefreshInfoCase1_Click);
			// 
			// DigitalInfoControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnControls);
			this.Controls.Add(this.pnBottom);
			this.Controls.Add(this.pnHeader);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "DigitalInfoControl";
			this.Size = new System.Drawing.Size(270, 534);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.pnControls.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.memoEditInfo.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditEnable.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.pnHeader.ResumeLayout(false);
			this.pnBottom.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.Panel pnControls;
		private DevComponents.DotNetBar.SuperTooltip superTooltip;
		private DevExpress.XtraEditors.MemoEdit memoEditInfo;
		private DevExpress.XtraEditors.CheckEdit checkEditEnable;
		private System.Windows.Forms.Label laTitle;
		private System.Windows.Forms.PictureBox pbLogo;
		private System.Windows.Forms.Panel pnHeader;
		private System.Windows.Forms.Panel pnBottom;
		private DevComponents.DotNetBar.ButtonX buttonXRefreshInfoCase3;
		private DevComponents.DotNetBar.ButtonX buttonXRefreshInfoCase2;
		private DevComponents.DotNetBar.ButtonX buttonXRefreshInfoCase1;
	}
}