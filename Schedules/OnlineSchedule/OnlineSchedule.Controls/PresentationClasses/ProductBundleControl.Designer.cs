namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
    partial class ProductBundleControl
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
            this.pnHeader = new System.Windows.Forms.Panel();
            this.comboBoxEditSlideHeader = new DevExpress.XtraEditors.ComboBoxEdit();
            this.styleController = new DevExpress.XtraEditors.StyleController(this.components);
            this.labelControlFlightDates = new DevExpress.XtraEditors.LabelControl();
            this.labelControlDecisionMaker = new DevExpress.XtraEditors.LabelControl();
            this.labelControlAdvertiser = new DevExpress.XtraEditors.LabelControl();
            this.labelControlPresentationDate = new DevExpress.XtraEditors.LabelControl();
            this.defaultLookAndFeel = new DevExpress.LookAndFeel.DefaultLookAndFeel(this.components);
            this.pnMain = new System.Windows.Forms.Panel();
            this.gridControlProductBundle = new DevExpress.XtraGrid.GridControl();
            this.bandedGridViewProductBundle = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBandProduct = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumnIndex = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumnName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemSpinEditCurrency = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.repositoryItemSpinEditInteger = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.pnFooterPackageBundle = new System.Windows.Forms.Panel();
            this.checkEditShowTotalsLastSlideBundle = new DevExpress.XtraEditors.CheckEdit();
            this.pnPackageBundleTotal = new System.Windows.Forms.Panel();
            this.spinEditTotalInvestment = new DevExpress.XtraEditors.SpinEdit();
            this.labelControlPackageBundleTotalInvestment = new DevExpress.XtraEditors.LabelControl();
            this.checkEditTotalCPM = new DevExpress.XtraEditors.CheckEdit();
            this.spinEditTotalImpressions = new DevExpress.XtraEditors.SpinEdit();
            this.labelControlPackageBundleTotalImpressions = new DevExpress.XtraEditors.LabelControl();
            this.pnPackageBundleMonthly = new System.Windows.Forms.Panel();
            this.spinEditMonthlyInvestment = new DevExpress.XtraEditors.SpinEdit();
            this.labelControlPackageBundleMonthlyInvestment = new DevExpress.XtraEditors.LabelControl();
            this.checkEditMonthlyCPM = new DevExpress.XtraEditors.CheckEdit();
            this.spinEditMonthlyImpressions = new DevExpress.XtraEditors.SpinEdit();
            this.labelControlPackageBundleMonthlyImpressions = new DevExpress.XtraEditors.LabelControl();
            this.pnHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            this.pnMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProductBundle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridViewProductBundle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditInteger)).BeginInit();
            this.pnFooterPackageBundle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditShowTotalsLastSlideBundle.Properties)).BeginInit();
            this.pnPackageBundleTotal.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditTotalInvestment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditTotalCPM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditTotalImpressions.Properties)).BeginInit();
            this.pnPackageBundleMonthly.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMonthlyInvestment.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditMonthlyCPM.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMonthlyImpressions.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnHeader
            // 
            this.pnHeader.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnHeader.Controls.Add(this.comboBoxEditSlideHeader);
            this.pnHeader.Controls.Add(this.labelControlFlightDates);
            this.pnHeader.Controls.Add(this.labelControlDecisionMaker);
            this.pnHeader.Controls.Add(this.labelControlAdvertiser);
            this.pnHeader.Controls.Add(this.labelControlPresentationDate);
            this.pnHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnHeader.Location = new System.Drawing.Point(0, 0);
            this.pnHeader.Name = "pnHeader";
            this.pnHeader.Size = new System.Drawing.Size(1000, 96);
            this.pnHeader.TabIndex = 3;
            // 
            // comboBoxEditSlideHeader
            // 
            this.comboBoxEditSlideHeader.Location = new System.Drawing.Point(5, 6);
            this.comboBoxEditSlideHeader.Name = "comboBoxEditSlideHeader";
            this.comboBoxEditSlideHeader.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.comboBoxEditSlideHeader.Properties.NullText = "Your Online Campaign";
            this.comboBoxEditSlideHeader.Size = new System.Drawing.Size(203, 22);
            this.comboBoxEditSlideHeader.StyleController = this.styleController;
            this.comboBoxEditSlideHeader.TabIndex = 14;
            this.comboBoxEditSlideHeader.EditValueChanged += new System.EventHandler(this.comboBoxEditSlideHeader_EditValueChanged);
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
            // labelControlFlightDates
            // 
            this.labelControlFlightDates.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlFlightDates.Location = new System.Drawing.Point(5, 63);
            this.labelControlFlightDates.Name = "labelControlFlightDates";
            this.labelControlFlightDates.Size = new System.Drawing.Size(637, 23);
            this.labelControlFlightDates.StyleController = this.styleController;
            this.labelControlFlightDates.TabIndex = 9;
            this.labelControlFlightDates.Text = "Flight Dates";
            // 
            // labelControlDecisionMaker
            // 
            this.labelControlDecisionMaker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlDecisionMaker.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlDecisionMaker.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlDecisionMaker.Location = new System.Drawing.Point(707, 34);
            this.labelControlDecisionMaker.Name = "labelControlDecisionMaker";
            this.labelControlDecisionMaker.Size = new System.Drawing.Size(286, 23);
            this.labelControlDecisionMaker.StyleController = this.styleController;
            this.labelControlDecisionMaker.TabIndex = 8;
            this.labelControlDecisionMaker.Text = "Decision Maker";
            // 
            // labelControlAdvertiser
            // 
            this.labelControlAdvertiser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelControlAdvertiser.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.labelControlAdvertiser.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlAdvertiser.Location = new System.Drawing.Point(707, 6);
            this.labelControlAdvertiser.Name = "labelControlAdvertiser";
            this.labelControlAdvertiser.Size = new System.Drawing.Size(286, 23);
            this.labelControlAdvertiser.StyleController = this.styleController;
            this.labelControlAdvertiser.TabIndex = 6;
            this.labelControlAdvertiser.Text = "Advertiser";
            // 
            // labelControlPresentationDate
            // 
            this.labelControlPresentationDate.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlPresentationDate.Location = new System.Drawing.Point(5, 34);
            this.labelControlPresentationDate.Name = "labelControlPresentationDate";
            this.labelControlPresentationDate.Size = new System.Drawing.Size(250, 23);
            this.labelControlPresentationDate.StyleController = this.styleController;
            this.labelControlPresentationDate.TabIndex = 5;
            this.labelControlPresentationDate.Text = "Presentation Date";
            // 
            // defaultLookAndFeel
            // 
            this.defaultLookAndFeel.LookAndFeel.SkinName = "Money Twins";
            // 
            // pnMain
            // 
            this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnMain.Controls.Add(this.gridControlProductBundle);
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 96);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(1000, 364);
            this.pnMain.TabIndex = 5;
            // 
            // gridControlProductBundle
            // 
            this.gridControlProductBundle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlProductBundle.Location = new System.Drawing.Point(0, 0);
            this.gridControlProductBundle.MainView = this.bandedGridViewProductBundle;
            this.gridControlProductBundle.Name = "gridControlProductBundle";
            this.gridControlProductBundle.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEditCurrency,
            this.repositoryItemSpinEditInteger});
            this.gridControlProductBundle.Size = new System.Drawing.Size(996, 360);
            this.gridControlProductBundle.TabIndex = 0;
            this.gridControlProductBundle.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridViewProductBundle});
            // 
            // bandedGridViewProductBundle
            // 
            this.bandedGridViewProductBundle.Appearance.BandPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.bandedGridViewProductBundle.Appearance.BandPanel.Options.UseFont = true;
            this.bandedGridViewProductBundle.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.bandedGridViewProductBundle.Appearance.HeaderPanel.Options.UseFont = true;
            this.bandedGridViewProductBundle.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bandedGridViewProductBundle.Appearance.Preview.Options.UseFont = true;
            this.bandedGridViewProductBundle.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
            this.bandedGridViewProductBundle.Appearance.Row.Options.UseFont = true;
            this.bandedGridViewProductBundle.Appearance.RowSeparator.BackColor = System.Drawing.Color.AliceBlue;
            this.bandedGridViewProductBundle.Appearance.RowSeparator.Options.UseBackColor = true;
            this.bandedGridViewProductBundle.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBandProduct});
            this.bandedGridViewProductBundle.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumnIndex,
            this.bandedGridColumnName});
            this.bandedGridViewProductBundle.GridControl = this.gridControlProductBundle;
            this.bandedGridViewProductBundle.Name = "bandedGridViewProductBundle";
            this.bandedGridViewProductBundle.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridViewProductBundle.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridViewProductBundle.OptionsBehavior.Editable = false;
            this.bandedGridViewProductBundle.OptionsBehavior.ReadOnly = true;
            this.bandedGridViewProductBundle.OptionsCustomization.AllowBandMoving = false;
            this.bandedGridViewProductBundle.OptionsCustomization.AllowBandResizing = false;
            this.bandedGridViewProductBundle.OptionsCustomization.AllowColumnMoving = false;
            this.bandedGridViewProductBundle.OptionsCustomization.AllowColumnResizing = false;
            this.bandedGridViewProductBundle.OptionsCustomization.AllowFilter = false;
            this.bandedGridViewProductBundle.OptionsCustomization.AllowGroup = false;
            this.bandedGridViewProductBundle.OptionsCustomization.AllowQuickHideColumns = false;
            this.bandedGridViewProductBundle.OptionsCustomization.AllowSort = false;
            this.bandedGridViewProductBundle.OptionsCustomization.ShowBandsInCustomizationForm = false;
            this.bandedGridViewProductBundle.OptionsMenu.EnableColumnMenu = false;
            this.bandedGridViewProductBundle.OptionsMenu.EnableFooterMenu = false;
            this.bandedGridViewProductBundle.OptionsMenu.EnableGroupPanelMenu = false;
            this.bandedGridViewProductBundle.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.bandedGridViewProductBundle.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.bandedGridViewProductBundle.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.bandedGridViewProductBundle.OptionsSelection.EnableAppearanceHideSelection = false;
            this.bandedGridViewProductBundle.OptionsView.AutoCalcPreviewLineCount = true;
            this.bandedGridViewProductBundle.OptionsView.ShowDetailButtons = false;
            this.bandedGridViewProductBundle.OptionsView.ShowGroupPanel = false;
            this.bandedGridViewProductBundle.OptionsView.ShowIndicator = false;
            this.bandedGridViewProductBundle.OptionsView.ShowPreview = true;
            this.bandedGridViewProductBundle.PreviewFieldName = "Name";
            this.bandedGridViewProductBundle.PreviewIndent = 20;
            this.bandedGridViewProductBundle.RowHeight = 25;
            this.bandedGridViewProductBundle.RowSeparatorHeight = 10;
            this.bandedGridViewProductBundle.CalcPreviewText += new DevExpress.XtraGrid.Views.Grid.CalcPreviewTextEventHandler(this.bandedGridViewProductBundle_CalcPreviewText);
            // 
            // gridBandProduct
            // 
            this.gridBandProduct.Caption = "Product";
            this.gridBandProduct.Columns.Add(this.bandedGridColumnIndex);
            this.gridBandProduct.Columns.Add(this.bandedGridColumnName);
            this.gridBandProduct.MinWidth = 20;
            this.gridBandProduct.Name = "gridBandProduct";
            this.gridBandProduct.Width = 455;
            // 
            // bandedGridColumnIndex
            // 
            this.bandedGridColumnIndex.AppearanceCell.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.bandedGridColumnIndex.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumnIndex.Caption = "Index";
            this.bandedGridColumnIndex.FieldName = "Index";
            this.bandedGridColumnIndex.Name = "bandedGridColumnIndex";
            this.bandedGridColumnIndex.OptionsColumn.FixedWidth = true;
            this.bandedGridColumnIndex.OptionsColumn.ShowCaption = false;
            this.bandedGridColumnIndex.Visible = true;
            this.bandedGridColumnIndex.Width = 40;
            // 
            // bandedGridColumnName
            // 
            this.bandedGridColumnName.AppearanceCell.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.bandedGridColumnName.AppearanceCell.Options.UseFont = true;
            this.bandedGridColumnName.Caption = "Name";
            this.bandedGridColumnName.FieldName = "Name";
            this.bandedGridColumnName.Name = "bandedGridColumnName";
            this.bandedGridColumnName.OptionsColumn.ShowCaption = false;
            this.bandedGridColumnName.Visible = true;
            this.bandedGridColumnName.Width = 415;
            // 
            // repositoryItemSpinEditCurrency
            // 
            this.repositoryItemSpinEditCurrency.AutoHeight = false;
            this.repositoryItemSpinEditCurrency.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditCurrency.DisplayFormat.FormatString = "$#,###.00";
            this.repositoryItemSpinEditCurrency.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditCurrency.EditFormat.FormatString = "$#,###.00";
            this.repositoryItemSpinEditCurrency.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditCurrency.Name = "repositoryItemSpinEditCurrency";
            this.repositoryItemSpinEditCurrency.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // repositoryItemSpinEditInteger
            // 
            this.repositoryItemSpinEditInteger.AutoHeight = false;
            this.repositoryItemSpinEditInteger.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemSpinEditInteger.DisplayFormat.FormatString = "#,##0";
            this.repositoryItemSpinEditInteger.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditInteger.EditFormat.FormatString = "#,##0";
            this.repositoryItemSpinEditInteger.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.repositoryItemSpinEditInteger.Name = "repositoryItemSpinEditInteger";
            this.repositoryItemSpinEditInteger.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // pnFooterPackageBundle
            // 
            this.pnFooterPackageBundle.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnFooterPackageBundle.Controls.Add(this.checkEditShowTotalsLastSlideBundle);
            this.pnFooterPackageBundle.Controls.Add(this.pnPackageBundleTotal);
            this.pnFooterPackageBundle.Controls.Add(this.pnPackageBundleMonthly);
            this.pnFooterPackageBundle.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnFooterPackageBundle.Location = new System.Drawing.Point(0, 401);
            this.pnFooterPackageBundle.Name = "pnFooterPackageBundle";
            this.pnFooterPackageBundle.Size = new System.Drawing.Size(1000, 59);
            this.pnFooterPackageBundle.TabIndex = 6;
            // 
            // checkEditShowTotalsLastSlideBundle
            // 
            this.checkEditShowTotalsLastSlideBundle.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkEditShowTotalsLastSlideBundle.Location = new System.Drawing.Point(854, 0);
            this.checkEditShowTotalsLastSlideBundle.Name = "checkEditShowTotalsLastSlideBundle";
            this.checkEditShowTotalsLastSlideBundle.Properties.AllowHtmlString = true;
            this.checkEditShowTotalsLastSlideBundle.Properties.Appearance.Options.UseTextOptions = true;
            this.checkEditShowTotalsLastSlideBundle.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.checkEditShowTotalsLastSlideBundle.Properties.AutoHeight = false;
            this.checkEditShowTotalsLastSlideBundle.Properties.Caption = "Show these totals<br>on last slide only";
            this.checkEditShowTotalsLastSlideBundle.Size = new System.Drawing.Size(142, 55);
            this.checkEditShowTotalsLastSlideBundle.StyleController = this.styleController;
            this.checkEditShowTotalsLastSlideBundle.TabIndex = 13;
            this.checkEditShowTotalsLastSlideBundle.CheckedChanged += new System.EventHandler(this.checkEditShowTotalsLastSlide_CheckedChanged);
            // 
            // pnPackageBundleTotal
            // 
            this.pnPackageBundleTotal.Controls.Add(this.spinEditTotalInvestment);
            this.pnPackageBundleTotal.Controls.Add(this.labelControlPackageBundleTotalInvestment);
            this.pnPackageBundleTotal.Controls.Add(this.checkEditTotalCPM);
            this.pnPackageBundleTotal.Controls.Add(this.spinEditTotalImpressions);
            this.pnPackageBundleTotal.Controls.Add(this.labelControlPackageBundleTotalImpressions);
            this.pnPackageBundleTotal.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnPackageBundleTotal.Location = new System.Drawing.Point(440, 0);
            this.pnPackageBundleTotal.Name = "pnPackageBundleTotal";
            this.pnPackageBundleTotal.Size = new System.Drawing.Size(390, 55);
            this.pnPackageBundleTotal.TabIndex = 1;
            // 
            // spinEditTotalInvestment
            // 
            this.spinEditTotalInvestment.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditTotalInvestment.Location = new System.Drawing.Point(151, 30);
            this.spinEditTotalInvestment.Name = "spinEditTotalInvestment";
            this.spinEditTotalInvestment.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.spinEditTotalInvestment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditTotalInvestment.Properties.DisplayFormat.FormatString = "$#,###.00";
            this.spinEditTotalInvestment.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.spinEditTotalInvestment.Properties.EditFormat.FormatString = "$#,###.00";
            this.spinEditTotalInvestment.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.spinEditTotalInvestment.Properties.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.spinEditTotalInvestment.Size = new System.Drawing.Size(117, 22);
            this.spinEditTotalInvestment.StyleController = this.styleController;
            this.spinEditTotalInvestment.TabIndex = 14;
            this.spinEditTotalInvestment.EditValueChanged += new System.EventHandler(this.spinEditTotalImpressions_EditValueChanged);
            // 
            // labelControlPackageBundleTotalInvestment
            // 
            this.labelControlPackageBundleTotalInvestment.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labelControlPackageBundleTotalInvestment.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPackageBundleTotalInvestment.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlPackageBundleTotalInvestment.Location = new System.Drawing.Point(151, 3);
            this.labelControlPackageBundleTotalInvestment.Name = "labelControlPackageBundleTotalInvestment";
            this.labelControlPackageBundleTotalInvestment.Size = new System.Drawing.Size(117, 22);
            this.labelControlPackageBundleTotalInvestment.StyleController = this.styleController;
            this.labelControlPackageBundleTotalInvestment.TabIndex = 13;
            this.labelControlPackageBundleTotalInvestment.Text = "Total Investment: ";
            // 
            // checkEditTotalCPM
            // 
            this.checkEditTotalCPM.EditValue = true;
            this.checkEditTotalCPM.Location = new System.Drawing.Point(274, 3);
            this.checkEditTotalCPM.Name = "checkEditTotalCPM";
            this.checkEditTotalCPM.Properties.Caption = "CPM: ";
            this.checkEditTotalCPM.Size = new System.Drawing.Size(114, 21);
            this.checkEditTotalCPM.StyleController = this.styleController;
            this.checkEditTotalCPM.TabIndex = 12;
            this.checkEditTotalCPM.CheckedChanged += new System.EventHandler(this.checkEditCPM_CheckedChanged);
            // 
            // spinEditTotalImpressions
            // 
            this.spinEditTotalImpressions.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditTotalImpressions.Location = new System.Drawing.Point(7, 30);
            this.spinEditTotalImpressions.Name = "spinEditTotalImpressions";
            this.spinEditTotalImpressions.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.spinEditTotalImpressions.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditTotalImpressions.Properties.DisplayFormat.FormatString = "#,##0";
            this.spinEditTotalImpressions.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.spinEditTotalImpressions.Properties.EditFormat.FormatString = "#,##0";
            this.spinEditTotalImpressions.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.spinEditTotalImpressions.Properties.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spinEditTotalImpressions.Properties.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.spinEditTotalImpressions.Size = new System.Drawing.Size(117, 22);
            this.spinEditTotalImpressions.StyleController = this.styleController;
            this.spinEditTotalImpressions.TabIndex = 11;
            this.spinEditTotalImpressions.EditValueChanged += new System.EventHandler(this.spinEditTotalImpressions_EditValueChanged);
            // 
            // labelControlPackageBundleTotalImpressions
            // 
            this.labelControlPackageBundleTotalImpressions.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labelControlPackageBundleTotalImpressions.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPackageBundleTotalImpressions.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlPackageBundleTotalImpressions.Location = new System.Drawing.Point(7, 3);
            this.labelControlPackageBundleTotalImpressions.Name = "labelControlPackageBundleTotalImpressions";
            this.labelControlPackageBundleTotalImpressions.Size = new System.Drawing.Size(144, 22);
            this.labelControlPackageBundleTotalImpressions.StyleController = this.styleController;
            this.labelControlPackageBundleTotalImpressions.TabIndex = 7;
            this.labelControlPackageBundleTotalImpressions.Text = "Total Impressions: ";
            // 
            // pnPackageBundleMonthly
            // 
            this.pnPackageBundleMonthly.Controls.Add(this.spinEditMonthlyInvestment);
            this.pnPackageBundleMonthly.Controls.Add(this.labelControlPackageBundleMonthlyInvestment);
            this.pnPackageBundleMonthly.Controls.Add(this.checkEditMonthlyCPM);
            this.pnPackageBundleMonthly.Controls.Add(this.spinEditMonthlyImpressions);
            this.pnPackageBundleMonthly.Controls.Add(this.labelControlPackageBundleMonthlyImpressions);
            this.pnPackageBundleMonthly.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnPackageBundleMonthly.Location = new System.Drawing.Point(0, 0);
            this.pnPackageBundleMonthly.Name = "pnPackageBundleMonthly";
            this.pnPackageBundleMonthly.Size = new System.Drawing.Size(440, 55);
            this.pnPackageBundleMonthly.TabIndex = 0;
            // 
            // spinEditMonthlyInvestment
            // 
            this.spinEditMonthlyInvestment.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditMonthlyInvestment.Location = new System.Drawing.Point(159, 30);
            this.spinEditMonthlyInvestment.Name = "spinEditMonthlyInvestment";
            this.spinEditMonthlyInvestment.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.spinEditMonthlyInvestment.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditMonthlyInvestment.Properties.DisplayFormat.FormatString = "$#,###.00";
            this.spinEditMonthlyInvestment.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.spinEditMonthlyInvestment.Properties.EditFormat.FormatString = "$#,###.00";
            this.spinEditMonthlyInvestment.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.spinEditMonthlyInvestment.Properties.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.spinEditMonthlyInvestment.Size = new System.Drawing.Size(117, 22);
            this.spinEditMonthlyInvestment.StyleController = this.styleController;
            this.spinEditMonthlyInvestment.TabIndex = 14;
            this.spinEditMonthlyInvestment.EditValueChanged += new System.EventHandler(this.spinEditMonthly_EditValueChanged);
            // 
            // labelControlPackageBundleMonthlyInvestment
            // 
            this.labelControlPackageBundleMonthlyInvestment.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labelControlPackageBundleMonthlyInvestment.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPackageBundleMonthlyInvestment.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlPackageBundleMonthlyInvestment.Location = new System.Drawing.Point(159, 3);
            this.labelControlPackageBundleMonthlyInvestment.Name = "labelControlPackageBundleMonthlyInvestment";
            this.labelControlPackageBundleMonthlyInvestment.Size = new System.Drawing.Size(151, 22);
            this.labelControlPackageBundleMonthlyInvestment.StyleController = this.styleController;
            this.labelControlPackageBundleMonthlyInvestment.TabIndex = 13;
            this.labelControlPackageBundleMonthlyInvestment.Text = "Monthly Investment: ";
            // 
            // checkEditMonthlyCPM
            // 
            this.checkEditMonthlyCPM.EditValue = true;
            this.checkEditMonthlyCPM.Location = new System.Drawing.Point(316, 3);
            this.checkEditMonthlyCPM.Name = "checkEditMonthlyCPM";
            this.checkEditMonthlyCPM.Properties.Caption = "CPM: ";
            this.checkEditMonthlyCPM.Size = new System.Drawing.Size(124, 21);
            this.checkEditMonthlyCPM.StyleController = this.styleController;
            this.checkEditMonthlyCPM.TabIndex = 12;
            this.checkEditMonthlyCPM.CheckedChanged += new System.EventHandler(this.checkEditCPM_CheckedChanged);
            // 
            // spinEditMonthlyImpressions
            // 
            this.spinEditMonthlyImpressions.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinEditMonthlyImpressions.Location = new System.Drawing.Point(7, 30);
            this.spinEditMonthlyImpressions.Name = "spinEditMonthlyImpressions";
            this.spinEditMonthlyImpressions.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.True;
            this.spinEditMonthlyImpressions.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.spinEditMonthlyImpressions.Properties.DisplayFormat.FormatString = "#,##0";
            this.spinEditMonthlyImpressions.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.spinEditMonthlyImpressions.Properties.EditFormat.FormatString = "#,##0";
            this.spinEditMonthlyImpressions.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.spinEditMonthlyImpressions.Properties.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spinEditMonthlyImpressions.Properties.MaxValue = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.spinEditMonthlyImpressions.Size = new System.Drawing.Size(117, 22);
            this.spinEditMonthlyImpressions.StyleController = this.styleController;
            this.spinEditMonthlyImpressions.TabIndex = 11;
            this.spinEditMonthlyImpressions.EditValueChanged += new System.EventHandler(this.spinEditMonthly_EditValueChanged);
            // 
            // labelControlPackageBundleMonthlyImpressions
            // 
            this.labelControlPackageBundleMonthlyImpressions.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labelControlPackageBundleMonthlyImpressions.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlPackageBundleMonthlyImpressions.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlPackageBundleMonthlyImpressions.Location = new System.Drawing.Point(7, 3);
            this.labelControlPackageBundleMonthlyImpressions.Name = "labelControlPackageBundleMonthlyImpressions";
            this.labelControlPackageBundleMonthlyImpressions.Size = new System.Drawing.Size(151, 22);
            this.labelControlPackageBundleMonthlyImpressions.StyleController = this.styleController;
            this.labelControlPackageBundleMonthlyImpressions.TabIndex = 7;
            this.labelControlPackageBundleMonthlyImpressions.Text = "Monthly Impressions: ";
            // 
            // ProductBundleControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnFooterPackageBundle);
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.pnHeader);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "ProductBundleControl";
            this.Size = new System.Drawing.Size(1000, 460);
            this.Load += new System.EventHandler(this.ProductBundleControl_Load);
            this.pnHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            this.pnMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProductBundle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridViewProductBundle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditInteger)).EndInit();
            this.pnFooterPackageBundle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEditShowTotalsLastSlideBundle.Properties)).EndInit();
            this.pnPackageBundleTotal.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinEditTotalInvestment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditTotalCPM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditTotalImpressions.Properties)).EndInit();
            this.pnPackageBundleMonthly.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMonthlyInvestment.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditMonthlyCPM.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinEditMonthlyImpressions.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnHeader;
        public DevExpress.XtraEditors.LabelControl labelControlAdvertiser;
        public DevExpress.XtraEditors.LabelControl labelControlPresentationDate;
        public DevExpress.XtraEditors.LabelControl labelControlDecisionMaker;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.Panel pnMain;
        public DevExpress.XtraEditors.LabelControl labelControlFlightDates;
        private DevExpress.XtraGrid.GridControl gridControlProductBundle;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridViewProductBundle;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnIndex;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnName;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditCurrency;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditInteger;
        private System.Windows.Forms.Panel pnFooterPackageBundle;
        private System.Windows.Forms.Panel pnPackageBundleMonthly;
        public DevExpress.XtraEditors.LabelControl labelControlPackageBundleMonthlyImpressions;
        private DevExpress.XtraEditors.SpinEdit spinEditMonthlyImpressions;
        public DevExpress.XtraEditors.LabelControl labelControlPackageBundleMonthlyInvestment;
        private DevExpress.XtraEditors.CheckEdit checkEditMonthlyCPM;
        private DevExpress.XtraEditors.SpinEdit spinEditMonthlyInvestment;
        private System.Windows.Forms.Panel pnPackageBundleTotal;
        private DevExpress.XtraEditors.SpinEdit spinEditTotalInvestment;
        public DevExpress.XtraEditors.LabelControl labelControlPackageBundleTotalInvestment;
        private DevExpress.XtraEditors.CheckEdit checkEditTotalCPM;
        private DevExpress.XtraEditors.SpinEdit spinEditTotalImpressions;
        public DevExpress.XtraEditors.LabelControl labelControlPackageBundleTotalImpressions;
        public DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSlideHeader;
        private DevExpress.XtraEditors.CheckEdit checkEditShowTotalsLastSlideBundle;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandProduct;
    }
}
