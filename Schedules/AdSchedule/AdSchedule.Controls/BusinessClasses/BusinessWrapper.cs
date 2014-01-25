﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;

namespace NewBizWiz.AdSchedule.Controls.BusinessClasses
{
	public class BusinessWrapper
	{
		private static readonly BusinessWrapper _instance = new BusinessWrapper();
		private readonly ThemeSaveHelper _themeSaveHelper;

		private BusinessWrapper()
		{
			ScheduleManager = new ScheduleManager();
			HelpManager = new HelpManager(Core.AdSchedule.SettingsManager.Instance.HelpLinksPath);
			OutputManager = new OutputManager();
			RateCardManager = new RateCardManager(Core.AdSchedule.SettingsManager.Instance.RateCardPath);
			TabPageManager = new TabPageManager(Path.Combine(Path.GetDirectoryName(typeof(TabPageManager).Assembly.Location), "adsched_tab_names.xml"));
			ThemeManager = new ThemeManager(Path.Combine(Core.Common.SettingsManager.Instance.ThemeCollectionPath, Core.Common.SettingsManager.Instance.SlideMasterFolder));
			_themeSaveHelper = new ThemeSaveHelper(ThemeManager);
			LoadLocalSettings();
		}

		public static BusinessWrapper Instance
		{
			get { return _instance; }
		}

		public ScheduleManager ScheduleManager { get; private set; }
		public HelpManager HelpManager { get; private set; }
		public OutputManager OutputManager { get; private set; }
		public RateCardManager RateCardManager { get; private set; }
		public TabPageManager TabPageManager { get; private set; }
		public ThemeManager ThemeManager { get; private set; }

		public string GetSelectedTheme(SlideType slideType)
		{
			return _themeSaveHelper.GetSelectedTheme(slideType).Name;
		}

		public void SetSelectedTheme(SlideType slideType, string themeName)
		{
			_themeSaveHelper.SetSelectedTheme(slideType, themeName);
		}

		public void LoadLocalSettings()
		{
			if (!File.Exists(Core.AdSchedule.SettingsManager.Instance.LocalSettingsPath)) return;
			var document = new XmlDocument();
			document.Load(Core.AdSchedule.SettingsManager.Instance.LocalSettingsPath);
			_themeSaveHelper.Deserialize(document.SelectNodes(@"//LocalSettings/SelectedTheme").OfType<XmlNode>());
		}

		public void SaveLocalSettings()
		{
			var xml = new StringBuilder();
			xml.AppendLine(@"<LocalSettings>");
			xml.AppendLine(_themeSaveHelper.Serialize());
			xml.AppendLine(@"</LocalSettings>");
			var userConfigurationPath = Path.Combine(Core.AdSchedule.SettingsManager.Instance.LocalSettingsPath);
			using (var sw = new StreamWriter(userConfigurationPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}
	}
}