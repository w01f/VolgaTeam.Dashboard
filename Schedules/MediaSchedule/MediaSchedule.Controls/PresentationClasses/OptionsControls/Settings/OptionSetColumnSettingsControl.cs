using System;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Option;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.BusinessClasses.Managers;
using DevComponents.DotNetBar;
using DevExpress.Skins;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Settings
{
	//public partial class OptionSetColumnSettingsControl : UserControl
	public partial class OptionSetColumnSettingsControl : XtraTabPage, IOptionSetSettingsControl
	{
		private bool _allowToSave;
		private OptionSet _optionsSetData;

		public int Order => 1;
		public bool IsAvailable => true;
		public ButtonInfo BarButton { get; }
		public OptionSettingsType SettingsType => OptionSettingsType.Schedule;
		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		public OptionSetColumnSettingsControl()
		{
			InitializeComponent();
			Text = "Info";
			BarButton = new ButtonInfo
			{
				Logo = BusinessObjects.Instance.ImageResourcesManager.OptionsRetractableBarColumnsImage ?? Properties.Resources.SectionSettingsInfo,
				Tooltip = "Open Schedule Info",
				Action = () => { TabControl.SelectedTabPage = this; }
			};
			
			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemStation.MaxSize = RectangleHelper.ScaleSize(layoutControlItemStation.MaxSize, scaleFactor);
			layoutControlItemStation.MinSize = RectangleHelper.ScaleSize(layoutControlItemStation.MinSize, scaleFactor);
			layoutControlItemProgram.MaxSize = RectangleHelper.ScaleSize(layoutControlItemProgram.MaxSize, scaleFactor);
			layoutControlItemProgram.MinSize = RectangleHelper.ScaleSize(layoutControlItemProgram.MinSize, scaleFactor);
			layoutControlItemDay.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDay.MaxSize, scaleFactor);
			layoutControlItemDay.MinSize = RectangleHelper.ScaleSize(layoutControlItemDay.MinSize, scaleFactor);
			layoutControlItemTime.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTime.MaxSize, scaleFactor);
			layoutControlItemTime.MinSize = RectangleHelper.ScaleSize(layoutControlItemTime.MinSize, scaleFactor);
			layoutControlItemRate.MaxSize = RectangleHelper.ScaleSize(layoutControlItemRate.MaxSize, scaleFactor);
			layoutControlItemRate.MinSize = RectangleHelper.ScaleSize(layoutControlItemRate.MinSize, scaleFactor);
			layoutControlItemLength.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLength.MaxSize, scaleFactor);
			layoutControlItemLength.MinSize = RectangleHelper.ScaleSize(layoutControlItemLength.MinSize, scaleFactor);
			layoutControlItemLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MaxSize, scaleFactor);
			layoutControlItemLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MinSize, scaleFactor);
			layoutControlItemWeeklySpots.MaxSize = RectangleHelper.ScaleSize(layoutControlItemWeeklySpots.MaxSize, scaleFactor);
			layoutControlItemWeeklySpots.MinSize = RectangleHelper.ScaleSize(layoutControlItemWeeklySpots.MinSize, scaleFactor);
			layoutControlItemMonthlySpots.MaxSize = RectangleHelper.ScaleSize(layoutControlItemMonthlySpots.MaxSize, scaleFactor);
			layoutControlItemMonthlySpots.MinSize = RectangleHelper.ScaleSize(layoutControlItemMonthlySpots.MinSize, scaleFactor);
			layoutControlItemTotalSpots.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTotalSpots.MaxSize, scaleFactor);
			layoutControlItemTotalSpots.MinSize = RectangleHelper.ScaleSize(layoutControlItemTotalSpots.MinSize, scaleFactor);
			layoutControlItemLineId.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLineId.MaxSize, scaleFactor);
			layoutControlItemLineId.MinSize = RectangleHelper.ScaleSize(layoutControlItemLineId.MinSize, scaleFactor);
			layoutControlItemCost.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCost.MaxSize, scaleFactor);
			layoutControlItemCost.MinSize = RectangleHelper.ScaleSize(layoutControlItemCost.MinSize, scaleFactor);
			layoutControlItemTallySpots.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTallySpots.MaxSize, scaleFactor);
			layoutControlItemTallySpots.MinSize = RectangleHelper.ScaleSize(layoutControlItemTallySpots.MinSize, scaleFactor);
			layoutControlItemTallyCost.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTallyCost.MaxSize, scaleFactor);
			layoutControlItemTallyCost.MinSize = RectangleHelper.ScaleSize(layoutControlItemTallyCost.MinSize, scaleFactor);
			layoutControlItemAvgRate.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAvgRate.MaxSize, scaleFactor);
			layoutControlItemAvgRate.MinSize = RectangleHelper.ScaleSize(layoutControlItemAvgRate.MinSize, scaleFactor);
		}

		public void LoadOptionsSetData(OptionSet optionSetData)
		{
			_optionsSetData = optionSetData;

			_allowToSave = false;

			checkEditApplyForAll.Checked = _optionsSetData.Parent.OptionsSummary.ApplySettingsForAll;

			buttonXStation.Checked = _optionsSetData.ShowStation;
			buttonXProgram.Checked = _optionsSetData.ShowProgram;
			buttonXDay.Checked = _optionsSetData.ShowDay;
			buttonXTime.Checked = _optionsSetData.ShowTime;
			buttonXRate.Checked = _optionsSetData.ShowRate;
			buttonXLength.Checked = _optionsSetData.ShowLenght;
			buttonXLogo.Checked = _optionsSetData.ShowLogo;
			buttonXWeeklySpots.Checked = false;
			buttonXMonthlySpots.Checked = false;
			buttonXTotalSpots.Checked = false;
			if (_optionsSetData.ShowSpots)
			{
				switch (_optionsSetData.SpotType)
				{
					case SpotType.Week:
						buttonXWeeklySpots.Checked = true;
						break;
					case SpotType.Month:
						buttonXMonthlySpots.Checked = true;
						break;
					case SpotType.Total:
						buttonXTotalSpots.Checked = true;
						break;
				}
			}
			buttonXLineId.Checked = _optionsSetData.ShowLineId;
			buttonXCost.Checked = _optionsSetData.ShowCost;
			buttonXTallySpots.Checked = _optionsSetData.ShowTotalSpots;
			buttonXTallyCost.Checked = _optionsSetData.ShowTotalCost;
			buttonXAvgRate.Checked = _optionsSetData.ShowAverageRate;

			_allowToSave = true;

			UpdateUniversalSettingsToggleVisibility();
		}

		public void UpdateUniversalSettingsToggleVisibility()
		{
			checkEditApplyForAll.Visible = _optionsSetData.Parent.Options.Count > 1;
		}

		private void OnSettingsChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			_optionsSetData.Parent.OptionsSummary.ApplySettingsForAll = checkEditApplyForAll.Checked;

			_optionsSetData.ShowStation = buttonXStation.Checked;
			_optionsSetData.ShowProgram = buttonXProgram.Checked;
			_optionsSetData.ShowDay = buttonXDay.Checked;
			_optionsSetData.ShowTime = buttonXTime.Checked;
			_optionsSetData.ShowRate = buttonXRate.Checked;
			_optionsSetData.ShowLenght = buttonXLength.Checked;
			_optionsSetData.ShowLogo = buttonXLogo.Checked;
			_optionsSetData.ShowSpots = buttonXWeeklySpots.Checked || buttonXMonthlySpots.Checked || buttonXTotalSpots.Checked;
			_optionsSetData.ShowLineId = buttonXLineId.Checked;
			_optionsSetData.ShowCost = buttonXCost.Checked;
			_optionsSetData.ShowTotalSpots = buttonXTallySpots.Checked;
			_optionsSetData.ShowTotalCost = buttonXTallyCost.Checked;
			_optionsSetData.ShowAverageRate = buttonXAvgRate.Checked;
			if (buttonXWeeklySpots.Checked)
				_optionsSetData.SpotType = SpotType.Week;
			else if (buttonXMonthlySpots.Checked)
				_optionsSetData.SpotType = SpotType.Month;
			else if (buttonXTotalSpots.Checked)
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
			buttonXWeeklySpots.Checked = false;
			buttonXMonthlySpots.Checked = false;
			buttonXTotalSpots.Checked = false;
			_allowToSave = true;
			button.Checked = true;
		}
	}
}
