using System;
using System.ComponentModel;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using CalendarBuilder.BusinessClasses;
using CalendarBuilder.ToolForms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;

namespace CalendarBuilder.PresentationClasses
{
	[ToolboxItem(false)]
	public partial class HomeControl : UserControl
	{
		private static HomeControl _instance;
		private bool _allowToSave;
		private Schedule _localCalendar;

		private HomeControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			if ((base.CreateGraphics()).DpiX > 96) { }
			ScheduleManager.Instance.SettingsSaved += (sender, e) =>
														  {
															  if (sender != this)
																  LoadCalendar(e.QuickSave);
														  };
		}

		public bool SettingsNotSaved { get; set; }

		public static HomeControl Instance
		{
			get
			{
				if (_instance == null)
					_instance = new HomeControl();
				return _instance;
			}
		}

		public bool AllowToLeaveControl
		{
			get
			{
				bool result = false;
				if (SettingsNotSaved)
				{
					if (SaveCalendar())
						result = true;
				}
				else
					result = true;
				return result;
			}
		}

		#region Common Methods
		public static void RemoveInstance()
		{
			try
			{
				_instance.Dispose();
			}
			catch { }
			finally
			{
				_instance = null;
			}
		}
		#endregion

		private void UncheckSalesStrategyButtons()
		{
			FormMain.Instance.buttonItemHomeSalesStrategyFaceCall.Checked = false;
			FormMain.Instance.buttonItemHomeSalesStrategyEmail.Checked = false;
			FormMain.Instance.buttonItemHomeSalesStrategyFax.Checked = false;
		}

		#region Calendar Methods
		public void LoadCalendar(bool quickLoad)
		{
			_localCalendar = ScheduleManager.Instance.GetLocalSchedule();

			if (!quickLoad)
			{
				_allowToSave = false;

				FormMain.Instance.comboBoxEditBusinessName.Properties.Items.Clear();
				FormMain.Instance.comboBoxEditBusinessName.Properties.Items.AddRange(ListManager.Instance.Advertisers.ToArray());
				FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.Clear();
				FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.AddRange(ListManager.Instance.DecisionMakers.ToArray());
				FormMain.Instance.comboBoxEditClientType.Properties.Items.Clear();
				FormMain.Instance.comboBoxEditClientType.Properties.Items.AddRange(ListManager.Instance.ClientTypes.ToArray());

				FormMain.Instance.comboBoxEditBusinessName.EditValue = !string.IsNullOrEmpty(_localCalendar.BusinessName) ? _localCalendar.BusinessName : null;
				FormMain.Instance.comboBoxEditDecisionMaker.EditValue = !string.IsNullOrEmpty(_localCalendar.DecisionMaker) ? _localCalendar.DecisionMaker : null;
				if (!string.IsNullOrEmpty(_localCalendar.ClientType))
					FormMain.Instance.comboBoxEditClientType.SelectedIndex = FormMain.Instance.comboBoxEditClientType.Properties.Items.IndexOf(_localCalendar.ClientType);
				switch (_localCalendar.SalesStrategy)
				{
					case SalesStrategy.Email:
						FormMain.Instance.buttonItemHomeSalesStrategyEmail.Checked = true;
						FormMain.Instance.buttonItemHomeSalesStrategyFax.Checked = false;
						FormMain.Instance.buttonItemHomeSalesStrategyFaceCall.Checked = false;
						break;
					case SalesStrategy.Fax:
						FormMain.Instance.buttonItemHomeSalesStrategyEmail.Checked = false;
						FormMain.Instance.buttonItemHomeSalesStrategyFax.Checked = true;
						FormMain.Instance.buttonItemHomeSalesStrategyFaceCall.Checked = false;
						break;
					case SalesStrategy.InPerson:
						FormMain.Instance.buttonItemHomeSalesStrategyEmail.Checked = false;
						FormMain.Instance.buttonItemHomeSalesStrategyFax.Checked = false;
						FormMain.Instance.buttonItemHomeSalesStrategyFaceCall.Checked = true;
						break;
				}

				if (_localCalendar.SundayBased)
				{
					FormMain.Instance.buttonItemHomeCalendarTypeSunday.Checked = true;
					FormMain.Instance.buttonItemHomeCalendarTypeMonday.Checked = false;
					Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
					Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Sunday;
					Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
					Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
				}
				else
				{
					FormMain.Instance.buttonItemHomeCalendarTypeSunday.Checked = false;
					FormMain.Instance.buttonItemHomeCalendarTypeMonday.Checked = true;
					Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
					Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
					Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
					Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
				}

				FormMain.Instance.buttonItemHomeProductsNewspaper.Checked = _localCalendar.ShowNewspaper;
				FormMain.Instance.buttonItemHomeProductsDigital.Checked = _localCalendar.ShowDigital;
				FormMain.Instance.buttonItemHomeProductsTV.Checked = _localCalendar.ShowTV;
				FormMain.Instance.buttonItemHomeProductsRadio.Checked = _localCalendar.ShowRadio;

				FormMain.Instance.dateEditPresentationDate.EditValue = _localCalendar.PresentationDate;
				FormMain.Instance.dateEditFlightDatesStart.EditValue = _localCalendar.FlightDateStart;
				FormMain.Instance.dateEditFlightDatesEnd.EditValue = _localCalendar.FlightDateEnd;
				FormMain.Instance.ribbonPanelHome.PerformLayout();

				_allowToSave = true;
			}
			FormMain.Instance.UpdateScheduleTabs(FormMain.Instance.comboBoxEditBusinessName.EditValue != null &
												 FormMain.Instance.comboBoxEditDecisionMaker.EditValue != null &
												 FormMain.Instance.comboBoxEditClientType.EditValue != null &
												 FormMain.Instance.dateEditPresentationDate.EditValue != null &
												 FormMain.Instance.dateEditFlightDatesStart.EditValue != null &
												 FormMain.Instance.dateEditFlightDatesEnd.EditValue != null);
			SettingsNotSaved = false;
		}

