using System.Collections.Generic;
using System.Linq;

namespace Asa.Business.Online.Configuration
{
	public class DigitalControlsConfiguration
	{
		#region Ribbon Buttons
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
		public string SectionsListTitle { get; set; }
		public string SectionsProductTitle { get; set; }
		public string SectionsPackageTitle { get; set; }
		public string SectionsSummaryTitle { get; set; }
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
		#endregion

		#region Package Section
		public string PackageColumnsCategoryTitle { get; set; }
		public string PackageColumnsSubCategoryTitle { get; set; }
		public string PackageColumnsProductTitle { get; set; }
		public string PackageColumnsInfoTitle { get; set; }
		public string PackageColumnsCommentsTitle { get; set; }
		public string PackageColumnsInvestmentTitle { get; set; }
		public string PackageColumnsImpressionsTitle { get; set; }
		public string PackageColumnsCPMTitle { get; set; }
		public string PackageColumnsRateTitle { get; set; }

		public string PackageSettingsCategoryTitle { get; set; }
		public string PackageSettingsSubCategoryTitle { get; set; }
		public string PackageSettingsProductTitle { get; set; }
		public string PackageSettingsInfoTitle { get; set; }
		public string PackageSettingsCommentsTitle { get; set; }
		public string PackageSettingsInvestmentTitle { get; set; }
		public string PackageSettingsImpressionsTitle { get; set; }
		public string PackageSettingsCPMTitle { get; set; }
		public string PackageSettingsRateTitle { get; set; }
		public string PackageSettingsScreenshotTitle { get; set; }
		public string PackageSettingsFormulaTitle { get; set; }
		#endregion

		#region Media Digital Section
		public string MediaDigitalColumnsCategoryTitle { get; set; }
		public string MediaDigitalColumnsSubCategoryTitle { get; set; }
		public string MediaDigitalColumnsProductTitle { get; set; }
		public string MediaDigitalColumnsInfoTitle { get; set; }

		public string MediaDigitalSettingsCategoryTitle { get; set; }
		public string MediaDigitalSettingsSubCategoryTitle { get; set; }
		public string MediaDigitalSettingsProductTitle { get; set; }
		public string MediaDigitalSettingsInfoTitle { get; set; }
		public string MediaDigitalSettingsLogosTitle { get; set; }
		public string MediaDigitalSettingsMontlyInvestmentTitle { get; set; }
		public string MediaDigitalSettingsTotalInvestmentTitle { get; set; }
		#endregion

