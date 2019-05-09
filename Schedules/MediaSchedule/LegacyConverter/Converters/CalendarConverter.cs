using System;

namespace Asa.Media.LegacyConverter.Converters
{
	static class CalendarConverter
	{
		public static void ImportData(
			this Business.Media.Entities.NonPersistent.Calendar.MediaCalendar target,
			Legacy.Common.Entities.Calendar.BaseCalendar source)
		{
			//target.Days.Clear();
			//foreach (var oldCalendarDay in source.Days)
			//{
			//	Business.Calendar.Entities.NonPersistent.CalendarDay day;
			//	if (target.Schedule.Settings.MondayBased)
			//		day = new Business.Calendar.Entities.NonPersistent.CalendarDayMondayBased(target);
			//	else
			//		day = new Business.Calendar.Entities.NonPersistent.CalendarDaySundayBased(target);
			//	day.ImportData(oldCalendarDay);
			//	target.Days.Add(day);
			//}

			//target.Months.Clear();
			//foreach (var oldCalendarMonths in source.Months)
			//{
			//	Business.Calendar.Entities.NonPersistent.CalendarMonth month;
			//	if (target.Schedule.Settings.MondayBased)
			//		month = new Business.Media.Entities.NonPersistent.Calendar.CalendarMonthMediaMondayBased(target);
			//	else
			//		month = new Business.Media.Entities.NonPersistent.Calendar.CalendarMonthMediaSundayBased(target);
			//	month.ImportData(oldCalendarMonths);
			//	target.Months.Add(month);
			//}

			//target.Notes.Clear();
			//foreach (var oldNote in source.Notes)
			//{
			//	Business.Calendar.Entities.NonPersistent.CalendarNote note;
			//	if (target is Business.Media.Entities.NonPersistent.Calendar.BroadcastCalendar)
			//		note = new Business.Media.Entities.NonPersistent.Calendar.MediaDataNote((Business.Media.Entities.NonPersistent.Calendar.BroadcastCalendar)target);
			//	else
			//		note = new Business.Calendar.Entities.NonPersistent.CommonCalendarNote(target);
			//	note.ImportData(oldNote);
			//	target.Notes.Add(note);
			//}

			//target.UpdateDaysCollection();
			//target.UpdateMonthCollection();
			//target.UpdateNotesCollection();
		}

		public static void ImportData(
			this Business.Media.Entities.NonPersistent.Calendar.BroadcastCalendar target,
			Legacy.Media.Entities.Calendar.BroadcastCalendar source)
		{
			target.DataSourceType = (Business.Media.Enums.BroadcastDataTypeEnum)(Int32)source.DataSourceType;
			((Business.Media.Entities.NonPersistent.Calendar.MediaCalendar)target).ImportData(source);
		}

		private static void ImportData(
			this Business.Calendar.Entities.NonPersistent.CalendarDay target,
			Legacy.Common.Entities.Calendar.CalendarDay source)
		{
			target.Date = source.Date;
			target.Comment = source.Comment;
			target.Logo.ImportData(source.Logo);
		}

		private static void ImportData(
			this Business.Calendar.Entities.NonPersistent.CalendarMonth target,
			Legacy.Common.Entities.Calendar.CalendarMonth source)
		{
			target.Date = source.Date;
			target.OutputData.ImportData(source.OutputData);
		}

		private static void ImportData(
			this Business.Calendar.Entities.NonPersistent.CalendarNote target,
			Legacy.Common.Entities.Calendar.CalendarNote source)
		{
			if (source.Note is Legacy.Common.Entities.Common.TextItem)
			{
				var sourceTextItem = (Legacy.Common.Entities.Common.TextItem)source.Note;
				target.Note = new Common.Core.Objects.Output.TextItem(sourceTextItem.Text, sourceTextItem.IsBold);
			}
			else if (source.Note is Legacy.Common.Entities.Common.TextGroup)
			{
				var sourceTextGroup = (Legacy.Common.Entities.Common.TextGroup)source.Note;
				target.Note = new Common.Core.Objects.Output.TextGroup(
					sourceTextGroup.Separator,
					sourceTextGroup.BorderLeft,
					sourceTextGroup.BorderRight);
				((Common.Core.Objects.Output.TextGroup)target.Note).ImportData(sourceTextGroup);
			}

			target.StartDay = source.StartDay;
			target.FinishDay = source.FinishDay;
			target.BackgroundColor = source.BackgroundColor;
			target.UserAdded = source.UserAdded;
		}

		private static void ImportData(
			this Business.Calendar.Entities.NonPersistent.CalendarOutputData target,
			Legacy.Common.Entities.Calendar.CalendarOutputData source)
		{
			target.ShowHeader = source.ShowHeader;
			target.ShowBusinessName = source.ShowBusinessName;
			target.ShowDecisionMaker = source.ShowDecisionMaker;

			target.Header = source.Header;
			target.ApplyForAllBasic = source.ApplyForAllBasic;

			target.ShowCustomComment = source.ShowCustomComment;
			target.CustomComment = source.CustomComment;
			target.ApplyForAllCustomComment = source.ApplyForAllCustomComment;

			target.SlideColor = source.SlideColor;
			target.ApplyForAllThemeColor = source.ApplyForAllThemeColor;
			target.ShowLogo = source.ShowLogo;
			target.ApplyForAllLogo = source.ApplyForAllLogo;
			target.ShowBigDate = source.ShowBigDate;
			if (source.Logo != null)
				target.Logo = Common.Core.Objects.Images.ImageSource.FromImage(source.Logo);
		}
	}
}
