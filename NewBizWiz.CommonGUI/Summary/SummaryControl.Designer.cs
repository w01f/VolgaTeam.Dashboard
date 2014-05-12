﻿namespace NewBizWiz.CommonGUI.Summary
{
	partial class SummaryControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SummaryControl));
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
			this.checkEditBusinessName = new DevExpress.XtraEditors.CheckEdit();
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageInput = new DevExpress.XtraTab.XtraTabPage();
			this.pnInputBorder = new System.Windows.Forms.Panel();
			this.xtraScrollableControlInput = new DevExpress.XtraEditors.XtraScrollableControl();
			this.pnInputSummary = new System.Windows.Forms.Panel();
			this.buttonXAddItem = new DevComponents.DotNetBar.ButtonX();
			this.laFlightDates = new System.Windows.Forms.Label();
			this.laPresentationDate = new System.Windows.Forms.Label();
			this.checkEditTotalInvestment = new DevExpress.XtraEditors.CheckEdit();
			this.spinEditTotal = new DevExpress.XtraEditors.SpinEdit();
			this.checkEditPresentationDate = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditFlightDates = new DevExpress.XtraEditors.CheckEdit();
			this.spinEditMonthly = new DevExpress.XtraEditors.SpinEdit();
			this.checkEditMonthlyInvestment = new DevExpress.XtraEditors.CheckEdit();
			this.pnInputHeader = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.comboBoxEditHeader = new DevExpress.XtraEditors.ComboBoxEdit();
			this.laTotalItems = new System.Windows.Forms.Label();
			this.pnInputFooter = new System.Windows.Forms.Panel();
			this.checkEditDecisionMaker = new DevExpress.XtraEditors.CheckEdit();
			this.laSlideCount = new System.Windows.Forms.Label();
			this.checkEditTableOutput = new DevExpress.XtraEditors.CheckEdit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditBusinessName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.xtraTabControl.SuspendLayout();
			this.xtraTabPageInput.SuspendLayout();
			this.pnInputBorder.SuspendLayout();
			this.pnInputSummary.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditTotalInvestment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditTotal.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditPresentationDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditFlightDates.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditMonthly.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditMonthlyInvestment.Properties)).BeginInit();
			this.pnInputHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditHeader.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditDecisionMaker.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditTableOutput.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
			// 
			// checkEditBusinessName
			// 
			this.checkEditBusinessName.Location = new System.Drawing.Point(7, 16);
			this.checkEditBusinessName.Name = "checkEditBusinessName";
			this.checkEditBusinessName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditBusinessName.Properties.Appearance.Options.UseFont = true;
			this.checkEditBusinessName.Properties.AutoWidth = true;
			this.checkEditBusinessName.Properties.Caption = "Business Name: ";
			this.checkEditBusinessName.Size = new System.Drawing.Size(123, 21);
			this.checkEditBusinessName.TabIndex = 1;
			// 
			// xtraTabControl
			// 
			this.xtraTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.xtraTabControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.Appearance.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControl.Location = new System.Drawing.Point(0, 50);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.SelectedTabPage = this.xtraTabPageInput;
			this.xtraTabControl.Size = new System.Drawing.Size(737, 507);
			this.xtraTabControl.TabIndex = 5;
			this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageInput});
			// 
			// xtraTabPageInput
			// 
			this.xtraTabPageInput.Controls.Add(this.pnInputBorder);
			this.xtraTabPageInput.Controls.Add(this.pnInputSummary);
			this.xtraTabPageInput.Controls.Add(this.pnInputHeader);
			this.xtraTabPageInput.Controls.Add(this.pnInputFooter);
			this.xtraTabPageInput.Name = "xtraTabPageInput";
			this.xtraTabPageInput.Size = new System.Drawing.Size(735, 481);
			this.xtraTabPageInput.Text = "What Are you Selling?";
			// 
			// pnInputBorder
			// 
			this.pnInputBorder.BackColor = System.Drawing.Color.White;
			this.pnInputBorder.Controls.Add(this.xtraScrollableControlInput);
			this.pnInputBorder.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnInputBorder.Location = new System.Drawing.Point(0, 50);
			this.pnInputBorder.Name = "pnInputBorder";
			this.pnInputBorder.Padding = new System.Windows.Forms.Padding(2);
			this.pnInputBorder.Size = new System.Drawing.Size(505, 405);
			this.pnInputBorder.TabIndex = 2;
			// 
			// xtraScrollableControlInput
			// 
			this.xtraScrollableControlInput.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.xtraScrollableControlInput.Appearance.Options.UseBackColor = true;
			this.xtraScrollableControlInput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraScrollableControlInput.Location = new System.Drawing.Point(2, 2);
			this.xtraScrollableControlInput.Name = "xtraScrollableControlInput";
			this.xtraScrollableControlInput.Size = new System.Drawing.Size(501, 401);
			this.xtraScrollableControlInput.TabIndex = 0;
			// 
			// pnInputSummary
			// 
			this.pnInputSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.pnInputSummary.Controls.Add(this.buttonXAddItem);
			this.pnInputSummary.Controls.Add(this.laFlightDates);
			this.pnInputSummary.Controls.Add(this.laPresentationDate);
			this.pnInputSummary.Controls.Add(this.checkEditTotalInvestment);
			this.pnInputSummary.Controls.Add(this.spinEditTotal);
			this.pnInputSummary.Controls.Add(this.checkEditPresentationDate);
			this.pnInputSummary.Controls.Add(this.checkEditFlightDates);
			this.pnInputSummary.Controls.Add(this.spinEditMonthly);
			this.pnInputSummary.Controls.Add(this.checkEditMonthlyInvestment);
			this.pnInputSummary.Dock = System.Windows.Forms.DockStyle.Right;
			this.pnInputSummary.Location = new System.Drawing.Point(505, 50);
			this.pnInputSummary.Name = "pnInputSummary";
			this.pnInputSummary.Size = new System.Drawing.Size(230, 405);
			this.pnInputSummary.TabIndex = 1;
			// 
			// buttonXAddItem
			// 
			this.buttonXAddItem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXAddItem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXAddItem.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXAddItem.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXAddItem.Location = new System.Drawing.Point(8, 365);
			this.buttonXAddItem.Name = "buttonXAddItem";
			this.buttonXAddItem.Size = new System.Drawing.Size(215, 40);
			this.buttonXAddItem.TabIndex = 120;
			this.buttonXAddItem.TabStop = false;
			this.buttonXAddItem.Text = "Add an Item";
			this.buttonXAddItem.TextColor = System.Drawing.Color.Black;
			// 
			// laFlightDates
			// 
			this.laFlightDates.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laFlightDates.Location = new System.Drawing.Point(24, 30);
			this.laFlightDates.Name = "laFlightDates";
			this.laFlightDates.Size = new System.Drawing.Size(137, 21);
			this.laFlightDates.TabIndex = 118;
			this.laFlightDates.Text = "Start-End Date Tag";
			// 
			// laPresentationDate
			// 
			this.laPresentationDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laPresentationDate.Location = new System.Drawing.Point(24, 95);
			this.laPresentationDate.Name = "laPresentationDate";
			this.laPresentationDate.Size = new System.Drawing.Size(134, 22);
			this.laPresentationDate.TabIndex = 119;
			this.laPresentationDate.Text = "$Tag";
			this.laPresentationDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkEditTotalInvestment
			// 
			this.checkEditTotalInvestment.Location = new System.Drawing.Point(6, 223);
			this.checkEditTotalInvestment.Name = "checkEditTotalInvestment";
			this.checkEditTotalInvestment.Properties.AllowFocused = false;
			this.checkEditTotalInvestment.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditTotalInvestment.Properties.Appearance.ForeColor = System.Drawing.Color.Gray;
			this.checkEditTotalInvestment.Properties.Appearance.Options.UseFont = true;
			this.checkEditTotalInvestment.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditTotalInvestment.Properties.AutoWidth = true;
			this.checkEditTotalInvestment.Properties.Caption = "Total Investment:";
			this.checkEditTotalInvestment.Size = new System.Drawing.Size(121, 21);
			this.checkEditTotalInvestment.TabIndex = 117;
			// 
			// spinEditTotal
			// 
			this.spinEditTotal.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spinEditTotal.Enabled = false;
			this.spinEditTotal.Location = new System.Drawing.Point(28, 250);
			this.spinEditTotal.Name = "spinEditTotal";
			this.spinEditTotal.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
			this.spinEditTotal.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.spinEditTotal.Properties.Appearance.Options.UseFont = true;
			this.spinEditTotal.Properties.Appearance.Options.UseForeColor = true;
			this.spinEditTotal.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
			this.spinEditTotal.Properties.AppearanceDisabled.Options.UseForeColor = true;
			this.spinEditTotal.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black;
			this.spinEditTotal.Properties.AppearanceFocused.Options.UseForeColor = true;
			this.spinEditTotal.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
			this.spinEditTotal.Properties.AppearanceReadOnly.Options.UseForeColor = true;
			this.spinEditTotal.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("spinEditTotal.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
			this.spinEditTotal.Properties.DisplayFormat.FormatString = "$#,###.00";
			this.spinEditTotal.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.spinEditTotal.Properties.EditFormat.FormatString = "$#,###.00";
			this.spinEditTotal.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.spinEditTotal.Size = new System.Drawing.Size(136, 30);
			this.spinEditTotal.TabIndex = 116;
			// 
			// checkEditPresentationDate
			// 
			this.checkEditPresentationDate.Location = new System.Drawing.Point(6, 71);
			this.checkEditPresentationDate.Name = "checkEditPresentationDate";
			this.checkEditPresentationDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditPresentationDate.Properties.Appearance.Options.UseFont = true;
			this.checkEditPresentationDate.Properties.AutoWidth = true;
			this.checkEditPresentationDate.Properties.Caption = "Presentation Date: ";
			this.checkEditPresentationDate.Size = new System.Drawing.Size(135, 21);
			this.checkEditPresentationDate.TabIndex = 111;
			// 
			// checkEditFlightDates
			// 
			this.checkEditFlightDates.Location = new System.Drawing.Point(6, 6);
			this.checkEditFlightDates.Name = "checkEditFlightDates";
			this.checkEditFlightDates.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditFlightDates.Properties.Appearance.Options.UseFont = true;
			this.checkEditFlightDates.Properties.AutoWidth = true;
			this.checkEditFlightDates.Properties.Caption = "Campaign Dates: ";
			this.checkEditFlightDates.Size = new System.Drawing.Size(127, 21);
			this.checkEditFlightDates.TabIndex = 112;
			// 
			// spinEditMonthly
			// 
			this.spinEditMonthly.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spinEditMonthly.Enabled = false;
			this.spinEditMonthly.Location = new System.Drawing.Point(28, 171);
			this.spinEditMonthly.Name = "spinEditMonthly";
			this.spinEditMonthly.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9F);
			this.spinEditMonthly.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.spinEditMonthly.Properties.Appearance.Options.UseFont = true;
			this.spinEditMonthly.Properties.Appearance.Options.UseForeColor = true;
			this.spinEditMonthly.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
			this.spinEditMonthly.Properties.AppearanceDisabled.Options.UseForeColor = true;
			this.spinEditMonthly.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black;
			this.spinEditMonthly.Properties.AppearanceFocused.Options.UseForeColor = true;
			this.spinEditMonthly.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
			this.spinEditMonthly.Properties.AppearanceReadOnly.Options.UseForeColor = true;
			this.spinEditMonthly.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("spinEditMonthly.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject4, "", null, null, true)});
			this.spinEditMonthly.Properties.DisplayFormat.FormatString = "$#,###.00";
			this.spinEditMonthly.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.spinEditMonthly.Properties.EditFormat.FormatString = "$#,###.00";
			this.spinEditMonthly.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.spinEditMonthly.Size = new System.Drawing.Size(136, 30);
			this.spinEditMonthly.TabIndex = 115;
			// 
			// checkEditMonthlyInvestment
			// 
			this.checkEditMonthlyInvestment.Location = new System.Drawing.Point(6, 144);
			this.checkEditMonthlyInvestment.Name = "checkEditMonthlyInvestment";
			this.checkEditMonthlyInvestment.Properties.AllowFocused = false;
			this.checkEditMonthlyInvestment.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditMonthlyInvestment.Properties.Appearance.ForeColor = System.Drawing.Color.Gray;
			this.checkEditMonthlyInvestment.Properties.Appearance.Options.UseFont = true;
			this.checkEditMonthlyInvestment.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditMonthlyInvestment.Properties.AutoWidth = true;
			this.checkEditMonthlyInvestment.Properties.Caption = "Monthly Investment:";
			this.checkEditMonthlyInvestment.Size = new System.Drawing.Size(139, 21);
			this.checkEditMonthlyInvestment.TabIndex = 114;
			// 
			// pnInputHeader
			// 
			this.pnInputHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.pnInputHeader.Controls.Add(this.label1);
			this.pnInputHeader.Controls.Add(this.comboBoxEditHeader);
			this.pnInputHeader.Controls.Add(this.laTotalItems);
			this.pnInputHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnInputHeader.Location = new System.Drawing.Point(0, 0);
			this.pnInputHeader.Name = "pnInputHeader";
			this.pnInputHeader.Size = new System.Drawing.Size(735, 50);
			this.pnInputHeader.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(315, 17);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(73, 16);
			this.label1.TabIndex = 72;
			this.label1.Text = "(Slide Title)";
			// 
			// comboBoxEditHeader
			// 
			this.comboBoxEditHeader.Location = new System.Drawing.Point(8, 14);
			this.comboBoxEditHeader.Name = "comboBoxEditHeader";
			this.comboBoxEditHeader.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comboBoxEditHeader.Properties.Appearance.Options.UseFont = true;
			this.comboBoxEditHeader.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditHeader.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditHeader.Size = new System.Drawing.Size(301, 22);
			this.comboBoxEditHeader.TabIndex = 71;
			// 
			// laTotalItems
			// 
			this.laTotalItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.laTotalItems.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalItems.Location = new System.Drawing.Point(513, 13);
			this.laTotalItems.Name = "laTotalItems";
			this.laTotalItems.Size = new System.Drawing.Size(215, 22);
			this.laTotalItems.TabIndex = 70;
			this.laTotalItems.Text = "Total Items: ";
			this.laTotalItems.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pnInputFooter
			// 
			this.pnInputFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.pnInputFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnInputFooter.Location = new System.Drawing.Point(0, 455);
			this.pnInputFooter.Name = "pnInputFooter";
			this.pnInputFooter.Size = new System.Drawing.Size(735, 26);
			this.pnInputFooter.TabIndex = 3;
			// 
			// checkEditDecisionMaker
			// 
			this.checkEditDecisionMaker.Location = new System.Drawing.Point(189, 16);
			this.checkEditDecisionMaker.Name = "checkEditDecisionMaker";
			this.checkEditDecisionMaker.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditDecisionMaker.Properties.Appearance.Options.UseFont = true;
			this.checkEditDecisionMaker.Properties.AutoWidth = true;
			this.checkEditDecisionMaker.Properties.Caption = "Decision Maker: ";
			this.checkEditDecisionMaker.Size = new System.Drawing.Size(121, 21);
			this.checkEditDecisionMaker.TabIndex = 6;
			// 
			// laSlideCount
			// 
			this.laSlideCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.laSlideCount.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laSlideCount.Location = new System.Drawing.Point(511, 5);
			this.laSlideCount.Name = "laSlideCount";
			this.laSlideCount.Size = new System.Drawing.Size(223, 21);
			this.laSlideCount.TabIndex = 119;
			this.laSlideCount.Text = "Estimated Slide Count:";
			this.laSlideCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// checkEditTableOutput
			// 
			this.checkEditTableOutput.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditTableOutput.Location = new System.Drawing.Point(560, 29);
			this.checkEditTableOutput.Name = "checkEditTableOutput";
			this.checkEditTableOutput.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditTableOutput.Properties.Appearance.Options.UseFont = true;
			this.checkEditTableOutput.Properties.AutoWidth = true;
			this.checkEditTableOutput.Properties.Caption = "Output 1 Slide Table Grid";
			this.checkEditTableOutput.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.checkEditTableOutput.Size = new System.Drawing.Size(169, 21);
			this.checkEditTableOutput.TabIndex = 120;
			// 
			// SummaryControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.checkEditTableOutput);
			this.Controls.Add(this.laSlideCount);
			this.Controls.Add(this.checkEditDecisionMaker);
			this.Controls.Add(this.xtraTabControl);
			this.Controls.Add(this.checkEditBusinessName);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "SummaryControl";
			this.Size = new System.Drawing.Size(737, 557);
			((System.ComponentModel.ISupportInitialize)(this.checkEditBusinessName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.xtraTabControl.ResumeLayout(false);
			this.xtraTabPageInput.ResumeLayout(false);
			this.pnInputBorder.ResumeLayout(false);
			this.pnInputSummary.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkEditTotalInvestment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditTotal.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditPresentationDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditFlightDates.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditMonthly.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditMonthlyInvestment.Properties)).EndInit();
			this.pnInputHeader.ResumeLayout(false);
			this.pnInputHeader.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditHeader.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditDecisionMaker.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditTableOutput.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		protected DevExpress.XtraEditors.CheckEdit checkEditBusinessName;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageInput;
		private System.Windows.Forms.Panel pnInputSummary;
		private System.Windows.Forms.Panel pnInputHeader;
		private System.Windows.Forms.Panel pnInputBorder;
		protected DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControlInput;
		private System.Windows.Forms.Panel pnInputFooter;
		protected System.Windows.Forms.Label laTotalItems;
		protected System.Windows.Forms.Label laFlightDates;
		protected System.Windows.Forms.Label laPresentationDate;
		protected DevExpress.XtraEditors.CheckEdit checkEditTotalInvestment;
		protected DevExpress.XtraEditors.SpinEdit spinEditTotal;
		protected DevExpress.XtraEditors.CheckEdit checkEditPresentationDate;
		protected DevExpress.XtraEditors.CheckEdit checkEditFlightDates;
		protected DevExpress.XtraEditors.SpinEdit spinEditMonthly;
		protected DevExpress.XtraEditors.CheckEdit checkEditMonthlyInvestment;
		protected DevComponents.DotNetBar.ButtonX buttonXAddItem;
		protected DevExpress.XtraEditors.CheckEdit checkEditDecisionMaker;
		protected System.Windows.Forms.Label laSlideCount;
		protected DevExpress.XtraEditors.ComboBoxEdit comboBoxEditHeader;
		private System.Windows.Forms.Label label1;
		protected DevExpress.XtraEditors.CheckEdit checkEditTableOutput;

	}
}
