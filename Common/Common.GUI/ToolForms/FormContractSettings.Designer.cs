namespace Asa.Common.GUI.ToolForms
{
	partial class FormContractSettings
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormContractSettings));
			DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.checkEditShowSignatureLine = new DevExpress.XtraEditors.CheckEdit();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.checkEditShowRatesExpiration = new DevExpress.XtraEditors.CheckEdit();
			this.checkEditShowDisclaimer = new DevExpress.XtraEditors.CheckEdit();
			this.laTitle = new System.Windows.Forms.Label();
			this.dateEditRatesExpirationDate = new DevExpress.XtraEditors.DateEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowSignatureLine.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowRatesExpiration.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowDisclaimer.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditRatesExpirationDate.Properties.CalendarTimeProperties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditRatesExpirationDate.Properties)).BeginInit();
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
			// checkEditShowSignatureLine
			// 
			this.checkEditShowSignatureLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditShowSignatureLine.Location = new System.Drawing.Point(12, 57);
			this.checkEditShowSignatureLine.Name = "checkEditShowSignatureLine";
			this.checkEditShowSignatureLine.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditShowSignatureLine.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkEditShowSignatureLine.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditShowSignatureLine.Properties.Caption = "<b>A.</b> Client Signature Line";
			this.checkEditShowSignatureLine.Size = new System.Drawing.Size(321, 20);
			this.checkEditShowSignatureLine.StyleController = this.styleController;
			this.checkEditShowSignatureLine.TabIndex = 121;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(56, 204);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(91, 36);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 122;
			this.buttonXOK.Text = "OK";
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(197, 204);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(91, 36);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 123;
			this.buttonXCancel.Text = "Cancel";
			// 
			// checkEditShowRatesExpiration
			// 
			this.checkEditShowRatesExpiration.Location = new System.Drawing.Point(12, 100);
			this.checkEditShowRatesExpiration.Name = "checkEditShowRatesExpiration";
			this.checkEditShowRatesExpiration.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditShowRatesExpiration.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkEditShowRatesExpiration.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditShowRatesExpiration.Properties.Caption = "<b>B.</b> Rates Expiration Date";
			this.checkEditShowRatesExpiration.Size = new System.Drawing.Size(179, 20);
			this.checkEditShowRatesExpiration.StyleController = this.styleController;
			this.checkEditShowRatesExpiration.TabIndex = 125;
			this.checkEditShowRatesExpiration.CheckedChanged += new System.EventHandler(this.checkEditShowRatesExpiration_CheckedChanged);
			// 
			// checkEditShowDisclaimer
			// 
			this.checkEditShowDisclaimer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.checkEditShowDisclaimer.Location = new System.Drawing.Point(12, 143);
			this.checkEditShowDisclaimer.Name = "checkEditShowDisclaimer";
			this.checkEditShowDisclaimer.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
			this.checkEditShowDisclaimer.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.checkEditShowDisclaimer.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.checkEditShowDisclaimer.Properties.Appearance.Options.UseBackColor = true;
			this.checkEditShowDisclaimer.Properties.Appearance.Options.UseForeColor = true;
			this.checkEditShowDisclaimer.Properties.Appearance.Options.UseTextOptions = true;
			this.checkEditShowDisclaimer.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditShowDisclaimer.Properties.AppearanceDisabled.Options.UseTextOptions = true;
			this.checkEditShowDisclaimer.Properties.AppearanceDisabled.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditShowDisclaimer.Properties.AppearanceFocused.Options.UseTextOptions = true;
			this.checkEditShowDisclaimer.Properties.AppearanceFocused.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditShowDisclaimer.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
			this.checkEditShowDisclaimer.Properties.AppearanceReadOnly.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.checkEditShowDisclaimer.Properties.Caption = "<b>C.</b> Legal disclaimer and cancellation policy";
			this.checkEditShowDisclaimer.Size = new System.Drawing.Size(321, 20);
			this.checkEditShowDisclaimer.StyleController = this.styleController;
			this.checkEditShowDisclaimer.TabIndex = 127;
			// 
			// laTitle
			// 
			this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laTitle.BackColor = System.Drawing.Color.White;
			this.laTitle.ForeColor = System.Drawing.Color.Black;
			this.laTitle.Location = new System.Drawing.Point(12, 9);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(321, 45);
			this.laTitle.TabIndex = 128;
			this.laTitle.Text = "Do you want to place informal contract or client agreement language on your Sched" +
    "ule slides?";
			// 
			// dateEditRatesExpirationDate
			// 
			this.dateEditRatesExpirationDate.EditValue = null;
			this.dateEditRatesExpirationDate.Enabled = false;
			this.dateEditRatesExpirationDate.Location = new System.Drawing.Point(197, 99);
			this.dateEditRatesExpirationDate.Name = "dateEditRatesExpirationDate";
			this.dateEditRatesExpirationDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
			this.dateEditRatesExpirationDate.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.dateEditRatesExpirationDate.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.dateEditRatesExpirationDate.Properties.Appearance.Options.UseBackColor = true;
			this.dateEditRatesExpirationDate.Properties.Appearance.Options.UseForeColor = true;
			this.dateEditRatesExpirationDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, ((System.Drawing.Image)(resources.GetObject("dateEditRatesExpirationDate.Properties.Buttons"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, "", null, null, true)});
			this.dateEditRatesExpirationDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.dateEditRatesExpirationDate.Properties.DisplayFormat.FormatString = "MM/dd/yyyy";
			this.dateEditRatesExpirationDate.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditRatesExpirationDate.Properties.EditFormat.FormatString = "MM/dd/yyyy";
			this.dateEditRatesExpirationDate.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.dateEditRatesExpirationDate.Properties.Mask.EditMask = "MM/dd/yyyy";
			this.dateEditRatesExpirationDate.Properties.NullText = "Select";
			this.dateEditRatesExpirationDate.Properties.ShowPopupShadow = false;
			this.dateEditRatesExpirationDate.Properties.ShowToday = false;
			this.dateEditRatesExpirationDate.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.dateEditRatesExpirationDate.Size = new System.Drawing.Size(120, 22);
			this.dateEditRatesExpirationDate.StyleController = this.styleController;
			this.dateEditRatesExpirationDate.TabIndex = 129;
			// 
			// FormContractSettings
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(345, 252);
			this.Controls.Add(this.dateEditRatesExpirationDate);
			this.Controls.Add(this.laTitle);
			this.Controls.Add(this.checkEditShowDisclaimer);
			this.Controls.Add(this.buttonXCancel);
			this.Controls.Add(this.buttonXOK);
			this.Controls.Add(this.checkEditShowSignatureLine);
			this.Controls.Add(this.checkEditShowRatesExpiration);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormContractSettings";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Contract Agreement Settings";
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowSignatureLine.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowRatesExpiration.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditShowDisclaimer.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditRatesExpirationDate.Properties.CalendarTimeProperties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dateEditRatesExpirationDate.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.StyleController styleController;
		private DevComponents.DotNetBar.ButtonX buttonXOK;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		public DevExpress.XtraEditors.CheckEdit checkEditShowSignatureLine;
		public DevExpress.XtraEditors.CheckEdit checkEditShowRatesExpiration;
		public DevExpress.XtraEditors.CheckEdit checkEditShowDisclaimer;
		private System.Windows.Forms.Label laTitle;
		public DevExpress.XtraEditors.DateEdit dateEditRatesExpirationDate;
	}
}