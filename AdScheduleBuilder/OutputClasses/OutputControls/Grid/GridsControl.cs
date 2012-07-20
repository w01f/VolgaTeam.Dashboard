using System;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class GridsControl : UserControl
    {
        private static GridsControl _instance;
        private IGridOutputControl _selectedOutput = null;
        private string _infoHelpKey = string.Empty;
        private DevComponents.DotNetBar.SuperTooltipInfo _infoTabSlideHeaderHelpTooltip = new DevComponents.DotNetBar.SuperTooltipInfo("Slide Headers Help", "", "Learn more about adding Slide Header information to your schedule slides", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);
        private DevComponents.DotNetBar.SuperTooltipInfo _infoTabSlideBulletsHelpTooltip = new DevComponents.DotNetBar.SuperTooltipInfo("Slide Totals Help", "", "Learn more about adding financial info and schedule totals to your schedule slides", null, null, DevComponents.DotNetBar.eTooltipColor.Gray);

        #region Operation Buttons
        public DevComponents.DotNetBar.ButtonItem HelpButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem OptionsButtonItem { get; set; }
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
                    _selectedOutput = OutputControls.OutputDetailedGridControl.Instance;
                    this.HelpButtonItem = FormMain.Instance.buttonItemDetailedGridHelp;
                    this.OptionsButtonItem = FormMain.Instance.buttonItemDetailedGridDetails;
                    break;
                case GridType.MultiGrid:
                    _selectedOutput = OutputControls.OutputMultiGridControl.Instance;
                    this.HelpButtonItem = FormMain.Instance.buttonItemMultiGridHelp;
                    this.OptionsButtonItem = FormMain.Instance.buttonItemMultiGridDetails;
                    break;
                case GridType.ChronoGrid:
                    _selectedOutput = OutputControls.OutputChronologicalControl.Instance;
                    this.HelpButtonItem = FormMain.Instance.buttonItemChronoGridHelp;
                    this.OptionsButtonItem = FormMain.Instance.buttonItemChronoGridDetails;
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

                if (!xtraTabPagePrint.Controls.Contains(_selectedOutput.PrintColumns))
                {
                    Application.DoEvents();
                    xtraTabPagePrint.Controls.Add(_selectedOutput.PrintColumns);
                }
                _selectedOutput.PrintColumns.BringToFront();

                if (!xtraTabPageAdNotes.Controls.Contains(_selectedOutput.AdNotes))
                {
                    Application.DoEvents();
                    xtraTabPageAdNotes.Controls.Add(_selectedOutput.AdNotes);
                }
                _selectedOutput.AdNotes.BringToFront();

                if (!pnSlideInfoBody.Controls.Contains(_selectedOutput.SlideBullets))
                {
                    Application.DoEvents();
                    pnSlideInfoBody.Controls.Add(_selectedOutput.SlideBullets);
                }
                if (!pnSlideInfoBody.Controls.Contains(_selectedOutput.SlideHeader))
                {
                    Application.DoEvents();
                    pnSlideInfoBody.Controls.Add(_selectedOutput.SlideHeader);
                }
                UpdateInfoTab();

                FormMain.Instance.superTooltip.SetSuperTooltip(this.HelpButtonItem, _selectedOutput.HelpToolTip);
            }
            else
            {
                pnEmpty.BringToFront();
                FormMain.Instance.superTooltip.SetSuperTooltip(this.HelpButtonItem, null);
            }
        }

        private void UpdateButtonsStateAccordingSelectedOutput()
        {
            if (_selectedOutput != null)
            {
                _selectedOutput.AllowToSave = false;
                this.OptionsButtonItem.Checked = _selectedOutput.ShowOptions;
                xtraTabControlOptions.SelectedTabPageIndex = _selectedOutput.SelectedOptionChapterIndex;
                _selectedOutput.AllowToSave = true;

                splitContainerControl.PanelVisibility = _selectedOutput.ShowOptions ? DevExpress.XtraEditors.SplitPanelVisibility.Both : DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
            }
        }

        public void buttonItemGridsDetails_CheckedChanged(object sender, EventArgs e)
        {
            if (_selectedOutput.AllowToSave)
            {
                _selectedOutput.ShowOptions = this.OptionsButtonItem.Checked;
                _selectedOutput.SaveView();
                splitContainerControl.PanelVisibility = _selectedOutput.ShowOptions ? DevExpress.XtraEditors.SplitPanelVisibility.Both : DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
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

        #region Options Stuff
        private void xtraTabControlDetails_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (_selectedOutput.AllowToSave)
            {
                _selectedOutput.SelectedOptionChapterIndex = xtraTabControlOptions.SelectedTabPageIndex;
                _selectedOutput.SaveView();
            }
        }

        private void UpdateInfoTab()
        {
            if (buttonXSlideBullets.Checked)
            {
                _selectedOutput.SlideBullets.BringToFront();
                _infoHelpKey = "totalsnavbar";
                superTooltip.SetSuperTooltip(pbInfoHelp, _infoTabSlideBulletsHelpTooltip);
            }
            else if (buttonXSlideHeaders.Checked)
            {
                _selectedOutput.SlideHeader.BringToFront();
                _infoHelpKey = "headersnavbar";
                superTooltip.SetSuperTooltip(pbInfoHelp, _infoTabSlideHeaderHelpTooltip);
            }
        }

        private void buttonXSlideInfoSelector_Click(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.ButtonX button = sender as DevComponents.DotNetBar.ButtonX;
            if (button != null && !button.Checked)
            {
                buttonXSlideBullets.Checked = false;
                buttonXSlideHeaders.Checked = false;
            }
            button.Checked = true;
        }

        private void buttonXSlideHeaders_CheckedChanged(object sender, EventArgs e)
        {
            UpdateInfoTab();
        }

        private void InfoHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink(_infoHelpKey);
        }
        #endregion

        #region Picture Box Clicks Habdlers
        /// <summary>
        /// Buttonize the PictureBox 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)(sender);
            pic.Top += 1;
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)(sender);
            pic.Top -= 1;
        }
        #endregion
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
        PrintControl PrintColumns { get; }
        AdNotesControl AdNotes { get; }
        SlideBulletsControl SlideBullets { get; }
        SlideHeaderControl SlideHeader { get; }

        bool AllowToSave { get; set; }

        DevComponents.DotNetBar.SuperTooltipInfo HelpToolTip { get; }

        bool EnableDateButton { get; }
        bool EnablePublicationButton { get; }
        bool EnableIDButton { get; }

        bool ShowOptions { get; set; }
        int SelectedOptionChapterIndex { get; set; }

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

        void UpdateColumnsAccordingToggles();
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
