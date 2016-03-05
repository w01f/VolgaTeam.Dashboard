using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;

namespace Asa.Media.Controls.PresentationClasses.SettingsControls
{
	public partial class FormFlightDatesEdit : MetroForm
	{
		private bool _loading;

		public DayOfWeek StartDayOfWeek { get; set; }
		public DayOfWeek EndDayOfWeek { get; set; }
		public DateTime? DateStart { get; set; }
		public DateTime? DateEnd { get; set; }

		public FormFlightDatesEdit()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 2, laTitle.Font.Style);
				buttonXSave.Font = new Font(buttonXSave.Font.FontFamily, buttonXSave.Font.Size - 2, buttonXSave.Font.Style);
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
			}
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			_loading = true;

			calendarControlDateStart.FirstDayOfWeek =
				calendarControlDateEnd.FirstDayOfWeek = StartDayOfWeek;

			if (DateStart.HasValue)
			{
				calendarControlDateStart.DateTime = DateStart.Value;
				calendarControlDateStart.SetSelection(DateStart.Value);
			}

			if (DateEnd.HasValue)
			{
				calendarControlDateEnd.DateTime = DateEnd.Value;
				calendarControlDateEnd.SetSelection(DateEnd.Value);
			}

			UpdateDates();
			UpdateSaveAvailability();
			UpdateWeeksCount();
			UpdateFlexFlightDatesWarning();

			_loading = false;
		}

		private void UpdateSaveAvailability()
		{
			buttonXSave.Enabled = DateStart.HasValue && DateEnd.HasValue;
		}

		private void UpdateDates()
		{
			labelControlStartTitle.Text = String.Format("<color=\"Gray\">Start: {0}</color>",
				DateStart?.ToString("M/d/yy") ?? String.Empty);
			labelControlEndTitle.Text = String.Format("<color=\"Gray\">End: {0}</color>",
					DateEnd?.ToString("M/d/yy") ?? String.Empty);
		}

		private void UpdateWeeksCount()
		{
			var weeksCount = MediaScheduleSettings.CalcWeeksCount(
				DateStart,
				DateEnd,
				StartDayOfWeek,
				EndDayOfWeek
				);

			if (weeksCount.HasValue)
			{
				laTitle.Text = String.Format("Select your campaign dates: {0} {1}",
					weeksCount,
					weeksCount > 1 ? " Weeks" : " Week");
			}
			else
				laTitle.Text = "Select your campaign dates";
		}

		private void UpdateFlexFlightDatesWarning()
		{
			if (!MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed) return;

			var warningText = new List<string>();
			if (DateStart.HasValue)
			{
				var startDate = DateStart.Value;
				if (startDate.DayOfWeek != StartDayOfWeek)
				{
					var weekEnd = startDate;
					while (weekEnd.DayOfWeek != EndDayOfWeek)
						weekEnd = weekEnd.AddDays(1);
					warningText.Add(String.Format("*The FIRST WEEK of your schedule STARTS on a {0}", startDate.DayOfWeek));
				}
			}
			if (DateEnd.HasValue)
			{
				var endDate = DateEnd.Value;
				if (endDate.DayOfWeek != EndDayOfWeek)
				{
					var weekStart = endDate;
					while (weekStart.DayOfWeek != StartDayOfWeek)
						weekStart = weekStart.AddDays(-1);
					warningText.Add(String.Format("*The LAST WEEK of your schedule ENDS on a {0}", endDate.DayOfWeek));
				}
			}

			if (warningText.Any())
			{
				labelControlWarnings.Text = String.Format("<color=\"Red\">{0}</color>",
					String.Join("<br>", warningText));
				labelControlWarnings.Visible = true;
			}
			else
			{
				labelControlWarnings.Text = String.Empty;
				labelControlWarnings.Visible = false;
			}
		}

		private void OnDateStartChanged(object sender, EventArgs e)
		{
			calendarControlDateStart.TodayDate = calendarControlDateStart.DateTime;
			if (_loading) return;
			var dateStart = calendarControlDateStart.DateTime;

			var moveDateToWeekStart = true;
			if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed)
			{
				if (dateStart.DayOfWeek != StartDayOfWeek)
					if (PopupMessageHelper.Instance.ShowWarningQuestion(
						String.Format(
							"Are you sure you want to start your schedule on a {0}?{1}{1}The broadcast week normally starts on a {2}.",
							dateStart.DayOfWeek, Environment.NewLine,
							StartDayOfWeek)) == DialogResult.Yes)
						moveDateToWeekStart = false;
			}
			if (moveDateToWeekStart)
			{
				while (dateStart.DayOfWeek != StartDayOfWeek)
					dateStart = dateStart.AddDays(-1);
				_loading = true;
				calendarControlDateStart.DateTime = dateStart;
				calendarControlDateStart.SetSelection(dateStart);
				_loading = false;
			}

			DateStart = dateStart;
			if (!DateEnd.HasValue || DateEnd < DateStart)
			{
				while (dateStart.DayOfWeek != EndDayOfWeek)
					dateStart = dateStart.AddDays(1);
				calendarControlDateEnd.DateTime = dateStart;
				calendarControlDateEnd.SetSelection(dateStart);
			}
			else
				OnDateChanged(sender, e);
		}

		private void OnDateEndChanged(object sender, EventArgs e)
		{
			calendarControlDateEnd.TodayDate = calendarControlDateEnd.DateTime;
			if (_loading) return;
			var dateEnd = calendarControlDateEnd.DateTime;
			var moveDateToWeekEnd = true;
			if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed)
			{
				if (dateEnd.DayOfWeek != EndDayOfWeek)
					if (PopupMessageHelper.Instance.ShowWarningQuestion(
						String.Format(
							"Are you sure you want to end your schedule on a {0}?{1}{1}The broadcast week normally ends on a {2}.",
							dateEnd.DayOfWeek, Environment.NewLine,
							EndDayOfWeek)) == DialogResult.Yes)
						moveDateToWeekEnd = false;
			}
			if (moveDateToWeekEnd)
			{
				while (dateEnd.DayOfWeek != EndDayOfWeek)
					dateEnd = dateEnd.AddDays(1);
				_loading = true;
				calendarControlDateEnd.DateTime = dateEnd;
				calendarControlDateEnd.SetSelection(dateEnd);
				_loading = false;
			}

			DateEnd = dateEnd;
			if (!DateStart.HasValue || DateEnd < DateStart)
			{
				while (dateEnd.DayOfWeek != StartDayOfWeek)
					dateEnd = dateEnd.AddDays(-1);
				calendarControlDateStart.DateTime = dateEnd;
				calendarControlDateStart.SetSelection(dateEnd);
			}
			else
				OnDateChanged(sender, e);

			OnDateChanged(sender, e);
		}

		private void OnDateChanged(object sender, EventArgs e)
		{
			if (_loading) return;
			UpdateDates();
			UpdateSaveAvailability();
			UpdateWeeksCount();
			UpdateFlexFlightDatesWarning();
		}
	}
}
