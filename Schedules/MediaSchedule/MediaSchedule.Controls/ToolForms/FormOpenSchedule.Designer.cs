namespace Asa.Media.Controls.ToolForms
{
    partial class FormOpenSchedule
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
			this.components = new System.ComponentModel.Container();
			this.barManager = new DevExpress.XtraBars.BarManager(this.components);
			this.barToolButtons = new DevExpress.XtraBars.Bar();
			this.barStaticItemLogo = new DevExpress.XtraBars.BarStaticItem();
			this.barLargeButtonItemOpen = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemDelete = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemExit = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.gridControlSchedules = new DevExpress.XtraGrid.GridControl();
			this.gridViewSchedules = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnBusinessName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnScheduleFile = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnLastModifiedDate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumnStatus = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemComboBoxStatus = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController(this.components);
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlSchedules)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewSchedules)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxStatus)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
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
			this.barManager.Controller = this.barAndDockingController;
			this.barManager.DockControls.Add(this.barDockControlTop);
			this.barManager.DockControls.Add(this.barDockControlBottom);
			this.barManager.DockControls.Add(this.barDockControlLeft);
			this.barManager.DockControls.Add(this.barDockControlRight);
			this.barManager.Form = this;
			this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barLargeButtonItemOpen,
            this.barLargeButtonItemDelete,
            this.barLargeButtonItemExit,
            this.barStaticItemLogo});
			this.barManager.MaxItemId = 21;
			// 
			// barToolButtons
			// 
			this.barToolButtons.BarName = "Tools";
			this.barToolButtons.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
			this.barToolButtons.DockCol = 0;
			this.barToolButtons.DockRow = 0;
			this.barToolButtons.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.barToolButtons.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barStaticItemLogo),
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
			// barStaticItemLogo
			// 
			this.barStaticItemLogo.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
			this.barStaticItemLogo.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
			this.barStaticItemLogo.Glyph = global::Asa.Media.Controls.Properties.Resources.TVRibbonLogo;
			this.barStaticItemLogo.Id = 20;
			this.barStaticItemLogo.Name = "barStaticItemLogo";
			this.barStaticItemLogo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.barStaticItemLogo.TextAlignment = System.Drawing.StringAlignment.Near;
			// 
			// barLargeButtonItemOpen
			// 
			this.barLargeButtonItemOpen.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.barLargeButtonItemOpen.Caption = "Open";
			this.barLargeButtonItemOpen.Glyph = global::Asa.Media.Controls.Properties.Resources.OpenSchedule;
			this.barLargeButtonItemOpen.Id = 15;
			this.barLargeButtonItemOpen.Name = "barLargeButtonItemOpen";
			this.barLargeButtonItemOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnScheduleOpenItemClick);
			// 
			// barLargeButtonItemDelete
			// 
			this.barLargeButtonItemDelete.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.barLargeButtonItemDelete.Caption = "Delete";
			this.barLargeButtonItemDelete.Glyph = global::Asa.Media.Controls.Properties.Resources.DeleteSchedule;
			this.barLargeButtonItemDelete.Id = 16;
			this.barLargeButtonItemDelete.Name = "barLargeButtonItemDelete";
			this.barLargeButtonItemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnScheduleDeleteDeleteItemClick);
			// 
			// barLargeButtonItemExit
			// 
			this.barLargeButtonItemExit.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.barLargeButtonItemExit.Caption = "Exit";
			this.barLargeButtonItemExit.Glyph = global::Asa.Media.Controls.Properties.Resources.Exit;
			this.barLargeButtonItemExit.Id = 18;
			this.barLargeButtonItemExit.Name = "barLargeButtonItemExit";
			this.barLargeButtonItemExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnExitItemClick);
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.ForeColor = System.Drawing.Color.Black;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(667, 87);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.ForeColor = System.Drawing.Color.Black;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 603);
			this.barDockControlBottom.Size = new System.Drawing.Size(667, 0);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.ForeColor = System.Drawing.Color.Black;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 87);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 516);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.ForeColor = System.Drawing.Color.Black;
			this.barDockControlRight.Location = new System.Drawing.Point(667, 87);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 516);
			// 
			// gridControlSchedules
			// 
			this.gridControlSchedules.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlSchedules.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlSchedules.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.White;
			this.gridControlSchedules.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gridControlSchedules.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			this.gridControlSchedules.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
			this.gridControlSchedules.Location = new System.Drawing.Point(0, 87);
			this.gridControlSchedules.MainView = this.gridViewSchedules;
			this.gridControlSchedules.Name = "gridControlSchedules";
			this.gridControlSchedules.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit,
            this.repositoryItemComboBoxStatus});
			this.gridControlSchedules.Size = new System.Drawing.Size(667, 516);
			this.gridControlSchedules.TabIndex = 40;
			this.gridControlSchedules.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSchedules});
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
			this.gridViewSchedules.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.OnSchedulesViewRowClick);
			this.gridViewSchedules.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.OnScheduleStatusChanged);
			// 
			// gridColumnBusinessName
			// 
			this.gridColumnBusinessName.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnBusinessName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gridColumnBusinessName.Caption = "Advertiser";
			this.gridColumnBusinessName.FieldName = "Advertiser";
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
			this.gridColumnScheduleFile.FieldName = "Name";
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
			this.gridColumnLastModifiedDate.FieldName = "LastModified";
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
			this.repositoryItemComboBoxStatus.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.OnStatusComboBoxClosed);
			// 
			// barAndDockingController
			// 
			this.barAndDockingController.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
			this.barAndDockingController.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
			this.barAndDockingController.PropertiesBar.ScaleIcons = false;
			// 
			// FormOpenSchedule
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(667, 603);
			this.Controls.Add(this.gridControlSchedules);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FormOpenSchedule";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Open Schedule";
			this.Load += new System.EventHandler(this.FormOpenSchedule_Load);
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlSchedules)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewSchedules)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxStatus)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

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
        private DevExpress.XtraGrid.GridControl gridControlSchedules;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSchedules;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnBusinessName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnScheduleFile;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnLastModifiedDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxStatus;
		private DevExpress.XtraBars.BarStaticItem barStaticItemLogo;
		private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
	}
}