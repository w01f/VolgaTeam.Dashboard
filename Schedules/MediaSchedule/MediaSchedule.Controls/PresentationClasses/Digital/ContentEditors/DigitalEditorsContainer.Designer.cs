using Asa.Media.Controls.PresentationClasses.Digital.Settings;

namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	partial class DigitalEditorsContainer
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
			this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
			this.xtraTabControlEditors = new DevExpress.XtraTab.XtraTabControl();
			this.retractableBarControl = new Asa.Common.GUI.RetractableBar.RetractableBarLeft();
			this.settingsContainer = new Asa.Media.Controls.PresentationClasses.Digital.Settings.SettingsContainer();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlEditors)).BeginInit();
			this.retractableBarControl.Content.SuspendLayout();
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
			// xtraTabControlEditors
			// 
			this.xtraTabControlEditors.AllowDrop = true;
			this.xtraTabControlEditors.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlEditors.Appearance.Options.UseFont = true;
			this.xtraTabControlEditors.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlEditors.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlEditors.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlEditors.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlEditors.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlEditors.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlEditors.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlEditors.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlEditors.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlEditors.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlEditors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlEditors.Location = new System.Drawing.Point(300, 0);
			this.xtraTabControlEditors.Name = "xtraTabControlEditors";
			this.xtraTabControlEditors.Size = new System.Drawing.Size(564, 593);
			this.xtraTabControlEditors.TabIndex = 5;
			// 
			// retractableBarControl
			// 
			this.retractableBarControl.AnimationDelay = 0;
			this.retractableBarControl.BackColor = System.Drawing.Color.Transparent;
			// 
			// retractableBarControl.Content
			// 
			this.retractableBarControl.Content.Controls.Add(this.settingsContainer);
			this.retractableBarControl.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBarControl.Content.Location = new System.Drawing.Point(2, 42);
			this.retractableBarControl.Content.Name = "Content";
			this.retractableBarControl.Content.Size = new System.Drawing.Size(296, 549);
			this.retractableBarControl.Content.TabIndex = 1;
			this.retractableBarControl.ContentSize = 300;
			this.retractableBarControl.Dock = System.Windows.Forms.DockStyle.Left;
			this.retractableBarControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			// 
			// retractableBarControl.Header
			// 
			this.retractableBarControl.Header.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBarControl.Header.Location = new System.Drawing.Point(49, 2);
			this.retractableBarControl.Header.Name = "Header";
			this.retractableBarControl.Header.Size = new System.Drawing.Size(245, 36);
			this.retractableBarControl.Header.TabIndex = 2;
			this.retractableBarControl.Location = new System.Drawing.Point(0, 0);
			this.retractableBarControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.retractableBarControl.Name = "retractableBarControl";
			this.retractableBarControl.Size = new System.Drawing.Size(300, 593);
			this.retractableBarControl.TabIndex = 4;
			// 
			// settingsContainer
			// 
			this.settingsContainer.BackColor = System.Drawing.Color.White;
			this.settingsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.settingsContainer.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.settingsContainer.Location = new System.Drawing.Point(0, 0);
			this.settingsContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.settingsContainer.Name = "settingsContainer";
			this.settingsContainer.Size = new System.Drawing.Size(296, 549);
			this.settingsContainer.TabIndex = 0;
			// 
			// DigitalEditorsContainer
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.xtraTabControlEditors);
			this.Controls.Add(this.retractableBarControl);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "DigitalEditorsContainer";
			this.Size = new System.Drawing.Size(864, 593);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlEditors)).EndInit();
			this.retractableBarControl.Content.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion
		private DevExpress.XtraEditors.StyleController styleController;
	    protected Common.GUI.RetractableBar.RetractableBarLeft retractableBarControl;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlEditors;
		private SettingsContainer settingsContainer;
	}
}