		private bool SaveCalendar(string scheduleName = "")
		{
			bool quickSave = true;

			if (!string.IsNullOrEmpty(scheduleName))
				_localCalendar.Name = scheduleName;
			if (FormMain.Instance.comboBoxEditBusinessName.EditValue != null)
				_localCalendar.BusinessName = FormMain.Instance.comboBoxEditBusinessName.EditValue.ToString();
			else
			{
				AppManager.ShowWarning("Your calendar is missing important information!\nPlease make sure you have a Business Name before you proceed.");
				return false;
			}
			if (FormMain.Instance.comboBoxEditDecisionMaker.EditValue != null)
				_localCalendar.DecisionMaker = FormMain.Instance.comboBoxEditDecisionMaker.EditValue.ToString();
			else
			{
				AppManager.ShowWarning("Your calendar is missing important information!\nPlease make sure you have a Owner/Decision-maker before you proceed.");
				return false;
			}

			if (FormMain.Instance.comboBoxEditClientType.EditValue != null)
				_localCalendar.ClientType = FormMain.Instance.comboBoxEditClientType.EditValue.ToString();
			else
			{
				AppManager.ShowWarning("You must set Client type before save");
				return false;
			}

			if (FormMain.Instance.buttonItemHomeSalesStrategyEmail.Checked)
				_localCalendar.SalesStrategy = SalesStrategy.Email;
			else if (FormMain.Instance.buttonItemHomeSalesStrategyFax.Checked)
				_localCalendar.SalesStrategy = SalesStrategy.Fax;
			else if (FormMain.Instance.buttonItemHomeSalesStrategyFaceCall.Checked)
				_localCalendar.SalesStrategy = SalesStrategy.InPerson;

			if (FormMain.Instance.dateEditPresentationDate.EditValue != null)
				_localCalendar.PresentationDate = FormMain.Instance.dateEditPresentationDate.DateTime;
			else
			{
				AppManager.ShowWarning("Your calendar is missing important information!\nPlease make sure you have a Presentation Date before you proceed.");
				return false;
			}

			bool sundayBased = _localCalendar.SundayBased;
			_localCalendar.SundayBased = FormMain.Instance.buttonItemHomeCalendarTypeSunday.Checked;
			quickSave &= sundayBased == _localCalendar.SundayBased;

			_localCalendar.ShowNewspaper = FormMain.Instance.buttonItemHomeProductsNewspaper.Checked;
			_localCalendar.ShowDigital = FormMain.Instance.buttonItemHomeProductsDigital.Checked;
			_localCalendar.ShowTV = FormMain.Instance.buttonItemHomeProductsTV.Checked;
			_localCalendar.ShowRadio = FormMain.Instance.buttonItemHomeProductsRadio.Checked;

			if (FormMain.Instance.dateEditFlightDatesStart.EditValue != null && FormMain.Instance.dateEditFlightDatesEnd.EditValue != null)
			{
				DateTime startDate = FormMain.Instance.dateEditFlightDatesStart.DateTime;
				DateTime endDate = FormMain.Instance.dateEditFlightDatesEnd.DateTime;
				if (startDate.DayOfWeek != (_localCalendar.SundayBased ? DayOfWeek.Sunday : DayOfWeek.Monday))
				{
					AppManager.ShowWarning(string.Format("Campaign Start Date must be {0}\nCampaign End Date must be {1}\nCampaign Start Date must be less then Campaign End Date.", new[] { _localCalendar.SundayBased ? "Sunday" : "Monday", _localCalendar.SundayBased ? "Saturday" : "Sunday" }));
					return false;
				}
				if (endDate.DayOfWeek != (_localCalendar.SundayBased ? DayOfWeek.Saturday : DayOfWeek.Sunday) || _localCalendar.FlightDateEnd < _localCalendar.FlightDateStart)
				{
					AppManager.ShowWarning(string.Format("Campaign Start Date must be {0}\nCampaign End Date must be {1}\nCampaign Start Date must be less then Campaign End Date.", new[] { _localCalendar.SundayBased ? "Sunday" : "Monday", _localCalendar.SundayBased ? "Saturday" : "Sunday" }));
					return false;
				}

				quickSave &= !(_localCalendar.FlightDateStart != startDate || _localCalendar.FlightDateEnd != endDate);
				_localCalendar.FlightDateStart = startDate;
				_localCalendar.FlightDateEnd = endDate;
			}
			else
			{
				AppManager.ShowWarning("Your calendar is missing important information!\nPlease make sure you have a Campaign Dates before you proceed.");
				return false;
			}

			FormMain.Instance.comboBoxEditBusinessName.Properties.Items.Clear();
			FormMain.Instance.comboBoxEditBusinessName.Properties.Items.AddRange(ListManager.Instance.Advertisers.ToArray());
			FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.Clear();
			FormMain.Instance.comboBoxEditDecisionMaker.Properties.Items.AddRange(ListManager.Instance.DecisionMakers.ToArray());
			FormMain.Instance.UpdateScheduleTabs(FormMain.Instance.comboBoxEditBusinessName.EditValue != null &
												 FormMain.Instance.comboBoxEditDecisionMaker.EditValue != null &
												 FormMain.Instance.comboBoxEditClientType.EditValue != null &
												 FormMain.Instance.dateEditPresentationDate.EditValue != null &
												 FormMain.Instance.dateEditFlightDatesStart.EditValue != null &
												 FormMain.Instance.dateEditFlightDatesEnd.EditValue != null);
			ScheduleManager.Instance.SaveSchedule(_localCalendar, quickSave, this);
			LoadCalendar(true);
			return true;
		}
		#endregion

