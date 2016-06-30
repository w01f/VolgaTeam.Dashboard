using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Option;
using Asa.Business.Media.Enums;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.Properties;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Settings
{
	//public partial class SummaryColumnSettingsControls : UserControl
	public partial class SummaryColumnSettingsControls : XtraTabPage, IContentSettingsControl
	{
		private bool _allowToSave;
		private OptionsContent _content;

		public Int32 Order => 0;
		public bool IsAvailable => true;
		public ButtonInfo BarButton { get; }
		public OptionSettingsType SettingsType => OptionSettingsType.Summary;
		public event EventHandler<SettingsChangedEventArgs> DataChanged;
		
		public SummaryColumnSettingsControls()
		{
			InitializeComponent();
			Text = "Info";
			BarButton = new ButtonInfo
			{
				Logo = Resources.SectionSettingsInfo,
				Tooltip = "Open Schedule Info",
				Action = () => { TabControl.SelectedTabPage = this; }
			};
			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				font = new Font(buttonXSummaryCampaign.Font.FontFamily, buttonXSummaryCampaign.Font.Size - 2,
					buttonXSummaryCampaign.Font.Style);
				buttonXSummaryCampaign.Font = font;
				buttonXSummaryComments.Font = font;
				buttonXSummaryTallyCost.Font = font;
				buttonXSummaryTallySpots.Font = font;
				buttonXSummaryTotalCost.Font = font;
				buttonXSummaryTotalWeeks.Font = font;
				buttonXSummaryLineId.Font = font;
				buttonXSummaryLogo.Font = font;
				buttonXSummaryMonthlyCost.Font = font;
				buttonXSummaryMonthlySpots.Font = font;
				buttonXSummaryWeeklyCost.Font = font;
				buttonXSummaryWeeklySpots.Font = font;
				buttonXSummaryTotalMonths.Font = font;
				buttonXSummaryTotalSpots.Font = font;
			}
		}

		public void LoadContentData(OptionsContent content)
		{
			_content = content;

			_allowToSave = false;

			buttonXSummaryLineId.Checked = _content.OptionsSummary.ShowLineId;
			buttonXSummaryCampaign.Checked = _content.OptionsSummary.ShowCampaign;
			buttonXSummaryComments.Checked = _content.OptionsSummary.ShowComments;
			buttonXSummaryLogo.Checked = _content.OptionsSummary.ShowLogo;
			buttonXSummaryTotalCost.Checked = _content.OptionsSummary.ShowTotalCost;
			buttonXSummaryTallySpots.Checked = _content.OptionsSummary.ShowTallySpots;
			buttonXSummaryTallyCost.Checked = _content.OptionsSummary.ShowTallyCost;

			switch (_content.OptionsSummary.SpotType)
			{
				case SpotType.Week:
					buttonXSummaryWeeklySpots.Enabled = true;
					buttonXSummaryWeeklyCost.Enabled = true;
					buttonXSummaryTotalWeeks.Enabled = true;
					buttonXSummaryWeeklySpots.Checked = _content.OptionsSummary.ShowSpots;
					buttonXSummaryWeeklyCost.Checked = _content.OptionsSummary.ShowCost;
					buttonXSummaryTotalWeeks.Checked = _content.OptionsSummary.ShowTotalPeriods;

					buttonXSummaryMonthlySpots.Enabled = false;
					buttonXSummaryMonthlyCost.Enabled = false;
					buttonXSummaryTotalMonths.Enabled = false;
					buttonXSummaryTotalSpots.Enabled = false;

					buttonXSummaryMonthlySpots.Checked = false;
					buttonXSummaryMonthlyCost.Checked = false;
					buttonXSummaryTotalMonths.Checked = false;
					buttonXSummaryTotalSpots.Checked = false;
					break;
				case SpotType.Month:
					buttonXSummaryMonthlySpots.Enabled = true;
					buttonXSummaryMonthlyCost.Enabled = true;
					buttonXSummaryTotalMonths.Enabled = true;
					buttonXSummaryMonthlySpots.Checked = _content.OptionsSummary.ShowSpots;
					buttonXSummaryMonthlyCost.Checked = _content.OptionsSummary.ShowCost;
					buttonXSummaryTotalMonths.Checked = _content.OptionsSummary.ShowTotalPeriods;

					buttonXSummaryWeeklySpots.Enabled = false;
					buttonXSummaryWeeklyCost.Enabled = false;
					buttonXSummaryTotalWeeks.Enabled = false;
					buttonXSummaryTotalSpots.Enabled = false;

					buttonXSummaryWeeklySpots.Checked = false;
					buttonXSummaryWeeklyCost.Checked = false;
					buttonXSummaryTotalWeeks.Checked = false;
					buttonXSummaryTotalSpots.Checked = false;
					break;
				case SpotType.Total:
					buttonXSummaryTotalSpots.Enabled = true;
					buttonXSummaryTotalSpots.Checked = _content.OptionsSummary.ShowSpots;

					buttonXSummaryWeeklySpots.Enabled = false;
					buttonXSummaryWeeklyCost.Enabled = false;
					buttonXSummaryTotalWeeks.Enabled = false;
					buttonXSummaryMonthlySpots.Enabled = false;
					buttonXSummaryMonthlyCost.Enabled = false;
					buttonXSummaryTotalMonths.Enabled = false;

					buttonXSummaryWeeklySpots.Checked = false;
					buttonXSummaryWeeklyCost.Checked = false;
					buttonXSummaryTotalWeeks.Checked = false;
					buttonXSummaryMonthlySpots.Checked = false;
					buttonXSummaryMonthlyCost.Checked = false;
					buttonXSummaryTotalMonths.Checked = false;
					break;
			}

			_allowToSave = true;
		}

		private void OnSettingsChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			_content.OptionsSummary.ShowLineId = buttonXSummaryLineId.Checked;
			_content.OptionsSummary.ShowCampaign = buttonXSummaryCampaign.Checked;
			_content.OptionsSummary.ShowComments = buttonXSummaryComments.Checked;
			_content.OptionsSummary.ShowSpots = buttonXSummaryWeeklySpots.Checked || buttonXSummaryMonthlySpots.Checked || buttonXSummaryTotalSpots.Checked;
			_content.OptionsSummary.ShowCost = buttonXSummaryWeeklyCost.Checked || buttonXSummaryMonthlyCost.Checked;
			_content.OptionsSummary.ShowLogo = buttonXSummaryLogo.Checked;
			_content.OptionsSummary.ShowTotalPeriods = buttonXSummaryTotalWeeks.Checked || buttonXSummaryTotalMonths.Checked;
			_content.OptionsSummary.ShowTotalCost = buttonXSummaryTotalCost.Checked;
			_content.OptionsSummary.ShowTallySpots = buttonXSummaryTallySpots.Checked;
			_content.OptionsSummary.ShowTallyCost = buttonXSummaryTallyCost.Checked;

			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}
	}
}
