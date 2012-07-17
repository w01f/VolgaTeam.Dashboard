using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class OutputDetailedGridControl : UserControl, IGridOutputControl
    {
        private static OutputDetailedGridControl _instance;
        private List<PublicationDetailedGridControl> _tabPages = new List<PublicationDetailedGridControl>();
        public BusinessClasses.Schedule LocalSchedule { get; set; }
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
                return this.LocalSchedule.ViewSettings.DetailedGridViewSettings.SlideBulletsState;
            }
        }

        public ConfigurationClasses.SlideHeaderState SlideHeaderState
        {
            get
            {
                return this.LocalSchedule.ViewSettings.DetailedGridViewSettings.SlideHeaderState;
            }
        }

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

        private OutputDetailedGridControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;

            this.AdNotes = new AdNotesControl(this);
            this.SlideBullets = new SlideBulletsControl(this);
            this.SlideHeader = new SlideHeaderControl(this);
            this.SlideHeader.checkEditLogo1.Text = "Publication Logo";
            this.SlideHeader.checkEditLogo2.Visible = false;
            this.SlideHeader.checkEditLogo3.Visible = false;
            this.SlideHeader.checkEditLogo4.Visible = false;

            #region Set Default Values
            this.EnableIDButton = true;
            this.EnableDateButton = true;
            this.EnablePublicationButton = true;

            this.PositionCommentsInPreview = 1;
            this.PositionSectionInPreview = 2;
            this.PositionMechanicalsInPreview = 3;
            this.PositionColumnInchesInPreview = 4;
            this.PositionPublicationInPreview = 5;
            this.PositionPageSizeInPreview = 6;
            this.PositionPercentOfPageInPreview = 7;
            this.PositionDimensionsInPreview = 8;
            this.PositionReadershipInPreview = 9;
            this.PositionDeliveryInPreview = 10;
            this.PositionDeadlineInPreview = 11;
            #endregion

            this.HelpToolTip = new DevComponents.DotNetBar.SuperTooltipInfo("HELP", "", "Help me understand how to use the Detailed Grid", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
            BusinessClasses.ScheduleManager.Instance.SettingsSaved += new EventHandler<BusinessClasses.SavingingEventArgs>((sender, e) =>
            {
                if (sender != this)
                    UpdateOutput(e.QuickSave);
            });


        }

        public static OutputDetailedGridControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new OutputDetailedGridControl();
                return _instance;
            }
        }

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

        private void LoadView()
        {
            this.PositionID = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IDPosition;
            this.PositionIndex = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IndexPosition;
            this.PositionDate = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DatePosition;
            this.PositionPCI = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PCIPosition;
            this.PositionCost = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.CostPosition;
            this.PositionFinalCost = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.FinalCostPosition;
            this.PositionDiscount = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DiscountPosition;
            this.PositionColor = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ColorPosition;
            this.PositionPublication = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PublicationPosition;
            this.PositionSquare = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SquarePosition;
            this.PositionPageSize = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PageSizePosition;
            this.PositionPercentOfPage = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PercentOfPagePosition;
            this.PositionDimensions = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DimensionsPosition;
            this.PositionMechanicals = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.MechanicalsPosition;
            this.PositionSection = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SectionPosition;
            this.PositionDelivery = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeliveryPosition;
            this.PositionReadership = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ReadershipPosition;
            this.PositionDeadline = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeadlinePosition;

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

            _showColorHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowColor;
            _showCommentsHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowAdNotes;
            _showCostHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowCost;
            _showDateHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDate;
            _showDeadlineHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDeadline;
            _showDeliveryHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDelivery;
            _showDimensionsHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDimensions;
            _showDiscountHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDiscount;
            _showFinalCostHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowFinalCost;
            _showIDHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowID;
            _showIndexHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowIndex;
            _showMechanicalsHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowMechanicals;
            _showPageSizeHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPageSize;
            _showPercentOfPageHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPercentOfPage & BusinessClasses.ListManager.Instance.ShareUnits.Count > 0;
            _showPCIHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPCI;
            _showPublicationHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPublication;
            _showReadershipHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowReadership;
            _showSectionHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowSection;
            _showSquareHeader = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowSquare;

            this.ShowColumnInchesInPreview = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowSquare;
            this.ShowCommentsInPreview = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowComments;
            this.ShowDeadlineInPreview = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowDeadline;
            this.ShowDeliveryInPreview = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowDelivery;
            this.ShowDimensionsInPreview = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowDimensions;
            this.ShowMechanicalsInPreview = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowMechanicals;
            this.ShowPageSizeInPreview = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowPageSize;
            this.ShowPercentOfPageInPreview = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowPercentOfPage;
            this.ShowPublicationInPreview = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowPublication;
            this.ShowReadershipInPreview = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowReadership;
            this.ShowSectionInPreview = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowSection;

            this.WidthID = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IDWidth;
            this.WidthIndex = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IndexWidth;
            this.WidthDate = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DateWidth;
            this.WidthPCI = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PCIWidth;
            this.WidthCost = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.CostWidth;
            this.WidthFinalCost = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.FinalCostWidth;
            this.WidthDiscount = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DiscountWidth;
            this.WidthColor = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ColorWidth;
            this.WidthPublication = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PublicationWidth;
            this.WidthSquare = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SquareWidth;
            this.WidthPageSize = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PageSizeWidth;
            this.WidthPercentOfPage = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PercentOfPageWidth;
            this.WidthDimensions = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DimensionsWidth;
            this.WidthMechanicals = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.MechanicalsWidth;
            this.WidthSection = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SectionWidth;
            this.WidthDelivery = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeliveryWidth;
            this.WidthReadership = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ReadershipWidth;
            this.WidthDeadline = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeadlineWidth;

            this.CaptionID = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IDCaption;
            this.CaptionIndex = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IndexCaption;
            this.CaptionDate = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DateCaption;
            this.CaptionPCI = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PCICaption;
            this.CaptionCost = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.CostCaption;
            this.CaptionFinalCost = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.FinalCostCaption;
            this.CaptionDiscount = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DiscountCaption;
            this.CaptionColor = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ColorCaption;
            this.CaptionPublication = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PublicationCaption;
            this.CaptionSquare = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SquareCaption;
            this.CaptionPageSize = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PageSizeCaption;
            this.CaptionPercentOfPage = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PercentOfPageCaption;
            this.CaptionDimensions = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DimensionsCaption;
            this.CaptionMechanicals = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.MechanicalsCaption;
            this.CaptionSection = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SectionCaption;
            this.CaptionDelivery = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeliveryCaption;
            this.CaptionReadership = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ReadershipCaption;
            this.CaptionDeadline = this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeadlineCaption;

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
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IDPosition = this.PositionID;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IndexPosition = this.PositionIndex;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DatePosition = this.PositionDate;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PCIPosition = this.PositionPCI;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.CostPosition = this.PositionCost;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.FinalCostPosition = this.PositionFinalCost;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DiscountPosition = this.PositionDiscount;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ColorPosition = this.PositionColor;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PublicationPosition = this.PositionPublication;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SquarePosition = this.PositionSquare;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PageSizePosition = this.PositionPageSize;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PercentOfPagePosition = this.PositionPercentOfPage;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DimensionsPosition = this.PositionDimensions;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.MechanicalsPosition = this.PositionMechanicals;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SectionPosition = this.PositionSection;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeliveryPosition = this.PositionDelivery;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ReadershipPosition = this.PositionReadership;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeadlinePosition = this.PositionDeadline;

                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionSquare = this.PositionColumnInchesInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionComments = this.PositionCommentsInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionDeadline = this.PositionDeadlineInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionDelivery = this.PositionDeliveryInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionDimensions = this.PositionDimensionsInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionMechanicals = this.PositionMechanicalsInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionPageSize = this.PositionPageSizeInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionPercentOfPage = this.PositionPercentOfPageInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionPublication = this.PositionPublicationInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionReadership = this.PositionReadershipInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.PositionSection = this.PositionSectionInPreview;

                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowColor = _showColorHeader;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowAdNotes = _showCommentsHeader;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowCost = _showCostHeader;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDate = _showDateHeader;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDeadline = _showDeadlineHeader;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDelivery = _showDeliveryHeader;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDimensions = _showDimensionsHeader;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowDiscount = _showDiscountHeader;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowFinalCost = _showFinalCostHeader;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowID = _showIDHeader;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowIndex = _showIndexHeader;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowMechanicals = _showMechanicalsHeader;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPageSize = _showPageSizeHeader;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPercentOfPage = _showPercentOfPageHeader;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPCI = _showPCIHeader;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowPublication = _showPublicationHeader;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowReadership = _showReadershipHeader;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowSection = _showSectionHeader;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ShowSquare = _showSquareHeader;

                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowSquare = this.ShowColumnInchesInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowComments = this.ShowCommentsInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowDeadline = this.ShowDeadlineInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowDelivery = this.ShowDeliveryInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowDimensions = this.ShowDimensionsInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowMechanicals = this.ShowMechanicalsInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowPageSize = this.ShowPageSizeInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowPercentOfPage = this.ShowPercentOfPageInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowPublication = this.ShowPublicationInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowReadership = this.ShowReadershipInPreview;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.AdNotesState.ShowSection = this.ShowSectionInPreview;

                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IDWidth = this.WidthID;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IndexWidth = this.WidthIndex;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DateWidth = this.WidthDate;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PCIWidth = this.WidthPCI;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.CostWidth = this.WidthCost;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.FinalCostWidth = this.WidthFinalCost;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DiscountWidth = this.WidthDiscount;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ColorWidth = this.WidthColor;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PublicationWidth = this.WidthPublication;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SquareWidth = this.WidthSquare;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PageSizeWidth = this.WidthPageSize;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PercentOfPageWidth = this.WidthPercentOfPage;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DimensionsWidth = this.WidthDimensions;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.MechanicalsWidth = this.WidthMechanicals;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SectionWidth = this.WidthSection;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeliveryWidth = this.WidthDelivery;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ReadershipWidth = this.WidthReadership;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeadlineWidth = this.WidthDeadline;

                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IDCaption = this.CaptionID;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.IndexCaption = this.CaptionIndex;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DateCaption = this.CaptionDate;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PCICaption = this.CaptionPCI;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.CostCaption = this.CaptionCost;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.FinalCostCaption = this.CaptionFinalCost;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DiscountCaption = this.CaptionDiscount;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ColorCaption = this.CaptionColor;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PublicationCaption = this.CaptionPublication;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SquareCaption = this.CaptionSquare;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PageSizeCaption = this.CaptionPageSize;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.PercentOfPageCaption = this.CaptionPercentOfPage;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DimensionsCaption = this.CaptionDimensions;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.MechanicalsCaption = this.CaptionMechanicals;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.SectionCaption = this.CaptionSection;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeliveryCaption = this.CaptionDelivery;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.ReadershipCaption = this.CaptionReadership;
                this.LocalSchedule.ViewSettings.DetailedGridViewSettings.GridColumnsState.DeadlineCaption = this.CaptionDeadline;

                this.SettingsNotSaved = true;
            }
        }

        public void SetToggleState()
        {
            if (this.AllowToSave)
            {
                this.ShowIDHeader = GridsControl.Instance.ColumnIDButtonItem.Checked;
                this.ShowDateHeader = GridsControl.Instance.ColumnDateButtonItem.Checked;
                this.ShowPCIHeader = GridsControl.Instance.ColumnPCIButtonItem.Checked;
                this.ShowCostHeader = GridsControl.Instance.ColumnCostButtonItem.Checked;
                this.ShowDiscountHeader = GridsControl.Instance.ColumnDiscountsButtonItem.Checked;
                this.ShowColorHeader = GridsControl.Instance.ColumnColorButtonItem.Checked;
                this.ShowFinalCostHeader = GridsControl.Instance.ColumnTotalCostButtonItem.Checked;
                this.ShowIndexHeader = GridsControl.Instance.ColumnIndexButtonItem.Checked;
                this.ShowSquareHeader = GridsControl.Instance.ColumnSquareButtonItem.Checked;
                this.ShowPageSizeHeader = GridsControl.Instance.ColumnPageSizeButtonItem.Checked;
                this.ShowPercentOfPageHeader = GridsControl.Instance.ColumnPercentOfPageButtonItem.Checked;
                this.ShowDimensionsHeader = GridsControl.Instance.ColumnDimensionsButtonItem.Checked;
                this.ShowMechanicalsHeader = GridsControl.Instance.ColumnMechanicalsButtonItem.Checked;
                this.ShowPublicationHeader = GridsControl.Instance.ColumnPublicationButtonItem.Checked;
                this.ShowReadershipHeader = GridsControl.Instance.ColumnReadershipButtonItem.Checked;
                this.ShowSectionHeader = GridsControl.Instance.ColumnSectionButtonItem.Checked;
                this.ShowDeliveryHeader = GridsControl.Instance.ColumnDeliveryButtonItem.Checked;
                this.ShowDeadlineHeader = GridsControl.Instance.ColumnDeadlineButtonItem.Checked;

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
            foreach (PublicationDetailedGridControl publicationControl in _tabPages)
            {
                publicationControl.gridColumnColorPricing.VisibleIndex = this.PositionColor;
                publicationControl.gridColumnColumnInches.VisibleIndex = this.PositionSquare;
                publicationControl.gridColumnDate.VisibleIndex = this.PositionDate;
                publicationControl.gridColumnDeadline.VisibleIndex = this.PositionDeadline;
                publicationControl.gridColumnDelivery.VisibleIndex = this.PositionDelivery;
                publicationControl.gridColumnDiscountRate.VisibleIndex = this.PositionDiscount;
                publicationControl.gridColumnFinalRate.VisibleIndex = this.PositionFinalCost;
                publicationControl.gridColumnID.VisibleIndex = this.PositionID;
                publicationControl.gridColumnIndex.VisibleIndex = this.PositionIndex;
                publicationControl.gridColumnMechanicals.VisibleIndex = this.PositionMechanicals;
                publicationControl.gridColumnPageSize.VisibleIndex = this.PositionPageSize;
                publicationControl.gridColumnPercentOfPage.VisibleIndex = this.PositionPercentOfPage;
                publicationControl.gridColumnPCIRate.VisibleIndex = this.PositionPCI;
                publicationControl.gridColumnADRate.VisibleIndex = this.PositionCost;
                publicationControl.gridColumnPublication.VisibleIndex = this.PositionPublication;
                publicationControl.gridColumnDimensions.VisibleIndex = this.PositionDimensions;
                publicationControl.gridColumnReadership.VisibleIndex = this.PositionReadership;
                publicationControl.gridColumnSection.VisibleIndex = this.PositionSection;

                publicationControl.gridViewPublications.OptionsView.ShowPreview = _showCommentsHeader;
                publicationControl.gridColumnColorPricing.Visible = _showColorHeader;
                publicationControl.gridColumnColumnInches.Visible = _showSquareHeader;
                publicationControl.gridColumnDate.Visible = _showDateHeader;
                publicationControl.gridColumnDeadline.Visible = _showDeadlineHeader;
                publicationControl.gridColumnDelivery.Visible = _showDeliveryHeader;
                publicationControl.gridColumnDiscountRate.Visible = _showDiscountHeader;
                publicationControl.gridColumnFinalRate.Visible = _showFinalCostHeader;
                publicationControl.gridColumnID.Visible = _showIDHeader;
                publicationControl.gridColumnIndex.Visible = _showIndexHeader;
                publicationControl.gridColumnMechanicals.Visible = _showMechanicalsHeader;
                publicationControl.gridColumnPageSize.Visible = _showPageSizeHeader;
                publicationControl.gridColumnPercentOfPage.Visible = _showPercentOfPageHeader;
                publicationControl.gridColumnPCIRate.Visible = _showPCIHeader;
                publicationControl.gridColumnADRate.Visible = _showCostHeader;
                publicationControl.gridColumnPublication.Visible = _showPublicationHeader;
                publicationControl.gridColumnDimensions.Visible = _showDimensionsHeader;
                publicationControl.gridColumnReadership.Visible = _showReadershipHeader;
                publicationControl.gridColumnSection.Visible = _showSectionHeader;

                publicationControl.gridColumnColorPricing.Width = this.WidthColor;
                publicationControl.gridColumnColumnInches.Width = this.WidthSquare;
                publicationControl.gridColumnDate.Width = this.WidthDate;
                publicationControl.gridColumnDeadline.Width = this.WidthDeadline;
                publicationControl.gridColumnDelivery.Width = this.WidthDelivery;
                publicationControl.gridColumnDiscountRate.Width = this.WidthDiscount;
                publicationControl.gridColumnFinalRate.Width = this.WidthFinalCost;
                publicationControl.gridColumnID.Width = this.WidthID;
                publicationControl.gridColumnIndex.Width = this.WidthIndex;
                publicationControl.gridColumnMechanicals.Width = this.WidthMechanicals;
                publicationControl.gridColumnPageSize.Width = this.WidthPageSize;
                publicationControl.gridColumnPercentOfPage.Width = this.WidthPercentOfPage;
                publicationControl.gridColumnPCIRate.Width = this.WidthPCI;
                publicationControl.gridColumnADRate.Width = this.WidthCost;
                publicationControl.gridColumnPublication.Width = this.WidthPublication;
                publicationControl.gridColumnDimensions.Width = this.WidthDimensions;
                publicationControl.gridColumnReadership.Width = this.WidthReadership;
                publicationControl.gridColumnSection.Width = this.WidthSection;

                publicationControl.gridColumnColorPricing.Caption = this.CaptionColor;
                publicationControl.gridColumnColumnInches.Caption = this.CaptionSquare;
                publicationControl.gridColumnDate.Caption = this.CaptionDate;
                publicationControl.gridColumnDeadline.Caption = this.CaptionDeadline;
                publicationControl.gridColumnDelivery.Caption = this.CaptionDelivery;
                publicationControl.gridColumnDiscountRate.Caption = this.CaptionDiscount;
                publicationControl.gridColumnFinalRate.Caption = this.CaptionFinalCost;
                publicationControl.gridColumnID.Caption = this.CaptionID;
                publicationControl.gridColumnIndex.Caption = this.CaptionIndex;
                publicationControl.gridColumnMechanicals.Caption = this.CaptionMechanicals;
                publicationControl.gridColumnPageSize.Caption = this.CaptionPageSize;
                publicationControl.gridColumnPercentOfPage.Caption = this.CaptionPercentOfPage;
                publicationControl.gridColumnPCIRate.Caption = this.CaptionPCI;
                publicationControl.gridColumnADRate.Caption = this.CaptionCost;
                publicationControl.gridColumnPublication.Caption = this.CaptionPublication;
                publicationControl.gridColumnDimensions.Caption = this.CaptionDimensions;
                publicationControl.gridColumnReadership.Caption = this.CaptionReadership;
                publicationControl.gridColumnSection.Caption = this.CaptionSection;
            }
            SaveView();
        }

        public void SetPreviewState()
        {
            foreach (PublicationDetailedGridControl publicationControl in _tabPages)
            {
                publicationControl.gridViewPublications.OptionsView.ShowPreview = _showCommentsHeader;
                publicationControl.gridViewPublications.RefreshData();
            }
        }

        public void SetSlideHeader()
        {
            foreach (PublicationDetailedGridControl publicationControl in _tabPages)
            {
                publicationControl.SetSlideHeader();
            }
        }

        private void UpdateSlideBullets()
        {
            PublicationDetailedGridControl publicationControl = xtraTabControlPublications.SelectedTabPage as PublicationDetailedGridControl;
            if (publicationControl != null)
            {
                this.SlideBullets.TotalInserts = publicationControl.Publication.TotalInserts.ToString("#,##0");
                this.SlideBullets.PageSize = publicationControl.Publication.SizeOptions.PageSizeOutput;
                this.SlideBullets.PercentOfPage = publicationControl.Publication.SizeOptions.PercentOfPageOutput;
                this.SlideBullets.Dimensions = publicationControl.Publication.SizeOptions.Dimensions;
                this.SlideBullets.ColumnInches = publicationControl.Publication.SizeOptions.Square.HasValue ? publicationControl.Publication.SizeOptions.Square.Value.ToString("#,###.00#") : "N/A";
                this.SlideBullets.AvgAdCost = publicationControl.Publication.AvgADRate.ToString("$#,###.00");
                this.SlideBullets.AvgFinalCost = publicationControl.Publication.AvgFinalRate.ToString("$#,###.00");
                this.SlideBullets.AvgPCI = publicationControl.Publication.AvgPCIRate.ToString("$#,###.00");
                this.SlideBullets.Delivery = publicationControl.Publication.DailyDelivery != null ? publicationControl.Publication.DailyDelivery.Value.ToString("#,##0") : string.Empty;
                this.SlideBullets.Readership = publicationControl.Publication.DailyReadership != null ? publicationControl.Publication.DailyReadership.Value.ToString("#,##0") : string.Empty;
                this.SlideBullets.TotalColor = publicationControl.Publication.TotalColorPricingCalculated.ToString("$#,###.00");
                this.SlideBullets.Discounts = publicationControl.Publication.TotalDiscountRate.ToString("$#,###.00");
                this.SlideBullets.TotalFinalCost = publicationControl.Publication.TotalFinalRate.ToString("$#,###.00");
                this.SlideBullets.TotalSquare = publicationControl.Publication.TotalSquare.HasValue ? publicationControl.Publication.TotalSquare.Value.ToString("#,###.00#") : "N/A";
            }
            else
            {
                this.SlideBullets.TotalInserts = string.Empty;
                this.SlideBullets.PageSize = string.Empty;
                this.SlideBullets.PercentOfPage = string.Empty;
                this.SlideBullets.Dimensions = string.Empty;
                this.SlideBullets.ColumnInches = string.Empty;
                this.SlideBullets.AvgAdCost = string.Empty;
                this.SlideBullets.AvgFinalCost = string.Empty;
                this.SlideBullets.AvgPCI = string.Empty;
                this.SlideBullets.Delivery = string.Empty;
                this.SlideBullets.Readership = string.Empty;
                this.SlideBullets.TotalColor = string.Empty;
                this.SlideBullets.Discounts = string.Empty;
                this.SlideBullets.TotalFinalCost = string.Empty;
                this.SlideBullets.TotalSquare = string.Empty;
            }
        }

        public void UpdateOutput(bool quickLoad)
        {
            this.LocalSchedule = BusinessClasses.ScheduleManager.Instance.GetLocalSchedule();
            laScheduleWindow.Text = string.Format("{0} - {1}", new object[] { this.LocalSchedule.FlightDateStart.ToString("MM/dd/yy"), this.LocalSchedule.FlightDateEnd.ToString("MM/dd/yy") });
            laScheduleName.Text = this.LocalSchedule.Name;
            laAdvertiser.Text = this.LocalSchedule.BusinessName + (!string.IsNullOrEmpty(this.LocalSchedule.AccountNumber) ? (" - " + this.LocalSchedule.AccountNumber) : string.Empty);
            if (!quickLoad)
            {
                xtraTabControlPublications.SuspendLayout();
                Application.DoEvents();
                xtraTabControlPublications.SelectedPageChanged -= new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControlPublications_SelectedPageChanged); ;
                xtraTabControlPublications.TabPages.Clear();
                _tabPages.RemoveAll(x => !this.LocalSchedule.Publications.Select(y => y.UniqueID).Contains(x.Publication.UniqueID));
                foreach (BusinessClasses.Publication publication in this.LocalSchedule.Publications)
                {
                    if (!string.IsNullOrEmpty(publication.Name))
                    {
                        PublicationDetailedGridControl publicationTab = _tabPages.Where(x => x.Publication.UniqueID.Equals(publication.UniqueID)).FirstOrDefault();
                        if (publicationTab == null)
                        {
                            publicationTab = new PublicationDetailedGridControl();
                            _tabPages.Add(publicationTab);
                            Application.DoEvents();
                        }
                        publicationTab.Publication = publication;
                        publicationTab.PageEnabled = publication.Inserts.Count > 0;
                        publicationTab.LoadPublication();
                        Application.DoEvents();
                    }
                }
                _tabPages.Sort((x, y) => x.Publication.Index.CompareTo(y.Publication.Index));
                xtraTabControlPublications.TabPages.AddRange(_tabPages.ToArray());

                LoadView();

                this.AllowToSave = false;
                SetColumnsState();
                UpdateSlideBullets();
                this.AllowToSave = true;
                Application.DoEvents();
                xtraTabControlPublications.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControlPublications_SelectedPageChanged); ;
                xtraTabControlPublications.ResumeLayout();
            }
            this.SettingsNotSaved = false;
        }

        public void OpenHelp()
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("detailedgrid");
        }

        private void xtraTabControlPublications_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            this.AllowToSave = false;
            UpdateSlideBullets();
            this.AllowToSave = true;
        }

        #region Output Stuff
        public void PrintOutput()
        {
            using (OutputForms.FormSelectPublication form = new OutputForms.FormSelectPublication())
            {
                form.Text = "Detailed Advertising Grid Slide Output";
                form.pbLogo.Image = Properties.Resources.Grid;
                form.laTitle.Text = "You have Several Publications in this Detailed Advertising Grid…";
                form.buttonXCurrentPublication.Text = "Send just the Current Advertising Grid to my PowerPoint Presentation";
                form.buttonXSelectedPublications.Text = "Send all Selected Advertising Grids to my PowerPoint Presentation";
                foreach (PublicationDetailedGridControl tabPage in _tabPages)
                    if (tabPage.PageEnabled)
                        form.checkedListBoxControlPublications.Items.Add(tabPage.Publication.UniqueID, tabPage.Publication.Name, CheckState.Checked, true);
                DialogResult result = DialogResult.Yes;
                if (form.checkedListBoxControlPublications.Items.Count > 1)
                {
                    ConfigurationClasses.RegistryHelper.MainFormHandle = form.Handle;
                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                    result = form.ShowDialog();
                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = true;
                    ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                    if (result == DialogResult.Cancel)
                        return;
                }
                using (OutputForms.FormSelectOutput formGridType = new OutputForms.FormSelectOutput(OutputForms.OutputType.PowerPoint))
                {
                    formGridType.buttonXGrid.Enabled = this.SelectedColumnsCount >= 4 && this.SelectedColumnsCount <= 6 && Directory.Exists(BusinessClasses.OutputManager.Instance.DetailedGridGridBasedTemlatesFolderPath);
                    DialogResult gridTypeResult = formGridType.ShowDialog();
                    if (gridTypeResult != DialogResult.Cancel)
                    {
                        bool pasteAsImage = gridTypeResult == DialogResult.Ignore;
                        bool excelOutput = gridTypeResult == DialogResult.No;
                        using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                        {
                            formProgress.laProgress.Text = "Chill-Out for a few seconds...\nGenerating slides so your presentation can look AWESOME!";
                            formProgress.TopMost = true;
                            formProgress.Show();
                            if (result == DialogResult.Yes)
                                (xtraTabControlPublications.TabPages[xtraTabControlPublications.SelectedTabPageIndex] as PublicationDetailedGridControl).PrintOutput(excelOutput, pasteAsImage);
                            else if (result == DialogResult.No)
                            {
                                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
                                {
                                    if (item.CheckState == CheckState.Checked)
                                    {
                                        PublicationDetailedGridControl tabPage = _tabPages.Where(x => x.Publication.UniqueID.Equals(item.Value)).FirstOrDefault();
                                        if (tabPage != null)
                                            tabPage.PrintOutput(excelOutput, pasteAsImage);
                                    }
                                }
                            }
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
        }

        public void Email()
        {
            using (OutputForms.FormSelectPublication form = new OutputForms.FormSelectPublication())
            {
                form.Text = "Detailed Advertising Grid Email Output";
                form.pbLogo.Image = Properties.Resources.EmailBig;
                form.laTitle.Text = "You have Several Publications in this Detailed Advertising Grid…";
                form.buttonXCurrentPublication.Text = "Attach the Current Advertising Grid to my Outlook Email Message";
                form.buttonXSelectedPublications.Text = "Attach all Selected Advertising Grids to my Outlook Email Message";
                foreach (PublicationDetailedGridControl tabPage in _tabPages)
                    if (tabPage.PageEnabled)
                        form.checkedListBoxControlPublications.Items.Add(tabPage.Publication.UniqueID, tabPage.Publication.Name, CheckState.Checked, true);
                DialogResult result = DialogResult.Yes;
                if (form.checkedListBoxControlPublications.Items.Count > 1)
                {
                    ConfigurationClasses.RegistryHelper.MainFormHandle = form.Handle;
                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                    result = form.ShowDialog();
                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = true;
                    ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                    if (result == DialogResult.Cancel)
                        return;
                }
                using (OutputForms.FormSelectOutput formGridType = new OutputForms.FormSelectOutput(OutputForms.OutputType.Email))
                {
                    formGridType.buttonXGrid.Enabled = this.SelectedColumnsCount >= 4 && this.SelectedColumnsCount <= 6 && Directory.Exists(BusinessClasses.OutputManager.Instance.DetailedGridGridBasedTemlatesFolderPath);
                    DialogResult gridTypeResult = formGridType.ShowDialog();
                    if (gridTypeResult != DialogResult.Cancel)
                    {
                        bool pasteAsImage = gridTypeResult == DialogResult.Ignore;
                        bool excelOutput = gridTypeResult == DialogResult.No;
                        using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                        {
                            formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Presentation for Email...";
                            formProgress.TopMost = true;
                            formProgress.Show();
                            string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
                            if (result == DialogResult.Yes)
                            {
                                PublicationDetailedGridControl outputControl = xtraTabControlPublications.TabPages[xtraTabControlPublications.SelectedTabPageIndex] as PublicationDetailedGridControl;
                                if (outputControl != null)
                                {
                                    outputControl.PrepareOutput(excelOutput);
                                    if (excelOutput)
                                        InteropClasses.PowerPointHelper.Instance.PrepareDetailedGridExcelBasedEmail(tempFileName, new PublicationDetailedGridControl[] { outputControl }, pasteAsImage);
                                    else
                                        InteropClasses.PowerPointHelper.Instance.PrepareDetailedGridGridBasedEmail(tempFileName, new PublicationDetailedGridControl[] { outputControl });
                                }
                            }
                            else if (result == DialogResult.No)
                            {
                                List<PublicationDetailedGridControl> emailPages = new List<PublicationDetailedGridControl>();
                                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
                                {
                                    if (item.CheckState == CheckState.Checked)
                                    {
                                        PublicationDetailedGridControl tabPage = _tabPages.Where(x => x.Publication.UniqueID.Equals(item.Value)).FirstOrDefault();
                                        if (tabPage != null)
                                        {
                                            tabPage.PrepareOutput(excelOutput);
                                            emailPages.Add(tabPage);
                                        }
                                    }
                                }
                                if (excelOutput)
                                    InteropClasses.PowerPointHelper.Instance.PrepareDetailedGridExcelBasedEmail(tempFileName, emailPages.ToArray(), pasteAsImage);
                                else
                                    InteropClasses.PowerPointHelper.Instance.PrepareDetailedGridGridBasedEmail(tempFileName, emailPages.ToArray());
                            }
                            formProgress.Close();
                            if (File.Exists(tempFileName))
                                using (OutputForms.FormEmail formEmail = new OutputForms.FormEmail())
                                {
                                    formEmail.Text = "Email this Detailed Advertising Grid";
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
        }

        public void Preview()
        {
            using (OutputForms.FormSelectPublication form = new OutputForms.FormSelectPublication())
            {
                form.Text = "Detailed Advertising Grid Preview";
                form.pbLogo.Image = Properties.Resources.Preview;
                form.laTitle.Text = "You have Several Publications in this Detailed Advertising Grid…";
                form.buttonXCurrentPublication.Text = "Preview the Current Advertising Grid";
                form.buttonXSelectedPublications.Text = "Preview all Selected Advertising Grids";
                foreach (PublicationDetailedGridControl tabPage in _tabPages)
                    if (tabPage.PageEnabled)
                        form.checkedListBoxControlPublications.Items.Add(tabPage.Publication.UniqueID, tabPage.Publication.Name, CheckState.Checked, true);
                DialogResult result = DialogResult.Yes;
                if (form.checkedListBoxControlPublications.Items.Count > 1)
                {
                    ConfigurationClasses.RegistryHelper.MainFormHandle = form.Handle;
                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                    result = form.ShowDialog();
                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = true;
                    ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                    if (result == DialogResult.Cancel)
                        return;
                }
                using (OutputForms.FormSelectOutput formGridType = new OutputForms.FormSelectOutput(OutputForms.OutputType.Preview))
                {
                    formGridType.buttonXGrid.Enabled = this.SelectedColumnsCount >= 4 && this.SelectedColumnsCount <= 6 && Directory.Exists(BusinessClasses.OutputManager.Instance.DetailedGridGridBasedTemlatesFolderPath);
                    DialogResult gridTypeResult = formGridType.ShowDialog();
                    if (gridTypeResult != DialogResult.Cancel)
                    {
                        bool pasteAsImage = gridTypeResult == DialogResult.Ignore;
                        bool excelOutput = gridTypeResult == DialogResult.No;
                        using (ToolForms.FormProgress formProgress = new ToolForms.FormProgress())
                        {
                            formProgress.laProgress.Text = "Chill-Out for a few seconds...\nPreparing Preview...";
                            formProgress.TopMost = true;
                            formProgress.Show();
                            string tempFileName = Path.Combine(ConfigurationClasses.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
                            if (result == DialogResult.Yes)
                            {
                                PublicationDetailedGridControl outputControl = xtraTabControlPublications.TabPages[xtraTabControlPublications.SelectedTabPageIndex] as PublicationDetailedGridControl;
                                if (outputControl != null)
                                {
                                    outputControl.PrepareOutput(excelOutput);
                                    if (excelOutput)
                                        InteropClasses.PowerPointHelper.Instance.PrepareDetailedGridExcelBasedEmail(tempFileName, new PublicationDetailedGridControl[] { outputControl }, pasteAsImage);
                                    else
                                        InteropClasses.PowerPointHelper.Instance.PrepareDetailedGridGridBasedEmail(tempFileName, new PublicationDetailedGridControl[] { outputControl });
                                }
                            }
                            else if (result == DialogResult.No)
                            {
                                List<PublicationDetailedGridControl> emailPages = new List<PublicationDetailedGridControl>();
                                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
                                {
                                    if (item.CheckState == CheckState.Checked)
                                    {
                                        PublicationDetailedGridControl tabPage = _tabPages.Where(x => x.Publication.UniqueID.Equals(item.Value)).FirstOrDefault();
                                        if (tabPage != null)
                                        {
                                            tabPage.PrepareOutput(excelOutput);
                                            emailPages.Add(tabPage);
                                        }
                                    }
                                }
                                if (excelOutput)
                                    InteropClasses.PowerPointHelper.Instance.PrepareDetailedGridExcelBasedEmail(tempFileName, emailPages.ToArray(), pasteAsImage);
                                else
                                    InteropClasses.PowerPointHelper.Instance.PrepareDetailedGridGridBasedEmail(tempFileName, emailPages.ToArray());
                            }
                            formProgress.Close();
                            if (File.Exists(tempFileName))
                                using (OutputForms.FormPreview formPreview = new OutputForms.FormPreview())
                                {
                                    formPreview.Text = "Preview Detailed Advertising Grid";
                                    formPreview.PresentationFile = tempFileName;
                                    ConfigurationClasses.RegistryHelper.MainFormHandle = formPreview.Handle;
                                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                                    formPreview.ShowDialog();
                                    ConfigurationClasses.RegistryHelper.MaximizeMainForm = true;
                                    ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                                }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
