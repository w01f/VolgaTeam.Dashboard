using System;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class GridsControl : UserControl
    {
        private static GridsControl _instance;
        private IGridOutputControl _selectedOutput = null;

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
                    if (AppManager.ShowWarningQuestion("Your Schedule settings have changed.\nDo you want to save changes?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        SaveSchedule();
                        result = true;
                    }
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

        public void UpdateNavBarView()
        {
            navBarControlDetails.View = new CustomNavPaneViewInfoRegistrator();
        }

        private void UncheckOutputOptions()
        {
            FormMain.Instance.buttonItemGridsDetailedGrid.Checked = false;
            FormMain.Instance.buttonItemGridsMultiGrid.Checked = false;
            FormMain.Instance.buttonItemGridsChronological.Checked = false;
        }

        public void UpdateButtonsStateAccordingSelectedOutput()
        {
            if (_selectedOutput != null)
            {
                _selectedOutput.AllowToSave = false;
                FormMain.Instance.buttonItemGridsDetails.Checked = FormMain.Instance.buttonItemGridsDetails.Checked | _selectedOutput.ShowGridDetails;

                FormMain.Instance.buttonItemGridsColumnsPercentOfPage.Enabled = BusinessClasses.ListManager.Instance.ShareUnits.Count > 0;
                FormMain.Instance.buttonItemGridsColumnsDate.Enabled = _selectedOutput.EnableDateButton;
                FormMain.Instance.buttonItemGridsColumnsPublication.Enabled = _selectedOutput.EnablePublicationButton;
                FormMain.Instance.buttonItemGridsColumnsID.Enabled = _selectedOutput.EnableIDButton;
                FormMain.Instance.buttonItemGridsColumnsColor.Checked = _selectedOutput.ShowColorHeader;
                FormMain.Instance.buttonItemGridsColumnsCost.Checked = _selectedOutput.ShowCostHeader;
                FormMain.Instance.buttonItemGridsColumnsDate.Checked = _selectedOutput.ShowDateHeader;
                FormMain.Instance.buttonItemGridsColumnsDeadline.Checked = _selectedOutput.ShowDeadlineHeader;
                FormMain.Instance.buttonItemGridsColumnsDelivery.Checked = _selectedOutput.ShowDeliveryHeader;
                FormMain.Instance.buttonItemGridsColumnsDiscounts.Checked = _selectedOutput.ShowDiscountHeader;
                FormMain.Instance.buttonItemGridsColumnsFinalCost.Checked = _selectedOutput.ShowFinalCostHeader;
                FormMain.Instance.buttonItemGridsColumnsID.Checked = _selectedOutput.ShowIDHeader;
                FormMain.Instance.buttonItemGridsColumnsIndex.Checked = _selectedOutput.ShowIndexHeader;
                FormMain.Instance.buttonItemGridsColumnsMechanicals.Checked = _selectedOutput.ShowMechanicalsHeader;
                FormMain.Instance.buttonItemGridsColumnsPageSize.Checked = _selectedOutput.ShowPageSizeHeader;
                FormMain.Instance.buttonItemGridsColumnsPercentOfPage.Checked = _selectedOutput.ShowPercentOfPageHeader & FormMain.Instance.buttonItemGridsColumnsPercentOfPage.Enabled;
                FormMain.Instance.buttonItemGridsColumnsPCI.Checked = _selectedOutput.ShowPCIHeader;
                FormMain.Instance.buttonItemGridsColumnsPublication.Checked = _selectedOutput.ShowPublicationHeader;
                FormMain.Instance.buttonItemGridsColumnsDimensions.Checked = _selectedOutput.ShowDimensionsHeader;
                FormMain.Instance.buttonItemGridsColumnsReadership.Checked = _selectedOutput.ShowReadershipHeader;
                FormMain.Instance.buttonItemGridsColumnsSection.Checked = _selectedOutput.ShowSectionHeader;
                FormMain.Instance.buttonItemGridsColumnsSquare.Checked = _selectedOutput.ShowSquareHeader;
                _selectedOutput.AllowToSave = true;
            }
        }

        public void UpdatePageAccordingToggledButton()
        {
            _selectedOutput = null;
            if (FormMain.Instance.buttonItemGridsDetailedGrid != null && FormMain.Instance.buttonItemGridsDetailedGrid.Checked)
            {
                _selectedOutput = OutputControls.OutputDetailedGridControl.Instance;
                UpdateButtonsStateAccordingSelectedOutput();
            }
            else if (FormMain.Instance.buttonItemGridsMultiGrid != null && FormMain.Instance.buttonItemGridsMultiGrid.Checked)
            {
                _selectedOutput = OutputControls.OutputMultiGridControl.Instance;
                UpdateButtonsStateAccordingSelectedOutput();
            }
            else if (FormMain.Instance.buttonItemGridsChronological != null && FormMain.Instance.buttonItemGridsChronological.Checked)
            {
                _selectedOutput = OutputControls.OutputChronologicalControl.Instance;
                UpdateButtonsStateAccordingSelectedOutput();
            }
            if (_selectedOutput != null)
            {
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

                if (!navBarGroupControlContainerAdNotes.Controls.Contains(_selectedOutput.AdNotes))
                {
                    Application.DoEvents();
                    navBarGroupControlContainerAdNotes.Controls.Add(_selectedOutput.AdNotes);
                }
                _selectedOutput.AdNotes.BringToFront();

                if (!navBarGroupControlContainerSlideBulltes.Controls.Contains(_selectedOutput.SlideBullets))
                {
                    Application.DoEvents();
                    navBarGroupControlContainerSlideBulltes.Controls.Add(_selectedOutput.SlideBullets);
                }
                _selectedOutput.SlideBullets.BringToFront();

                if (!navBarGroupControlContainerSlideHeader.Controls.Contains(_selectedOutput.SlideHeader))
                {
                    Application.DoEvents();
                    navBarGroupControlContainerSlideHeader.Controls.Add(_selectedOutput.SlideHeader);
                }
                _selectedOutput.SlideHeader.BringToFront();
            }
            FormMain.Instance.superTooltip.SetSuperTooltip(FormMain.Instance.buttonItemGridsHelp, _selectedOutput.HelpToolTip);
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

        public void buttonItemGridsDetailedGrid_Click(object sender, EventArgs e)
        {
            if (!(sender as DevComponents.DotNetBar.ButtonItem).Checked)
                if (this.AllowToLeaveControl)
                {
                    UncheckOutputOptions();
                    FormMain.Instance.buttonItemGridsDetailedGrid.Checked = true;
                    UpdatePageAccordingToggledButton();
                }
        }

        public void buttonItemGridsMultiGrid_Click(object sender, EventArgs e)
        {
            if (!(sender as DevComponents.DotNetBar.ButtonItem).Checked)
                if (this.AllowToLeaveControl)
                {
                    UncheckOutputOptions();
                    FormMain.Instance.buttonItemGridsMultiGrid.Checked = true;
                    UpdatePageAccordingToggledButton();
                }
        }

        public void buttonItemGridsChronological_Click(object sender, EventArgs e)
        {
            if (!(sender as DevComponents.DotNetBar.ButtonItem).Checked)
                if (this.AllowToLeaveControl)
                {
                    UncheckOutputOptions();
                    FormMain.Instance.buttonItemGridsChronological.Checked = true;
                    UpdatePageAccordingToggledButton();
                }
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
            _selectedOutput.SetToggleState();
            UpdateNavBarView();
            splitContainerControl.PanelVisibility = FormMain.Instance.buttonItemGridsDetails.Checked ? DevExpress.XtraEditors.SplitPanelVisibility.Both : DevExpress.XtraEditors.SplitPanelVisibility.Panel2;
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

        bool ShowGridDetails { get; set; }

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
    }
}
