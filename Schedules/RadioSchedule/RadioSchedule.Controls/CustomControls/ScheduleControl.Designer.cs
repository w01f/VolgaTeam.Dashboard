namespace TVScheduleBuilder.CustomControls
{
    partial class ScheduleControl
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
            this.gridControlSchedule = new DevExpress.XtraGrid.GridControl();
            this.advBandedGridViewSchedule = new DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView();
            this.gridBandProgram = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumnIndex = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumnStation = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemComboBoxStations = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.bandedGridColumnName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemComboBoxPrograms = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridBandDate = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumnDay = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemComboBoxDays = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.bandedGridColumnTime = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemComboBoxTimes = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridBandLength = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumnLength = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemComboBoxLengths = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.gridBandRate = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumnRate = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemSpinEditRate = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.bandedGridColumnRating = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemSpinEditRating = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.gridBandCPP = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumnCPP = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumnGRP = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBandSpots = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.repositoryItemSpinEditSpot = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.repositoryItemSpinEdit000s = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.pnTop = new System.Windows.Forms.Panel();
            this.checkEditCPM = new DevExpress.XtraEditors.CheckEdit();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.checkEditCPP = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditRating = new DevExpress.XtraEditors.CheckEdit();
            this.comboBoxEditDemo = new DevExpress.XtraEditors.ComboBoxEdit();
            this.laScheduleName = new System.Windows.Forms.Label();
            this.pnBottom = new System.Windows.Forms.Panel();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridViewSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxStations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxPrograms)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxTimes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxLengths)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditRating)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditSpot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit000s)).BeginInit();
            this.pnTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCPM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCPP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditRating.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDemo.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControlSchedule
            // 
            this.gridControlSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlSchedule.Location = new System.Drawing.Point(0, 41);
            this.gridControlSchedule.MainView = this.advBandedGridViewSchedule;
            this.gridControlSchedule.Name = "gridControlSchedule";
            this.gridControlSchedule.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBoxStations,
            this.repositoryItemComboBoxPrograms,
            this.repositoryItemComboBoxDays,
            this.repositoryItemComboBoxTimes,
            this.repositoryItemComboBoxLengths,
            this.repositoryItemSpinEditRate,
            this.repositoryItemSpinEditRating,
            this.repositoryItemSpinEditSpot,
            this.repositoryItemSpinEdit000s});
            this.gridControlSchedule.Size = new System.Drawing.Size(800, 291);
            this.gridControlSchedule.TabIndex = 0;
            this.gridControlSchedule.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.advBandedGridViewSchedule});
            // 
            // advBandedGridViewSchedule
            // 
            this.advBandedGridViewSchedule.Appearance.BandPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.advBandedGridViewSchedule.Appearance.BandPanel.Options.UseFont = true;
            this.advBandedGridViewSchedule.Appearance.BandPanel.Options.UseTextOptions = true;
            this.advBandedGridViewSchedule.Appearance.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.advBandedGridViewSchedule.Appearance.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.advBandedGridViewSchedule.Appearance.EvenRow.Options.UseFont = true;
            this.advBandedGridViewSchedule.Appearance.EvenRow.Options.UseTextOptions = true;
            this.advBandedGridViewSchedule.Appearance.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.advBandedGridViewSchedule.Appearance.FocusedCell.Font = new System.Drawing.Font("Arial", 9.75F);
            this.advBandedGridViewSchedule.Appearance.FocusedCell.Options.UseFont = true;
            this.advBandedGridViewSchedule.Appearance.FocusedCell.Options.UseTextOptions = true;
            this.advBandedGridViewSchedule.Appearance.FocusedCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.advBandedGridViewSchedule.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.advBandedGridViewSchedule.Appearance.FocusedRow.Options.UseFont = true;
            this.advBandedGridViewSchedule.Appearance.FocusedRow.Options.UseTextOptions = true;
            this.advBandedGridViewSchedule.Appearance.FocusedRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.advBandedGridViewSchedule.Appearance.FooterPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.advBandedGridViewSchedule.Appearance.FooterPanel.ForeColor = System.Drawing.Color.Black;
            this.advBandedGridViewSchedule.Appearance.FooterPanel.Options.UseFont = true;
            this.advBandedGridViewSchedule.Appearance.FooterPanel.Options.UseForeColor = true;
            this.advBandedGridViewSchedule.Appearance.FooterPanel.Options.UseTextOptions = true;
            this.advBandedGridViewSchedule.Appearance.FooterPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.advBandedGridViewSchedule.Appearance.GroupFooter.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.advBandedGridViewSchedule.Appearance.GroupFooter.Options.UseFont = true;
            this.advBandedGridViewSchedule.Appearance.GroupFooter.Options.UseTextOptions = true;
            this.advBandedGridViewSchedule.Appearance.GroupFooter.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.advBandedGridViewSchedule.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.advBandedGridViewSchedule.Appearance.HeaderPanel.Options.UseFont = true;
            this.advBandedGridViewSchedule.Appearance.HeaderPanel.Options.UseTextOptions = true;
            this.advBandedGridViewSchedule.Appearance.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.advBandedGridViewSchedule.Appearance.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.advBandedGridViewSchedule.Appearance.OddRow.Options.UseFont = true;
            this.advBandedGridViewSchedule.Appearance.OddRow.Options.UseTextOptions = true;
            this.advBandedGridViewSchedule.Appearance.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.advBandedGridViewSchedule.AppearancePrint.BandPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.advBandedGridViewSchedule.AppearancePrint.BandPanel.Options.UseFont = true;
            this.advBandedGridViewSchedule.AppearancePrint.BandPanel.Options.UseTextOptions = true;
            this.advBandedGridViewSchedule.AppearancePrint.BandPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.advBandedGridViewSchedule.AppearancePrint.EvenRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.advBandedGridViewSchedule.AppearancePrint.EvenRow.Options.UseFont = true;
            this.advBandedGridViewSchedule.AppearancePrint.EvenRow.Options.UseTextOptions = true;
            this.advBandedGridViewSchedule.AppearancePrint.EvenRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.advBandedGridViewSchedule.AppearancePrint.FooterPanel.Font = new System.Drawing.Font("Arial", 9.75F);
            this.advBandedGridViewSchedule.AppearancePrint.FooterPanel.Options.UseFont = true;
            this.advBandedGridViewSchedule.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.advBandedGridViewSchedule.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.advBandedGridViewSchedule.AppearancePrint.HeaderPanel.Options.UseTextOptions = true;
            this.advBandedGridViewSchedule.AppearancePrint.HeaderPanel.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.advBandedGridViewSchedule.AppearancePrint.OddRow.Font = new System.Drawing.Font("Arial", 9.75F);
            this.advBandedGridViewSchedule.AppearancePrint.OddRow.Options.UseFont = true;
            this.advBandedGridViewSchedule.AppearancePrint.OddRow.Options.UseTextOptions = true;
            this.advBandedGridViewSchedule.AppearancePrint.OddRow.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.advBandedGridViewSchedule.AppearancePrint.Row.Font = new System.Drawing.Font("Arial", 9.75F);
            this.advBandedGridViewSchedule.AppearancePrint.Row.Options.UseFont = true;
            this.advBandedGridViewSchedule.AppearancePrint.Row.Options.UseTextOptions = true;
            this.advBandedGridViewSchedule.AppearancePrint.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.advBandedGridViewSchedule.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBandProgram,
            this.gridBandDate,
            this.gridBandLength,
            this.gridBandRate,
            this.gridBandCPP,
            this.gridBandSpots});
            this.advBandedGridViewSchedule.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumnIndex,
            this.bandedGridColumnStation,
            this.bandedGridColumnName,
            this.bandedGridColumnDay,
            this.bandedGridColumnTime,
            this.bandedGridColumnLength,
            this.bandedGridColumnRate,
            this.bandedGridColumnRating,
            this.bandedGridColumnCPP,
            this.bandedGridColumnGRP});
            this.advBandedGridViewSchedule.FooterPanelHeight = 20;
            this.advBandedGridViewSchedule.GridControl = this.gridControlSchedule;
            this.advBandedGridViewSchedule.Name = "advBandedGridViewSchedule";
            this.advBandedGridViewSchedule.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.advBandedGridViewSchedule.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.advBandedGridViewSchedule.OptionsCustomization.AllowBandMoving = false;
            this.advBandedGridViewSchedule.OptionsCustomization.AllowBandResizing = false;
            this.advBandedGridViewSchedule.OptionsCustomization.AllowColumnMoving = false;
            this.advBandedGridViewSchedule.OptionsCustomization.AllowColumnResizing = false;
            this.advBandedGridViewSchedule.OptionsCustomization.AllowFilter = false;
            this.advBandedGridViewSchedule.OptionsCustomization.AllowGroup = false;
            this.advBandedGridViewSchedule.OptionsCustomization.AllowQuickHideColumns = false;
            this.advBandedGridViewSchedule.OptionsCustomization.AllowSort = false;
            this.advBandedGridViewSchedule.OptionsCustomization.ShowBandsInCustomizationForm = false;
            this.advBandedGridViewSchedule.OptionsFilter.AllowFilterEditor = false;
            this.advBandedGridViewSchedule.OptionsFind.AllowFindPanel = false;
            this.advBandedGridViewSchedule.OptionsMenu.EnableColumnMenu = false;
            this.advBandedGridViewSchedule.OptionsMenu.EnableFooterMenu = false;
            this.advBandedGridViewSchedule.OptionsMenu.EnableGroupPanelMenu = false;
            this.advBandedGridViewSchedule.OptionsMenu.ShowAutoFilterRowItem = false;
            this.advBandedGridViewSchedule.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.advBandedGridViewSchedule.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.advBandedGridViewSchedule.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.advBandedGridViewSchedule.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.advBandedGridViewSchedule.OptionsSelection.EnableAppearanceHideSelection = false;
            this.advBandedGridViewSchedule.OptionsView.EnableAppearanceEvenRow = true;
            this.advBandedGridViewSchedule.OptionsView.EnableAppearanceOddRow = true;
            this.advBandedGridViewSchedule.OptionsView.ShowBands = false;
            this.advBandedGridViewSchedule.OptionsView.ShowDetailButtons = false;
            this.advBandedGridViewSchedule.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.Never;
            this.advBandedGridViewSchedule.OptionsView.ShowFooter = true;
            this.advBandedGridViewSchedule.OptionsView.ShowGroupPanel = false;
            this.advBandedGridViewSchedule.OptionsView.ShowIndicator = false;
            this.advBandedGridViewSchedule.CustomDrawFooter += new DevExpress.XtraGrid.Views.Base.RowObjectCustomDrawEventHandler(this.advBandedGridViewSchedule_CustomDrawFooter);
            this.advBandedGridViewSchedule.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.advBandedGridViewSchedule_CellValueChanged);
            // 
            // gridBandProgram
            // 
            this.gridBandProgram.Caption = "Program";
            this.gridBandProgram.Columns.Add(this.bandedGridColumnIndex);
            this.gridBandProgram.Columns.Add(this.bandedGridColumnStation);
            this.gridBandProgram.Columns.Add(this.bandedGridColumnName);
            this.gridBandProgram.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridBandProgram.MinWidth = 20;
            this.gridBandProgram.Name = "gridBandProgram";
            this.gridBandProgram.RowCount = 2;
            this.gridBandProgram.Width = 426;
            // 
            // bandedGridColumnIndex
            // 
            this.bandedGridColumnIndex.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumnIndex.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnIndex.AutoFillDown = true;
            this.bandedGridColumnIndex.Caption = "Line ID";
            this.bandedGridColumnIndex.FieldName = "Index";
            this.bandedGridColumnIndex.Name = "bandedGridColumnIndex";
            this.bandedGridColumnIndex.OptionsColumn.AllowEdit = false;
            this.bandedGridColumnIndex.OptionsColumn.AllowSize = false;
            this.bandedGridColumnIndex.OptionsColumn.FixedWidth = true;
            this.bandedGridColumnIndex.OptionsColumn.ReadOnly = true;
            this.bandedGridColumnIndex.Visible = true;
            this.bandedGridColumnIndex.Width = 60;
            // 
            // bandedGridColumnStation
            // 
            this.bandedGridColumnStation.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumnStation.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnStation.AutoFillDown = true;
            this.bandedGridColumnStation.Caption = "Station";
            this.bandedGridColumnStation.ColumnEdit = this.repositoryItemComboBoxStations;
            this.bandedGridColumnStation.FieldName = "Station";
            this.bandedGridColumnStation.Name = "bandedGridColumnStation";
            this.bandedGridColumnStation.Visible = true;
            this.bandedGridColumnStation.Width = 93;
            // 
            // repositoryItemComboBoxStations
            // 
            this.repositoryItemComboBoxStations.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.repositoryItemComboBoxStations.Appearance.Options.UseFont = true;
            this.repositoryItemComboBoxStations.Appearance.Options.UseTextOptions = true;
            this.repositoryItemComboBoxStations.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxStations.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxStations.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemComboBoxStations.AppearanceDisabled.Options.UseTextOptions = true;
            this.repositoryItemComboBoxStations.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxStations.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxStations.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBoxStations.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxStations.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemComboBoxStations.AppearanceFocused.Options.UseTextOptions = true;
            this.repositoryItemComboBoxStations.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxStations.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxStations.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemComboBoxStations.AppearanceReadOnly.Options.UseTextOptions = true;
            this.repositoryItemComboBoxStations.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxStations.AutoHeight = false;
            this.repositoryItemComboBoxStations.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxStations.Name = "repositoryItemComboBoxStations";
            this.repositoryItemComboBoxStations.NullText = "Select";
            this.repositoryItemComboBoxStations.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // bandedGridColumnName
            // 
            this.bandedGridColumnName.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumnName.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnName.AutoFillDown = true;
            this.bandedGridColumnName.Caption = "Program";
            this.bandedGridColumnName.ColumnEdit = this.repositoryItemComboBoxPrograms;
            this.bandedGridColumnName.FieldName = "Name";
            this.bandedGridColumnName.Name = "bandedGridColumnName";
            this.bandedGridColumnName.Visible = true;
            this.bandedGridColumnName.Width = 273;
            // 
            // repositoryItemComboBoxPrograms
            // 
            this.repositoryItemComboBoxPrograms.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxPrograms.Appearance.Options.UseFont = true;
            this.repositoryItemComboBoxPrograms.Appearance.Options.UseTextOptions = true;
            this.repositoryItemComboBoxPrograms.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxPrograms.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxPrograms.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemComboBoxPrograms.AppearanceDisabled.Options.UseTextOptions = true;
            this.repositoryItemComboBoxPrograms.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxPrograms.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxPrograms.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBoxPrograms.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxPrograms.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemComboBoxPrograms.AppearanceFocused.Options.UseTextOptions = true;
            this.repositoryItemComboBoxPrograms.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxPrograms.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxPrograms.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemComboBoxPrograms.AppearanceReadOnly.Options.UseTextOptions = true;
            this.repositoryItemComboBoxPrograms.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxPrograms.AutoHeight = false;
            this.repositoryItemComboBoxPrograms.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxPrograms.Name = "repositoryItemComboBoxPrograms";
            this.repositoryItemComboBoxPrograms.NullText = "Select or Type";
            // 
            // gridBandDate
            // 
            this.gridBandDate.Caption = "Date";
            this.gridBandDate.Columns.Add(this.bandedGridColumnDay);
            this.gridBandDate.Columns.Add(this.bandedGridColumnTime);
            this.gridBandDate.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridBandDate.MinWidth = 20;
            this.gridBandDate.Name = "gridBandDate";
            this.gridBandDate.OptionsBand.FixedWidth = true;
            this.gridBandDate.RowCount = 2;
            this.gridBandDate.Width = 113;
            // 
            // bandedGridColumnDay
            // 
            this.bandedGridColumnDay.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumnDay.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnDay.AutoFillDown = true;
            this.bandedGridColumnDay.Caption = "Days";
            this.bandedGridColumnDay.ColumnEdit = this.repositoryItemComboBoxDays;
            this.bandedGridColumnDay.FieldName = "Day";
            this.bandedGridColumnDay.Name = "bandedGridColumnDay";
            this.bandedGridColumnDay.OptionsColumn.FixedWidth = true;
            this.bandedGridColumnDay.Visible = true;
            this.bandedGridColumnDay.Width = 113;
            // 
            // repositoryItemComboBoxDays
            // 
            this.repositoryItemComboBoxDays.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDays.Appearance.Options.UseFont = true;
            this.repositoryItemComboBoxDays.Appearance.Options.UseTextOptions = true;
            this.repositoryItemComboBoxDays.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxDays.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDays.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemComboBoxDays.AppearanceDisabled.Options.UseTextOptions = true;
            this.repositoryItemComboBoxDays.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxDays.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDays.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBoxDays.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDays.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemComboBoxDays.AppearanceFocused.Options.UseTextOptions = true;
            this.repositoryItemComboBoxDays.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxDays.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxDays.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemComboBoxDays.AppearanceReadOnly.Options.UseTextOptions = true;
            this.repositoryItemComboBoxDays.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxDays.AutoHeight = false;
            this.repositoryItemComboBoxDays.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxDays.Name = "repositoryItemComboBoxDays";
            this.repositoryItemComboBoxDays.NullText = "Select or Type";
            // 
            // bandedGridColumnTime
            // 
            this.bandedGridColumnTime.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumnTime.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnTime.AutoFillDown = true;
            this.bandedGridColumnTime.Caption = "Time";
            this.bandedGridColumnTime.ColumnEdit = this.repositoryItemComboBoxTimes;
            this.bandedGridColumnTime.FieldName = "Time";
            this.bandedGridColumnTime.Name = "bandedGridColumnTime";
            this.bandedGridColumnTime.OptionsColumn.FixedWidth = true;
            this.bandedGridColumnTime.RowIndex = 1;
            this.bandedGridColumnTime.Visible = true;
            this.bandedGridColumnTime.Width = 113;
            // 
            // repositoryItemComboBoxTimes
            // 
            this.repositoryItemComboBoxTimes.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxTimes.Appearance.Options.UseFont = true;
            this.repositoryItemComboBoxTimes.Appearance.Options.UseTextOptions = true;
            this.repositoryItemComboBoxTimes.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxTimes.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxTimes.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemComboBoxTimes.AppearanceDisabled.Options.UseTextOptions = true;
            this.repositoryItemComboBoxTimes.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxTimes.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxTimes.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBoxTimes.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxTimes.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemComboBoxTimes.AppearanceFocused.Options.UseTextOptions = true;
            this.repositoryItemComboBoxTimes.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxTimes.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxTimes.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemComboBoxTimes.AppearanceReadOnly.Options.UseTextOptions = true;
            this.repositoryItemComboBoxTimes.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxTimes.AutoHeight = false;
            this.repositoryItemComboBoxTimes.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxTimes.Name = "repositoryItemComboBoxTimes";
            this.repositoryItemComboBoxTimes.NullText = "Select or Type";
            // 
            // gridBandLength
            // 
            this.gridBandLength.Caption = "Length";
            this.gridBandLength.Columns.Add(this.bandedGridColumnLength);
            this.gridBandLength.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridBandLength.MinWidth = 20;
            this.gridBandLength.Name = "gridBandLength";
            this.gridBandLength.OptionsBand.FixedWidth = true;
            this.gridBandLength.RowCount = 2;
            this.gridBandLength.Width = 86;
            // 
            // bandedGridColumnLength
            // 
            this.bandedGridColumnLength.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumnLength.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnLength.AutoFillDown = true;
            this.bandedGridColumnLength.Caption = "Length";
            this.bandedGridColumnLength.ColumnEdit = this.repositoryItemComboBoxLengths;
            this.bandedGridColumnLength.FieldName = "Length";
            this.bandedGridColumnLength.Name = "bandedGridColumnLength";
            this.bandedGridColumnLength.OptionsColumn.FixedWidth = true;
            this.bandedGridColumnLength.Visible = true;
            this.bandedGridColumnLength.Width = 86;
            // 
            // repositoryItemComboBoxLengths
            // 
            this.repositoryItemComboBoxLengths.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxLengths.Appearance.Options.UseFont = true;
            this.repositoryItemComboBoxLengths.Appearance.Options.UseTextOptions = true;
            this.repositoryItemComboBoxLengths.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxLengths.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxLengths.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemComboBoxLengths.AppearanceDisabled.Options.UseTextOptions = true;
            this.repositoryItemComboBoxLengths.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxLengths.AppearanceDropDown.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxLengths.AppearanceDropDown.Options.UseFont = true;
            this.repositoryItemComboBoxLengths.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxLengths.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemComboBoxLengths.AppearanceFocused.Options.UseTextOptions = true;
            this.repositoryItemComboBoxLengths.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxLengths.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemComboBoxLengths.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemComboBoxLengths.AppearanceReadOnly.Options.UseTextOptions = true;
            this.repositoryItemComboBoxLengths.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemComboBoxLengths.AutoHeight = false;
            this.repositoryItemComboBoxLengths.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxLengths.Name = "repositoryItemComboBoxLengths";
            this.repositoryItemComboBoxLengths.NullText = "Select";
            this.repositoryItemComboBoxLengths.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // gridBandRate
            // 
            this.gridBandRate.Caption = "Rate";
            this.gridBandRate.Columns.Add(this.bandedGridColumnRate);
            this.gridBandRate.Columns.Add(this.bandedGridColumnRating);
            this.gridBandRate.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridBandRate.MinWidth = 20;
            this.gridBandRate.Name = "gridBandRate";
            this.gridBandRate.OptionsBand.FixedWidth = true;
            this.gridBandRate.RowCount = 2;
            this.gridBandRate.Width = 108;
            // 
            // bandedGridColumnRate
            // 
            this.bandedGridColumnRate.AutoFillDown = true;
            this.bandedGridColumnRate.Caption = "Rate";
            this.bandedGridColumnRate.ColumnEdit = this.repositoryItemSpinEditRate;
            this.bandedGridColumnRate.FieldName = "Rate";
            this.bandedGridColumnRate.Name = "bandedGridColumnRate";
            this.bandedGridColumnRate.OptionsColumn.FixedWidth = true;
            this.bandedGridColumnRate.Visible = true;
            this.bandedGridColumnRate.Width = 108;
            // 
            // repositoryItemSpinEditRate
            // 
            this.repositoryItemSpinEditRate.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditRate.Appearance.Options.UseFont = true;
            this.repositoryItemSpinEditRate.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditRate.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemSpinEditRate.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditRate.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemSpinEditRate.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditRate.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemSpinEditRate.AutoHeight = false;
            this.repositoryItemSpinEditRate.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditRate.DisplayFormat.FormatString = "$#,###";
            this.repositoryItemSpinEditRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditRate.EditFormat.FormatString = "$#,###";
            this.repositoryItemSpinEditRate.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditRate.IsFloatValue = false;
            this.repositoryItemSpinEditRate.Mask.EditMask = "N00";
            this.repositoryItemSpinEditRate.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.repositoryItemSpinEditRate.Name = "repositoryItemSpinEditRate";
            // 
            // bandedGridColumnRating
            // 
            this.bandedGridColumnRating.AutoFillDown = true;
            this.bandedGridColumnRating.Caption = "Rtg";
            this.bandedGridColumnRating.ColumnEdit = this.repositoryItemSpinEditRating;
            this.bandedGridColumnRating.FieldName = "Rating";
            this.bandedGridColumnRating.Name = "bandedGridColumnRating";
            this.bandedGridColumnRating.OptionsColumn.FixedWidth = true;
            this.bandedGridColumnRating.RowIndex = 1;
            this.bandedGridColumnRating.Visible = true;
            this.bandedGridColumnRating.Width = 108;
            // 
            // repositoryItemSpinEditRating
            // 
            this.repositoryItemSpinEditRating.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repositoryItemSpinEditRating.Appearance.Options.UseFont = true;
            this.repositoryItemSpinEditRating.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditRating.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemSpinEditRating.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditRating.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemSpinEditRating.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditRating.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemSpinEditRating.AutoHeight = false;
            this.repositoryItemSpinEditRating.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditRating.DisplayFormat.FormatString = "#,##0.0";
            this.repositoryItemSpinEditRating.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditRating.EditFormat.FormatString = "#,##0.0";
            this.repositoryItemSpinEditRating.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditRating.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.repositoryItemSpinEditRating.Name = "repositoryItemSpinEditRating";
            // 
            // gridBandCPP
            // 
            this.gridBandCPP.Caption = "CPP";
            this.gridBandCPP.Columns.Add(this.bandedGridColumnCPP);
            this.gridBandCPP.Columns.Add(this.bandedGridColumnGRP);
            this.gridBandCPP.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            this.gridBandCPP.Name = "gridBandCPP";
            this.gridBandCPP.OptionsBand.FixedWidth = true;
            this.gridBandCPP.RowCount = 2;
            this.gridBandCPP.Width = 111;
            // 
            // bandedGridColumnCPP
            // 
            this.bandedGridColumnCPP.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumnCPP.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnCPP.AutoFillDown = true;
            this.bandedGridColumnCPP.Caption = "CPP";
            this.bandedGridColumnCPP.FieldName = "CPP";
            this.bandedGridColumnCPP.Name = "bandedGridColumnCPP";
            this.bandedGridColumnCPP.OptionsColumn.AllowEdit = false;
            this.bandedGridColumnCPP.OptionsColumn.FixedWidth = true;
            this.bandedGridColumnCPP.OptionsColumn.ReadOnly = true;
            this.bandedGridColumnCPP.Visible = true;
            this.bandedGridColumnCPP.Width = 111;
            // 
            // bandedGridColumnGRP
            // 
            this.bandedGridColumnGRP.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumnGRP.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnGRP.AutoFillDown = true;
            this.bandedGridColumnGRP.Caption = "GRPs";
            this.bandedGridColumnGRP.FieldName = "GRP";
            this.bandedGridColumnGRP.Name = "bandedGridColumnGRP";
            this.bandedGridColumnGRP.OptionsColumn.AllowEdit = false;
            this.bandedGridColumnGRP.OptionsColumn.FixedWidth = true;
            this.bandedGridColumnGRP.OptionsColumn.ReadOnly = true;
            this.bandedGridColumnGRP.RowIndex = 1;
            this.bandedGridColumnGRP.Visible = true;
            this.bandedGridColumnGRP.Width = 111;
            // 
            // gridBandSpots
            // 
            this.gridBandSpots.Caption = "Spots";
            this.gridBandSpots.MinWidth = 20;
            this.gridBandSpots.Name = "gridBandSpots";
            this.gridBandSpots.RowCount = 2;
            this.gridBandSpots.Width = 659;
            // 
            // repositoryItemSpinEditSpot
            // 
            this.repositoryItemSpinEditSpot.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.repositoryItemSpinEditSpot.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.repositoryItemSpinEditSpot.Appearance.Options.UseFont = true;
            this.repositoryItemSpinEditSpot.Appearance.Options.UseTextOptions = true;
            this.repositoryItemSpinEditSpot.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemSpinEditSpot.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditSpot.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemSpinEditSpot.AppearanceDisabled.Options.UseTextOptions = true;
            this.repositoryItemSpinEditSpot.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemSpinEditSpot.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditSpot.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemSpinEditSpot.AppearanceFocused.Options.UseTextOptions = true;
            this.repositoryItemSpinEditSpot.AppearanceFocused.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemSpinEditSpot.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEditSpot.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemSpinEditSpot.AppearanceReadOnly.Options.UseTextOptions = true;
            this.repositoryItemSpinEditSpot.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.repositoryItemSpinEditSpot.AutoHeight = false;
            this.repositoryItemSpinEditSpot.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditSpot.DisplayFormat.FormatString = "#,###";
            this.repositoryItemSpinEditSpot.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditSpot.EditFormat.FormatString = "#,###";
            this.repositoryItemSpinEditSpot.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditSpot.IsFloatValue = false;
            this.repositoryItemSpinEditSpot.Mask.EditMask = "N00";
            this.repositoryItemSpinEditSpot.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.repositoryItemSpinEditSpot.Name = "repositoryItemSpinEditSpot";
            this.repositoryItemSpinEditSpot.NullText = "-";
            // 
            // repositoryItemSpinEdit000s
            // 
            this.repositoryItemSpinEdit000s.Appearance.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEdit000s.Appearance.Options.UseFont = true;
            this.repositoryItemSpinEdit000s.AppearanceDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEdit000s.AppearanceDisabled.Options.UseFont = true;
            this.repositoryItemSpinEdit000s.AppearanceFocused.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEdit000s.AppearanceFocused.Options.UseFont = true;
            this.repositoryItemSpinEdit000s.AppearanceReadOnly.Font = new System.Drawing.Font("Arial", 9.75F);
            this.repositoryItemSpinEdit000s.AppearanceReadOnly.Options.UseFont = true;
            this.repositoryItemSpinEdit000s.AutoHeight = false;
            this.repositoryItemSpinEdit000s.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEdit000s.DisplayFormat.FormatString = "#,##0";
            this.repositoryItemSpinEdit000s.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEdit000s.EditFormat.FormatString = "#,##0";
            this.repositoryItemSpinEdit000s.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEdit000s.IsFloatValue = false;
            this.repositoryItemSpinEdit000s.Mask.EditMask = "N00";
            this.repositoryItemSpinEdit000s.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.repositoryItemSpinEdit000s.Name = "repositoryItemSpinEdit000s";
            // 
            // pnTop
            // 
            this.pnTop.Controls.Add(this.checkEditCPM);
            this.pnTop.Controls.Add(this.checkEditCPP);
            this.pnTop.Controls.Add(this.checkEditRating);
            this.pnTop.Controls.Add(this.comboBoxEditDemo);
            this.pnTop.Controls.Add(this.laScheduleName);
            this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTop.Location = new System.Drawing.Point(0, 0);
            this.pnTop.Name = "pnTop";
            this.pnTop.Size = new System.Drawing.Size(800, 41);
            this.pnTop.TabIndex = 1;
            // 
            // checkEditCPM
            // 
            this.checkEditCPM.Location = new System.Drawing.Point(292, 10);
            this.checkEditCPM.Name = "checkEditCPM";
            this.checkEditCPM.Properties.AutoWidth = true;
            this.checkEditCPM.Properties.Caption = "000s";
            this.checkEditCPM.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.checkEditCPM.Size = new System.Drawing.Size(51, 21);
            this.checkEditCPM.StyleController = this.styleController;
            this.checkEditCPM.TabIndex = 5;
            this.checkEditCPM.CheckedChanged += new System.EventHandler(this.checkEditCPP_CheckedChanged);
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
            // checkEditCPP
            // 
            this.checkEditCPP.Location = new System.Drawing.Point(203, 10);
            this.checkEditCPP.Name = "checkEditCPP";
            this.checkEditCPP.Properties.AutoWidth = true;
            this.checkEditCPP.Properties.Caption = "Ratings";
            this.checkEditCPP.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.checkEditCPP.Size = new System.Drawing.Size(67, 21);
            this.checkEditCPP.StyleController = this.styleController;
            this.checkEditCPP.TabIndex = 4;
            this.checkEditCPP.CheckedChanged += new System.EventHandler(this.checkEditCPP_CheckedChanged);
            // 
            // checkEditRating
            // 
            this.checkEditRating.Location = new System.Drawing.Point(3, 10);
            this.checkEditRating.Name = "checkEditRating";
            this.checkEditRating.Properties.AutoWidth = true;
            this.checkEditRating.Properties.Caption = "";
            this.checkEditRating.Size = new System.Drawing.Size(23, 21);
            this.checkEditRating.StyleController = this.styleController;
            this.checkEditRating.TabIndex = 3;
            this.checkEditRating.CheckedChanged += new System.EventHandler(this.checkEditRating_CheckedChanged);
            // 
            // comboBoxEditDemo
            // 
            this.comboBoxEditDemo.Location = new System.Drawing.Point(28, 9);
            this.comboBoxEditDemo.Name = "comboBoxEditDemo";
            this.comboBoxEditDemo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditDemo.Properties.NullText = "Select Demo";
            this.comboBoxEditDemo.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.comboBoxEditDemo.Size = new System.Drawing.Size(149, 22);
            this.comboBoxEditDemo.StyleController = this.styleController;
            this.comboBoxEditDemo.TabIndex = 2;
            this.comboBoxEditDemo.EditValueChanged += new System.EventHandler(this.comboBoxEditDemo_EditValueChanged);
            // 
            // laScheduleName
            // 
            this.laScheduleName.Dock = System.Windows.Forms.DockStyle.Right;
            this.laScheduleName.Location = new System.Drawing.Point(570, 0);
            this.laScheduleName.Name = "laScheduleName";
            this.laScheduleName.Size = new System.Drawing.Size(230, 41);
            this.laScheduleName.TabIndex = 1;
            this.laScheduleName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnBottom
            // 
            this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnBottom.Location = new System.Drawing.Point(0, 332);
            this.pnBottom.Name = "pnBottom";
            this.pnBottom.Size = new System.Drawing.Size(800, 30);
            this.pnBottom.TabIndex = 2;
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // ScheduleControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.gridControlSchedule);
            this.Controls.Add(this.pnBottom);
            this.Controls.Add(this.pnTop);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "ScheduleControl";
            this.Size = new System.Drawing.Size(800, 362);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.advBandedGridViewSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxStations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxPrograms)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxTimes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxLengths)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditRating)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditSpot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit000s)).EndInit();
            this.pnTop.ResumeLayout(false);
            this.pnTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCPM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditCPP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditRating.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditDemo.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlSchedule;
        private System.Windows.Forms.Panel pnTop;
        private System.Windows.Forms.Panel pnBottom;
        private DevExpress.XtraGrid.Views.BandedGrid.AdvBandedGridView advBandedGridViewSchedule;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnIndex;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnStation;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnLength;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnTime;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnRate;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnRating;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnCPP;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnGRP;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnDay;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxStations;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxPrograms;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxDays;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxTimes;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxLengths;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditRate;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditRating;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditSpot;
        private System.Windows.Forms.Label laScheduleName;
        private DevExpress.XtraEditors.CheckEdit checkEditCPM;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.XtraEditors.CheckEdit checkEditCPP;
        private DevExpress.XtraEditors.CheckEdit checkEditRating;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEditDemo;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit000s;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandProgram;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandDate;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandLength;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandRate;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandCPP;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandSpots;
    }
}
