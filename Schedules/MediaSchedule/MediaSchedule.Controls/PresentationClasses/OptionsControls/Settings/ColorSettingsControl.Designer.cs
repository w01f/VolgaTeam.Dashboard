namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Settings
{
	partial class ColorSettingsControl
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
			this.components = new System.ComponentModel.Container();
			this.pnStyle = new System.Windows.Forms.Panel();
			this.outputColorSelector = new Asa.Common.GUI.OutputColors.OutputColorSelector();
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.pnTitle = new System.Windows.Forms.Panel();
			this.labelControlTitle = new DevExpress.XtraEditors.LabelControl();
			this.pnStyle.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.pnTitle.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnStyle
			// 
			this.pnStyle.BackColor = System.Drawing.Color.Transparent;
			this.pnStyle.Controls.Add(this.outputColorSelector);
			this.pnStyle.Controls.Add(this.pnTitle);
			this.pnStyle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnStyle.Location = new System.Drawing.Point(0, 0);
			this.pnStyle.Name = "pnStyle";
			this.pnStyle.Size = new System.Drawing.Size(326, 510);
			this.pnStyle.TabIndex = 1;
			// 
			// outputColorSelector
			// 
			this.outputColorSelector.BackColor = System.Drawing.Color.White;
			this.outputColorSelector.Dock = System.Windows.Forms.DockStyle.Fill;
			this.outputColorSelector.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.outputColorSelector.Location = new System.Drawing.Point(0, 40);
			this.outputColorSelector.Name = "outputColorSelector";
			this.outputColorSelector.Size = new System.Drawing.Size(326, 470);
			this.outputColorSelector.TabIndex = 50;
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
			// pnTitle
			// 
			this.pnTitle.Controls.Add(this.labelControlTitle);
			this.pnTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTitle.Location = new System.Drawing.Point(0, 0);
			this.pnTitle.Name = "pnTitle";
			this.pnTitle.Padding = new System.Windows.Forms.Padding(10);
			this.pnTitle.Size = new System.Drawing.Size(326, 40);
			this.pnTitle.TabIndex = 52;
			// 
			// labelControlTitle
			// 
			this.labelControlTitle.AllowHtmlString = true;
			this.labelControlTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlTitle.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControlTitle.Location = new System.Drawing.Point(10, 10);
			this.labelControlTitle.Name = "labelControlTitle";
			this.labelControlTitle.Size = new System.Drawing.Size(306, 20);
			this.labelControlTitle.StyleController = this.styleController;
			this.labelControlTitle.TabIndex = 49;
			this.labelControlTitle.Text = "<color=gray>Schedule Table Color:</color>";
			// 
			// ColorSettingsControl
			// 
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnStyle);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.Name = "ColorSettingsControl";
			this.Size = new System.Drawing.Size(326, 510);
			this.pnStyle.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.pnTitle.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnStyle;
		protected Common.GUI.OutputColors.OutputColorSelector outputColorSelector;
		private System.Windows.Forms.Panel pnTitle;
		private DevExpress.XtraEditors.LabelControl labelControlTitle;
		private DevExpress.XtraEditors.StyleController styleController;
	}
}
