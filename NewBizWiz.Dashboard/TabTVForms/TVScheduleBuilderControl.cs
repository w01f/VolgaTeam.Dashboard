using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.Dashboard.TabTVForms
{
	[ToolboxItem(false)]
	public partial class TVScheduleBuilderControl : UserControl
	{
		private static TVScheduleBuilderControl _instance;
		private ShortSchedule[] _scheduleList;

		private TVScheduleBuilderControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			AppManager.Instance.SetClickEventHandler(this);
			if (!((CreateGraphics()).DpiX > 96)) return;
			laNoDataWarning.Font = new Font(laNoDataWarning.Font.Name, laNoDataWarning.Font.Size - 5, laNoDataWarning.Font.Style);
			laNoSlidesWarningText1.Font = new Font(laNoSlidesWarningText1.Font.Name, laNoSlidesWarningText1.Font.Size - 3, laNoSlidesWarningText1.Font.Style);
			laNoSlidesWarningText2.Font = new Font(laNoSlidesWarningText2.Font.Name, laNoSlidesWarningText2.Font.Size - 3, laNoSlidesWarningText2.Font.Style);
			laNoSlidesWarningText3.Font = new Font(laNoSlidesWarningText3.Font.Name, laNoSlidesWarningText3.Font.Size - 3, laNoSlidesWarningText3.Font.Style);
		}

		public static TVScheduleBuilderControl Instance
		{
			get
			{
				if (_instance == null)
					_instance = new TVScheduleBuilderControl();
				return _instance;
			}
		}

		public void LoadSchedules()
		{
			MediaMetaData.Instance.Init<TVSettingsManager, TVListManager>(MediaDataType.TV);
			gridViewSchedules.FocusedRowChanged -= gridViewSchedules_FocusedRowChanged;
			OutsideClick();
			_scheduleList = MediaSchedule.Internal.AppManager.GetShortScheduleList();
			if (!MediaSchedule.Internal.AppManager.ProgramDataAvailable)
			{
				FormMain.Instance.buttonItemTVNew.Enabled = false;
				gridControlSchedules.Visible = false;
				pnNoSlidesWarning.Visible = false;
				pnNoDataWarning.Visible = true;
				gridControlSchedules.DataSource = null;
			}
			else if (!Directory.Exists(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder) || Directory.GetDirectories(MasterWizardManager.Instance.SelectedWizard.TVScheduleSlideFolder).Length == 0)
			{
				FormMain.Instance.buttonItemTVNew.Enabled = false;
				gridControlSchedules.Visible = false;
				pnNoSlidesWarning.Visible = true;
				laNoSlidesWarningText2.Text = string.Format("{0} {1}", new object[] {MasterWizardManager.Instance.SelectedWizard.Name, SettingsManager.Instance.Size});
				pnNoDataWarning.Visible = false;
				gridControlSchedules.DataSource = null;
			}
			else
			{
				FormMain.Instance.buttonItemTVNew.Enabled = true;
				gridControlSchedules.Visible = true;
				pnNoSlidesWarning.Visible = false;
				pnNoDataWarning.Visible = false;
				repositoryItemComboBoxStatus.Items.Clear();
				repositoryItemComboBoxStatus.Items.AddRange(MediaMetaData.Instance.ListManager.Statuses);
				gridControlSchedules.DataSource = new BindingList<ShortSchedule>(_scheduleList);
			}
			gridViewSchedules.FocusedRowChanged += gridViewSchedules_FocusedRowChanged;
		}

		public void OutsideClick()
		{
			gridViewSchedules.FocusedRowHandle = -1;
			gridViewSchedules.FocusedColumn = gridColumnBusinessName;
			gridViewSchedules.OptionsSelection.EnableAppearanceFocusedRow = false;
			FormMain.Instance.buttonItemTVOpen.Enabled = false;
			FormMain.Instance.buttonItemTVDelete.Enabled = false;
		}

		public void buttonXNewSchedule_Click(object sender, EventArgs e)
		{
			FormMain.Instance.Opacity = 0;
			RegistryHelper.MaximizeMainForm = true;
			MediaSchedule.Internal.FormMain.Instance.Resize -= FormMain.Instance.FormScheduleResize;
			MediaSchedule.Internal.FormMain.Instance.Resize += FormMain.Instance.FormScheduleResize;
			MediaSchedule.Internal.FormMain.Instance.FloaterRequested -= FormMain.Instance.buttonItemFloater_Click;
			MediaSchedule.Internal.FormMain.Instance.FloaterRequested += FormMain.Instance.buttonItemFloater_Click;
			MediaSchedule.Internal.AppManager.NewSchedule();
			if (FormMain.Instance.IsDead) return;
			FormMain.Instance.Opacity = 1;
			RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
			RegistryHelper.MaximizeMainForm = false;
			LoadSchedules();
		}

		public void buttonXOpenSchedule_Click(object sender, EventArgs e)
		{
			FormMain.Instance.Opacity = 0;
			RegistryHelper.MaximizeMainForm = true;
			MediaSchedule.Internal.FormMain.Instance.Resize -= FormMain.Instance.FormScheduleResize;
			MediaSchedule.Internal.FormMain.Instance.Resize += FormMain.Instance.FormScheduleResize;
			MediaSchedule.Internal.FormMain.Instance.FloaterRequested -= FormMain.Instance.buttonItemFloater_Click;
			MediaSchedule.Internal.FormMain.Instance.FloaterRequested += FormMain.Instance.buttonItemFloater_Click;
			MediaSchedule.Internal.AppManager.OpenSchedule(_scheduleList[gridViewSchedules.GetFocusedDataSourceRowIndex()].FullFileName);
			if (FormMain.Instance.IsDead) return;
			FormMain.Instance.Opacity = 1;
			RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
			RegistryHelper.MaximizeMainForm = false;
			LoadSchedules();
		}

		public void buttonXDeleteSchedule_Click(object sender, EventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Delete this TV Schedule?") != DialogResult.Yes) return;
			var fileName = _scheduleList[gridViewSchedules.GetFocusedDataSourceRowIndex()].FullFileName;
			try
			{
				if (File.Exists(fileName))
					File.Delete(fileName);
			}
			catch
			{
				Utilities.Instance.ShowWarning("Couldn't delete selected schedule.");
			}
			LoadSchedules();
		}

		private void gridViewSchedules_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
		{
			gridViewSchedules.OptionsSelection.EnableAppearanceFocusedRow = true;
			FormMain.Instance.buttonItemTVOpen.Enabled = gridViewSchedules.SelectedRowsCount > 0;
			FormMain.Instance.buttonItemTVDelete.Enabled = gridViewSchedules.SelectedRowsCount > 0;
		}

		private void gridViewSchedules_RowClick(object sender, RowClickEventArgs e)
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

		private void gridViewSchedules_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			var schedule = _scheduleList[gridViewSchedules.GetDataSourceRowIndex(e.RowHandle)];
			schedule.Save();
		}

		private void repositoryItemComboBoxStatus_Closed(object sender, ClosedEventArgs e)
		{
			gridViewSchedules.CloseEditor();
		}
	}
}