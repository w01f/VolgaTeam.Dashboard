namespace NewBizWiz.Dashboard.TabOnlineForms
{
    partial class OnlineScheduleBuilderControl
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
			this.gridControlSchedules = new DevExpress.XtraGrid.GridControl();
			this.gridViewSchedules = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnBusinessName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnScheduleFile = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnLastModifiedDate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumnStatus = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemComboBoxStatus = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.pnNoSlidesWarning = new System.Windows.Forms.Panel();
			this.laNoSlidesWarningText3 = new System.Windows.Forms.Label();
			this.laNoSlidesWarningText2 = new System.Windows.Forms.Label();
			this.pbNoSlidesWarning = new System.Windows.Forms.PictureBox();
			this.laNoSlidesWarningText1 = new System.Windows.Forms.Label();
			this.pnNoDataWarning = new System.Windows.Forms.Panel();
			this.pbNoDataWarning = new System.Windows.Forms.PictureBox();
			this.laNoDataWarning = new System.Windows.Forms.Label();
			this.pbWaterMark = new System.Windows.Forms.PictureBox();
			this.pbTitle = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.gridControlSchedules)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewSchedules)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxStatus)).BeginInit();
			this.pnNoSlidesWarning.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbNoSlidesWarning)).BeginInit();
			this.pnNoDataWarning.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbNoDataWarning)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbWaterMark)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbTitle)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControlSchedules
			// 
			this.gridControlSchedules.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridControlSchedules.Location = new System.Drawing.Point(16, 92);
			this.gridControlSchedules.MainView = this.gridViewSchedules;
			this.gridControlSchedules.Name = "gridControlSchedules";
			this.gridControlSchedules.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit,
            this.repositoryItemComboBoxStatus});
			this.gridControlSchedules.Size = new System.Drawing.Size(630, 410);
			this.gridControlSchedules.TabIndex = 24;
			this.gridControlSchedules.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSchedules});
			this.gridControlSchedules.Click += new System.EventHandler(this.gridControlSchedules_Click);
			// 
			// gridViewSchedules
			// 
			this.gridViewSchedules.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewSchedules.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewSchedules.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gridViewSchedules.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridViewSchedules.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewSchedules.Appearance.Row.Options.UseFont = true;
			this.gridViewSchedules.Appearance.Row.Options.UseTextOptions = true;
			this.gridViewSchedules.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridViewSchedules.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnBusinessName,
            this.gridColumnScheduleFile,
            this.gridColumnLastModifiedDate,
            this.gridColumnStatus});
			this.gridViewSchedules.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridViewSchedules.GridControl = this.gridControlSchedules;
			this.gridViewSchedules.Name = "gridViewSchedules";
			this.gridViewSchedules.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewSchedules.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewSchedules.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewSchedules.OptionsCustomization.AllowFilter = false;
			this.gridViewSchedules.OptionsCustomization.AllowGroup = false;
			this.gridViewSchedules.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewSchedules.OptionsFilter.AllowColumnMRUFilterList = false;
			this.gridViewSchedules.OptionsMenu.EnableColumnMenu = false;
			this.gridViewSchedules.OptionsMenu.EnableFooterMenu = false;
			this.gridViewSchedules.OptionsMenu.EnableGroupPanelMenu = false;
			this.gridViewSchedules.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
			this.gridViewSchedules.OptionsMenu.ShowGroupSortSummaryItems = false;
			this.gridViewSchedules.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewSchedules.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewSchedules.OptionsView.ShowDetailButtons = false;
			this.gridViewSchedules.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewSchedules.OptionsView.ShowGroupPanel = false;
			this.gridViewSchedules.OptionsView.ShowIndicator = false;
			this.gridViewSchedules.RowHeight = 40;
			this.gridViewSchedules.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewSchedules_RowClick);
			this.gridViewSchedules.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewSchedules_FocusedRowChanged);
			this.gridViewSchedules.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewSchedules_CellValueChanged);
			// 
			// gridColumnBusinessName
			// 
			this.gridColumnBusinessName.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnBusinessName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gridColumnBusinessName.Caption = "Advertiser";
			this.gridColumnBusinessName.FieldName = "BusinessName";
			this.gridColumnBusinessName.Name = "gridColumnBusinessName";
			this.gridColumnBusinessName.OptionsColumn.AllowEdit = false;
			this.gridColumnBusinessName.OptionsColumn.ReadOnly = true;
			this.gridColumnBusinessName.Visible = true;
			this.gridColumnBusinessName.VisibleIndex = 0;
			this.gridColumnBusinessName.Width = 181;
			// 
			// gridColumnScheduleFile
			// 
			this.gridColumnScheduleFile.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnScheduleFile.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gridColumnScheduleFile.Caption = "Schedule File";
			this.gridColumnScheduleFile.FieldName = "ShortFileName";
			this.gridColumnScheduleFile.Name = "gridColumnScheduleFile";
			this.gridColumnScheduleFile.OptionsColumn.AllowEdit = false;
			this.gridColumnScheduleFile.OptionsColumn.ReadOnly = true;
			this.gridColumnScheduleFile.Visible = true;
			this.gridColumnScheduleFile.VisibleIndex = 1;
			this.gridColumnScheduleFile.Width = 186;
			// 
			// gridColumnLastModifiedDate
			// 
			this.gridColumnLastModifiedDate.Caption = "Last Modified";
			this.gridColumnLastModifiedDate.ColumnEdit = this.repositoryItemButtonEdit;
			this.gridColumnLastModifiedDate.FieldName = "LastModifiedDate";
			this.gridColumnLastModifiedDate.Name = "gridColumnLastModifiedDate";
			this.gridColumnLastModifiedDate.OptionsColumn.AllowEdit = false;
			this.gridColumnLastModifiedDate.OptionsColumn.ReadOnly = true;
			this.gridColumnLastModifiedDate.Visible = true;
			this.gridColumnLastModifiedDate.VisibleIndex = 2;
			this.gridColumnLastModifiedDate.Width = 150;
			// 
			// repositoryItemButtonEdit
			// 
			this.repositoryItemButtonEdit.AutoHeight = false;
			this.repositoryItemButtonEdit.DisplayFormat.FormatString = "MM/dd/yy h:mmtt";
			this.repositoryItemButtonEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemButtonEdit.EditFormat.FormatString = "MM/dd/yy h:mmtt";
			this.repositoryItemButtonEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
			this.repositoryItemButtonEdit.Name = "repositoryItemButtonEdit";
			this.repositoryItemButtonEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			// 
			// gridColumnStatus
			// 
			this.gridColumnStatus.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridColumnStatus.AppearanceCell.Options.UseFont = true;
			this.gridColumnStatus.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridColumnStatus.Caption = "Status";
			this.gridColumnStatus.ColumnEdit = this.repositoryItemComboBoxStatus;
			this.gridColumnStatus.FieldName = "Status";
			this.gridColumnStatus.Name = "gridColumnStatus";
			this.gridColumnStatus.Visible = true;
			this.gridColumnStatus.VisibleIndex = 3;
			this.gridColumnStatus.Width = 124;
			// 
			// repositoryItemComboBoxStatus
			// 
			this.repositoryItemComboBoxStatus.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxStatus.Appearance.Options.UseFont = true;
			this.repositoryItemComboBoxStatus.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxStatus.AppearanceDisabled.Options.UseFont = true;
			this.repositoryItemComboBoxStatus.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxStatus.AppearanceDropDown.Options.UseFont = true;
			this.repositoryItemComboBoxStatus.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxStatus.AppearanceFocused.Options.UseFont = true;
			this.repositoryItemComboBoxStatus.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
			this.repositoryItemComboBoxStatus.AppearanceReadOnly.Options.UseFont = true;
			this.repositoryItemComboBoxStatus.AutoHeight = false;
			this.repositoryItemComboBoxStatus.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.repositoryItemComboBoxStatus.Name = "repositoryItemComboBoxStatus";
			this.repositoryItemComboBoxStatus.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
			this.repositoryItemComboBoxStatus.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.repositoryItemComboBoxStatus_Closed);
			// 
			// pnNoSlidesWarning
			// 
			this.pnNoSlidesWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnNoSlidesWarning.Controls.Add(this.laNoSlidesWarningText3);
			this.pnNoSlidesWarning.Controls.Add(this.laNoSlidesWarningText2);
			this.pnNoSlidesWarning.Controls.Add(this.pbNoSlidesWarning);
			this.pnNoSlidesWarning.Controls.Add(this.laNoSlidesWarningText1);
			this.pnNoSlidesWarning.Location = new System.Drawing.Point(16, 92);
			this.pnNoSlidesWarning.Name = "pnNoSlidesWarning";
			this.pnNoSlidesWarning.Size = new System.Drawing.Size(630, 410);
			this.pnNoSlidesWarning.TabIndex = 33;
			// 
			// laNoSlidesWarningText3
			// 
			this.laNoSlidesWarningText3.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laNoSlidesWarningText3.ForeColor = System.Drawing.Color.Red;
			this.laNoSlidesWarningText3.Location = new System.Drawing.Point(123, 237);
			this.laNoSlidesWarningText3.Name = "laNoSlidesWarningText3";
			this.laNoSlidesWarningText3.Size = new System.Drawing.Size(519, 145);
			this.laNoSlidesWarningText3.TabIndex = 27;
			this.laNoSlidesWarningText3.Text = "Go to the PowerPoint tab on your minibar and change to another slide background t" +
    "hat will work with the Internet Schedule Builder…";
			this.laNoSlidesWarningText3.UseMnemonic = false;
			// 
			// laNoSlidesWarningText2
			// 
			this.laNoSlidesWarningText2.Font = new System.Drawing.Font("Tahoma", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laNoSlidesWarningText2.ForeColor = System.Drawing.Color.Red;
			this.laNoSlidesWarningText2.Location = new System.Drawing.Point(123, 135);
			this.laNoSlidesWarningText2.Name = "laNoSlidesWarningText2";
			this.laNoSlidesWarningText2.Size = new System.Drawing.Size(519, 58);
			this.laNoSlidesWarningText2.TabIndex = 26;
			this.laNoSlidesWarningText2.Text = "Master Wizard";
			this.laNoSlidesWarningText2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.laNoSlidesWarningText2.UseMnemonic = false;
			// 
			// pbNoSlidesWarning
			// 
			this.pbNoSlidesWarning.Image = global::NewBizWiz.Dashboard.Properties.Resources.RedWarning;
			this.pbNoSlidesWarning.Location = new System.Drawing.Point(6, 47);
			this.pbNoSlidesWarning.Name = "pbNoSlidesWarning";
			this.pbNoSlidesWarning.Size = new System.Drawing.Size(100, 88);
			this.pbNoSlidesWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbNoSlidesWarning.TabIndex = 25;
			this.pbNoSlidesWarning.TabStop = false;
			// 
			// laNoSlidesWarningText1
			// 
			this.laNoSlidesWarningText1.Font = new System.Drawing.Font("Tahoma", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laNoSlidesWarningText1.ForeColor = System.Drawing.Color.Red;
			this.laNoSlidesWarningText1.Location = new System.Drawing.Point(123, 47);
			this.laNoSlidesWarningText1.Name = "laNoSlidesWarningText1";
			this.laNoSlidesWarningText1.Size = new System.Drawing.Size(519, 88);
			this.laNoSlidesWarningText1.TabIndex = 24;
			this.laNoSlidesWarningText1.Text = "The Internet Schedule Builder is NOT available for your current PowerPoint slide " +
    "background:";
			this.laNoSlidesWarningText1.UseMnemonic = false;
			// 
			// pnNoDataWarning
			// 
			this.pnNoDataWarning.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnNoDataWarning.Controls.Add(this.pbNoDataWarning);
			this.pnNoDataWarning.Controls.Add(this.laNoDataWarning);
			this.pnNoDataWarning.Location = new System.Drawing.Point(16, 92);
			this.pnNoDataWarning.Name = "pnNoDataWarning";
			this.pnNoDataWarning.Size = new System.Drawing.Size(630, 410);
			this.pnNoDataWarning.TabIndex = 34;
			// 
			// pbNoDataWarning
			// 
			this.pbNoDataWarning.Image = global::NewBizWiz.Dashboard.Properties.Resources.RedWarning;
			this.pbNoDataWarning.Location = new System.Drawing.Point(6, 47);
			this.pbNoDataWarning.Name = "pbNoDataWarning";
			this.pbNoDataWarning.Size = new System.Drawing.Size(100, 88);
			this.pbNoDataWarning.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
			this.pbNoDataWarning.TabIndex = 25;
			this.pbNoDataWarning.TabStop = false;
			// 
			// laNoDataWarning
			// 
			this.laNoDataWarning.Font = new System.Drawing.Font("Tahoma", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laNoDataWarning.ForeColor = System.Drawing.Color.Red;
			this.laNoDataWarning.Location = new System.Drawing.Point(123, 47);
			this.laNoDataWarning.Name = "laNoDataWarning";
			this.laNoDataWarning.Size = new System.Drawing.Size(519, 363);
			this.laNoDataWarning.TabIndex = 24;
			this.laNoDataWarning.Text = "Your Internet Data is not yet available for your software…\r\n\r\nCheck back in a few" +
    " days…\r\n\r\nIt will be ready soon…";
			this.laNoDataWarning.UseMnemonic = false;
			// 
			// pbWaterMark
			// 
			this.pbWaterMark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pbWaterMark.Image = global::NewBizWiz.Dashboard.Properties.Resources.LableOnline;
			this.pbWaterMark.Location = new System.Drawing.Point(652, 438);
			this.pbWaterMark.Name = "pbWaterMark";
			this.pbWaterMark.Size = new System.Drawing.Size(254, 64);
			this.pbWaterMark.TabIndex = 39;
			this.pbWaterMark.TabStop = false;
			// 
			// pbTitle
			// 
			this.pbTitle.Image = global::NewBizWiz.Dashboard.Properties.Resources.TitleOnline;
			this.pbTitle.Location = new System.Drawing.Point(16, 36);
			this.pbTitle.Name = "pbTitle";
			this.pbTitle.Size = new System.Drawing.Size(630, 47);
			this.pbTitle.TabIndex = 38;
			this.pbTitle.TabStop = false;
			// 
			// OnlineScheduleBuilderControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.pbWaterMark);
			this.Controls.Add(this.pbTitle);
			this.Controls.Add(this.gridControlSchedules);
			this.Controls.Add(this.pnNoDataWarning);
			this.Controls.Add(this.pnNoSlidesWarning);
			this.Name = "OnlineScheduleBuilderControl";
			this.Size = new System.Drawing.Size(909, 515);
			this.Click += new System.EventHandler(this.gridControlSchedules_Click);
			((System.ComponentModel.ISupportInitialize)(this.gridControlSchedules)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewSchedules)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxStatus)).EndInit();
			this.pnNoSlidesWarning.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbNoSlidesWarning)).EndInit();
			this.pnNoDataWarning.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbNoDataWarning)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbWaterMark)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbTitle)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraGrid.GridControl gridControlSchedules;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSchedules;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnBusinessName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnScheduleFile;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnLastModifiedDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxStatus;
        private System.Windows.Forms.Panel pnNoSlidesWarning;
        private System.Windows.Forms.Label laNoSlidesWarningText3;
        private System.Windows.Forms.Label laNoSlidesWarningText2;
        private System.Windows.Forms.PictureBox pbNoSlidesWarning;
        private System.Windows.Forms.Label laNoSlidesWarningText1;
        private System.Windows.Forms.Panel pnNoDataWarning;
        private System.Windows.Forms.PictureBox pbNoDataWarning;
		private System.Windows.Forms.Label laNoDataWarning;
		private System.Windows.Forms.PictureBox pbWaterMark;
		private System.Windows.Forms.PictureBox pbTitle;

    }
}
