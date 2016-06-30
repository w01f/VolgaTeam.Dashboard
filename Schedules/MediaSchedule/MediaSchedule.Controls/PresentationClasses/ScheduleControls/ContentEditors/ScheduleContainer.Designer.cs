using DevExpress.Utils;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	partial class ScheduleContainer
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
			this.pnTop = new System.Windows.Forms.Panel();
			this.labelControlScheduleInfo = new DevExpress.XtraEditors.LabelControl();
			this.styleController = new DevExpress.XtraEditors.StyleController();
			this.quarterSelectorControl = new Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors.QuarterSelectorControl();
			this.pnBottom = new System.Windows.Forms.Panel();
			this.pnAgencyDiscount = new System.Windows.Forms.Panel();
			this.laAgencyDiscountValue = new System.Windows.Forms.Label();
			this.laAgencyDiscountTitle = new System.Windows.Forms.Label();
			this.pnNetRate = new System.Windows.Forms.Panel();
			this.laNetRateValue = new System.Windows.Forms.Label();
			this.laNetRateTitle = new System.Windows.Forms.Label();
			this.pnTotalCost = new System.Windows.Forms.Panel();
			this.laTotalCostValue = new System.Windows.Forms.Label();
			this.laTotalCostTitle = new System.Windows.Forms.Label();
			this.pnAvgRate = new System.Windows.Forms.Panel();
			this.laAvgRateValue = new System.Windows.Forms.Label();
			this.laAvgRateTitle = new System.Windows.Forms.Label();
			this.pnTotalCPP = new System.Windows.Forms.Panel();
			this.laTotalCPPValue = new System.Windows.Forms.Label();
			this.laTotalCPPTitle = new System.Windows.Forms.Label();
			this.pnTotalGRP = new System.Windows.Forms.Panel();
			this.laTotalGRPValue = new System.Windows.Forms.Label();
			this.laTotalGRPTitle = new System.Windows.Forms.Label();
			this.pnTotalSpots = new System.Windows.Forms.Panel();
			this.laTotalSpotsValue = new System.Windows.Forms.Label();
			this.laTotalSpotsTitle = new System.Windows.Forms.Label();
			this.pnTotalPeriods = new System.Windows.Forms.Panel();
			this.laTotalPeriodsValue = new System.Windows.Forms.Label();
			this.laTotalPeriodsTitle = new System.Windows.Forms.Label();
			this.labelControlFlexFlightDatesWarning = new DevExpress.XtraEditors.LabelControl();
			this.xtraTabControlSections = new DevExpress.XtraTab.XtraTabControl();
			this.pbNoSections = new System.Windows.Forms.PictureBox();
			this.retractableBarControl = new Asa.Common.GUI.RetractableBar.RetractableBarLeft();
			this.settingsContainer = new Settings.SettingsContainer();
			this.pnSections = new System.Windows.Forms.Panel();
			this.pnNoSections = new System.Windows.Forms.Panel();
			this.contextMenuStripSections = new System.Windows.Forms.ContextMenuStrip();
			this.toolStripMenuItemSnapshotRename = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemSnapshotClone = new System.Windows.Forms.ToolStripMenuItem();
			this.pnTop.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.styleController)).BeginInit();
			this.pnBottom.SuspendLayout();
			this.pnAgencyDiscount.SuspendLayout();
			this.pnNetRate.SuspendLayout();
			this.pnTotalCost.SuspendLayout();
			this.pnAvgRate.SuspendLayout();
			this.pnTotalCPP.SuspendLayout();
			this.pnTotalGRP.SuspendLayout();
			this.pnTotalSpots.SuspendLayout();
			this.pnTotalPeriods.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlSections)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbNoSections)).BeginInit();
			this.retractableBarControl.Content.SuspendLayout();
			this.pnSections.SuspendLayout();
			this.pnNoSections.SuspendLayout();
			this.contextMenuStripSections.SuspendLayout();
			this.SuspendLayout();
			// 
			// pnTop
			// 
			this.pnTop.Controls.Add(this.labelControlScheduleInfo);
			this.pnTop.Controls.Add(this.quarterSelectorControl);
			this.pnTop.Dock = System.Windows.Forms.DockStyle.Top;
			this.pnTop.Location = new System.Drawing.Point(300, 0);
			this.pnTop.Name = "pnTop";
			this.pnTop.Size = new System.Drawing.Size(792, 40);
			this.pnTop.TabIndex = 1;
			// 
			// labelControlScheduleInfo
			// 
			this.labelControlScheduleInfo.AllowHtmlString = true;
			this.labelControlScheduleInfo.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlScheduleInfo.AppearanceHovered.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
			this.labelControlScheduleInfo.AppearanceHovered.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlScheduleInfo.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlScheduleInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelControlScheduleInfo.Location = new System.Drawing.Point(0, 0);
			this.labelControlScheduleInfo.Name = "labelControlScheduleInfo";
			this.labelControlScheduleInfo.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.labelControlScheduleInfo.Size = new System.Drawing.Size(404, 40);
			this.labelControlScheduleInfo.StyleController = this.styleController;
			this.labelControlScheduleInfo.TabIndex = 52;
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
			// quarterSelectorControl
			// 
			this.quarterSelectorControl.BackColor = System.Drawing.Color.Transparent;
			this.quarterSelectorControl.Dock = System.Windows.Forms.DockStyle.Right;
			this.quarterSelectorControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.quarterSelectorControl.Location = new System.Drawing.Point(404, 0);
			this.quarterSelectorControl.Name = "quarterSelectorControl";
			this.quarterSelectorControl.Size = new System.Drawing.Size(388, 40);
			this.quarterSelectorControl.TabIndex = 51;
			// 
			// pnBottom
			// 
			this.pnBottom.Controls.Add(this.pnAgencyDiscount);
			this.pnBottom.Controls.Add(this.pnNetRate);
			this.pnBottom.Controls.Add(this.pnTotalCost);
			this.pnBottom.Controls.Add(this.pnAvgRate);
			this.pnBottom.Controls.Add(this.pnTotalCPP);
			this.pnBottom.Controls.Add(this.pnTotalGRP);
			this.pnBottom.Controls.Add(this.pnTotalSpots);
			this.pnBottom.Controls.Add(this.pnTotalPeriods);
			this.pnBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pnBottom.Location = new System.Drawing.Point(0, 498);
			this.pnBottom.Name = "pnBottom";
			this.pnBottom.Size = new System.Drawing.Size(1092, 43);
			this.pnBottom.TabIndex = 2;
			// 
			// pnAgencyDiscount
			// 
			this.pnAgencyDiscount.Controls.Add(this.laAgencyDiscountValue);
			this.pnAgencyDiscount.Controls.Add(this.laAgencyDiscountTitle);
			this.pnAgencyDiscount.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnAgencyDiscount.Location = new System.Drawing.Point(860, 0);
			this.pnAgencyDiscount.Name = "pnAgencyDiscount";
			this.pnAgencyDiscount.Size = new System.Drawing.Size(145, 43);
			this.pnAgencyDiscount.TabIndex = 6;
			// 
			// laAgencyDiscountValue
			// 
			this.laAgencyDiscountValue.Dock = System.Windows.Forms.DockStyle.Top;
			this.laAgencyDiscountValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAgencyDiscountValue.Location = new System.Drawing.Point(0, 19);
			this.laAgencyDiscountValue.Name = "laAgencyDiscountValue";
			this.laAgencyDiscountValue.Size = new System.Drawing.Size(145, 19);
			this.laAgencyDiscountValue.TabIndex = 2;
			this.laAgencyDiscountValue.Text = "Agency Discount:";
			this.laAgencyDiscountValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laAgencyDiscountTitle
			// 
			this.laAgencyDiscountTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laAgencyDiscountTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAgencyDiscountTitle.Location = new System.Drawing.Point(0, 0);
			this.laAgencyDiscountTitle.Name = "laAgencyDiscountTitle";
			this.laAgencyDiscountTitle.Size = new System.Drawing.Size(145, 19);
			this.laAgencyDiscountTitle.TabIndex = 1;
			this.laAgencyDiscountTitle.Text = "Agency Discount:";
			this.laAgencyDiscountTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnNetRate
			// 
			this.pnNetRate.Controls.Add(this.laNetRateValue);
			this.pnNetRate.Controls.Add(this.laNetRateTitle);
			this.pnNetRate.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnNetRate.Location = new System.Drawing.Point(720, 0);
			this.pnNetRate.Name = "pnNetRate";
			this.pnNetRate.Size = new System.Drawing.Size(140, 43);
			this.pnNetRate.TabIndex = 5;
			// 
			// laNetRateValue
			// 
			this.laNetRateValue.Dock = System.Windows.Forms.DockStyle.Top;
			this.laNetRateValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laNetRateValue.Location = new System.Drawing.Point(0, 19);
			this.laNetRateValue.Name = "laNetRateValue";
			this.laNetRateValue.Size = new System.Drawing.Size(140, 19);
			this.laNetRateValue.TabIndex = 2;
			this.laNetRateValue.Text = "Net Investment:";
			this.laNetRateValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laNetRateTitle
			// 
			this.laNetRateTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laNetRateTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laNetRateTitle.Location = new System.Drawing.Point(0, 0);
			this.laNetRateTitle.Name = "laNetRateTitle";
			this.laNetRateTitle.Size = new System.Drawing.Size(140, 19);
			this.laNetRateTitle.TabIndex = 1;
			this.laNetRateTitle.Text = "Net Investment:";
			this.laNetRateTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnTotalCost
			// 
			this.pnTotalCost.Controls.Add(this.laTotalCostValue);
			this.pnTotalCost.Controls.Add(this.laTotalCostTitle);
			this.pnTotalCost.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnTotalCost.Location = new System.Drawing.Point(575, 0);
			this.pnTotalCost.Name = "pnTotalCost";
			this.pnTotalCost.Size = new System.Drawing.Size(145, 43);
			this.pnTotalCost.TabIndex = 4;
			// 
			// laTotalCostValue
			// 
			this.laTotalCostValue.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalCostValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalCostValue.Location = new System.Drawing.Point(0, 19);
			this.laTotalCostValue.Name = "laTotalCostValue";
			this.laTotalCostValue.Size = new System.Drawing.Size(145, 19);
			this.laTotalCostValue.TabIndex = 2;
			this.laTotalCostValue.Text = "Gross Investment:";
			this.laTotalCostValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laTotalCostTitle
			// 
			this.laTotalCostTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalCostTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalCostTitle.Location = new System.Drawing.Point(0, 0);
			this.laTotalCostTitle.Name = "laTotalCostTitle";
			this.laTotalCostTitle.Size = new System.Drawing.Size(145, 19);
			this.laTotalCostTitle.TabIndex = 1;
			this.laTotalCostTitle.Text = "Gross Investment:";
			this.laTotalCostTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnAvgRate
			// 
			this.pnAvgRate.Controls.Add(this.laAvgRateValue);
			this.pnAvgRate.Controls.Add(this.laAvgRateTitle);
			this.pnAvgRate.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnAvgRate.Location = new System.Drawing.Point(460, 0);
			this.pnAvgRate.Name = "pnAvgRate";
			this.pnAvgRate.Size = new System.Drawing.Size(115, 43);
			this.pnAvgRate.TabIndex = 3;
			// 
			// laAvgRateValue
			// 
			this.laAvgRateValue.Dock = System.Windows.Forms.DockStyle.Top;
			this.laAvgRateValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAvgRateValue.Location = new System.Drawing.Point(0, 19);
			this.laAvgRateValue.Name = "laAvgRateValue";
			this.laAvgRateValue.Size = new System.Drawing.Size(115, 19);
			this.laAvgRateValue.TabIndex = 2;
			this.laAvgRateValue.Text = "Average Rate:";
			this.laAvgRateValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laAvgRateTitle
			// 
			this.laAvgRateTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laAvgRateTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laAvgRateTitle.Location = new System.Drawing.Point(0, 0);
			this.laAvgRateTitle.Name = "laAvgRateTitle";
			this.laAvgRateTitle.Size = new System.Drawing.Size(115, 19);
			this.laAvgRateTitle.TabIndex = 1;
			this.laAvgRateTitle.Text = "Average Rate:";
			this.laAvgRateTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnTotalCPP
			// 
			this.pnTotalCPP.Controls.Add(this.laTotalCPPValue);
			this.pnTotalCPP.Controls.Add(this.laTotalCPPTitle);
			this.pnTotalCPP.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnTotalCPP.Location = new System.Drawing.Point(345, 0);
			this.pnTotalCPP.Name = "pnTotalCPP";
			this.pnTotalCPP.Size = new System.Drawing.Size(115, 43);
			this.pnTotalCPP.TabIndex = 2;
			// 
			// laTotalCPPValue
			// 
			this.laTotalCPPValue.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalCPPValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalCPPValue.Location = new System.Drawing.Point(0, 19);
			this.laTotalCPPValue.Name = "laTotalCPPValue";
			this.laTotalCPPValue.Size = new System.Drawing.Size(115, 19);
			this.laTotalCPPValue.TabIndex = 2;
			this.laTotalCPPValue.Text = "Overall CPP:";
			this.laTotalCPPValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laTotalCPPTitle
			// 
			this.laTotalCPPTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalCPPTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalCPPTitle.Location = new System.Drawing.Point(0, 0);
			this.laTotalCPPTitle.Name = "laTotalCPPTitle";
			this.laTotalCPPTitle.Size = new System.Drawing.Size(115, 19);
			this.laTotalCPPTitle.TabIndex = 1;
			this.laTotalCPPTitle.Text = "Overall CPP:";
			this.laTotalCPPTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnTotalGRP
			// 
			this.pnTotalGRP.Controls.Add(this.laTotalGRPValue);
			this.pnTotalGRP.Controls.Add(this.laTotalGRPTitle);
			this.pnTotalGRP.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnTotalGRP.Location = new System.Drawing.Point(230, 0);
			this.pnTotalGRP.Name = "pnTotalGRP";
			this.pnTotalGRP.Size = new System.Drawing.Size(115, 43);
			this.pnTotalGRP.TabIndex = 1;
			// 
			// laTotalGRPValue
			// 
			this.laTotalGRPValue.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalGRPValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalGRPValue.Location = new System.Drawing.Point(0, 19);
			this.laTotalGRPValue.Name = "laTotalGRPValue";
			this.laTotalGRPValue.Size = new System.Drawing.Size(115, 19);
			this.laTotalGRPValue.TabIndex = 2;
			this.laTotalGRPValue.Text = "Total GRPs:";
			this.laTotalGRPValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laTotalGRPTitle
			// 
			this.laTotalGRPTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalGRPTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalGRPTitle.Location = new System.Drawing.Point(0, 0);
			this.laTotalGRPTitle.Name = "laTotalGRPTitle";
			this.laTotalGRPTitle.Size = new System.Drawing.Size(115, 19);
			this.laTotalGRPTitle.TabIndex = 1;
			this.laTotalGRPTitle.Text = "Total GRPs:";
			this.laTotalGRPTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnTotalSpots
			// 
			this.pnTotalSpots.Controls.Add(this.laTotalSpotsValue);
			this.pnTotalSpots.Controls.Add(this.laTotalSpotsTitle);
			this.pnTotalSpots.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnTotalSpots.Location = new System.Drawing.Point(115, 0);
			this.pnTotalSpots.Name = "pnTotalSpots";
			this.pnTotalSpots.Size = new System.Drawing.Size(115, 43);
			this.pnTotalSpots.TabIndex = 7;
			// 
			// laTotalSpotsValue
			// 
			this.laTotalSpotsValue.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalSpotsValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalSpotsValue.Location = new System.Drawing.Point(0, 19);
			this.laTotalSpotsValue.Name = "laTotalSpotsValue";
			this.laTotalSpotsValue.Size = new System.Drawing.Size(115, 19);
			this.laTotalSpotsValue.TabIndex = 2;
			this.laTotalSpotsValue.Text = "Total Spots:";
			this.laTotalSpotsValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laTotalSpotsTitle
			// 
			this.laTotalSpotsTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalSpotsTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalSpotsTitle.Location = new System.Drawing.Point(0, 0);
			this.laTotalSpotsTitle.Name = "laTotalSpotsTitle";
			this.laTotalSpotsTitle.Size = new System.Drawing.Size(115, 19);
			this.laTotalSpotsTitle.TabIndex = 1;
			this.laTotalSpotsTitle.Text = "Total Spots:";
			this.laTotalSpotsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pnTotalPeriods
			// 
			this.pnTotalPeriods.Controls.Add(this.laTotalPeriodsValue);
			this.pnTotalPeriods.Controls.Add(this.laTotalPeriodsTitle);
			this.pnTotalPeriods.Dock = System.Windows.Forms.DockStyle.Left;
			this.pnTotalPeriods.Location = new System.Drawing.Point(0, 0);
			this.pnTotalPeriods.Name = "pnTotalPeriods";
			this.pnTotalPeriods.Size = new System.Drawing.Size(115, 43);
			this.pnTotalPeriods.TabIndex = 0;
			// 
			// laTotalPeriodsValue
			// 
			this.laTotalPeriodsValue.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalPeriodsValue.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalPeriodsValue.Location = new System.Drawing.Point(0, 19);
			this.laTotalPeriodsValue.Name = "laTotalPeriodsValue";
			this.laTotalPeriodsValue.Size = new System.Drawing.Size(115, 19);
			this.laTotalPeriodsValue.TabIndex = 2;
			this.laTotalPeriodsValue.Text = "Total Weeks:";
			this.laTotalPeriodsValue.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// laTotalPeriodsTitle
			// 
			this.laTotalPeriodsTitle.Dock = System.Windows.Forms.DockStyle.Top;
			this.laTotalPeriodsTitle.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.laTotalPeriodsTitle.Location = new System.Drawing.Point(0, 0);
			this.laTotalPeriodsTitle.Name = "laTotalPeriodsTitle";
			this.laTotalPeriodsTitle.Size = new System.Drawing.Size(115, 19);
			this.laTotalPeriodsTitle.TabIndex = 1;
			this.laTotalPeriodsTitle.Text = "Total Weeks:";
			this.laTotalPeriodsTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelControlFlexFlightDatesWarning
			// 
			this.labelControlFlexFlightDatesWarning.AllowHtmlString = true;
			this.labelControlFlexFlightDatesWarning.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.labelControlFlexFlightDatesWarning.Appearance.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelControlFlexFlightDatesWarning.Appearance.ForeColor = System.Drawing.Color.Red;
			this.labelControlFlexFlightDatesWarning.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
			this.labelControlFlexFlightDatesWarning.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
			this.labelControlFlexFlightDatesWarning.Cursor = System.Windows.Forms.Cursors.Hand;
			this.labelControlFlexFlightDatesWarning.Dock = System.Windows.Forms.DockStyle.Top;
			this.labelControlFlexFlightDatesWarning.Location = new System.Drawing.Point(300, 40);
			this.labelControlFlexFlightDatesWarning.LookAndFeel.UseDefaultLookAndFeel = false;
			this.labelControlFlexFlightDatesWarning.Name = "labelControlFlexFlightDatesWarning";
			this.labelControlFlexFlightDatesWarning.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.labelControlFlexFlightDatesWarning.Size = new System.Drawing.Size(792, 27);
			this.labelControlFlexFlightDatesWarning.TabIndex = 4;
			this.labelControlFlexFlightDatesWarning.Text = "*You have PARTIAL WEEKS in your schedule. <u><b><color=red>CLICK HERE</color></b>" +
    "</u>";
			this.labelControlFlexFlightDatesWarning.Click += new System.EventHandler(this.OnFlexFlightDatesWarningClick);
			// 
			// xtraTabControlSections
			// 
			this.xtraTabControlSections.AllowDrop = true;
			this.xtraTabControlSections.Appearance.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlSections.Appearance.Options.UseFont = true;
			this.xtraTabControlSections.AppearancePage.Header.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlSections.AppearancePage.Header.Options.UseFont = true;
			this.xtraTabControlSections.AppearancePage.Header.TextOptions.HotkeyPrefix = HKeyPrefix.None;
			this.xtraTabControlSections.AppearancePage.HeaderActive.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.xtraTabControlSections.AppearancePage.HeaderActive.Options.UseFont = true;
			this.xtraTabControlSections.AppearancePage.HeaderActive.TextOptions.HotkeyPrefix = HKeyPrefix.None;
			this.xtraTabControlSections.AppearancePage.HeaderDisabled.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlSections.AppearancePage.HeaderDisabled.Options.UseFont = true;
			this.xtraTabControlSections.AppearancePage.HeaderHotTracked.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlSections.AppearancePage.HeaderHotTracked.Options.UseFont = true;
			this.xtraTabControlSections.AppearancePage.PageClient.Font = new System.Drawing.Font("Arial", 9.75F);
			this.xtraTabControlSections.AppearancePage.PageClient.Options.UseFont = true;
			this.xtraTabControlSections.ClosePageButtonShowMode = DevExpress.XtraTab.ClosePageButtonShowMode.InAllTabPageHeaders;
			this.xtraTabControlSections.Dock = System.Windows.Forms.DockStyle.Fill;
			this.xtraTabControlSections.Location = new System.Drawing.Point(300, 67);
			this.xtraTabControlSections.Name = "xtraTabControlSections";
			this.xtraTabControlSections.Size = new System.Drawing.Size(792, 431);
			this.xtraTabControlSections.TabIndex = 5;
			this.xtraTabControlSections.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.OnSelectedSectionChanged);
			this.xtraTabControlSections.CloseButtonClick += new System.EventHandler(this.OnSectionTabCloseClick);
			this.xtraTabControlSections.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnSectionTabMouseDown);
			// 
			// pbNoSections
			// 
			this.pbNoSections.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pbNoSections.Image = global::Asa.Media.Controls.Properties.Resources.SectionNoRecords;
			this.pbNoSections.Location = new System.Drawing.Point(40, 20);
			this.pbNoSections.Name = "pbNoSections";
			this.pbNoSections.Size = new System.Drawing.Size(140, 60);
			this.pbNoSections.TabIndex = 1;
			this.pbNoSections.TabStop = false;
			// 
			// retractableBarControl
			// 
			this.retractableBarControl.AnimationDelay = 0;
			this.retractableBarControl.BackColor = System.Drawing.Color.Transparent;
			// 
			// retractableBarControl.Content
			// 
			this.retractableBarControl.Content.Controls.Add(this.settingsContainer);
			this.retractableBarControl.Content.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBarControl.Content.Location = new System.Drawing.Point(2, 42);
			this.retractableBarControl.Content.Name = "Content";
			this.retractableBarControl.Content.Size = new System.Drawing.Size(296, 454);
			this.retractableBarControl.Content.TabIndex = 1;
			this.retractableBarControl.ContentSize = 300;
			this.retractableBarControl.Dock = System.Windows.Forms.DockStyle.Left;
			this.retractableBarControl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			// 
			// retractableBarControl.Header
			// 
			this.retractableBarControl.Header.Dock = System.Windows.Forms.DockStyle.Fill;
			this.retractableBarControl.Header.Location = new System.Drawing.Point(49, 2);
			this.retractableBarControl.Header.Name = "Header";
			this.retractableBarControl.Header.Size = new System.Drawing.Size(245, 36);
			this.retractableBarControl.Header.TabIndex = 2;
			this.retractableBarControl.Location = new System.Drawing.Point(0, 0);
			this.retractableBarControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.retractableBarControl.Name = "retractableBarControl";
			this.retractableBarControl.Size = new System.Drawing.Size(300, 498);
			this.retractableBarControl.TabIndex = 4;
			// 
			// settingsContainer
			// 
			this.settingsContainer.BackColor = System.Drawing.Color.White;
			this.settingsContainer.Dock = System.Windows.Forms.DockStyle.Fill;
			this.settingsContainer.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.settingsContainer.Location = new System.Drawing.Point(0, 0);
			this.settingsContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
			this.settingsContainer.Name = "settingsContainer";
			this.settingsContainer.Size = new System.Drawing.Size(296, 454);
			this.settingsContainer.TabIndex = 0;
			// 
			// pnSections
			// 
			this.pnSections.Controls.Add(this.xtraTabControlSections);
			this.pnSections.Controls.Add(this.labelControlFlexFlightDatesWarning);
			this.pnSections.Controls.Add(this.pnTop);
			this.pnSections.Controls.Add(this.retractableBarControl);
			this.pnSections.Controls.Add(this.pnBottom);
			this.pnSections.Location = new System.Drawing.Point(3, 3);
			this.pnSections.Name = "pnSections";
			this.pnSections.Size = new System.Drawing.Size(1092, 541);
			this.pnSections.TabIndex = 6;
			// 
			// pnNoSections
			// 
			this.pnNoSections.Controls.Add(this.pbNoSections);
			this.pnNoSections.Location = new System.Drawing.Point(246, 530);
			this.pnNoSections.Name = "pnNoSections";
			this.pnNoSections.Padding = new System.Windows.Forms.Padding(40, 20, 20, 20);
			this.pnNoSections.Size = new System.Drawing.Size(200, 100);
			this.pnNoSections.TabIndex = 7;
			// 
			// contextMenuStripSections
			// 
			this.contextMenuStripSections.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemSnapshotRename,
            this.toolStripMenuItemSnapshotClone});
			this.contextMenuStripSections.Name = "contextMenuStripSnapshot";
			this.contextMenuStripSections.Size = new System.Drawing.Size(118, 48);
			// 
			// toolStripMenuItemSnapshotRename
			// 
			this.toolStripMenuItemSnapshotRename.Name = "toolStripMenuItemSnapshotRename";
			this.toolStripMenuItemSnapshotRename.Size = new System.Drawing.Size(117, 22);
			this.toolStripMenuItemSnapshotRename.Text = "Rename";
			this.toolStripMenuItemSnapshotRename.Click += new System.EventHandler(this.OnRenameSectionClick);
			// 
			// toolStripMenuItemSnapshotClone
			// 
			this.toolStripMenuItemSnapshotClone.Name = "toolStripMenuItemSnapshotClone";
			this.toolStripMenuItemSnapshotClone.Size = new System.Drawing.Size(117, 22);
			this.toolStripMenuItemSnapshotClone.Text = "Clone";
			this.toolStripMenuItemSnapshotClone.Click += new System.EventHandler(this.OnCloneSectionClick);
			// 
			// ScheduleContainer
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.BackColor = System.Drawing.Color.White;
			this.Controls.Add(this.pnNoSections);
			this.Controls.Add(this.pnSections);
			this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.Name = "ScheduleContainer";
			this.Size = new System.Drawing.Size(1098, 593);
			this.pnTop.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.styleController)).EndInit();
			this.pnBottom.ResumeLayout(false);
			this.pnAgencyDiscount.ResumeLayout(false);
			this.pnNetRate.ResumeLayout(false);
			this.pnTotalCost.ResumeLayout(false);
			this.pnAvgRate.ResumeLayout(false);
			this.pnTotalCPP.ResumeLayout(false);
			this.pnTotalGRP.ResumeLayout(false);
			this.pnTotalSpots.ResumeLayout(false);
			this.pnTotalPeriods.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.xtraTabControlSections)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbNoSections)).EndInit();
			this.retractableBarControl.Content.ResumeLayout(false);
			this.pnSections.ResumeLayout(false);
			this.pnNoSections.ResumeLayout(false);
			this.contextMenuStripSections.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

		protected System.Windows.Forms.Panel pnTop;
		protected System.Windows.Forms.Panel pnBottom;
		private DevExpress.XtraEditors.StyleController styleController;
        private System.Windows.Forms.Panel pnAgencyDiscount;
        private System.Windows.Forms.Label laAgencyDiscountValue;
        private System.Windows.Forms.Label laAgencyDiscountTitle;
        private System.Windows.Forms.Panel pnNetRate;
        private System.Windows.Forms.Label laNetRateValue;
        private System.Windows.Forms.Label laNetRateTitle;
        private System.Windows.Forms.Panel pnTotalCost;
        private System.Windows.Forms.Label laTotalCostValue;
        private System.Windows.Forms.Label laTotalCostTitle;
        private System.Windows.Forms.Panel pnAvgRate;
        private System.Windows.Forms.Label laAvgRateValue;
        private System.Windows.Forms.Label laAvgRateTitle;
        private System.Windows.Forms.Panel pnTotalCPP;
        private System.Windows.Forms.Label laTotalCPPValue;
        private System.Windows.Forms.Label laTotalCPPTitle;
        private System.Windows.Forms.Panel pnTotalGRP;
        private System.Windows.Forms.Label laTotalGRPValue;
        private System.Windows.Forms.Label laTotalGRPTitle;
        private System.Windows.Forms.Panel pnTotalPeriods;
		private System.Windows.Forms.Label laTotalPeriodsValue;
        private System.Windows.Forms.Panel pnTotalSpots;
        private System.Windows.Forms.Label laTotalSpotsValue;
		private System.Windows.Forms.Label laTotalSpotsTitle;
		protected System.Windows.Forms.Label laTotalPeriodsTitle;
		private QuarterSelectorControl quarterSelectorControl;
	    protected Common.GUI.RetractableBar.RetractableBarLeft retractableBarControl;
		private DevExpress.XtraEditors.LabelControl labelControlFlexFlightDatesWarning;
	    protected DevExpress.XtraEditors.LabelControl labelControlScheduleInfo;
		private System.Windows.Forms.PictureBox pbNoSections;
		private DevExpress.XtraTab.XtraTabControl xtraTabControlSections;
		private System.Windows.Forms.Panel pnSections;
		private System.Windows.Forms.Panel pnNoSections;
		private System.Windows.Forms.ContextMenuStrip contextMenuStripSections;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSnapshotRename;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSnapshotClone;
		private Settings.SettingsContainer settingsContainer;
	}
}