		#region Toggles Switch Events
		public void buttonItemHomeSalesStrategyFaceCall_Click(object sender, EventArgs e)
		{
			UncheckSalesStrategyButtons();
			FormMain.Instance.buttonItemHomeSalesStrategyFaceCall.Checked = true;
		}

		public void buttonItemHomeSalesStrategyEmail_Click(object sender, EventArgs e)
		{
			UncheckSalesStrategyButtons();
			FormMain.Instance.buttonItemHomeSalesStrategyEmail.Checked = true;
		}

		public void buttonItemHomeSalesStrategyFax_Click(object sender, EventArgs e)
		{
			UncheckSalesStrategyButtons();
			FormMain.Instance.buttonItemHomeSalesStrategyFax.Checked = true;
		}

		public void buttonItemHomeCalendarType_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				if (FormMain.Instance.buttonItemHomeCalendarTypeSunday.Checked)
				{
					Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
					Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Sunday;
					Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
					Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
				}
				else
				{
					Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");
					Thread.CurrentThread.CurrentCulture.DateTimeFormat.FirstDayOfWeek = DayOfWeek.Monday;
					Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = @"MM/dd/yyyy";
					Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
				}
				FormMain.Instance.dateEditFlightDatesStart.EditValue = null;
				FormMain.Instance.dateEditFlightDatesEnd.EditValue = null;
				SettingsNotSaved = true;
			}
		}

		public void buttonItemHomeCalendarType_Click(object sender, EventArgs e)
		{
			var button = sender as ButtonItem;
			if (button != null && !button.Checked)
			{
				if (AppManager.ShowWarningQuestion("Campaign Dates and Calendars will be reset after you change Calendar Type.\nDo you want to proceed?") == DialogResult.Yes)
				{
					FormMain.Instance.buttonItemHomeCalendarTypeSunday.Checked = false;
					FormMain.Instance.buttonItemHomeCalendarTypeMonday.Checked = false;
					button.Checked = true;
				}
			}
		}

		public void buttonItemHomeProducts_CheckedChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
				SettingsNotSaved = true;
		}
		#endregion

		#region Editors Events
		public void SchedulePropertyEditValueChanged(object sender, EventArgs e)
		{
			if (_allowToSave)
			{
				FormMain.Instance.UpdateScheduleTabs(FormMain.Instance.comboBoxEditBusinessName.EditValue != null &
													 FormMain.Instance.comboBoxEditDecisionMaker.EditValue != null &
													 FormMain.Instance.comboBoxEditClientType.EditValue != null &
													 FormMain.Instance.dateEditPresentationDate.EditValue != null &
													 FormMain.Instance.dateEditFlightDatesStart.EditValue != null &
													 FormMain.Instance.dateEditFlightDatesEnd.EditValue != null);
				SettingsNotSaved = true;
			}
		}

		public void FlightDateStartEditValueChanged(object sender, EventArgs e)
		{
			if (FormMain.Instance.dateEditFlightDatesStart.EditValue != null && _allowToSave)
			{
				DateTime dateStart = FormMain.Instance.dateEditFlightDatesStart.DateTime;
				SettingsNotSaved = true;
				if (FormMain.Instance.dateEditFlightDatesEnd.EditValue == null)
				{
					while (dateStart.DayOfWeek != (FormMain.Instance.buttonItemHomeCalendarTypeSunday.Checked ? DayOfWeek.Saturday : DayOfWeek.Sunday))
						dateStart = dateStart.AddDays(1);
					FormMain.Instance.dateEditFlightDatesEnd.EditValue = dateStart;
				}
			}
			SchedulePropertyEditValueChanged(null, null);
		}

		public void FlightDateEndEditValueChanged(object sender, EventArgs e)
		{
			if (FormMain.Instance.dateEditFlightDatesStart.EditValue != null && _allowToSave)
				SettingsNotSaved = true;
			SchedulePropertyEditValueChanged(null, null);
		}

		public void CalcWeeksOnFlightDatesChange(object sender, EventArgs e)
		{
			FormMain.Instance.labelItemFlightDatesWeeks.Text = "";
			FormMain.Instance.labelItemFlightDatesWeeks.Visible = false;
			if (FormMain.Instance.dateEditFlightDatesStart.DateTime != null && FormMain.Instance.dateEditFlightDatesEnd.DateTime != null)
			{
				TimeSpan datesRange = FormMain.Instance.dateEditFlightDatesEnd.DateTime - FormMain.Instance.dateEditFlightDatesStart.DateTime;
				int weeksCount = datesRange.Days / 7 + 1;
				FormMain.Instance.labelItemFlightDatesWeeks.Text = weeksCount.ToString() + (weeksCount > 1 ? " Weeks" : " Week");
				FormMain.Instance.labelItemFlightDatesWeeks.Visible = true;
			}
		}

		public void dateEditFlightDatesStart_CloseUp(object sender, CloseUpEventArgs e)
		{
			if (e.Value != null)
			{
				DateTime temp = DateTime.MinValue;
				if (DateTime.TryParse(e.Value.ToString(), out temp))
				{
					while (temp.DayOfWeek != (FormMain.Instance.buttonItemHomeCalendarTypeSunday.Checked ? DayOfWeek.Sunday : DayOfWeek.Monday))
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
					while (temp.DayOfWeek != (FormMain.Instance.buttonItemHomeCalendarTypeSunday.Checked ? DayOfWeek.Saturday : DayOfWeek.Sunday))
						temp = temp.AddDays(1);
					e.Value = temp;
				}
			}
		}
		#endregion

		#region Ribbon Operations Events
		public void buttonItemHomeHelp_Click(object sender, EventArgs e)
		{
			HelpManager.Instance.OpenHelpLink("Home");
		}

		public void buttonItemHomeSave_Click(object sender, EventArgs e)
		{
			if (SaveCalendar())
				AppManager.ShowInformation("Calendar Saved");
		}

		public void buttonItemHomeSaveAs_Click(object sender, EventArgs e)
		{
			using (var from = new FormNewCalendar())
			{
				from.Text = "Save Calendar";
				from.laLogo.Text = "Please set a new name for your Calendar:";
				if (from.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(from.ScheduleName))
					{
						if (SaveCalendar(from.ScheduleName))
							AppManager.ShowInformation("Calendar was saved");
					}
					else
					{
						AppManager.ShowWarning("Calendar Name can't be empty");
					}
				}
			}
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
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion
	}
}