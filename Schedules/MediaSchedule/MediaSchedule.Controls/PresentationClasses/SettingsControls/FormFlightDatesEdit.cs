using System;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;

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

			layoutControlItemDateStart.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDateStart.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDateStart.MinSize = RectangleHelper.ScaleSize(layoutControlItemDateStart.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDateEnd.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDateEnd.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemDateEnd.MinSize = RectangleHelper.ScaleSize(layoutControlItemDateEnd.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void OnFormLoad(object sender, EventArgs e)
		{
			_loading = true;

			calendarControlDateStart.FirstDayOfWeek =
				calendarControlDateEnd.FirstDayOfWeek = StartDayOfWeek;
			calendarControlDateStart.DateTime = DateStart ?? DateTime.Today;
			calendarControlDateEnd.DateTime = DateEnd ?? DateTime.Today;

			UpdateDates();
			UpdateSaveAvailability();
			UpdateWeeksCount();

			_loading = false;
		}

		private void UpdateSaveAvailability()
		{
			buttonXSave.Enabled = DateStart.HasValue && DateEnd.HasValue;
		}

		private void UpdateDates()
		{
			layoutControlItemDateStart.Text = String.Format("<color=\"Gray\">Start: {0}</color>",
				DateStart?.ToString("M/d/yy") ?? String.Empty);
			layoutControlItemDateEnd.Text = String.Format("<color=\"Gray\">End: {0}</color>",
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
				simpleLabelItemTitle.Text = String.Format("<size=+4>Select your campaign dates: {0} {1}</size>",
					weeksCount,
					weeksCount > 1 ? " Weeks" : " Week");
			}
			else
				simpleLabelItemTitle.Text = "Select your campaign dates";
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
				_loading = false;
			}

			DateStart = dateStart;
			if (!DateEnd.HasValue || DateEnd < DateStart)
			{
				while (dateStart.DayOfWeek != EndDayOfWeek)
					dateStart = dateStart.AddDays(1);
				calendarControlDateEnd.DateTime = dateStart;
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
				_loading = false;
			}

			DateEnd = dateEnd;
			if (!DateStart.HasValue || DateEnd < DateStart)
			{
				while (dateEnd.DayOfWeek != StartDayOfWeek)
					dateEnd = dateEnd.AddDays(-1);
				calendarControlDateStart.DateTime = dateEnd;
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
		}
	}
}
