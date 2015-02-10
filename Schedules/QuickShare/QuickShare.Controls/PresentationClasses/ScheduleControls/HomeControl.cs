using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.Core.QuickShare;
using NewBizWiz.QuickShare.Controls.BusinessClasses;

namespace NewBizWiz.QuickShare.Controls.PresentationClasses.ScheduleControls
{
	[ToolboxItem(false)]
	public partial class HomeControl : UserControl
	{
		private bool _allowToSave;
		private Package _localPackage;
		private SchedulePageManager _schedulePageManager;

		public HomeControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			if ((base.CreateGraphics()).DpiX > 96)
			{
			}
		}

		public bool SettingsNotSaved { get; set; }

		public bool AllowToLeaveControl(bool quiet = false)
		{
			bool result = true;
			if (_schedulePageManager != null)
				_schedulePageManager.SchedulePages.Where(sp => sp.Section != null).ToList().ForEach(sp => result &= sp.Section.AllowToLeaveControl);
			if (SettingsNotSaved)
				result = SaveSchedule(quiet);
			return result;
		}

		private void UpdateControl()
		{
			var enableSchedules = Controller.Instance.HomeBusinessName.EditValue != null &
								   Controller.Instance.HomeDecisionMaker.EditValue != null &
								   Controller.Instance.HomeClientType.EditValue != null &
								   Controller.Instance.HomePresentationDate.EditValue != null;
			xtraTabControl.Enabled = enableSchedules;
		}

		public void LoadPackage()
		{
			_allowToSave = false;
			_localPackage = BusinessWrapper.Instance.PackageManager.GetLocalPackage();

			Controller.Instance.HomeClientType.Properties.Items.Clear();
			Controller.Instance.HomeClientType.Properties.Items.AddRange(MediaMetaData.Instance.ListManager.ClientTypes.ToArray());

			Controller.Instance.HomeBusinessName.EditValue = !String.IsNullOrEmpty(_localPackage.BusinessName) ? _localPackage.BusinessName : null;
			Controller.Instance.HomeDecisionMaker.EditValue = !String.IsNullOrEmpty(_localPackage.DecisionMaker) ? _localPackage.DecisionMaker : null;
			if (!string.IsNullOrEmpty(_localPackage.ClientType))
				Controller.Instance.HomeClientType.SelectedIndex = Controller.Instance.HomeClientType.Properties.Items.IndexOf(_localPackage.ClientType);
			Controller.Instance.HomeAccountNumberCheck.Checked = !String.IsNullOrEmpty(_localPackage.AccountNumber);
			Controller.Instance.HomeAccountNumberText.EditValue = _localPackage.AccountNumber;
			Controller.Instance.HomePresentationDate.EditValue = _localPackage.PresentationDate;
			Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = _localPackage.StartDayOfWeek;

			UpdateControl();

			if (_schedulePageManager != null)
				_schedulePageManager.Dispose();
			_schedulePageManager = new SchedulePageManager(_localPackage);
			_schedulePageManager.RebuildTabPages();

			UpdateScheduleList();

			SettingsNotSaved = false;
			_allowToSave = true;
		}

