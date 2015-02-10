namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
    partial class OutputBasicOverviewControl
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
			this.xtraTabControlPublications = new DevExpress.XtraTab.XtraTabControl();
			this.pnHeader = new System.Windows.Forms.Panel();
			this.labelControlScheduleInfo = new DevExpress.XtraEditors.LabelControl();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.hyperLinkEditReset = new DevExpress.XtraEditors.HyperLinkEdit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlPublications)).BeginInit();
			this.pnHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditReset.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// xtraTabControlPublications
			// 
			this.xtraTabControlPublications.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlPublications.Appearance.Options.UseFont = true;
			this.xtraTabControlPublications.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlPublications.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlPublications.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlPublications.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlPublications.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlPublications.Location = new System.Drawing.Point(0, 30);
			this.xtraTabControlPublications.Name = "xtraTabControlPublications";
			this.xtraTabControlPublications.Size = new System.Drawing.Size(737, 400);
			this.xtraTabControlPublications.TabIndex = 3;
			this.xtraTabControlPublications.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControlPublications_SelectedPageChanged);
			// 
			// pnHeader
			// 
			this.pnHeader.Controls.Add(this.labelControlScheduleInfo);
			this.pnHeader.Controls.Add(this.hyperLinkEditReset);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(737, 30);
			this.pnHeader.TabIndex = 4;
			// 
			// labelControlScheduleInfo
			// 
			this.labelControlScheduleInfo.AllowHtmlString = true;
			this.labelControlScheduleInfo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlScheduleInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlScheduleInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControlScheduleInfo.Location = new System.Drawing.Point(0, 0);
			this.labelControlScheduleInfo.Name = "labelControlScheduleInfo";
			this.labelControlScheduleInfo.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.labelControlScheduleInfo.Size = new System.Drawing.Size(611, 30);
			this.labelControlScheduleInfo.StyleController = this.styleController;
			this.labelControlScheduleInfo.TabIndex = 127;
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
			// hyperLinkEditReset
			// 
			this.hyperLinkEditReset.Dock = System.Windows.Forms.DockStyle.Right;
			this.hyperLinkEditReset.EditValue = "Reset Defaults";
			this.hyperLinkEditReset.Location = new System.Drawing.Point(611, 0);
			this.hyperLinkEditReset.Name = "hyperLinkEditReset";
			this.hyperLinkEditReset.Properties.AllowFocused = false;
			this.hyperLinkEditReset.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.hyperLinkEditReset.Properties.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.hyperLinkEditReset.Properties.Appearance.ForeColor = System.Drawing.Color.DarkBlue;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseBackColor = true;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseFont = true;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseForeColor = true;
			this.hyperLinkEditReset.Properties.Appearance.Options.UseTextOptions = true;
			this.hyperLinkEditReset.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
			this.hyperLinkEditReset.Properties.AutoHeight = false;
			this.hyperLinkEditReset.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.hyperLinkEditReset.Size = new System.Drawing.Size(126, 30);
			toolTipItem1.Text = "Reset original default data";
			superToolTip1.Items.Add(toolTipItem1);
			this.hyperLinkEditReset.SuperTip = superToolTip1;
			this.hyperLinkEditReset.TabIndex = 105;
			this.hyperLinkEditReset.OpenLink += new DevExpress.XtraEditors.Controls.OpenLinkEventHandler(this.hyperLinkEditReset_OpenLink);
			// 
			// OutputBasicOverviewControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.xtraTabControlPublications);
			this.Controls.Add(this.pnHeader);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "OutputBasicOverviewControl";
			this.Size = new System.Drawing.Size(737, 430);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlPublications)).EndInit();
			this.pnHeader.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.hyperLinkEditReset.Properties)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraTab.XtraTabControl xtraTabControlPublications;
		private System.Windows.Forms.Panel pnHeader;
		protected DevExpress.XtraEditors.HyperLinkEdit hyperLinkEditReset;
		protected DevExpress.XtraEditors.LabelControl labelControlScheduleInfo;
		private DevExpress.XtraEditors.StyleController styleController;

    }
}
