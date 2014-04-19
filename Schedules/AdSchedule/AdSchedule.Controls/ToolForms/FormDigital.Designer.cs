namespace NewBizWiz.AdSchedule.Controls.ToolForms
{
	partial class FormDigital
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
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.memoEditManual = new DevExpress.XtraEditors.MemoEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
			this.checkEditEnable = new DevExpress.XtraEditors.CheckEdit();
			this.pnControls = new System.Windows.Forms.Panel();
			this.memoEditAuto3 = new DevExpress.XtraEditors.MemoEdit();
			this.memoEditAuto2 = new DevExpress.XtraEditors.MemoEdit();
			this.memoEditAuto1 = new DevExpress.XtraEditors.MemoEdit();
			this.checkEditManual = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditAuto3 = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditAuto2 = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditAuto1 = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditApplyAll = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditOutputOnlyOnce = new DevExpress.XtraEditors.CheckEdit();
			this.superTooltip = new DevComponents.DotNetBar.SuperTooltip();
			((System.ComponentModel.ISupportInitialize)(this.memoEditManual.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditEnable.Properties)).BeginInit();
			this.pnControls.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.memoEditAuto3.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditAuto2.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditAuto1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditManual.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAuto3.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAuto2.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAuto1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditApplyAll.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditOutputOnlyOnce.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(479, 481);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(107, 36);
			this.buttonXCancel.TabIndex = 8;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(360, 481);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(107, 36);
			this.buttonXOK.TabIndex = 7;
			this.buttonXOK.Text = "OK";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// memoEditManual
			// 
			this.memoEditManual.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditManual.Enabled = false;
			this.memoEditManual.Location = new System.Drawing.Point(36, 290);
			this.memoEditManual.Name = "memoEditManual";
			this.memoEditManual.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.memoEditManual.Properties.Appearance.Options.UseFont = true;
			this.memoEditManual.Properties.NullText = "Type your own info here...";
			this.memoEditManual.Size = new System.Drawing.Size(551, 134);
			this.memoEditManual.TabIndex = 4;
			this.memoEditManual.EditValueChanged += new System.EventHandler(this.memoEditManual_EditValueChanged);
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
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
			// 
			// checkEditEnable
			// 
			this.checkEditEnable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditEnable.Location = new System.Drawing.Point(9, 7);
			this.checkEditEnable.Name = "checkEditEnable";
			this.checkEditEnable.Properties.Appearance.Font = new System.Drawing.Font("Arial", 12F);
			this.checkEditEnable.Properties.Appearance.Options.UseFont = true;
			this.checkEditEnable.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditEnable.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditEnable.Properties.Caption = "Show Digital Product Info on this PowerPoint Slide:";
			this.checkEditEnable.Size = new System.Drawing.Size(570, 23);
			this.checkEditEnable.TabIndex = 10;
			this.checkEditEnable.CheckedChanged += new System.EventHandler(this.checkEditEnable_CheckedChanged);
			// 
			// pnControls
			// 
			this.pnControls.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.pnControls.Controls.Add(this.memoEditAuto3);
			this.pnControls.Controls.Add(this.memoEditAuto2);
			this.pnControls.Controls.Add(this.memoEditAuto1);
			this.pnControls.Controls.Add(this.checkEditManual);
			this.pnControls.Controls.Add(this.memoEditManual);
			this.pnControls.Controls.Add(this.checkEditAuto3);
			this.pnControls.Controls.Add(this.checkEditAuto2);
			this.pnControls.Controls.Add(this.checkEditAuto1);
			this.pnControls.Enabled = false;
			this.pnControls.Location = new System.Drawing.Point(-1, 36);
			this.pnControls.Name = "pnControls";
			this.pnControls.Size = new System.Drawing.Size(595, 427);
			this.pnControls.TabIndex = 103;
			// 
			// memoEditAuto3
			// 
			this.memoEditAuto3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditAuto3.Enabled = false;
			this.memoEditAuto3.Location = new System.Drawing.Point(36, 199);
			this.memoEditAuto3.Name = "memoEditAuto3";
			this.memoEditAuto3.Properties.AllowFocused = false;
			this.memoEditAuto3.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.memoEditAuto3.Properties.Appearance.Options.UseBackColor = true;
			this.memoEditAuto3.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.memoEditAuto3.Properties.ReadOnly = true;
			this.memoEditAuto3.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.memoEditAuto3.Size = new System.Drawing.Size(551, 85);
			this.memoEditAuto3.StyleController = this.styleController;
			this.memoEditAuto3.TabIndex = 7;
			this.memoEditAuto3.EditValueChanged += new System.EventHandler(this.memoEditAuto_EditValueChanged);
			// 
			// memoEditAuto2
			// 
			this.memoEditAuto2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditAuto2.Enabled = false;
			this.memoEditAuto2.Location = new System.Drawing.Point(36, 108);
			this.memoEditAuto2.Name = "memoEditAuto2";
			this.memoEditAuto2.Properties.AllowFocused = false;
			this.memoEditAuto2.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.memoEditAuto2.Properties.Appearance.Options.UseBackColor = true;
			this.memoEditAuto2.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.memoEditAuto2.Properties.ReadOnly = true;
			this.memoEditAuto2.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.memoEditAuto2.Size = new System.Drawing.Size(551, 85);
			this.memoEditAuto2.StyleController = this.styleController;
			this.memoEditAuto2.TabIndex = 6;
			this.memoEditAuto2.EditValueChanged += new System.EventHandler(this.memoEditAuto_EditValueChanged);
			// 
			// memoEditAuto1
			// 
			this.memoEditAuto1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditAuto1.Enabled = false;
			this.memoEditAuto1.Location = new System.Drawing.Point(36, 17);
			this.memoEditAuto1.Name = "memoEditAuto1";
			this.memoEditAuto1.Properties.AllowFocused = false;
			this.memoEditAuto1.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.memoEditAuto1.Properties.Appearance.Options.UseBackColor = true;
			this.memoEditAuto1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.memoEditAuto1.Properties.ReadOnly = true;
			this.memoEditAuto1.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.memoEditAuto1.Size = new System.Drawing.Size(551, 85);
			this.memoEditAuto1.StyleController = this.styleController;
			this.memoEditAuto1.TabIndex = 5;
			this.memoEditAuto1.EditValueChanged += new System.EventHandler(this.memoEditAuto_EditValueChanged);
			// 
			// checkEditManual
			// 
			this.checkEditManual.Location = new System.Drawing.Point(10, 290);
			this.checkEditManual.Name = "checkEditManual";
			this.checkEditManual.Properties.Caption = "checkEdit1";
			this.checkEditManual.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditManual.Properties.RadioGroupIndex = 1;
			this.checkEditManual.Size = new System.Drawing.Size(20, 21);
			this.checkEditManual.StyleController = this.styleController;
			this.checkEditManual.TabIndex = 3;
			this.checkEditManual.TabStop = false;
			this.checkEditManual.CheckedChanged += new System.EventHandler(this.checkEditCase_CheckedChanged);
			// 
			// checkEditAuto3
			// 
			this.checkEditAuto3.Location = new System.Drawing.Point(10, 197);
			this.checkEditAuto3.Name = "checkEditAuto3";
			this.checkEditAuto3.Properties.Caption = "checkEdit1";
			this.checkEditAuto3.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditAuto3.Properties.RadioGroupIndex = 1;
			this.checkEditAuto3.Size = new System.Drawing.Size(20, 21);
			this.checkEditAuto3.StyleController = this.styleController;
			this.checkEditAuto3.TabIndex = 2;
			this.checkEditAuto3.TabStop = false;
			this.checkEditAuto3.CheckedChanged += new System.EventHandler(this.checkEditCase_CheckedChanged);
			// 
			// checkEditAuto2
			// 
			this.checkEditAuto2.Location = new System.Drawing.Point(10, 106);
			this.checkEditAuto2.Name = "checkEditAuto2";
			this.checkEditAuto2.Properties.Caption = "checkEdit1";
			this.checkEditAuto2.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditAuto2.Properties.RadioGroupIndex = 1;
			this.checkEditAuto2.Size = new System.Drawing.Size(20, 21);
			this.checkEditAuto2.StyleController = this.styleController;
			this.checkEditAuto2.TabIndex = 1;
			this.checkEditAuto2.TabStop = false;
			this.checkEditAuto2.CheckedChanged += new System.EventHandler(this.checkEditCase_CheckedChanged);
			// 
			// checkEditAuto1
			// 
			this.checkEditAuto1.Location = new System.Drawing.Point(10, 15);
			this.checkEditAuto1.Name = "checkEditAuto1";
			this.checkEditAuto1.Properties.Caption = "checkEdit1";
			this.checkEditAuto1.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditAuto1.Properties.RadioGroupIndex = 1;
			this.checkEditAuto1.Size = new System.Drawing.Size(20, 21);
			this.checkEditAuto1.StyleController = this.styleController;
			this.checkEditAuto1.TabIndex = 0;
			this.checkEditAuto1.TabStop = false;
			this.checkEditAuto1.CheckedChanged += new System.EventHandler(this.checkEditCase_CheckedChanged);
			// 
			// checkEditApplyAll
			// 
			this.checkEditApplyAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkEditApplyAll.Enabled = false;
			this.checkEditApplyAll.Location = new System.Drawing.Point(9, 496);
			this.checkEditApplyAll.Name = "checkEditApplyAll";
			this.checkEditApplyAll.Properties.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditApplyAll.Properties.Appearance.ForeColor = System.Drawing.Color.White;
			this.checkEditApplyAll.Properties.Appearance.Options.UseFont = true;
			this.checkEditApplyAll.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditApplyAll.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditApplyAll.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditApplyAll.Properties.AutoWidth = true;
			this.checkEditApplyAll.Properties.Caption = "Apply this digital info to all schedule types";
			this.checkEditApplyAll.Size = new System.Drawing.Size(292, 22);
			this.checkEditApplyAll.TabIndex = 104;
			// 
			// checkEditOutputOnlyOnce
			// 
			this.checkEditOutputOnlyOnce.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.checkEditOutputOnlyOnce.Enabled = false;
			this.checkEditOutputOnlyOnce.Location = new System.Drawing.Point(9, 469);
			this.checkEditOutputOnlyOnce.Name = "checkEditOutputOnlyOnce";
			this.checkEditOutputOnlyOnce.Properties.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditOutputOnlyOnce.Properties.Appearance.ForeColor = System.Drawing.Color.White;
			this.checkEditOutputOnlyOnce.Properties.Appearance.Options.UseFont = true;
			this.checkEditOutputOnlyOnce.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditOutputOnlyOnce.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditOutputOnlyOnce.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditOutputOnlyOnce.Properties.AutoWidth = true;
			this.checkEditOutputOnlyOnce.Properties.Caption = "Show digital info only on the 1st slide";
			this.checkEditOutputOnlyOnce.Size = new System.Drawing.Size(259, 22);
			this.checkEditOutputOnlyOnce.TabIndex = 106;
			// 
			// superTooltip
			// 
			this.superTooltip.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
			// 
			// FormDigital
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(593, 524);
			this.Controls.Add(this.pnControls);
			this.Controls.Add(this.checkEditOutputOnlyOnce);
			this.Controls.Add(this.checkEditApplyAll);
			this.Controls.Add(this.checkEditEnable);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXOK);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormDigital";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Show Digital Product Info";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormDigital_FormClosing);
			this.Load += new System.EventHandler(this.FormDigital_Load);
			((System.ComponentModel.ISupportInitialize)(this.memoEditManual.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditEnable.Properties)).EndInit();
			this.pnControls.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.memoEditAuto3.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditAuto2.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditAuto1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditManual.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAuto3.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAuto2.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAuto1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditApplyAll.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditOutputOnlyOnce.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private DevComponents.DotNetBar.ButtonX buttonXOK;
		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.MemoEdit memoEditManual;
		private DevExpress.XtraEditors.CheckEdit checkEditEnable;
		private System.Windows.Forms.Panel pnControls;
		private DevExpress.XtraEditors.CheckEdit checkEditApplyAll;
		private DevExpress.XtraEditors.CheckEdit checkEditOutputOnlyOnce;
		private DevComponents.DotNetBar.SuperTooltip superTooltip;
		private DevExpress.XtraEditors.CheckEdit checkEditManual;
		private DevExpress.XtraEditors.CheckEdit checkEditAuto3;
		private DevExpress.XtraEditors.CheckEdit checkEditAuto2;
		private DevExpress.XtraEditors.CheckEdit checkEditAuto1;
		private DevExpress.XtraEditors.MemoEdit memoEditAuto1;
		private DevExpress.XtraEditors.MemoEdit memoEditAuto3;
		private DevExpress.XtraEditors.MemoEdit memoEditAuto2;
	}
}