namespace Asa.Common.GUI.Themes
{
	partial class FormThemeSelector
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
			this.pnMain = new System.Windows.Forms.Panel();
			this.pnButtons = new System.Windows.Forms.Panel();
			this.buttonXOK = new DevComponents.DotNetBar.ButtonX();
			this.buttonXCancel = new DevComponents.DotNetBar.ButtonX();
			this.pnHeader = new System.Windows.Forms.Panel();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.labelControlSlideSize = new DevExpress.XtraEditors.LabelControl();
			this.labelControlThemName = new DevExpress.XtraEditors.LabelControl();
			this.checkEditApplyThemeForAllSlideTypes = new DevExpress.XtraEditors.CheckEdit();
			this.pnButtons.SuspendLayout();
			this.pnHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditApplyThemeForAllSlideTypes.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// pnMain
			// 
			this.pnMain.BackColor = System.Drawing.Color.Transparent;
			this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnMain.ForeColor = System.Drawing.Color.Black;
			this.pnMain.Location = new System.Drawing.Point(0, 40);
			this.pnMain.Name = "pnMain";
			this.pnMain.Size = new System.Drawing.Size(894, 408);
			this.pnMain.TabIndex = 3;
			// 
			// pnButtons
			// 
			this.pnButtons.BackColor = System.Drawing.Color.Transparent;
			this.pnButtons.Controls.Add(this.checkEditApplyThemeForAllSlideTypes);
			this.pnButtons.Controls.Add(this.buttonXOK);
			this.pnButtons.Controls.Add(this.buttonXCancel);
			this.pnButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnButtons.ForeColor = System.Drawing.Color.Black;
			this.pnButtons.Location = new System.Drawing.Point(0, 448);
			this.pnButtons.Name = "pnButtons";
			this.pnButtons.Size = new System.Drawing.Size(894, 46);
			this.pnButtons.TabIndex = 4;
			// 
			// buttonXOK
			// 
			this.buttonXOK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXOK.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.buttonXOK.Location = new System.Drawing.Point(613, 7);
			this.buttonXOK.Name = "buttonXOK";
			this.buttonXOK.Size = new System.Drawing.Size(122, 32);
			this.buttonXOK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXOK.TabIndex = 4;
			this.buttonXOK.Text = "Select";
			this.buttonXOK.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXCancel
			// 
			this.buttonXCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXCancel.Location = new System.Drawing.Point(760, 7);
			this.buttonXCancel.Name = "buttonXCancel";
			this.buttonXCancel.Size = new System.Drawing.Size(122, 32);
			this.buttonXCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.buttonXCancel.TabIndex = 5;
			this.buttonXCancel.Text = "Cancel";
			this.buttonXCancel.TextColor = System.Drawing.Color.Black;
			// 
			// pnHeader
			// 
			this.pnHeader.BackColor = System.Drawing.Color.Transparent;
			this.pnHeader.Controls.Add(this.labelControlThemName);
			this.pnHeader.Controls.Add(this.labelControlSlideSize);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeader.ForeColor = System.Drawing.Color.Black;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(894, 40);
			this.pnHeader.TabIndex = 6;
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
			// labelControlSlideSize
			// 
			this.labelControlSlideSize.AllowHtmlString = true;
			this.labelControlSlideSize.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlSlideSize.Dock = System.Windows.Forms.DockStyle.Left;
			this.labelControlSlideSize.Location = new System.Drawing.Point(0, 0);
			this.labelControlSlideSize.Name = "labelControlSlideSize";
			this.labelControlSlideSize.Size = new System.Drawing.Size(327, 40);
			this.labelControlSlideSize.StyleController = this.styleController;
			this.labelControlSlideSize.TabIndex = 4;
			this.labelControlSlideSize.Text = "<b><size=+4>Slide Size: {0}</size></b>";
			// 
			// labelControlThemName
			// 
			this.labelControlThemName.AllowHtmlString = true;
			this.labelControlThemName.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.labelControlThemName.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlThemName.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControlThemName.Location = new System.Drawing.Point(327, 0);
			this.labelControlThemName.Name = "labelControlThemName";
			this.labelControlThemName.Size = new System.Drawing.Size(567, 40);
			this.labelControlThemName.StyleController = this.styleController;
			this.labelControlThemName.TabIndex = 5;
			this.labelControlThemName.Text = "<b><size=+4>Theme Name</size></b>";
			// 
			// checkEditApplyThemeForAllSlideTypes
			// 
			this.checkEditApplyThemeForAllSlideTypes.Location = new System.Drawing.Point(12, 13);
			this.checkEditApplyThemeForAllSlideTypes.Name = "checkEditApplyThemeForAllSlideTypes";
			this.checkEditApplyThemeForAllSlideTypes.Properties.Caption = "Use this Slide Theme for everything";
			this.checkEditApplyThemeForAllSlideTypes.Size = new System.Drawing.Size(227, 20);
			this.checkEditApplyThemeForAllSlideTypes.StyleController = this.styleController;
			this.checkEditApplyThemeForAllSlideTypes.TabIndex = 6;
			// 
			// FormThemeSelector
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(894, 494);
			this.Controls.Add(this.pnMain);
			this.Controls.Add(this.pnHeader);
			this.Controls.Add(this.pnButtons);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.ForeColor = System.Drawing.Color.Black;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormThemeSelector";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Select Theme";
			this.TopMost = true;
			this.pnButtons.ResumeLayout(false);
			this.pnHeader.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.checkEditApplyThemeForAllSlideTypes.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnMain;
		private System.Windows.Forms.Panel pnButtons;
		private DevComponents.DotNetBar.ButtonX buttonXOK;
		private DevComponents.DotNetBar.ButtonX buttonXCancel;
		private System.Windows.Forms.Panel pnHeader;
		private DevExpress.XtraEditors.StyleController styleController;
		private DevExpress.XtraEditors.LabelControl labelControlThemName;
		private DevExpress.XtraEditors.LabelControl labelControlSlideSize;
		private DevExpress.XtraEditors.CheckEdit checkEditApplyThemeForAllSlideTypes;
	}
}