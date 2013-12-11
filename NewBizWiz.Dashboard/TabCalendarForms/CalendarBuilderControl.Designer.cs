namespace NewBizWiz.Dashboard.TabCalendarForms
{
    partial class CalendarBuilderControl
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
			this.gridControlCalendars = new DevExpress.XtraGrid.GridControl();
			this.gridViewCalendars = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnBusinessName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnCalendarFile = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnLastModifiedDate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumnStatus = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemComboBoxStatus = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.pnNoDataWarning = new System.Windows.Forms.Panel();
			this.pbNoDataWarning = new System.Windows.Forms.PictureBox();
			this.laNoDataWarning = new System.Windows.Forms.Label();
			this.pbWaterMark = new System.Windows.Forms.PictureBox();
			this.pbTitle = new System.Windows.Forms.PictureBox();
			((System.ComponentModel.ISupportInitialize)(this.gridControlCalendars)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewCalendars)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxStatus)).BeginInit();
			this.pnNoDataWarning.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbNoDataWarning)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbWaterMark)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbTitle)).BeginInit();
			this.SuspendLayout();
			// 
			// gridControlCalendars
			// 
			this.gridControlCalendars.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.gridControlCalendars.Location = new System.Drawing.Point(16, 92);
			this.gridControlCalendars.MainView = this.gridViewCalendars;
			this.gridControlCalendars.Name = "gridControlCalendars";
			this.gridControlCalendars.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit,
            this.repositoryItemComboBoxStatus});
			this.gridControlCalendars.Size = new System.Drawing.Size(630, 410);
			this.gridControlCalendars.TabIndex = 25;
			this.gridControlCalendars.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCalendars});
			this.gridControlCalendars.Click += new System.EventHandler(this.gridControlCalendars_Click);
			// 
			// gridViewCalendars
			// 
			this.gridViewCalendars.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewCalendars.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewCalendars.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gridViewCalendars.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridViewCalendars.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewCalendars.Appearance.Row.Options.UseFont = true;
			this.gridViewCalendars.Appearance.Row.Options.UseTextOptions = true;
			this.gridViewCalendars.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridViewCalendars.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnBusinessName,
            this.gridColumnCalendarFile,
            this.gridColumnLastModifiedDate,
            this.gridColumnStatus});
			this.gridViewCalendars.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridViewCalendars.GridControl = this.gridControlCalendars;
			this.gridViewCalendars.Name = "gridViewCalendars";
			this.gridViewCalendars.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewCalendars.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewCalendars.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewCalendars.OptionsCustomization.AllowFilter = false;
			this.gridViewCalendars.OptionsCustomization.AllowGroup = false;
			this.gridViewCalendars.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewCalendars.OptionsFilter.AllowColumnMRUFilterList = false;
			this.gridViewCalendars.OptionsMenu.EnableColumnMenu = false;
			this.gridViewCalendars.OptionsMenu.EnableFooterMenu = false;
			this.gridViewCalendars.OptionsMenu.EnableGroupPanelMenu = false;
			this.gridViewCalendars.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
			this.gridViewCalendars.OptionsMenu.ShowGroupSortSummaryItems = false;
			this.gridViewCalendars.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewCalendars.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewCalendars.OptionsView.ShowDetailButtons = false;
			this.gridViewCalendars.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewCalendars.OptionsView.ShowGroupPanel = false;
			this.gridViewCalendars.OptionsView.ShowIndicator = false;
			this.gridViewCalendars.RowHeight = 40;
			this.gridViewCalendars.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewCalendars_RowClick);
			this.gridViewCalendars.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewCalendars_FocusedRowChanged);
			this.gridViewCalendars.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewCalendars_CellValueChanged);
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
			// gridColumnCalendarFile
			// 
			this.gridColumnCalendarFile.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnCalendarFile.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gridColumnCalendarFile.Caption = "File Name";
			this.gridColumnCalendarFile.FieldName = "ShortFileName";
			this.gridColumnCalendarFile.Name = "gridColumnCalendarFile";
			this.gridColumnCalendarFile.OptionsColumn.AllowEdit = false;
			this.gridColumnCalendarFile.OptionsColumn.ReadOnly = true;
			this.gridColumnCalendarFile.Visible = true;
			this.gridColumnCalendarFile.VisibleIndex = 1;
			this.gridColumnCalendarFile.Width = 186;
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
			this.pnNoDataWarning.TabIndex = 35;
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
			this.laNoDataWarning.Text = "Your Calendar Marketing Data is not yet available for your software…\r\n\r\nCheck bac" +
    "k in a few days…\r\n\r\nIt will be ready soon…";
			this.laNoDataWarning.UseMnemonic = false;
			// 
			// pbWaterMark
			// 
			this.pbWaterMark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.pbWaterMark.Image = global::NewBizWiz.Dashboard.Properties.Resources.LabelCalendar;
			this.pbWaterMark.Location = new System.Drawing.Point(652, 438);
			this.pbWaterMark.Name = "pbWaterMark";
			this.pbWaterMark.Size = new System.Drawing.Size(254, 64);
			this.pbWaterMark.TabIndex = 39;
			this.pbWaterMark.TabStop = false;
			// 
			// pbTitle
			// 
			this.pbTitle.Image = global::NewBizWiz.Dashboard.Properties.Resources.TitleCalendar;
			this.pbTitle.Location = new System.Drawing.Point(16, 36);
			this.pbTitle.Name = "pbTitle";
			this.pbTitle.Size = new System.Drawing.Size(630, 47);
			this.pbTitle.TabIndex = 38;
			this.pbTitle.TabStop = false;
			// 
			// CalendarBuilderControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
			this.Controls.Add(this.pbWaterMark);
			this.Controls.Add(this.pbTitle);
			this.Controls.Add(this.gridControlCalendars);
			this.Controls.Add(this.pnNoDataWarning);
			this.Name = "CalendarBuilderControl";
			this.Size = new System.Drawing.Size(909, 515);
			this.Click += new System.EventHandler(this.gridControlCalendars_Click);
			((System.ComponentModel.ISupportInitialize)(this.gridControlCalendars)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewCalendars)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxStatus)).EndInit();
			this.pnNoDataWarning.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.pbNoDataWarning)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbWaterMark)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbTitle)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraGrid.GridControl gridControlCalendars;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCalendars;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnBusinessName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCalendarFile;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnLastModifiedDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxStatus;
        private System.Windows.Forms.Panel pnNoDataWarning;
        private System.Windows.Forms.PictureBox pbNoDataWarning;
		private System.Windows.Forms.Label laNoDataWarning;
		private System.Windows.Forms.PictureBox pbWaterMark;
		private System.Windows.Forms.PictureBox pbTitle;

    }
}
