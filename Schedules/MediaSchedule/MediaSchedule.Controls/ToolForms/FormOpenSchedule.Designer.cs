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
			this.barManager = new DevExpress.XtraBars.BarManager();
			this.barToolButtons = new DevExpress.XtraBars.Bar();
			this.barStaticItemLogo = new DevExpress.XtraBars.BarStaticItem();
			this.barLargeButtonItemOpen = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemDelete = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemExit = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barAndDockingController = new DevExpress.XtraBars.BarAndDockingController();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.gridControlSchedules = new DevExpress.XtraGrid.GridControl();
			this.gridViewSchedules = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnSchedulesBusinessName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnSchedulesFile = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnSchedulesLastModifiedDate = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemButtonEdit = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
			this.gridColumnSchedulesStatus = new DevExpress.XtraGrid.Columns.GridColumn();
			this.repositoryItemComboBoxStatus = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
			this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
			this.xtraTabPageSchedules = new DevExpress.XtraTab.XtraTabPage();
			this.xtraTabPageTemplates = new DevExpress.XtraTab.XtraTabPage();
			this.gridControlTemplates = new DevExpress.XtraGrid.GridControl();
			this.gridViewTemplates = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnTemplatesLogin = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnTemplatesBusinessName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnTemplatesFile = new DevExpress.XtraGrid.Columns.GridColumn();
			this.gridColumnTemplatesDate = new DevExpress.XtraGrid.Columns.GridColumn();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlSchedules)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewSchedules)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxStatus)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
			this.xtraTabControl.SuspendLayout();
			this.xtraTabPageSchedules.SuspendLayout();
			this.xtraTabPageTemplates.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.gridControlTemplates)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewTemplates)).BeginInit();
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
			this.barStaticItemLogo.Id = 20;
			this.barStaticItemLogo.ImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.False;
			this.barStaticItemLogo.ImageOptions.Image = global::Asa.Media.Controls.Properties.Resources.RibbonLogo;
			this.barStaticItemLogo.Name = "barStaticItemLogo";
			this.barStaticItemLogo.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
			this.barStaticItemLogo.TextAlignment = System.Drawing.StringAlignment.Near;
			// 
			// barLargeButtonItemOpen
			// 
			this.barLargeButtonItemOpen.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.barLargeButtonItemOpen.Caption = "Open";
			this.barLargeButtonItemOpen.Id = 15;
			this.barLargeButtonItemOpen.ImageOptions.Image = global::Asa.Media.Controls.Properties.Resources.OpenSchedule;
			this.barLargeButtonItemOpen.Name = "barLargeButtonItemOpen";
			this.barLargeButtonItemOpen.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnScheduleOpenItemClick);
			// 
			// barLargeButtonItemDelete
			// 
			this.barLargeButtonItemDelete.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.barLargeButtonItemDelete.Caption = "Delete";
			this.barLargeButtonItemDelete.Id = 16;
			this.barLargeButtonItemDelete.ImageOptions.Image = global::Asa.Media.Controls.Properties.Resources.DeleteSchedule;
			this.barLargeButtonItemDelete.Name = "barLargeButtonItemDelete";
			this.barLargeButtonItemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnScheduleDeleteDeleteItemClick);
			// 
			// barLargeButtonItemExit
			// 
			this.barLargeButtonItemExit.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.barLargeButtonItemExit.Caption = "Exit";
			this.barLargeButtonItemExit.Id = 18;
			this.barLargeButtonItemExit.ImageOptions.Image = global::Asa.Media.Controls.Properties.Resources.Exit;
			this.barLargeButtonItemExit.Name = "barLargeButtonItemExit";
			this.barLargeButtonItemExit.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.OnExitItemClick);
			// 
			// barAndDockingController
			// 
			this.barAndDockingController.PropertiesBar.DefaultGlyphSize = new System.Drawing.Size(16, 16);
			this.barAndDockingController.PropertiesBar.DefaultLargeGlyphSize = new System.Drawing.Size(32, 32);
			this.barAndDockingController.PropertiesBar.ScaleIcons = false;
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.ForeColor = System.Drawing.Color.Black;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Manager = this.barManager;
			this.barDockControlTop.Size = new System.Drawing.Size(667, 84);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.ForeColor = System.Drawing.Color.Black;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 603);
			this.barDockControlBottom.Manager = this.barManager;
			this.barDockControlBottom.Size = new System.Drawing.Size(667, 0);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.ForeColor = System.Drawing.Color.Black;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 84);
			this.barDockControlLeft.Manager = this.barManager;
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 519);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.ForeColor = System.Drawing.Color.Black;
			this.barDockControlRight.Location = new System.Drawing.Point(667, 84);
			this.barDockControlRight.Manager = this.barManager;
			this.barDockControlRight.Size = new System.Drawing.Size(0, 519);
			// 
			// gridControlSchedules
			// 
			this.gridControlSchedules.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlSchedules.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlSchedules.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.White;
			this.gridControlSchedules.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gridControlSchedules.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			this.gridControlSchedules.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
			this.gridControlSchedules.Location = new System.Drawing.Point(0, 0);
			this.gridControlSchedules.MainView = this.gridViewSchedules;
			this.gridControlSchedules.Name = "gridControlSchedules";
			this.gridControlSchedules.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemButtonEdit,
            this.repositoryItemComboBoxStatus});
			this.gridControlSchedules.Size = new System.Drawing.Size(665, 491);
			this.gridControlSchedules.TabIndex = 40;
			this.gridControlSchedules.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSchedules});
			// 
			// gridViewSchedules
			// 
			this.gridViewSchedules.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewSchedules.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewSchedules.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gridViewSchedules.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridViewSchedules.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewSchedules.Appearance.Row.Options.UseFont = true;
			this.gridViewSchedules.Appearance.Row.Options.UseTextOptions = true;
			this.gridViewSchedules.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridViewSchedules.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnSchedulesBusinessName,
            this.gridColumnSchedulesFile,
            this.gridColumnSchedulesLastModifiedDate,
            this.gridColumnSchedulesStatus});
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
			// gridColumnSchedulesBusinessName
			// 
			this.gridColumnSchedulesBusinessName.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnSchedulesBusinessName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gridColumnSchedulesBusinessName.Caption = "Advertiser";
			this.gridColumnSchedulesBusinessName.FieldName = "Advertiser";
			this.gridColumnSchedulesBusinessName.Name = "gridColumnSchedulesBusinessName";
			this.gridColumnSchedulesBusinessName.OptionsColumn.AllowEdit = false;
			this.gridColumnSchedulesBusinessName.OptionsColumn.ReadOnly = true;
			this.gridColumnSchedulesBusinessName.Visible = true;
			this.gridColumnSchedulesBusinessName.VisibleIndex = 0;
			this.gridColumnSchedulesBusinessName.Width = 181;
			// 
			// gridColumnSchedulesFile
			// 
			this.gridColumnSchedulesFile.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnSchedulesFile.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gridColumnSchedulesFile.Caption = "File";
			this.gridColumnSchedulesFile.FieldName = "Name";
			this.gridColumnSchedulesFile.Name = "gridColumnSchedulesFile";
			this.gridColumnSchedulesFile.OptionsColumn.AllowEdit = false;
			this.gridColumnSchedulesFile.OptionsColumn.ReadOnly = true;
			this.gridColumnSchedulesFile.Visible = true;
			this.gridColumnSchedulesFile.VisibleIndex = 1;
			this.gridColumnSchedulesFile.Width = 186;
			// 
			// gridColumnSchedulesLastModifiedDate
			// 
			this.gridColumnSchedulesLastModifiedDate.Caption = "Last Modified";
			this.gridColumnSchedulesLastModifiedDate.ColumnEdit = this.repositoryItemButtonEdit;
			this.gridColumnSchedulesLastModifiedDate.FieldName = "LastModified";
			this.gridColumnSchedulesLastModifiedDate.Name = "gridColumnSchedulesLastModifiedDate";
			this.gridColumnSchedulesLastModifiedDate.OptionsColumn.AllowEdit = false;
			this.gridColumnSchedulesLastModifiedDate.OptionsColumn.ReadOnly = true;
			this.gridColumnSchedulesLastModifiedDate.Visible = true;
			this.gridColumnSchedulesLastModifiedDate.VisibleIndex = 2;
			this.gridColumnSchedulesLastModifiedDate.Width = 150;
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
			// gridColumnSchedulesStatus
			// 
			this.gridColumnSchedulesStatus.AppearanceCell.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridColumnSchedulesStatus.AppearanceCell.Options.UseFont = true;
			this.gridColumnSchedulesStatus.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnSchedulesStatus.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridColumnSchedulesStatus.Caption = "Status";
			this.gridColumnSchedulesStatus.ColumnEdit = this.repositoryItemComboBoxStatus;
			this.gridColumnSchedulesStatus.FieldName = "Status";
			this.gridColumnSchedulesStatus.Name = "gridColumnSchedulesStatus";
			this.gridColumnSchedulesStatus.Visible = true;
			this.gridColumnSchedulesStatus.VisibleIndex = 3;
			this.gridColumnSchedulesStatus.Width = 124;
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
			// xtraTabControl
			// 
			this.xtraTabControl.Appearance.BackColor = System.Drawing.Color.White;
			this.xtraTabControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.xtraTabControl.Appearance.ForeColor = System.Drawing.Color.Black;
			this.xtraTabControl.Appearance.Options.UseBackColor = true;
			this.xtraTabControl.Appearance.Options.UseFont = true;
			this.xtraTabControl.Appearance.Options.UseForeColor = true;
			this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControl.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControl.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControl.Location = new System.Drawing.Point(0, 84);
			this.xtraTabControl.Name = "xtraTabControl";
			this.xtraTabControl.SelectedTabPage = this.xtraTabPageSchedules;
			this.xtraTabControl.Size = new System.Drawing.Size(667, 519);
			this.xtraTabControl.TabIndex = 45;
			this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageSchedules,
            this.xtraTabPageTemplates});
			this.xtraTabControl.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.OnSelectedTabPageChanged);
			// 
			// xtraTabPageSchedules
			// 
			this.xtraTabPageSchedules.Appearance.PageClient.ForeColor = System.Drawing.Color.Black;
			this.xtraTabPageSchedules.Appearance.PageClient.Options.UseForeColor = true;
			this.xtraTabPageSchedules.Controls.Add(this.gridControlSchedules);
			this.xtraTabPageSchedules.Name = "xtraTabPageSchedules";
			this.xtraTabPageSchedules.Size = new System.Drawing.Size(665, 491);
			this.xtraTabPageSchedules.Text = "My Solutions";
			// 
			// xtraTabPageTemplates
			// 
			this.xtraTabPageTemplates.Appearance.PageClient.ForeColor = System.Drawing.Color.Black;
			this.xtraTabPageTemplates.Appearance.PageClient.Options.UseForeColor = true;
			this.xtraTabPageTemplates.Controls.Add(this.gridControlTemplates);
			this.xtraTabPageTemplates.Name = "xtraTabPageTemplates";
			this.xtraTabPageTemplates.Size = new System.Drawing.Size(665, 491);
			this.xtraTabPageTemplates.Text = "Public Cloud";
			// 
			// gridControlTemplates
			// 
			this.gridControlTemplates.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlTemplates.EmbeddedNavigator.Appearance.BackColor = System.Drawing.Color.White;
			this.gridControlTemplates.EmbeddedNavigator.Appearance.ForeColor = System.Drawing.Color.Black;
			this.gridControlTemplates.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
			this.gridControlTemplates.EmbeddedNavigator.Appearance.Options.UseForeColor = true;
			this.gridControlTemplates.Location = new System.Drawing.Point(0, 0);
			this.gridControlTemplates.MainView = this.gridViewTemplates;
			this.gridControlTemplates.Name = "gridControlTemplates";
			this.gridControlTemplates.Size = new System.Drawing.Size(665, 491);
			this.gridControlTemplates.TabIndex = 41;
			this.gridControlTemplates.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTemplates});
			// 
			// gridViewTemplates
			// 
			this.gridViewTemplates.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
			this.gridViewTemplates.Appearance.HeaderPanel.Options.UseFont = true;
			this.gridViewTemplates.Appearance.HeaderPanel.Options.UseTextOptions = true;
			this.gridViewTemplates.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridViewTemplates.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewTemplates.Appearance.Row.Options.UseFont = true;
			this.gridViewTemplates.Appearance.Row.Options.UseTextOptions = true;
			this.gridViewTemplates.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
			this.gridViewTemplates.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnTemplatesLogin,
            this.gridColumnTemplatesBusinessName,
            this.gridColumnTemplatesFile,
            this.gridColumnTemplatesDate});
			this.gridViewTemplates.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
			this.gridViewTemplates.GridControl = this.gridControlTemplates;
			this.gridViewTemplates.Name = "gridViewTemplates";
			this.gridViewTemplates.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewTemplates.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewTemplates.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewTemplates.OptionsCustomization.AllowFilter = false;
			this.gridViewTemplates.OptionsCustomization.AllowGroup = false;
			this.gridViewTemplates.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewTemplates.OptionsFilter.AllowColumnMRUFilterList = false;
			this.gridViewTemplates.OptionsMenu.EnableColumnMenu = false;
			this.gridViewTemplates.OptionsMenu.EnableFooterMenu = false;
			this.gridViewTemplates.OptionsMenu.EnableGroupPanelMenu = false;
			this.gridViewTemplates.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
			this.gridViewTemplates.OptionsMenu.ShowGroupSortSummaryItems = false;
			this.gridViewTemplates.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewTemplates.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewTemplates.OptionsView.ShowDetailButtons = false;
			this.gridViewTemplates.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewTemplates.OptionsView.ShowGroupPanel = false;
			this.gridViewTemplates.OptionsView.ShowIndicator = false;
			this.gridViewTemplates.RowHeight = 40;
			this.gridViewTemplates.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.OnTemplatesRowClick);
			// 
			// gridColumnTemplatesLogin
			// 
			this.gridColumnTemplatesLogin.Caption = "User Account";
			this.gridColumnTemplatesLogin.FieldName = "User";
			this.gridColumnTemplatesLogin.Name = "gridColumnTemplatesLogin";
			this.gridColumnTemplatesLogin.OptionsColumn.AllowEdit = false;
			this.gridColumnTemplatesLogin.OptionsColumn.ReadOnly = true;
			this.gridColumnTemplatesLogin.Visible = true;
			this.gridColumnTemplatesLogin.VisibleIndex = 0;
			this.gridColumnTemplatesLogin.Width = 150;
			// 
			// gridColumnTemplatesBusinessName
			// 
			this.gridColumnTemplatesBusinessName.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnTemplatesBusinessName.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gridColumnTemplatesBusinessName.Caption = "Advertiser";
			this.gridColumnTemplatesBusinessName.FieldName = "Advertiser";
			this.gridColumnTemplatesBusinessName.Name = "gridColumnTemplatesBusinessName";
			this.gridColumnTemplatesBusinessName.OptionsColumn.AllowEdit = false;
			this.gridColumnTemplatesBusinessName.OptionsColumn.ReadOnly = true;
			this.gridColumnTemplatesBusinessName.Visible = true;
			this.gridColumnTemplatesBusinessName.VisibleIndex = 1;
			this.gridColumnTemplatesBusinessName.Width = 178;
			// 
			// gridColumnTemplatesFile
			// 
			this.gridColumnTemplatesFile.AppearanceCell.Options.UseTextOptions = true;
			this.gridColumnTemplatesFile.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.gridColumnTemplatesFile.Caption = "File";
			this.gridColumnTemplatesFile.FieldName = "Name";
			this.gridColumnTemplatesFile.Name = "gridColumnTemplatesFile";
			this.gridColumnTemplatesFile.OptionsColumn.AllowEdit = false;
			this.gridColumnTemplatesFile.OptionsColumn.ReadOnly = true;
			this.gridColumnTemplatesFile.Visible = true;
			this.gridColumnTemplatesFile.VisibleIndex = 2;
			this.gridColumnTemplatesFile.Width = 183;
			// 
			// gridColumnTemplatesDate
			// 
			this.gridColumnTemplatesDate.Caption = "Last Modified";
			this.gridColumnTemplatesDate.FieldName = "Date";
			this.gridColumnTemplatesDate.Name = "gridColumnTemplatesDate";
			this.gridColumnTemplatesDate.OptionsColumn.AllowEdit = false;
			this.gridColumnTemplatesDate.OptionsColumn.ReadOnly = true;
			this.gridColumnTemplatesDate.Visible = true;
			this.gridColumnTemplatesDate.VisibleIndex = 3;
			this.gridColumnTemplatesDate.Width = 152;
			// 
			// FormOpenSchedule
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(667, 603);
			this.Controls.Add(this.xtraTabControl);
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
			this.Text = "Open Previous Solutions";
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barAndDockingController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlSchedules)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewSchedules)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxStatus)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
			this.xtraTabControl.ResumeLayout(false);
			this.xtraTabPageSchedules.ResumeLayout(false);
			this.xtraTabPageTemplates.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.gridControlTemplates)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewTemplates)).EndInit();
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
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulesBusinessName;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulesFile;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulesLastModifiedDate;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnSchedulesStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxStatus;
		private DevExpress.XtraBars.BarStaticItem barStaticItemLogo;
		private DevExpress.XtraBars.BarAndDockingController barAndDockingController;
		private DevExpress.XtraTab.XtraTabControl xtraTabControl;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageSchedules;
		private DevExpress.XtraTab.XtraTabPage xtraTabPageTemplates;
		private DevExpress.XtraGrid.GridControl gridControlTemplates;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewTemplates;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnTemplatesBusinessName;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnTemplatesFile;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnTemplatesDate;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnTemplatesLogin;
	}
}