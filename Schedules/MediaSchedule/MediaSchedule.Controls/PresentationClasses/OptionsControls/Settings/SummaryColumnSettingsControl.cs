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
	//public partial class SummaryColumnSettingsControl : UserControl
	public partial class SummaryColumnSettingsControl : XtraTabPage, IContentSettingsControl
	{
		private bool _allowToSave;
		private OptionsContent _content;

		public Int32 Order => 0;
		public bool IsAvailable => true;
		public ButtonInfo BarButton { get; }
		public OptionSettingsType SettingsType => OptionSettingsType.Summary;
		public event EventHandler<SettingsChangedEventArgs> DataChanged;
		
		public SummaryColumnSettingsControl()
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
				var font = new Font(buttonXCampaign.Font.FontFamily, buttonXCampaign.Font.Size - 2,
					buttonXCampaign.Font.Style);
				buttonXCampaign.Font = font;
				buttonXComments.Font = font;
				buttonXTallyCost.Font = font;
				buttonXTallySpots.Font = font;
				buttonXTotalCost.Font = font;
				buttonXTotalWeeks.Font = font;
				buttonXLineId.Font = font;
				buttonXLogo.Font = font;
				buttonXMonthlyCost.Font = font;
				buttonXMonthlySpots.Font = font;
				buttonXWeeklyCost.Font = font;
				buttonXWeeklySpots.Font = font;
				buttonXTotalMonths.Font = font;
				buttonXTotalSpots.Font = font;
			}
		}

		public void LoadContentData(OptionsContent content)
		{
			_content = content;

			_allowToSave = false;

			buttonXLineId.Checked = _content.OptionsSummary.ShowLineId;
			buttonXCampaign.Checked = _content.OptionsSummary.ShowCampaign;
			buttonXComments.Checked = _content.OptionsSummary.ShowComments;
			buttonXLogo.Checked = _content.OptionsSummary.ShowLogo;
			buttonXTotalCost.Checked = _content.OptionsSummary.ShowTotalCost;
			buttonXTallySpots.Checked = _content.OptionsSummary.ShowTallySpots;
			buttonXTallyCost.Checked = _content.OptionsSummary.ShowTallyCost;

			switch (_content.OptionsSummary.SpotType)
			{
				case SpotType.Week:
					buttonXWeeklySpots.Enabled = true;
					buttonXWeeklyCost.Enabled = true;
					buttonXTotalWeeks.Enabled = true;
					buttonXWeeklySpots.Checked = _content.OptionsSummary.ShowSpots;
					buttonXWeeklyCost.Checked = _content.OptionsSummary.ShowCost;
					buttonXTotalWeeks.Checked = _content.OptionsSummary.ShowTotalPeriods;

					buttonXMonthlySpots.Enabled = false;
					buttonXMonthlyCost.Enabled = false;
					buttonXTotalMonths.Enabled = false;
					buttonXTotalSpots.Enabled = false;

					buttonXMonthlySpots.Checked = false;
					buttonXMonthlyCost.Checked = false;
					buttonXTotalMonths.Checked = false;
					buttonXTotalSpots.Checked = false;
					break;
				case SpotType.Month:
					buttonXMonthlySpots.Enabled = true;
					buttonXMonthlyCost.Enabled = true;
					buttonXTotalMonths.Enabled = true;
					buttonXMonthlySpots.Checked = _content.OptionsSummary.ShowSpots;
					buttonXMonthlyCost.Checked = _content.OptionsSummary.ShowCost;
					buttonXTotalMonths.Checked = _content.OptionsSummary.ShowTotalPeriods;

					buttonXWeeklySpots.Enabled = false;
					buttonXWeeklyCost.Enabled = false;
					buttonXTotalWeeks.Enabled = false;
					buttonXTotalSpots.Enabled = false;

					buttonXWeeklySpots.Checked = false;
					buttonXWeeklyCost.Checked = false;
					buttonXTotalWeeks.Checked = false;
					buttonXTotalSpots.Checked = false;
					break;
				case SpotType.Total:
					buttonXTotalSpots.Enabled = true;
					buttonXTotalSpots.Checked = _content.OptionsSummary.ShowSpots;

					buttonXWeeklySpots.Enabled = false;
					buttonXWeeklyCost.Enabled = false;
					buttonXTotalWeeks.Enabled = false;
					buttonXMonthlySpots.Enabled = false;
					buttonXMonthlyCost.Enabled = false;
					buttonXTotalMonths.Enabled = false;

					buttonXWeeklySpots.Checked = false;
					buttonXWeeklyCost.Checked = false;
					buttonXTotalWeeks.Checked = false;
					buttonXMonthlySpots.Checked = false;
					buttonXMonthlyCost.Checked = false;
					buttonXTotalMonths.Checked = false;
					break;
			}

			_allowToSave = true;
		}

		private void OnSettingsChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			_content.OptionsSummary.ShowLineId = buttonXLineId.Checked;
			_content.OptionsSummary.ShowCampaign = buttonXCampaign.Checked;
			_content.OptionsSummary.ShowComments = buttonXComments.Checked;
			_content.OptionsSummary.ShowSpots = buttonXWeeklySpots.Checked || buttonXMonthlySpots.Checked || buttonXTotalSpots.Checked;
			_content.OptionsSummary.ShowCost = buttonXWeeklyCost.Checked || buttonXMonthlyCost.Checked;
			_content.OptionsSummary.ShowLogo = buttonXLogo.Checked;
			_content.OptionsSummary.ShowTotalPeriods = buttonXTotalWeeks.Checked || buttonXTotalMonths.Checked;
			_content.OptionsSummary.ShowTotalCost = buttonXTotalCost.Checked;
			_content.OptionsSummary.ShowTallySpots = buttonXTallySpots.Checked;
			_content.OptionsSummary.ShowTallyCost = buttonXTallyCost.Checked;

			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}
	}
}
