using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class OutputChronologicalControl : UserControl, IGridOutputControl
    {
        private static OutputChronologicalControl _instance;
        private DevExpress.XtraGrid.Columns.GridColumn _activeCol = null;

        public BusinessClasses.Schedule LocalSchedule { get; set; }

        private List<BusinessClasses.Insert> _inserts = new List<BusinessClasses.Insert>();

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
                return this.LocalSchedule.ViewSettings.ChronoGridViewSettings.SlideBulletsState;
            }
        }

        public ConfigurationClasses.SlideHeaderState SlideHeaderState
        {
            get
            {
                return this.LocalSchedule.ViewSettings.ChronoGridViewSettings.SlideHeaderState;
            }
        }

        public bool ShowGridDetails { get; set; }

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
        public bool EnableIDButton { get; set; }
        public bool EnableDateButton { get; set; }
        public bool EnablePublicationButton { get; set; }
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

        private OutputChronologicalControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.AllowToSave = true;

            textEditHeader.Hide();
            textEditHeader.Parent = this.gridControlPublication;


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


            #region Set Default Values
            this.EnableIDButton = false;
            this.EnableDateButton = false;
            this.EnablePublicationButton = false;

            this.PositionCommentsInPreview = 1;
            this.PositionSectionInPreview = 2;
            this.PositionMechanicalsInPreview = 3;
            this.PositionColumnInchesInPreview = 4;
            this.PositionPublicationInPreview = 0;
            this.PositionPageSizeInPreview = 6;
            this.PositionPercentOfPageInPreview = 7;
            this.PositionDimensionsInPreview = 8;
            this.PositionReadershipInPreview = 9;
            this.PositionDeliveryInPreview = 10;
            this.PositionDeadlineInPreview = 11;
            #endregion

            this.HelpToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me understand how to use the Chronological Grid", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);

            BusinessClasses.ScheduleManager.Instance.SettingsSaved += new EventHandler<BusinessClasses.SavingingEventArgs>((sender, e) =>
            {
                if (sender != this)
                    UpdateOutput(e.QuickSave);
            });
        }

        public static OutputChronologicalControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new OutputChronologicalControl();
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
            this.SlideBullets.TotalSquare = this.LocalSchedule.Publications.Sum(x => x.TotalSquare.HasValue ? x.TotalSquare.Value : 0).ToString("#,###.00");
        }

        private void PrepareInserts()
        {
            _inserts.Clear();
            foreach (BusinessClasses.Publication publication in this.LocalSchedule.Publications)
                _inserts.AddRange(publication.Inserts.Where(x => x.Date != DateTime.MinValue));
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
                laDate.Text = this.LocalSchedule.PresentationDateObject != null ? this.LocalSchedule.PresentationDate.ToString("MM/dd/yy") : string.Empty;
                laBusinessName.Text = " " + this.LocalSchedule.BusinessName + (!string.IsNullOrEmpty(this.LocalSchedule.AccountNumber) ? (" - " + this.LocalSchedule.AccountNumber) : string.Empty);
                laDecisionMaker.Text = this.LocalSchedule.DecisionMaker;
                laFlightDates.Text = " " + this.LocalSchedule.FlightDateStart.ToString("MM/dd/yy") + " - " + this.LocalSchedule.FlightDateEnd.ToString("MM/dd/yy");

                this.AllowToSave = false;
                comboBoxEditSchedule.Properties.Items.Clear();
                comboBoxEditSchedule.Properties.Items.AddRange(BusinessClasses.ListManager.Instance.OutputHeaders.ToArray());
                if (string.IsNullOrEmpty(this.LocalSchedule.ViewSettings.ChronoGridViewSettings.SlideHeader))
                {
                    if (comboBoxEditSchedule.Properties.Items.Count > 0)
                        comboBoxEditSchedule.SelectedIndex = 0;
                }
                else
                {
                    int index = comboBoxEditSchedule.Properties.Items.IndexOf(this.LocalSchedule.ViewSettings.ChronoGridViewSettings.SlideHeader);
                    if (index >= 0)
                        comboBoxEditSchedule.SelectedIndex = index;
                    else
                        comboBoxEditSchedule.SelectedIndex = 0;
                }
                PrepareInserts();
                gridControlPublication.DataSource = _inserts;

                LoadView();

                SetColumnsState();
                UpdateSlideBullets();

                this.AllowToSave = true;
            }
            this.SettingsNotSaved = false;
        }

        public void OpenHelp()
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("chronogrid");
        }
        #endregion

        #region View Methods
        private void LoadView()
        {
            this.ShowGridDetails = this.LocalSchedule.ViewSettings.ShowGridDetails;

            this.PositionID = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.IDPosition;
            this.PositionIndex = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.IndexPosition;
            this.PositionDate = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DatePosition;
            this.PositionPCI = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PCIPosition;
            this.PositionCost = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.CostPosition;
            this.PositionFinalCost = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.FinalCostPosition;
            this.PositionDiscount = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DiscountPosition;
            this.PositionColor = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ColorPosition;
            this.PositionPublication = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PublicationPosition;
            this.PositionSquare = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.SquarePosition;
            this.PositionPageSize = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PageSizePosition;
            this.PositionPercentOfPage = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PercentOfPagePosition;
            this.PositionDimensions = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DimensionsPosition;
            this.PositionMechanicals = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.MechanicalsPosition;
            this.PositionSection = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.SectionPosition;
            this.PositionDelivery = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DeliveryPosition;
            this.PositionReadership = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ReadershipPosition;
            this.PositionDeadline = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DeadlinePosition;

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

            _showColorHeader = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowColor;
            _showCommentsHeader = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowAdNotes;
            _showCostHeader = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowCost;
            _showDateHeader = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowDate;
            _showDeadlineHeader = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowDeadline;
            _showDeliveryHeader = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowDelivery;
            _showDimensionsHeader = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowDimensions;
            _showDiscountHeader = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowDiscount;
            _showFinalCostHeader = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowFinalCost;
            _showIDHeader = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowID;
            _showIndexHeader = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowIndex;
            _showMechanicalsHeader = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowMechanicals;
            _showPageSizeHeader = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowPageSize;
            _showPercentOfPageHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPercentOfPage & BusinessClasses.ListManager.Instance.ShareUnits.Count > 0;
            _showPCIHeader = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowPCI;
            _showPublicationHeader = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowPublication;
            _showReadershipHeader = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowReadership;
            _showSectionHeader = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowSection;
            _showSquareHeader = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowSquare;

            this.ShowColumnInchesInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowSquare;
            this.ShowCommentsInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowComments;
            this.ShowDeadlineInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowDeadline;
            this.ShowDeliveryInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowDelivery;
            this.ShowDimensionsInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowDimensions;
            this.ShowMechanicalsInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowMechanicals;
            this.ShowPageSizeInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowPageSize;
            this.ShowPercentOfPageInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowPercentOfPage;
            this.ShowPublicationInPreview = false;
            this.ShowReadershipInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowReadership;
            this.ShowSectionInPreview = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowSection;

            this.WidthID = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.IDWidth;
            this.WidthIndex = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.IndexWidth;
            this.WidthDate = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DateWidth;
            this.WidthPCI = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PCIWidth;
            this.WidthCost = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.CostWidth;
            this.WidthFinalCost = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.FinalCostWidth;
            this.WidthDiscount = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DiscountWidth;
            this.WidthColor = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ColorWidth;
            this.WidthPublication = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PublicationWidth;
            this.WidthSquare = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.SquareWidth;
            this.WidthPageSize = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PageSizeWidth;
            this.WidthPercentOfPage = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PercentOfPageWidth;
            this.WidthDimensions = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DimensionsWidth;
            this.WidthMechanicals = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.MechanicalsWidth;
            this.WidthSection = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.SectionWidth;
            this.WidthDelivery = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DeliveryWidth;
            this.WidthReadership = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ReadershipWidth;
            this.WidthDeadline = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DeadlineWidth;

            this.CaptionID = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.IDCaption;
            this.CaptionIndex = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.IndexCaption;
            this.CaptionDate = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DateCaption;
            this.CaptionPCI = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PCICaption;
            this.CaptionCost = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.CostCaption;
            this.CaptionFinalCost = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.FinalCostCaption;
            this.CaptionDiscount = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DiscountCaption;
            this.CaptionColor = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ColorCaption;
            this.CaptionPublication = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PublicationCaption;
            this.CaptionSquare = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.SquareCaption;
            this.CaptionPageSize = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PageSizeCaption;
            this.CaptionPercentOfPage = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PercentOfPageCaption;
            this.CaptionDimensions = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DimensionsCaption;
            this.CaptionMechanicals = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.MechanicalsCaption;
            this.CaptionSection = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.SectionCaption;
            this.CaptionDelivery = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DeliveryCaption;
            this.CaptionReadership = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ReadershipCaption;
            this.CaptionDeadline = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DeadlineCaption;

            pictureEditLogo1.Image = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.Logo1 != null ? new Bitmap(this.LocalSchedule.ViewSettings.ChronoGridViewSettings.Logo1) : null;
            pictureEditLogo2.Image = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.Logo2 != null ? new Bitmap(this.LocalSchedule.ViewSettings.ChronoGridViewSettings.Logo2) : null;
            pictureEditLogo3.Image = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.Logo3 != null ? new Bitmap(this.LocalSchedule.ViewSettings.ChronoGridViewSettings.Logo3) : null;
            pictureEditLogo4.Image = this.LocalSchedule.ViewSettings.ChronoGridViewSettings.Logo4 != null ? new Bitmap(this.LocalSchedule.ViewSettings.ChronoGridViewSettings.Logo4) : null;

            this.AdNotes.LoadAdNotes();
            this.SlideBullets.LoadSlideBullets();
            this.SlideHeader.LoadSlideHeader();
        }

        public void SaveView()
        {
            if (this.AllowToSave)
            {
                this.LocalSchedule.ViewSettings.ShowGridDetails = this.ShowGridDetails;

                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.IDPosition = this.PositionID;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.IndexPosition = this.PositionIndex;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DatePosition = this.PositionDate;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PCIPosition = this.PositionPCI;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.CostPosition = this.PositionCost;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.FinalCostPosition = this.PositionFinalCost;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DiscountPosition = this.PositionDiscount;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ColorPosition = this.PositionColor;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PublicationPosition = this.PositionPublication;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.SquarePosition = this.PositionSquare;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PageSizePosition = this.PositionPageSize;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PercentOfPagePosition = this.PositionPercentOfPage;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DimensionsPosition = this.PositionDimensions;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.MechanicalsPosition = this.PositionMechanicals;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.SectionPosition = this.PositionSection;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DeliveryPosition = this.PositionDelivery;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ReadershipPosition = this.PositionReadership;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DeadlinePosition = this.PositionDeadline;

                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionSquare = this.PositionColumnInchesInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionComments = this.PositionCommentsInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionDeadline = this.PositionDeadlineInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionDelivery = this.PositionDeliveryInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionDimensions = this.PositionDimensionsInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionMechanicals = this.PositionMechanicalsInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionPageSize = this.PositionPageSizeInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionPercentOfPage = this.PositionPercentOfPageInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionPublication = this.PositionPublicationInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionReadership = this.PositionReadershipInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.PositionSection = this.PositionSectionInPreview;

                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowColor = _showColorHeader;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowAdNotes = _showCommentsHeader;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowCost = _showCostHeader;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowDate = _showDateHeader;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowDeadline = _showDeadlineHeader;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowDelivery = _showDeliveryHeader;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowDimensions = _showDimensionsHeader;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowDiscount = _showDiscountHeader;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowFinalCost = _showFinalCostHeader;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowID = _showIDHeader;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowIndex = _showIndexHeader;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowMechanicals = _showMechanicalsHeader;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowPageSize = _showPageSizeHeader;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowPercentOfPage = _showPercentOfPageHeader;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowPCI = _showPCIHeader;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowPublication = _showPublicationHeader;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowReadership = _showReadershipHeader;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowSection = _showSectionHeader;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ShowSquare = _showSquareHeader;

                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowSquare = this.ShowColumnInchesInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowComments = this.ShowCommentsInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowDeadline = this.ShowDeadlineInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowDelivery = this.ShowDeliveryInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowDimensions = this.ShowDimensionsInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowMechanicals = this.ShowMechanicalsInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowPageSize = this.ShowPageSizeInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowPercentOfPage = this.ShowPercentOfPageInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowPublication = this.ShowPublicationInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowReadership = this.ShowReadershipInPreview;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.AdNotesState.ShowSection = this.ShowSectionInPreview;

                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.IDWidth = this.WidthID;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.IndexWidth = this.WidthIndex;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DateWidth = this.WidthDate;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PCIWidth = this.WidthPCI;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.CostWidth = this.WidthCost;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.FinalCostWidth = this.WidthFinalCost;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DiscountWidth = this.WidthDiscount;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ColorWidth = this.WidthColor;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PublicationWidth = this.WidthPublication;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.SquareWidth = this.WidthSquare;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PageSizeWidth = this.WidthPageSize;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PercentOfPageWidth = this.WidthPercentOfPage;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DimensionsWidth = this.WidthDimensions;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.MechanicalsWidth = this.WidthMechanicals;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.SectionWidth = this.WidthSection;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DeliveryWidth = this.WidthDelivery;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ReadershipWidth = this.WidthReadership;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DeadlineWidth = this.WidthDeadline;

                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.IDCaption = this.CaptionID;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.IndexCaption = this.CaptionIndex;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DateCaption = this.CaptionDate;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PCICaption = this.CaptionPCI;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.CostCaption = this.CaptionCost;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.FinalCostCaption = this.CaptionFinalCost;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DiscountCaption = this.CaptionDiscount;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ColorCaption = this.CaptionColor;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PublicationCaption = this.CaptionPublication;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.SquareCaption = this.CaptionSquare;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PageSizeCaption = this.CaptionPageSize;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.PercentOfPageCaption = this.CaptionPercentOfPage;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DimensionsCaption = this.CaptionDimensions;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.MechanicalsCaption = this.CaptionMechanicals;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.SectionCaption = this.CaptionSection;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DeliveryCaption = this.CaptionDelivery;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.ReadershipCaption = this.CaptionReadership;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.GridColumnsState.DeadlineCaption = this.CaptionDeadline;

                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.Logo1 = pictureEditLogo1.Image != null ? new Bitmap(pictureEditLogo1.Image) : null;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.Logo2 = pictureEditLogo2.Image != null ? new Bitmap(pictureEditLogo2.Image) : null;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.Logo3 = pictureEditLogo3.Image != null ? new Bitmap(pictureEditLogo3.Image) : null;
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.Logo4 = pictureEditLogo4.Image != null ? new Bitmap(pictureEditLogo4.Image) : null;

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
            pictureEditLogo1.Enabled = this.SlideHeaderState.ShowLogo1;
            pictureEditLogo2.Enabled = this.SlideHeaderState.ShowLogo2;
            pictureEditLogo3.Enabled = this.SlideHeaderState.ShowLogo3;
            pictureEditLogo4.Enabled = this.SlideHeaderState.ShowLogo4;
        }

        public void SetToggleState()
        {
            if (this.AllowToSave)
            {
                this.ShowGridDetails = FormMain.Instance.buttonItemGridsDetails.Checked;

                this.ShowIDHeader = FormMain.Instance.buttonItemGridsColumnsID.Checked;
                this.ShowDateHeader = FormMain.Instance.buttonItemGridsColumnsDate.Checked;
                this.ShowPCIHeader = FormMain.Instance.buttonItemGridsColumnsPCI.Checked;
                this.ShowCostHeader = FormMain.Instance.buttonItemGridsColumnsCost.Checked;
                this.ShowDiscountHeader = FormMain.Instance.buttonItemGridsColumnsDiscounts.Checked;
                this.ShowColorHeader = FormMain.Instance.buttonItemGridsColumnsColor.Checked;
                this.ShowFinalCostHeader = FormMain.Instance.buttonItemGridsColumnsFinalCost.Checked;
                this.ShowIndexHeader = FormMain.Instance.buttonItemGridsColumnsIndex.Checked;
                this.ShowSquareHeader = FormMain.Instance.buttonItemGridsColumnsSquare.Checked;
                this.ShowPageSizeHeader = FormMain.Instance.buttonItemGridsColumnsPageSize.Checked;
                this.ShowPercentOfPageHeader = FormMain.Instance.buttonItemGridsColumnsPercentOfPage.Checked;
                this.ShowDimensionsHeader = FormMain.Instance.buttonItemGridsColumnsDimensions.Checked;
                this.ShowMechanicalsHeader = FormMain.Instance.buttonItemGridsColumnsMechanicals.Checked;
                this.ShowPublicationHeader = FormMain.Instance.buttonItemGridsColumnsPublication.Checked;
                this.ShowReadershipHeader = FormMain.Instance.buttonItemGridsColumnsReadership.Checked;
                this.ShowSectionHeader = FormMain.Instance.buttonItemGridsColumnsSection.Checked;
                this.ShowDeliveryHeader = FormMain.Instance.buttonItemGridsColumnsDelivery.Checked;
                this.ShowDeadlineHeader = FormMain.Instance.buttonItemGridsColumnsDeadline.Checked;

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
            GridsControl.Instance.UpdateButtonsStateAccordingSelectedOutput();
        }

        private void SetColumnsState()
        {
            gridViewPublications.ColumnPositionChanged -= new EventHandler(gridViewPublications_ColumnPositionChanged);
            gridColumnColorPricing.VisibleIndex = this.PositionColor;
            gridColumnColumnInches.VisibleIndex = this.PositionSquare;
            gridColumnDate.VisibleIndex = this.PositionDate;
            gridColumnDeadline.VisibleIndex = this.PositionDeadline;
            gridColumnDelivery.VisibleIndex = this.PositionDelivery;
            gridColumnDiscountRate.VisibleIndex = this.PositionDiscount;
            gridColumnFinalRate.VisibleIndex = this.PositionFinalCost;
            gridColumnID.VisibleIndex = this.PositionID;
            gridColumnIndex.VisibleIndex = this.PositionIndex;
            gridColumnMechanicals.VisibleIndex = this.PositionMechanicals;
            gridColumnPageSize.VisibleIndex = this.PositionPageSize;
            gridColumnPercentOfPage.VisibleIndex = this.PositionPercentOfPage;
            gridColumnPCIRate.VisibleIndex = this.PositionPCI;
            gridColumnADRate.VisibleIndex = this.PositionCost;
            gridColumnPublication.VisibleIndex = this.PositionPublication;
            gridColumnDimensions.VisibleIndex = this.PositionDimensions;
            gridColumnReadership.VisibleIndex = this.PositionReadership;
            gridColumnSection.VisibleIndex = this.PositionSection;

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

        private void comboBoxEditSchedule_EditValueChanged(object sender, EventArgs e)
        {
            if (this.AllowToSave)
            {
                this.LocalSchedule.ViewSettings.ChronoGridViewSettings.SlideHeader = comboBoxEditSchedule.EditValue != null ? comboBoxEditSchedule.EditValue.ToString() : string.Empty;
                this.SettingsNotSaved = true;
            }
        }

        private void pictureEditLogo_Click(object sender, EventArgs e)
        {
            using (ToolForms.FormImageGallery form = new ToolForms.FormImageGallery())
            {
                if (form.ShowDialog() == DialogResult.OK && form.SelectedSource != null && form.SelectedSource.SmallLogo != null)
                {
                    (sender as DevExpress.XtraEditors.PictureEdit).Image = new System.Drawing.Bitmap(form.SelectedSource.SmallLogo);
                    SaveView();
                }
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
        #endregion

        #region Grid Events
        private void gridViewPublication_DoubleClick(object sender, EventArgs e)
        {
            DevExpress.Utils.DXMouseEventArgs args = (e as DevExpress.Utils.DXMouseEventArgs);
            DevExpress.XtraGrid.Views.Grid.GridView view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hi = view.CalcHitInfo(args.Location);
            if (hi.InColumn)
            {
                if (hi.Column != gridColumnDate && hi.Column != gridColumnPublication)
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

        private void gridViewPublications_ColumnPositionChanged(object sender, EventArgs e)
        {
            if (this.AllowToSave)
            {
                this.PositionCost = gridColumnADRate.VisibleIndex;
                this.PositionColor = gridColumnColorPricing.VisibleIndex;
                this.PositionSquare = gridColumnColumnInches.VisibleIndex;
                this.PositionDate = gridColumnDate.VisibleIndex;
                this.PositionDeadline = gridColumnDeadline.VisibleIndex;
                this.PositionDelivery = gridColumnDelivery.VisibleIndex;
                this.PositionDimensions = gridColumnDimensions.VisibleIndex;
                this.PositionDiscount = gridColumnDiscountRate.VisibleIndex;
                this.PositionFinalCost = gridColumnFinalRate.VisibleIndex;
                this.PositionID = gridColumnID.VisibleIndex;
                this.PositionIndex = gridColumnIndex.VisibleIndex;
                this.PositionMechanicals = gridColumnMechanicals.VisibleIndex;
                this.PositionPageSize = gridColumnPageSize.VisibleIndex;
                this.PositionPercentOfPage = gridColumnPercentOfPage.VisibleIndex;
                this.PositionPCI = gridColumnPCIRate.VisibleIndex;
                this.PositionPublication = gridColumnPublication.VisibleIndex;
                this.PositionReadership = gridColumnReadership.VisibleIndex;
                this.PositionSection = gridColumnSection.VisibleIndex;
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
        #endregion

        #region Output Stuff
        public int OutputFileIndex
        {
            get
            {
                if (InteropClasses.PowerPointHelper.Instance.Is2003)
                {
                    if (_showCommentsHeader)
                        return (this.SlideHeaderState.ShowLogo1 || this.SlideHeaderState.ShowLogo2 || this.SlideHeaderState.ShowLogo3 || this.SlideHeaderState.ShowLogo4) ? 1 : 2;
                    else
                        return (this.SlideHeaderState.ShowLogo1 || this.SlideHeaderState.ShowLogo2 || this.SlideHeaderState.ShowLogo3 || this.SlideHeaderState.ShowLogo4) ? 3 : 4;
                }
                else
                {
                    if (_showCommentsHeader)
                        return (this.SlideHeaderState.ShowLogo1 || this.SlideHeaderState.ShowLogo2 || this.SlideHeaderState.ShowLogo3 || this.SlideHeaderState.ShowLogo4) ? 5 : 6;
                    else
                        return (this.SlideHeaderState.ShowLogo1 || this.SlideHeaderState.ShowLogo2 || this.SlideHeaderState.ShowLogo3 || this.SlideHeaderState.ShowLogo4) ? 7 : 8;
                }
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

        public string LogoFile1
        {
            get
            {
                string result = string.Empty;
                if (this.SlideHeaderState.ShowLogo1 && pictureEditLogo1.Image != null)
                {
                    result = System.IO.Path.GetTempFileName();
                    pictureEditLogo1.Image.Save(result);
                }
                return result;
            }
        }

        public string LogoFile2
        {
            get
            {
                string result = string.Empty;
                if (this.SlideHeaderState.ShowLogo2 && pictureEditLogo2.Image != null)
                {
                    result = System.IO.Path.GetTempFileName();
                    pictureEditLogo2.Image.Save(result);
                }
                return result;
            }
        }

        public string LogoFile3
        {
            get
            {
                string result = string.Empty;
                if (this.SlideHeaderState.ShowLogo3 && pictureEditLogo3.Image != null)
                {
                    result = System.IO.Path.GetTempFileName();
                    pictureEditLogo3.Image.Save(result);
                }
                return result;
            }
        }

        public string LogoFile4
        {
            get
            {
                string result = string.Empty;
                if (this.SlideHeaderState.ShowLogo4 && pictureEditLogo4.Image != null)
                {
                    result = System.IO.Path.GetTempFileName();
                    pictureEditLogo4.Image.Save(result);
                }
                return result;
            }
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
                return this.SlideBulletsState.DoNotShow;
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

            int rowCountPerSlide = _showCommentsHeader ? (excelOutput ? BusinessClasses.OutputManager.ChronoGridExcelBasedRowsCountWithNotes : BusinessClasses.OutputManager.ChronoGridGridBasedRowsCountWithNotes) : (excelOutput ? BusinessClasses.OutputManager.ChronoGridExcelBasedRowsCountWithoutNotes : BusinessClasses.OutputManager.ChronoGridGridBasedRowsCountWithoutNotes);
            int insertsCount = _inserts.Count;
            for (int i = 0; i < insertsCount; i += rowCountPerSlide)
            {
                slides.Clear();
                for (int j = 0; j < rowCountPerSlide; j++)
                {
                    row.Clear();
                    if ((i + j) < insertsCount)
                    {
                        if (_showCommentsHeader)
                        {
                            adNotes.Clear();
                            int maxNumber = 12;
                            if (this.ShowCommentsInPreview && !string.IsNullOrEmpty(_inserts[i + j].FullComment))
                                adNotes.Add(this.PositionCommentsInPreview > 0 && !adNotes.Keys.Contains(this.PositionCommentsInPreview) ? this.PositionCommentsInPreview : ++maxNumber, _inserts[i + j].FullComment);
                            if (this.ShowSectionInPreview && !string.IsNullOrEmpty(_inserts[i + j].Section))
                                adNotes.Add(this.PositionSectionInPreview > 0 && !adNotes.Keys.Contains(this.PositionSectionInPreview) ? this.PositionSectionInPreview : ++maxNumber, "Section: " + _inserts[i + j].Section);
                            if (this.ShowMechanicalsInPreview && !string.IsNullOrEmpty(_inserts[i + j].Mechanicals))
                                adNotes.Add(this.PositionMechanicalsInPreview > 0 && !adNotes.Keys.Contains(this.PositionMechanicalsInPreview) ? this.PositionMechanicalsInPreview : ++maxNumber, "Mech: " + _inserts[i + j].MechanicalsOutput);
                            if (this.ShowDeliveryInPreview && !string.IsNullOrEmpty(_inserts[i + j].Delivery))
                                adNotes.Add(this.PositionDeliveryInPreview > 0 && !adNotes.Keys.Contains(this.PositionDeliveryInPreview) ? this.PositionDeliveryInPreview : ++maxNumber, "Delivery: " + _inserts[i + j].Delivery);
                            if (this.ShowPublicationInPreview && !string.IsNullOrEmpty(_inserts[i + j].Publication))
                                adNotes.Add(this.PositionPublicationInPreview > 0 && !adNotes.Keys.Contains(this.PositionPublicationInPreview) ? this.PositionPublicationInPreview : ++maxNumber, "Publication: " + _inserts[i + j].Publication);
                            if (this.ShowPageSizeInPreview && !string.IsNullOrEmpty(_inserts[i + j].PageSize))
                                adNotes.Add(this.PositionPageSizeInPreview > 0 && !adNotes.Keys.Contains(this.PositionPageSizeInPreview) ? this.PositionPageSizeInPreview : ++maxNumber, "Page Size: " + _inserts[i + j].PageSizeOutput);
                            if (this.ShowPercentOfPageInPreview && !string.IsNullOrEmpty(_inserts[i + j].PercentOfPage))
                                adNotes.Add(this.PositionPercentOfPageInPreview > 0 && !adNotes.Keys.Contains(this.PositionPercentOfPageInPreview) ? this.PositionPercentOfPageInPreview : ++maxNumber, "% of Page: " + _inserts[i + j].PercentOfPageOutput);
                            if (this.ShowDimensionsInPreview && !string.IsNullOrEmpty(_inserts[i + j].Dimensions))
                                adNotes.Add(this.PositionDimensionsInPreview > 0 && !adNotes.Keys.Contains(this.PositionDimensionsInPreview) ? this.PositionDimensionsInPreview : ++maxNumber, "Col. x Inches: " + _inserts[i + j].Dimensions);
                            if (this.ShowColumnInchesInPreview && !string.IsNullOrEmpty(_inserts[i + j].SquareStringFormatted))
                                adNotes.Add(this.PositionColumnInchesInPreview > 0 && !adNotes.Keys.Contains(this.PositionColumnInchesInPreview) ? this.PositionColumnInchesInPreview : ++maxNumber, "Total Col In: " + _inserts[i + j].SquareStringFormatted + " col. in.");
                            if (this.ShowReadershipInPreview && !string.IsNullOrEmpty(_inserts[i + j].Readership))
                                adNotes.Add(this.PositionReadershipInPreview > 0 && !adNotes.Keys.Contains(this.PositionReadershipInPreview) ? this.PositionReadershipInPreview : ++maxNumber, "Readership: " + _inserts[i + j].Readership);
                            if (this.ShowDeadlineInPreview && !string.IsNullOrEmpty(_inserts[i + j].Deadline))
                                adNotes.Add(this.PositionDeadlineInPreview > 0 && !adNotes.Keys.Contains(this.PositionDeadlineInPreview) ? this.PositionDeadlineInPreview : ++maxNumber, "Deadline: " + _inserts[i + j].DeadlineForOutput);
                            if (adNotes.Count > 0)
                                row.Add(-1, string.Join(",   ", adNotes.Values.ToArray()));
                            else
                                row.Add(-1, "            ");
                        }
                        if (this.PositionDate != -1)
                            row.Add(this.PositionDate, _inserts[i + j].Date.ToString("MM/dd/yy"));
                        if (this.PositionPCI != -1)
                            row.Add(this.PositionPCI, _inserts[i + j].PCIRate.HasValue ? (_inserts[i + j].PCIRate.Value.ToString("$#,###.00")) : "N/A");
                        if (this.PositionCost != -1)
                            row.Add(this.PositionCost, _inserts[i + j].ADRate.ToString("$#,###.00"));
                        if (this.PositionDiscount != -1)
                            row.Add(this.PositionDiscount, _inserts[i + j].DiscountRate.ToString("$#,###.00"));
                        if (this.PositionColor != -1)
                            row.Add(this.PositionColor, _inserts[i + j].ColorPricingCalculated > 0 ? _inserts[i + j].ColorPricingCalculated.ToString("$#,###.00") : _inserts[i + j].ColorPricingObject.ToString());
                        if (this.PositionFinalCost != -1)
                            row.Add(this.PositionFinalCost, _inserts[i + j].FinalRate.ToString("$#,###.00"));
                        if (this.PositionIndex != -1)
                            row.Add(this.PositionIndex, _inserts[i + j].Index.ToString("#,##0"));
                        if (this.PositionSquare != -1)
                            row.Add(this.PositionSquare, "'" + _inserts[i + j].SquareStringFormatted);
                        if (this.PositionPageSize != -1)
                            row.Add(this.PositionPageSize, _inserts[i + j].PageSizeOutput);
                        if (this.PositionPercentOfPage != -1)
                            row.Add(this.PositionPercentOfPage, _inserts[i + j].PercentOfPageOutput);
                        if (this.PositionMechanicals != -1)
                            row.Add(this.PositionMechanicals, _inserts[i + j].MechanicalsOutput);
                        if (this.PositionPublication != -1)
                            row.Add(this.PositionPublication, _inserts[i + j].Publication);
                        if (this.PositionDimensions != -1)
                            row.Add(this.PositionDimensions, _inserts[i + j].DimensionsOutput);
                        if (this.PositionSection != -1)
                            row.Add(this.PositionSection, _inserts[i + j].Section);
                        if (this.PositionReadership != -1)
                            row.Add(this.PositionReadership, _inserts[i + j].Readership);
                        if (this.PositionDelivery != -1)
                            row.Add(this.PositionDelivery, _inserts[i + j].Delivery);
                        if (this.PositionDeadline != -1)
                            row.Add(this.PositionDeadline, _inserts[i + j].DeadlineForOutput);
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
            if (!excelOutput)
                PopulateWeeklyScheduleReplacementsList();
        }

        public void PrintOutput()
        {
            using (ToolForms.FormGridType formGridType = new ToolForms.FormGridType())
            {
                formGridType.buttonXTable.Visible = this.SelectedColumnsCount >= 4 && this.SelectedColumnsCount <= 6 && Directory.Exists(BusinessClasses.OutputManager.Instance.ChronoGridGridBasedTemlatesFolderPath);
                DialogResult gridTypeResult = formGridType.ShowDialog();
                if (gridTypeResult != DialogResult.Cancel)
                {
                    bool pasteAsImage = gridTypeResult == DialogResult.No;
                    bool excelOutput = gridTypeResult != DialogResult.Ignore;
                    using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                    {
                        formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
                        formProgress.TopMost = true;
                        formProgress.Show();
                        PrepareOutput(excelOutput);
                        if (excelOutput)
                            InteropClasses.PowerPointHelper.Instance.AppendChronoGridExcelBased(pasteAsImage);
                        else
                            InteropClasses.PowerPointHelper.Instance.AppendChronoGridGridBased();
                        formProgress.Close();
                    }

                    using (OutputForms.FormSlideOutput formOutput = new OutputForms.FormSlideOutput())
                    {
                        if (formOutput.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                            AppManager.ActivateForm(FormMain.Instance.Handle, true, false);
                    }
                }
            }
        }

        public void Email()
        {
            using (ToolForms.FormGridType formGridType = new ToolForms.FormGridType())
            {
                formGridType.buttonXTable.Visible = this.SelectedColumnsCount >= 4 && this.SelectedColumnsCount <= 6 && Directory.Exists(BusinessClasses.OutputManager.Instance.MultiGridGridBasedTemlatesFolderPath);
                DialogResult gridTypeResult = formGridType.ShowDialog();
                if (gridTypeResult != DialogResult.Cancel)
                {
                    bool pasteAsImage = gridTypeResult == DialogResult.No;
                    bool excelOutput = gridTypeResult != DialogResult.Ignore;
                    using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                    {
                        formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
                        formProgress.TopMost = true;
                        formProgress.Show();
                        PrepareOutput(excelOutput);
                        string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
                        if (excelOutput)
                            InteropClasses.PowerPointHelper.Instance.PrepareChronoGridExcelBasedEmail(tempFileName, pasteAsImage);
                        else
                            InteropClasses.PowerPointHelper.Instance.PrepareChronoGridGridBasedEmail(tempFileName);
                        formProgress.Close();
                        if (File.Exists(tempFileName))
                            using (OutputForms.FormEmail formEmail = new OutputForms.FormEmail())
                            {
                                formEmail.Text = "Email this Chronological Schedule Grid";
                                formEmail.PresentationFile = tempFileName;
                                ConfigurationClasses.RegistryHelper.MainFormHandle = formEmail.Handle;
                                ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                                formEmail.ShowDialog();
                                ConfigurationClasses.RegistryHelper.MaximizeMainForm = true;
                                ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                            }
                    }
                }
            }
        }
        #endregion
    }
}
