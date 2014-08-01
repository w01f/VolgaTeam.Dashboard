namespace NewBizWiz.OnlineSchedule.DigitalPackage.PresentationClasses
{
    partial class ScheduleListControl
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
			DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip5 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem5 = new DevExpress.Utils.ToolTipItem();
			DevExpress.Utils.SuperToolTip superToolTip6 = new DevExpress.Utils.SuperToolTip();
			DevExpress.Utils.ToolTipItem toolTipItem6 = new DevExpress.Utils.ToolTipItem();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.barManager = new DevExpress.XtraBars.BarManager();
			this.barToolbar = new DevExpress.XtraBars.Bar();
			this.barLargeButtonItemAdd = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemClone = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemDelete = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barLargeButtonItemHelp = new DevExpress.XtraBars.BarLargeButtonItem();
			this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
			this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
			this.gridControlFiles = new DevExpress.XtraGrid.GridControl();
			this.gridViewFiles = new DevExpress.XtraGrid.Views.Grid.GridView();
			this.gridColumnName = new DevExpress.XtraGrid.Columns.GridColumn();
			this.toolTipController = new DevExpress.Utils.ToolTipController();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlFiles)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewFiles)).BeginInit();
			this.SuspendLayout();
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
			// barManager
			// 
			this.barManager.AllowCustomization = false;
			this.barManager.AllowMoveBarOnToolbar = false;
			this.barManager.AllowQuickCustomization = false;
			this.barManager.AllowShowToolbarsPopup = false;
			this.barManager.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.barToolbar});
			this.barManager.DockControls.Add(this.barDockControlTop);
			this.barManager.DockControls.Add(this.barDockControlBottom);
			this.barManager.DockControls.Add(this.barDockControlLeft);
			this.barManager.DockControls.Add(this.barDockControlRight);
			this.barManager.Form = this;
			this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barLargeButtonItemAdd,
            this.barLargeButtonItemClone,
            this.barLargeButtonItemDelete,
            this.barLargeButtonItemHelp});
			this.barManager.MaxItemId = 14;
			// 
			// barToolbar
			// 
			this.barToolbar.BarName = "Toolbar";
			this.barToolbar.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
			this.barToolbar.DockCol = 0;
			this.barToolbar.DockRow = 0;
			this.barToolbar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
			this.barToolbar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemAdd),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemClone),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemDelete),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemHelp)});
			this.barToolbar.OptionsBar.AllowQuickCustomization = false;
			this.barToolbar.OptionsBar.DrawDragBorder = false;
			this.barToolbar.OptionsBar.UseWholeRow = true;
			this.barToolbar.Text = "Toolbar";
			// 
			// barLargeButtonItemAdd
			// 
			this.barLargeButtonItemAdd.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Left;
			this.barLargeButtonItemAdd.Caption = "Add";
			this.barLargeButtonItemAdd.Glyph = global::NewBizWiz.OnlineSchedule.DigitalPackage.Properties.Resources.AddSchedule;
			this.barLargeButtonItemAdd.Id = 10;
			this.barLargeButtonItemAdd.Name = "barLargeButtonItemAdd";
			toolTipItem4.Text = "Create Schedule";
			superToolTip4.Items.Add(toolTipItem4);
			this.barLargeButtonItemAdd.SuperTip = superToolTip4;
			this.barLargeButtonItemAdd.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemAdd_ItemClick);
			// 
			// barLargeButtonItemClone
			// 
			this.barLargeButtonItemClone.Caption = "Clone";
			this.barLargeButtonItemClone.Glyph = global::NewBizWiz.OnlineSchedule.DigitalPackage.Properties.Resources.CloneSchedule;
			this.barLargeButtonItemClone.Id = 11;
			this.barLargeButtonItemClone.Name = "barLargeButtonItemClone";
			toolTipItem5.Text = "Clone Schedule";
			superToolTip5.Items.Add(toolTipItem5);
			this.barLargeButtonItemClone.SuperTip = superToolTip5;
			this.barLargeButtonItemClone.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemClone_ItemClick);
			// 
			// barLargeButtonItemDelete
			// 
			this.barLargeButtonItemDelete.Caption = "Delete";
			this.barLargeButtonItemDelete.Glyph = global::NewBizWiz.OnlineSchedule.DigitalPackage.Properties.Resources.DeleteSchedule;
			this.barLargeButtonItemDelete.Id = 12;
			this.barLargeButtonItemDelete.Name = "barLargeButtonItemDelete";
			toolTipItem6.Text = "Delete Schedule";
			superToolTip6.Items.Add(toolTipItem6);
			this.barLargeButtonItemDelete.SuperTip = superToolTip6;
			this.barLargeButtonItemDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemDelete_ItemClick);
			// 
			// barLargeButtonItemHelp
			// 
			this.barLargeButtonItemHelp.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
			this.barLargeButtonItemHelp.Caption = "help";
			this.barLargeButtonItemHelp.Glyph = global::NewBizWiz.OnlineSchedule.DigitalPackage.Properties.Resources.HelpSmall;
			this.barLargeButtonItemHelp.Id = 13;
			this.barLargeButtonItemHelp.Name = "barLargeButtonItemHelp";
			this.barLargeButtonItemHelp.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemHelp_ItemClick);
			// 
			// barDockControlTop
			// 
			this.barDockControlTop.CausesValidation = false;
			this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
			this.barDockControlTop.Size = new System.Drawing.Size(300, 48);
			// 
			// barDockControlBottom
			// 
			this.barDockControlBottom.CausesValidation = false;
			this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.barDockControlBottom.Location = new System.Drawing.Point(0, 521);
			this.barDockControlBottom.Size = new System.Drawing.Size(300, 0);
			// 
			// barDockControlLeft
			// 
			this.barDockControlLeft.CausesValidation = false;
			this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
			this.barDockControlLeft.Location = new System.Drawing.Point(0, 48);
			this.barDockControlLeft.Size = new System.Drawing.Size(0, 473);
			// 
			// barDockControlRight
			// 
			this.barDockControlRight.CausesValidation = false;
			this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
			this.barDockControlRight.Location = new System.Drawing.Point(300, 48);
			this.barDockControlRight.Size = new System.Drawing.Size(0, 473);
			// 
			// gridControlFiles
			// 
			this.gridControlFiles.Cursor = System.Windows.Forms.Cursors.Default;
			this.gridControlFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gridControlFiles.Location = new System.Drawing.Point(0, 48);
			this.gridControlFiles.MainView = this.gridViewFiles;
			this.gridControlFiles.Name = "gridControlFiles";
			this.gridControlFiles.Size = new System.Drawing.Size(300, 473);
			this.gridControlFiles.TabIndex = 50;
			this.gridControlFiles.ToolTipController = this.toolTipController;
			this.gridControlFiles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewFiles});
			// 
			// gridViewFiles
			// 
			this.gridViewFiles.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewFiles.Appearance.FocusedCell.Options.UseFont = true;
			this.gridViewFiles.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewFiles.Appearance.FocusedRow.Options.UseFont = true;
			this.gridViewFiles.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gridViewFiles.Appearance.Row.Options.UseFont = true;
			this.gridViewFiles.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
			this.gridViewFiles.Appearance.SelectedRow.Options.UseFont = true;
			this.gridViewFiles.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnName});
			this.gridViewFiles.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
			this.gridViewFiles.GridControl = this.gridControlFiles;
			this.gridViewFiles.Name = "gridViewFiles";
			this.gridViewFiles.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewFiles.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
			this.gridViewFiles.OptionsBehavior.AutoPopulateColumns = false;
			this.gridViewFiles.OptionsBehavior.Editable = false;
			this.gridViewFiles.OptionsBehavior.ReadOnly = true;
			this.gridViewFiles.OptionsCustomization.AllowColumnMoving = false;
			this.gridViewFiles.OptionsCustomization.AllowColumnResizing = false;
			this.gridViewFiles.OptionsCustomization.AllowFilter = false;
			this.gridViewFiles.OptionsCustomization.AllowGroup = false;
			this.gridViewFiles.OptionsCustomization.AllowQuickHideColumns = false;
			this.gridViewFiles.OptionsCustomization.AllowSort = false;
			this.gridViewFiles.OptionsSelection.EnableAppearanceFocusedCell = false;
			this.gridViewFiles.OptionsSelection.EnableAppearanceFocusedRow = false;
			this.gridViewFiles.OptionsSelection.EnableAppearanceHideSelection = false;
			this.gridViewFiles.OptionsView.ShowColumnHeaders = false;
			this.gridViewFiles.OptionsView.ShowDetailButtons = false;
			this.gridViewFiles.OptionsView.ShowGroupExpandCollapseButtons = false;
			this.gridViewFiles.OptionsView.ShowGroupPanel = false;
			this.gridViewFiles.OptionsView.ShowIndicator = false;
			this.gridViewFiles.RowHeight = 30;
			this.gridViewFiles.RowCellClick += new DevExpress.XtraGrid.Views.Grid.RowCellClickEventHandler(this.gridViewFiles_RowCellClick);
			this.gridViewFiles.MouseMove += new System.Windows.Forms.MouseEventHandler(this.gridViewFiles_MouseMove);
			// 
			// gridColumnName
			// 
			this.gridColumnName.Caption = "Schedule Name";
			this.gridColumnName.FieldName = "ShortFileName";
			this.gridColumnName.Name = "gridColumnName";
			this.gridColumnName.Visible = true;
			this.gridColumnName.VisibleIndex = 0;
			// 
			// toolTipController
			// 
			this.toolTipController.Rounded = true;
			this.toolTipController.ShowShadow = false;
			this.toolTipController.GetActiveObjectInfo += new DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventHandler(this.toolTipController_GetActiveObjectInfo);
			// 
			// ScheduleListControl
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.gridControlFiles);
			this.Controls.Add(this.barDockControlLeft);
			this.Controls.Add(this.barDockControlRight);
			this.Controls.Add(this.barDockControlBottom);
			this.Controls.Add(this.barDockControlTop);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "ScheduleListControl";
			this.Size = new System.Drawing.Size(300, 521);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridControlFiles)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.gridViewFiles)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

		private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
		private DevExpress.XtraBars.Bar barToolbar;
		private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemAdd;
		private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemClone;
		private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemDelete;
		private DevExpress.XtraGrid.GridControl gridControlFiles;
		private DevExpress.XtraGrid.Views.Grid.GridView gridViewFiles;
		private DevExpress.XtraGrid.Columns.GridColumn gridColumnName;
		private DevExpress.Utils.ToolTipController toolTipController;
		private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemHelp;
    }
}
