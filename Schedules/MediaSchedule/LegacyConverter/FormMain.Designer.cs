namespace Asa.Media.LegacyConverter
{
	partial class FormMain
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
			this.styleManager = new DevComponents.DotNetBar.StyleManager();
			this.buttonXConvert = new DevComponents.DotNetBar.ButtonX();
			this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.labelControlSourceTitle = new DevExpress.XtraEditors.LabelControl();
			this.buttonEditSource = new DevExpress.XtraEditors.ButtonEdit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditSource.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// styleManager
			// 
			this.styleManager.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
			this.styleManager.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(87)))), ((int)(((byte)(154))))));
			// 
			// buttonXConvert
			// 
			this.buttonXConvert.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXConvert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXConvert.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXConvert.Location = new System.Drawing.Point(57, 78);
			this.buttonXConvert.Name = "buttonXConvert";
			this.buttonXConvert.Size = new System.Drawing.Size(101, 31);
			this.buttonXConvert.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXConvert.TabIndex = 0;
			this.buttonXConvert.Text = "Convert";
			this.buttonXConvert.Click += new System.EventHandler(this.OnConvertClick);
			// 
			// buttonXClose
			// 
			this.buttonXClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClose.Location = new System.Drawing.Point(247, 78);
			this.buttonXClose.Name = "buttonXClose";
			this.buttonXClose.Size = new System.Drawing.Size(101, 31);
			this.buttonXClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXClose.TabIndex = 1;
			this.buttonXClose.Text = "Close";
			this.buttonXClose.Click += new System.EventHandler(this.OnCloseClick);
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
			// labelControlSourceTitle
			// 
			this.labelControlSourceTitle.Location = new System.Drawing.Point(12, 12);
			this.labelControlSourceTitle.Name = "labelControlSourceTitle";
			this.labelControlSourceTitle.Size = new System.Drawing.Size(197, 16);
			this.labelControlSourceTitle.StyleController = this.styleController;
			this.labelControlSourceTitle.TabIndex = 2;
			this.labelControlSourceTitle.Text = "Select Source Schedule Location:";
			// 
			// buttonEditSource
			// 
			this.buttonEditSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonEditSource.Location = new System.Drawing.Point(12, 34);
			this.buttonEditSource.Name = "buttonEditSource";
			this.buttonEditSource.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
			this.buttonEditSource.Size = new System.Drawing.Size(382, 22);
			this.buttonEditSource.StyleController = this.styleController;
			this.buttonEditSource.TabIndex = 3;
			this.buttonEditSource.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.OnSourceButtonClick);
			// 
			// FormMain
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.ClientSize = new System.Drawing.Size(406, 115);
			this.Controls.Add(this.buttonEditSource);
			this.Controls.Add(this.labelControlSourceTitle);
			this.Controls.Add(this.buttonXClose);
			this.Controls.Add(this.buttonXConvert);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Import Schedules";
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.buttonEditSource.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private DevComponents.DotNetBar.StyleManager styleManager;
		private DevComponents.DotNetBar.ButtonX buttonXConvert;
		private DevComponents.DotNetBar.ButtonX buttonXClose;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlSourceTitle;
		private DevExpress.XtraEditors.ButtonEdit buttonEditSource;
	}
}

