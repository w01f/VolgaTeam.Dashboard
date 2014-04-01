using System;
using NewBizWiz.Core.Calendar;
using SettingsManager = NewBizWiz.Core.Common.SettingsManager;

namespace NewBizWiz.Calendar.Controls.BusinessClasses
{
	public class OutputManager
	{
		private const string CalendarTemlatesFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\Calendar\{1}";
		public const string CalendarBackgroundFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\Calendar\!Calendar_Images";
		public const string BackgroundFilePath = @"{0}\{1}";
		public static string MasterWizardsRootFolderPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Dashboard", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

		public CalendarTemplatesManager TemplatesManager { get; private set; }

		public OutputManager()
		{
			TemplatesManager = new CalendarTemplatesManager();
		}

		public string CalendarTemlatesFolderPath
		{
			get { return string.Format(CalendarTemlatesFolderName, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), SettingsManager.Instance.SlideFolder + "new"); }
		}

		public string CalendarBackgroundFolderPath
		{
			get { return string.Format(CalendarBackgroundFolderName, Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}
	}
}