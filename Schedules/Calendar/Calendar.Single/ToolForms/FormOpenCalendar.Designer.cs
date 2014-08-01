namespace NewBizWiz.Calendar.Single
{
    partial class FormOpenCalendar
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormOpenCalendar));
			this.barManager = new DevExpress.XtraBars.BarManager();
			this.barToolButtons = new DevExpress.XtraBars.Bar();
			this.barLargeButtonItemOpen = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemDelete = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemExit = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.gridControlCalendar = new DevExpress.XtraGrid.GridControl();
			this.gridViewCalendar = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnBusinessName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnCalendarFile = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnLastModifiedDate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumnStatus = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemComboBoxStatus = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlCalendar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewCalendar)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxStatus)).BeginInit();
			this.SuspendLayout();
			// 
			// barManager
			// 
			this.barManager.AllowCustomization = false;
			this.barManager.AllowItemAnimatedHighlighting = false;
			this.barManager.AllowMoveBarOnToolbar = false;
			this.barManager.AllowQuickCustomization = false;
			this.barManager.AllowShowToolbarsPopup = false;
			this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barToolButtons});
			this.barManager.DockControls.Add(this.barDockControlTop);
			this.barManager.DockControls.Add(this.barDockControlBottom);
			this.barManager.DockControls.Add(this.barDockControlLeft);
			this.barManager.DockControls.Add(this.barDockControlRight);
			this.barManager.Form = this;
			this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barLargeButtonItemOpen,
            this.barLargeButtonItemDelete,
            this.barLargeButtonItemExit});
			this.barManager.MaxItemId = 20;
			// 
			// barToolButtons
			// 
			this.barToolButtons.BarName = "Tools";
			this.barToolButtons.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
			this.barToolButtons.DockCol = 0;
			this.barToolButtons.DockRow = 0;
			this.barToolButtons.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.barToolButtons.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barLargeButtonItemOpen, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barLargeButtonItemDelete, DevExpress.XtraBars.BarItemPaintStyle.Standard),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.barLargeButtonItemExit, DevExpress.XtraBars.BarItemPaintStyle.Standard)});
			this.barToolButtons.OptionsBar.AllowQuickCustomization = false;
			this.barToolButtons.OptionsBar.DisableClose = true;
			this.barToolButtons.OptionsBar.DisableCustomization = true;
			this.barToolButtons.OptionsBar.DrawDragBorder = false;
			this.barToolButtons.OptionsBar.UseWholeRow = true;
			this.barToolButtons.Text = "Tools";
			// 
			// barLargeButtonItemOpen
			// 
			this.barLargeButtonItemOpen.Caption = "Open";
			this.barLargeButtonItemOpen.Glyph = ((System.Drawing.Image)(resources.GetObject("barLargeButtonItemOpen.Glyph")));
			this.barLargeButtonItemOpen.Id = 15;
			this.barLargeButtonItemOpen.Name = "barLargeButtonItemOpen";
			this.barLargeButtonItemOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemOpen_ItemClick);
			// 
			// barLargeButtonItemDelete
			// 
			this.barLargeButtonItemDelete.Caption = "Delete";
			this.barLargeButtonItemDelete.Glyph = ((System.Drawing.Image)(resources.GetObject("barLargeButtonItemDelete.Glyph")));
			this.barLargeButtonItemDelete.Id = 16;
			this.barLargeButtonItemDelete.Name = "barLargeButtonItemDelete";
			this.barLargeButtonItemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemDelete_ItemClick);
			// 
			// barLargeButtonItemExit
			// 
			this.barLargeButtonItemExit.Caption = "Exit";
			this.barLargeButtonItemExit.Glyph = ((System.Drawing.Image)(resources.GetObject("barLargeButtonItemExit.Glyph")));
			this.barLargeButtonItemExit.Id = 18;
			this.barLargeButtonItemExit.Name = "barLargeButtonItemExit";
			this.barLargeButtonItemExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemExit_ItemClick);
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(667, 84);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 603);
			this.barDockControlBottom.Size = new System.Drawing.Size(667, 0);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 84);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 519);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(667, 84);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 519);
			// 
			// gridControlCalendar
			// 
			this.gridControlCalendar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.gridControlCalendar.Location = new System.Drawing.Point(12, 94);
			this.gridControlCalendar.MainView = this.gridViewCalendar;
			this.gridControlCalendar.Name = "gridControlCalendar";
			this.gridControlCalendar.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit,
            this.repositoryItemComboBoxStatus});
			this.gridControlCalendar.Size = new System.Drawing.Size(645, 489);
			this.gridControlCalendar.TabIndex = 40;
			this.gridControlCalendar.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCalendar});
			// 
			// gridViewCalendar
			// 
			this.gridViewCalendar.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewCalendar.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewCalendar.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gridViewCalendar.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
			this.gridViewCalendar.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewCalendar.Appearance.Row.Options.UseFont = true;
			this.gridViewCalendar.Appearance.Row.Options.UseTextOptions = true;
			this.gridViewCalendar.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridViewCalendar.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnBusinessName,
            this.gridColumnCalendarFile,
            this.gridColumnLastModifiedDate,
            this.gridColumnStatus});
			this.gridViewCalendar.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridViewCalendar.GridControl = this.gridControlCalendar;
			this.gridViewCalendar.Name = "gridViewCalendar";
			this.gridViewCalendar.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewCalendar.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewCalendar.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewCalendar.OptionsCustomization.AllowFilter = false;
			this.gridViewCalendar.OptionsCustomization.AllowGroup = false;
			this.gridViewCalendar.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewCalendar.OptionsFilter.AllowColumnMRUFilterList = false;
			this.gridViewCalendar.OptionsMenu.EnableColumnMenu = false;
			this.gridViewCalendar.OptionsMenu.EnableFooterMenu = false;
			this.gridViewCalendar.OptionsMenu.EnableGroupPanelMenu = false;
			this.gridViewCalendar.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
			this.gridViewCalendar.OptionsMenu.ShowGroupSortSummaryItems = false;
			this.gridViewCalendar.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewCalendar.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewCalendar.OptionsView.ShowDetailButtons = false;
			this.gridViewCalendar.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewCalendar.OptionsView.ShowGroupPanel = false;
			this.gridViewCalendar.OptionsView.ShowIndicator = false;
			this.gridViewCalendar.RowHeight = 40;
			this.gridViewCalendar.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.gridViewSchedules_RowClick);
			this.gridViewCalendar.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewSchedules_CellValueChanged);
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
			// FormOpenCalendar
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(667, 603);
			this.Controls.Add(this.gridControlCalendar);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormOpenCalendar";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Open Schedule";
			this.Load += new System.EventHandler(this.FormOpenSchedule_Load);
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlCalendar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewCalendar)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxStatus)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar barToolButtons;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemOpen;
		private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemDelete;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemExit;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
		private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gridControlCalendar;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewCalendar;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnBusinessName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnCalendarFile;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnLastModifiedDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnStatus;
		private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxStatus;
    }
}