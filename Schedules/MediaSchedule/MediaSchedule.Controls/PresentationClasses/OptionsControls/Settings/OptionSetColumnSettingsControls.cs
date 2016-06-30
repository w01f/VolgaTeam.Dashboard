using System;
using System.Drawing;
using System.Linq;
using Asa.Business.Media.Entities.NonPersistent.Option;
using Asa.Business.Media.Enums;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.Properties;
using DevComponents.DotNetBar;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Settings
{
	//public partial class OptionSetColumnSettingsControls : UserControl
	public partial class OptionSetColumnSettingsControls : XtraTabPage, IOptionSetSettingsControl
	{
		private bool _allowToSave;
		private OptionSet _optionsSetData;

		public int Order => 1;
		public bool IsAvailable => true;
		public ButtonInfo BarButton { get; }
		public OptionSettingsType SettingsType => OptionSettingsType.Schedule;
		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		public OptionSetColumnSettingsControls()
		{
			InitializeComponent();
			Text = "Info";
			BarButton = new ButtonInfo
			{
				Logo = Resources.SectionSettingsInfo,
				Tooltip = "Open Schedule Info",
				Action = () => { TabControl.SelectedTabPage = this; }
			};
			if ((CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;

				font = new Font(buttonXOptionAvgRate.Font.FontFamily, buttonXOptionAvgRate.Font.Size - 2,
					buttonXOptionAvgRate.Font.Style);
				buttonXOptionAvgRate.Font = font;
				buttonXOptionCost.Font = font;
				buttonXOptionDay.Font = font;
				buttonXOptionLength.Font = font;
				buttonXOptionLineId.Font = font;
				buttonXOptionLogo.Font = font;
				buttonXOptionProgram.Font = font;
				buttonXOptionRate.Font = font;
				buttonXOptionStation.Font = font;
				buttonXOptionTime.Font = font;
				buttonXOptionMonthlySpots.Font = font;
				buttonXOptionWeeklySpots.Font = font;
				buttonXOptionTotalSpots.Font = font;
				buttonXOptionTallySpots.Font = font;
				buttonXOptionTallyCost.Font = font;
			}
		}

		public void LoadOptionsSetData(OptionSet optionSetData)
		{
			_optionsSetData = optionSetData;

			_allowToSave = false;

			checkEditApplyForAll.Checked = _optionsSetData.Parent.OptionsSummary.ApplySettingsForAll;

			buttonXOptionStation.Checked = _optionsSetData.ShowStation;
			buttonXOptionProgram.Checked = _optionsSetData.ShowProgram;
			buttonXOptionDay.Checked = _optionsSetData.ShowDay;
			buttonXOptionTime.Checked = _optionsSetData.ShowTime;
			buttonXOptionRate.Checked = _optionsSetData.ShowRate;
			buttonXOptionLength.Checked = _optionsSetData.ShowLenght;
			buttonXOptionLogo.Checked = _optionsSetData.ShowLogo;
			buttonXOptionWeeklySpots.Checked = false;
			buttonXOptionMonthlySpots.Checked = false;
			buttonXOptionTotalSpots.Checked = false;
			if (_optionsSetData.ShowSpots)
			{
				switch (_optionsSetData.SpotType)
				{
					case SpotType.Week:
						buttonXOptionWeeklySpots.Checked = true;
						break;
					case SpotType.Month:
						buttonXOptionMonthlySpots.Checked = true;
						break;
					case SpotType.Total:
						buttonXOptionTotalSpots.Checked = true;
						break;
				}
			}
			buttonXOptionLineId.Checked = _optionsSetData.ShowLineId;
			buttonXOptionCost.Checked = _optionsSetData.ShowCost;
			buttonXOptionTallySpots.Checked = _optionsSetData.ShowTotalSpots;
			buttonXOptionTallyCost.Checked = _optionsSetData.ShowTotalCost;
			buttonXOptionAvgRate.Checked = _optionsSetData.ShowAverageRate;

			_allowToSave = true;
		}

		public void UpdateUniversalSettingsToggleVisibility()
		{
			checkEditApplyForAll.Visible = _optionsSetData.Parent.Options.Count > 1;
		}

		private void OnSettingsChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			_optionsSetData.Parent.OptionsSummary.ApplySettingsForAll = checkEditApplyForAll.Checked;

			_optionsSetData.ShowStation = buttonXOptionStation.Checked;
			_optionsSetData.ShowProgram = buttonXOptionProgram.Checked;
			_optionsSetData.ShowDay = buttonXOptionDay.Checked;
			_optionsSetData.ShowTime = buttonXOptionTime.Checked;
			_optionsSetData.ShowRate = buttonXOptionRate.Checked;
			_optionsSetData.ShowLenght = buttonXOptionLength.Checked;
			_optionsSetData.ShowLogo = buttonXOptionLogo.Checked;
			_optionsSetData.ShowSpots = buttonXOptionWeeklySpots.Checked || buttonXOptionMonthlySpots.Checked || buttonXOptionTotalSpots.Checked;
			_optionsSetData.ShowLineId = buttonXOptionLineId.Checked;
			_optionsSetData.ShowCost = buttonXOptionCost.Checked;
			_optionsSetData.ShowTotalSpots = buttonXOptionTallySpots.Checked;
			_optionsSetData.ShowTotalCost = buttonXOptionTallyCost.Checked;
			_optionsSetData.ShowAverageRate = buttonXOptionAvgRate.Checked;
			if (buttonXOptionWeeklySpots.Checked)
				_optionsSetData.SpotType = SpotType.Week;
			else if (buttonXOptionMonthlySpots.Checked)
				_optionsSetData.SpotType = SpotType.Month;
			else if (buttonXOptionTotalSpots.Checked)
				_optionsSetData.SpotType = SpotType.Total;

			if (_optionsSetData.Parent.OptionsSummary.ApplySettingsForAll)
			{
				foreach (var optionsControl in _optionsSetData.Parent.Options.Where(oc => oc.UniqueID != _optionsSetData.UniqueID))
				{
					optionsControl.ShowStation = _optionsSetData.ShowStation;
					optionsControl.ShowProgram = _optionsSetData.ShowProgram;
					optionsControl.ShowDay = _optionsSetData.ShowDay;
					optionsControl.ShowTime = _optionsSetData.ShowTime;
					optionsControl.ShowRate = _optionsSetData.ShowRate;
					optionsControl.ShowLenght = _optionsSetData.ShowLenght;
					optionsControl.ShowLogo = _optionsSetData.ShowLogo;
					optionsControl.ShowSpots = _optionsSetData.ShowSpots;
					optionsControl.ShowLineId = _optionsSetData.ShowLineId;
					optionsControl.ShowCost = _optionsSetData.ShowCost;
					optionsControl.ShowTotalSpots = _optionsSetData.ShowTotalSpots;
					optionsControl.ShowTotalCost = _optionsSetData.ShowTotalCost;
					optionsControl.ShowAverageRate = _optionsSetData.ShowAverageRate;
					optionsControl.UseDecimalRates = _optionsSetData.UseDecimalRates;
					optionsControl.CloneLineToTheEnd = _optionsSetData.CloneLineToTheEnd;
					optionsControl.ShowSpotsX = _optionsSetData.ShowSpotsX;
					optionsControl.SpotType = _optionsSetData.SpotType;

					optionsControl.PositionStation = _optionsSetData.PositionStation;
					optionsControl.PositionProgram = _optionsSetData.PositionProgram;
					optionsControl.PositionDay = _optionsSetData.PositionDay;
					optionsControl.PositionTime = _optionsSetData.PositionTime;
					optionsControl.PositionRate = _optionsSetData.PositionRate;
					optionsControl.PositionLenght = _optionsSetData.PositionLenght;
					optionsControl.PositionSpots = _optionsSetData.PositionSpots;
					optionsControl.PositionCost = _optionsSetData.PositionCost;
				}
			}

			_optionsSetData.Parent.OptionsSummary.UpdateSpotType();

			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}

		private void OnSpotsClick(object sender, EventArgs e)
		{
			var button = sender as ButtonX;
			if (button == null) return;
			if (button.Checked)
			{
				button.Checked = false;
				return;
			}
			_allowToSave = false;
			buttonXOptionWeeklySpots.Checked = false;
			buttonXOptionMonthlySpots.Checked = false;
			buttonXOptionTotalSpots.Checked = false;
			_allowToSave = true;
			button.Checked = true;
		}
	}
}
