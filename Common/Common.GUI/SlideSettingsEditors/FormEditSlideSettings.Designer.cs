namespace Asa.Common.GUI.SlideSettingsEditors
{
	partial class FormEditSlideSettings
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
			this.buttonXSize34 = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSize169 = new DevComponents.DotNetBar.ButtonX();
			this.buttonXSize43 = new DevComponents.DotNetBar.ButtonX();
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
			this.buttonXClose.Location = new System.Drawing.Point(220, 132);
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
			this.buttonXApply.Location = new System.Drawing.Point(56, 132);
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
			this.laSlideFormat.Location = new System.Drawing.Point(12, 9);
			this.laSlideFormat.Name = "laSlideFormat";
			this.laSlideFormat.Size = new System.Drawing.Size(414, 26);
			this.laSlideFormat.TabIndex = 14;
			this.laSlideFormat.Text = "Slide Format:";
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
			this.comboBoxEditSlideFormat.Location = new System.Drawing.Point(108, 11);
			this.comboBoxEditSlideFormat.Name = "comboBoxEditSlideFormat";
			this.comboBoxEditSlideFormat.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.comboBoxEditSlideFormat.Properties.Appearance.ForeColor = System.Drawing.Color.Black;
			this.comboBoxEditSlideFormat.Properties.Appearance.Options.UseBackColor = true;
			this.comboBoxEditSlideFormat.Properties.Appearance.Options.UseForeColor = true;
			this.comboBoxEditSlideFormat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEditSlideFormat.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.comboBoxEditSlideFormat.Size = new System.Drawing.Size(278, 22);
			this.comboBoxEditSlideFormat.StyleController = this.styleController;
			this.comboBoxEditSlideFormat.TabIndex = 15;
			this.comboBoxEditSlideFormat.EditValueChanged += new System.EventHandler(this.comboBoxEditSlideFormat_EditValueChanged);
			// 
			// buttonXSize34
			// 
			this.buttonXSize34.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSize34.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSize34.Location = new System.Drawing.Point(276, 57);
			this.buttonXSize34.Name = "buttonXSize34";
			this.buttonXSize34.Size = new System.Drawing.Size(108, 35);
			this.buttonXSize34.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSize34.TabIndex = 21;
			this.buttonXSize34.Text = "3 x 4";
			this.buttonXSize34.TextColor = System.Drawing.Color.Black;
			this.buttonXSize34.Click += new System.EventHandler(this.buttonXSize_Click);
			// 
			// buttonXSize169
			// 
			this.buttonXSize169.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSize169.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSize169.Location = new System.Drawing.Point(145, 57);
			this.buttonXSize169.Name = "buttonXSize169";
			this.buttonXSize169.Size = new System.Drawing.Size(108, 35);
			this.buttonXSize169.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSize169.TabIndex = 20;
			this.buttonXSize169.Text = "16 x 9";
			this.buttonXSize169.TextColor = System.Drawing.Color.Black;
			this.buttonXSize169.Click += new System.EventHandler(this.buttonXSize_Click);
			// 
			// buttonXSize43
			// 
			this.buttonXSize43.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXSize43.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXSize43.Location = new System.Drawing.Point(14, 57);
			this.buttonXSize43.Name = "buttonXSize43";
			this.buttonXSize43.Size = new System.Drawing.Size(108, 35);
			this.buttonXSize43.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXSize43.TabIndex = 19;
			this.buttonXSize43.Text = "4 x 3";
			this.buttonXSize43.TextColor = System.Drawing.Color.Black;
			this.buttonXSize43.Click += new System.EventHandler(this.buttonXSize_Click);
			// 
			// FormEditSlideSettings
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(398, 180);
			this.Controls.Add(this.buttonXSize34);
			this.Controls.Add(this.buttonXSize169);
			this.Controls.Add(this.buttonXSize43);
			this.Controls.Add(this.comboBoxEditSlideFormat);
			this.Controls.Add(this.laSlideFormat);
			this.Controls.Add(this.buttonXClose);
			this.Controls.Add(this.buttonXApply);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormEditSlideSettings";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Edit Slide Settings";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormEditSlideSettings_FormClosing);
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FormEditSlideSettings_FormClosed);
			this.Load += new System.EventHandler(this.FormEditSlideSettings_Load);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideFormat.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevComponents.DotNetBar.ButtonX buttonXClose;
		public DevComponents.DotNetBar.ButtonX buttonXApply;
		public System.Windows.Forms.Label laSlideFormat;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSlideFormat;
		public DevComponents.DotNetBar.ButtonX buttonXSize34;
		public DevComponents.DotNetBar.ButtonX buttonXSize169;
		public DevComponents.DotNetBar.ButtonX buttonXSize43;
	}
}