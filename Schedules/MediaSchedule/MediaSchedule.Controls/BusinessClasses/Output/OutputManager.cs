using System;
using Asa.Business.Media.Configuration;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Media.Controls.BusinessClasses.Output
{
	public class OutputManager
	{
		public OutputColorList ScheduleColors { get; private set; }
		public OutputColorList SnapshotColors { get; private set; }
		public OutputColorList OptionsColors { get; private set; }
		public OutputColorList CalendarColors { get; private set; }

		public event EventHandler<EventArgs> ColorsChanged;

		public StorageDirectory ContractTemplateFolder
		{
			get
			{
				return new StorageDirectory(Common.Core.Configuration.ResourceManager.Instance.ScheduleSlideTemplatesFolder.RelativePathParts
					.Merge(new[]
					{
						PowerPointManager.Instance.SlideSettings.SlideFolder.ToLower(),
						String.Format("{0} Slides",MediaMetaData.Instance.DataTypeString),
						"legal"
					}));
			}
		}

		public void Init()
		{
			ScheduleColors = new OutputColorList();
			SnapshotColors = new OutputColorList();
			OptionsColors = new OutputColorList();
			CalendarColors = new OutputColorList();

			UpdateColors();
		}

		public void UpdateColors()
		{
			ScheduleColors.Load(
				new StorageDirectory(Common.Core.Configuration.ResourceManager.Instance.ScheduleSlideTemplatesFolder.RelativePathParts
					.Merge(new[]
					{
						PowerPointManager.Instance.SlideSettings.SlideFolder.ToLower(),
						String.Format("{0} Slides", MediaMetaData.Instance.DataTypeString),
						"tables"
					})));
			SnapshotColors.Load(
				new StorageDirectory(Common.Core.Configuration.ResourceManager.Instance.ScheduleSlideTemplatesFolder.RelativePathParts
					.Merge(new[]
					{
						PowerPointManager.Instance.SlideSettings.SlideFolder.ToLower(),
						String.Format("{0} Slides", MediaMetaData.Instance.DataTypeString),
						"snapshot"
					})));
			OptionsColors.Load(
				new StorageDirectory(Common.Core.Configuration.ResourceManager.Instance.ScheduleSlideTemplatesFolder.RelativePathParts
					.Merge(new[]
					{
						PowerPointManager.Instance.SlideSettings.SlideFolder.ToLower(),
						String.Format("{0} Slides", MediaMetaData.Instance.DataTypeString),
						"options"
					})));
			CalendarColors.Load(
				new StorageDirectory(Common.Core.Configuration.ResourceManager.Instance.CalendarSlideTemplatesFolder.RelativePathParts
					.Merge(new[]
					{
						"broadcast_cal",
						"broadcast_images",
					})));
			ColorsChanged?.Invoke(this, EventArgs.Empty);
		}

		private string GetScheduleTemplateFile(string[] fileName)
		{
			var file = new StorageFile(Common.Core.Configuration.ResourceManager.Instance.ScheduleSlideTemplatesFolder.RelativePathParts
				.Merge(new[]
					{
						PowerPointManager.Instance.SlideSettings.SlideFolder.ToLower(),
						String.Format("{0} Slides",MediaMetaData.Instance.DataTypeString)
					})
				.Merge(fileName));
			return file.LocalPath;
		}

		public string GetMediaOneSheetFile(
			string color,
			bool includeDigital,
			int programsPerSlide,
			int spotsPerSlide)
		{
			return GetScheduleTemplateFile(new[]
			{
				"tables",
				color,
				includeDigital ? "3_broadcast_and_digital" : "1_broadcast_only",
				String.Format("{0}_programs",programsPerSlide),
				String.Format("{0}-{1}.pptx",programsPerSlide,spotsPerSlide)
			});
		}

		public string GetDigitalOneSheetFile(
			string color,
			int productsPerSlide)
		{
			return GetScheduleTemplateFile(new[]
			{
				"tables",
				color,
				"2_digital_only",
				String.Format("{0}_products", productsPerSlide),
				String.Format("{0}-0.pptx", productsPerSlide)
			});
		}

		public string GetSnapshotItemFile(
			string color,
			bool showLogo,
			int programsPerSlide,
			string slideSuffix
			)
		{
			return GetScheduleTemplateFile(new[]
			{
				"snapshot",
				color,
				showLogo ? "logo" : "no_logo",
				String.Format("1s{0}r",programsPerSlide),
				String.Format("{0}rows_{1}.pptx",programsPerSlide,slideSuffix)
			});
		}

		public string GetSnapshotSummaryFile(string color, int columnsCount)
		{
			return GetScheduleTemplateFile(new[]
			{
				"snapshot",
				color,
				"summary",
				String.Format("snapshot_summary_{0}.pptx",columnsCount)
			});
		}

		public string GetOptionsItemFile(
			string color,
			bool showLogo
			)
		{
			return GetScheduleTemplateFile(new[]
				{
					"options",
					color,
					String.Format("options{0}.pptx",showLogo ? "_logo" : String.Empty)
				});
		}

		public string GetOptionsSummaryFile(string color, int columnsCount)
		{
			return GetScheduleTemplateFile(new[]
			{
				"options",
				color,
				"summary",
				String.Format("options_summary_{0}.pptx",columnsCount)
			});
		}

		public string GetOptionsColumnsWidthFile()
		{
			return GetScheduleTemplateFile(new[]
			{
				"options",
				"table_column_widths.txt"
			});
		}

		private string GetCalendarTemplateFile(string[] fileName)
		{
			var file = new StorageFile(Common.Core.Configuration.ResourceManager.Instance.CalendarSlideTemplatesFolder.RelativePathParts
				.Merge(new[]
					{
						"broadcast_cal"
					})
				.Merge(fileName));
			return file.LocalPath;
		}

		public string GetCalendarFile(bool showLogo, int daysCount)
		{
			return GetCalendarTemplateFile(new[]
			{
				"broadcast_slides",
				String.Format("Broadcast_{0}_{1}_{2}.pptx",
					showLogo ? "logo" : "no_logo",
					daysCount,
					PowerPointManager.Instance.SlideSettings.SlideFolder.Replace("Slides", ""))
			});
		}

		public string GetCalendarBackgroundFile(string color, DateTime calendarMonthDate, bool showBigDates)
		{
			return GetCalendarTemplateFile(new[]
			{
				"broadcast_images",
				color,
				calendarMonthDate.ToString("yyyy"),
				String.Format("{0}{1}.png", calendarMonthDate.ToString("MMM").ToLower(), (showBigDates ? "1" : "2"))
			});
		}
	}
}