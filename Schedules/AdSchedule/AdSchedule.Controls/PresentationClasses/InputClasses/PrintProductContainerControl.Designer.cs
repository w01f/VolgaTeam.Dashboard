namespace Asa.AdSchedule.Controls.PresentationClasses.InputClasses
{
    partial class PrintProductContainerControl
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
			this.pnHeader = new System.Windows.Forms.Panel();
			this.xtraTabControlPublications = new DevExpress.XtraTab.XtraTabControl();
			this.labelControlScheduleInfo = new DevExpress.XtraEditors.LabelControl();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.pnHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlPublications)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.SuspendLayout();
			// 
			// pnHeader
			// 
			this.pnHeader.Controls.Add(this.labelControlScheduleInfo);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(785, 30);
			this.pnHeader.TabIndex = 2;
			// 
			// xtraTabControlPublications
			// 
			this.xtraTabControlPublications.AllowDrop = true;
			this.xtraTabControlPublications.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlPublications.Appearance.Options.UseFont = true;
			this.xtraTabControlPublications.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlPublications.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlPublications.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlPublications.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlPublications.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlPublications.Location = new System.Drawing.Point(0, 30);
			this.xtraTabControlPublications.Name = "xtraTabControlPublications";
			this.xtraTabControlPublications.Size = new System.Drawing.Size(785, 400);
			this.xtraTabControlPublications.TabIndex = 3;
			this.xtraTabControlPublications.MouseDown += new System.Windows.Forms.MouseEventHandler(this.xtraTabControlPublications_MouseDown);
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
			this.labelControlScheduleInfo.Size = new System.Drawing.Size(785, 30);
			this.labelControlScheduleInfo.StyleController = this.styleController;
			this.labelControlScheduleInfo.TabIndex = 126;
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
			// PrintProductContainerControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.xtraTabControlPublications);
			this.Controls.Add(this.pnHeader);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "PrintProductContainerControl";
			this.Size = new System.Drawing.Size(785, 430);
			this.Load += new System.EventHandler(this.ScheduleBuilderControl_Load);
			this.pnHeader.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlPublications)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Panel pnHeader;
		public DevExpress.XtraTab.XtraTabControl xtraTabControlPublications;
		protected DevExpress.XtraEditors.LabelControl labelControlScheduleInfo;
		private DevExpress.XtraEditors.StyleController styleController;

    }
}
