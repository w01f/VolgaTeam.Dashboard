using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Asa.Business.Calendar.Configuration;
using Asa.Business.Media.Interfaces;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;

namespace Asa.Business.Media.Configuration
{
	public class MediaSettingsManager : IMediaSettingsManager
	{
		private ThemeSaveHelper _themeSaveHelper;

		public string SaveFolder { get; set; }
		public string SelectedColor { get; set; }
		public bool UseSlideMaster { get; set; }
		public bool ApplyThemeForAllSlideTypes { get; set; }
		public CalendarSettings BroadcastCalendarSettings { get; }

		public string SalesRep { get; set; }

		public string SelectedStarOutputItemsEncoded { get; set; }

		public MediaSettingsManager()
		{
			BroadcastCalendarSettings = new CalendarSettings();
		}

		public void LoadSettings()
		{
			SettingsManager.Instance.LoadSharedSettings();

			if (!Asa.Common.Core.Configuration.ResourceManager.Instance.AppSettingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(Asa.Common.Core.Configuration.ResourceManager.Instance.AppSettingsFile.LocalPath);

			var node = document.SelectSingleNode(@"/Settings/SelectedColor");
			if (node != null)
				SelectedColor = node.InnerText;
			node = document.SelectSingleNode(@"/Settings/UseSlideMaster");
			if (node != null)
			{
				if (Boolean.TryParse(node.InnerText, out var tempBool))
					UseSlideMaster = tempBool;
			}
			node = document.SelectSingleNode(@"/Settings/BroadcastCalendarSettings");
			if (node != null)
				BroadcastCalendarSettings.Deserialize(node);
			node = document.SelectSingleNode(@"/Settings/SalesRep");
			if (node != null)
				SalesRep = node.InnerText;
			node = document.SelectSingleNode(@"/Settings/SelectedStarOutputItemsEncoded");
			if (node != null)
				SelectedStarOutputItemsEncoded = node.InnerText;
			node = document.SelectSingleNode(@"/Settings/ApplyThemeForAllSlideTypes");
			if (node != null)
			{
				if (Boolean.TryParse(node.InnerText, out var tempBool))
					ApplyThemeForAllSlideTypes = tempBool;
			}
			_themeSaveHelper.Deserialize(document.SelectNodes(@"//Settings/SelectedTheme").OfType<XmlNode>());
		}

		public void SaveSettings()
		{
			var xml = new StringBuilder();
			xml.AppendLine(@"<Settings>");
			if (!String.IsNullOrEmpty(SelectedColor))
				xml.AppendLine(@"<SelectedColor>" + SelectedColor.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedColor>");
			xml.AppendLine(@"<UseSlideMaster>" + UseSlideMaster + @"</UseSlideMaster>");
			xml.AppendLine(@"<BroadcastCalendarSettings>" + BroadcastCalendarSettings.Serialize() + @"</BroadcastCalendarSettings>");
			if (!String.IsNullOrEmpty(SalesRep))
				xml.AppendLine(@"<SalesRep>" + SalesRep.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SalesRep>");
			if (!String.IsNullOrEmpty(SelectedStarOutputItemsEncoded))
				xml.AppendLine(@"<SelectedStarOutputItemsEncoded>" + SelectedStarOutputItemsEncoded.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedStarOutputItemsEncoded>");
			xml.AppendLine(@"<ApplyThemeForAllSlideTypes>" + ApplyThemeForAllSlideTypes + @"</ApplyThemeForAllSlideTypes>");
			xml.AppendLine(_themeSaveHelper.Serialize());
			xml.AppendLine(@"</Settings>");
			using (var sw = new StreamWriter(Asa.Common.Core.Configuration.ResourceManager.Instance.AppSettingsFile.LocalPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		public void InitThemeHelper(ThemeManager themeManager)
		{
			_themeSaveHelper = new ThemeSaveHelper(
				themeManager,
				new[]
				{
					SlideType.TVSchedulePrograms,
					SlideType.TVScheduleDigital,
					SlideType.TVScheduleSummary,

					SlideType.TVSnapshotPrograms,
					SlideType.TVSnapshotDigital,
					SlideType.TVSnapshotSummary,

					SlideType.TVOptionsPrograms,
					SlideType.TVOptionsDigital,
					SlideType.TVOptionstSummary,

					SlideType.RadioSchedulePrograms,
					SlideType.RadioScheduleDigital,
					SlideType.RadioScheduleSummary,

					SlideType.RadioSnapshotPrograms,
					SlideType.RadioSnapshotDigital,
					SlideType.RadioSnapshotSummary,

					SlideType.RadioOptionsPrograms,
					SlideType.RadioOptionsDigital,
					SlideType.RadioOptionstSummary,

					SlideType.DigitalProducts,
					SlideType.DigitalSummary,
					SlideType.DigitalProductPackage,
					SlideType.DigitalStandalonePackage,

					SlideType.Cleanslate,
					SlideType.Cover,
					SlideType.LeadoffStatement,
					SlideType.ClientGoals,
					SlideType.TargetCustomers,
					SlideType.SimpleSummary,
				}
			);
		}

		public string GetSelectedThemeName(SlideType slideType)
		{
			return GetSelectedTheme(slideType).Name;
		}

		public Theme GetSelectedTheme(SlideType slideType)
		{
			return _themeSaveHelper.GetSelectedTheme(slideType);
		}

		public void SetSelectedTheme(SlideType slideType, string themeName, bool applyForAllSlidesTypes)
		{
			ApplyThemeForAllSlideTypes = applyForAllSlidesTypes;
			_themeSaveHelper.SetSelectedTheme(slideType, themeName, applyForAllSlidesTypes);
		}
	}
}
