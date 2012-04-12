namespace MobileScheduleBuilder.CustomControls
{
    partial class ProductSummaryControl
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
            this.pnFooterSummaryFormat = new System.Windows.Forms.Panel();
            this.pnCombinedTotals = new System.Windows.Forms.Panel();
            this.checkEditShowTotalsLastSlideSummary = new DevExpress.XtraEditors.CheckEdit();
            this.pnPackageSummaryTotal = new System.Windows.Forms.Panel();
            this.labelControlTotalInvestment = new DevExpress.XtraEditors.LabelControl();
            this.labelControlTotalImpressions = new DevExpress.XtraEditors.LabelControl();
            this.pnPackageSummaryMonthly = new System.Windows.Forms.Panel();
            this.labelControlMonthlyInvestment = new DevExpress.XtraEditors.LabelControl();
            this.labelControlMonthlyImpressions = new DevExpress.XtraEditors.LabelControl();
            this.pnMain = new System.Windows.Forms.Panel();
            this.gridControlProductSummary = new DevExpress.XtraGrid.GridControl();
            this.bandedGridViewProductSummary = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            this.gridBandProduct = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumnIndex = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumnName = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBandMonthly = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumnMonthlyInvestment = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemSpinEditCurrency = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.bandedGridColumnMonthlyImpressions = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.repositoryItemSpinEditInteger = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.bandedGridColumnMonthlyCPM = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.gridBandTotal = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            this.bandedGridColumnTotalInvestment = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumnTotalImpressions = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.bandedGridColumnTotalCPM = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn();
            this.pnHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
            this.pnFooterSummaryFormat.SuspendLayout();
            this.pnCombinedTotals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditShowTotalsLastSlideSummary.Properties)).BeginInit();
            this.pnPackageSummaryTotal.SuspendLayout();
            this.pnPackageSummaryMonthly.SuspendLayout();
            this.pnMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProductSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridViewProductSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditInteger)).BeginInit();
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
            this.comboBoxEditSlideHeader.Properties.NullText = "Your Mobile Campaign";
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
            // pnFooterSummaryFormat
            // 
            this.pnFooterSummaryFormat.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnFooterSummaryFormat.Controls.Add(this.pnCombinedTotals);
            this.pnFooterSummaryFormat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnFooterSummaryFormat.Location = new System.Drawing.Point(0, 403);
            this.pnFooterSummaryFormat.Name = "pnFooterSummaryFormat";
            this.pnFooterSummaryFormat.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.pnFooterSummaryFormat.Size = new System.Drawing.Size(1000, 57);
            this.pnFooterSummaryFormat.TabIndex = 4;
            // 
            // pnCombinedTotals
            // 
            this.pnCombinedTotals.Controls.Add(this.checkEditShowTotalsLastSlideSummary);
            this.pnCombinedTotals.Controls.Add(this.pnPackageSummaryTotal);
            this.pnCombinedTotals.Controls.Add(this.pnPackageSummaryMonthly);
            this.pnCombinedTotals.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnCombinedTotals.Location = new System.Drawing.Point(5, 0);
            this.pnCombinedTotals.Name = "pnCombinedTotals";
            this.pnCombinedTotals.Size = new System.Drawing.Size(991, 53);
            this.pnCombinedTotals.TabIndex = 9;
            // 
            // checkEditShowTotalsLastSlideSummary
            // 
            this.checkEditShowTotalsLastSlideSummary.Dock = System.Windows.Forms.DockStyle.Right;
            this.checkEditShowTotalsLastSlideSummary.Location = new System.Drawing.Point(849, 0);
            this.checkEditShowTotalsLastSlideSummary.Name = "checkEditShowTotalsLastSlideSummary";
            this.checkEditShowTotalsLastSlideSummary.Properties.AllowHtmlString = true;
            this.checkEditShowTotalsLastSlideSummary.Properties.Appearance.Options.UseTextOptions = true;
            this.checkEditShowTotalsLastSlideSummary.Properties.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.checkEditShowTotalsLastSlideSummary.Properties.AutoHeight = false;
            this.checkEditShowTotalsLastSlideSummary.Properties.Caption = "Show these totals<br>on last slide only";
            this.checkEditShowTotalsLastSlideSummary.Size = new System.Drawing.Size(142, 53);
            this.checkEditShowTotalsLastSlideSummary.StyleController = this.styleController;
            this.checkEditShowTotalsLastSlideSummary.TabIndex = 12;
            this.checkEditShowTotalsLastSlideSummary.CheckedChanged += new System.EventHandler(this.checkEditShowTotalsLastSlide_CheckedChanged);
            // 
            // pnPackageSummaryTotal
            // 
            this.pnPackageSummaryTotal.Controls.Add(this.labelControlTotalInvestment);
            this.pnPackageSummaryTotal.Controls.Add(this.labelControlTotalImpressions);
            this.pnPackageSummaryTotal.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnPackageSummaryTotal.Location = new System.Drawing.Point(233, 0);
            this.pnPackageSummaryTotal.Name = "pnPackageSummaryTotal";
            this.pnPackageSummaryTotal.Size = new System.Drawing.Size(235, 53);
            this.pnPackageSummaryTotal.TabIndex = 11;
            // 
            // labelControlTotalInvestment
            // 
            this.labelControlTotalInvestment.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labelControlTotalInvestment.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlTotalInvestment.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlTotalInvestment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelControlTotalInvestment.Location = new System.Drawing.Point(0, 30);
            this.labelControlTotalInvestment.Name = "labelControlTotalInvestment";
            this.labelControlTotalInvestment.Size = new System.Drawing.Size(235, 23);
            this.labelControlTotalInvestment.StyleController = this.styleController;
            this.labelControlTotalInvestment.TabIndex = 10;
            this.labelControlTotalInvestment.Text = "Total Investment: #";
            // 
            // labelControlTotalImpressions
            // 
            this.labelControlTotalImpressions.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labelControlTotalImpressions.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlTotalImpressions.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlTotalImpressions.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControlTotalImpressions.Location = new System.Drawing.Point(0, 0);
            this.labelControlTotalImpressions.Name = "labelControlTotalImpressions";
            this.labelControlTotalImpressions.Size = new System.Drawing.Size(235, 24);
            this.labelControlTotalImpressions.StyleController = this.styleController;
            this.labelControlTotalImpressions.TabIndex = 8;
            this.labelControlTotalImpressions.Text = "Total Impressions: #";
            // 
            // pnPackageSummaryMonthly
            // 
            this.pnPackageSummaryMonthly.Controls.Add(this.labelControlMonthlyInvestment);
            this.pnPackageSummaryMonthly.Controls.Add(this.labelControlMonthlyImpressions);
            this.pnPackageSummaryMonthly.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnPackageSummaryMonthly.Location = new System.Drawing.Point(0, 0);
            this.pnPackageSummaryMonthly.Name = "pnPackageSummaryMonthly";
            this.pnPackageSummaryMonthly.Size = new System.Drawing.Size(233, 53);
            this.pnPackageSummaryMonthly.TabIndex = 10;
            // 
            // labelControlMonthlyInvestment
            // 
            this.labelControlMonthlyInvestment.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labelControlMonthlyInvestment.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlMonthlyInvestment.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlMonthlyInvestment.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.labelControlMonthlyInvestment.Location = new System.Drawing.Point(0, 30);
            this.labelControlMonthlyInvestment.Name = "labelControlMonthlyInvestment";
            this.labelControlMonthlyInvestment.Size = new System.Drawing.Size(233, 23);
            this.labelControlMonthlyInvestment.StyleController = this.styleController;
            this.labelControlMonthlyInvestment.TabIndex = 9;
            this.labelControlMonthlyInvestment.Text = "Monthly Investment: #";
            // 
            // labelControlMonthlyImpressions
            // 
            this.labelControlMonthlyImpressions.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            this.labelControlMonthlyImpressions.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            this.labelControlMonthlyImpressions.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControlMonthlyImpressions.Dock = System.Windows.Forms.DockStyle.Top;
            this.labelControlMonthlyImpressions.Location = new System.Drawing.Point(0, 0);
            this.labelControlMonthlyImpressions.Name = "labelControlMonthlyImpressions";
            this.labelControlMonthlyImpressions.Size = new System.Drawing.Size(233, 24);
            this.labelControlMonthlyImpressions.StyleController = this.styleController;
            this.labelControlMonthlyImpressions.TabIndex = 7;
            this.labelControlMonthlyImpressions.Text = "Monthly Impressions: #";
            // 
            // pnMain
            // 
            this.pnMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnMain.Controls.Add(this.gridControlProductSummary);
            this.pnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnMain.Location = new System.Drawing.Point(0, 96);
            this.pnMain.Name = "pnMain";
            this.pnMain.Size = new System.Drawing.Size(1000, 307);
            this.pnMain.TabIndex = 5;
            // 
            // gridControlProductSummary
            // 
            this.gridControlProductSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlProductSummary.Location = new System.Drawing.Point(0, 0);
            this.gridControlProductSummary.MainView = this.bandedGridViewProductSummary;
            this.gridControlProductSummary.Name = "gridControlProductSummary";
            this.gridControlProductSummary.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEditCurrency,
            this.repositoryItemSpinEditInteger});
            this.gridControlProductSummary.Size = new System.Drawing.Size(996, 303);
            this.gridControlProductSummary.TabIndex = 0;
            this.gridControlProductSummary.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.bandedGridViewProductSummary});
            // 
            // bandedGridViewProductSummary
            // 
            this.bandedGridViewProductSummary.Appearance.BandPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.bandedGridViewProductSummary.Appearance.BandPanel.Options.UseFont = true;
            this.bandedGridViewProductSummary.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.bandedGridViewProductSummary.Appearance.HeaderPanel.Options.UseFont = true;
            this.bandedGridViewProductSummary.Appearance.Preview.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bandedGridViewProductSummary.Appearance.Preview.Options.UseFont = true;
            this.bandedGridViewProductSummary.Appearance.Row.Font = new System.Drawing.Font("Arial", 9.75F);
            this.bandedGridViewProductSummary.Appearance.Row.Options.UseFont = true;
            this.bandedGridViewProductSummary.Appearance.RowSeparator.BackColor = System.Drawing.Color.AliceBlue;
            this.bandedGridViewProductSummary.Appearance.RowSeparator.Options.UseBackColor = true;
            this.bandedGridViewProductSummary.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] {
            this.gridBandProduct,
            this.gridBandMonthly,
            this.gridBandTotal});
            this.bandedGridViewProductSummary.Columns.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn[] {
            this.bandedGridColumnIndex,
            this.bandedGridColumnName,
            this.bandedGridColumnMonthlyInvestment,
            this.bandedGridColumnMonthlyImpressions,
            this.bandedGridColumnMonthlyCPM,
            this.bandedGridColumnTotalInvestment,
            this.bandedGridColumnTotalImpressions,
            this.bandedGridColumnTotalCPM});
            this.bandedGridViewProductSummary.GridControl = this.gridControlProductSummary;
            this.bandedGridViewProductSummary.Name = "bandedGridViewProductSummary";
            this.bandedGridViewProductSummary.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridViewProductSummary.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.bandedGridViewProductSummary.OptionsBehavior.Editable = false;
            this.bandedGridViewProductSummary.OptionsBehavior.ReadOnly = true;
            this.bandedGridViewProductSummary.OptionsCustomization.AllowBandMoving = false;
            this.bandedGridViewProductSummary.OptionsCustomization.AllowBandResizing = false;
            this.bandedGridViewProductSummary.OptionsCustomization.AllowColumnMoving = false;
            this.bandedGridViewProductSummary.OptionsCustomization.AllowColumnResizing = false;
            this.bandedGridViewProductSummary.OptionsCustomization.AllowFilter = false;
            this.bandedGridViewProductSummary.OptionsCustomization.AllowGroup = false;
            this.bandedGridViewProductSummary.OptionsCustomization.AllowQuickHideColumns = false;
            this.bandedGridViewProductSummary.OptionsCustomization.AllowSort = false;
            this.bandedGridViewProductSummary.OptionsCustomization.ShowBandsInCustomizationForm = false;
            this.bandedGridViewProductSummary.OptionsMenu.EnableColumnMenu = false;
            this.bandedGridViewProductSummary.OptionsMenu.EnableFooterMenu = false;
            this.bandedGridViewProductSummary.OptionsMenu.EnableGroupPanelMenu = false;
            this.bandedGridViewProductSummary.OptionsMenu.ShowDateTimeGroupIntervalItems = false;
            this.bandedGridViewProductSummary.OptionsMenu.ShowGroupSortSummaryItems = false;
            this.bandedGridViewProductSummary.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.bandedGridViewProductSummary.OptionsSelection.EnableAppearanceHideSelection = false;
            this.bandedGridViewProductSummary.OptionsView.AutoCalcPreviewLineCount = true;
            this.bandedGridViewProductSummary.OptionsView.ShowDetailButtons = false;
            this.bandedGridViewProductSummary.OptionsView.ShowGroupPanel = false;
            this.bandedGridViewProductSummary.OptionsView.ShowIndicator = false;
            this.bandedGridViewProductSummary.OptionsView.ShowPreview = true;
            this.bandedGridViewProductSummary.PreviewFieldName = "Name";
            this.bandedGridViewProductSummary.PreviewIndent = 20;
            this.bandedGridViewProductSummary.RowHeight = 25;
            this.bandedGridViewProductSummary.RowSeparatorHeight = 10;
            this.bandedGridViewProductSummary.CalcPreviewText += new DevExpress.XtraGrid.Views.Grid.CalcPreviewTextEventHandler(this.bandedGridViewProductSummary_CalcPreviewText);
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
            // gridBandMonthly
            // 
            this.gridBandMonthly.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBandMonthly.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBandMonthly.Caption = "Monthly";
            this.gridBandMonthly.Columns.Add(this.bandedGridColumnMonthlyInvestment);
            this.gridBandMonthly.Columns.Add(this.bandedGridColumnMonthlyImpressions);
            this.gridBandMonthly.Columns.Add(this.bandedGridColumnMonthlyCPM);
            this.gridBandMonthly.MinWidth = 20;
            this.gridBandMonthly.Name = "gridBandMonthly";
            this.gridBandMonthly.Width = 270;
            // 
            // bandedGridColumnMonthlyInvestment
            // 
            this.bandedGridColumnMonthlyInvestment.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumnMonthlyInvestment.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnMonthlyInvestment.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumnMonthlyInvestment.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnMonthlyInvestment.Caption = "Investment";
            this.bandedGridColumnMonthlyInvestment.ColumnEdit = this.repositoryItemSpinEditCurrency;
            this.bandedGridColumnMonthlyInvestment.FieldName = "MonthlyInvestmentCalculated";
            this.bandedGridColumnMonthlyInvestment.Name = "bandedGridColumnMonthlyInvestment";
            this.bandedGridColumnMonthlyInvestment.Visible = true;
            this.bandedGridColumnMonthlyInvestment.Width = 101;
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
            // bandedGridColumnMonthlyImpressions
            // 
            this.bandedGridColumnMonthlyImpressions.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumnMonthlyImpressions.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnMonthlyImpressions.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumnMonthlyImpressions.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnMonthlyImpressions.Caption = "Impressions";
            this.bandedGridColumnMonthlyImpressions.ColumnEdit = this.repositoryItemSpinEditInteger;
            this.bandedGridColumnMonthlyImpressions.FieldName = "MonthlyImpressionsCalculated";
            this.bandedGridColumnMonthlyImpressions.Name = "bandedGridColumnMonthlyImpressions";
            this.bandedGridColumnMonthlyImpressions.Visible = true;
            this.bandedGridColumnMonthlyImpressions.Width = 95;
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
            // bandedGridColumnMonthlyCPM
            // 
            this.bandedGridColumnMonthlyCPM.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumnMonthlyCPM.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnMonthlyCPM.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumnMonthlyCPM.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnMonthlyCPM.Caption = "CPM";
            this.bandedGridColumnMonthlyCPM.ColumnEdit = this.repositoryItemSpinEditCurrency;
            this.bandedGridColumnMonthlyCPM.FieldName = "MonthlyCPMCalculated";
            this.bandedGridColumnMonthlyCPM.Name = "bandedGridColumnMonthlyCPM";
            this.bandedGridColumnMonthlyCPM.Visible = true;
            this.bandedGridColumnMonthlyCPM.Width = 74;
            // 
            // gridBandTotal
            // 
            this.gridBandTotal.AppearanceHeader.Options.UseTextOptions = true;
            this.gridBandTotal.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridBandTotal.Caption = "Total";
            this.gridBandTotal.Columns.Add(this.bandedGridColumnTotalInvestment);
            this.gridBandTotal.Columns.Add(this.bandedGridColumnTotalImpressions);
            this.gridBandTotal.Columns.Add(this.bandedGridColumnTotalCPM);
            this.gridBandTotal.MinWidth = 20;
            this.gridBandTotal.Name = "gridBandTotal";
            this.gridBandTotal.Width = 267;
            // 
            // bandedGridColumnTotalInvestment
            // 
            this.bandedGridColumnTotalInvestment.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumnTotalInvestment.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnTotalInvestment.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumnTotalInvestment.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnTotalInvestment.Caption = "Investment";
            this.bandedGridColumnTotalInvestment.ColumnEdit = this.repositoryItemSpinEditCurrency;
            this.bandedGridColumnTotalInvestment.FieldName = "TotalInvestmentCalculated";
            this.bandedGridColumnTotalInvestment.Name = "bandedGridColumnTotalInvestment";
            this.bandedGridColumnTotalInvestment.Visible = true;
            this.bandedGridColumnTotalInvestment.Width = 88;
            // 
            // bandedGridColumnTotalImpressions
            // 
            this.bandedGridColumnTotalImpressions.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumnTotalImpressions.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnTotalImpressions.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumnTotalImpressions.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnTotalImpressions.Caption = "Impressions";
            this.bandedGridColumnTotalImpressions.ColumnEdit = this.repositoryItemSpinEditInteger;
            this.bandedGridColumnTotalImpressions.FieldName = "TotalImpressionsCalculated";
            this.bandedGridColumnTotalImpressions.Name = "bandedGridColumnTotalImpressions";
            this.bandedGridColumnTotalImpressions.Visible = true;
            this.bandedGridColumnTotalImpressions.Width = 91;
            // 
            // bandedGridColumnTotalCPM
            // 
            this.bandedGridColumnTotalCPM.AppearanceCell.Options.UseTextOptions = true;
            this.bandedGridColumnTotalCPM.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnTotalCPM.AppearanceHeader.Options.UseTextOptions = true;
            this.bandedGridColumnTotalCPM.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.bandedGridColumnTotalCPM.Caption = "CPM";
            this.bandedGridColumnTotalCPM.ColumnEdit = this.repositoryItemSpinEditCurrency;
            this.bandedGridColumnTotalCPM.FieldName = "TotalCPMCalculated";
            this.bandedGridColumnTotalCPM.Name = "bandedGridColumnTotalCPM";
            this.bandedGridColumnTotalCPM.Visible = true;
            this.bandedGridColumnTotalCPM.Width = 88;
            // 
            // ProductSummaryControl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(175)))), ((int)(((byte)(210)))), ((int)(((byte)(255)))));
            this.Controls.Add(this.pnMain);
            this.Controls.Add(this.pnFooterSummaryFormat);
            this.Controls.Add(this.pnHeader);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Name = "ProductSummaryControl";
            this.Size = new System.Drawing.Size(1000, 460);
            this.pnHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.comboBoxEditSlideHeader.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
            this.pnFooterSummaryFormat.ResumeLayout(false);
            this.pnCombinedTotals.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEditShowTotalsLastSlideSummary.Properties)).EndInit();
            this.pnPackageSummaryTotal.ResumeLayout(false);
            this.pnPackageSummaryMonthly.ResumeLayout(false);
            this.pnMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlProductSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bandedGridViewProductSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEditInteger)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnHeader;
        public DevExpress.XtraEditors.LabelControl labelControlAdvertiser;
        public DevExpress.XtraEditors.LabelControl labelControlPresentationDate;
        public DevExpress.XtraEditors.LabelControl labelControlDecisionMaker;
        private DevExpress.XtraEditors.StyleController styleController;
        private DevExpress.LookAndFeel.DefaultLookAndFeel defaultLookAndFeel;
        private System.Windows.Forms.Panel pnFooterSummaryFormat;
        private System.Windows.Forms.Panel pnMain;
        public DevExpress.XtraEditors.LabelControl labelControlFlightDates;
        private System.Windows.Forms.Panel pnCombinedTotals;
        private DevExpress.XtraGrid.GridControl gridControlProductSummary;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridViewProductSummary;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnIndex;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnMonthlyInvestment;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditCurrency;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnMonthlyImpressions;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEditInteger;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnMonthlyCPM;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandProduct;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandMonthly;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBandTotal;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnTotalInvestment;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnTotalImpressions;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridColumn bandedGridColumnTotalCPM;
        public DevExpress.XtraEditors.ComboBoxEdit comboBoxEditSlideHeader;
        private DevExpress.XtraEditors.CheckEdit checkEditShowTotalsLastSlideSummary;
        private System.Windows.Forms.Panel pnPackageSummaryTotal;
        public DevExpress.XtraEditors.LabelControl labelControlTotalInvestment;
        public DevExpress.XtraEditors.LabelControl labelControlTotalImpressions;
        private System.Windows.Forms.Panel pnPackageSummaryMonthly;
        public DevExpress.XtraEditors.LabelControl labelControlMonthlyInvestment;
        public DevExpress.XtraEditors.LabelControl labelControlMonthlyImpressions;
    }
}
