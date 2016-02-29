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
			this.pnCase2 = new System.Windows.Forms.Panel();
			this.memoEditAuto2 = new DevExpress.XtraEditors.MemoEdit();
			this.checkEditAuto2 = new DevExpress.XtraEditors.CheckEdit();
			this.pnCase1 = new System.Windows.Forms.Panel();
			this.memoEditAuto1 = new DevExpress.XtraEditors.MemoEdit();
			this.checkEditAuto1 = new DevExpress.XtraEditors.CheckEdit();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			this.laTitle = new System.Windows.Forms.Label();
			this.pbLogo = new System.Windows.Forms.PictureBox();
			this.pnHeader = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.pnControls.SuspendLayout();
			this.pnCase2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditAuto2.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAuto2.Properties)).BeginInit();
			this.pnCase1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditAuto1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAuto1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).BeginInit();
			this.pnHeader.SuspendLayout();
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
			this.pnControls.Controls.Add(this.pnCase2);
			this.pnControls.Controls.Add(this.pnCase1);
			this.pnControls.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnControls.Location = new System.Drawing.Point(0, 68);
			this.pnControls.Name = "pnControls";
			this.pnControls.Size = new System.Drawing.Size(270, 466);
			this.pnControls.TabIndex = 103;
			this.pnControls.Resize += new System.EventHandler(this.pnControls_Resize);
			// 
			// pnCase2
			// 
			this.pnCase2.Controls.Add(this.memoEditAuto2);
			this.pnCase2.Controls.Add(this.checkEditAuto2);
			this.pnCase2.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnCase2.Location = new System.Drawing.Point(0, 123);
			this.pnCase2.Name = "pnCase2";
			this.pnCase2.Size = new System.Drawing.Size(270, 134);
			this.pnCase2.TabIndex = 17;
			// 
			// memoEditAuto2
			// 
			this.memoEditAuto2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditAuto2.Enabled = false;
			this.memoEditAuto2.Location = new System.Drawing.Point(29, 16);
			this.memoEditAuto2.Name = "memoEditAuto2";
			this.memoEditAuto2.Properties.AllowFocused = false;
			this.memoEditAuto2.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.memoEditAuto2.Properties.Appearance.Options.UseBackColor = true;
			this.memoEditAuto2.Size = new System.Drawing.Size(232, 101);
			this.memoEditAuto2.StyleController = this.styleController;
			this.memoEditAuto2.TabIndex = 14;
			this.memoEditAuto2.EditValueChanged += new System.EventHandler(this.memoEdit_EditValueChanged);
			// 
			// checkEditAuto2
			// 
			this.checkEditAuto2.Location = new System.Drawing.Point(3, 14);
			this.checkEditAuto2.Name = "checkEditAuto2";
			this.checkEditAuto2.Properties.Caption = "checkEdit1";
			this.checkEditAuto2.Size = new System.Drawing.Size(19, 20);
			this.checkEditAuto2.StyleController = this.styleController;
			this.checkEditAuto2.TabIndex = 9;
			this.checkEditAuto2.TabStop = false;
			this.checkEditAuto2.CheckedChanged += new System.EventHandler(this.checkEditCase_CheckedChanged);
			// 
			// pnCase1
			// 
			this.pnCase1.Controls.Add(this.memoEditAuto1);
			this.pnCase1.Controls.Add(this.checkEditAuto1);
			this.pnCase1.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnCase1.Location = new System.Drawing.Point(0, 0);
			this.pnCase1.Name = "pnCase1";
			this.pnCase1.Size = new System.Drawing.Size(270, 123);
			this.pnCase1.TabIndex = 16;
			// 
			// memoEditAuto1
			// 
			this.memoEditAuto1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditAuto1.Enabled = false;
			this.memoEditAuto1.Location = new System.Drawing.Point(29, 14);
			this.memoEditAuto1.Name = "memoEditAuto1";
			this.memoEditAuto1.Properties.AllowFocused = false;
			this.memoEditAuto1.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.memoEditAuto1.Properties.Appearance.Options.UseBackColor = true;
			this.memoEditAuto1.Size = new System.Drawing.Size(232, 94);
			this.memoEditAuto1.StyleController = this.styleController;
			this.memoEditAuto1.TabIndex = 13;
			this.memoEditAuto1.EditValueChanged += new System.EventHandler(this.memoEdit_EditValueChanged);
			// 
			// checkEditAuto1
			// 
			this.checkEditAuto1.Location = new System.Drawing.Point(3, 12);
			this.checkEditAuto1.Name = "checkEditAuto1";
			this.checkEditAuto1.Properties.Caption = "checkEdit1";
			this.checkEditAuto1.Size = new System.Drawing.Size(19, 20);
			this.checkEditAuto1.StyleController = this.styleController;
			this.checkEditAuto1.TabIndex = 8;
			this.checkEditAuto1.TabStop = false;
			this.checkEditAuto1.CheckedChanged += new System.EventHandler(this.checkEditCase_CheckedChanged);
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
			this.laTitle.Font = new System.Drawing.Font("Arial", 9.75F);
			this.laTitle.Location = new System.Drawing.Point(97, 9);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(164, 48);
			this.laTitle.TabIndex = 104;
			this.laTitle.Text = "Show basic digital info at the bottom of this slide:";
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
			this.pnHeader.Size = new System.Drawing.Size(270, 68);
			this.pnHeader.TabIndex = 106;
			// 
			// DigitalInfoControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnControls);
			this.Controls.Add(this.pnHeader);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "DigitalInfoControl";
			this.Size = new System.Drawing.Size(270, 534);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.pnControls.ResumeLayout(false);
			this.pnCase2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.memoEditAuto2.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAuto2.Properties)).EndInit();
			this.pnCase1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.memoEditAuto1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAuto1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbLogo)).EndInit();
			this.pnHeader.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.Panel pnControls;
		private DevComponents.DotNetBar.SuperTooltip superTooltip;
		private DevExpress.XtraEditors.MemoEdit memoEditAuto2;
		private DevExpress.XtraEditors.MemoEdit memoEditAuto1;
		private DevExpress.XtraEditors.CheckEdit checkEditAuto2;
		private DevExpress.XtraEditors.CheckEdit checkEditAuto1;
		private System.Windows.Forms.Panel pnCase2;
		private System.Windows.Forms.Panel pnCase1;
		private System.Windows.Forms.Label laTitle;
		private System.Windows.Forms.PictureBox pbLogo;
		private System.Windows.Forms.Panel pnHeader;
	}
}