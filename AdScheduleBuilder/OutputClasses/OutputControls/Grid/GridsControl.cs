using System;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class GridsControl : UserControl
    {
        private static GridsControl _instance;
        private IGridOutputControl _selectedOutput = null;

        public bool ShowGridDetails { get; set; }

        #region Operation Buttons
        public DevComponents.DotNetBar.ButtonItem HelpButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem DetailsButtonItem { get; set; }

        public DevComponents.DotNetBar.ButtonItem ColumnIDButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem ColumnIndexButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem ColumnDateButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem ColumnPCIButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem ColumnPercentOfPageButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem ColumnColorButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem ColumnCostButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem ColumnSectionButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem ColumnTotalCostButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem ColumnPublicationButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem ColumnDimensionsButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem ColumnMechanicalsButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem ColumnDeliveryButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem ColumnDiscountsButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem ColumnPageSizeButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem ColumnSquareButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem ColumnDeadlineButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem ColumnReadershipButtonItem { get; set; }
        #endregion

        private GridsControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
        }

        public static GridsControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GridsControl();
                return _instance;
            }
        }

        public bool AllowToLeaveControl
        {
            get
            {
                bool result = false;
                if (_selectedOutput.SettingsNotSaved)
                {
                    SaveSchedule();
                    result = true;
                }
                else
                    result = true;
                return result;
            }
        }

        private void SaveSchedule(string newName = "")
        {
            if (_selectedOutput != null)
            {
                if (!string.IsNullOrEmpty(newName))
                    _selectedOutput.LocalSchedule.Name = newName;
                _selectedOutput.SettingsNotSaved = false;
                _selectedOutput.LocalSchedule.ViewSettings.SaveDefaultViewSettings();
                BusinessClasses.ScheduleManager.Instance.SaveSchedule(_selectedOutput.LocalSchedule, true, _selectedOutput as Control);
                _selectedOutput.UpdateOutput(true);
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

        public void SelectGrid(GridType gridType)
        {
            switch (gridType)
            {
                case GridType.DetailedGrid:
                    if (_selectedOutput == null)
                        this.ShowGridDetails = OutputControls.OutputDetailedGridControl.Instance.LocalSchedule.ViewSettings.ShowGridDetails;
                    _selectedOutput = OutputControls.OutputDetailedGridControl.Instance;
                    this.HelpButtonItem = FormMain.Instance.buttonItemDetailedGridHelp;
                    this.DetailsButtonItem = FormMain.Instance.buttonItemDetailedGridDetails;

                    this.ColumnIDButtonItem = FormMain.Instance.buttonItemDetailedGridColumnsID;
                    this.ColumnIndexButtonItem = FormMain.Instance.buttonItemDetailedGridColumnsIndex;
                    this.ColumnDateButtonItem = FormMain.Instance.buttonItemDetailedGridColumnsDate;
                    this.ColumnPCIButtonItem = FormMain.Instance.buttonItemDetailedGridColumnsPCI;
                    this.ColumnPercentOfPageButtonItem = FormMain.Instance.buttonItemDetailedGridColumnsPercentOfPage;
                    this.ColumnColorButtonItem = FormMain.Instance.buttonItemDetailedGridColumnsColor;
                    this.ColumnCostButtonItem = FormMain.Instance.buttonItemDetailedGridColumnsCost;
                    this.ColumnSectionButtonItem = FormMain.Instance.buttonItemDetailedGridColumnsSection;
                    this.ColumnTotalCostButtonItem = FormMain.Instance.buttonItemDetailedGridColumnsTotalCost;
                    this.ColumnPublicationButtonItem = FormMain.Instance.buttonItemDetailedGridColumnsPublication;
                    this.ColumnDimensionsButtonItem = FormMain.Instance.buttonItemDetailedGridColumnsDimensions;
                    this.ColumnMechanicalsButtonItem = FormMain.Instance.buttonItemDetailedGridColumnsMechanicals;
                    this.ColumnDeliveryButtonItem = FormMain.Instance.buttonItemDetailedGridColumnsDelivery;
                    this.ColumnDiscountsButtonItem = FormMain.Instance.buttonItemDetailedGridColumnsDiscounts;
                    this.ColumnPageSizeButtonItem = FormMain.Instance.buttonItemDetailedGridColumnsPageSize;
                    this.ColumnSquareButtonItem = FormMain.Instance.buttonItemDetailedGridColumnsSquare;
                    this.ColumnDeadlineButtonItem = FormMain.Instance.buttonItemDetailedGridColumnsDeadline;
                    this.ColumnReadershipButtonItem = FormMain.Instance.buttonItemDetailedGridColumnsReadership;
                    break;
                case GridType.MultiGrid:
                    if (_selectedOutput == null)
                        this.ShowGridDetails = OutputControls.OutputMultiGridControl.Instance.LocalSchedule.ViewSettings.ShowGridDetails;
                    _selectedOutput = OutputControls.OutputMultiGridControl.Instance;
                    this.HelpButtonItem = FormMain.Instance.buttonItemMultiGridHelp;
                    this.DetailsButtonItem = FormMain.Instance.buttonItemMultiGridDetails;

                    this.ColumnIDButtonItem = FormMain.Instance.buttonItemMultiGridColumnsID;
                    this.ColumnIndexButtonItem = FormMain.Instance.buttonItemMultiGridColumnsIndex;
                    this.ColumnDateButtonItem = FormMain.Instance.buttonItemMultiGridColumnsDate;
                    this.ColumnPCIButtonItem = FormMain.Instance.buttonItemMultiGridColumnsPCI;
                    this.ColumnPercentOfPageButtonItem = FormMain.Instance.buttonItemMultiGridColumnsPercentOfPage;
                    this.ColumnColorButtonItem = FormMain.Instance.buttonItemMultiGridColumnsColor;
                    this.ColumnCostButtonItem = FormMain.Instance.buttonItemMultiGridColumnsCost;
                    this.ColumnSectionButtonItem = FormMain.Instance.buttonItemMultiGridColumnsSection;
                    this.ColumnTotalCostButtonItem = FormMain.Instance.buttonItemMultiGridColumnsTotalCost;
                    this.ColumnPublicationButtonItem = FormMain.Instance.buttonItemMultiGridColumnsPublication;
                    this.ColumnDimensionsButtonItem = FormMain.Instance.buttonItemMultiGridColumnsDimensions;
                    this.ColumnMechanicalsButtonItem = FormMain.Instance.buttonItemMultiGridColumnsMechanicals;
                    this.ColumnDeliveryButtonItem = FormMain.Instance.buttonItemMultiGridColumnsDelivery;
                    this.ColumnDiscountsButtonItem = FormMain.Instance.buttonItemMultiGridColumnsDiscounts;
                    this.ColumnPageSizeButtonItem = FormMain.Instance.buttonItemMultiGridColumnsPageSize;
                    this.ColumnSquareButtonItem = FormMain.Instance.buttonItemMultiGridColumnsSquare;
                    this.ColumnDeadlineButtonItem = FormMain.Instance.buttonItemMultiGridColumnsDeadline;
                    this.ColumnReadershipButtonItem = FormMain.Instance.buttonItemMultiGridColumnsReadership;
                    break;
                case GridType.ChronoGrid:
                    if (_selectedOutput == null)
                        this.ShowGridDetails = OutputControls.OutputChronologicalControl.Instance.LocalSchedule.ViewSettings.ShowGridDetails;
                    _selectedOutput = OutputControls.OutputChronologicalControl.Instance;
                    this.HelpButtonItem = FormMain.Instance.buttonItemChronoGridHelp;
                    this.DetailsButtonItem = FormMain.Instance.buttonItemChronoGridDetails;

                    this.ColumnIDButtonItem = FormMain.Instance.buttonItemChronoGridColumnsID;
                    this.ColumnIndexButtonItem = FormMain.Instance.buttonItemChronoGridColumnsIndex;
                    this.ColumnDateButtonItem = FormMain.Instance.buttonItemChronoGridColumnsDate;
                    this.ColumnPCIButtonItem = FormMain.Instance.buttonItemChronoGridColumnsPCI;
                    this.ColumnPercentOfPageButtonItem = FormMain.Instance.buttonItemChronoGridColumnsPercentOfPage;
                    this.ColumnColorButtonItem = FormMain.Instance.buttonItemChronoGridColumnsColor;
                    this.ColumnCostButtonItem = FormMain.Instance.buttonItemChronoGridColumnsCost;
                    this.ColumnSectionButtonItem = FormMain.Instance.buttonItemChronoGridColumnsSection;
                    this.ColumnTotalCostButtonItem = FormMain.Instance.buttonItemChronoGridColumnsTotalCost;
                    this.ColumnPublicationButtonItem = FormMain.Instance.buttonItemChronoGridColumnsPublication;
                    this.ColumnDimensionsButtonItem = FormMain.Instance.buttonItemChronoGridColumnsDimensions;
                    this.ColumnMechanicalsButtonItem = FormMain.Instance.buttonItemChronoGridColumnsMechanicals;
                    this.ColumnDeliveryButtonItem = FormMain.Instance.buttonItemChronoGridColumnsDelivery;
                    this.ColumnDiscountsButtonItem = FormMain.Instance.buttonItemChronoGridColumnsDiscounts;
                    this.ColumnPageSizeButtonItem = FormMain.Instance.buttonItemChronoGridColumnsPageSize;
                    this.ColumnSquareButtonItem = FormMain.Instance.buttonItemChronoGridColumnsSquare;
                    this.ColumnDeadlineButtonItem = FormMain.Instance.buttonItemChronoGridColumnsDeadline;
                    this.ColumnReadershipButtonItem = FormMain.Instance.buttonItemChronoGridColumnsReadership;
                    break;
                default:
                    _selectedOutput = null;
                    break;
            }

            if (_selectedOutput != null)
            {
                UpdateButtonsStateAccordingSelectedOutput();

                if (!pnMain.Controls.Contains(_selectedOutput as Control))
                {
                    Application.DoEvents();
                    pnEmpty.BringToFront();
                    Application.DoEvents();
                    pnMain.Controls.Add(_selectedOutput as Control);
                    Application.DoEvents();
                    pnMain.BringToFront();
                    Application.DoEvents();
                }
                (_selectedOutput as Control).BringToFront();

                if (!xtraTabPageAdNotes.Controls.Contains(_selectedOutput.AdNotes))
                {
                    Application.DoEvents();
                    xtraTabPageAdNotes.Controls.Add(_selectedOutput.AdNotes);
                }
                _selectedOutput.AdNotes.BringToFront();

                if (!xtraTabPageSlideBullets.Controls.Contains(_selectedOutput.SlideBullets))
                {
                    Application.DoEvents();
                    xtraTabPageSlideBullets.Controls.Add(_selectedOutput.SlideBullets);
                }
                _selectedOutput.SlideBullets.BringToFront();

                if (!xtraTabPageSlideHeaders.Controls.Contains(_selectedOutput.SlideHeader))
                {
                    Application.DoEvents();
                    xtraTabPageSlideHeaders.Controls.Add(_selectedOutput.SlideHeader);
                }
                _selectedOutput.SlideHeader.BringToFront();

                FormMain.Instance.superTooltip.SetSuperTooltip(this.HelpButtonItem, _selectedOutput.HelpToolTip);
            }
            else
            {
                pnEmpty.BringToFront();
                FormMain.Instance.superTooltip.SetSuperTooltip(this.HelpButtonItem, null);
            }
        }

        public void UpdateButtonsStateAccordingSelectedOutput()
        {
            if (_selectedOutput != null)
            {
                _selectedOutput.AllowToSave = false;

                this.DetailsButtonItem.Checked = this.ShowGridDetails;

                this.ColumnPercentOfPageButtonItem.Enabled = BusinessClasses.ListManager.Instance.ShareUnits.Count > 0;
                this.ColumnDateButtonItem.Enabled = _selectedOutput.EnableDateButton;
                this.ColumnPublicationButtonItem.Enabled = _selectedOutput.EnablePublicationButton;
                this.ColumnIDButtonItem.Enabled = _selectedOutput.EnableIDButton;
                this.ColumnColorButtonItem.Checked = _selectedOutput.ShowColorHeader;
                this.ColumnCostButtonItem.Checked = _selectedOutput.ShowCostHeader;
                this.ColumnDateButtonItem.Checked = _selectedOutput.ShowDateHeader;
                this.ColumnDeadlineButtonItem.Checked = _selectedOutput.ShowDeadlineHeader;
                this.ColumnDeliveryButtonItem.Checked = _selectedOutput.ShowDeliveryHeader;
                this.ColumnDiscountsButtonItem.Checked = _selectedOutput.ShowDiscountHeader;
                this.ColumnTotalCostButtonItem.Checked = _selectedOutput.ShowFinalCostHeader;
                this.ColumnIDButtonItem.Checked = _selectedOutput.ShowIDHeader;
                this.ColumnIndexButtonItem.Checked = _selectedOutput.ShowIndexHeader;
                this.ColumnMechanicalsButtonItem.Checked = _selectedOutput.ShowMechanicalsHeader;
                this.ColumnPageSizeButtonItem.Checked = _selectedOutput.ShowPageSizeHeader;
                this.ColumnPercentOfPageButtonItem.Checked = _selectedOutput.ShowPercentOfPageHeader & this.ColumnPercentOfPageButtonItem.Enabled;
                this.ColumnPCIButtonItem.Checked = _selectedOutput.ShowPCIHeader;
                this.ColumnPublicationButtonItem.Checked = _selectedOutput.ShowPublicationHeader;
                this.ColumnDimensionsButtonItem.Checked = _selectedOutput.ShowDimensionsHeader;
                this.ColumnReadershipButtonItem.Checked = _selectedOutput.ShowReadershipHeader;
                this.ColumnSectionButtonItem.Checked = _selectedOutput.ShowSectionHeader;
                this.ColumnSquareButtonItem.Checked = _selectedOutput.ShowSquareHeader;

                splitContainerControl.PanelVisibility = this.ShowGridDetails ? DevExpress.XtraEditors.SplitPanelVisibility.Both : DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
                _selectedOutput.AllowToSave = true;
            }
        }

        private bool AllowCheckColumnsButton(bool checkState)
        {
            int count = 0;
            if (_selectedOutput != null)
                count = _selectedOutput.SelectedColumnsCount;
            if (checkState)
                return count > 4;
            else
                return count < 12;
        }

        public void buttonItemGridsColumns_Click(object sender, EventArgs e)
        {
            if (AllowCheckColumnsButton((sender as DevComponents.DotNetBar.ButtonItem).Checked))
                (sender as DevComponents.DotNetBar.ButtonItem).Checked = !(sender as DevComponents.DotNetBar.ButtonItem).Checked;
            else
                AppManager.ShowWarning("You may select between 4 and 12 Columns");
        }

        public void buttonItemGridsColumns_CheckedChanged(object sender, EventArgs e)
        {
            _selectedOutput.SetToggleState();
        }

        public void buttonItemGridsDetails_CheckedChanged(object sender, EventArgs e)
        {
            if (_selectedOutput.AllowToSave)
            {
                this.ShowGridDetails = this.DetailsButtonItem.Checked;
                _selectedOutput.LocalSchedule.ViewSettings.ShowGridDetails = this.ShowGridDetails;
                _selectedOutput.SettingsNotSaved = true;
                splitContainerControl.PanelVisibility = this.DetailsButtonItem.Checked ? DevExpress.XtraEditors.SplitPanelVisibility.Both : DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            }
        }

        public void buttonItemGridsPreview_Click(object sender, EventArgs e)
        {
            _selectedOutput.Preview();
        }

        public void buttonItemGridsPowerPoint_Click(object sender, EventArgs e)
        {
            _selectedOutput.PrintOutput();
        }

        public void buttonItemGridsEmail_Click(object sender, EventArgs e)
        {
            _selectedOutput.Email();
        }

        public void buttonItemGridsSave_Click(object sender, EventArgs e)
        {
            SaveSchedule();
            AppManager.ShowInformation("Schedule Saved");
        }

        public void buttonItemGridsSaveAs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = ConfigurationClasses.SettingsManager.Instance.SaveFolder;
                dialog.Title = "Save Schedule As...";
                dialog.Filter = "Schedule Files|*.xml";
                dialog.FileName = _selectedOutput.LocalSchedule.Name + ".xml";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    SaveSchedule(dialog.FileName.Replace(".xml", ""));
                    AppManager.ShowInformation("Schedule Saved");
                }
            }
        }

        public void buttonItemGridsHelp_Click(object sender, EventArgs e)
        {
            _selectedOutput.OpenHelp();
        }

        private void buttonXAdNotesHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("adnotesnavbar");
        }

        private void buttonXHeadersHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("headersnavbar");
        }

        private void buttonXTotalsHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("totalsnavbar");
        }
    }

    public enum GridType
    {
        DetailedGrid,
        MultiGrid,
        ChronoGrid
    }

    public interface IGridOutputControl : ISettingsContainer
    {
        BusinessClasses.Schedule LocalSchedule { get; set; }
        AdNotesControl AdNotes { get; }
        SlideBulletsControl SlideBullets { get; }
        SlideHeaderControl SlideHeader { get; }

        bool AllowToSave { get; set; }

        DevComponents.DotNetBar.SuperTooltipInfo HelpToolTip { get; }

        bool EnableDateButton { get; }
        bool EnablePublicationButton { get; }
        bool EnableIDButton { get; }

        bool ShowIDHeader { get; set; }
        bool ShowDateHeader { get; set; }
        bool ShowPCIHeader { get; set; }
        bool ShowCostHeader { get; set; }
        bool ShowDiscountHeader { get; set; }
        bool ShowColorHeader { get; set; }
        bool ShowFinalCostHeader { get; set; }
        bool ShowIndexHeader { get; set; }
        bool ShowCommentsHeader { get; set; }
        bool ShowSquareHeader { get; set; }
        bool ShowPageSizeHeader { get; set; }
        bool ShowPercentOfPageHeader { get; set; }
        bool ShowDimensionsHeader { get; set; }
        bool ShowMechanicalsHeader { get; set; }
        bool ShowPublicationHeader { get; set; }
        bool ShowSectionHeader { get; set; }
        bool ShowReadershipHeader { get; set; }
        bool ShowDeliveryHeader { get; set; }
        bool ShowDeadlineHeader { get; set; }

        bool ShowCommentsInPreview { get; set; }
        bool ShowSectionInPreview { get; set; }
        bool ShowMechanicalsInPreview { get; set; }
        bool ShowColumnInchesInPreview { get; set; }
        bool ShowPublicationInPreview { get; set; }
        bool ShowPageSizeInPreview { get; set; }
        bool ShowPercentOfPageInPreview { get; set; }
        bool ShowDimensionsInPreview { get; set; }
        bool ShowReadershipInPreview { get; set; }
        bool ShowDeliveryInPreview { get; set; }
        bool ShowDeadlineInPreview { get; set; }

        int PositionCommentsInPreview { get; set; }
        int PositionSectionInPreview { get; set; }
        int PositionMechanicalsInPreview { get; set; }
        int PositionColumnInchesInPreview { get; set; }
        int PositionPublicationInPreview { get; set; }
        int PositionPageSizeInPreview { get; set; }
        int PositionPercentOfPageInPreview { get; set; }
        int PositionDimensionsInPreview { get; set; }
        int PositionReadershipInPreview { get; set; }
        int PositionDeliveryInPreview { get; set; }
        int PositionDeadlineInPreview { get; set; }

        int SelectedColumnsCount { get; }

        void SetToggleState();
        void SetToggleStateAfterAdNotesChange();
        void SetPreviewState();
        void SetSlideHeader();
        void SaveView();
        void UpdateOutput(bool quickLoad);
        void OpenHelp();
        void PrintOutput();
        void Email();
        void Preview();
    }
}
