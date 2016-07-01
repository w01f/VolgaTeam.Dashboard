using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Asa.Business.Online.Configuration
{
	public class DigitalControlsConfiguration
	{
		#region Ribbon Buttons
		public string RibbonGroupDigitalLogoTitle { get; set; }
		public string RibbonButtonDigitalAddTitle { get; set; }
		public string RibbonButtonDigitalAddTooltip { get; set; }
		public string RibbonButtonDigitalCloneTitle { get; set; }
		public string RibbonButtonDigitalCloneTooltip { get; set; }
		public string RibbonButtonDigitalDeleteTitle { get; set; }
		public string RibbonButtonDigitalDeleteTooltip { get; set; }

		public string RibbonButtonMediaDigitalAddTitle { get; set; }
		public string RibbonButtonMediaDigitalAddTooltip { get; set; }
		public string RibbonButtonMediaDigitalDeleteTitle { get; set; }
		public string RibbonButtonMediaDigitalDeleteTooltip { get; set; }
		#endregion

		#region Sections
		public string SectionsHomeTitle { get; set; }
		public string SectionsListTitle { get; set; }
		public string SectionsProductTitle { get; set; }
		public string SectionsProductPackageTitle { get; set; }
		public string SectionsSummaryTitle { get; set; }
		public string SectionsStandalonePackageTitle { get; set; }
		#endregion

		#region List Section
		public string ListColumnsGroupTitle { get; set; }
		public string ListColumnsProductTitle { get; set; }
		public string ListColumnsPricingTitle { get; set; }
		public string ListColumnsOptionsTitle { get; set; }

		public string ListSettingsDimensionTitle { get; set; }
		public string ListSettingsStrategyTitle { get; set; }
		public string ListSettingsLocationTitle { get; set; }
		public string ListSettingsTargetingTitle { get; set; }
		public string ListSettingsRichMediaTitle { get; set; }

		public string ListEditorsGroupTitle { get; set; }
		public string ListEditorsProductTitle { get; set; }
		public string ListEditorsLocationTitle { get; set; }
		public string ListEditorsTargetingTitle { get; set; }
		public string ListEditorsRichMediaTitle { get; set; }
		#endregion

		#region Product Section
		public string ProductEditorsNameTitle { get; set; }
		public string ProductEditorsDescriptionTitle { get; set; }
		public string ProductEditorsSitesTitle { get; set; }
		public string ProductEditorsPricingTitle { get; set; }
		public string ProductEditorsCommentsTitle { get; set; }

		public Image ProductEditorsNameLogo { get; set; }
		public Image ProductEditorsDescriptionLogo { get; set; }
		public Image ProductEditorsSitesLogo { get; set; }
		public Image ProductEditorsPricingLogo { get; set; }
		public Image ProductEditorsCommentsLogo { get; set; }
		#endregion

		#region Product Package Section
		public string ProductPackageColumnsCategoryTitle { get; set; }
		public string ProductPackageColumnsSubCategoryTitle { get; set; }
		public string ProductPackageColumnsProductTitle { get; set; }
		public string ProductPackageColumnsInfoTitle { get; set; }
		public string ProductPackageColumnsLocationTitle { get; set; }
		public string ProductPackageColumnsInvestmentTitle { get; set; }
		public string ProductPackageColumnsImpressionsTitle { get; set; }
		public string ProductPackageColumnsCPMTitle { get; set; }
		public string ProductPackageColumnsRateTitle { get; set; }

		public string ProductPackageSettingsCategoryTitle { get; set; }
		public string ProductPackageSettingsSubCategoryTitle { get; set; }
		public string ProductPackageSettingsProductTitle { get; set; }
		public string ProductPackageSettingsInfoTitle { get; set; }
		public string ProductPackageSettingsLocationTitle { get; set; }
		public string ProductPackageSettingsInvestmentTitle { get; set; }
		public string ProductPackageSettingsImpressionsTitle { get; set; }
		public string ProductPackageSettingsCPMTitle { get; set; }
		public string ProductPackageSettingsRateTitle { get; set; }
		public string ProductPackageSettingsScreenshotTitle { get; set; }
		public string ProductPackageSettingsFormulaTitle { get; set; }
		#endregion

		#region Standalone Package Section
		public string StandalonePackageColumnsCategoryTitle { get; set; }
		public string StandalonePackageColumnsSubCategoryTitle { get; set; }
		public string StandalonePackageColumnsProductTitle { get; set; }
		public string StandalonePackageColumnsInfoTitle { get; set; }
		public string StandalonePackageColumnsLocationTitle { get; set; }
		public string StandalonePackageColumnsInvestmentTitle { get; set; }
		public string StandalonePackageColumnsImpressionsTitle { get; set; }
		public string StandalonePackageColumnsCPMTitle { get; set; }
		public string StandalonePackageColumnsRateTitle { get; set; }

		public string StandalonePackageSettingsCategoryTitle { get; set; }
		public string StandalonePackageSettingsSubCategoryTitle { get; set; }
		public string StandalonePackageSettingsProductTitle { get; set; }
		public string StandalonePackageSettingsInfoTitle { get; set; }
		public string StandalonePackageSettingsLocationTitle { get; set; }
		public string StandalonePackageSettingsInvestmentTitle { get; set; }
		public string StandalonePackageSettingsImpressionsTitle { get; set; }
		public string StandalonePackageSettingsCPMTitle { get; set; }
		public string StandalonePackageSettingsRateTitle { get; set; }
		public string StandalonePackageSettingsScreenshotTitle { get; set; }
		public string StandalonePackageSettingsFormulaTitle { get; set; }
		#endregion

		#region Media Digital Section
		public string DigitalInfoColumnsCategoryTitle { get; set; }
		public string DigitalInfoColumnsSubCategoryTitle { get; set; }
		public string DigitalInfoColumnsProductTitle { get; set; }
		public string DigitalInfoColumnsInfoTitle { get; set; }

		public string DigitalInfoSettingsCategoryTitle { get; set; }
		public string DigitalInfoSettingsSubCategoryTitle { get; set; }
		public string DigitalInfoSettingsProductTitle { get; set; }
		public string DigitalInfoSettingsInfoTitle { get; set; }
		public string DigitalInfoSettingsLogosTitle { get; set; }
		public string DigitalInfoSettingsMontlyInvestmentTitle { get; set; }
		public string DigitalInfoSettingsTotalInvestmentTitle { get; set; }
		#endregion

		public void ApplyValues(string groupName, IList<string> values)
		{
			switch (groupName)
			{
				case "ribbon_group_1_name":
					RibbonGroupDigitalLogoTitle = values.ElementAtOrDefault(0);
					break;
				case "top_level_subtab_names":
					SectionsHomeTitle = values.ElementAtOrDefault(0);
					SectionsListTitle = values.ElementAtOrDefault(1);
					SectionsProductTitle = values.ElementAtOrDefault(2);
					SectionsSummaryTitle = values.ElementAtOrDefault(3);
					SectionsProductPackageTitle = values.ElementAtOrDefault(4);
					SectionsStandalonePackageTitle = values.ElementAtOrDefault(5);
					break;
				case "tab_2_column_header_names":
					ListColumnsGroupTitle = values.ElementAtOrDefault(0);
					ListColumnsProductTitle = values.ElementAtOrDefault(1);
					ListColumnsPricingTitle = values.ElementAtOrDefault(2);
					ListColumnsOptionsTitle = values.ElementAtOrDefault(3);
					break;
				case "tab_2_left_panel_button_names":
					ListSettingsDimensionTitle = values.ElementAtOrDefault(0);
					ListSettingsLocationTitle = values.ElementAtOrDefault(1);
					ListSettingsStrategyTitle = values.ElementAtOrDefault(2);
					ListSettingsTargetingTitle = values.ElementAtOrDefault(3);
					ListSettingsRichMediaTitle = values.ElementAtOrDefault(4);
					break;
				case "tab_2_new_line_placeholder_names":
					ListEditorsGroupTitle = values.ElementAtOrDefault(0);
					ListEditorsProductTitle = values.ElementAtOrDefault(1);
					ListEditorsLocationTitle = values.ElementAtOrDefault(2);
					break;
				case "tab_2_new_line_options_label_names":
					ListEditorsTargetingTitle = values.ElementAtOrDefault(0);
					ListEditorsRichMediaTitle = values.ElementAtOrDefault(1);
					break;
				case "tab_2_add_product_hover_tip":
					RibbonButtonDigitalAddTitle = values.ElementAtOrDefault(0);
					RibbonButtonDigitalAddTooltip = values.ElementAtOrDefault(1);
					break;
				case "tab_2_delete_product_hover_tip":
					RibbonButtonDigitalDeleteTitle = values.ElementAtOrDefault(0);
					RibbonButtonDigitalDeleteTooltip = values.ElementAtOrDefault(1);
					break;
				case "tab_2_clone_product_hover_tip":
					RibbonButtonDigitalCloneTitle = values.ElementAtOrDefault(0);
					RibbonButtonDigitalCloneTooltip = values.ElementAtOrDefault(1);
					break;
				case "tab_3_section_titles":
					ProductEditorsSitesTitle = values.ElementAtOrDefault(0);
					ProductEditorsNameTitle = values.ElementAtOrDefault(1);
					ProductEditorsDescriptionTitle = values.ElementAtOrDefault(2);
					ProductEditorsPricingTitle = values.ElementAtOrDefault(3);
					ProductEditorsCommentsTitle = values.ElementAtOrDefault(4);
					break;
				case "tab_3_section_icons":
					var imageRootFolder = Path.Combine(
						Path.GetDirectoryName(typeof(DigitalControlsConfiguration).Assembly.Location),
						"Data",
						"!Main_Dashboard",
						"Online Source",
						"Icon Images");
					if (!Directory.Exists(imageRootFolder)) break;
					ProductEditorsSitesLogo = !String.IsNullOrEmpty(values.ElementAtOrDefault(0)) ? Image.FromFile(Path.Combine(imageRootFolder, values[0])) : null;
					ProductEditorsNameLogo = !String.IsNullOrEmpty(values.ElementAtOrDefault(1)) ? Image.FromFile(Path.Combine(imageRootFolder, values[1])) : null;
					ProductEditorsDescriptionLogo = !String.IsNullOrEmpty(values.ElementAtOrDefault(2)) ? Image.FromFile(Path.Combine(imageRootFolder, values[2])) : null;
					ProductEditorsPricingLogo = !String.IsNullOrEmpty(values.ElementAtOrDefault(3)) ? Image.FromFile(Path.Combine(imageRootFolder, values[3])) : null;
					ProductEditorsCommentsLogo = !String.IsNullOrEmpty(values.ElementAtOrDefault(4)) ? Image.FromFile(Path.Combine(imageRootFolder, values[4])) : null;
					break;
				case "tab_5_column_header_names":
					ProductPackageColumnsCategoryTitle = values.ElementAtOrDefault(0);
					ProductPackageColumnsSubCategoryTitle = values.ElementAtOrDefault(1);
					ProductPackageColumnsProductTitle = values.ElementAtOrDefault(2);
					ProductPackageColumnsInfoTitle = values.ElementAtOrDefault(3);
					ProductPackageColumnsLocationTitle = values.ElementAtOrDefault(4);
					ProductPackageColumnsInvestmentTitle = values.ElementAtOrDefault(5);
					ProductPackageColumnsImpressionsTitle = values.ElementAtOrDefault(6);
					ProductPackageColumnsCPMTitle = values.ElementAtOrDefault(7);
					ProductPackageColumnsRateTitle = values.ElementAtOrDefault(8);
					break;
				case "tab_5_left_panel_button_names":
					ProductPackageSettingsCategoryTitle = values.ElementAtOrDefault(0);
					ProductPackageSettingsSubCategoryTitle = values.ElementAtOrDefault(1);
					ProductPackageSettingsProductTitle = values.ElementAtOrDefault(2);
					ProductPackageSettingsInfoTitle = values.ElementAtOrDefault(3);
					ProductPackageSettingsLocationTitle = values.ElementAtOrDefault(4);
					ProductPackageSettingsInvestmentTitle = values.ElementAtOrDefault(5);
					ProductPackageSettingsImpressionsTitle = values.ElementAtOrDefault(6);
					ProductPackageSettingsCPMTitle = values.ElementAtOrDefault(7);
					ProductPackageSettingsRateTitle = values.ElementAtOrDefault(8);
					ProductPackageSettingsScreenshotTitle = values.ElementAtOrDefault(9);
					ProductPackageSettingsFormulaTitle = values.ElementAtOrDefault(10);
					break;
				case "tab_6_column_header_names":
					StandalonePackageColumnsCategoryTitle = values.ElementAtOrDefault(0);
					StandalonePackageColumnsSubCategoryTitle = values.ElementAtOrDefault(1);
					StandalonePackageColumnsProductTitle = values.ElementAtOrDefault(2);
					StandalonePackageColumnsInfoTitle = values.ElementAtOrDefault(3);
					StandalonePackageColumnsLocationTitle = values.ElementAtOrDefault(4);
					StandalonePackageColumnsInvestmentTitle = values.ElementAtOrDefault(5);
					StandalonePackageColumnsImpressionsTitle = values.ElementAtOrDefault(6);
					StandalonePackageColumnsCPMTitle = values.ElementAtOrDefault(7);
					StandalonePackageColumnsRateTitle = values.ElementAtOrDefault(8);
					break;
				case "tab_6_left_panel_button_names":
					StandalonePackageSettingsCategoryTitle = values.ElementAtOrDefault(0);
					StandalonePackageSettingsSubCategoryTitle = values.ElementAtOrDefault(1);
					StandalonePackageSettingsProductTitle = values.ElementAtOrDefault(2);
					StandalonePackageSettingsInfoTitle = values.ElementAtOrDefault(3);
					StandalonePackageSettingsLocationTitle = values.ElementAtOrDefault(4);
					StandalonePackageSettingsInvestmentTitle = values.ElementAtOrDefault(5);
					StandalonePackageSettingsImpressionsTitle = values.ElementAtOrDefault(6);
					StandalonePackageSettingsCPMTitle = values.ElementAtOrDefault(7);
					StandalonePackageSettingsRateTitle = values.ElementAtOrDefault(8);
					StandalonePackageSettingsScreenshotTitle = values.ElementAtOrDefault(9);
					StandalonePackageSettingsFormulaTitle = values.ElementAtOrDefault(10);
					break;
				case "column_header_names":
					DigitalInfoColumnsCategoryTitle = values.ElementAtOrDefault(0);
					DigitalInfoColumnsSubCategoryTitle = values.ElementAtOrDefault(1);
					DigitalInfoColumnsProductTitle = values.ElementAtOrDefault(2);
					DigitalInfoColumnsInfoTitle = values.ElementAtOrDefault(3);
					break;
				case "left_panel_button_names":
					DigitalInfoSettingsCategoryTitle = values.ElementAtOrDefault(0);
					DigitalInfoSettingsSubCategoryTitle = values.ElementAtOrDefault(1);
					DigitalInfoSettingsProductTitle = values.ElementAtOrDefault(2);
					DigitalInfoSettingsInfoTitle = values.ElementAtOrDefault(3);
					DigitalInfoSettingsLogosTitle = values.ElementAtOrDefault(4);
					DigitalInfoSettingsMontlyInvestmentTitle = values.ElementAtOrDefault(5);
					DigitalInfoSettingsTotalInvestmentTitle = values.ElementAtOrDefault(6);
					break;
				case "add_product_hover_tip":
					RibbonButtonMediaDigitalAddTitle = values.ElementAtOrDefault(0);
					RibbonButtonMediaDigitalAddTooltip = values.ElementAtOrDefault(1);
					break;
				case "delete_product_hover_tip":
					RibbonButtonMediaDigitalDeleteTitle = values.ElementAtOrDefault(0);
					RibbonButtonMediaDigitalDeleteTooltip = values.ElementAtOrDefault(1);
					break;
			}
		}
	}
}
