﻿namespace Asa.Solutions.Dashboard.PresentationClasses.ContentEditors
{
	sealed partial class CoverControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CoverControl));
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			this.checkEditPresentationDate = new DevExpress.XtraEditors.CheckEdit();
			this.comboBoxEditDecisionMaker = new Asa.Common.GUI.Common.ComboBoxListEdit();
			this.comboBoxEditAdvertiser = new Asa.Common.GUI.Common.ComboBoxListEdit();
			this.buttonXSalesQuote = new DevComponents.DotNetBar.ButtonX();
			this.memoEditSalesQuote = new DevExpress.XtraEditors.MemoEdit();
			this.textEditSalesQuoteAuthor = new DevExpress.XtraEditors.TextEdit();
			this.laAdvertiser = new System.Windows.Forms.Label();
			this.pbSalesRep = new System.Windows.Forms.PictureBox();
			this.dateEditPresentationDate = new DevExpress.XtraEditors.DateEdit();
			this.laDecisionMaker = new System.Windows.Forms.Label();
			this.pbPresentationDate = new System.Windows.Forms.PictureBox();
			this.pbDecisionMaker = new System.Windows.Forms.PictureBox();
			this.pbAdvertiser = new System.Windows.Forms.PictureBox();
			this.checkEditSalesRep = new DevExpress.XtraEditors.CheckEdit();
			this.comboBoxEditSalesRep = new DevExpress.XtraEditors.ComboBoxEdit();
			this.laSalesRepEmail = new System.Windows.Forms.Label();
			this.laSalesQuotesHint = new System.Windows.Forms.Label();
			this.laSalesRepPhone = new System.Windows.Forms.Label();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.checkEditAddAsPageOne = new DevExpress.XtraEditors.CheckEdit();
			this.pnMain.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).BeginInit();
			this.pnTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbSplash)).BeginInit();
			this.pnSplash.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditPresentationDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDecisionMaker.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditAdvertiser.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditSalesQuote.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditSalesQuoteAuthor.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSalesRep)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditPresentationDate.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditPresentationDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbPresentationDate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbDecisionMaker)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbAdvertiser)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditSalesRep.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSalesRep.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAddAsPageOne.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.Controls.Add(this.laSalesRepPhone);
			this.pnMain.Controls.Add(this.laSalesRepEmail);
			this.pnMain.Controls.Add(this.comboBoxEditSalesRep);
			this.pnMain.Controls.Add(this.pbAdvertiser);
			this.pnMain.Controls.Add(this.checkEditSalesRep);
			this.pnMain.Controls.Add(this.checkEditPresentationDate);
			this.pnMain.Controls.Add(this.pbDecisionMaker);
			this.pnMain.Controls.Add(this.pbPresentationDate);
			this.pnMain.Controls.Add(this.laDecisionMaker);
			this.pnMain.Controls.Add(this.comboBoxEditDecisionMaker);
			this.pnMain.Controls.Add(this.dateEditPresentationDate);
			this.pnMain.Controls.Add(this.comboBoxEditAdvertiser);
			this.pnMain.Controls.Add(this.pbSalesRep);
			this.pnMain.Controls.Add(this.buttonXSalesQuote);
			this.pnMain.Controls.Add(this.laAdvertiser);
			this.pnMain.Controls.Add(this.memoEditSalesQuote);
			this.pnMain.Controls.Add(this.textEditSalesQuoteAuthor);
			this.pnMain.Controls.Add(this.laSalesQuotesHint);
			// 
			// comboBoxEditSlideHeader
			// 
			this.comboBoxEditSlideHeader.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comboBoxEditSlideHeader.Properties.Appearance.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceDisabled.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceDropDown.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceFocused.Options.UseFont = true;
			this.comboBoxEditSlideHeader.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSlideHeader.Properties.AppearanceReadOnly.Options.UseFont = true;
			this.comboBoxEditSlideHeader.EditValueChanged += new System.EventHandler(this.EditValueChanged);
			// 
			// pnTop
			// 
			this.pnTop.Controls.Add(this.checkEditAddAsPageOne);
			this.pnTop.Controls.SetChildIndex(this.comboBoxEditSlideHeader, 0);
			this.pnTop.Controls.SetChildIndex(this.checkEditAddAsPageOne, 0);
			// 
			// checkEditPresentationDate
			// 
			this.checkEditPresentationDate.Location = new System.Drawing.Point(510, 30);
			this.checkEditPresentationDate.Name = "checkEditPresentationDate";
			this.checkEditPresentationDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 12F);
			this.checkEditPresentationDate.Properties.Appearance.Options.UseFont = true;
			this.checkEditPresentationDate.Properties.Caption = "Presentation Date?";
			this.checkEditPresentationDate.Size = new System.Drawing.Size(165, 22);
			this.checkEditPresentationDate.TabIndex = 5;
			this.checkEditPresentationDate.CheckedChanged += new System.EventHandler(this.ckPresentationDate_CheckedChanged);
			// 
			// comboBoxEditDecisionMaker
			// 
			this.comboBoxEditDecisionMaker.Location = new System.Drawing.Point(70, 158);
			this.comboBoxEditDecisionMaker.Name = "comboBoxEditDecisionMaker";
			this.comboBoxEditDecisionMaker.OverrideTab = false;
			this.comboBoxEditDecisionMaker.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comboBoxEditDecisionMaker.Properties.Appearance.Options.UseFont = true;
			this.comboBoxEditDecisionMaker.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditDecisionMaker.Properties.AppearanceDisabled.Options.UseFont = true;
			this.comboBoxEditDecisionMaker.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditDecisionMaker.Properties.AppearanceDropDown.Options.UseFont = true;
			this.comboBoxEditDecisionMaker.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditDecisionMaker.Properties.AppearanceFocused.Options.UseFont = true;
			this.comboBoxEditDecisionMaker.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditDecisionMaker.Properties.AppearanceReadOnly.Options.UseFont = true;
			this.comboBoxEditDecisionMaker.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.comboBoxEditDecisionMaker.Properties.ListType = Asa.Common.GUI.Common.ListType.DecisionMakers;
			this.comboBoxEditDecisionMaker.Properties.NullText = "Type or Select";
			this.comboBoxEditDecisionMaker.Size = new System.Drawing.Size(344, 22);
			this.comboBoxEditDecisionMaker.TabIndex = 2;
			this.comboBoxEditDecisionMaker.EditValueChanged += new System.EventHandler(this.EditValueChanged);
			// 
			// comboBoxEditAdvertiser
			// 
			this.comboBoxEditAdvertiser.Location = new System.Drawing.Point(70, 61);
			this.comboBoxEditAdvertiser.Name = "comboBoxEditAdvertiser";
			this.comboBoxEditAdvertiser.OverrideTab = false;
			this.comboBoxEditAdvertiser.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comboBoxEditAdvertiser.Properties.Appearance.Options.UseFont = true;
			this.comboBoxEditAdvertiser.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditAdvertiser.Properties.AppearanceDisabled.Options.UseFont = true;
			this.comboBoxEditAdvertiser.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditAdvertiser.Properties.AppearanceDropDown.Options.UseFont = true;
			this.comboBoxEditAdvertiser.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditAdvertiser.Properties.AppearanceFocused.Options.UseFont = true;
			this.comboBoxEditAdvertiser.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditAdvertiser.Properties.AppearanceReadOnly.Options.UseFont = true;
			this.comboBoxEditAdvertiser.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.comboBoxEditAdvertiser.Properties.CaseSensitiveSearch = true;
			this.comboBoxEditAdvertiser.Properties.ListType = Asa.Common.GUI.Common.ListType.Advertisers;
			this.comboBoxEditAdvertiser.Properties.NullText = "Type or Select";
			this.comboBoxEditAdvertiser.Size = new System.Drawing.Size(344, 22);
			this.comboBoxEditAdvertiser.TabIndex = 1;
			this.comboBoxEditAdvertiser.EditValueChanged += new System.EventHandler(this.EditValueChanged);
			// 
			// buttonXSalesQuote
			// 
			this.buttonXSalesQuote.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSalesQuote.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSalesQuote.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonXSalesQuote.Image = global::Asa.Solutions.Dashboard.Properties.Resources.SalesQuotes;
			this.buttonXSalesQuote.Location = new System.Drawing.Point(11, 359);
			this.buttonXSalesQuote.Name = "buttonXSalesQuote";
			this.buttonXSalesQuote.Size = new System.Drawing.Size(181, 48);
			this.buttonXSalesQuote.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSalesQuote.TabIndex = 91;
			this.buttonXSalesQuote.TabStop = false;
			this.buttonXSalesQuote.TextColor = System.Drawing.Color.Black;
			this.buttonXSalesQuote.Click += new System.EventHandler(this.buttonXSalesQuote_Click);
			// 
			// memoEditSalesQuote
			// 
			this.memoEditSalesQuote.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.memoEditSalesQuote.EditValue = "";
			this.memoEditSalesQuote.Enabled = false;
			this.memoEditSalesQuote.Location = new System.Drawing.Point(198, 389);
			this.memoEditSalesQuote.Name = "memoEditSalesQuote";
			this.memoEditSalesQuote.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.memoEditSalesQuote.Properties.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.memoEditSalesQuote.Properties.Appearance.ForeColor = System.Drawing.Color.Gray;
			this.memoEditSalesQuote.Properties.Appearance.Options.UseBackColor = true;
			this.memoEditSalesQuote.Properties.Appearance.Options.UseFont = true;
			this.memoEditSalesQuote.Properties.Appearance.Options.UseForeColor = true;
			this.memoEditSalesQuote.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White;
			this.memoEditSalesQuote.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Gray;
			this.memoEditSalesQuote.Properties.AppearanceDisabled.Options.UseBackColor = true;
			this.memoEditSalesQuote.Properties.AppearanceDisabled.Options.UseForeColor = true;
			this.memoEditSalesQuote.Properties.AppearanceFocused.BackColor = System.Drawing.Color.White;
			this.memoEditSalesQuote.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Gray;
			this.memoEditSalesQuote.Properties.AppearanceFocused.Options.UseBackColor = true;
			this.memoEditSalesQuote.Properties.AppearanceFocused.Options.UseForeColor = true;
			this.memoEditSalesQuote.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
			this.memoEditSalesQuote.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Gray;
			this.memoEditSalesQuote.Properties.AppearanceReadOnly.Options.UseBackColor = true;
			this.memoEditSalesQuote.Properties.AppearanceReadOnly.Options.UseForeColor = true;
			this.memoEditSalesQuote.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.memoEditSalesQuote.Properties.NullText = "You can add a creative “Sales Positioning Quote” to your cover slide and make a g" +
    "reat first impression with your client…";
			this.memoEditSalesQuote.Properties.ReadOnly = true;
			this.memoEditSalesQuote.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.memoEditSalesQuote.Size = new System.Drawing.Size(477, 58);
			this.memoEditSalesQuote.TabIndex = 90;
			this.memoEditSalesQuote.TabStop = false;
			this.memoEditSalesQuote.Visible = false;
			// 
			// textEditSalesQuoteAuthor
			// 
			this.textEditSalesQuoteAuthor.EditValue = "";
			this.textEditSalesQuoteAuthor.Enabled = false;
			this.textEditSalesQuoteAuthor.Location = new System.Drawing.Point(198, 359);
			this.textEditSalesQuoteAuthor.Name = "textEditSalesQuoteAuthor";
			this.textEditSalesQuoteAuthor.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.textEditSalesQuoteAuthor.Properties.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.textEditSalesQuoteAuthor.Properties.Appearance.Options.UseBackColor = true;
			this.textEditSalesQuoteAuthor.Properties.Appearance.Options.UseFont = true;
			this.textEditSalesQuoteAuthor.Properties.AppearanceDisabled.BackColor = System.Drawing.Color.White;
			this.textEditSalesQuoteAuthor.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.Black;
			this.textEditSalesQuoteAuthor.Properties.AppearanceDisabled.Options.UseBackColor = true;
			this.textEditSalesQuoteAuthor.Properties.AppearanceDisabled.Options.UseForeColor = true;
			this.textEditSalesQuoteAuthor.Properties.AppearanceFocused.BackColor = System.Drawing.Color.White;
			this.textEditSalesQuoteAuthor.Properties.AppearanceFocused.ForeColor = System.Drawing.Color.Black;
			this.textEditSalesQuoteAuthor.Properties.AppearanceFocused.Options.UseBackColor = true;
			this.textEditSalesQuoteAuthor.Properties.AppearanceFocused.Options.UseForeColor = true;
			this.textEditSalesQuoteAuthor.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.White;
			this.textEditSalesQuoteAuthor.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.Black;
			this.textEditSalesQuoteAuthor.Properties.AppearanceReadOnly.Options.UseBackColor = true;
			this.textEditSalesQuoteAuthor.Properties.AppearanceReadOnly.Options.UseForeColor = true;
			this.textEditSalesQuoteAuthor.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.textEditSalesQuoteAuthor.Properties.NullText = "Sales Quotes";
			this.textEditSalesQuoteAuthor.Properties.ReadOnly = true;
			this.textEditSalesQuoteAuthor.Size = new System.Drawing.Size(477, 24);
			this.textEditSalesQuoteAuthor.TabIndex = 89;
			this.textEditSalesQuoteAuthor.TabStop = false;
			this.textEditSalesQuoteAuthor.Visible = false;
			// 
			// laAdvertiser
			// 
			this.laAdvertiser.BackColor = System.Drawing.Color.White;
			this.laAdvertiser.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAdvertiser.Location = new System.Drawing.Point(67, 27);
			this.laAdvertiser.Name = "laAdvertiser";
			this.laAdvertiser.Size = new System.Drawing.Size(261, 27);
			this.laAdvertiser.TabIndex = 83;
			this.laAdvertiser.Text = "Advertiser:";
			// 
			// pbSalesRep
			// 
			this.pbSalesRep.BackColor = System.Drawing.Color.White;
			this.pbSalesRep.Image = global::Asa.Solutions.Dashboard.Properties.Resources.SalesRep;
			this.pbSalesRep.Location = new System.Drawing.Point(11, 227);
			this.pbSalesRep.Name = "pbSalesRep";
			this.pbSalesRep.Size = new System.Drawing.Size(50, 51);
			this.pbSalesRep.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbSalesRep.TabIndex = 81;
			this.pbSalesRep.TabStop = false;
			// 
			// dateEditPresentationDate
			// 
			this.dateEditPresentationDate.EditValue = null;
			this.dateEditPresentationDate.Enabled = false;
			this.dateEditPresentationDate.Location = new System.Drawing.Point(511, 62);
			this.dateEditPresentationDate.Name = "dateEditPresentationDate";
			this.dateEditPresentationDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.dateEditPresentationDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.dateEditPresentationDate.Properties.Appearance.Options.UseFont = true;
			this.dateEditPresentationDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("dateEditPresentationDate.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
			this.dateEditPresentationDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.dateEditPresentationDate.Properties.DisplayFormat.FormatString = "MM/dd/yyyy";
			this.dateEditPresentationDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditPresentationDate.Properties.EditFormat.FormatString = "MM/dd/yyyy";
			this.dateEditPresentationDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditPresentationDate.Properties.FirstDayOfWeek = System.DayOfWeek.Monday;
			this.dateEditPresentationDate.Properties.Mask.EditMask = "MM/dd/yyyy";
			this.dateEditPresentationDate.Properties.NullText = "Select";
			this.dateEditPresentationDate.Properties.ShowPopupShadow = false;
			this.dateEditPresentationDate.Properties.ShowToday = false;
			this.dateEditPresentationDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.dateEditPresentationDate.Size = new System.Drawing.Size(164, 22);
			this.dateEditPresentationDate.TabIndex = 6;
			// 
			// laDecisionMaker
			// 
			this.laDecisionMaker.BackColor = System.Drawing.Color.White;
			this.laDecisionMaker.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laDecisionMaker.Location = new System.Drawing.Point(67, 129);
			this.laDecisionMaker.Name = "laDecisionMaker";
			this.laDecisionMaker.Size = new System.Drawing.Size(261, 27);
			this.laDecisionMaker.TabIndex = 74;
			this.laDecisionMaker.Text = "Decision-maker:";
			// 
			// pbPresentationDate
			// 
			this.pbPresentationDate.BackColor = System.Drawing.Color.White;
			this.pbPresentationDate.Image = global::Asa.Solutions.Dashboard.Properties.Resources.Date;
			this.pbPresentationDate.Location = new System.Drawing.Point(452, 32);
			this.pbPresentationDate.Name = "pbPresentationDate";
			this.pbPresentationDate.Size = new System.Drawing.Size(50, 51);
			this.pbPresentationDate.TabIndex = 77;
			this.pbPresentationDate.TabStop = false;
			// 
			// pbDecisionMaker
			// 
			this.pbDecisionMaker.BackColor = System.Drawing.Color.White;
			this.pbDecisionMaker.Image = global::Asa.Solutions.Dashboard.Properties.Resources.DecisionMaker;
			this.pbDecisionMaker.Location = new System.Drawing.Point(11, 129);
			this.pbDecisionMaker.Name = "pbDecisionMaker";
			this.pbDecisionMaker.Size = new System.Drawing.Size(50, 51);
			this.pbDecisionMaker.TabIndex = 75;
			this.pbDecisionMaker.TabStop = false;
			// 
			// pbAdvertiser
			// 
			this.pbAdvertiser.BackColor = System.Drawing.Color.White;
			this.pbAdvertiser.Image = global::Asa.Solutions.Dashboard.Properties.Resources.Advertiser;
			this.pbAdvertiser.Location = new System.Drawing.Point(11, 30);
			this.pbAdvertiser.Name = "pbAdvertiser";
			this.pbAdvertiser.Size = new System.Drawing.Size(50, 51);
			this.pbAdvertiser.TabIndex = 72;
			this.pbAdvertiser.TabStop = false;
			// 
			// checkEditSalesRep
			// 
			this.checkEditSalesRep.Location = new System.Drawing.Point(68, 225);
			this.checkEditSalesRep.Name = "checkEditSalesRep";
			this.checkEditSalesRep.Properties.Appearance.Font = new System.Drawing.Font("Arial", 12F);
			this.checkEditSalesRep.Properties.Appearance.Options.UseFont = true;
			this.checkEditSalesRep.Properties.Caption = "Sales Rep:";
			this.checkEditSalesRep.Size = new System.Drawing.Size(227, 22);
			this.checkEditSalesRep.TabIndex = 3;
			this.checkEditSalesRep.CheckedChanged += new System.EventHandler(this.checkEditSalesRep_CheckedChanged);
			// 
			// comboBoxEditSalesRep
			// 
			this.comboBoxEditSalesRep.Enabled = false;
			this.comboBoxEditSalesRep.Location = new System.Drawing.Point(70, 258);
			this.comboBoxEditSalesRep.Name = "comboBoxEditSalesRep";
			this.comboBoxEditSalesRep.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.comboBoxEditSalesRep.Properties.Appearance.Options.UseFont = true;
			this.comboBoxEditSalesRep.Properties.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSalesRep.Properties.AppearanceDisabled.Options.UseFont = true;
			this.comboBoxEditSalesRep.Properties.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSalesRep.Properties.AppearanceDropDown.Options.UseFont = true;
			this.comboBoxEditSalesRep.Properties.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSalesRep.Properties.AppearanceFocused.Options.UseFont = true;
			this.comboBoxEditSalesRep.Properties.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.comboBoxEditSalesRep.Properties.AppearanceReadOnly.Options.UseFont = true;
			this.comboBoxEditSalesRep.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditSalesRep.Properties.NullText = "Type or Select";
			this.comboBoxEditSalesRep.Size = new System.Drawing.Size(344, 22);
			this.comboBoxEditSalesRep.TabIndex = 4;
			this.comboBoxEditSalesRep.EditValueChanged += new System.EventHandler(this.comboBoxEditSalesRep_EditValueChanged);
			// 
			// laSalesRepEmail
			// 
			this.laSalesRepEmail.ForeColor = System.Drawing.Color.Black;
			this.laSalesRepEmail.Location = new System.Drawing.Point(73, 283);
			this.laSalesRepEmail.Name = "laSalesRepEmail";
			this.laSalesRepEmail.Size = new System.Drawing.Size(222, 21);
			this.laSalesRepEmail.TabIndex = 100;
			this.laSalesRepEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laSalesQuotesHint
			// 
			this.laSalesQuotesHint.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laSalesQuotesHint.Location = new System.Drawing.Point(198, 359);
			this.laSalesQuotesHint.Name = "laSalesQuotesHint";
			this.laSalesQuotesHint.Size = new System.Drawing.Size(477, 48);
			this.laSalesQuotesHint.TabIndex = 101;
			this.laSalesQuotesHint.Text = "Add “Words of Wisdom” to your cover page…";
			this.laSalesQuotesHint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laSalesRepPhone
			// 
			this.laSalesRepPhone.ForeColor = System.Drawing.Color.Black;
			this.laSalesRepPhone.Location = new System.Drawing.Point(301, 283);
			this.laSalesRepPhone.Name = "laSalesRepPhone";
			this.laSalesRepPhone.Size = new System.Drawing.Size(113, 21);
			this.laSalesRepPhone.TabIndex = 102;
			this.laSalesRepPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			// checkEditAddAsPageOne
			// 
			this.checkEditAddAsPageOne.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditAddAsPageOne.Location = new System.Drawing.Point(808, 15);
			this.checkEditAddAsPageOne.Name = "checkEditAddAsPageOne";
			this.checkEditAddAsPageOne.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditAddAsPageOne.Properties.Caption = "<color=gray>Always Output to Page 1</color>";
			this.checkEditAddAsPageOne.Size = new System.Drawing.Size(186, 20);
			this.checkEditAddAsPageOne.StyleController = this.styleController;
			this.checkEditAddAsPageOne.TabIndex = 29;
			this.checkEditAddAsPageOne.CheckedChanged += new System.EventHandler(this.EditValueChanged);
			// 
			// CoverControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Name = "CoverControl";
			this.pnMain.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).EndInit();
			this.pnTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbSplash)).EndInit();
			this.pnSplash.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.checkEditPresentationDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDecisionMaker.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditAdvertiser.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.memoEditSalesQuote.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.textEditSalesQuoteAuthor.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbSalesRep)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditPresentationDate.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditPresentationDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbPresentationDate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbDecisionMaker)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbAdvertiser)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditSalesRep.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSalesRep.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditAddAsPageOne.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbSalesRep;
        private DevExpress.XtraEditors.DateEdit dateEditPresentationDate;
        private System.Windows.Forms.Label laDecisionMaker;
        private System.Windows.Forms.PictureBox pbPresentationDate;
        private System.Windows.Forms.PictureBox pbDecisionMaker;
        private System.Windows.Forms.PictureBox pbAdvertiser;
        private System.Windows.Forms.Label laAdvertiser;
        private DevExpress.XtraEditors.TextEdit textEditSalesQuoteAuthor;
        private DevComponents.DotNetBar.ButtonX buttonXSalesQuote;
        private DevExpress.XtraEditors.MemoEdit memoEditSalesQuote;
		private Asa.Common.GUI.Common.ComboBoxListEdit comboBoxEditDecisionMaker;
		private Asa.Common.GUI.Common.ComboBoxListEdit comboBoxEditAdvertiser;
        private DevExpress.XtraEditors.CheckEdit checkEditPresentationDate;
		private DevExpress.XtraEditors.CheckEdit checkEditSalesRep;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSalesRep;
		private System.Windows.Forms.Label laSalesRepEmail;
		private System.Windows.Forms.Label laSalesQuotesHint;
		private System.Windows.Forms.Label laSalesRepPhone;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.CheckEdit checkEditAddAsPageOne;
	}
}