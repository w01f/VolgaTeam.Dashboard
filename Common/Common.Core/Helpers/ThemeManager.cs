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
			ApprovedThemes.Add(SlideType.CustomSlide, new List<SlideApprovedThemeInfo>());

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
						slideType = SlideType.StarAppCover;
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

					#region Shift
					case "Shift_01a_cover":
						slideType = SlideType.ShiftCoverA;
						break;
					case "Shift_01b_cover":
						slideType = SlideType.ShiftCoverB;
						break;
					case "Shift_01c_cover":
						slideType = SlideType.ShiftCoverC;
						break;
					case "Shift_01d_cover":
						slideType = SlideType.ShiftCoverD;
						break;
					case "Shift_02a_intro":
						slideType = SlideType.ShiftIntroA;
						break;
					case "Shift_02b_intro":
						slideType = SlideType.ShiftIntroB;
						break;
					case "Shift_02c_intro":
						slideType = SlideType.ShiftIntroC;
						break;
					case "Shift_02d_intro":
						slideType = SlideType.ShiftIntroD;
						break;
					case "Shift_03a_agenda":
						slideType = SlideType.ShiftAgendaA;
						break;
					case "Shift_03b_agenda":
						slideType = SlideType.ShiftAgendaB;
						break;
					case "Shift_03c_agenda":
						slideType = SlideType.ShiftAgendaC;
						break;
					case "Shift_03d_agenda":
						slideType = SlideType.ShiftAgendaD;
						break;
					case "Shift_04a_goals":
						slideType = SlideType.ShiftGoalsA;
						break;
					case "Shift_04b_goals":
						slideType = SlideType.ShiftGoalsB;
						break;
					case "Shift_04c_goals":
						slideType = SlideType.ShiftGoalsC;
						break;
					case "Shift_04d_goals":
						slideType = SlideType.ShiftGoalsD;
						break;
					case "Shift_05a_mktopp":
						slideType = SlideType.ShiftMarketA;
						break;
					case "Shift_05b_mktopp":
						slideType = SlideType.ShiftMarketB;
						break;
					case "Shift_05c_mktopp":
						slideType = SlideType.ShiftMarketC;
						break;
					case "Shift_05d_mktopp":
						slideType = SlideType.ShiftMarketD;
						break;
					case "Shift_05e_mktopp":
						slideType = SlideType.ShiftMarketE;
						break;
					case "Shift_06a_prtnr":
						slideType = SlideType.ShiftPartnershipA;
						break;
					case "Shift_06b_prtnr":
						slideType = SlideType.ShiftPartnershipB;
						break;
					case "Shift_06c_prtnr":
						slideType = SlideType.ShiftPartnershipC;
						break;
					case "Shift_06d_prtnr":
						slideType = SlideType.ShiftPartnershipD;
						break;
					case "Shift_07a1_mktneeds":
						slideType = SlideType.ShiftNeedsSolutionsA1;
						break;
					case "Shift_07a2_mktneeds":
						slideType = SlideType.ShiftNeedsSolutionsA2;
						break;
					case "Shift_07a3_mktneeds":
						slideType = SlideType.ShiftNeedsSolutionsA3;
						break;
					case "Shift_07a4_mktneeds":
						slideType = SlideType.ShiftNeedsSolutionsA4;
						break;
					case "Shift_07b1_mktneeds":
						slideType = SlideType.ShiftNeedsSolutionsB1;
						break;
					case "Shift_07b2_mktneeds":
						slideType = SlideType.ShiftNeedsSolutionsB2;
						break;
					case "Shift_07b3_mktneeds":
						slideType = SlideType.ShiftNeedsSolutionsB3;
						break;
					case "Shift_07b4_mktneeds":
						slideType = SlideType.ShiftNeedsSolutionsB4;
						break;
					case "Shift_07c1_our_solutions":
						slideType = SlideType.ShiftNeedsSolutionsC1;
						break;
					case "Shift_07c2_our_solutions":
						slideType = SlideType.ShiftNeedsSolutionsC2;
						break;
					case "Shift_07c3_our_solutions":
						slideType = SlideType.ShiftNeedsSolutionsC3;
						break;
					case "Shift_07c4_our_solutions":
						slideType = SlideType.ShiftNeedsSolutionsC4;
						break;
					case "Shift_07d1_our_solutions":
						slideType = SlideType.ShiftNeedsSolutionsD1;
						break;
					case "Shift_07d2_our_solutions":
						slideType = SlideType.ShiftNeedsSolutionsD2;
						break;
					case "Shift_07d3_our_solutions":
						slideType = SlideType.ShiftNeedsSolutionsD3;
						break;
					case "Shift_07d4_our_solutions":
						slideType = SlideType.ShiftNeedsSolutionsD4;
						break;
					case "Shift_07e1_mktneeds":
						slideType = SlideType.ShiftNeedsSolutionsE1;
						break;
					case "Shift_07e2_mktneeds":
						slideType = SlideType.ShiftNeedsSolutionsE2;
						break;
					case "Shift_07e3_mktneeds":
						slideType = SlideType.ShiftNeedsSolutionsE3;
						break;
					case "Shift_07e4_mktneeds":
						slideType = SlideType.ShiftNeedsSolutionsE4;
						break;
					case "Shift_07f1_our_solutions":
						slideType = SlideType.ShiftNeedsSolutionsF1;
						break;
					case "Shift_07f2_our_solutions":
						slideType = SlideType.ShiftNeedsSolutionsF2;
						break;
					case "Shift_07f3_our_solutions":
						slideType = SlideType.ShiftNeedsSolutionsF3;
						break;
					case "Shift_07f4_our_solutions":
						slideType = SlideType.ShiftNeedsSolutionsF4;
						break;
					case "Shift_08_cbc":
						slideType = SlideType.ShiftCBC;
						break;
					case "Shift_09_integrated_solution":
						slideType = SlideType.ShiftIntegratedSolution;
						break;
					case "Shift_10_investment":
						slideType = SlideType.ShiftInvestment;
						break;
					case "Shift_11_relationship_next_steps":
						slideType = SlideType.ShiftNextSteps;
						break;
					case "Shift_12_agreement_contract":
						slideType = SlideType.ShiftContract;
						break;
					case "Shift_13_support_materials":
						slideType = SlideType.ShiftSupportMaterials;
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

		public IList<Theme> GetThemes(SlideType slideType)
		{
			if (!ApprovedThemes.ContainsKey(slideType))
				return _themes;
			return ApprovedThemes[slideType]
				.OrderByDescending(themeInfo => themeInfo.IsDefault)
				.Select(themeInfo => _themes.FirstOrDefault(theme => theme.Name == themeInfo.ThemName))
				.Where(theme => theme != null)
				.ToList();
		}
	}
}
