namespace Asa.OnlineSchedule.Controls.PresentationClasses
{
	partial class DigitalProductContainer
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
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.xtraTabControlProducts = new DevExpress.XtraTab.XtraTabControl();
			this.pnHeader = new System.Windows.Forms.Panel();
			this.labelControlCategory = new DevExpress.XtraEditors.LabelControl();
			this.checkEditShowFlightDates = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditWeeks = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditMonths = new DevExpress.XtraEditors.CheckEdit();
			this.spinEditDuration = new DevExpress.XtraEditors.SpinEdit();
			this.checkEditDuration = new DevExpress.XtraEditors.CheckEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlProducts)).BeginInit();
			this.pnHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowFlightDates.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditWeeks.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditMonths.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditDuration.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditDuration.Properties)).BeginInit();
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
			// xtraTabControlProducts
			// 
			this.xtraTabControlProducts.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlProducts.Appearance.Options.UseFont = true;
			this.xtraTabControlProducts.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlProducts.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlProducts.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlProducts.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlProducts.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlProducts.Location = new System.Drawing.Point(0, 41);
			this.xtraTabControlProducts.Name = "xtraTabControlProducts";
			this.xtraTabControlProducts.Size = new System.Drawing.Size(1000, 521);
			this.xtraTabControlProducts.TabIndex = 3;
			this.xtraTabControlProducts.MouseDown += new System.Windows.Forms.MouseEventHandler(this.xtraTabControlProducts_MouseDown);
			// 
			// pnHeader
			// 
			this.pnHeader.Controls.Add(this.labelControlCategory);
			this.pnHeader.Controls.Add(this.checkEditShowFlightDates);
			this.pnHeader.Controls.Add(this.checkEditWeeks);
			this.pnHeader.Controls.Add(this.checkEditMonths);
			this.pnHeader.Controls.Add(this.spinEditDuration);
			this.pnHeader.Controls.Add(this.checkEditDuration);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
			this.pnHeader.Size = new System.Drawing.Size(1000, 41);
			this.pnHeader.TabIndex = 4;
			// 
			// labelControlCategory
			// 
			this.labelControlCategory.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlCategory.Location = new System.Drawing.Point(13, 12);
			this.labelControlCategory.Name = "labelControlCategory";
			this.labelControlCategory.Size = new System.Drawing.Size(52, 16);
			this.labelControlCategory.TabIndex = 27;
			this.labelControlCategory.Text = "Category";
			// 
			// checkEditShowFlightDates
			// 
			this.checkEditShowFlightDates.Location = new System.Drawing.Point(397, 10);
			this.checkEditShowFlightDates.Name = "checkEditShowFlightDates";
			this.checkEditShowFlightDates.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditShowFlightDates.Properties.Appearance.Options.UseFont = true;
			this.checkEditShowFlightDates.Properties.AutoWidth = true;
			this.checkEditShowFlightDates.Properties.Caption = "Flight Dates";
			this.checkEditShowFlightDates.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Default;
			this.checkEditShowFlightDates.Size = new System.Drawing.Size(92, 20);
			this.checkEditShowFlightDates.StyleController = this.styleController;
			this.checkEditShowFlightDates.TabIndex = 25;
			this.checkEditShowFlightDates.CheckedChanged += new System.EventHandler(this.checkEditProductTogle_CheckedChanged);
			// 
			// checkEditWeeks
			// 
			this.checkEditWeeks.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditWeeks.Location = new System.Drawing.Point(914, 10);
			this.checkEditWeeks.Name = "checkEditWeeks";
			this.checkEditWeeks.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditWeeks.Properties.Appearance.Options.UseFont = true;
			this.checkEditWeeks.Properties.Caption = "Weeks";
			this.checkEditWeeks.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditWeeks.Size = new System.Drawing.Size(75, 19);
			this.checkEditWeeks.StyleController = this.styleController;
			this.checkEditWeeks.TabIndex = 24;
			this.checkEditWeeks.CheckedChanged += new System.EventHandler(this.checkEditWeeks_CheckedChanged);
			// 
			// checkEditMonths
			// 
			this.checkEditMonths.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditMonths.EditValue = true;
			this.checkEditMonths.Location = new System.Drawing.Point(835, 10);
			this.checkEditMonths.Name = "checkEditMonths";
			this.checkEditMonths.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditMonths.Properties.Appearance.Options.UseFont = true;
			this.checkEditMonths.Properties.Caption = "Months";
			this.checkEditMonths.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
			this.checkEditMonths.Size = new System.Drawing.Size(75, 19);
			this.checkEditMonths.StyleController = this.styleController;
			this.checkEditMonths.TabIndex = 23;
			this.checkEditMonths.CheckedChanged += new System.EventHandler(this.checkEditMonths_CheckedChanged);
			// 
			// spinEditDuration
			// 
			this.spinEditDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.spinEditDuration.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spinEditDuration.Enabled = false;
			this.spinEditDuration.Location = new System.Drawing.Point(774, 9);
			this.spinEditDuration.Name = "spinEditDuration";
			this.spinEditDuration.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
			this.spinEditDuration.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.spinEditDuration.Properties.IsFloatValue = false;
			this.spinEditDuration.Properties.Mask.EditMask = "N00";
			this.spinEditDuration.Properties.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
			this.spinEditDuration.Size = new System.Drawing.Size(53, 22);
			this.spinEditDuration.StyleController = this.styleController;
			this.spinEditDuration.TabIndex = 22;
			// 
			// checkEditDuration
			// 
			this.checkEditDuration.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditDuration.Location = new System.Drawing.Point(686, 10);
			this.checkEditDuration.Name = "checkEditDuration";
			this.checkEditDuration.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditDuration.Properties.Appearance.Options.UseFont = true;
			this.checkEditDuration.Properties.Caption = "Duration:";
			this.checkEditDuration.Size = new System.Drawing.Size(89, 19);
			this.checkEditDuration.StyleController = this.styleController;
			this.checkEditDuration.TabIndex = 21;
			this.checkEditDuration.CheckedChanged += new System.EventHandler(this.ckDuration_CheckedChanged);
			// 
			// DigitalProductContainer
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.xtraTabControlProducts);
			this.Controls.Add(this.pnHeader);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "DigitalProductContainer";
			this.Size = new System.Drawing.Size(1000, 562);
			this.Resize += new System.EventHandler(this.DigitalProductContainer_Resize);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlProducts)).EndInit();
			this.pnHeader.ResumeLayout(false);
			this.pnHeader.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowFlightDates.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditWeeks.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditMonths.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditDuration.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditDuration.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		public DevExpress.XtraTab.XtraTabControl xtraTabControlProducts;
		private DevExpress.XtraEditors.StyleController styleController;
		private System.Windows.Forms.Panel pnHeader;
		private DevExpress.XtraEditors.CheckEdit checkEditWeeks;
		private DevExpress.XtraEditors.CheckEdit checkEditMonths;
		private DevExpress.XtraEditors.SpinEdit spinEditDuration;
		private DevExpress.XtraEditors.CheckEdit checkEditDuration;
		protected DevExpress.XtraEditors.CheckEdit checkEditShowFlightDates;
		protected DevExpress.XtraEditors.LabelControl labelControlCategory;

    }
}
