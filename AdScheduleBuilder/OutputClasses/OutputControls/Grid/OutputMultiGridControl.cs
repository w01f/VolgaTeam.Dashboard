using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class OutputMultiGridControl : UserControl, IGridOutputControl
    {
        private static OutputMultiGridControl _instance;
        private DevExpress.XtraGrid.Columns.GridColumn _activeCol = null;
        public BusinessClasses.Schedule LocalSchedule { get; set; }
        private List<BusinessClasses.Insert> _inserts = new List<BusinessClasses.Insert>();
        public PrintControl PrintColumns { get; private set; }
        public AdNotesControl AdNotes { get; private set; }
        public SlideBulletsControl SlideBullets { get; private set; }
        public SlideHeaderControl SlideHeader { get; private set; }

        public bool AllowToSave { get; set; }
        public bool SettingsNotSaved { get; set; }

        public DevComponents.DotNetBar.SuperTooltipInfo HelpToolTip { get; private set; }

        public ConfigurationClasses.SlideBulletsState SlideBulletsState
        {
            get
            {
                return this.LocalSchedule.ViewSettings.MultiGridViewSettings.SlideBulletsState;
            }
        }

        public ConfigurationClasses.SlideHeaderState SlideHeaderState
        {
            get
            {
                return this.LocalSchedule.ViewSettings.MultiGridViewSettings.SlideHeaderState;
            }
        }

        public bool ShowOptions { get; set; }
        public int SelectedOptionChapterIndex { get; set; }

        #region Column Positions
        public int PositionID { get; set; }
        public int PositionIndex { get; set; }
        public int PositionDate { get; set; }
        public int PositionPCI { get; set; }
        public int PositionCost { get; set; }
        public int PositionFinalCost { get; set; }
        public int PositionDiscount { get; set; }
        public int PositionColor { get; set; }
        public int PositionPublication { get; set; }
        public int PositionSquare { get; set; }
        public int PositionPageSize { get; set; }
        public int PositionPercentOfPage { get; set; }
        public int PositionDimensions { get; set; }
        public int PositionMechanicals { get; set; }
        public int PositionSection { get; set; }
        public int PositionDelivery { get; set; }
        public int PositionReadership { get; set; }
        public int PositionDeadline { get; set; }
        #endregion

        #region Column Widths
        public int WidthID { get; set; }
        public int WidthIndex { get; set; }
        public int WidthDate { get; set; }
        public int WidthPCI { get; set; }
        public int WidthCost { get; set; }
        public int WidthFinalCost { get; set; }
        public int WidthDiscount { get; set; }
        public int WidthColor { get; set; }
        public int WidthPublication { get; set; }
        public int WidthSquare { get; set; }
        public int WidthPageSize { get; set; }
        public int WidthPercentOfPage { get; set; }
        public int WidthDimensions { get; set; }
        public int WidthMechanicals { get; set; }
        public int WidthSection { get; set; }
        public int WidthDelivery { get; set; }
        public int WidthReadership { get; set; }
        public int WidthDeadline { get; set; }
        #endregion

        #region Column Captions
        public string CaptionID { get; set; }
        public string CaptionIndex { get; set; }
        public string CaptionDate { get; set; }
        public string CaptionPCI { get; set; }
        public string CaptionCost { get; set; }
        public string CaptionFinalCost { get; set; }
        public string CaptionDiscount { get; set; }
        public string CaptionColor { get; set; }
        public string CaptionPublication { get; set; }
        public string CaptionSquare { get; set; }
        public string CaptionPageSize { get; set; }
        public string CaptionPercentOfPage { get; set; }
        public string CaptionDimensions { get; set; }
        public string CaptionMechanicals { get; set; }
        public string CaptionSection { get; set; }
        public string CaptionDelivery { get; set; }
        public string CaptionReadership { get; set; }
        public string CaptionDeadline { get; set; }
        #endregion

        #region Enable Columns
        public bool EnableIDHeader { get; set; }
        public bool EnableIndexHeader { get; set; }
        public bool EnableDateHeader { get; set; }
        public bool EnableColorHeader { get; set; }
        public bool EnableSectionHeader { get; set; }
        public bool EnablePCIHeader { get; set; }
        public bool EnableFinalCostHeader { get; set; }
        public bool EnablePublicationHeader { get; set; }
        public bool EnablePercentOfPageHeader { get; set; }
        public bool EnableCostHeader { get; set; }
        public bool EnableDimensionsHeader { get; set; }
        public bool EnableMechanicalsHeader { get; set; }
        public bool EnableDeliveryHeader { get; set; }
        public bool EnableDiscountHeader { get; set; }
        public bool EnablePageSizeHeader { get; set; }
        public bool EnableSquareHeader { get; set; }
        public bool EnableDeadlineHeader { get; set; }
        public bool EnableReadershipHeader { get; set; }
        public bool EnableCommentsHeader { get; set; }
        #endregion

        #region Show Columns
        private bool _showIDHeader = true;
        private bool _showDateHeader = true;
        private bool _showPCIHeader = true;
        private bool _showCostHeader = true;
        private bool _showDiscountHeader = true;
        private bool _showColorHeader = true;
        private bool _showFinalCostHeader = true;
        private bool _showIndexHeader = true;
        private bool _showCommentsHeader = false;
        private bool _showSquareHeader = false;
        private bool _showPageSizeHeader = false;
        private bool _showPercentOfPageHeader = false;
        private bool _showMechanicalsHeader = false;
        private bool _showPublicationHeader = false;
        private bool _showDimensionsHeader = false;
        private bool _showSectionHeader = false;
        private bool _showReadershipHeader = false;
        private bool _showDeliveryHeader = false;
        private bool _showDeadlineHeader = false;

        public bool ShowIDHeader
        {
            get
            {
                return _showIDHeader;
            }
            set
            {
                _showIDHeader = value;
            }
        }
        public bool ShowDateHeader
        {
            get
            {
                return _showDateHeader;
            }
            set
            {
                _showDateHeader = value;
            }
        }
        public bool ShowPCIHeader
        {
            get
            {
                return _showPCIHeader;
            }
            set
            {
                _showPCIHeader = value;
            }
        }
        public bool ShowCostHeader
        {
            get
            {
                return _showCostHeader;
            }
            set
            {
                _showCostHeader = value;
            }
        }
        public bool ShowDiscountHeader
        {
            get
            {
                return _showDiscountHeader;
            }
            set
            {
                _showDiscountHeader = value;
            }
        }
        public bool ShowColorHeader
        {
            get
            {
                return _showColorHeader;
            }
            set
            {
                _showColorHeader = value;
            }
        }
        public bool ShowFinalCostHeader
        {
            get
            {
                return _showFinalCostHeader;
            }
            set
            {
                _showFinalCostHeader = value;
            }
        }
        public bool ShowIndexHeader
        {
            get
            {
                return _showIndexHeader;
            }
            set
            {
                _showIndexHeader = value;
            }
        }
        public bool ShowCommentsHeader
        {
            get
            {
                return _showCommentsHeader;
            }
            set
            {
                _showCommentsHeader = value;
            }
        }
        public bool ShowSquareHeader
        {
            get
            {
                return _showSquareHeader;
            }
            set
            {
                _showSquareHeader = value;
                this.ShowColumnInchesInPreview &= !value;
            }
        }
        public bool ShowPageSizeHeader
        {
            get
            {
                return _showPageSizeHeader;
            }
            set
            {
                _showPageSizeHeader = value;
                this.ShowPageSizeInPreview &= !value;
            }
        }
        public bool ShowPercentOfPageHeader
        {
            get
            {
                return _showPercentOfPageHeader;
            }
            set
            {
                _showPercentOfPageHeader = value;
                this.ShowPercentOfPageInPreview &= !value;
            }
        }
        public bool ShowMechanicalsHeader
        {
            get
            {
                return _showMechanicalsHeader;
            }
            set
            {
                _showMechanicalsHeader = value;
                this.ShowMechanicalsInPreview &= !value;
            }
        }
        public bool ShowPublicationHeader
        {
            get
            {
                return _showPublicationHeader;
            }
            set
            {
                _showPublicationHeader = value;
                this.ShowPublicationInPreview &= !value;
            }
        }
        public bool ShowDimensionsHeader
        {
            get
            {
                return _showDimensionsHeader;
            }
            set
            {
                _showDimensionsHeader = value;
                this.ShowDimensionsInPreview &= !value;
            }
        }
        public bool ShowSectionHeader
        {
            get
            {
                return _showSectionHeader;
            }
            set
            {
                _showSectionHeader = value;
                this.ShowSectionInPreview &= !value;
            }
        }
        public bool ShowReadershipHeader
        {
            get
            {
                return _showReadershipHeader;
            }
            set
            {
                _showReadershipHeader = value;
                this.ShowReadershipInPreview &= !value;
            }
        }
        public bool ShowDeliveryHeader
        {
            get
            {
                return _showDeliveryHeader;
            }
            set
            {
                _showDeliveryHeader = value;
                this.ShowDeliveryInPreview &= !value;
            }
        }
        public bool ShowDeadlineHeader
        {
            get
            {
                return _showDeadlineHeader;
            }
            set
            {
                _showDeadlineHeader = value;
                this.ShowDeadlineInPreview &= !value;
            }
        }
        #endregion

        #region Enable AdNotes
        public bool EnableCommentsInPreview { get; set; }
        public bool EnableSectionInPreview { get; set; }
        public bool EnableMechanicalsInPreview { get; set; }
        public bool EnableColumnInchesInPreview { get; set; }
        public bool EnablePublicationInPreview { get; set; }
        public bool EnablePageSizeInPreview { get; set; }
        public bool EnablePercentOfPageInPreview { get; set; }
        public bool EnableDimensionsInPreview { get; set; }
        public bool EnableReadershipInPreview { get; set; }
        public bool EnableDeliveryInPreview { get; set; }
        public bool EnableDeadlineInPreview { get; set; }
        #endregion

        #region Show AdNotes
        public bool ShowCommentsInPreview { get; set; }
        public bool ShowSectionInPreview { get; set; }
        public bool ShowMechanicalsInPreview { get; set; }
        public bool ShowColumnInchesInPreview { get; set; }
        public bool ShowPublicationInPreview { get; set; }
        public bool ShowPageSizeInPreview { get; set; }
        public bool ShowPercentOfPageInPreview { get; set; }
        public bool ShowDimensionsInPreview { get; set; }
        public bool ShowReadershipInPreview { get; set; }
        public bool ShowDeliveryInPreview { get; set; }
        public bool ShowDeadlineInPreview { get; set; }
        #endregion

        #region Position AdNotes
        public int PositionCommentsInPreview { get; set; }
        public int PositionSectionInPreview { get; set; }
        public int PositionMechanicalsInPreview { get; set; }
        public int PositionColumnInchesInPreview { get; set; }
        public int PositionPublicationInPreview { get; set; }
        public int PositionPageSizeInPreview { get; set; }
        public int PositionPercentOfPageInPreview { get; set; }
        public int PositionDimensionsInPreview { get; set; }
        public int PositionReadershipInPreview { get; set; }
        public int PositionDeliveryInPreview { get; set; }
        public int PositionDeadlineInPreview { get; set; }
        #endregion

        public int SelectedColumnsCount
        {
            get
            {
                int count = 0;
                if (this.ShowColorHeader)
                    count++;
                if (this.ShowCostHeader)
                    count++;
                if (this.ShowDateHeader)
                    count++;
                if (this.ShowDeadlineHeader)
                    count++;
                if (this.ShowDeliveryHeader)
                    count++;
                if (this.ShowDiscountHeader)
                    count++;
                if (this.ShowFinalCostHeader)
                    count++;
                if (this.ShowIDHeader)
                    count++;
                if (this.ShowIndexHeader)
                    count++;
                if (this.ShowMechanicalsHeader)
                    count++;
                if (this.ShowPageSizeHeader)
                    count++;
                if (this.ShowPercentOfPageHeader)
                    count++;
                if (this.ShowDimensionsHeader)
                    count++;
                if (this.ShowPCIHeader)
                    count++;
                if (this.ShowPublicationHeader)
                    count++;
                if (this.ShowReadershipHeader)
                    count++;
                if (this.ShowSectionHeader)
                    count++;
                if (this.ShowSquareHeader)
                    count++;
                return count;
            }
        }

        private OutputMultiGridControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.AllowToSave = true;

            textEditHeader.Hide();
            textEditHeader.Parent = this.gridControlPublication;

            this.PrintColumns = new PrintControl(this);
            this.AdNotes = new AdNotesControl(this);
            this.SlideBullets = new SlideBulletsControl(this);
            this.SlideBullets.checkEditColumnInches.Visible = false;
            this.SlideBullets.checkEditDimensions.Visible = false;
            this.SlideBullets.checkEditPageSize.Visible = false;
            this.SlideBullets.checkEditPercentOfPage.Visible = false;
            this.SlideBullets.checkEditDelivery.Visible = false;
            this.SlideBullets.checkEditReadership.Visible = false;
            this.SlideHeader = new SlideHeaderControl(this);
            this.SlideHeader.checkEditPublicationName.Visible = false;
            this.SlideHeader.checkEditLogo1.Visible = false;
            this.SlideHeader.checkEditLogo2.Visible = false;
            this.SlideHeader.checkEditLogo3.Visible = false;
            this.SlideHeader.checkEditLogo4.Visible = false;

            this.HelpToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Learn more about the Logo Grid", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);

            BusinessClasses.ScheduleManager.Instance.SettingsSaved += new EventHandler<BusinessClasses.SavingingEventArgs>((sender, e) =>
            {
                if (sender != this)
                    UpdateOutput(e.QuickSave);
            });
        }

        public static OutputMultiGridControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new OutputMultiGridControl();
                return _instance;
            }
        }

        #region Common Methods
        public static void RemoveInstance()
        {
            try
            {
                _instance.Dispose();
            }
            catch
            {
            }
            finally
            {
                _instance = null;
            }
        }

        private void UpdateSlideBullets()
        {
            this.SlideBullets.TotalInserts = this.LocalSchedule.Publications.Sum(x => x.TotalInserts).ToString("#,##0");
            this.SlideBullets.AvgAdCost = this.LocalSchedule.Publications.Count > 0 ? this.LocalSchedule.Publications.Average(x => x.AvgADRate).ToString("$#,###.00") : string.Empty;
            this.SlideBullets.AvgFinalCost = this.LocalSchedule.Publications.Count > 0 ? this.LocalSchedule.Publications.Average(x => x.AvgFinalRate).ToString("$#,###.00") : string.Empty;
            this.SlideBullets.AvgPCI = this.LocalSchedule.Publications.Count > 0 ? this.LocalSchedule.Publications.Average(x => x.AvgPCIRate).ToString("$#,###.00") : string.Empty;
            this.SlideBullets.TotalColor = this.LocalSchedule.Publications.Sum(x => x.TotalColorPricingCalculated).ToString("$#,###.00");
            this.SlideBullets.Discounts = this.LocalSchedule.Publications.Sum(x => x.TotalDiscountRate).ToString("$#,###.00");
            this.SlideBullets.TotalFinalCost = this.LocalSchedule.Publications.Sum(x => x.TotalFinalRate).ToString("$#,###.00");
            this.SlideBullets.TotalSquare = this.LocalSchedule.Publications.Sum(x => x.TotalSquare.HasValue ? x.TotalSquare.Value : 0).ToString("#,###.00#");
        }

        private void PrepareInserts()
        {
            _inserts.Clear();
            foreach (BusinessClasses.Publication publication in this.LocalSchedule.Publications)
                _inserts.AddRange(publication.Inserts);
            _inserts.Sort((x, y) => x.Date.CompareTo(y.Date));
        }

        public void UpdateOutput(bool quickLoad)
        {
            this.LocalSchedule = BusinessClasses.ScheduleManager.Instance.GetLocalSchedule();
            laScheduleWindow.Text = string.Format("{0} - {1}", new object[] { this.LocalSchedule.FlightDateStart.ToString("MM/dd/yy"), this.LocalSchedule.FlightDateEnd.ToString("MM/dd/yy") });
            laScheduleName.Text = this.LocalSchedule.Name;
            laAdvertiser.Text = this.LocalSchedule.BusinessName + (!string.IsNullOrEmpty(this.LocalSchedule.AccountNumber) ? (" - " + this.LocalSchedule.AccountNumber) : string.Empty);
            if (!quickLoad)
            {
                this.AllowToSave = false;
                comboBoxEditSchedule.Properties.Items.Clear();
                comboBoxEditSchedule.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.OutputHeaders.ToArray());
                if (string.IsNullOrEmpty(this.LocalSchedule.ViewSettings.MultiGridViewSettings.SlideHeader))
                {
                    if (comboBoxEditSchedule.Properties.Items.Count > 0)
                        comboBoxEditSchedule.SelectedIndex = 0;
                }
                else
                {
                    int index = comboBoxEditSchedule.Properties.Items.IndexOf(this.LocalSchedule.ViewSettings.MultiGridViewSettings.SlideHeader);
                    if (index >= 0)
                        comboBoxEditSchedule.SelectedIndex = index;
                    else
                        comboBoxEditSchedule.SelectedIndex = 0;
                }

                laDate.Text = this.LocalSchedule.PresentationDateObject != null ? this.LocalSchedule.PresentationDate.ToString("MM/dd/yy") : string.Empty;
                laBusinessName.Text = " " + this.LocalSchedule.BusinessName + (!string.IsNullOrEmpty(this.LocalSchedule.AccountNumber) ? (" - " + this.LocalSchedule.AccountNumber) : string.Empty);
                laDecisionMaker.Text = this.LocalSchedule.DecisionMaker;
                laFlightDates.Text = " " + this.LocalSchedule.FlightDateStart.ToString("MM/dd/yy") + " - " + this.LocalSchedule.FlightDateEnd.ToString("MM/dd/yy");

                PrepareInserts();
                gridControlPublication.DataSource = _inserts;

                LoadView();

                SetColumnsState();

                UpdateSlideBullets();
                this.AllowToSave = true;
            }
            this.SettingsNotSaved = false;
        }

        public void ResetToDefault()
        {
            this.LocalSchedule.ViewSettings.MultiGridViewSettings.ResetToDefault();

            this.AllowToSave = false;
            LoadView();
            SetColumnsState();
            UpdateSlideBullets();
            this.AllowToSave = true;
        }

        public void OpenHelp()
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("logogrid");
        }
        #endregion

        #region View Methods
        private void LoadView()
        {
            this.ShowOptions = this.LocalSchedule.ViewSettings.MultiGridViewSettings.ShowOptions;
            this.SelectedOptionChapterIndex = this.LocalSchedule.ViewSettings.MultiGridViewSettings.SelectedOptionChapterIndex;

            this.PositionID = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IDPosition;
            this.PositionIndex = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IndexPosition;
            this.PositionDate = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DatePosition;
            this.PositionPCI = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PCIPosition;
            this.PositionCost = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.CostPosition;
            this.PositionFinalCost = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.FinalCostPosition;
            this.PositionDiscount = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DiscountPosition;
            this.PositionColor = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ColorPosition;
            this.PositionPublication = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PublicationPosition;
            this.PositionSquare = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SquarePosition;
            this.PositionPageSize = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PageSizePosition;
            this.PositionPercentOfPage = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PercentOfPagePosition;
            this.PositionDimensions = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DimensionsPosition;
            this.PositionMechanicals = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.MechanicalsPosition;
            this.PositionSection = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SectionPosition;
            this.PositionDelivery = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeliveryPosition;
            this.PositionReadership = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ReadershipPosition;
            this.PositionDeadline = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeadlinePosition;

            this.PositionColumnInchesInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionSquare;
            this.PositionCommentsInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionComments;
            this.PositionDeadlineInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionDeadline;
            this.PositionDeliveryInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionDelivery;
            this.PositionDimensionsInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionDimensions;
            this.PositionMechanicalsInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionMechanicals;
            this.PositionPageSizeInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionPageSize;
            this.PositionPercentOfPageInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionPercentOfPage;
            this.PositionPublicationInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionPublication;
            this.PositionReadershipInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionReadership;
            this.PositionSectionInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionSection;

            this.EnableColorHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableColor;
            this.EnableCostHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableCost;
            this.EnableDateHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableDate;
            this.EnableDeadlineHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableDeadline;
            this.EnableDeliveryHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableDelivery;
            this.EnableDimensionsHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableDimensions;
            this.EnableDiscountHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableDiscount;
            this.EnableFinalCostHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableFinalCost;
            this.EnableIDHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableID;
            this.EnableIndexHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableIndex;
            this.EnableMechanicalsHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableMechanicals;
            this.EnablePageSizeHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnablePageSize;
            this.EnablePercentOfPageHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.EnablePercentOfPage & BusinessClasses.ListManager.Instance.ShareUnits.Count > 0;
            this.EnablePCIHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnablePCI;
            this.EnablePublicationHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnablePublication;
            this.EnableReadershipHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableReadership;
            this.EnableSectionHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableSection;
            this.EnableSquareHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableSquare;
            this.EnableCommentsHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.EnableAdNotes;

            _showColorHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowColor;
            _showCostHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowCost;
            _showDateHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDate;
            _showDeadlineHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDeadline;
            _showDeliveryHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDelivery;
            _showDimensionsHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDimensions;
            _showDiscountHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDiscount;
            _showFinalCostHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowFinalCost;
            _showIDHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowID;
            _showIndexHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowIndex;
            _showMechanicalsHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowMechanicals;
            _showPageSizeHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowPageSize;
            _showPercentOfPageHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPercentOfPage & BusinessClasses.ListManager.Instance.ShareUnits.Count > 0;
            _showPCIHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowPCI;
            _showPublicationHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowPublication;
            _showReadershipHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowReadership;
            _showSectionHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowSection;
            _showSquareHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowSquare;
            _showCommentsHeader = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowAdNotes;

            this.EnableColumnInchesInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnableSquare;
            this.EnableCommentsInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnableComments;
            this.EnableDeadlineInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnableDeadline;
            this.EnableDeliveryInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnableDelivery;
            this.EnableDimensionsInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnableDimensions;
            this.EnableMechanicalsInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnableMechanicals;
            this.EnablePageSizeInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnablePageSize;
            this.EnablePercentOfPageInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnablePercentOfPage;
            this.EnablePublicationInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnablePublication;
            this.EnableReadershipInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnableReadership;
            this.EnableSectionInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.EnableSection;

            this.ShowColumnInchesInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowSquare;
            this.ShowCommentsInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowComments;
            this.ShowDeadlineInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowDeadline;
            this.ShowDeliveryInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowDelivery;
            this.ShowDimensionsInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowDimensions;
            this.ShowMechanicalsInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowMechanicals;
            this.ShowPageSizeInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowPageSize;
            this.ShowPercentOfPageInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowPercentOfPage;
            this.ShowPublicationInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowPublication;
            this.ShowReadershipInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowReadership;
            this.ShowSectionInPreview = this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowSection;

            this.WidthID = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IDWidth;
            this.WidthIndex = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IndexWidth;
            this.WidthDate = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DateWidth;
            this.WidthPCI = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PCIWidth;
            this.WidthCost = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.CostWidth;
            this.WidthFinalCost = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.FinalCostWidth;
            this.WidthDiscount = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DiscountWidth;
            this.WidthColor = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ColorWidth;
            this.WidthPublication = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PublicationWidth;
            this.WidthSquare = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SquareWidth;
            this.WidthPageSize = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PageSizeWidth;
            this.WidthPercentOfPage = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PercentOfPageWidth;
            this.WidthDimensions = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DimensionsWidth;
            this.WidthMechanicals = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.MechanicalsWidth;
            this.WidthSection = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SectionWidth;
            this.WidthDelivery = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeliveryWidth;
            this.WidthReadership = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ReadershipWidth;
            this.WidthDeadline = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeadlineWidth;

            this.CaptionID = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IDCaption;
            this.CaptionIndex = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IndexCaption;
            this.CaptionDate = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DateCaption;
            this.CaptionPCI = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PCICaption;
            this.CaptionCost = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.CostCaption;
            this.CaptionFinalCost = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.FinalCostCaption;
            this.CaptionDiscount = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DiscountCaption;
            this.CaptionColor = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ColorCaption;
            this.CaptionPublication = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PublicationCaption;
            this.CaptionSquare = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SquareCaption;
            this.CaptionPageSize = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PageSizeCaption;
            this.CaptionPercentOfPage = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PercentOfPageCaption;
            this.CaptionDimensions = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DimensionsCaption;
            this.CaptionMechanicals = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.MechanicalsCaption;
            this.CaptionSection = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SectionCaption;
            this.CaptionDelivery = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeliveryCaption;
            this.CaptionReadership = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ReadershipCaption;
            this.CaptionDeadline = this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeadlineCaption;

            this.PrintColumns.LoadColumnsState();
            this.AdNotes.LoadAdNotes();
            this.SlideBullets.LoadSlideBullets();
            this.SlideHeader.LoadSlideHeader();

            SetPreviewState();
            SaveView();
        }

        public void SaveView()
        {
            if (this.AllowToSave)
            {
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.ShowOptions = this.ShowOptions;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.SelectedOptionChapterIndex = this.SelectedOptionChapterIndex;

                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IDPosition = this.PositionID;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IndexPosition = this.PositionIndex;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DatePosition = this.PositionDate;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PCIPosition = this.PositionPCI;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.CostPosition = this.PositionCost;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.FinalCostPosition = this.PositionFinalCost;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DiscountPosition = this.PositionDiscount;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ColorPosition = this.PositionColor;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PublicationPosition = this.PositionPublication;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SquarePosition = this.PositionSquare;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PageSizePosition = this.PositionPageSize;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PercentOfPagePosition = this.PositionPercentOfPage;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DimensionsPosition = this.PositionDimensions;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.MechanicalsPosition = this.PositionMechanicals;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SectionPosition = this.PositionSection;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeliveryPosition = this.PositionDelivery;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ReadershipPosition = this.PositionReadership;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeadlinePosition = this.PositionDeadline;

                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionSquare = this.PositionColumnInchesInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionComments = this.PositionCommentsInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionDeadline = this.PositionDeadlineInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionDelivery = this.PositionDeliveryInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionDimensions = this.PositionDimensionsInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionMechanicals = this.PositionMechanicalsInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionPageSize = this.PositionPageSizeInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionPercentOfPage = this.PositionPercentOfPageInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionPublication = this.PositionPublicationInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionReadership = this.PositionReadershipInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.PositionSection = this.PositionSectionInPreview;

                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowColor = _showColorHeader;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowAdNotes = _showCommentsHeader;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowCost = _showCostHeader;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDate = _showDateHeader;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDeadline = _showDeadlineHeader;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDelivery = _showDeliveryHeader;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDimensions = _showDimensionsHeader;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowDiscount = _showDiscountHeader;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowFinalCost = _showFinalCostHeader;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowID = _showIDHeader;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowIndex = _showIndexHeader;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowMechanicals = _showMechanicalsHeader;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowPageSize = _showPageSizeHeader;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowPercentOfPage = _showPercentOfPageHeader;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowPCI = _showPCIHeader;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowPublication = _showPublicationHeader;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowReadership = _showReadershipHeader;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowSection = _showSectionHeader;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ShowSquare = _showSquareHeader;

                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowSquare = this.ShowColumnInchesInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowComments = this.ShowCommentsInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowDeadline = this.ShowDeadlineInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowDelivery = this.ShowDeliveryInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowDimensions = this.ShowDimensionsInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowMechanicals = this.ShowMechanicalsInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowPageSize = this.ShowPageSizeInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowPercentOfPage = this.ShowPercentOfPageInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowPublication = this.ShowPublicationInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowReadership = this.ShowReadershipInPreview;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.AdNotesState.ShowSection = this.ShowSectionInPreview;

                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IDWidth = this.WidthID;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IndexWidth = this.WidthIndex;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DateWidth = this.WidthDate;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PCIWidth = this.WidthPCI;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.CostWidth = this.WidthCost;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.FinalCostWidth = this.WidthFinalCost;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DiscountWidth = this.WidthDiscount;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ColorWidth = this.WidthColor;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PublicationWidth = this.WidthPublication;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SquareWidth = this.WidthSquare;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PageSizeWidth = this.WidthPageSize;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PercentOfPageWidth = this.WidthPercentOfPage;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DimensionsWidth = this.WidthDimensions;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.MechanicalsWidth = this.WidthMechanicals;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SectionWidth = this.WidthSection;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeliveryWidth = this.WidthDelivery;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ReadershipWidth = this.WidthReadership;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeadlineWidth = this.WidthDeadline;

                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IDCaption = this.CaptionID;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.IndexCaption = this.CaptionIndex;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DateCaption = this.CaptionDate;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PCICaption = this.CaptionPCI;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.CostCaption = this.CaptionCost;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.FinalCostCaption = this.CaptionFinalCost;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DiscountCaption = this.CaptionDiscount;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ColorCaption = this.CaptionColor;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PublicationCaption = this.CaptionPublication;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SquareCaption = this.CaptionSquare;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PageSizeCaption = this.CaptionPageSize;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.PercentOfPageCaption = this.CaptionPercentOfPage;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DimensionsCaption = this.CaptionDimensions;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.MechanicalsCaption = this.CaptionMechanicals;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.SectionCaption = this.CaptionSection;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeliveryCaption = this.CaptionDelivery;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.ReadershipCaption = this.CaptionReadership;
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.GridColumnsState.DeadlineCaption = this.CaptionDeadline;

                this.SettingsNotSaved = true;
            }
        }

        public void SetPreviewState()
        {
            gridViewPublications.OptionsView.ShowPreview = _showCommentsHeader;
            gridViewPublications.RefreshData();
        }

        public void SetSlideHeader()
        {
            comboBoxEditSchedule.Enabled = this.SlideHeaderState.ShowSlideHeader;
            laBusinessName.Enabled = this.SlideHeaderState.ShowAdvertiser;
            laDate.Enabled = this.SlideHeaderState.ShowPresentationDate;
            laDecisionMaker.Enabled = this.SlideHeaderState.ShowDecisionMaker;
            laFlightDates.Enabled = this.SlideHeaderState.ShowFlightDates;
        }

        public void UpdateColumnsAccordingToggles()
        {
            if (this.AllowToSave)
            {
                this.AdNotes.LoadAdNotes();
                SetColumnsState();
            }
        }

        public void SetToggleStateAfterAdNotesChange()
        {
            this.ShowSquareHeader &= !this.ShowColumnInchesInPreview;
            this.ShowDeadlineHeader &= !this.ShowDeadlineInPreview;
            this.ShowDimensionsHeader &= !this.ShowDimensionsInPreview;
            this.ShowMechanicalsHeader &= !this.ShowMechanicalsInPreview;
            this.ShowDeliveryHeader &= !this.ShowDeliveryInPreview;
            this.ShowSectionHeader &= !this.ShowSectionInPreview;
            this.ShowPageSizeHeader &= !this.ShowPageSizeInPreview;
            this.ShowPercentOfPageHeader &= !this.ShowPercentOfPageInPreview;
            this.ShowPublicationHeader &= !this.ShowPublicationInPreview;
            this.ShowReadershipHeader &= !this.ShowReadershipInPreview;

            SetColumnsState();
            this.PrintColumns.LoadColumnsState();
        }

        private void SetColumnsState()
        {
            gridViewPublications.ColumnPositionChanged -= new EventHandler(gridViewPublications_ColumnPositionChanged);
            gridColumnColorPricing.VisibleIndex = this.PositionColor != -1 ? this.PositionColor + 1 : -1;
            gridColumnColumnInches.VisibleIndex = this.PositionSquare != -1 ? this.PositionSquare + 1 : -1;
            gridColumnDate.VisibleIndex = this.PositionDate != -1 ? this.PositionDate + 1 : -1;
            gridColumnDeadline.VisibleIndex = this.PositionDeadline != -1 ? this.PositionDeadline + 1 : -1;
            gridColumnDelivery.VisibleIndex = this.PositionDelivery != -1 ? this.PositionDelivery + 1 : -1;
            gridColumnDiscountRate.VisibleIndex = this.PositionDiscount != -1 ? this.PositionDiscount + 1 : -1;
            gridColumnFinalRate.VisibleIndex = this.PositionFinalCost != -1 ? this.PositionFinalCost + 1 : -1;
            gridColumnID.VisibleIndex = this.PositionID != -1 ? this.PositionID + 1 : -1;
            gridColumnIndex.VisibleIndex = this.PositionIndex != -1 ? this.PositionIndex + 1 : -1;
            gridColumnMechanicals.VisibleIndex = this.PositionMechanicals != -1 ? this.PositionMechanicals + 1 : -1;
            gridColumnPageSize.VisibleIndex = this.PositionPageSize != -1 ? this.PositionPageSize + 1 : -1;
            gridColumnPercentOfPage.VisibleIndex = this.PositionPercentOfPage != -1 ? this.PositionPercentOfPage + 1 : -1;
            gridColumnPCIRate.VisibleIndex = this.PositionPCI != -1 ? this.PositionPCI + 1 : -1;
            gridColumnADRate.VisibleIndex = this.PositionCost != -1 ? this.PositionCost + 1 : -1;
            gridColumnPublication.VisibleIndex = this.PositionPublication != -1 ? this.PositionPublication + 1 : -1;
            gridColumnDimensions.VisibleIndex = this.PositionDimensions != -1 ? this.PositionDimensions + 1 : -1;
            gridColumnReadership.VisibleIndex = this.PositionReadership != -1 ? this.PositionReadership + 1 : -1;
            gridColumnSection.VisibleIndex = this.PositionSection != -1 ? this.PositionSection + 1 : -1;


            gridViewPublications.OptionsView.ShowPreview = _showCommentsHeader;
            gridColumnColorPricing.Visible = _showColorHeader;
            gridColumnColumnInches.Visible = _showSquareHeader;
            gridColumnDate.Visible = _showDateHeader;
            gridColumnDeadline.Visible = _showDeadlineHeader;
            gridColumnDelivery.Visible = _showDeliveryHeader;
            gridColumnDiscountRate.Visible = _showDiscountHeader;
            gridColumnFinalRate.Visible = _showFinalCostHeader;
            gridColumnID.Visible = _showIDHeader;
            gridColumnIndex.Visible = _showIndexHeader;
            gridColumnMechanicals.Visible = _showMechanicalsHeader;
            gridColumnPageSize.Visible = _showPageSizeHeader;
            gridColumnPercentOfPage.Visible = _showPercentOfPageHeader;
            gridColumnPCIRate.Visible = _showPCIHeader;
            gridColumnADRate.Visible = _showCostHeader;
            gridColumnPublication.Visible = _showPublicationHeader;
            gridColumnDimensions.Visible = _showDimensionsHeader;
            gridColumnReadership.Visible = _showReadershipHeader;
            gridColumnSection.Visible = _showSectionHeader;
            gridViewPublications.ColumnPositionChanged += new EventHandler(gridViewPublications_ColumnPositionChanged);

            gridViewPublications.ColumnWidthChanged -= new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(gridViewPublications_ColumnWidthChanged);
            gridColumnColorPricing.Width = this.WidthColor;
            gridColumnColumnInches.Width = this.WidthSquare;
            gridColumnDate.Width = this.WidthDate;
            gridColumnDeadline.Width = this.WidthDeadline;
            gridColumnDelivery.Width = this.WidthDelivery;
            gridColumnDiscountRate.Width = this.WidthDiscount;
            gridColumnFinalRate.Width = this.WidthFinalCost;
            gridColumnID.Width = this.WidthID;
            gridColumnIndex.Width = this.WidthIndex;
            gridColumnMechanicals.Width = this.WidthMechanicals;
            gridColumnPageSize.Width = this.WidthPageSize;
            gridColumnPercentOfPage.Width = this.WidthPercentOfPage;
            gridColumnPCIRate.Width = this.WidthPCI;
            gridColumnADRate.Width = this.WidthCost;
            gridColumnPublication.Width = this.WidthPublication;
            gridColumnDimensions.Width = this.WidthDimensions;
            gridColumnReadership.Width = this.WidthReadership;
            gridColumnSection.Width = this.WidthSection;
            gridViewPublications.ColumnWidthChanged += new DevExpress.XtraGrid.Views.Base.ColumnEventHandler(gridViewPublications_ColumnWidthChanged);

            gridColumnColorPricing.Caption = this.CaptionColor;
            gridColumnColumnInches.Caption = this.CaptionSquare;
            gridColumnDate.Caption = this.CaptionDate;
            gridColumnDeadline.Caption = this.CaptionDeadline;
            gridColumnDelivery.Caption = this.CaptionDelivery;
            gridColumnDiscountRate.Caption = this.CaptionDiscount;
            gridColumnFinalRate.Caption = this.CaptionFinalCost;
            gridColumnID.Caption = this.CaptionID;
            gridColumnIndex.Caption = this.CaptionIndex;
            gridColumnMechanicals.Caption = this.CaptionMechanicals;
            gridColumnPageSize.Caption = this.CaptionPageSize;
            gridColumnPercentOfPage.Caption = this.CaptionPercentOfPage;
            gridColumnPCIRate.Caption = this.CaptionPCI;
            gridColumnADRate.Caption = this.CaptionCost;
            gridColumnPublication.Caption = this.CaptionPublication;
            gridColumnDimensions.Caption = this.CaptionDimensions;
            gridColumnReadership.Caption = this.CaptionReadership;
            gridColumnSection.Caption = this.CaptionSection;

            SetPreviewState();
            gridViewPublications_ColumnPositionChanged(null, null);
        }
        #endregion

        #region Editor's Events
        private void comboBoxEditSchedule_EditValueChanged(object sender, EventArgs e)
        {
            if (this.AllowToSave)
            {
                this.LocalSchedule.ViewSettings.MultiGridViewSettings.SlideHeader = comboBoxEditSchedule.EditValue != null ? comboBoxEditSchedule.EditValue.ToString() : string.Empty;
                this.SettingsNotSaved = true;
            }
        }

        private void checkEdit_MouseDown(object sender, MouseEventArgs e)
        {
            DevExpress.XtraEditors.CheckEdit cEdit = (DevExpress.XtraEditors.CheckEdit)sender;
            DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo cInfo = (DevExpress.XtraEditors.ViewInfo.CheckEditViewInfo)cEdit.GetViewInfo();
            System.Drawing.Rectangle r = cInfo.CheckInfo.GlyphRect;
            System.Drawing.Rectangle editorRect = new System.Drawing.Rectangle(new System.Drawing.Point(0, 0), cEdit.Size);
            if (!r.Contains(e.Location) && editorRect.Contains(e.Location))
                ((DevExpress.Utils.DXMouseEventArgs)e).Handled = true;
        }

        private void textEditHeader_Leave(object sender, EventArgs e)
        {
            _activeCol.Caption = textEditHeader.Text;

            if (_activeCol == gridColumnADRate)
                this.CaptionCost = _activeCol.Caption;
            else if (_activeCol == gridColumnColorPricing)
                this.CaptionColor = _activeCol.Caption;
            else if (_activeCol == gridColumnColumnInches)
                this.CaptionSquare = _activeCol.Caption;
            else if (_activeCol == gridColumnDate)
                this.CaptionDate = _activeCol.Caption;
            else if (_activeCol == gridColumnDeadline)
                this.CaptionDeadline = _activeCol.Caption;
            else if (_activeCol == gridColumnDelivery)
                this.CaptionDelivery = _activeCol.Caption;
            else if (_activeCol == gridColumnDimensions)
                this.CaptionDimensions = _activeCol.Caption;
            else if (_activeCol == gridColumnDiscountRate)
                this.CaptionDiscount = _activeCol.Caption;
            else if (_activeCol == gridColumnFinalRate)
                this.CaptionFinalCost = _activeCol.Caption;
            else if (_activeCol == gridColumnID)
                this.CaptionID = _activeCol.Caption;
            else if (_activeCol == gridColumnIndex)
                this.CaptionIndex = _activeCol.Caption;
            else if (_activeCol == gridColumnMechanicals)
                this.CaptionMechanicals = _activeCol.Caption;
            else if (_activeCol == gridColumnPageSize)
                this.CaptionPageSize = _activeCol.Caption;
            else if (_activeCol == gridColumnPercentOfPage)
                this.CaptionPercentOfPage = _activeCol.Caption;
            else if (_activeCol == gridColumnPCIRate)
                this.CaptionPCI = _activeCol.Caption;
            else if (_activeCol == gridColumnPublication)
                this.CaptionPublication = _activeCol.Caption;
            else if (_activeCol == gridColumnReadership)
                this.CaptionReadership = _activeCol.Caption;
            else if (_activeCol == gridColumnSection)
                this.CaptionSection = _activeCol.Caption;

            textEditHeader.Hide();

            SaveView();
        }
        #endregion

        #region Grid Events
        private void gridViewPublications_ColumnPositionChanged(object sender, EventArgs e)
        {
            if (this.AllowToSave)
            {
                this.PositionCost = gridColumnADRate.VisibleIndex != -1 ? gridColumnADRate.VisibleIndex - 1 : -1;
                this.PositionColor = gridColumnColorPricing.VisibleIndex != -1 ? gridColumnColorPricing.VisibleIndex - 1 : -1;
                this.PositionSquare = gridColumnColumnInches.VisibleIndex != -1 ? gridColumnColumnInches.VisibleIndex - 1 : -1;
                this.PositionDate = gridColumnDate.VisibleIndex != -1 ? gridColumnDate.VisibleIndex - 1 : -1;
                this.PositionDeadline = gridColumnDeadline.VisibleIndex != -1 ? gridColumnDeadline.VisibleIndex - 1 : -1;
                this.PositionDelivery = gridColumnDelivery.VisibleIndex != -1 ? gridColumnDelivery.VisibleIndex - 1 : -1;
                this.PositionDimensions = gridColumnDimensions.VisibleIndex != -1 ? gridColumnDimensions.VisibleIndex - 1 : -1;
                this.PositionDiscount = gridColumnDiscountRate.VisibleIndex != -1 ? gridColumnDiscountRate.VisibleIndex - 1 : -1;
                this.PositionFinalCost = gridColumnFinalRate.VisibleIndex != -1 ? gridColumnFinalRate.VisibleIndex - 1 : -1;
                this.PositionID = gridColumnID.VisibleIndex != -1 ? gridColumnID.VisibleIndex - 1 : -1;
                this.PositionIndex = gridColumnIndex.VisibleIndex != -1 ? gridColumnIndex.VisibleIndex - 1 : -1;
                this.PositionMechanicals = gridColumnMechanicals.VisibleIndex != -1 ? gridColumnMechanicals.VisibleIndex - 1 : -1;
                this.PositionPageSize = gridColumnPageSize.VisibleIndex != -1 ? gridColumnPageSize.VisibleIndex - 1 : -1;
                this.PositionPercentOfPage = gridColumnPercentOfPage.VisibleIndex != -1 ? gridColumnPercentOfPage.VisibleIndex - 1 : -1;
                this.PositionPCI = gridColumnPCIRate.VisibleIndex != -1 ? gridColumnPCIRate.VisibleIndex - 1 : -1;
                this.PositionPublication = gridColumnPublication.VisibleIndex != -1 ? gridColumnPublication.VisibleIndex - 1 : -1;
                this.PositionReadership = gridColumnReadership.VisibleIndex != -1 ? gridColumnReadership.VisibleIndex - 1 : -1;
                this.PositionSection = gridColumnSection.VisibleIndex != -1 ? gridColumnSection.VisibleIndex - 1 : -1;
                SaveView();
            }
        }

        private void gridViewPublications_ColumnWidthChanged(object sender, DevExpress.XtraGrid.Views.Base.ColumnEventArgs e)
        {
            if (this.AllowToSave)
            {
                this.WidthCost = gridColumnADRate.Width;
                this.WidthColor = gridColumnColorPricing.Width;
                this.WidthSquare = gridColumnColumnInches.Width;
                this.WidthDate = gridColumnDate.Width;
                this.WidthDeadline = gridColumnDeadline.Width;
                this.WidthDelivery = gridColumnDelivery.Width;
                this.WidthDimensions = gridColumnDimensions.Width;
                this.WidthDiscount = gridColumnDiscountRate.Width;
                this.WidthFinalCost = gridColumnFinalRate.Width;
                this.WidthID = gridColumnID.Width;
                this.WidthIndex = gridColumnIndex.Width;
                this.WidthMechanicals = gridColumnMechanicals.Width;
                this.WidthPageSize = gridColumnPageSize.Width;
                this.WidthPercentOfPage = gridColumnPercentOfPage.Width;
                this.WidthPCI = gridColumnPCIRate.Width;
                this.WidthPublication = gridColumnPublication.Width;
                this.WidthReadership = gridColumnReadership.Width;
                this.WidthSection = gridColumnSection.Width;
                SaveView();
            }
        }

        private void gridViewPublication_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs args = (e as DevExpress.Utils.DXMouseEventArgs);
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = view.CalcHitInfo(args.Location);
            if (hi.InColumn)
            {
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo vi = view.GetViewInfo() as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo;
                System.Drawing.Rectangle bounds = vi.ColumnsInfo[hi.Column].Bounds;
                bounds.Width -= 10;
                bounds.Height -= 3;
                bounds.Y += 3;
                textEditHeader.SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                textEditHeader.EditValue = hi.Column.Caption;
                textEditHeader.Show();
                textEditHeader.Focus();
                _activeCol = hi.Column;
            }
            else
                AppManager.ShowWarning(" If you want to modify this Schedule Data,\ngo to the Schedules Tab and save changes...");
        }

        private void gridViewPublications_MouseUp(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs args = (e as DevExpress.Utils.DXMouseEventArgs);
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = view.CalcHitInfo(args.Location);
            if (hi.InColumn && e.Clicks == 2)
            {
                if (hi.Column != gridColumnDate && hi.Column != gridColumnPublication)
                {
                    DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo ViewInfo = view.GetViewInfo() as DevExpress.XtraGrid.Views.Grid.ViewInfo.GridViewInfo;
                    DevExpress.XtraGrid.Views.Grid.GridState prevState = view.State;
                    if ((e.Button & System.Windows.Forms.MouseButtons.Left) != 0)
                    {
                        if (ViewInfo.ColumnsInfo[hi.Column].CaptionRect.Contains(new System.Drawing.Point(e.X, e.Y)))
                        {
                            ViewInfo.SelectionInfo.ClearPressedInfo();
                            args.Handled = true;
                        }
                    }
                }
            }
        }

        private void gridViewPublications_CalcPreviewText(object sender, DevExpress.XtraGrid.Views.Grid.CalcPreviewTextEventArgs e)
        {
            SortedDictionary<int, string> previewText = new SortedDictionary<int, string>();
            int maxNumber = 12;
            if (this.ShowCommentsInPreview && !string.IsNullOrEmpty(e.PreviewText))
                previewText.Add(this.PositionCommentsInPreview > 0 && !previewText.Keys.Contains(this.PositionCommentsInPreview) ? this.PositionCommentsInPreview : ++maxNumber, e.PreviewText);
            if (this.ShowSectionInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnSection).ToString()))
                previewText.Add(this.PositionSectionInPreview > 0 && !previewText.Keys.Contains(this.PositionSectionInPreview) ? this.PositionSectionInPreview : ++maxNumber, "Section: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnSection).ToString());
            if (this.ShowMechanicalsInPreview && !string.IsNullOrEmpty((string)gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnMechanicals)))
                previewText.Add(this.PositionMechanicalsInPreview > 0 && !previewText.Keys.Contains(this.PositionMechanicalsInPreview) ? this.PositionMechanicalsInPreview : ++maxNumber, "Mech: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnMechanicals).ToString());
            if (this.ShowDeliveryInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnDelivery).ToString()))
                previewText.Add(this.PositionDeliveryInPreview > 0 && !previewText.Keys.Contains(this.PositionDeliveryInPreview) ? this.PositionDeliveryInPreview : ++maxNumber, "Delivery: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnDelivery).ToString());
            if (this.ShowPublicationInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnPublication).ToString()))
                previewText.Add(this.PositionPublicationInPreview > 0 && !previewText.Keys.Contains(this.PositionPublicationInPreview) ? this.PositionPublicationInPreview : ++maxNumber, "Publication: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnPublication).ToString());
            if (this.ShowPageSizeInPreview && !string.IsNullOrEmpty((string)gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnPageSize)))
                previewText.Add(this.PositionPageSizeInPreview > 0 && !previewText.Keys.Contains(this.PositionPageSizeInPreview) ? this.PositionPageSizeInPreview : ++maxNumber, "Page Size: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnPageSize).ToString());
            if (this.ShowPercentOfPageInPreview && !string.IsNullOrEmpty((string)gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnPercentOfPage)))
                previewText.Add(this.PositionPercentOfPageInPreview > 0 && !previewText.Keys.Contains(this.PositionPercentOfPageInPreview) ? this.PositionPercentOfPageInPreview : ++maxNumber, gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnPercentOfPage).ToString() + " Share of Page");
            if (this.ShowDimensionsInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnDimensions).ToString()))
                previewText.Add(this.PositionDimensionsInPreview > 0 && !previewText.Keys.Contains(this.PositionDimensionsInPreview) ? this.PositionDimensionsInPreview : ++maxNumber, "Col. x Inches: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnDimensions).ToString());
            if (this.ShowColumnInchesInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnColumnInches).ToString()))
                previewText.Add(this.PositionColumnInchesInPreview > 0 && !previewText.Keys.Contains(this.PositionColumnInchesInPreview) ? this.PositionColumnInchesInPreview : ++maxNumber, "Total Col In: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnColumnInches).ToString() + " col. in.");
            if (this.ShowReadershipInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnReadership).ToString()))
                previewText.Add(this.PositionReadershipInPreview > 0 && !previewText.Keys.Contains(this.PositionReadershipInPreview) ? this.PositionReadershipInPreview : ++maxNumber, "Readership: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnReadership).ToString());
            if (this.ShowDeadlineInPreview && !string.IsNullOrEmpty(gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnDeadline).ToString()))
                previewText.Add(this.PositionDeadlineInPreview > 0 && !previewText.Keys.Contains(this.PositionDeadlineInPreview) ? this.PositionDeadlineInPreview : ++maxNumber, "Deadline: " + gridViewPublications.GetRowCellValue(e.RowHandle, gridColumnDeadline).ToString());
            e.PreviewText = string.Join(",   ", previewText.Values.ToArray());
            if (string.IsNullOrEmpty(e.PreviewText))
                e.PreviewText = "            ";
        }

        private void gridViewPublications_MouseMove(object sender, MouseEventArgs e)
        {
            gridViewPublications.Focus();
        }
        #endregion

        #region Output Stuff
        public int OutputFileIndex
        {
            get
            {
                if (InteropClasses.PowerPointHelper.Instance.Is2003)
                    return _showCommentsHeader ? 1 : 2;
                else
                    return _showCommentsHeader ? 3 : 4;
            }
        }

        public string Header
        {
            get
            {
                string result = string.Empty;
                if (comboBoxEditSchedule.EditValue != null && this.SlideHeaderState.ShowSlideHeader)
                    result = comboBoxEditSchedule.EditValue.ToString();
                return result;
            }
        }


        public string PresentationDate
        {
            get
            {
                string result = string.Empty;
                if (this.SlideHeaderState.ShowPresentationDate)
                    result = laDate.Text;
                return result;
            }
        }


        public string BusinessName
        {
            get
            {
                string result = string.Empty;
                if (this.SlideHeaderState.ShowAdvertiser)
                    result = laBusinessName.Text.Trim();
                return result;
            }
        }

        public string DecisionMaker
        {
            get
            {
                string result = string.Empty;
                if (this.SlideHeaderState.ShowDecisionMaker)
                    result = laDecisionMaker.Text.Trim();
                return result;
            }
        }

        public string FlightDates
        {
            get
            {
                string result = string.Empty;
                if (this.SlideHeaderState.ShowFlightDates)
                    result = laFlightDates.Text.Trim();
                return result;
            }
        }

        public bool ShowSignatureLine
        {
            get
            {
                return this.SlideBulletsState.ShowSignature;
            }
        }

        public string[][] PublicationLogos { get; set; }

        private string[][] GetPublicationLogos(bool excelOutput)
        {
            List<string[]> logos = new List<string[]>();
            List<string> logosOnSlide = new List<string>();
            int rowCountPerSlide = _showCommentsHeader ? (excelOutput ? BusinessClasses.OutputManager.MultiGridExcelBasedRowsCountWithNotes : BusinessClasses.OutputManager.MultiGridGridBasedRowsCountWithNotes) : (excelOutput ? BusinessClasses.OutputManager.MultiGridExcelBasedRowsCountWithoutNotes : BusinessClasses.OutputManager.MultiGridGridBasedRowsCountWithoutNotes);
            int insertsCount = _inserts.Count;
            for (int i = 0; i < insertsCount; i += rowCountPerSlide)
            {
                logosOnSlide.Clear();
                for (int j = 0; j < rowCountPerSlide; j++)
                {
                    string fileName = string.Empty;
                    if ((i + j) < insertsCount)
                    {
                        int k = gridViewPublications.GetDataSourceRowIndex(i + j);
                        if (_inserts[k].MultiGridLogo != null)
                        {
                            fileName = System.IO.Path.GetTempFileName();
                            _inserts[k].MultiGridLogo.Save(fileName);
                        }
                    }
                    logosOnSlide.Add(fileName);
                }
                logos.Add(logosOnSlide.ToArray());
            }
            return logos.ToArray();
        }

        public bool ShowAdSpecsOnlyOnLastSlide
        {
            get
            {
                return this.SlideBulletsState.ShowOnlyOnLastSlide;
            }
        }

        public bool DoNotShowAdSpecs
        {
            get
            {
                return !this.SlideBulletsState.ShowSlideBullets;
            }
        }

        public string[] AdSpecs
        {
            get
            {
                List<string> values = new List<string>();
                if (!string.IsNullOrEmpty(this.SlideBullets.TotalInserts))
                    values.Add(this.SlideBullets.TotalInserts);
                if (!string.IsNullOrEmpty(this.SlideBullets.PageSize))
                    values.Add(this.SlideBullets.PageSize);
                if (!string.IsNullOrEmpty(this.SlideBullets.PercentOfPage))
                    values.Add(this.SlideBullets.PercentOfPage);
                if (!string.IsNullOrEmpty(this.SlideBullets.Dimensions))
                    values.Add(this.SlideBullets.Dimensions);
                if (!string.IsNullOrEmpty(this.SlideBullets.ColumnInches))
                    values.Add(this.SlideBullets.ColumnInches);
                if (!string.IsNullOrEmpty(this.SlideBullets.AvgAdCost))
                    values.Add(this.SlideBullets.AvgAdCost);
                if (!string.IsNullOrEmpty(this.SlideBullets.AvgFinalCost))
                    values.Add(this.SlideBullets.AvgFinalCost);
                if (!string.IsNullOrEmpty(this.SlideBullets.AvgPCI))
                    values.Add(this.SlideBullets.AvgPCI);
                if (!string.IsNullOrEmpty(this.SlideBullets.Delivery))
                    values.Add(this.SlideBullets.Delivery);
                if (!string.IsNullOrEmpty(this.SlideBullets.Readership))
                    values.Add(this.SlideBullets.Readership);
                if (!string.IsNullOrEmpty(this.SlideBullets.TotalColor))
                    values.Add(this.SlideBullets.TotalColor);
                if (!string.IsNullOrEmpty(this.SlideBullets.Discounts))
                    values.Add(this.SlideBullets.Discounts);
                if (!string.IsNullOrEmpty(this.SlideBullets.TotalFinalCost))
                    values.Add(this.SlideBullets.TotalFinalCost);
                if (!string.IsNullOrEmpty(this.SlideBullets.TotalSquare))
                    values.Add(this.SlideBullets.TotalSquare);
                return values.ToArray();
            }
        }

        public string[] GridHeaders
        {
            get
            {
                SortedDictionary<int, string> headers = new SortedDictionary<int, string>();
                if (this.PositionID != -1)
                    headers.Add(this.PositionID, this.CaptionID);
                if (this.PositionDate != -1)
                    headers.Add(this.PositionDate, this.CaptionDate);
                if (this.PositionPCI != -1)
                    headers.Add(this.PositionPCI, this.CaptionPCI);
                if (this.PositionCost != -1)
                    headers.Add(this.PositionCost, this.CaptionCost.Replace("&&", "&"));
                if (this.PositionDiscount != -1)
                    headers.Add(this.PositionDiscount, this.CaptionDiscount);
                if (this.PositionColor != -1)
                    headers.Add(this.PositionColor, this.CaptionColor);
                if (this.PositionFinalCost != -1)
                    headers.Add(this.PositionFinalCost, this.CaptionFinalCost);
                if (this.PositionIndex != -1)
                    headers.Add(this.PositionIndex, this.CaptionIndex);
                if (this.PositionSquare != -1)
                    headers.Add(this.PositionSquare, this.CaptionSquare);
                if (this.PositionPageSize != -1)
                    headers.Add(this.PositionPageSize, this.CaptionPageSize);
                if (this.PositionPercentOfPage != -1)
                    headers.Add(this.PositionPercentOfPage, this.CaptionPercentOfPage);
                if (this.PositionMechanicals != -1)
                    headers.Add(this.PositionMechanicals, this.CaptionMechanicals);
                if (this.PositionPublication != -1)
                    headers.Add(this.PositionPublication, this.CaptionPublication);
                if (this.PositionDimensions != -1)
                    headers.Add(this.PositionDimensions, this.CaptionDimensions);
                if (this.PositionSection != -1)
                    headers.Add(this.PositionSection, this.CaptionSection);
                if (this.PositionReadership != -1)
                    headers.Add(this.PositionReadership, this.CaptionReadership);
                if (this.PositionDelivery != -1)
                    headers.Add(this.PositionDelivery, this.CaptionDelivery);
                if (this.PositionDeadline != -1)
                    headers.Add(this.PositionDeadline, this.CaptionDeadline);
                return headers.Values.ToArray();
            }
        }

        public int[] GridHeaderSizes
        {
            get
            {
                SortedDictionary<int, int> sizes = new SortedDictionary<int, int>();
                if (this.PositionID != -1)
                    sizes.Add(this.PositionID, this.WidthID);
                if (this.PositionDate != -1)
                    sizes.Add(this.PositionDate, this.WidthDate);
                if (this.PositionPCI != -1)
                    sizes.Add(this.PositionPCI, this.WidthPCI);
                if (this.PositionCost != -1)
                    sizes.Add(this.PositionCost, this.WidthCost);
                if (this.PositionDiscount != -1)
                    sizes.Add(this.PositionDiscount, this.WidthDiscount);
                if (this.PositionColor != -1)
                    sizes.Add(this.PositionColor, this.WidthColor);
                if (this.PositionFinalCost != -1)
                    sizes.Add(this.PositionFinalCost, this.WidthFinalCost);
                if (this.PositionIndex != -1)
                    sizes.Add(this.PositionIndex, this.WidthIndex);
                if (this.PositionSquare != -1)
                    sizes.Add(this.PositionSquare, this.WidthSquare);
                if (this.PositionPageSize != -1)
                    sizes.Add(this.PositionPageSize, this.WidthPageSize);
                if (this.PositionPercentOfPage != -1)
                    sizes.Add(this.PositionPercentOfPage, this.WidthPercentOfPage);
                if (this.PositionMechanicals != -1)
                    sizes.Add(this.PositionMechanicals, this.WidthMechanicals);
                if (this.PositionPublication != -1)
                    sizes.Add(this.PositionPublication, this.WidthPublication);
                if (this.PositionDimensions != -1)
                    sizes.Add(this.PositionDimensions, this.WidthDimensions);
                if (this.PositionSection != -1)
                    sizes.Add(this.PositionSection, this.WidthSection);
                if (this.PositionReadership != -1)
                    sizes.Add(this.PositionReadership, this.WidthReadership);
                if (this.PositionDelivery != -1)
                    sizes.Add(this.PositionDelivery, this.WidthDelivery);
                if (this.PositionDeadline != -1)
                    sizes.Add(this.PositionDeadline, this.WidthDeadline);
                return sizes.Values.ToArray();
            }
        }

        public string[][][] Grid { get; private set; }

        private string[][][] GetGrid(bool excelOutput)
        {
            List<string[][]> result = new List<string[][]>();
            List<string[]> slides = new List<string[]>();
            SortedDictionary<int, string> row = new SortedDictionary<int, string>();
            SortedDictionary<int, string> adNotes = new SortedDictionary<int, string>();

            int rowCountPerSlide = _showCommentsHeader ? (excelOutput ? BusinessClasses.OutputManager.MultiGridExcelBasedRowsCountWithNotes : BusinessClasses.OutputManager.MultiGridGridBasedRowsCountWithNotes) : (excelOutput ? BusinessClasses.OutputManager.MultiGridExcelBasedRowsCountWithoutNotes : BusinessClasses.OutputManager.MultiGridGridBasedRowsCountWithoutNotes);
            int insertsCount = _inserts.Count;
            for (int i = 0; i < insertsCount; i += rowCountPerSlide)
            {
                slides.Clear();
                for (int j = 0; j < rowCountPerSlide; j++)
                {
                    row.Clear();
                    if ((i + j) < insertsCount)
                    {
                        int k = gridViewPublications.GetDataSourceRowIndex(i + j);
                        if (_showCommentsHeader)
                        {
                            adNotes.Clear();
                            int maxNumber = 12;
                            if (this.ShowCommentsInPreview && !string.IsNullOrEmpty(_inserts[k].FullComment))
                                adNotes.Add(this.PositionCommentsInPreview > 0 && !adNotes.Keys.Contains(this.PositionCommentsInPreview) ? this.PositionCommentsInPreview : ++maxNumber, _inserts[k].FullComment);
                            if (this.ShowSectionInPreview && !string.IsNullOrEmpty(_inserts[k].FullSection))
                                adNotes.Add(this.PositionSectionInPreview > 0 && !adNotes.Keys.Contains(this.PositionSectionInPreview) ? this.PositionSectionInPreview : ++maxNumber, "Section: " + _inserts[k].FullSection);
                            if (this.ShowMechanicalsInPreview && !string.IsNullOrEmpty(_inserts[k].Mechanicals))
                                adNotes.Add(this.PositionMechanicalsInPreview > 0 && !adNotes.Keys.Contains(this.PositionMechanicalsInPreview) ? this.PositionMechanicalsInPreview : ++maxNumber, "Mech: " + _inserts[k].MechanicalsOutput);
                            if (this.ShowDeliveryInPreview && !string.IsNullOrEmpty(_inserts[k].Delivery))
                                adNotes.Add(this.PositionDeliveryInPreview > 0 && !adNotes.Keys.Contains(this.PositionDeliveryInPreview) ? this.PositionDeliveryInPreview : ++maxNumber, "Delivery: " + _inserts[k].Delivery);
                            if (this.ShowPublicationInPreview && !string.IsNullOrEmpty(_inserts[k].Publication))
                                adNotes.Add(this.PositionPublicationInPreview > 0 && !adNotes.Keys.Contains(this.PositionPublicationInPreview) ? this.PositionPublicationInPreview : ++maxNumber, "Publication: " + _inserts[k].Publication);
                            if (this.ShowPageSizeInPreview && !string.IsNullOrEmpty(_inserts[k].PageSize))
                                adNotes.Add(this.PositionPageSizeInPreview > 0 && !adNotes.Keys.Contains(this.PositionPageSizeInPreview) ? this.PositionPageSizeInPreview : ++maxNumber, "Page Size: " + _inserts[k].PageSizeOutput);
                            if (this.ShowPercentOfPageInPreview && !string.IsNullOrEmpty(_inserts[k].PercentOfPage))
                                adNotes.Add(this.PositionPercentOfPageInPreview > 0 && !adNotes.Keys.Contains(this.PositionPercentOfPageInPreview) ? this.PositionPercentOfPageInPreview : ++maxNumber, "% of Page: " + _inserts[k].PercentOfPageOutput);
                            if (this.ShowDimensionsInPreview && !string.IsNullOrEmpty(_inserts[k].Dimensions))
                                adNotes.Add(this.PositionDimensionsInPreview > 0 && !adNotes.Keys.Contains(this.PositionDimensionsInPreview) ? this.PositionDimensionsInPreview : ++maxNumber, "Col. x Inches: " + _inserts[k].Dimensions);
                            if (this.ShowColumnInchesInPreview && !string.IsNullOrEmpty(_inserts[k].SquareStringFormatted))
                                adNotes.Add(this.PositionColumnInchesInPreview > 0 && !adNotes.Keys.Contains(this.PositionColumnInchesInPreview) ? this.PositionColumnInchesInPreview : ++maxNumber, "Total Col In: " + _inserts[k].SquareStringFormatted + " col. in.");
                            if (this.ShowReadershipInPreview && !string.IsNullOrEmpty(_inserts[k].Readership))
                                adNotes.Add(this.PositionReadershipInPreview > 0 && !adNotes.Keys.Contains(this.PositionReadershipInPreview) ? this.PositionReadershipInPreview : ++maxNumber, "Readership: " + _inserts[k].Readership);
                            if (this.ShowDeadlineInPreview && !string.IsNullOrEmpty(_inserts[k].Deadline))
                                adNotes.Add(this.PositionDeadlineInPreview > 0 && !adNotes.Keys.Contains(this.PositionDeadlineInPreview) ? this.PositionDeadlineInPreview : ++maxNumber, "Deadline: " + _inserts[k].DeadlineForOutput);
                            if (adNotes.Count > 0)
                                row.Add(-1, string.Join(",   ", adNotes.Values.ToArray()));
                            else
                                row.Add(-1, "            ");
                        }
                        if (this.PositionID != -1)
                            row.Add(this.PositionID, _inserts[k].ID);
                        if (this.PositionDate != -1)
                            row.Add(this.PositionDate, _inserts[k].Date.ToString("MM/dd/yy"));
                        if (this.PositionPCI != -1)
                            row.Add(this.PositionPCI, _inserts[k].PCIRate.HasValue ? (_inserts[k].PCIRate.Value.ToString("$#,###.00")) : "N/A");
                        if (this.PositionCost != -1)
                            row.Add(this.PositionCost, _inserts[k].ADRate.ToString("$#,###.00"));
                        if (this.PositionDiscount != -1)
                            row.Add(this.PositionDiscount, _inserts[k].DiscountRate.ToString("$#,###.00"));
                        if (this.PositionColor != -1)
                            row.Add(this.PositionColor, _inserts[k].ColorPricingCalculated > 0 ? _inserts[k].ColorPricingCalculated.ToString("$#,###.00") : _inserts[k].ColorPricingObject.ToString());
                        if (this.PositionFinalCost != -1)
                            row.Add(this.PositionFinalCost, _inserts[k].FinalRate.ToString("$#,###.00"));
                        if (this.PositionIndex != -1)
                            row.Add(this.PositionIndex, _inserts[k].Index.ToString("#,##0"));
                        if (this.PositionSquare != -1)
                            row.Add(this.PositionSquare, "'" + _inserts[k].SquareStringFormatted);
                        if (this.PositionPageSize != -1)
                            row.Add(this.PositionPageSize, _inserts[k].PageSizeOutput);
                        if (this.PositionPercentOfPage != -1)
                            row.Add(this.PositionPercentOfPage, _inserts[k].PercentOfPageOutput);
                        if (this.PositionMechanicals != -1)
                            row.Add(this.PositionMechanicals, _inserts[k].MechanicalsOutput);
                        if (this.PositionPublication != -1)
                            row.Add(this.PositionPublication, _inserts[k].Publication);
                        if (this.PositionDimensions != -1)
                            row.Add(this.PositionDimensions, _inserts[k].DimensionsOutput);
                        if (this.PositionSection != -1)
                            row.Add(this.PositionSection, _inserts[k].FullSection);
                        if (this.PositionReadership != -1)
                            row.Add(this.PositionReadership, _inserts[k].Readership);
                        if (this.PositionDelivery != -1)
                            row.Add(this.PositionDelivery, _inserts[k].Delivery);
                        if (this.PositionDeadline != -1)
                            row.Add(this.PositionDeadline, _inserts[k].DeadlineForOutput);
                    }
                    if (row.Values.Count > 0)
                        slides.Add(row.Values.ToArray());
                }
                result.Add(slides.ToArray());
            }
            return result.ToArray();
        }

        public List<Dictionary<string, string>> OutputReplacementsLists { get; set; }

        public void PopulateWeeklyScheduleReplacementsList()
        {
            string key = string.Empty;
            string value = string.Empty;
            List<string> temp = new List<string>();
            this.OutputReplacementsLists = new List<Dictionary<string, string>>();
            foreach (string[][] slideGrid in this.Grid)
            {
                Dictionary<string, string> slideReplacementList = new Dictionary<string, string>();

                string[] gridHeaders = this.GridHeaders;
                for (int i = 0; i < gridHeaders.Length; i++)
                {
                    key = string.Format("Header{0}", (i + 1).ToString());
                    value = gridHeaders[i];
                    if (!slideReplacementList.Keys.Contains(key))
                        slideReplacementList.Add(key, value);
                }

                for (int i = 0; i < slideGrid.Length; i++)
                {
                    for (int j = 0; j < slideGrid[i].Length; j++)
                    {
                        string columnPrefix = "a";
                        if (this.ShowCommentsHeader)
                        {
                            switch (j)
                            {
                                case 0:
                                    columnPrefix = "g";
                                    break;
                                case 1:
                                    columnPrefix = "a";
                                    break;
                                case 2:
                                    columnPrefix = "b";
                                    break;
                                case 3:
                                    columnPrefix = "c";
                                    break;
                                case 4:
                                    columnPrefix = "d";
                                    break;
                                case 5:
                                    columnPrefix = "e";
                                    break;
                                case 6:
                                    columnPrefix = "f";
                                    break;
                            }
                        }
                        else
                        {
                            switch (j)
                            {
                                case 0:
                                    columnPrefix = "a";
                                    break;
                                case 1:
                                    columnPrefix = "b";
                                    break;
                                case 2:
                                    columnPrefix = "c";
                                    break;
                                case 3:
                                    columnPrefix = "d";
                                    break;
                                case 4:
                                    columnPrefix = "e";
                                    break;
                                case 5:
                                    columnPrefix = "f";
                                    break;
                            }
                        }
                        key = string.Format("{0}{1}", new object[] { (i + 1).ToString(), columnPrefix });
                        value = slideGrid[i][j];
                        if (!slideReplacementList.Keys.Contains(key))
                            slideReplacementList.Add(key, value);
                    }
                }
                this.OutputReplacementsLists.Add(slideReplacementList);
            }
        }

        public void PrepareOutput(bool excelOutput)
        {
            this.Grid = GetGrid(excelOutput);
            this.PublicationLogos = GetPublicationLogos(excelOutput);
            if (!excelOutput)
                PopulateWeeklyScheduleReplacementsList();
        }

        public void PrintOutput()
        {
            using (OutputForms.FormSelectOutput formGridType = new OutputForms.FormSelectOutput(OutputForms.OutputType.PowerPoint))
            {
                formGridType.buttonXGrid.Enabled = this.SelectedColumnsCount >= 4 && this.SelectedColumnsCount <= 5 && Directory.Exists(BusinessClasses.OutputManager.Instance.MultiGridGridBasedTemlatesFolderPath);
                DialogResult gridTypeResult = formGridType.ShowDialog();
                if (gridTypeResult != DialogResult.Cancel)
                {
                    bool pasteAsImage = gridTypeResult == DialogResult.Ignore;
                    bool excelOutput = gridTypeResult == DialogResult.No || gridTypeResult == DialogResult.Ignore;
                    using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                    {
                        formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
                        formProgress.TopMost = true;
                        formProgress.Show();
                        PrepareOutput(excelOutput);
                        if (excelOutput)
                            InteropClasses.PowerPointHelper.Instance.AppendMultiGridExcelBased(pasteAsImage);
                        else
                            InteropClasses.PowerPointHelper.Instance.AppendMultiGridGridBased();
                        formProgress.Close();
                    }
                    using (OutputForms.FormSlideOutput formOutput = new OutputForms.FormSlideOutput())
                    {
                        if (formOutput.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                            AppManager.ActivateForm(FormMain.Instance.Handle, FormMain.Instance.IsMaximized, false);
                        else
                        {
                            AppManager.ActivatePowerPoint();
                            AppManager.ActivateMiniBar();
                        }
                    }
                }
            }
        }

        public void Email()
        {
            using (OutputForms.FormSelectOutput formGridType = new OutputForms.FormSelectOutput(OutputForms.OutputType.Email))
            {
                formGridType.buttonXGrid.Enabled = this.SelectedColumnsCount >= 4 && this.SelectedColumnsCount <= 5 && Directory.Exists(BusinessClasses.OutputManager.Instance.MultiGridGridBasedTemlatesFolderPath);
                DialogResult gridTypeResult = formGridType.ShowDialog();
                if (gridTypeResult != DialogResult.Cancel)
                {
                    bool pasteAsImage = gridTypeResult == DialogResult.Ignore;
                    bool excelOutput = gridTypeResult == DialogResult.No || gridTypeResult == DialogResult.Ignore;
                    using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                    {
                        formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
                        formProgress.TopMost = true;
                        formProgress.Show();
                        PrepareOutput(excelOutput);
                        string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
                        if (excelOutput)
                            InteropClasses.PowerPointHelper.Instance.PrepareMultiGridExcelBasedEmail(tempFileName, pasteAsImage);
                        else
                            InteropClasses.PowerPointHelper.Instance.PrepareMultiGridGridBasedEmail(tempFileName);
                        formProgress.Close();
                        if (File.Exists(tempFileName))
                            using (OutputForms.FormEmail formEmail = new OutputForms.FormEmail())
                            {
                                formEmail.Text = "Email this Logo Grid";
                                formEmail.PresentationFile = tempFileName;
                                ConfigurationClasses.RegistryHelper.MainFormHandle = formEmail.Handle;
                                ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                                formEmail.ShowDialog();
                                ConfigurationClasses.RegistryHelper.MaximizeMainForm = FormMain.Instance.IsMaximized;
                                ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                            }
                    }
                }
            }
        }

        public void Preview()
        {
            using (OutputForms.FormSelectOutput formGridType = new OutputForms.FormSelectOutput(OutputForms.OutputType.Preview))
            {
                formGridType.buttonXGrid.Enabled = this.SelectedColumnsCount >= 4 && this.SelectedColumnsCount <= 5 && Directory.Exists(BusinessClasses.OutputManager.Instance.MultiGridGridBasedTemlatesFolderPath);
                DialogResult gridTypeResult = formGridType.ShowDialog();
                if (gridTypeResult != DialogResult.Cancel)
                {
                    bool pasteAsImage = gridTypeResult == DialogResult.Ignore;
                    bool excelOutput = gridTypeResult == DialogResult.No || gridTypeResult == DialogResult.Ignore;
                    using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                    {
                        formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
                        formProgress.TopMost = true;
                        formProgress.Show();
                        PrepareOutput(excelOutput);
                        string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
                        if (excelOutput)
                            InteropClasses.PowerPointHelper.Instance.PrepareMultiGridExcelBasedEmail(tempFileName, pasteAsImage);
                        else
                            InteropClasses.PowerPointHelper.Instance.PrepareMultiGridGridBasedEmail(tempFileName);
                        formProgress.Close();
                        if (File.Exists(tempFileName))
                            using (OutputForms.FormPreview formPreview = new OutputForms.FormPreview())
                            {
                                formPreview.Text = "Preview Logo Grid";
                                formPreview.PresentationFile = tempFileName;
                                ConfigurationClasses.RegistryHelper.MainFormHandle = formPreview.Handle;
                                ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                                DialogResult previewResult = formPreview.ShowDialog();
                                ConfigurationClasses.RegistryHelper.MaximizeMainForm = FormMain.Instance.IsMaximized;
                                ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                                if (previewResult != System.Windows.Forms.DialogResult.OK)
                                    AppManager.ActivateForm(FormMain.Instance.Handle, true, false);
                                else
                                {
                                    AppManager.ActivatePowerPoint();
                                    AppManager.ActivateMiniBar();
                                }
                            }
                    }
                }
            }
        }
        #endregion
    }
}
