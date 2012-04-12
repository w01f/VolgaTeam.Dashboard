using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace NewBizWizForm.TabMobileForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class MobileScheduleBuilderControl : UserControl
    {
        private MobileScheduleBuilder.BusinessClasses.ShortSchedule[] _scheduleList = null;

        private static MobileScheduleBuilderControl _instance;

        private MobileScheduleBuilderControl()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            AppManager.Instance.SetClickEventHandler(this);
            if ((base.CreateGraphics()).DpiX > 96)
            {
                laTitle.Font = new System.Drawing.Font(laTitle.Font.Name, laTitle.Font.Size - 5, laTitle.Font.Style);
                laNoDataWarning.Font = new System.Drawing.Font(laNoDataWarning.Font.Name, laNoDataWarning.Font.Size - 5, laNoDataWarning.Font.Style);
                laNoSlidesWarningText1.Font = new System.Drawing.Font(laNoSlidesWarningText1.Font.Name, laNoSlidesWarningText1.Font.Size - 3, laNoSlidesWarningText1.Font.Style);
                laNoSlidesWarningText2.Font = new System.Drawing.Font(laNoSlidesWarningText2.Font.Name, laNoSlidesWarningText2.Font.Size - 3, laNoSlidesWarningText2.Font.Style);
                laNoSlidesWarningText3.Font = new System.Drawing.Font(laNoSlidesWarningText3.Font.Name, laNoSlidesWarningText3.Font.Size - 3, laNoSlidesWarningText3.Font.Style);
            }
        }

        public static MobileScheduleBuilderControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MobileScheduleBuilderControl();
                return _instance;
            }
        }

        public void LoadSchedules()
        {
            gridViewSchedules.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewSchedules_FocusedRowChanged);
            OutsideClick();
            _scheduleList = MobileScheduleBuilder.AppManager.GetShortScheduleList();
            if (!MobileScheduleBuilder.AppManager.ProgramDataAvailable)
            {
                FormMain.Instance.buttonItemMobileNew.Enabled = false;
                gridControlSchedules.Visible = false;
                pnNoSlidesWarning.Visible = false;
                pnNoDataWarning.Visible = true;
                gridControlSchedules.DataSource = null;
            }
            else if (!Directory.Exists(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.MobileScheduleSlideFolder) || Directory.GetDirectories(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.MobileScheduleSlideFolder).Length == 0)
            {
                FormMain.Instance.buttonItemMobileNew.Enabled = false;
                gridControlSchedules.Visible = false;
                pnNoSlidesWarning.Visible = true;
                laNoSlidesWarningText2.Text = string.Format("{0} {1}", new object[] { BusinessClasses.MasterWizardManager.Instance.SelectedWizard.Name, ConfigurationClasses.SettingsManager.Instance.Size });
                pnNoDataWarning.Visible = false;
                gridControlSchedules.DataSource = null;
            }
            else
            {
                FormMain.Instance.buttonItemMobileNew.Enabled = true;
                gridControlSchedules.Visible = true;
                pnNoSlidesWarning.Visible = false;
                pnNoDataWarning.Visible = false;
                repositoryItemComboBoxStatus.Items.Clear();
                repositoryItemComboBoxStatus.Items.AddRange(MobileScheduleBuilder.BusinessClasses.ListManager.Instance.Statuses);
                gridControlSchedules.DataSource = new BindingList<MobileScheduleBuilder.BusinessClasses.ShortSchedule>(_scheduleList);
            }
            gridViewSchedules.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewSchedules_FocusedRowChanged);
        }

        public void OutsideClick()
        {
            gridViewSchedules.FocusedRowHandle = -1;
            gridViewSchedules.FocusedColumn = gridColumnBusinessName;
            gridViewSchedules.OptionsSelection.EnableAppearanceFocusedRow = false;
            FormMain.Instance.buttonItemMobileOpen.Enabled = false;
            FormMain.Instance.buttonItemMobileDelete.Enabled = false;
        }

        public void buttonXNewSchedule_Click(object sender, EventArgs e)
        {
            InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 3, 0, 0);
            FormMain.Instance.Opacity = 0;
            ConfigurationClasses.RegistryHelper.MaximizeMainForm = true;
            MobileScheduleBuilder.FormMain.Instance.Resize -= new EventHandler(FormMain.Instance.FormMobileScheduleResize);
            MobileScheduleBuilder.FormMain.Instance.Resize += new EventHandler(FormMain.Instance.FormMobileScheduleResize);
            MobileScheduleBuilder.AppManager.NewSchedule();
            if (!FormMain.Instance.IsDead)
            {
                FormMain.Instance.Opacity = 1;
                ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                LoadSchedules();
            }
            InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 4, 0, 0);
            AppManager.Instance.ActivateMiniBar();
        }

        public void buttonXOpenSchedule_Click(object sender, EventArgs e)
        {
            InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 3, 0, 0);
            FormMain.Instance.Opacity = 0;
            ConfigurationClasses.RegistryHelper.MaximizeMainForm = true;
            MobileScheduleBuilder.FormMain.Instance.Resize -= new EventHandler(FormMain.Instance.FormMobileScheduleResize);
            MobileScheduleBuilder.FormMain.Instance.Resize += new EventHandler(FormMain.Instance.FormMobileScheduleResize);
            MobileScheduleBuilder.AppManager.OpenSchedule(_scheduleList[gridViewSchedules.GetFocusedDataSourceRowIndex()].FullFileName);
            if (!FormMain.Instance.IsDead)
            {
                FormMain.Instance.Opacity = 1;
                ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                LoadSchedules();
            }
            InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 4, 0, 0);
            AppManager.Instance.ActivateMiniBar();
        }

        public void buttonXDeleteSchedule_Click(object sender, EventArgs e)
        {
            if (AppManager.Instance.ShowWarningQuestion("Delete this Schedule?") == DialogResult.Yes)
            {
                string fileName = _scheduleList[gridViewSchedules.GetFocusedDataSourceRowIndex()].FullFileName;
                try
                {
                    if (File.Exists(fileName))
                        File.Delete(fileName);
                }
                catch
                {
                    AppManager.Instance.ShowWarning("Couldn't delete selected schedule.");
                }
                LoadSchedules();
            }
        }

        private void gridViewSchedules_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridViewSchedules.OptionsSelection.EnableAppearanceFocusedRow = true;
            FormMain.Instance.buttonItemMobileOpen.Enabled = gridViewSchedules.SelectedRowsCount > 0;
            FormMain.Instance.buttonItemMobileDelete.Enabled = gridViewSchedules.SelectedRowsCount > 0;
        }

        private void gridViewSchedules_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            gridViewSchedules_FocusedRowChanged(null, null);
            if (e.Clicks == 2)
            {
                buttonXOpenSchedule_Click(null, null);
            }
        }

        private void gridControlSchedules_Click(object sender, EventArgs e)
        {
            OutsideClick();
        }

        private void gridViewSchedules_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            MobileScheduleBuilder.BusinessClasses.ShortSchedule schedule = _scheduleList[gridViewSchedules.GetDataSourceRowIndex(e.RowHandle)];
            schedule.Save();
        }

        private void repositoryItemComboBoxStatus_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            gridViewSchedules.CloseEditor();
        }
    }
}
