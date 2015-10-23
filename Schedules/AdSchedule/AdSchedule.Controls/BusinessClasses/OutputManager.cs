using System;
using Asa.Core.Calendar;
using Asa.Core.Common;

namespace Asa.AdSchedule.Controls.BusinessClasses
{
	public class OutputManager
	{
		public const int Columns = 12;

		public const int DetailedGridGridBasedRowsCountWithNotes = 8;
		public const int DetailedGridGridBasedRowsCountWithoutNotes = 10;

		public const int MultiGridGridBasedRowsCountWithNotes = 8;
		public const int MultiGridGridBasedRowsCountWithoutNotes = 10;

		public CalendarTemplatesManager TemplatesManager { get; private set; }
		public OutputColorList CalendarColors { get; private set; }
		public event EventHandler<EventArgs> ColorsChanged;

		public void Init()
		{
			TemplatesManager = new CalendarTemplatesManager();
			TemplatesManager.LoadCalendarTemplates();

			CalendarColors = new OutputColorList();
			UpdateColors();
		}

		public void UpdateColors()
		{
			CalendarColors.Load(
				new StorageDirectory(ResourceManager.Instance.CalendarSlideTemplatesFolder.RelativePathParts
					.Merge(new[]
					{
						"!Calendar_Images",
					})));
			if (ColorsChanged != null)
				ColorsChanged(this, EventArgs.Empty);
		}

		private string GetScheduleTemplateFile(string[] fileName)
		{
			var file = new StorageFile(ResourceManager.Instance.ScheduleSlideTemplatesFolder.RelativePathParts
				.Merge(new[]
					{
						PowerPointManager.Instance.SlideSettings.SlideFolder.ToLower(),
						""
					})
				.Merge(fileName));
			return file.LocalPath;
		}

		private string GetCalendarTemplateFile(string[] fileName)
		{
			var file = new StorageFile(ResourceManager.Instance.CalendarSlideTemplatesFolder.RelativePathParts
				.Merge(fileName));
			return file.LocalPath;
		}

		public string GetCalendarFile(string fileName)
		{
			return GetCalendarTemplateFile(new[]
			{
				String.Format("{0}new", PowerPointManager.Instance.SlideSettings.SlideFolder),
				fileName
			});
		}

		public string GetCalendarBackgroundFile(string color, DateTime calendarMonthDate, bool showBigDates)
		{
			return GetCalendarTemplateFile(new[]
			{
				"!Calendar_Images",
				color,
				calendarMonthDate.ToString("yyyy"),
				String.Format("{0}{1}.png", calendarMonthDate.ToString("MMM").ToLower(), (showBigDates ? "1" : "2"))
			});
		}
	}
}