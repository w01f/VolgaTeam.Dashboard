namespace CalendarBuilder.CustomControls.Grid
{
    partial class GridControl
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
            this.components = new System.ComponentModel.Container();
            this.barManager = new DevExpress.XtraBars.BarManager(this.components);
            this.barToolbar = new DevExpress.XtraBars.Bar();
            this.barLargeButtonItemApply = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barLargeButtonItemClose = new DevExpress.XtraBars.BarLargeButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.pnNavbar = new System.Windows.Forms.Panel();
            this.xtraTabControl = new DevExpress.XtraTab.XtraTabControl();
            this.xtraTabPageDigital = new DevExpress.XtraTab.XtraTabPage();
            this.gridControlDigital = new DevExpress.XtraGrid.GridControl();
            this.persistentRepository = new DevExpress.XtraEditors.Repository.PersistentRepository(this.components);
            this.repositoryItemComboBoxDigitalCategory = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxDigitalSubCategory = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxDigitalProduct = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemTextEditCustomComment = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.repositoryItemDateEdit = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.gridViewDigital = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnDigitalCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDigitalSubCategory = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDigitalProduct = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDigitalCustomNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnDigitalDay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.xtraTabPageNewspaper = new DevExpress.XtraTab.XtraTabPage();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.gridControlNewspaper = new DevExpress.XtraGrid.GridControl();
            this.gridViewNewspaper = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnNewspaperPublication = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNewspaperSection = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNewspaperPageSize = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNewspaperCustomNote = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNewspaperDay = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNewspaperColor = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnNewspaperCost = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemComboBoxNewspaperPublication = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxNewspaperSection = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxNewspaperPageSize = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxNewspaperColor = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemSpinEditNewspaperTotalCost = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).BeginInit();
            this.pnNavbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).BeginInit();
            this.xtraTabControl.SuspendLayout();
            this.xtraTabPageDigital.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDigital)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxDigitalCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxDigitalSubCategory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxDigitalProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditCustomComment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit.VistaTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDigital)).BeginInit();
            this.xtraTabPageNewspaper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlNewspaper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNewspaper)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxNewspaperPublication)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxNewspaperSection)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxNewspaperPageSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxNewspaperColor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNewspaperTotalCost)).BeginInit();
            this.SuspendLayout();
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
            this.barManager.Form = this.pnNavbar;
            this.barManager.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.barLargeButtonItemApply,
            this.barLargeButtonItemClose});
            this.barManager.MaxItemId = 7;
            // 
            // barToolbar
            // 
            this.barToolbar.BarName = "Tools";
            this.barToolbar.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Top;
            this.barToolbar.DockCol = 0;
            this.barToolbar.DockRow = 0;
            this.barToolbar.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.barToolbar.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemApply),
            new DevExpress.XtraBars.LinkPersistInfo(this.barLargeButtonItemClose)});
            this.barToolbar.OptionsBar.AllowQuickCustomization = false;
            this.barToolbar.OptionsBar.DrawDragBorder = false;
            this.barToolbar.OptionsBar.UseWholeRow = true;
            this.barToolbar.Text = "Tools";
            // 
            // barLargeButtonItemApply
            // 
            this.barLargeButtonItemApply.Caption = "Apply";
            this.barLargeButtonItemApply.Glyph = global::CalendarBuilder.Properties.Resources.ApplyDayProperties;
            this.barLargeButtonItemApply.Id = 3;
            this.barLargeButtonItemApply.Name = "barLargeButtonItemApply";
            this.barLargeButtonItemApply.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemApply_ItemClick);
            // 
            // barLargeButtonItemClose
            // 
            this.barLargeButtonItemClose.Caption = "Close";
            this.barLargeButtonItemClose.Glyph = global::CalendarBuilder.Properties.Resources.CloseDayProperties;
            this.barLargeButtonItemClose.Id = 6;
            this.barLargeButtonItemClose.Name = "barLargeButtonItemClose";
            this.barLargeButtonItemClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barLargeButtonItemClose_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(818, 44);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 404);
            this.barDockControlBottom.Size = new System.Drawing.Size(818, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 44);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 360);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(818, 44);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 360);
            // 
            // pnNavbar
            // 
            this.pnNavbar.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnNavbar.Controls.Add(this.xtraTabControl);
            this.pnNavbar.Controls.Add(this.barDockControlLeft);
            this.pnNavbar.Controls.Add(this.barDockControlRight);
            this.pnNavbar.Controls.Add(this.barDockControlBottom);
            this.pnNavbar.Controls.Add(this.barDockControlTop);
            this.pnNavbar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnNavbar.Location = new System.Drawing.Point(0, 0);
            this.pnNavbar.Name = "pnNavbar";
            this.pnNavbar.Size = new System.Drawing.Size(822, 408);
            this.pnNavbar.TabIndex = 5;
            // 
            // xtraTabControl
            // 
            this.xtraTabControl.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControl.Appearance.Options.UseFont = true;
            this.xtraTabControl.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControl.AppearancePage.Header.Options.UseFont = true;
            this.xtraTabControl.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControl.AppearancePage.HeaderActive.Options.UseFont = true;
            this.xtraTabControl.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControl.AppearancePage.HeaderDisabled.Options.UseFont = true;
            this.xtraTabControl.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControl.AppearancePage.HeaderHotTracked.Options.UseFont = true;
            this.xtraTabControl.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
            this.xtraTabControl.AppearancePage.PageClient.Options.UseFont = true;
            this.xtraTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl.Location = new System.Drawing.Point(0, 44);
            this.xtraTabControl.Name = "xtraTabControl";
            this.xtraTabControl.SelectedTabPage = this.xtraTabPageDigital;
            this.xtraTabControl.Size = new System.Drawing.Size(818, 360);
            this.xtraTabControl.TabIndex = 4;
            this.xtraTabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPageDigital,
            this.xtraTabPageNewspaper});
            // 
            // xtraTabPageDigital
            // 
            this.xtraTabPageDigital.Controls.Add(this.gridControlDigital);
            this.xtraTabPageDigital.Name = "xtraTabPageDigital";
            this.xtraTabPageDigital.Size = new System.Drawing.Size(816, 334);
            this.xtraTabPageDigital.Text = "Digital";
            // 
            // gridControlDigital
            // 
            this.gridControlDigital.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlDigital.ExternalRepository = this.persistentRepository;
            this.gridControlDigital.Location = new System.Drawing.Point(0, 0);
            this.gridControlDigital.MainView = this.gridViewDigital;
            this.gridControlDigital.MenuManager = this.barManager;
            this.gridControlDigital.Name = "gridControlDigital";
            this.gridControlDigital.Size = new System.Drawing.Size(816, 334);
            this.gridControlDigital.TabIndex = 0;
            this.gridControlDigital.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDigital});
            // 
            // persistentRepository
            // 
            this.persistentRepository.Items.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBoxDigitalCategory,
            this.repositoryItemComboBoxDigitalSubCategory,
            this.repositoryItemComboBoxDigitalProduct,
            this.repositoryItemTextEditCustomComment,
            this.repositoryItemDateEdit,
            this.repositoryItemComboBoxNewspaperPublication,
            this.repositoryItemComboBoxNewspaperSection,
            this.repositoryItemComboBoxNewspaperPageSize,
            this.repositoryItemComboBoxNewspaperColor,
            this.repositoryItemSpinEditNewspaperTotalCost});
            // 
            // repositoryItemComboBoxDigitalCategory
            // 
            this.repositoryItemComboBoxDigitalCategory.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repositoryItemComboBoxDigitalCategory.Appearance.Options.UseFont = true;
            this.repositoryItemComboBoxDigitalCategory.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDigitalCategory.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemComboBoxDigitalCategory.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDigitalCategory.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBoxDigitalCategory.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDigitalCategory.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemComboBoxDigitalCategory.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDigitalCategory.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemComboBoxDigitalCategory.AutoHeight = false;
            this.repositoryItemComboBoxDigitalCategory.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxDigitalCategory.Name = "repositoryItemComboBoxDigitalCategory";
            this.repositoryItemComboBoxDigitalCategory.NullText = "Select Category...";
            this.repositoryItemComboBoxDigitalCategory.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemComboBoxDigitalCategory.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.repositoryItemComboBox_Closed);
            // 
            // repositoryItemComboBoxDigitalSubCategory
            // 
            this.repositoryItemComboBoxDigitalSubCategory.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDigitalSubCategory.Appearance.Options.UseFont = true;
            this.repositoryItemComboBoxDigitalSubCategory.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDigitalSubCategory.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemComboBoxDigitalSubCategory.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDigitalSubCategory.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBoxDigitalSubCategory.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDigitalSubCategory.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemComboBoxDigitalSubCategory.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDigitalSubCategory.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemComboBoxDigitalSubCategory.AutoHeight = false;
            this.repositoryItemComboBoxDigitalSubCategory.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxDigitalSubCategory.Name = "repositoryItemComboBoxDigitalSubCategory";
            this.repositoryItemComboBoxDigitalSubCategory.NullText = "Select Sub-Group...";
            this.repositoryItemComboBoxDigitalSubCategory.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemComboBoxDigitalSubCategory.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.repositoryItemComboBox_Closed);
            // 
            // repositoryItemComboBoxDigitalProduct
            // 
            this.repositoryItemComboBoxDigitalProduct.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDigitalProduct.Appearance.Options.UseFont = true;
            this.repositoryItemComboBoxDigitalProduct.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDigitalProduct.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemComboBoxDigitalProduct.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDigitalProduct.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBoxDigitalProduct.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDigitalProduct.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemComboBoxDigitalProduct.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDigitalProduct.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemComboBoxDigitalProduct.AutoHeight = false;
            this.repositoryItemComboBoxDigitalProduct.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxDigitalProduct.Name = "repositoryItemComboBoxDigitalProduct";
            this.repositoryItemComboBoxDigitalProduct.NullText = "Type or Select Digital Inventory...";
            this.repositoryItemComboBoxDigitalProduct.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.repositoryItemComboBox_Closed);
            // 
            // repositoryItemTextEditCustomComment
            // 
            this.repositoryItemTextEditCustomComment.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemTextEditCustomComment.Appearance.Options.UseFont = true;
            this.repositoryItemTextEditCustomComment.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemTextEditCustomComment.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemTextEditCustomComment.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemTextEditCustomComment.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemTextEditCustomComment.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemTextEditCustomComment.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemTextEditCustomComment.AutoHeight = false;
            this.repositoryItemTextEditCustomComment.Name = "repositoryItemTextEditCustomComment";
            this.repositoryItemTextEditCustomComment.NullText = "Type Custom Comment...";
            // 
            // repositoryItemDateEdit
            // 
            this.repositoryItemDateEdit.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemDateEdit.Appearance.Options.UseFont = true;
            this.repositoryItemDateEdit.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemDateEdit.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemDateEdit.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemDateEdit.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemDateEdit.AppearanceDropDownHeader.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemDateEdit.AppearanceDropDownHeader.Options.UseFont = true;
            this.repositoryItemDateEdit.AppearanceDropDownHeaderHighlight.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemDateEdit.AppearanceDropDownHeaderHighlight.Options.UseFont = true;
            this.repositoryItemDateEdit.AppearanceDropDownHighlight.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemDateEdit.AppearanceDropDownHighlight.Options.UseFont = true;
            this.repositoryItemDateEdit.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemDateEdit.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemDateEdit.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemDateEdit.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemDateEdit.AppearanceWeekNumber.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemDateEdit.AppearanceWeekNumber.Options.UseFont = true;
            this.repositoryItemDateEdit.AutoHeight = false;
            this.repositoryItemDateEdit.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemDateEdit.DisplayFormat.FormatString = "dddd, MM/dd/yyyy";
            this.repositoryItemDateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit.EditFormat.FormatString = "dddd, MM/dd/yyyy";
            this.repositoryItemDateEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.repositoryItemDateEdit.Name = "repositoryItemDateEdit";
            this.repositoryItemDateEdit.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemDateEdit.VistaTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            // 
            // gridViewDigital
            // 
            this.gridViewDigital.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewDigital.Appearance.EvenRow.Options.UseFont = true;
            this.gridViewDigital.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewDigital.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewDigital.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewDigital.Appearance.OddRow.Options.UseFont = true;
            this.gridViewDigital.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewDigital.Appearance.Row.Options.UseFont = true;
            this.gridViewDigital.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewDigital.Appearance.SelectedRow.Options.UseFont = true;
            this.gridViewDigital.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnDigitalCategory,
            this.gridColumnDigitalSubCategory,
            this.gridColumnDigitalProduct,
            this.gridColumnDigitalCustomNote,
            this.gridColumnDigitalDay});
            this.gridViewDigital.GridControl = this.gridControlDigital;
            this.gridViewDigital.Name = "gridViewDigital";
            this.gridViewDigital.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewDigital.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewDigital.OptionsBehavior.AutoPopulateColumns = false;
            this.gridViewDigital.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewDigital.OptionsCustomization.AllowFilter = false;
            this.gridViewDigital.OptionsCustomization.AllowGroup = false;
            this.gridViewDigital.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewDigital.OptionsCustomization.AllowSort = false;
            this.gridViewDigital.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewDigital.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridViewDigital.OptionsView.ColumnAutoWidth = false;
            this.gridViewDigital.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewDigital.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewDigital.OptionsView.ShowDetailButtons = false;
            this.gridViewDigital.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewDigital.OptionsView.ShowGroupPanel = false;
            this.gridViewDigital.OptionsView.ShowIndicator = false;
            this.gridViewDigital.RowHeight = 40;
            this.gridViewDigital.RowSeparatorHeight = 5;
            this.gridViewDigital.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gridViewDigital_ShowingEditor);
            this.gridViewDigital.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewDigital_CellValueChanged);
            // 
            // gridColumnDigitalCategory
            // 
            this.gridColumnDigitalCategory.Caption = "Category";
            this.gridColumnDigitalCategory.ColumnEdit = this.repositoryItemComboBoxDigitalCategory;
            this.gridColumnDigitalCategory.FieldName = "Category";
            this.gridColumnDigitalCategory.Name = "gridColumnDigitalCategory";
            this.gridColumnDigitalCategory.Visible = true;
            this.gridColumnDigitalCategory.VisibleIndex = 1;
            this.gridColumnDigitalCategory.Width = 165;
            // 
            // gridColumnDigitalSubCategory
            // 
            this.gridColumnDigitalSubCategory.Caption = "Sub-Group";
            this.gridColumnDigitalSubCategory.ColumnEdit = this.repositoryItemComboBoxDigitalSubCategory;
            this.gridColumnDigitalSubCategory.FieldName = "SubCategory";
            this.gridColumnDigitalSubCategory.Name = "gridColumnDigitalSubCategory";
            this.gridColumnDigitalSubCategory.Visible = true;
            this.gridColumnDigitalSubCategory.VisibleIndex = 2;
            this.gridColumnDigitalSubCategory.Width = 191;
            // 
            // gridColumnDigitalProduct
            // 
            this.gridColumnDigitalProduct.Caption = "Digital Inventory";
            this.gridColumnDigitalProduct.ColumnEdit = this.repositoryItemComboBoxDigitalProduct;
            this.gridColumnDigitalProduct.FieldName = "ProductName";
            this.gridColumnDigitalProduct.Name = "gridColumnDigitalProduct";
            this.gridColumnDigitalProduct.Visible = true;
            this.gridColumnDigitalProduct.VisibleIndex = 3;
            this.gridColumnDigitalProduct.Width = 242;
            // 
            // gridColumnDigitalCustomNote
            // 
            this.gridColumnDigitalCustomNote.Caption = "Custome Note";
            this.gridColumnDigitalCustomNote.ColumnEdit = this.repositoryItemTextEditCustomComment;
            this.gridColumnDigitalCustomNote.FieldName = "CustomNote";
            this.gridColumnDigitalCustomNote.Name = "gridColumnDigitalCustomNote";
            this.gridColumnDigitalCustomNote.Visible = true;
            this.gridColumnDigitalCustomNote.VisibleIndex = 4;
            this.gridColumnDigitalCustomNote.Width = 540;
            // 
            // gridColumnDigitalDay
            // 
            this.gridColumnDigitalDay.Caption = "Day";
            this.gridColumnDigitalDay.ColumnEdit = this.repositoryItemDateEdit;
            this.gridColumnDigitalDay.FieldName = "Day";
            this.gridColumnDigitalDay.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumnDigitalDay.Name = "gridColumnDigitalDay";
            this.gridColumnDigitalDay.OptionsColumn.AllowEdit = false;
            this.gridColumnDigitalDay.OptionsColumn.ReadOnly = true;
            this.gridColumnDigitalDay.Visible = true;
            this.gridColumnDigitalDay.VisibleIndex = 0;
            this.gridColumnDigitalDay.Width = 180;
            // 
            // xtraTabPageNewspaper
            // 
            this.xtraTabPageNewspaper.Controls.Add(this.gridControlNewspaper);
            this.xtraTabPageNewspaper.Name = "xtraTabPageNewspaper";
            this.xtraTabPageNewspaper.Size = new System.Drawing.Size(816, 334);
            this.xtraTabPageNewspaper.Text = "Newspapaer";
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
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // gridControlNewspaper
            // 
            this.gridControlNewspaper.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlNewspaper.ExternalRepository = this.persistentRepository;
            this.gridControlNewspaper.Location = new System.Drawing.Point(0, 0);
            this.gridControlNewspaper.MainView = this.gridViewNewspaper;
            this.gridControlNewspaper.MenuManager = this.barManager;
            this.gridControlNewspaper.Name = "gridControlNewspaper";
            this.gridControlNewspaper.Size = new System.Drawing.Size(816, 334);
            this.gridControlNewspaper.TabIndex = 1;
            this.gridControlNewspaper.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewNewspaper});
            // 
            // gridViewNewspaper
            // 
            this.gridViewNewspaper.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewNewspaper.Appearance.EvenRow.Options.UseFont = true;
            this.gridViewNewspaper.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.gridViewNewspaper.Appearance.HeaderPanel.Options.UseFont = true;
            this.gridViewNewspaper.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewNewspaper.Appearance.OddRow.Options.UseFont = true;
            this.gridViewNewspaper.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewNewspaper.Appearance.Row.Options.UseFont = true;
            this.gridViewNewspaper.Appearance.SelectedRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.gridViewNewspaper.Appearance.SelectedRow.Options.UseFont = true;
            this.gridViewNewspaper.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnNewspaperPublication,
            this.gridColumnNewspaperSection,
            this.gridColumnNewspaperPageSize,
            this.gridColumnNewspaperCustomNote,
            this.gridColumnNewspaperDay,
            this.gridColumnNewspaperColor,
            this.gridColumnNewspaperCost});
            this.gridViewNewspaper.GridControl = this.gridControlNewspaper;
            this.gridViewNewspaper.Name = "gridViewNewspaper";
            this.gridViewNewspaper.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewNewspaper.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gridViewNewspaper.OptionsBehavior.AutoPopulateColumns = false;
            this.gridViewNewspaper.OptionsCustomization.AllowColumnMoving = false;
            this.gridViewNewspaper.OptionsCustomization.AllowFilter = false;
            this.gridViewNewspaper.OptionsCustomization.AllowGroup = false;
            this.gridViewNewspaper.OptionsCustomization.AllowQuickHideColumns = false;
            this.gridViewNewspaper.OptionsCustomization.AllowSort = false;
            this.gridViewNewspaper.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewNewspaper.OptionsSelection.EnableAppearanceHideSelection = false;
            this.gridViewNewspaper.OptionsView.ColumnAutoWidth = false;
            this.gridViewNewspaper.OptionsView.EnableAppearanceEvenRow = true;
            this.gridViewNewspaper.OptionsView.EnableAppearanceOddRow = true;
            this.gridViewNewspaper.OptionsView.ShowDetailButtons = false;
            this.gridViewNewspaper.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridViewNewspaper.OptionsView.ShowGroupPanel = false;
            this.gridViewNewspaper.OptionsView.ShowIndicator = false;
            this.gridViewNewspaper.RowHeight = 40;
            this.gridViewNewspaper.RowSeparatorHeight = 5;
            this.gridViewNewspaper.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gridViewNewspaper_CellValueChanged);
            // 
            // gridColumnNewspaperPublication
            // 
            this.gridColumnNewspaperPublication.Caption = "Newspaper";
            this.gridColumnNewspaperPublication.ColumnEdit = this.repositoryItemComboBoxNewspaperPublication;
            this.gridColumnNewspaperPublication.FieldName = "PublicationName";
            this.gridColumnNewspaperPublication.Name = "gridColumnNewspaperPublication";
            this.gridColumnNewspaperPublication.Visible = true;
            this.gridColumnNewspaperPublication.VisibleIndex = 1;
            this.gridColumnNewspaperPublication.Width = 254;
            // 
            // gridColumnNewspaperSection
            // 
            this.gridColumnNewspaperSection.Caption = "Section";
            this.gridColumnNewspaperSection.ColumnEdit = this.repositoryItemComboBoxNewspaperSection;
            this.gridColumnNewspaperSection.FieldName = "Section";
            this.gridColumnNewspaperSection.Name = "gridColumnNewspaperSection";
            this.gridColumnNewspaperSection.Visible = true;
            this.gridColumnNewspaperSection.VisibleIndex = 2;
            this.gridColumnNewspaperSection.Width = 156;
            // 
            // gridColumnNewspaperPageSize
            // 
            this.gridColumnNewspaperPageSize.Caption = "Ad-Size";
            this.gridColumnNewspaperPageSize.ColumnEdit = this.repositoryItemComboBoxNewspaperPageSize;
            this.gridColumnNewspaperPageSize.FieldName = "PageSize";
            this.gridColumnNewspaperPageSize.Name = "gridColumnNewspaperPageSize";
            this.gridColumnNewspaperPageSize.Visible = true;
            this.gridColumnNewspaperPageSize.VisibleIndex = 3;
            this.gridColumnNewspaperPageSize.Width = 145;
            // 
            // gridColumnNewspaperCustomNote
            // 
            this.gridColumnNewspaperCustomNote.Caption = "Custome Note";
            this.gridColumnNewspaperCustomNote.ColumnEdit = this.repositoryItemTextEditCustomComment;
            this.gridColumnNewspaperCustomNote.FieldName = "CustomNote";
            this.gridColumnNewspaperCustomNote.Name = "gridColumnNewspaperCustomNote";
            this.gridColumnNewspaperCustomNote.Visible = true;
            this.gridColumnNewspaperCustomNote.VisibleIndex = 6;
            this.gridColumnNewspaperCustomNote.Width = 540;
            // 
            // gridColumnNewspaperDay
            // 
            this.gridColumnNewspaperDay.Caption = "Day";
            this.gridColumnNewspaperDay.ColumnEdit = this.repositoryItemDateEdit;
            this.gridColumnNewspaperDay.FieldName = "Day";
            this.gridColumnNewspaperDay.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridColumnNewspaperDay.Name = "gridColumnNewspaperDay";
            this.gridColumnNewspaperDay.OptionsColumn.AllowEdit = false;
            this.gridColumnNewspaperDay.OptionsColumn.ReadOnly = true;
            this.gridColumnNewspaperDay.Visible = true;
            this.gridColumnNewspaperDay.VisibleIndex = 0;
            this.gridColumnNewspaperDay.Width = 180;
            // 
            // gridColumnNewspaperColor
            // 
            this.gridColumnNewspaperColor.Caption = "Color or BW";
            this.gridColumnNewspaperColor.ColumnEdit = this.repositoryItemComboBoxNewspaperColor;
            this.gridColumnNewspaperColor.FieldName = "Color";
            this.gridColumnNewspaperColor.Name = "gridColumnNewspaperColor";
            this.gridColumnNewspaperColor.Visible = true;
            this.gridColumnNewspaperColor.VisibleIndex = 4;
            this.gridColumnNewspaperColor.Width = 111;
            // 
            // gridColumnNewspaperCost
            // 
            this.gridColumnNewspaperCost.Caption = "Cost";
            this.gridColumnNewspaperCost.ColumnEdit = this.repositoryItemSpinEditNewspaperTotalCost;
            this.gridColumnNewspaperCost.FieldName = "TotalCost";
            this.gridColumnNewspaperCost.Name = "gridColumnNewspaperCost";
            this.gridColumnNewspaperCost.Visible = true;
            this.gridColumnNewspaperCost.VisibleIndex = 5;
            this.gridColumnNewspaperCost.Width = 98;
            // 
            // repositoryItemComboBoxNewspaperPublication
            // 
            this.repositoryItemComboBoxNewspaperPublication.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repositoryItemComboBoxNewspaperPublication.Appearance.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperPublication.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperPublication.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperPublication.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperPublication.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperPublication.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperPublication.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperPublication.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperPublication.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperPublication.AutoHeight = false;
            this.repositoryItemComboBoxNewspaperPublication.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxNewspaperPublication.Name = "repositoryItemComboBoxNewspaperPublication";
            this.repositoryItemComboBoxNewspaperPublication.NullText = "Select Newspaper";
            this.repositoryItemComboBoxNewspaperPublication.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemComboBoxNewspaperPublication.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.repositoryItemComboBox_Closed);
            // 
            // repositoryItemComboBoxNewspaperSection
            // 
            this.repositoryItemComboBoxNewspaperSection.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperSection.Appearance.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperSection.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperSection.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperSection.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperSection.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperSection.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperSection.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperSection.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperSection.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperSection.AutoHeight = false;
            this.repositoryItemComboBoxNewspaperSection.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxNewspaperSection.Name = "repositoryItemComboBoxNewspaperSection";
            this.repositoryItemComboBoxNewspaperSection.NullText = "Select or Type: Section";
            this.repositoryItemComboBoxNewspaperSection.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.repositoryItemComboBox_Closed);
            // 
            // repositoryItemComboBoxNewspaperPageSize
            // 
            this.repositoryItemComboBoxNewspaperPageSize.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperPageSize.Appearance.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperPageSize.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperPageSize.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperPageSize.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperPageSize.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperPageSize.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperPageSize.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperPageSize.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperPageSize.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperPageSize.AutoHeight = false;
            this.repositoryItemComboBoxNewspaperPageSize.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxNewspaperPageSize.Name = "repositoryItemComboBoxNewspaperPageSize";
            this.repositoryItemComboBoxNewspaperPageSize.NullText = "Select or Type: Ad-Size";
            this.repositoryItemComboBoxNewspaperPageSize.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.repositoryItemComboBox_Closed);
            // 
            // repositoryItemComboBoxNewspaperColor
            // 
            this.repositoryItemComboBoxNewspaperColor.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperColor.Appearance.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperColor.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperColor.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperColor.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperColor.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperColor.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperColor.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperColor.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxNewspaperColor.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemComboBoxNewspaperColor.AutoHeight = false;
            this.repositoryItemComboBoxNewspaperColor.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxNewspaperColor.Items.AddRange(new object[] {
            "Full Color",
            "Spot Color",
            "Black and White"});
            this.repositoryItemComboBoxNewspaperColor.Name = "repositoryItemComboBoxNewspaperColor";
            this.repositoryItemComboBoxNewspaperColor.NullText = "Select Color or BW";
            this.repositoryItemComboBoxNewspaperColor.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.repositoryItemComboBoxNewspaperColor.Closed += new DevExpress.XtraEditors.Controls.ClosedEventHandler(this.repositoryItemComboBox_Closed);
            // 
            // repositoryItemSpinEditNewspaperTotalCost
            // 
            this.repositoryItemSpinEditNewspaperTotalCost.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.repositoryItemSpinEditNewspaperTotalCost.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditNewspaperTotalCost.Appearance.Options.UseFont = true;
            this.repositoryItemSpinEditNewspaperTotalCost.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditNewspaperTotalCost.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemSpinEditNewspaperTotalCost.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditNewspaperTotalCost.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemSpinEditNewspaperTotalCost.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditNewspaperTotalCost.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemSpinEditNewspaperTotalCost.AutoHeight = false;
            this.repositoryItemSpinEditNewspaperTotalCost.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditNewspaperTotalCost.DisplayFormat.FormatString = "$#,###.00";
            this.repositoryItemSpinEditNewspaperTotalCost.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditNewspaperTotalCost.EditFormat.FormatString = "$#,###.00";
            this.repositoryItemSpinEditNewspaperTotalCost.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditNewspaperTotalCost.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.repositoryItemSpinEditNewspaperTotalCost.Name = "repositoryItemSpinEditNewspaperTotalCost";
            // 
            // GridControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.pnNavbar);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "GridControl";
            this.Size = new System.Drawing.Size(822, 408);
            ((System.ComponentModel.ISupportInitialize)(this.barManager)).EndInit();
            this.pnNavbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl)).EndInit();
            this.xtraTabControl.ResumeLayout(false);
            this.xtraTabPageDigital.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDigital)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxDigitalCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxDigitalSubCategory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxDigitalProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEditCustomComment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit.VistaTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemDateEdit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDigital)).EndInit();
            this.xtraTabPageNewspaper.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlNewspaper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewNewspaper)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxNewspaperPublication)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxNewspaperSection)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxNewspaperPageSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxNewspaperColor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditNewspaperTotalCost)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager;
        private DevExpress.XtraBars.Bar barToolbar;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.StyleController styleController;
        private System.Windows.Forms.Panel pnNavbar;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemApply;
        private DevExpress.XtraBars.BarLargeButtonItem barLargeButtonItemClose;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageNewspaper;
        private DevExpress.XtraTab.XtraTabPage xtraTabPageDigital;
        private DevExpress.XtraEditors.Repository.PersistentRepository persistentRepository;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxDigitalCategory;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxDigitalSubCategory;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxDigitalProduct;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEditCustomComment;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit repositoryItemDateEdit;
        private DevExpress.XtraGrid.GridControl gridControlDigital;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDigital;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDigitalCategory;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDigitalSubCategory;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDigitalProduct;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDigitalCustomNote;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnDigitalDay;
        private DevExpress.XtraGrid.GridControl gridControlNewspaper;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewNewspaper;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNewspaperPublication;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNewspaperSection;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNewspaperPageSize;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNewspaperCustomNote;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNewspaperDay;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNewspaperColor;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnNewspaperCost;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxNewspaperPublication;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxNewspaperSection;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxNewspaperPageSize;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxNewspaperColor;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditNewspaperTotalCost;
    }
}