		private bool SaveSchedule(bool quiet = false, string scheduleName = "")
		{
			advBandedGridViewSchedules.CloseEditor();
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			if (nameChanged)
				_localPackage.Name = scheduleName;
			var businessName = Controller.Instance.HomeBusinessName.EditValue as String;
			if (!String.IsNullOrEmpty(businessName))
			{
				if (_localPackage.BusinessName != businessName)
				{
					_localPackage.BusinessName = businessName;
					BusinessWrapper.Instance.ActivityManager.AddActivity(new PropertyEditActivity("Business Name", businessName));
				}
			}
			else
			{
				if (!quiet)
					Utilities.Instance.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Business Name before you proceed.");
				return false;
			}
			var decisionMaker = Controller.Instance.HomeDecisionMaker.EditValue as String;
			if (!String.IsNullOrEmpty(decisionMaker))
			{
				if (_localPackage.DecisionMaker != decisionMaker)
				{
					_localPackage.DecisionMaker = decisionMaker;
					BusinessWrapper.Instance.ActivityManager.AddActivity(new PropertyEditActivity("Decision Maker", decisionMaker));
				}
			}
			else
			{
				if (!quiet)
					Utilities.Instance.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Owner/Decision-maker before you proceed.");
				return false;
			}

			if (Controller.Instance.HomeClientType.EditValue != null)
				_localPackage.ClientType = Controller.Instance.HomeClientType.EditValue.ToString();
			else
			{
				if (!quiet)
					Utilities.Instance.ShowWarning("You must set Client type before save");
				return false;
			}

			if (Controller.Instance.HomeAccountNumberCheck.Checked && Controller.Instance.HomeAccountNumberText.EditValue != null)
				_localPackage.AccountNumber = Controller.Instance.HomeAccountNumberText.EditValue.ToString();
			else
				_localPackage.AccountNumber = string.Empty;

			if (Controller.Instance.HomePresentationDate.EditValue != null)
				_localPackage.PresentationDate = Controller.Instance.HomePresentationDate.DateTime;
			else
			{
				if (!quiet)
					Utilities.Instance.ShowWarning("Your schedule is missing important information!\nPlease make sure you have a Presentation Date before you proceed.");
				return false;
			}

			foreach (var page in xtraTabControl.TabPages.OfType<ScheduleSettingsControl>())
				page.SaveData();

			UpdateControl();
			Controller.Instance.SavePackage(_localPackage, nameChanged, true, this);
			SettingsNotSaved = false;
			return true;
		}

		private void UpdateScheduleList()
		{
			gridControlSchedules.DataSource = _localPackage.Schedules;
			advBandedGridViewSchedules.RefreshData();
			UpdateScheduleSettings();
		}

		private void UpdateScheduleSettings()
		{
			foreach (var schedule in _localPackage.Schedules)
			{
				var settingsPage = xtraTabControl.TabPages.OfType<ScheduleSettingsControl>().FirstOrDefault(sp => sp.Schedule.Id == schedule.Id);
				var fullUpdate = false;
				if (settingsPage == null)
				{
					settingsPage = new ScheduleSettingsControl(schedule);
					settingsPage.Changed += (o, e) =>
					{
						SettingsNotSaved = true;
					};
					xtraTabControl.TabPages.Add(settingsPage);
					fullUpdate = true;
				}
				settingsPage.UpdateData(fullUpdate);
			}
			var obsoletePages = xtraTabControl.TabPages.OfType<ScheduleSettingsControl>().Where(sp => _localPackage.Schedules.All(s => s.Id != sp.Schedule.Id)).ToList();
			foreach (var page in obsoletePages)
				xtraTabControl.TabPages.Remove(page);
		}

