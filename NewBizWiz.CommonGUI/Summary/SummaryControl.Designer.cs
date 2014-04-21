namespace NewBizWiz.CommonGUI.Summary
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
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SummaryControl));
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.checkEditBusinessName = new DevExpress.XtraEditors.CheckEdit();
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageInput = new DevExpress.XtraTab.XtraTabPage();
			this.pnInputBorder = new System.Windows.Forms.Panel();
			this.xtraScrollableControlInput = new DevExpress.XtraEditors.XtraScrollableControl();
			this.pnInputSummary = new System.Windows.Forms.Panel();
			this.laFlightDates = new System.Windows.Forms.Label();
			this.laPresentationDate = new System.Windows.Forms.Label();
			this.checkEditTotalInvestment = new DevExpress.XtraEditors.CheckEdit();
			this.spinEditTotal = new DevExpress.XtraEditors.SpinEdit();
			this.checkEditPresentationDate = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditFlightDates = new DevExpress.XtraEditors.CheckEdit();
			this.spinEditMonthly = new DevExpress.XtraEditors.SpinEdit();
			this.checkEditMonthlyInvestment = new DevExpress.XtraEditors.CheckEdit();
			this.pnInputHeader = new System.Windows.Forms.Panel();
			this.labelControlInputTitle = new DevExpress.XtraEditors.LabelControl();
			this.pnInputFooter = new System.Windows.Forms.Panel();
			this.laTotalItems = new System.Windows.Forms.Label();
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
			this.SuspendLayout();
			// 
			// defaultLookAndFeel
			// 
			this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
			// 
			// labelControlTitle
			// 
			this.labelControlTitle.Appearance.Font = new System.Drawing.Font("Trebuchet MS", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlTitle.Location = new System.Drawing.Point(9, 9);
			this.labelControlTitle.Name = "labelControlTitle";
			this.labelControlTitle.Size = new System.Drawing.Size(533, 24);
			this.labelControlTitle.TabIndex = 0;
			this.labelControlTitle.Text = "Create a Simple Closing Summary Slide for your Presentation…";
			// 
			// checkEditBusinessName
			// 
			this.checkEditBusinessName.Location = new System.Drawing.Point(7, 48);
			this.checkEditBusinessName.Name = "checkEditBusinessName";
			this.checkEditBusinessName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditBusinessName.Properties.Appearance.Options.UseFont = true;
			this.checkEditBusinessName.Properties.AutoWidth = true;
			this.checkEditBusinessName.Properties.Caption = "Business Name: ";
			this.checkEditBusinessName.Size = new System.Drawing.Size(123, 21);
			this.checkEditBusinessName.TabIndex = 1;
			this.checkEditBusinessName.CheckedChanged += new System.EventHandler(this.checkEdit_CheckedChanged);
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
			this.xtraTabControl.Location = new System.Drawing.Point(0, 75);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.SelectedTabPage = this.xtraTabPageInput;
			this.xtraTabControl.Size = new System.Drawing.Size(737, 482);
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
			this.xtraTabPageInput.Size = new System.Drawing.Size(735, 456);
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
			this.pnInputBorder.Size = new System.Drawing.Size(505, 380);
			this.pnInputBorder.TabIndex = 2;
			// 
			// xtraScrollableControlInput
			// 
			this.xtraScrollableControlInput.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.xtraScrollableControlInput.Appearance.Options.UseBackColor = true;
			this.xtraScrollableControlInput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraScrollableControlInput.Location = new System.Drawing.Point(2, 2);
			this.xtraScrollableControlInput.Name = "xtraScrollableControlInput";
			this.xtraScrollableControlInput.Size = new System.Drawing.Size(501, 376);
			this.xtraScrollableControlInput.TabIndex = 0;
			// 
			// pnInputSummary
			// 
			this.pnInputSummary.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
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
			this.pnInputSummary.Size = new System.Drawing.Size(230, 380);
			this.pnInputSummary.TabIndex = 1;
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
			this.laPresentationDate.Location = new System.Drawing.Point(24, 104);
			this.laPresentationDate.Name = "laPresentationDate";
			this.laPresentationDate.Size = new System.Drawing.Size(134, 22);
			this.laPresentationDate.TabIndex = 119;
			this.laPresentationDate.Text = "$Tag";
			this.laPresentationDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkEditTotalInvestment
			// 
			this.checkEditTotalInvestment.Location = new System.Drawing.Point(6, 237);
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
			this.checkEditTotalInvestment.CheckedChanged += new System.EventHandler(this.checkEdit_CheckedChanged);
			// 
			// spinEditTotal
			// 
			this.spinEditTotal.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spinEditTotal.Enabled = false;
			this.spinEditTotal.Location = new System.Drawing.Point(28, 264);
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
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("spinEditTotal.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject6, "", null, null, true)});
			this.spinEditTotal.Properties.DisplayFormat.FormatString = "$#,###.00";
			this.spinEditTotal.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.spinEditTotal.Properties.EditFormat.FormatString = "$#,###.00";
			this.spinEditTotal.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.spinEditTotal.Size = new System.Drawing.Size(136, 30);
			this.spinEditTotal.TabIndex = 116;
			this.spinEditTotal.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.spinEditTotal_ButtonClick);
			this.spinEditTotal.EditValueChanged += new System.EventHandler(this.spinEditTotal_EditValueChanged);
			// 
			// checkEditPresentationDate
			// 
			this.checkEditPresentationDate.Location = new System.Drawing.Point(6, 80);
			this.checkEditPresentationDate.Name = "checkEditPresentationDate";
			this.checkEditPresentationDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditPresentationDate.Properties.Appearance.Options.UseFont = true;
			this.checkEditPresentationDate.Properties.AutoWidth = true;
			this.checkEditPresentationDate.Properties.Caption = "Presentation Date: ";
			this.checkEditPresentationDate.Size = new System.Drawing.Size(135, 21);
			this.checkEditPresentationDate.TabIndex = 111;
			this.checkEditPresentationDate.CheckedChanged += new System.EventHandler(this.checkEdit_CheckedChanged);
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
			this.checkEditFlightDates.CheckedChanged += new System.EventHandler(this.checkEdit_CheckedChanged);
			// 
			// spinEditMonthly
			// 
			this.spinEditMonthly.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spinEditMonthly.Enabled = false;
			this.spinEditMonthly.Location = new System.Drawing.Point(28, 185);
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
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis, "", -1, true, false, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, null, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("spinEditMonthly.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
			this.spinEditMonthly.Properties.DisplayFormat.FormatString = "$#,###.00";
			this.spinEditMonthly.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.spinEditMonthly.Properties.EditFormat.FormatString = "$#,###.00";
			this.spinEditMonthly.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.spinEditMonthly.Size = new System.Drawing.Size(136, 30);
			this.spinEditMonthly.TabIndex = 115;
			this.spinEditMonthly.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.spinEditMonthly_ButtonClick);
			this.spinEditMonthly.EditValueChanged += new System.EventHandler(this.spinEditMonthly_EditValueChanged);
			// 
			// checkEditMonthlyInvestment
			// 
			this.checkEditMonthlyInvestment.Location = new System.Drawing.Point(6, 158);
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
			this.checkEditMonthlyInvestment.CheckedChanged += new System.EventHandler(this.checkEdit_CheckedChanged);
			// 
			// pnInputHeader
			// 
			this.pnInputHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.pnInputHeader.Controls.Add(this.labelControlInputTitle);
			this.pnInputHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnInputHeader.Location = new System.Drawing.Point(0, 0);
			this.pnInputHeader.Name = "pnInputHeader";
			this.pnInputHeader.Size = new System.Drawing.Size(735, 50);
			this.pnInputHeader.TabIndex = 0;
			// 
			// labelControlInputTitle
			// 
			this.labelControlInputTitle.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlInputTitle.Location = new System.Drawing.Point(8, 17);
			this.labelControlInputTitle.Name = "labelControlInputTitle";
			this.labelControlInputTitle.Size = new System.Drawing.Size(497, 16);
			this.labelControlInputTitle.TabIndex = 0;
			this.labelControlInputTitle.Text = "This slide can help you summarize the different media components of this campaign" +
    "...";
			// 
			// pnInputFooter
			// 
			this.pnInputFooter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.pnInputFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnInputFooter.Location = new System.Drawing.Point(0, 430);
			this.pnInputFooter.Name = "pnInputFooter";
			this.pnInputFooter.Size = new System.Drawing.Size(735, 26);
			this.pnInputFooter.TabIndex = 3;
			// 
			// laTotalItems
			// 
			this.laTotalItems.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.laTotalItems.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalItems.Location = new System.Drawing.Point(530, 47);
			this.laTotalItems.Name = "laTotalItems";
			this.laTotalItems.Size = new System.Drawing.Size(200, 22);
			this.laTotalItems.TabIndex = 70;
			this.laTotalItems.Text = "Total Items: ";
			this.laTotalItems.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// SummaryControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.laTotalItems);
			this.Controls.Add(this.xtraTabControl);
			this.Controls.Add(this.checkEditBusinessName);
			this.Controls.Add(this.labelControlTitle);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "SummaryControl";
			this.Size = new System.Drawing.Size(737, 557);
			((System.ComponentModel.ISupportInitialize)(this.checkEditBusinessName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.xtraTabControl.ResumeLayout(false);
			this.xtraTabPageInput.ResumeLayout(false);
			this.pnInputBorder.ResumeLayout(false);
			this.pnInputSummary.ResumeLayout(false);
			this.pnInputSummary.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditTotalInvestment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditTotal.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditPresentationDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditFlightDates.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditMonthly.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditMonthlyInvestment.Properties)).EndInit();
			this.pnInputHeader.ResumeLayout(false);
			this.pnInputHeader.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

		private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
	    protected DevExpress.XtraEditors.CheckEdit checkEditBusinessName;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageInput;
		private System.Windows.Forms.Panel pnInputSummary;
		private System.Windows.Forms.Panel pnInputHeader;
		private System.Windows.Forms.Panel pnInputBorder;
		private DevExpress.XtraEditors.XtraScrollableControl xtraScrollableControlInput;
		private DevExpress.XtraEditors.LabelControl labelControlInputTitle;
		private System.Windows.Forms.Panel pnInputFooter;
		private System.Windows.Forms.Label laTotalItems;
	    protected System.Windows.Forms.Label laFlightDates;
	    protected System.Windows.Forms.Label laPresentationDate;
		private DevExpress.XtraEditors.CheckEdit checkEditTotalInvestment;
		private DevExpress.XtraEditors.SpinEdit spinEditTotal;
		private DevExpress.XtraEditors.CheckEdit checkEditPresentationDate;
		private DevExpress.XtraEditors.CheckEdit checkEditFlightDates;
		private DevExpress.XtraEditors.SpinEdit spinEditMonthly;
		private DevExpress.XtraEditors.CheckEdit checkEditMonthlyInvestment;

    }
}
