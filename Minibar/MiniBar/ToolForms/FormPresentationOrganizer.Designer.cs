namespace NewBizWiz.MiniBar.ToolForms
{
    partial class FormPresentationOrganizer
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
			this.laTitle = new System.Windows.Forms.Label();
			this.buttonXContentsAdd = new DevComponents.DotNetBar.ButtonX();
			this.buttonXContentsDelete = new DevComponents.DotNetBar.ButtonX();
			this.buttonXClose = new DevComponents.DotNetBar.ButtonX();
			this.buttonXAutoUpdate = new DevComponents.DotNetBar.ButtonX();
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageContents = new DevExpress.XtraTab.XtraTabPage();
			this.pnContents = new System.Windows.Forms.Panel();
			this.xtraTabPageHeader = new DevExpress.XtraTab.XtraTabPage();
			this.pnHeader = new System.Windows.Forms.Panel();
			this.buttonXHeaderAdd = new DevComponents.DotNetBar.ButtonX();
			this.buttonXHeaderReplace = new DevComponents.DotNetBar.ButtonX();
			this.xtraTabPageNumbers = new DevExpress.XtraTab.XtraTabPage();
			this.pnNumbers = new System.Windows.Forms.Panel();
			this.buttonXNumbersAdd = new DevComponents.DotNetBar.ButtonX();
			this.buttonXNumbersDelete = new DevComponents.DotNetBar.ButtonX();
			this.buttonXHeaderDelete = new DevComponents.DotNetBar.ButtonX();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.xtraTabControl.SuspendLayout();
			this.xtraTabPageContents.SuspendLayout();
			this.pnContents.SuspendLayout();
			this.xtraTabPageHeader.SuspendLayout();
			this.pnHeader.SuspendLayout();
			this.xtraTabPageNumbers.SuspendLayout();
			this.pnNumbers.SuspendLayout();
			this.SuspendLayout();
			// 
			// laTitle
			// 
			this.laTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.laTitle.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTitle.Location = new System.Drawing.Point(12, 3);
			this.laTitle.Name = "laTitle";
			this.laTitle.Size = new System.Drawing.Size(319, 47);
			this.laTitle.TabIndex = 1;
			this.laTitle.Text = "Add or Remove a Contents Slide";
			this.laTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonXContentsAdd
			// 
			this.buttonXContentsAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXContentsAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXContentsAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXContentsAdd.Location = new System.Drawing.Point(15, 18);
			this.buttonXContentsAdd.Name = "buttonXContentsAdd";
			this.buttonXContentsAdd.Size = new System.Drawing.Size(316, 41);
			this.buttonXContentsAdd.TabIndex = 2;
			this.buttonXContentsAdd.Text = "Add Contents Slide";
			this.buttonXContentsAdd.TextColor = System.Drawing.Color.Black;
			this.buttonXContentsAdd.Click += new System.EventHandler(this.buttonXAddContents_Click);
			// 
			// buttonXContentsDelete
			// 
			this.buttonXContentsDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXContentsDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXContentsDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXContentsDelete.Location = new System.Drawing.Point(15, 101);
			this.buttonXContentsDelete.Name = "buttonXContentsDelete";
			this.buttonXContentsDelete.Size = new System.Drawing.Size(316, 41);
			this.buttonXContentsDelete.TabIndex = 4;
			this.buttonXContentsDelete.Text = "Delete Contents Slide";
			this.buttonXContentsDelete.TextColor = System.Drawing.Color.Black;
			this.buttonXContentsDelete.Click += new System.EventHandler(this.buttonXDeleteContents_Click);
			// 
			// buttonXClose
			// 
			this.buttonXClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.buttonXClose.Location = new System.Drawing.Point(195, 317);
			this.buttonXClose.Name = "buttonXClose";
			this.buttonXClose.Size = new System.Drawing.Size(136, 41);
			this.buttonXClose.TabIndex = 5;
			this.buttonXClose.Text = "CANCEL/CLOSE";
			this.buttonXClose.TextColor = System.Drawing.Color.Black;
			// 
			// buttonXAutoUpdate
			// 
			this.buttonXAutoUpdate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXAutoUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.buttonXAutoUpdate.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXAutoUpdate.Location = new System.Drawing.Point(15, 317);
			this.buttonXAutoUpdate.Name = "buttonXAutoUpdate";
			this.buttonXAutoUpdate.Size = new System.Drawing.Size(136, 41);
			this.buttonXAutoUpdate.TabIndex = 6;
			this.buttonXAutoUpdate.Text = "Auto Update";
			this.buttonXAutoUpdate.TextColor = System.Drawing.Color.Black;
			this.buttonXAutoUpdate.Click += new System.EventHandler(this.buttonXAutoUpdate_Click);
			// 
			// xtraTabControl
			// 
			this.xtraTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.xtraTabControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.Appearance.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.xtraTabControl.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControl.Location = new System.Drawing.Point(-1, 47);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.SelectedTabPage = this.xtraTabPageContents;
			this.xtraTabControl.Size = new System.Drawing.Size(347, 264);
			this.xtraTabControl.TabIndex = 7;
			this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageContents,
            this.xtraTabPageHeader,
            this.xtraTabPageNumbers});
			this.xtraTabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.xtraTabControl_SelectedPageChanged);
			// 
			// xtraTabPageContents
			// 
			this.xtraTabPageContents.Controls.Add(this.pnContents);
			this.xtraTabPageContents.Name = "xtraTabPageContents";
			this.xtraTabPageContents.Size = new System.Drawing.Size(345, 238);
			this.xtraTabPageContents.Text = "Contents Slide";
			// 
			// pnContents
			// 
			this.pnContents.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.pnContents.Controls.Add(this.buttonXContentsAdd);
			this.pnContents.Controls.Add(this.buttonXContentsDelete);
			this.pnContents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnContents.Location = new System.Drawing.Point(0, 0);
			this.pnContents.Name = "pnContents";
			this.pnContents.Size = new System.Drawing.Size(345, 238);
			this.pnContents.TabIndex = 5;
			// 
			// xtraTabPageHeader
			// 
			this.xtraTabPageHeader.Controls.Add(this.pnHeader);
			this.xtraTabPageHeader.Name = "xtraTabPageHeader";
			this.xtraTabPageHeader.Size = new System.Drawing.Size(345, 238);
			this.xtraTabPageHeader.Text = "Header";
			// 
			// pnHeader
			// 
			this.pnHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.pnHeader.Controls.Add(this.buttonXHeaderDelete);
			this.pnHeader.Controls.Add(this.buttonXHeaderAdd);
			this.pnHeader.Controls.Add(this.buttonXHeaderReplace);
			this.pnHeader.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnHeader.Location = new System.Drawing.Point(0, 0);
			this.pnHeader.Name = "pnHeader";
			this.pnHeader.Size = new System.Drawing.Size(345, 238);
			this.pnHeader.TabIndex = 6;
			// 
			// buttonXHeaderAdd
			// 
			this.buttonXHeaderAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXHeaderAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXHeaderAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXHeaderAdd.Location = new System.Drawing.Point(15, 18);
			this.buttonXHeaderAdd.Name = "buttonXHeaderAdd";
			this.buttonXHeaderAdd.Size = new System.Drawing.Size(316, 41);
			this.buttonXHeaderAdd.TabIndex = 5;
			this.buttonXHeaderAdd.Text = "Add a Slide Header to the Active Slide";
			this.buttonXHeaderAdd.TextColor = System.Drawing.Color.Black;
			this.buttonXHeaderAdd.Click += new System.EventHandler(this.buttonXHeaderAdd_Click);
			// 
			// buttonXHeaderReplace
			// 
			this.buttonXHeaderReplace.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXHeaderReplace.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXHeaderReplace.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXHeaderReplace.Location = new System.Drawing.Point(15, 101);
			this.buttonXHeaderReplace.Name = "buttonXHeaderReplace";
			this.buttonXHeaderReplace.Size = new System.Drawing.Size(316, 41);
			this.buttonXHeaderReplace.TabIndex = 6;
			this.buttonXHeaderReplace.Text = "Replace all Slide Headers on All Slides with NEW Headers";
			this.buttonXHeaderReplace.TextColor = System.Drawing.Color.Black;
			this.buttonXHeaderReplace.Click += new System.EventHandler(this.buttonXHeaderReplace_Click);
			// 
			// xtraTabPageNumbers
			// 
			this.xtraTabPageNumbers.Controls.Add(this.pnNumbers);
			this.xtraTabPageNumbers.Name = "xtraTabPageNumbers";
			this.xtraTabPageNumbers.Size = new System.Drawing.Size(345, 238);
			this.xtraTabPageNumbers.Text = "Numbers";
			// 
			// pnNumbers
			// 
			this.pnNumbers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.pnNumbers.Controls.Add(this.buttonXNumbersAdd);
			this.pnNumbers.Controls.Add(this.buttonXNumbersDelete);
			this.pnNumbers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnNumbers.Location = new System.Drawing.Point(0, 0);
			this.pnNumbers.Name = "pnNumbers";
			this.pnNumbers.Size = new System.Drawing.Size(345, 238);
			this.pnNumbers.TabIndex = 6;
			// 
			// buttonXNumbersAdd
			// 
			this.buttonXNumbersAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXNumbersAdd.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXNumbersAdd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXNumbersAdd.Location = new System.Drawing.Point(15, 18);
			this.buttonXNumbersAdd.Name = "buttonXNumbersAdd";
			this.buttonXNumbersAdd.Size = new System.Drawing.Size(316, 41);
			this.buttonXNumbersAdd.TabIndex = 7;
			this.buttonXNumbersAdd.Text = "Show Page Numbers";
			this.buttonXNumbersAdd.TextColor = System.Drawing.Color.Black;
			this.buttonXNumbersAdd.Click += new System.EventHandler(this.buttonXNumbersAdd_Click);
			// 
			// buttonXNumbersDelete
			// 
			this.buttonXNumbersDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXNumbersDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXNumbersDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXNumbersDelete.Location = new System.Drawing.Point(15, 101);
			this.buttonXNumbersDelete.Name = "buttonXNumbersDelete";
			this.buttonXNumbersDelete.Size = new System.Drawing.Size(316, 41);
			this.buttonXNumbersDelete.TabIndex = 8;
			this.buttonXNumbersDelete.Text = "Remove Page Numbers";
			this.buttonXNumbersDelete.TextColor = System.Drawing.Color.Black;
			this.buttonXNumbersDelete.Click += new System.EventHandler(this.buttonXNumbersDelete_Click);
			// 
			// buttonXHeaderDelete
			// 
			this.buttonXHeaderDelete.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.buttonXHeaderDelete.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonXHeaderDelete.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
			this.buttonXHeaderDelete.Location = new System.Drawing.Point(15, 184);
			this.buttonXHeaderDelete.Name = "buttonXHeaderDelete";
			this.buttonXHeaderDelete.Size = new System.Drawing.Size(316, 41);
			this.buttonXHeaderDelete.TabIndex = 7;
			this.buttonXHeaderDelete.Text = "Delete All Slide Headers in this Presentation";
			this.buttonXHeaderDelete.TextColor = System.Drawing.Color.Black;
			this.buttonXHeaderDelete.Click += new System.EventHandler(this.buttonXHeaderDelete_Click);
			// 
			// FormSlideContentTools
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.ClientSize = new System.Drawing.Size(343, 370);
			this.Controls.Add(this.xtraTabControl);
			this.Controls.Add(this.buttonXAutoUpdate);
			this.Controls.Add(this.buttonXClose);
			this.Controls.Add(this.laTitle);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormSlideContentTools";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Presentation Organizer";
			this.TopMost = true;
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.xtraTabControl.ResumeLayout(false);
			this.xtraTabPageContents.ResumeLayout(false);
			this.pnContents.ResumeLayout(false);
			this.xtraTabPageHeader.ResumeLayout(false);
			this.pnHeader.ResumeLayout(false);
			this.xtraTabPageNumbers.ResumeLayout(false);
			this.pnNumbers.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Label laTitle;
        private DevComponents.DotNetBar.ButtonX buttonXContentsAdd;
        private DevComponents.DotNetBar.ButtonX buttonXContentsDelete;
        private DevComponents.DotNetBar.ButtonX buttonXClose;
		private DevComponents.DotNetBar.ButtonX buttonXAutoUpdate;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageContents;
		private System.Windows.Forms.Panel pnContents;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageHeader;
		private System.Windows.Forms.Panel pnHeader;
		private DevComponents.DotNetBar.ButtonX buttonXHeaderAdd;
		private DevComponents.DotNetBar.ButtonX buttonXHeaderReplace;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageNumbers;
		private System.Windows.Forms.Panel pnNumbers;
		private DevComponents.DotNetBar.ButtonX buttonXNumbersAdd;
		private DevComponents.DotNetBar.ButtonX buttonXNumbersDelete;
		private DevComponents.DotNetBar.ButtonX buttonXHeaderDelete;
    }
}