		#region Editors Events
		public void SchedulePropertyEditValueChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;
			UpdateControl();
			SettingsNotSaved = true;
		}

		public void checkBoxItemAccountNumber_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
		{
			Controller.Instance.HomeAccountNumberText.Enabled = Controller.Instance.HomeAccountNumberCheck.Checked;
			SchedulePropertyEditValueChanged(null, null);
		}

		public void SchedulePropertiesEditor_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Tab) return;
			if (sender == Controller.Instance.HomeBusinessName)
				Controller.Instance.HomeDecisionMaker.Focus();
			else if (sender == Controller.Instance.HomeDecisionMaker)
				Controller.Instance.HomeClientType.Focus();
			else if (sender == Controller.Instance.HomeClientType)
				Controller.Instance.HomePresentationDate.Focus();
			else if (sender == Controller.Instance.HomePresentationDate)
				Controller.Instance.HomeBusinessName.Focus();
			e.Handled = true;
		}
		#endregion

		#region Schedule Grid Events
		private void advBandedGridViewSchedules_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			if (!_allowToSave) return;
			var targetView = sender as AdvBandedGridView;
			if (targetView == null) return;
			var targetSchedule = targetView.GetRow(e.RowHandle) as PackageSchedule;
			if (targetSchedule == null) return;
			var targetSchedulePage = _schedulePageManager.SchedulePages.FirstOrDefault(sp => sp.Schedule.Id == targetSchedule.Id);
			if (targetSchedulePage == null) return;
			var needFullScheduleUpdate = false;
			if (e.Column == bandedGridColumnScheduleSpotType)
			{
				targetSchedule.ResetSection();
				needFullScheduleUpdate = true;
			}
			else if (e.Column == bandedGridColumnScheduleFlightDateStart)
			{
				var startDate = (DateTime?)e.Value;
				if (startDate.HasValue && !targetSchedule.UserFlightDateEnd.HasValue)
				{
					var endDate = startDate.Value;
					while (endDate.DayOfWeek != _localPackage.EndDayOfWeek)
						endDate = endDate.AddDays(1);
					targetSchedule.UserFlightDateEnd = endDate;
				}
				targetSchedule.RebuildSectionSpots();
				needFullScheduleUpdate = true;
			}
			else if (e.Column == bandedGridColumnScheduleFlightDateEnd)
			{
				targetSchedule.RebuildSectionSpots();
				needFullScheduleUpdate = true;
			}
			targetSchedulePage.NeedFullUpdate |= needFullScheduleUpdate;
			targetSchedulePage.Update(true);
			UpdateScheduleSettings();
			SettingsNotSaved = true;
		}

		private void advBandedGridViewSchedules_CustomRowCellEdit(object sender, CustomRowCellEditEventArgs e)
		{
			var targetView = sender as AdvBandedGridView;
			if (targetView == null) return;
			var targetSchedule = targetView.GetRow(e.RowHandle) as PackageSchedule;
			if (targetSchedule == null) return;
			if (!String.IsNullOrEmpty(targetSchedule.Name)) return;
			if (e.Column == bandedGridColumnScheduleSpotType)
				e.RepositoryItem = repositoryItemTextEditDefault;
			if (e.Column == bandedGridColumnScheduleFlightDateStart ||
				e.Column == bandedGridColumnScheduleFlightDateEnd)
				e.RepositoryItem = repositoryItemDateEditDisabled;
		}

		private void advBandedGridViewSchedules_ShowingEditor(object sender, CancelEventArgs e)
		{
			e.Cancel = false;
			var targetView = sender as AdvBandedGridView;
			if (targetView == null) return;
			var targetSchedule = targetView.GetFocusedRow() as PackageSchedule;
			if (targetSchedule == null) return;
			if (!String.IsNullOrEmpty(targetSchedule.Name)) return;
			if (targetView.FocusedColumn == bandedGridColumnScheduleSpotType ||
				targetView.FocusedColumn == bandedGridColumnScheduleFlightDateStart ||
				targetView.FocusedColumn == bandedGridColumnScheduleFlightDateEnd)
				e.Cancel = true;
		}

		private void advBandedGridViewSchedules_RowCellStyle(object sender, RowCellStyleEventArgs e)
		{
			var targetView = sender as AdvBandedGridView;
			if (targetView == null) return;
			var targetSchedule = targetView.GetRow(e.RowHandle) as PackageSchedule;
			if (targetSchedule == null) return;
			if (e.Column == bandedGridColumnScheduleName ||
				e.Column == bandedGridColumnScheduleSpotType ||
				e.Column == bandedGridColumnScheduleFlightDateStart ||
				e.Column == bandedGridColumnScheduleFlightDateEnd ||
				e.Column == bandedGridColumnScheduleDuration)
			{
				if (String.IsNullOrEmpty(targetSchedule.Name))
					e.Appearance.ForeColor = Color.Gray;
			}
			if (e.Column == bandedGridColumnScheduleName)
			{
				if (String.IsNullOrEmpty(targetSchedule.Name))
					e.Appearance.Font = new Font(e.Appearance.Font.FontFamily, e.Appearance.Font.Size, FontStyle.Italic);
			}
		}

		private void repositoryItemButtonEditScheduleOperations_ButtonClick(object sender, ButtonPressedEventArgs e)
		{
			var targetSchedule = advBandedGridViewSchedules.GetFocusedRow() as PackageSchedule;
			if (targetSchedule == null) return;
			switch (e.Button.Index)
			{
				case 0:
					_localPackage.DeleteSchedule(targetSchedule);
					UpdateScheduleList();
					_schedulePageManager.RebuildTabPages();
					SettingsNotSaved = true;
					break;
			}
		}

		private void repositoryItemDateEditEnabled_CloseUp(object sender, CloseUpEventArgs e)
		{
			var dateEdit = sender as DateEdit;
			if (dateEdit == null) return;
			if (dateEdit.EditValue == e.Value) return;
			if (e.Value == null) return;
			DateTime temp;
			if (!DateTime.TryParse(e.Value.ToString(), out temp)) return;
			if (advBandedGridViewSchedules.FocusedColumn == bandedGridColumnScheduleFlightDateStart)
			{
				var moveDateToWeekStart = true;
				if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed)
				{
					if (temp.DayOfWeek != _localPackage.StartDayOfWeek)
						if (Utilities.Instance.ShowWarningQuestion(String.Format("Are you sure you want to start your schedule on a {0}?{1}{1}The broadcast week normally starts on a {2}.", temp.DayOfWeek, Environment.NewLine, _localPackage.StartDayOfWeek)) == DialogResult.Yes)
							moveDateToWeekStart = false;
				}
				if (moveDateToWeekStart)
				{
					while (temp.DayOfWeek != _localPackage.StartDayOfWeek)
						temp = temp.AddDays(-1);
					e.Value = temp;
				}
			}
			else if (advBandedGridViewSchedules.FocusedColumn == bandedGridColumnScheduleFlightDateEnd)
			{
				var moveDateToWeekEnd = true;
				if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed)
				{
					if (temp.DayOfWeek != _localPackage.EndDayOfWeek)
						if (Utilities.Instance.ShowWarningQuestion(String.Format("Are you sure you want to end your schedule on a {0}?{1}{1}The broadcast week normally ends on a {2}.", temp.DayOfWeek, Environment.NewLine, _localPackage.EndDayOfWeek)) == DialogResult.Yes)
							moveDateToWeekEnd = false;
				}
				if (moveDateToWeekEnd)
				{
					while (temp.DayOfWeek != _localPackage.EndDayOfWeek)
						temp = temp.AddDays(1);
					e.Value = temp;
				}
			}
		}

		private void repositoryItem_EditValueChanged(object sender, EventArgs e)
		{
			advBandedGridViewSchedules.CloseEditor();
			advBandedGridViewSchedules.RefreshData();
		}
		#endregion

		#region Ribbon Operations Events
		public void Add_Click(object sender, EventArgs e)
		{
			_localPackage.AddSchedule();
			UpdateScheduleList();
			_schedulePageManager.RebuildTabPages();
			SettingsNotSaved = true;
		}

		public void Clone_Click(object sender, EventArgs e)
		{
			var targetSchedule = advBandedGridViewSchedules.GetFocusedRow() as PackageSchedule;
			if (targetSchedule == null) return;
			_localPackage.CloneSchedule(targetSchedule);
			UpdateScheduleList();
			_schedulePageManager.RebuildTabPages();
			SettingsNotSaved = true;
		}

		public void Help_Click(object sender, EventArgs e)
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink(String.Format("Home{0}", MediaMetaData.Instance.DataTypeString));
		}

		public void Save_Click(object sender, EventArgs e)
		{
			if (SaveSchedule())
				Utilities.Instance.ShowInformation("Package Saved");
		}

		public void SaveAs_Click(object sender, EventArgs e)
		{
			using (var form = new FormNewSchedule(PackageManager.GetShortPackageList().Select(p => p.ShortFileName)))
			{
				form.Text = "Save Package";
				form.laLogo.Text = "Please set a new name for your Package:";
				if (form.ShowDialog() != DialogResult.OK) return;
				if (!string.IsNullOrEmpty(form.ScheduleName))
				{
					if (SaveSchedule(scheduleName: form.ScheduleName))
						Utilities.Instance.ShowInformation("Package saved");
				}
				else
				{
					Utilities.Instance.ShowWarning("Package Name can't be empty");
				}
			}
		}

		public void Ribbon_ScheduleTabChanged(object sender, EventArgs e)
		{
			var schedulePage = Controller.Instance.Ribbon.SelectedRibbonTabItem.Tag as SchedulePage;
			if (schedulePage == null) return;
			if (schedulePage.Section == null)
			{
				using (var form = new FormProgress())
				{
					form.laProgress.Text = "Loading Schedule...";
					form.TopMost = true;
					var thread = new Thread(() => Invoke((MethodInvoker)(schedulePage.BuidSection)));
					form.Show();
					thread.Start();
					while (thread.IsAlive)
						Application.DoEvents();
					form.Close();
				}
				Controller.Instance.PagesContainer.Controls.Add(schedulePage.Section);
			}
			schedulePage.Update();
			schedulePage.Section.BringToFront();
			Controller.Instance.PagesContainer.BringToFront();
		}
		#endregion

	}
}
