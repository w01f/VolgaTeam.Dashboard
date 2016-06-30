using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.Core.Objects.Themes;

namespace Asa.Common.Core.Helpers
{
	public class ThemeManager
	{
		private readonly List<Theme> _themes = new List<Theme>();

		public Dictionary<SlideType, List<string>> ApprovedThemes { get; private set; }

		public event EventHandler<EventArgs> ThemesChanged;

		public ThemeManager()
		{
			ApprovedThemes = new Dictionary<SlideType, List<string>>();
		}

		private void LoadApprovedThemes(StorageDirectory root)
		{
			var contentFile = new StorageFile(root.GetParentFolder().RelativePathParts.Merge("ApprovedThemes.xml"));

			if (!contentFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(contentFile.LocalPath);

			foreach (var slideNode in document.SelectNodes(@"//Root/Slide").OfType<XmlNode>())
			{
				var slideAttribute = slideNode.Attributes["Name"];
				if (slideAttribute == null) continue;
				var slideType = SlideType.None;
				switch (slideAttribute.Value)
				{
					#region Dashboard
					case "Cleanslate":
						slideType = SlideType.Cleanslate;
						break;
					case "Cover":
						slideType = SlideType.Cover;
						break;
					case "Intro":
						slideType = SlideType.LeadoffStatement;
						break;
					case "NeedsAnalysis":
						slideType = SlideType.ClientGoals;
						break;
					case "TargetCustomers":
						slideType = SlideType.TargetCustomers;
						break;
					case "ClosingSummary":
						slideType = SlideType.SimpleSummary;
						break;
					#endregion

					#region Online Schedule
					case "OnlineDigitalProduct":
						slideType = SlideType.OnlineDigitalProduct;
						break;
					case "OnlineWebPackage":
						slideType = SlideType.OnlineWebPackage;
						break;
					case "OnlineAdPlan":
						slideType = SlideType.OnlineAdPlan;
						break;
					#endregion

					#region TV Schedule
					case "TVDigitalProduct":
						slideType = SlideType.TVDigitalProduct;
						break;
					case "TVWeeklySchedule":
					case "TVMonthlySchedule":
						slideType = SlideType.TVProgramSchedule;
						break;
					case "TVSnapshot":
						slideType = SlideType.TVSnapshot;
						break;
					case "TVOptions":
						slideType = SlideType.TVOptions;
						break;
					#endregion

					#region Radio Schedule
					case "RadioDigitalProduct":
						slideType = SlideType.RadioDigitalProduct;
						break;
					case "RadioWeeklySchedule":
					case "RadioMonthlySchedule":
						slideType = SlideType.RadioProgramSchedule;
						break;
					case "RadioSnapshot":
						slideType = SlideType.RadioSnapshot;
						break;
					case "RadioOptions":
						slideType = SlideType.RadioOptions;
						break;
					#endregion
				}
				if (slideType == SlideType.None) continue;
				foreach (var themeNode in slideNode.SelectNodes("Theme").OfType<XmlNode>())
				{
					if (!ApprovedThemes.ContainsKey(slideType))
						ApprovedThemes.Add(slideType, new List<string>());
					ApprovedThemes[slideType].Add(themeNode.InnerText);
				}
			}
		}

		public void Load()
		{
			_themes.Clear();
			var storageDirectory = new StorageDirectory(ResourceManager.Instance.ThemesFolder.RelativePathParts.Merge(PowerPointManager.Instance.SlideSettings.SlideMasterFolder));
			if (!storageDirectory.ExistsLocal()) return;

			LoadApprovedThemes(storageDirectory);

			foreach (var themeFolder in storageDirectory.GetLocalFolders())
			{
				var theme = new Theme(themeFolder);
				theme.Load();
				foreach (var approvedTheme in ApprovedThemes.Where(approvedTheme => approvedTheme.Value.Any(t => t.Equals(theme.Name))))
					theme.ApprovedSlides.Add(approvedTheme.Key);
				_themes.Add(theme);
			}
			_themes.Sort((x, y) => x.Order.CompareTo(y.Order));

			ThemesChanged?.Invoke(this, EventArgs.Empty);
		}

		public IEnumerable<Theme> GetThemes(SlideType slideType)
		{
			return _themes.Where(t => t.ApprovedSlides.Contains(slideType) || !ApprovedThemes.ContainsKey(slideType));
		}
	}
}
