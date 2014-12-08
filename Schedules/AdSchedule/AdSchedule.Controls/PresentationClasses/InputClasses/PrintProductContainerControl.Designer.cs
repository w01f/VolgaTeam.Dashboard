namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.InputClasses
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
			this.laScheduleWindow = new System.Windows.Forms.Label();
			this.laAdvertiser = new System.Windows.Forms.Label();
			this.xtraTabControlPublications = new DevExpress.XtraTab.XtraTabControl();
			this.pnHeader.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlPublications)).BeginInit();
			this.SuspendLayout();
			// 
			// pnHeader
			// 
			this.pnHeader.Controls.Add(this.laScheduleWindow);
			this.pnHeader.Controls.Add(this.laAdvertiser);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(785, 30);
			this.pnHeader.TabIndex = 2;
			// 
			// laScheduleWindow
			// 
			this.laScheduleWindow.Dock = System.Windows.Forms.DockStyle.Fill;
			this.laScheduleWindow.Location = new System.Drawing.Point(300, 0);
			this.laScheduleWindow.Name = "laScheduleWindow";
			this.laScheduleWindow.Size = new System.Drawing.Size(485, 30);
			this.laScheduleWindow.TabIndex = 1;
			this.laScheduleWindow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// laAdvertiser
			// 
			this.laAdvertiser.Dock = System.Windows.Forms.DockStyle.Left;
			this.laAdvertiser.Location = new System.Drawing.Point(0, 0);
			this.laAdvertiser.Name = "laAdvertiser";
			this.laAdvertiser.Size = new System.Drawing.Size(300, 30);
			this.laAdvertiser.TabIndex = 2;
			this.laAdvertiser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Panel pnHeader;
		private System.Windows.Forms.Label laScheduleWindow;
        public DevExpress.XtraTab.XtraTabControl xtraTabControlPublications;
        private System.Windows.Forms.Label laAdvertiser;

    }
}
