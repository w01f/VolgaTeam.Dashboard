using System.Windows.Forms;
using Asa.Media.Controls.PresentationClasses.ScheduleControls;

namespace Asa.Media.Controls.PresentationClasses.Summary
{
	partial class SummaryContainer
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
			DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
			this.xtraTabControlSections = new DevExpress.XtraTab.XtraTabControl();
			this.retractableBarControl = new Asa.Common.GUI.RetractableBar.RetractableBarLeft();
			this.xtraTabControlOptions = new DevExpress.XtraTab.XtraTabControl();
			this.pnInfoBottom = new System.Windows.Forms.Panel();
			this.hyperLinkEditInfoContract = new DevExpress.XtraEditors.HyperLinkEdit();
			this.pnSections = new System.Windows.Forms.Panel();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlSections)).BeginInit();
			this.retractableBarControl.Content.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlOptions)).BeginInit();
			this.pnInfoBottom.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditInfoContract.Properties)).BeginInit();
			this.pnSections.SuspendLayout();
			this.SuspendLayout();
			// 
			// xtraTabControlSections
			// 
			this.xtraTabControlSections.AllowDrop = true;
			this.xtraTabControlSections.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlSections.Appearance.Options.UseFont = true;
			this.xtraTabControlSections.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlSections.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlSections.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlSections.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlSections.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlSections.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlSections.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlSections.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlSections.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlSections.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlSections.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlSections.Location = new System.Drawing.Point(300, 0);
			this.xtraTabControlSections.Name = "xtraTabControlSections";
			this.xtraTabControlSections.Size = new System.Drawing.Size(711, 593);
			this.xtraTabControlSections.TabIndex = 5;
			this.xtraTabControlSections.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControlSections_SelectedPageChanged);
			// 
			// retractableBarControl
			// 
			this.retractableBarControl.AnimationDelay = 0;
			this.retractableBarControl.BackColor = System.Drawing.Color.Transparent;
			// 
			// retractableBarControl.Content
			// 
			this.retractableBarControl.Content.Controls.Add(this.xtraTabControlOptions);
			this.retractableBarControl.Content.Controls.Add(this.pnInfoBottom);
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
			// xtraTabControlOptions
			// 
			this.xtraTabControlOptions.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlOptions.Appearance.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlOptions.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.Header.Options.UseTextOptions = true;
			this.xtraTabControlOptions.AppearancePage.Header.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.xtraTabControlOptions.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlOptions.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.HeaderActive.Options.UseTextOptions = true;
			this.xtraTabControlOptions.AppearancePage.HeaderActive.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.xtraTabControlOptions.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlOptions.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.HeaderDisabled.Options.UseTextOptions = true;
			this.xtraTabControlOptions.AppearancePage.HeaderDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.xtraTabControlOptions.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlOptions.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlOptions.AppearancePage.HeaderHotTracked.Options.UseTextOptions = true;
			this.xtraTabControlOptions.AppearancePage.HeaderHotTracked.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.xtraTabControlOptions.AppearancePage.PageClient.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.xtraTabControlOptions.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlOptions.AppearancePage.PageClient.Options.UseBackColor = true;
			this.xtraTabControlOptions.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlOptions.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.True;
			this.xtraTabControlOptions.Location = new System.Drawing.Point(0, 0);
			this.xtraTabControlOptions.MultiLine = DevExpress.Utils.DefaultBoolean.False;
			this.xtraTabControlOptions.Name = "xtraTabControlOptions";
			this.xtraTabControlOptions.Size = new System.Drawing.Size(296, 499);
			this.xtraTabControlOptions.TabIndex = 0;
			// 
			// pnInfoBottom
			// 
			this.pnInfoBottom.BackColor = System.Drawing.Color.White;
			this.pnInfoBottom.Controls.Add(this.hyperLinkEditInfoContract);
			this.pnInfoBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnInfoBottom.Location = new System.Drawing.Point(0, 499);
			this.pnInfoBottom.Name = "pnInfoBottom";
			this.pnInfoBottom.Size = new System.Drawing.Size(296, 50);
			this.pnInfoBottom.TabIndex = 124;
			// 
			// hyperLinkEditInfoContract
			// 
			this.hyperLinkEditInfoContract.EditValue = "Contract Settings";
			this.hyperLinkEditInfoContract.Location = new System.Drawing.Point(82, 15);
			this.hyperLinkEditInfoContract.Name = "hyperLinkEditInfoContract";
			this.hyperLinkEditInfoContract.Properties.Appearance.BackColor = System.Drawing.Color.White;
			this.hyperLinkEditInfoContract.Properties.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Underline))), System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.hyperLinkEditInfoContract.Properties.Appearance.ForeColor = System.Drawing.Color.DarkBlue;
			this.hyperLinkEditInfoContract.Properties.Appearance.Options.UseBackColor = true;
			this.hyperLinkEditInfoContract.Properties.Appearance.Options.UseFont = true;
			this.hyperLinkEditInfoContract.Properties.Appearance.Options.UseForeColor = true;
			this.hyperLinkEditInfoContract.Properties.Appearance.Options.UseTextOptions = true;
			this.hyperLinkEditInfoContract.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.hyperLinkEditInfoContract.Properties.AppearanceDisabled.ForeColor = System.Drawing.Color.LightGray;
			this.hyperLinkEditInfoContract.Properties.AppearanceDisabled.Options.UseForeColor = true;
			this.hyperLinkEditInfoContract.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.hyperLinkEditInfoContract.Size = new System.Drawing.Size(132, 20);
			toolTipItem1.Text = "Change Slide Output Settings";
			superToolTip1.Items.Add(toolTipItem1);
			this.hyperLinkEditInfoContract.SuperTip = superToolTip1;
			this.hyperLinkEditInfoContract.TabIndex = 123;
			this.hyperLinkEditInfoContract.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.OnContractSettingsOpenLink);
			// 
			// pnSections
			// 
			this.pnSections.Controls.Add(this.xtraTabControlSections);
			this.pnSections.Controls.Add(this.retractableBarControl);
			this.pnSections.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnSections.Location = new System.Drawing.Point(0, 0);
			this.pnSections.Name = "pnSections";
			this.pnSections.Size = new System.Drawing.Size(1011, 593);
			this.pnSections.TabIndex = 6;
			// 
			// SummaryContainer
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnSections);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "SummaryContainer";
			this.Size = new System.Drawing.Size(1011, 593);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlSections)).EndInit();
			this.retractableBarControl.Content.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlOptions)).EndInit();
			this.pnInfoBottom.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditInfoContract.Properties)).EndInit();
			this.pnSections.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		protected Common.GUI.RetractableBar.RetractableBarLeft retractableBarControl;
		private Panel pnInfoBottom;
		private DevExpress.XtraEditors.HyperLinkEdit hyperLinkEditInfoContract;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlSections;
		private Panel pnSections;
		protected DevExpress.XtraTab.XtraTabControl xtraTabControlOptions;
    }
}
