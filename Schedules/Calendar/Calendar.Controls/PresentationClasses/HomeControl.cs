using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.Calendar.Controls.BusinessClasses;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using SettingsManager = NewBizWiz.Core.Calendar.SettingsManager;

namespace NewBizWiz.Calendar.Controls.PresentationClasses
{
	[ToolboxItem(false)]
	public partial class HomeControl : UserControl
	{
		private bool _allowToSave;
		private Schedule _localCalendar;

		public HomeControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			if ((base.CreateGraphics()).DpiX > 96) { }
			BusinessObjects.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.BeginInvoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadCalendar(e.QuickSave);
			});
		}

		public bool SettingsNotSaved { get; set; }

		public bool AllowToLeaveControl
		{
			get
			{
				bool result = false;
				if (SettingsNotSaved)
				{
					if (SaveCalendar(false))
						result = true;
				}
				else
					result = true;
				return result;
			}
		}

		#region Calendar Methods
		public void LoadCalendar(bool quickLoad)
		{
			_localCalendar = BusinessObjects.Instance.ScheduleManager.GetLocalSchedule();

			if (!quickLoad)
			{
				_allowToSave = false;

				Controller.Instance.HomeClientType.Properties.Items.Clear();
				Controller.Instance.HomeClientType.Properties.Items.AddRange(Core.AdSchedule.ListManager.Instance.ClientTypes.ToArray());

				Controller.Instance.HomeBusinessName.EditValue = !string.IsNullOrEmpty(_localCalendar.BusinessName) ? _localCalendar.BusinessName : null;
				Controller.Instance.HomeDecisionMaker.EditValue = !string.IsNullOrEmpty(_localCalendar.DecisionMaker) ? _localCalendar.DecisionMaker : null;
				if (!string.IsNullOrEmpty(_localCalendar.ClientType))
					Controller.Instance.HomeClientType.SelectedIndex = Controller.Instance.HomeClientType.Properties.Items.IndexOf(_localCalendar.ClientType);

				Controller.Instance.HomeAccountNumberCheck.Checked = !string.IsNullOrEmpty(_localCalendar.AccountNumber);
				Controller.Instance.HomeAccountNumberText.EditValue = _localCalendar.AccountNumber;
				Controller.Instance.HomePresentationDate.EditValue = _localCalendar.PresentationDate;
				Controller.Instance.HomeFlightDatesStart.EditValue = _localCalendar.FlightDateStart;
				Controller.Instance.HomeFlightDatesEnd.EditValue = _localCalendar.FlightDateEnd;
				Controller.Instance.HomePanel.PerformLayout();

				_allowToSave = true;
			}
			Controller.Instance.UpdateScheduleTabs(Controller.Instance.HomeBusinessName.EditValue != null &
												   Controller.Instance.HomeDecisionMaker.EditValue != null &
												   Controller.Instance.HomeClientType.EditValue != null &
												   Controller.Instance.HomePresentationDate.EditValue != null &
												   Controller.Instance.HomeFlightDatesStart.EditValue != null &
												   Controller.Instance.HomeFlightDatesEnd.EditValue != null);
			SettingsNotSaved = false;
		}

		private bool SaveCalendar(bool byUser, string scheduleName = "")
		{
			bool quickSave = true;

			if (Controller.Instance.HomeBusinessName.EditValue != null)
				_localCalendar.BusinessName = Controller.Instance.HomeBusinessName.EditValue.ToString();
			else
			{
				Utilities.Instance.ShowWarning("Your calendar is missing important information!\nPlease make sure you have a Business Name before you proceed.");
				return false;
			}
			if (Controller.Instance.HomeDecisionMaker.EditValue != null)
				_localCalendar.DecisionMaker = Controller.Instance.HomeDecisionMaker.EditValue.ToString();
			else
			{
				Utilities.Instance.ShowWarning("Your calendar is missing important information!\nPlease make sure you have a Owner/Decision-maker before you proceed.");
				return false;
			}

			if (Controller.Instance.HomeClientType.EditValue != null)
				_localCalendar.ClientType = Controller.Instance.HomeClientType.EditValue.ToString();
			else
			{
				Utilities.Instance.ShowWarning("You must set Client type before save");
				return false;
			}

			if (Controller.Instance.HomePresentationDate.EditValue != null)
				_localCalendar.PresentationDate = Controller.Instance.HomePresentationDate.DateTime;
			else
			{
				Utilities.Instance.ShowWarning("Your calendar is missing important information!\nPlease make sure you have a Presentation Date before you proceed.");
				return false;
			}

			if (Controller.Instance.HomeAccountNumberCheck.Checked && Controller.Instance.HomeAccountNumberText.EditValue != null)
				_localCalendar.AccountNumber = Controller.Instance.HomeAccountNumberText.EditValue.ToString();
			else
				_localCalendar.AccountNumber = string.Empty;

			if (Controller.Instance.HomeFlightDatesStart.EditValue != null && Controller.Instance.HomeFlightDatesEnd.EditValue != null)
			{
				DateTime startDate = Controller.Instance.HomeFlightDatesStart.DateTime;
				DateTime endDate = Controller.Instance.HomeFlightDatesEnd.DateTime;
				if (startDate.DayOfWeek != DayOfWeek.Sunday)
				{
					Utilities.Instance.ShowWarning(string.Format("Campaign Start Date must be {0}\nCampaign End Date must be {1}\nCampaign Start Date must be less then Campaign End Date.", new[] { "Sunday", "Saturday" }));
					return false;
				}
				if (endDate.DayOfWeek != DayOfWeek.Saturday || _localCalendar.FlightDateEnd < _localCalendar.FlightDateStart)
				{
					Utilities.Instance.ShowWarning(string.Format("Campaign Start Date must be {0}\nCampaign End Date must be {1}\nCampaign Start Date must be less then Campaign End Date.", new[] { "Sunday", "Saturday" }));
					return false;
				}

				quickSave &= !(_localCalendar.FlightDateStart != startDate || _localCalendar.FlightDateEnd != endDate);
				_localCalendar.FlightDateStart = startDate;
				_localCalendar.FlightDateEnd = endDate;
			}
			else
			{
				Utilities.Instance.ShowWarning("Your calendar is missing important information!\nPlease make sure you have a Campaign Dates before you proceed.");
				return false;
			}

			Controller.Instance.UpdateScheduleTabs(Controller.Instance.HomeBusinessName.EditValue != null &
												   Controller.Instance.HomeDecisionMaker.EditValue != null &
												   Controller.Instance.HomeClientType.EditValue != null &
												   Controller.Instance.HomePresentationDate.EditValue != null &
												   Controller.Instance.HomeFlightDatesStart.EditValue != null &
												   Controller.Instance.HomeFlightDatesEnd.EditValue != null);
			var nameChanged = !string.IsNullOrEmpty(scheduleName);
			if (nameChanged)
				_localCalendar.Name = scheduleName;
			Controller.Instance.SaveSchedule(_localCalendar, byUser, nameChanged, quickSave, this);
			LoadCalendar(true);
			return true;
		}
		#endregion

		#region Editors Events
		public void SchedulePropertyEditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				Controller.Instance.UpdateScheduleTabs(Controller.Instance.HomeBusinessName.EditValue != null &
													   Controller.Instance.HomeDecisionMaker.EditValue != null &
													   Controller.Instance.HomeClientType.EditValue != null &
													   Controller.Instance.HomePresentationDate.EditValue != null &
													   Controller.Instance.HomeFlightDatesStart.EditValue != null &
													   Controller.Instance.HomeFlightDatesEnd.EditValue != null);
				SettingsNotSaved = true;
			}
		}

		public void checkBoxItemAccountNumber_CheckedChanged(object sender, CheckBoxChangeEventArgs e)
		{
			Controller.Instance.HomeAccountNumberText.Enabled = Controller.Instance.HomeAccountNumberCheck.Checked;
			SchedulePropertyEditValueChanged(null, null);
		}

		public void FlightDateStartEditValueChanged(object sender, EventArgs e)
		{
			if (Controller.Instance.HomeFlightDatesStart.EditValue != null && _allowToSave)
			{
				DateTime dateStart = Controller.Instance.HomeFlightDatesStart.DateTime;
				SettingsNotSaved = true;
				if (Controller.Instance.HomeFlightDatesEnd.EditValue == null)
				{
					while (dateStart.DayOfWeek != DayOfWeek.Saturday)
						dateStart = dateStart.AddDays(1);
					Controller.Instance.HomeFlightDatesEnd.EditValue = dateStart;
				}
			}
			SchedulePropertyEditValueChanged(null, null);
		}

		public void FlightDateEndEditValueChanged(object sender, EventArgs e)
		{
			if (Controller.Instance.HomeFlightDatesStart.EditValue != null && _allowToSave)
				SettingsNotSaved = true;
			SchedulePropertyEditValueChanged(null, null);
		}

		public void CalcWeeksOnFlightDatesChange(object sender, EventArgs e)
		{
			Controller.Instance.HomeWeeks.Text = "";
			Controller.Instance.HomeWeeks.Visible = false;
			if (Controller.Instance.HomeFlightDatesStart.DateTime != null && Controller.Instance.HomeFlightDatesEnd.DateTime != null)
			{
				TimeSpan datesRange = Controller.Instance.HomeFlightDatesEnd.DateTime - Controller.Instance.HomeFlightDatesStart.DateTime;
				int weeksCount = datesRange.Days / 7 + 1;
				Controller.Instance.HomeWeeks.Text = weeksCount.ToString() + (weeksCount > 1 ? " Weeks" : " Week");
				Controller.Instance.HomeWeeks.Visible = true;
			}
		}

		public void dateEditFlightDatesStart_CloseUp(object sender, CloseUpEventArgs e)
		{
			if (e.Value != null)
			{
				DateTime temp = DateTime.MinValue;
				if (DateTime.TryParse(e.Value.ToString(), out temp))
				{
					while (temp.DayOfWeek != DayOfWeek.Sunday)
						temp = temp.AddDays(-1);
					e.Value = temp;
				}
			}
		}

		public void dateEditFlightDatesEnd_CloseUp(object sender, CloseUpEventArgs e)
		{
			if (e.Value != null)
			{
				DateTime temp = DateTime.MinValue;
				if (DateTime.TryParse(e.Value.ToString(), out temp))
				{
					while (temp.DayOfWeek != DayOfWeek.Saturday)
						temp = temp.AddDays(1);
					e.Value = temp;
				}
			}
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
				Controller.Instance.HomeFlightDatesStart.Focus();
			else if (sender == Controller.Instance.HomeFlightDatesStart)
				Controller.Instance.HomeFlightDatesEnd.Focus();
			else if (sender == Controller.Instance.HomeFlightDatesEnd)
				Controller.Instance.HomeBusinessName.Focus();
			e.Handled = true;
		}
		#endregion

		#region Ribbon Operations Events
		public void HomeHelp_Click(object sender, EventArgs e)
		{
			BusinessObjects.Instance.HelpManager.OpenHelpLink("Home");
		}

		public void HomeSave_Click(object sender, EventArgs e)
		{
			if (SaveCalendar(true))
				Utilities.Instance.ShowInformation("Calendar Saved");
		}

		public void HomeSaveAs_Click(object sender, EventArgs e)
		{
			using (var form = new FormNewSchedule(ScheduleManager.GetShortScheduleList(new DirectoryInfo(SettingsManager.Instance.SaveFolder)).Select(s => s.ShortFileName)))
			{
				form.Text = "Save Calendar";
				form.laLogo.Text = "Please set a new name for your Calendar:";
				if (form.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(form.ScheduleName))
					{
						if (SaveCalendar(true, form.ScheduleName))
							Utilities.Instance.ShowInformation("Calendar was saved");
					}
					else
					{
						Utilities.Instance.ShowWarning("Calendar Name can't be empty");
					}
				}
			}
		}
		#endregion
	}
}