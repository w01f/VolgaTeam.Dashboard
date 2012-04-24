using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace NewBizWizForm.TabCalendarForms
{
    [System.ComponentModel.ToolboxItem(false)]
    public partial class CalendarBuilderControl : UserControl
    {
        private CalendarBuilder.BusinessClasses.ShortSchedule[] _calendarList = null;

        private static CalendarBuilderControl _instance;

        private CalendarBuilderControl()
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

        public static CalendarBuilderControl Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CalendarBuilderControl();
                return _instance;
            }
        }

        public void LoadCalendars()
        {
            gridViewCalendars.FocusedRowChanged -= new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewCalendars_FocusedRowChanged);
            OutsideClick();
            _calendarList = CalendarBuilder.AppManager.GetShortScheduleList();
            if (!CalendarBuilder.AppManager.ProgramDataAvailable)
            {
                FormMain.Instance.buttonItemCalendarNew.Enabled = false;
                gridControlCalendars.Visible = false;
                pnNoSlidesWarning.Visible = false;
                pnNoDataWarning.Visible = true;
                gridControlCalendars.DataSource = null;
            }
            //else if (!Directory.Exists(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.CalendarSlideFolder) || Directory.GetDirectories(BusinessClasses.MasterWizardManager.Instance.SelectedWizard.CalendarSlideFolder).Length == 0)
            //{
            //    FormMain.Instance.buttonItemCalendarNew.Enabled = false;
            //    gridControlCalendars.Visible = false;
            //    pnNoSlidesWarning.Visible = true;
            //    laNoSlidesWarningText2.Text = string.Format("{0} {1}", new object[] { BusinessClasses.MasterWizardManager.Instance.SelectedWizard.Name, ConfigurationClasses.SettingsManager.Instance.Size });
            //    pnNoDataWarning.Visible = false;
            //    gridControlCalendars.DataSource = null;
            //}
            else
            {
                FormMain.Instance.buttonItemCalendarNew.Enabled = true;
                gridControlCalendars.Visible = true;
                pnNoSlidesWarning.Visible = false;
                pnNoDataWarning.Visible = false;
                repositoryItemComboBoxStatus.Items.Clear();
                repositoryItemComboBoxStatus.Items.AddRange(CalendarBuilder.BusinessClasses.ListManager.Instance.Statuses);
                gridControlCalendars.DataSource = new BindingList<CalendarBuilder.BusinessClasses.ShortSchedule>(_calendarList);
            }
            gridViewCalendars.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(gridViewCalendars_FocusedRowChanged);
        }

        public void OutsideClick()
        {
            gridViewCalendars.FocusedRowHandle = -1;
            gridViewCalendars.FocusedColumn = gridColumnBusinessName;
            gridViewCalendars.OptionsSelection.EnableAppearanceFocusedRow = false;
            FormMain.Instance.buttonItemCalendarOpen.Enabled = false;
            FormMain.Instance.buttonItemCalendarDelete.Enabled = false;
        }

        public void buttonXNewCalendar_Click(object sender, EventArgs e)
        {
            InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 3, 0, 0);
            FormMain.Instance.Opacity = 0;
            ConfigurationClasses.RegistryHelper.MaximizeMainForm = true;
            CalendarBuilder.FormMain.Instance.Resize -= new EventHandler(FormMain.Instance.FormCalendarResize);
            CalendarBuilder.FormMain.Instance.Resize += new EventHandler(FormMain.Instance.FormCalendarResize);
            CalendarBuilder.AppManager.NewSchedule();
            if (!FormMain.Instance.IsDead)
            {
                FormMain.Instance.Opacity = 1;
                ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                LoadCalendars();
            }
            InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 4, 0, 0);
            AppManager.Instance.ActivateMiniBar();
        }

        public void buttonXOpenCalendar_Click(object sender, EventArgs e)
        {
            InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 3, 0, 0);
            FormMain.Instance.Opacity = 0;
            ConfigurationClasses.RegistryHelper.MaximizeMainForm = true;
            CalendarBuilder.FormMain.Instance.Resize -= new EventHandler(FormMain.Instance.FormCalendarResize);
            CalendarBuilder.FormMain.Instance.Resize += new EventHandler(FormMain.Instance.FormCalendarResize);
            CalendarBuilder.AppManager.OpenSchedule(_calendarList[gridViewCalendars.GetFocusedDataSourceRowIndex()].FullFileName);
            if (!FormMain.Instance.IsDead)
            {
                FormMain.Instance.Opacity = 1;
                ConfigurationClasses.RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
                ConfigurationClasses.RegistryHelper.MaximizeMainForm = false;
                LoadCalendars();
            }
            InteropClasses.WinAPIHelper.PostMessage(ConfigurationClasses.RegistryHelper.MinibarHandle, InteropClasses.WinAPIHelper.WM_APP + 4, 0, 0);
            AppManager.Instance.ActivateMiniBar();
        }

        public void buttonXDeleteCalendar_Click(object sender, EventArgs e)
        {
            if (AppManager.Instance.ShowWarningQuestion("Delete this Schedule?") == DialogResult.Yes)
            {
                string fileName = _calendarList[gridViewCalendars.GetFocusedDataSourceRowIndex()].FullFileName;
                try
                {
                    if (File.Exists(fileName))
                        File.Delete(fileName);
                }
                catch
                {
                    AppManager.Instance.ShowWarning("Couldn't delete selected schedule.");
                }
                LoadCalendars();
            }
        }

        private void gridViewCalendars_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gridViewCalendars.OptionsSelection.EnableAppearanceFocusedRow = true;
            FormMain.Instance.buttonItemCalendarOpen.Enabled = gridViewCalendars.SelectedRowsCount > 0;
            FormMain.Instance.buttonItemCalendarDelete.Enabled = gridViewCalendars.SelectedRowsCount > 0;
        }

        private void gridViewCalendars_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            gridViewCalendars_FocusedRowChanged(null, null);
            if (e.Clicks == 2)
            {
                buttonXOpenCalendar_Click(null, null);
            }
        }

        private void gridControlCalendars_Click(object sender, EventArgs e)
        {
            OutsideClick();
        }

        private void gridViewCalendars_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            CalendarBuilder.BusinessClasses.ShortSchedule schedule = _calendarList[gridViewCalendars.GetDataSourceRowIndex(e.RowHandle)];
            schedule.Save();
        }

        private void repositoryItemComboBoxStatus_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            gridViewCalendars.CloseEditor();
        }
    }
}