		public void ApplyValues(string groupName, IList<string> values)
		{
			switch (groupName)
			{
				case "tab_1_top_level_tab_names":
					SectionsListTitle = values.ElementAtOrDefault(0);
					SectionsProductTitle = values.ElementAtOrDefault(1);
					SectionsSummaryTitle = values.ElementAtOrDefault(2);
					SectionsPackageTitle = values.ElementAtOrDefault(3);
					break;
				case "tab_1_column_header_names":
					ListColumnsGroupTitle = values.ElementAtOrDefault(0);
					ListColumnsProductTitle = values.ElementAtOrDefault(1);
					ListColumnsPricingTitle = values.ElementAtOrDefault(2);
					ListColumnsOptionsTitle = values.ElementAtOrDefault(3);
					break;
				case "tab_1_left_panel_button_names":
					ListSettingsDimensionTitle = values.ElementAtOrDefault(0);
					ListSettingsLocationTitle = values.ElementAtOrDefault(1);
					ListSettingsStrategyTitle = values.ElementAtOrDefault(2);
					ListSettingsTargetingTitle = values.ElementAtOrDefault(3);
					ListSettingsRichMediaTitle = values.ElementAtOrDefault(4);
					break;
				case "tab_1_new_line_placeholder_names":
					ListEditorsGroupTitle = values.ElementAtOrDefault(0);
					ListEditorsProductTitle = values.ElementAtOrDefault(1);
					ListEditorsLocationTitle = values.ElementAtOrDefault(2);
					break;
				case "tab_1_new_line_options_label_names":
					ListEditorsTargetingTitle = values.ElementAtOrDefault(0);
					ListEditorsRichMediaTitle = values.ElementAtOrDefault(1);
					break;
				case "tab_1_add_product_hover_tip":
					RibbonButtonDigitalAddTitle = values.ElementAtOrDefault(0);
					RibbonButtonDigitalAddTooltip = values.ElementAtOrDefault(1);
					break;
				case "tab_1_delete_product_hover_tip":
					RibbonButtonDigitalDeleteTitle = values.ElementAtOrDefault(0);
					RibbonButtonDigitalDeleteTooltip = values.ElementAtOrDefault(1);
					break;
				case "tab_1_clone_product_hover_tip":
					RibbonButtonDigitalCloneTitle = values.ElementAtOrDefault(0);
					RibbonButtonDigitalCloneTooltip = values.ElementAtOrDefault(1);
					break;
				case "tab_2_section_titles":
					ProductEditorsSitesTitle = values.ElementAtOrDefault(0);
					ProductEditorsNameTitle = values.ElementAtOrDefault(1);
					ProductEditorsDescriptionTitle = values.ElementAtOrDefault(2);
					ProductEditorsPricingTitle = values.ElementAtOrDefault(3);
					ProductEditorsCommentsTitle = values.ElementAtOrDefault(4);
					break;
				case "tab_4_column_header_names":
					PackageColumnsCategoryTitle = values.ElementAtOrDefault(0);
					PackageColumnsSubCategoryTitle = values.ElementAtOrDefault(1);
					PackageColumnsProductTitle = values.ElementAtOrDefault(2);
					PackageColumnsInfoTitle = values.ElementAtOrDefault(3);
					PackageColumnsCommentsTitle = values.ElementAtOrDefault(4);
					PackageColumnsInvestmentTitle = values.ElementAtOrDefault(5);
					PackageColumnsImpressionsTitle = values.ElementAtOrDefault(6);
					PackageColumnsCPMTitle = values.ElementAtOrDefault(7);
					PackageColumnsRateTitle = values.ElementAtOrDefault(8);
					break;
				case "tab_4_left_panel_button_names":
					PackageSettingsCategoryTitle = values.ElementAtOrDefault(0);
					PackageSettingsSubCategoryTitle = values.ElementAtOrDefault(1);
					PackageSettingsProductTitle = values.ElementAtOrDefault(2);
					PackageSettingsInfoTitle = values.ElementAtOrDefault(3);
					PackageSettingsCommentsTitle = values.ElementAtOrDefault(4);
					PackageSettingsInvestmentTitle = values.ElementAtOrDefault(5);
					PackageSettingsImpressionsTitle = values.ElementAtOrDefault(6);
					PackageSettingsCPMTitle = values.ElementAtOrDefault(7);
					PackageSettingsRateTitle = values.ElementAtOrDefault(8);
					PackageSettingsScreenshotTitle = values.ElementAtOrDefault(9);
					PackageSettingsFormulaTitle = values.ElementAtOrDefault(10);
					break;
				case "column_header_names":
					MediaDigitalColumnsCategoryTitle = values.ElementAtOrDefault(0);
					MediaDigitalColumnsSubCategoryTitle = values.ElementAtOrDefault(1);
					MediaDigitalColumnsProductTitle = values.ElementAtOrDefault(2);
					MediaDigitalColumnsInfoTitle = values.ElementAtOrDefault(3);
					break;
				case "left_panel_button_names":
					MediaDigitalSettingsCategoryTitle = values.ElementAtOrDefault(0);
					MediaDigitalSettingsSubCategoryTitle = values.ElementAtOrDefault(1);
					MediaDigitalSettingsProductTitle = values.ElementAtOrDefault(2);
					MediaDigitalSettingsInfoTitle = values.ElementAtOrDefault(3);
					MediaDigitalSettingsLogosTitle = values.ElementAtOrDefault(4);
					MediaDigitalSettingsMontlyInvestmentTitle = values.ElementAtOrDefault(5);
					MediaDigitalSettingsTotalInvestmentTitle = values.ElementAtOrDefault(6);
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
