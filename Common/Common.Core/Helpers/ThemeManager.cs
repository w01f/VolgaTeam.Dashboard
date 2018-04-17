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

		public Dictionary<SlideType, List<SlideApprovedThemeInfo>> ApprovedThemes { get; }

		public event EventHandler<EventArgs> ThemesChanged;

		public ThemeManager()
		{
			ApprovedThemes = new Dictionary<SlideType, List<SlideApprovedThemeInfo>>();
		}

		private void LoadApprovedThemes(StorageDirectory root)
		{
			var contentFile = new StorageFile(root.RelativePathParts.Merge("ApprovedThemes.xml"));

			ApprovedThemes.Clear();

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

					#region TV Schedule
					case "TVScheduleTV":
						slideType = SlideType.TVSchedulePrograms;
						break;
					case "TVScheduleDigital":
						slideType = SlideType.TVScheduleDigital;
						break;
					case "TVScheduleSummary":
						slideType = SlideType.TVScheduleSummary;
						break;

					case "TVOptionsTV":
						slideType = SlideType.TVOptionsPrograms;
						break;
					case "TVOptionsDigital":
						slideType = SlideType.TVOptionsDigital;
						break;
					case "TVOptionsTVSummary":
						slideType = SlideType.TVOptionstSummary;
						break;

					case "TVSnapTV":
						slideType = SlideType.TVSnapshotPrograms;
						break;
					case "TVSnapDigital":
						slideType = SlideType.TVSnapshotDigital;
						break;
					case "TVSnapTVSummary":
						slideType = SlideType.TVSnapshotSummary;
						break;
					#endregion

					#region Radio Schedule
					case "RadioScheduleRadio":
						slideType = SlideType.RadioSchedulePrograms;
						break;
					case "RadioScheduleDigital":
						slideType = SlideType.RadioScheduleDigital;
						break;
					case "RadioScheduleSummary":
						slideType = SlideType.RadioScheduleSummary;
						break;

					case "RadioOptionsRadio":
						slideType = SlideType.RadioOptionsPrograms;
						break;
					case "RadioOptionsDigital":
						slideType = SlideType.RadioOptionsDigital;
						break;
					case "RadioOptionsRadioSummary":
						slideType = SlideType.RadioOptionstSummary;
						break;

					case "RadioSnapRadio":
						slideType = SlideType.RadioSnapshotPrograms;
						break;
					case "RadioSnapDigital":
						slideType = SlideType.RadioSnapshotDigital;
						break;
					case "RadioSnapRadioSummary":
						slideType = SlideType.RadioSnapshotSummary;
						break;
					#endregion

					#region Digital
					case "DigitalPlanner":
						slideType = SlideType.DigitalProducts;
						break;
					case "DigitalWrapup":
						slideType = SlideType.DigitalSummary;
						break;
					case "DigitalPkgA":
						slideType = SlideType.DigitalProductPackage;
						break;
					case "DigitalPkgB":
						slideType = SlideType.DigitalStandalonePackage;
						break;
					#endregion

					#region StarApp
					case "Star_01_cover":
						slideType = SlideType.Cover;
						break;
					case "Star_02_cna":
						slideType = SlideType.StarAppCNA;
						break;
					case "Star_03_fishing":
						slideType = SlideType.StarAppFishing;
						break;
					case "Star_04_customer":
						slideType = SlideType.StarAppCustomer;
						break;
					case "Star_05_share":
						slideType = SlideType.StarAppShare;
						break;
					case "Star_06_roi":
						slideType = SlideType.StarAppROI;
						break;
					case "Star_07_market":
						slideType = SlideType.StarAppMarket;
						break;
					case "Star_08_video":
						slideType = SlideType.StarAppVideo;
						break;
					case "Star_09_audience":
						slideType = SlideType.StarAppAudience;
						break;
					case "Star_10_solution":
						slideType = SlideType.StarAppSolution;
						break;
					case "Star_11_closers":
						slideType = SlideType.StarAppClosers;
						break;
					#endregion
				}
				if (slideType == SlideType.None) continue;
				foreach (var themeNode in slideNode.SelectNodes("Theme").OfType<XmlNode>())
				{
					if (!ApprovedThemes.ContainsKey(slideType))
						ApprovedThemes.Add(slideType, new List<SlideApprovedThemeInfo>());
					var defaultAttribute = themeNode.Attributes["Default"];
					ApprovedThemes[slideType].Add(new SlideApprovedThemeInfo { ThemName = themeNode.InnerText, IsDefault = defaultAttribute != null });
				}
			}
		}

		public void Load()
		{
			_themes.Clear();
			var storageDirectory = new StorageDirectory(ResourceManager.Instance.ThemesFolder.RelativePathParts.Merge(SlideSettingsManager.Instance.SlideSettings.SlideMasterFolder));
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

			ThemesChanged?.Invoke(null, EventArgs.Empty);
		}

		public IEnumerable<Theme> GetThemes(SlideType slideType)
		{
			if (!ApprovedThemes.ContainsKey(slideType))
				return _themes;
			return ApprovedThemes[slideType]
				.OrderByDescending(themeInfo => themeInfo.IsDefault)
				.Select(themeInfo => _themes.FirstOrDefault(theme => theme.Name == themeInfo.ThemName))
				.Where(theme => theme != null);
		}
	}
}
