namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	partial class DigitalSummaryEditorControl
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			this.xtraScrollableControl = new DevExpress.XtraEditors.XtraScrollableControl();
			this.pnSummaryExt = new System.Windows.Forms.Panel();
			this.pnSummaryInt = new System.Windows.Forms.Panel();
			this.spinEditTotalInvestment = new DevExpress.XtraEditors.SpinEdit();
			this.checkEditTotalInvestment = new DevExpress.XtraEditors.CheckEdit();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.spinEditMonthlyInvestment = new DevExpress.XtraEditors.SpinEdit();
			this.checkEditMonthlyInvestment = new DevExpress.XtraEditors.CheckEdit();
			this.labelControlInvestmentTitle = new DevExpress.XtraEditors.LabelControl();
			this.memoEditStatement = new DevExpress.XtraEditors.MemoEdit();
			this.checkEditStatement = new DevExpress.XtraEditors.CheckEdit();
			this.pnSummaryExt.SuspendLayout();
			this.pnSummaryInt.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.spinEditTotalInvestment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditTotalInvestment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditMonthlyInvestment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditMonthlyInvestment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditStatement.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditStatement.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// xtraScrollableControl
			// 
			this.xtraScrollableControl.Appearance.BackColor = System.Drawing.Color.White;
			this.xtraScrollableControl.Appearance.Options.UseBackColor = true;
			this.xtraScrollableControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraScrollableControl.Location = new System.Drawing.Point(0, 0);
			this.xtraScrollableControl.Name = "xtraScrollableControl";
			this.xtraScrollableControl.Size = new System.Drawing.Size(627, 341);
			this.xtraScrollableControl.TabIndex = 0;
			// 
			// pnSummaryExt
			// 
			this.pnSummaryExt.BackColor = System.Drawing.Color.LightGray;
			this.pnSummaryExt.Controls.Add(this.pnSummaryInt);
			this.pnSummaryExt.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnSummaryExt.Location = new System.Drawing.Point(0, 341);
			this.pnSummaryExt.Name = "pnSummaryExt";
			this.pnSummaryExt.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
			this.pnSummaryExt.Size = new System.Drawing.Size(627, 104);
			this.pnSummaryExt.TabIndex = 1;
			// 
			// pnSummaryInt
			// 
			this.pnSummaryInt.BackColor = System.Drawing.Color.White;
			this.pnSummaryInt.Controls.Add(this.spinEditTotalInvestment);
			this.pnSummaryInt.Controls.Add(this.checkEditTotalInvestment);
			this.pnSummaryInt.Controls.Add(this.spinEditMonthlyInvestment);
			this.pnSummaryInt.Controls.Add(this.checkEditMonthlyInvestment);
			this.pnSummaryInt.Controls.Add(this.labelControlInvestmentTitle);
			this.pnSummaryInt.Controls.Add(this.memoEditStatement);
			this.pnSummaryInt.Controls.Add(this.checkEditStatement);
			this.pnSummaryInt.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnSummaryInt.Location = new System.Drawing.Point(0, 2);
			this.pnSummaryInt.Name = "pnSummaryInt";
			this.pnSummaryInt.Size = new System.Drawing.Size(627, 102);
			this.pnSummaryInt.TabIndex = 2;
			// 
			// spinEditTotalInvestment
			// 
			this.spinEditTotalInvestment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.spinEditTotalInvestment.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spinEditTotalInvestment.Enabled = false;
			this.spinEditTotalInvestment.Location = new System.Drawing.Point(509, 56);
			this.spinEditTotalInvestment.Name = "spinEditTotalInvestment";
			this.spinEditTotalInvestment.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
			this.spinEditTotalInvestment.Properties.Appearance.Options.UseFont = true;
			this.spinEditTotalInvestment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
			this.spinEditTotalInvestment.Properties.DisplayFormat.FormatString = "$#,###.00";
			this.spinEditTotalInvestment.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.spinEditTotalInvestment.Properties.EditFormat.FormatString = "$#,###.00";
			this.spinEditTotalInvestment.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.spinEditTotalInvestment.Properties.NullText = "N/A";
			this.spinEditTotalInvestment.Size = new System.Drawing.Size(106, 21);
			this.spinEditTotalInvestment.TabIndex = 6;
			// 
			// checkEditTotalInvestment
			// 
			this.checkEditTotalInvestment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditTotalInvestment.Location = new System.Drawing.Point(423, 56);
			this.checkEditTotalInvestment.Name = "checkEditTotalInvestment";
			this.checkEditTotalInvestment.Properties.Caption = "Total:";
			this.checkEditTotalInvestment.Size = new System.Drawing.Size(80, 20);
			this.checkEditTotalInvestment.StyleController = this.styleController;
			this.checkEditTotalInvestment.TabIndex = 5;
			this.checkEditTotalInvestment.CheckedChanged += new System.EventHandler(this.checkEditTotalInvestment_CheckedChanged);
			// 
			// styleController
			// 
			this.styleController.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.styleController.Appearance.Options.UseFont = true;
			this.styleController.Appearance.Options.UseTextOptions = true;
			this.styleController.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.styleController.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDisabled.Options.UseFont = true;
			this.styleController.AppearanceDisabled.Options.UseTextOptions = true;
			this.styleController.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.styleController.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDown.Options.UseFont = true;
			this.styleController.AppearanceDropDown.Options.UseTextOptions = true;
			this.styleController.AppearanceDropDown.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.styleController.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceDropDownHeader.Options.UseFont = true;
			this.styleController.AppearanceDropDownHeader.Options.UseTextOptions = true;
			this.styleController.AppearanceDropDownHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.styleController.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceFocused.Options.UseFont = true;
			this.styleController.AppearanceFocused.Options.UseTextOptions = true;
			this.styleController.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.styleController.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.styleController.AppearanceReadOnly.Options.UseFont = true;
			this.styleController.AppearanceReadOnly.Options.UseTextOptions = true;
			this.styleController.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			// 
			// spinEditMonthlyInvestment
			// 
			this.spinEditMonthlyInvestment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.spinEditMonthlyInvestment.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spinEditMonthlyInvestment.Enabled = false;
			this.spinEditMonthlyInvestment.Location = new System.Drawing.Point(509, 13);
			this.spinEditMonthlyInvestment.Name = "spinEditMonthlyInvestment";
			this.spinEditMonthlyInvestment.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
			this.spinEditMonthlyInvestment.Properties.Appearance.Options.UseFont = true;
			this.spinEditMonthlyInvestment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
			this.spinEditMonthlyInvestment.Properties.DisplayFormat.FormatString = "$#,###.00";
			this.spinEditMonthlyInvestment.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.spinEditMonthlyInvestment.Properties.EditFormat.FormatString = "$#,###.00";
			this.spinEditMonthlyInvestment.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.spinEditMonthlyInvestment.Properties.NullText = "N/A";
			this.spinEditMonthlyInvestment.Size = new System.Drawing.Size(106, 22);
			this.spinEditMonthlyInvestment.TabIndex = 4;
			// 
			// checkEditMonthlyInvestment
			// 
			this.checkEditMonthlyInvestment.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditMonthlyInvestment.Location = new System.Drawing.Point(423, 13);
			this.checkEditMonthlyInvestment.Name = "checkEditMonthlyInvestment";
			this.checkEditMonthlyInvestment.Properties.Caption = "Monthly:";
			this.checkEditMonthlyInvestment.Size = new System.Drawing.Size(80, 20);
			this.checkEditMonthlyInvestment.StyleController = this.styleController;
			this.checkEditMonthlyInvestment.TabIndex = 3;
			this.checkEditMonthlyInvestment.CheckedChanged += new System.EventHandler(this.checkEditMonthlyInvestment_CheckedChanged);
			// 
			// labelControlInvestmentTitle
			// 
			this.labelControlInvestmentTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelControlInvestmentTitle.Location = new System.Drawing.Point(300, 15);
			this.labelControlInvestmentTitle.Name = "labelControlInvestmentTitle";
			this.labelControlInvestmentTitle.Size = new System.Drawing.Size(102, 16);
			this.labelControlInvestmentTitle.StyleController = this.styleController;
			this.labelControlInvestmentTitle.TabIndex = 2;
			this.labelControlInvestmentTitle.Text = "Digital Investment";
			// 
			// memoEditStatement
			// 
			this.memoEditStatement.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.memoEditStatement.Enabled = false;
			this.memoEditStatement.Location = new System.Drawing.Point(29, 15);
			this.memoEditStatement.Name = "memoEditStatement";
			this.memoEditStatement.Properties.NullText = "Closing Summary Statement";
			this.memoEditStatement.Size = new System.Drawing.Size(241, 63);
			this.memoEditStatement.StyleController = this.styleController;
			this.memoEditStatement.TabIndex = 1;
			this.memoEditStatement.EditValueChanged += new System.EventHandler(this.EditValueChanged);
			// 
			// checkEditStatement
			// 
			this.checkEditStatement.Location = new System.Drawing.Point(3, 13);
			this.checkEditStatement.Name = "checkEditStatement";
			this.checkEditStatement.Properties.AutoWidth = true;
			this.checkEditStatement.Properties.Caption = "";
			this.checkEditStatement.Size = new System.Drawing.Size(19, 19);
			this.checkEditStatement.StyleController = this.styleController;
			this.checkEditStatement.TabIndex = 0;
			this.checkEditStatement.CheckedChanged += new System.EventHandler(this.checkEditStatement_CheckedChanged);
			// 
			// DigitalSummaryControl
			// 
			this.Controls.Add(this.xtraScrollableControl);
			this.Controls.Add(this.pnSummaryExt);
			this.Name = "DigitalSummaryControl";
			this.Size = new System.Drawing.Size(627, 445);
			this.pnSummaryExt.ResumeLayout(false);
			this.pnSummaryInt.ResumeLayout(false);
			this.pnSummaryInt.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.spinEditTotalInvestment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditTotalInvestment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditMonthlyInvestment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditMonthlyInvestment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditStatement.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditStatement.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControl;
		private System.Windows.Forms.Panel pnSummaryExt;
		private System.Windows.Forms.Panel pnSummaryInt;
		private DevExpress.XtraEditors.CheckEdit checkEditMonthlyInvestment;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlInvestmentTitle;
		private DevExpress.XtraEditors.MemoEdit memoEditStatement;
		private DevExpress.XtraEditors.CheckEdit checkEditStatement;
		private DevExpress.XtraEditors.SpinEdit spinEditTotalInvestment;
		private DevExpress.XtraEditors.CheckEdit checkEditTotalInvestment;
		private DevExpress.XtraEditors.SpinEdit spinEditMonthlyInvestment;
	}
}
