namespace Asa.Common.GUI.SlideSettingsEditors
{
	partial class FormSelectMasterWizard
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
			this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
			this.buttonXApply = new DevComponents.DotNetBar.ButtonX();
			this.laSlideFormat = new System.Windows.Forms.Label();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.comboBoxEditSlideFormat = new DevExpress.XtraEditors.ComboBoxEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideFormat.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// buttonXClose
			// 
			this.buttonXClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXClose.Location = new System.Drawing.Point(263, 95);
			this.buttonXClose.Name = "buttonXClose";
			this.buttonXClose.Size = new System.Drawing.Size(123, 36);
			this.buttonXClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXClose.TabIndex = 13;
			this.buttonXClose.Text = "Cancel";
			this.buttonXClose.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXApply
			// 
			this.buttonXApply.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXApply.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXApply.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXApply.Location = new System.Drawing.Point(52, 95);
			this.buttonXApply.Name = "buttonXApply";
			this.buttonXApply.Size = new System.Drawing.Size(123, 36);
			this.buttonXApply.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXApply.TabIndex = 12;
			this.buttonXApply.Text = "Apply";
			this.buttonXApply.TextColor = System.Drawing.Color.Black;
			// 
			// laSlideFormat
			// 
			this.laSlideFormat.BackColor = System.Drawing.Color.White;
			this.laSlideFormat.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laSlideFormat.ForeColor = System.Drawing.Color.Black;
			this.laSlideFormat.Location = new System.Drawing.Point(10, 9);
			this.laSlideFormat.Name = "laSlideFormat";
			this.laSlideFormat.Size = new System.Drawing.Size(418, 26);
			this.laSlideFormat.TabIndex = 14;
			this.laSlideFormat.Text = "Slide Format not found for selected size. Select from available:";
			this.laSlideFormat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			// comboBoxEditSlideFormat
			// 
			this.comboBoxEditSlideFormat.Location = new System.Drawing.Point(10, 47);
			this.comboBoxEditSlideFormat.Name = "comboBoxEditSlideFormat";
			this.comboBoxEditSlideFormat.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.comboBoxEditSlideFormat.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.comboBoxEditSlideFormat.Properties.Appearance.Options.UseBackColor = true;
			this.comboBoxEditSlideFormat.Properties.Appearance.Options.UseForeColor = true;
			this.comboBoxEditSlideFormat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditSlideFormat.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditSlideFormat.Size = new System.Drawing.Size(418, 22);
			this.comboBoxEditSlideFormat.StyleController = this.styleController;
			this.comboBoxEditSlideFormat.TabIndex = 15;
			// 
			// FormSelectMasterWizard
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(438, 143);
			this.Controls.Add(this.comboBoxEditSlideFormat);
			this.Controls.Add(this.laSlideFormat);
			this.Controls.Add(this.buttonXClose);
			this.Controls.Add(this.buttonXApply);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSelectMasterWizard";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Slide Format";
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideFormat.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonXClose;
		public DevComponents.DotNetBar.ButtonX buttonXApply;
		public System.Windows.Forms.Label laSlideFormat;
		private DevExpress.XtraEditors.StyleController styleController;
		public DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSlideFormat;
	}
}