namespace Asa.Media.Controls.PresentationClasses.Summary
{
	partial class BaseSummaryInfoControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BaseSummaryInfoControl));
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
			this.checkEditDecisionMaker = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditBusinessName = new DevExpress.XtraEditors.CheckEdit();
			this.laFlightDates = new System.Windows.Forms.Label();
			this.laPresentationDate = new System.Windows.Forms.Label();
			this.checkEditTotalInvestment = new DevExpress.XtraEditors.CheckEdit();
			this.spinEditTotal = new DevExpress.XtraEditors.SpinEdit();
			this.checkEditPresentationDate = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditFlightDates = new DevExpress.XtraEditors.CheckEdit();
			this.spinEditMonthly = new DevExpress.XtraEditors.SpinEdit();
			this.checkEditMonthlyInvestment = new DevExpress.XtraEditors.CheckEdit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditDecisionMaker.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditBusinessName.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditTotalInvestment.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditTotal.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditPresentationDate.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditFlightDates.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditMonthly.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditMonthlyInvestment.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// checkEditDecisionMaker
			// 
			this.checkEditDecisionMaker.Location = new System.Drawing.Point(15, 71);
			this.checkEditDecisionMaker.Name = "checkEditDecisionMaker";
			this.checkEditDecisionMaker.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditDecisionMaker.Properties.Appearance.Options.UseFont = true;
			this.checkEditDecisionMaker.Properties.AutoWidth = true;
			this.checkEditDecisionMaker.Properties.Caption = "Decision Maker: ";
			this.checkEditDecisionMaker.Size = new System.Drawing.Size(120, 20);
			this.checkEditDecisionMaker.TabIndex = 8;
			this.checkEditDecisionMaker.CheckedChanged += new System.EventHandler(this.OnSettingChanged);
			// 
			// checkEditBusinessName
			// 
			this.checkEditBusinessName.Location = new System.Drawing.Point(15, 18);
			this.checkEditBusinessName.Name = "checkEditBusinessName";
			this.checkEditBusinessName.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditBusinessName.Properties.Appearance.Options.UseFont = true;
			this.checkEditBusinessName.Properties.AutoWidth = true;
			this.checkEditBusinessName.Properties.Caption = "Business Name: ";
			this.checkEditBusinessName.Size = new System.Drawing.Size(122, 20);
			this.checkEditBusinessName.TabIndex = 7;
			this.checkEditBusinessName.CheckedChanged += new System.EventHandler(this.OnSettingChanged);
			// 
			// laFlightDates
			// 
			this.laFlightDates.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laFlightDates.Location = new System.Drawing.Point(33, 149);
			this.laFlightDates.Name = "laFlightDates";
			this.laFlightDates.Size = new System.Drawing.Size(137, 21);
			this.laFlightDates.TabIndex = 126;
			this.laFlightDates.Text = "Start-End Date Tag";
			// 
			// laPresentationDate
			// 
			this.laPresentationDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laPresentationDate.Location = new System.Drawing.Point(33, 214);
			this.laPresentationDate.Name = "laPresentationDate";
			this.laPresentationDate.Size = new System.Drawing.Size(134, 22);
			this.laPresentationDate.TabIndex = 127;
			this.laPresentationDate.Text = "$Tag";
			this.laPresentationDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// checkEditTotalInvestment
			// 
			this.checkEditTotalInvestment.Location = new System.Drawing.Point(15, 342);
			this.checkEditTotalInvestment.Name = "checkEditTotalInvestment";
			this.checkEditTotalInvestment.Properties.AllowFocused = false;
			this.checkEditTotalInvestment.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditTotalInvestment.Properties.Appearance.ForeColor = System.Drawing.Color.Gray;
			this.checkEditTotalInvestment.Properties.Appearance.Options.UseFont = true;
			this.checkEditTotalInvestment.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditTotalInvestment.Properties.AutoWidth = true;
			this.checkEditTotalInvestment.Properties.Caption = "Total Investment:";
			this.checkEditTotalInvestment.Size = new System.Drawing.Size(120, 20);
			this.checkEditTotalInvestment.TabIndex = 125;
			this.checkEditTotalInvestment.CheckedChanged += new System.EventHandler(this.OnSettingChanged);
			// 
			// spinEditTotal
			// 
			this.spinEditTotal.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spinEditTotal.Enabled = false;
			this.spinEditTotal.Location = new System.Drawing.Point(37, 369);
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
			this.spinEditTotal.TabIndex = 124;
			// 
			// checkEditPresentationDate
			// 
			this.checkEditPresentationDate.Location = new System.Drawing.Point(15, 190);
			this.checkEditPresentationDate.Name = "checkEditPresentationDate";
			this.checkEditPresentationDate.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditPresentationDate.Properties.Appearance.Options.UseFont = true;
			this.checkEditPresentationDate.Properties.AutoWidth = true;
			this.checkEditPresentationDate.Properties.Caption = "Presentation Date: ";
			this.checkEditPresentationDate.Size = new System.Drawing.Size(134, 20);
			this.checkEditPresentationDate.TabIndex = 120;
			this.checkEditPresentationDate.CheckedChanged += new System.EventHandler(this.OnSettingChanged);
			// 
			// checkEditFlightDates
			// 
			this.checkEditFlightDates.Location = new System.Drawing.Point(15, 125);
			this.checkEditFlightDates.Name = "checkEditFlightDates";
			this.checkEditFlightDates.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditFlightDates.Properties.Appearance.Options.UseFont = true;
			this.checkEditFlightDates.Properties.AutoWidth = true;
			this.checkEditFlightDates.Properties.Caption = "Campaign Dates: ";
			this.checkEditFlightDates.Size = new System.Drawing.Size(126, 20);
			this.checkEditFlightDates.TabIndex = 121;
			this.checkEditFlightDates.CheckedChanged += new System.EventHandler(this.OnSettingChanged);
			// 
			// spinEditMonthly
			// 
			this.spinEditMonthly.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
			this.spinEditMonthly.Enabled = false;
			this.spinEditMonthly.Location = new System.Drawing.Point(37, 290);
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
			this.spinEditMonthly.TabIndex = 123;
			// 
			// checkEditMonthlyInvestment
			// 
			this.checkEditMonthlyInvestment.Location = new System.Drawing.Point(15, 263);
			this.checkEditMonthlyInvestment.Name = "checkEditMonthlyInvestment";
			this.checkEditMonthlyInvestment.Properties.AllowFocused = false;
			this.checkEditMonthlyInvestment.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.checkEditMonthlyInvestment.Properties.Appearance.ForeColor = System.Drawing.Color.Gray;
			this.checkEditMonthlyInvestment.Properties.Appearance.Options.UseFont = true;
			this.checkEditMonthlyInvestment.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditMonthlyInvestment.Properties.AutoWidth = true;
			this.checkEditMonthlyInvestment.Properties.Caption = "Monthly Investment:";
			this.checkEditMonthlyInvestment.Size = new System.Drawing.Size(138, 20);
			this.checkEditMonthlyInvestment.TabIndex = 122;
			this.checkEditMonthlyInvestment.CheckedChanged += new System.EventHandler(this.OnSettingChanged);
			// 
			// BaseSummaryInfoControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.laFlightDates);
			this.Controls.Add(this.laPresentationDate);
			this.Controls.Add(this.checkEditTotalInvestment);
			this.Controls.Add(this.spinEditTotal);
			this.Controls.Add(this.checkEditPresentationDate);
			this.Controls.Add(this.checkEditFlightDates);
			this.Controls.Add(this.spinEditMonthly);
			this.Controls.Add(this.checkEditMonthlyInvestment);
			this.Controls.Add(this.checkEditDecisionMaker);
			this.Controls.Add(this.checkEditBusinessName);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "BaseSummaryInfoControl";
			this.Size = new System.Drawing.Size(292, 457);
			((System.ComponentModel.ISupportInitialize)(this.checkEditDecisionMaker.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditBusinessName.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditTotalInvestment.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditTotal.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditPresentationDate.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditFlightDates.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.spinEditMonthly.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditMonthlyInvestment.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		protected DevExpress.XtraEditors.CheckEdit checkEditDecisionMaker;
		protected DevExpress.XtraEditors.CheckEdit checkEditBusinessName;
		protected System.Windows.Forms.Label laFlightDates;
		protected System.Windows.Forms.Label laPresentationDate;
		protected DevExpress.XtraEditors.CheckEdit checkEditTotalInvestment;
		protected DevExpress.XtraEditors.SpinEdit spinEditTotal;
		protected DevExpress.XtraEditors.CheckEdit checkEditPresentationDate;
		protected DevExpress.XtraEditors.CheckEdit checkEditFlightDates;
		protected DevExpress.XtraEditors.SpinEdit spinEditMonthly;
		protected DevExpress.XtraEditors.CheckEdit checkEditMonthlyInvestment;
	}
}
