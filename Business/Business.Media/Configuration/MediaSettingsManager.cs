using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Asa.Business.Calendar.Configuration;
using Asa.Business.Media.Interfaces;
using Asa.Business.Solutions.Dashboard.Configuration;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;

namespace Asa.Business.Media.Configuration
{
	public class MediaSettingsManager : IMediaSettingsManager, IDashboardSettingsContainer
	{
		private ThemeSaveHelper _themeSaveHelper;

		public string SaveFolder { get; set; }
		public string SelectedColor { get; set; }
		public bool UseSlideMaster { get; set; }
		public CalendarSettings BroadcastCalendarSettings { get; }

		public string SalesRep { get; set; }

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
				bool tempBool;
				if (Boolean.TryParse(node.InnerText, out tempBool))
					UseSlideMaster = tempBool;
			}
			node = document.SelectSingleNode(@"/Settings/BroadcastCalendarSettings");
			if (node != null)
				BroadcastCalendarSettings.Deserialize(node);
			node = document.SelectSingleNode(@"/Settings/SalesRep");
			if (node != null)
				SalesRep = node.InnerText;
			_themeSaveHelper.Deserialize(document.SelectNodes(@"//Settings/SelectedTheme").OfType<XmlNode>());
		}

		public void SaveSettings()
		{
			var xml = new StringBuilder();
			xml.AppendLine(@"<Settings>");
			xml.AppendLine(_themeSaveHelper.Serialize());
			if (!String.IsNullOrEmpty(SelectedColor))
				xml.AppendLine(@"<SelectedColor>" + SelectedColor.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedColor>");
			xml.AppendLine(@"<UseSlideMaster>" + UseSlideMaster + @"</UseSlideMaster>");
			xml.AppendLine(@"<BroadcastCalendarSettings>" + BroadcastCalendarSettings.Serialize() + @"</BroadcastCalendarSettings>");
			if (!String.IsNullOrEmpty(SalesRep))
				xml.AppendLine(@"<SalesRep>" + SalesRep.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SalesRep>");
			xml.AppendLine(@"</Settings>");
			using (var sw = new StreamWriter(Asa.Common.Core.Configuration.ResourceManager.Instance.AppSettingsFile.LocalPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		public void InitThemeHelper(ThemeManager themeManager)
		{
			_themeSaveHelper = new ThemeSaveHelper(themeManager);
		}

		public string GetSelectedThemeName(SlideType slideType)
		{
			return GetSelectedTheme(slideType).Name;
		}

		public Theme GetSelectedTheme(SlideType slideType)
		{
			return _themeSaveHelper.GetSelectedTheme(slideType);
		}

		public void SetSelectedTheme(SlideType slideType, string themeName)
		{
			_themeSaveHelper.SetSelectedTheme(slideType, themeName);
		}
	}
}
