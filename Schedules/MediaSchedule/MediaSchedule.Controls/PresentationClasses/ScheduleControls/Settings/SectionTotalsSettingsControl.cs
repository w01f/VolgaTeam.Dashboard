using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Enums;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.Properties;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	//public partial class SectionTotalsSettingsControl : UserControl, ISectionSettingsControl
	public partial class SectionTotalsSettingsControl : XtraTabPage, ISectionSettingsControl
	{
		private bool _allowToSave;
		private ScheduleSection _sectionData;
		public int Order => 3;
		public bool IsAvailable => true;
		public ButtonInfo BarButton { get; }
		public ScheduleSettingsType SettingsType => ScheduleSettingsType.Totals;
		public event EventHandler<SettingsChangedEventArgs> DataChanged;
		public SectionTotalsSettingsControl()
		{
			InitializeComponent();

			Text = "Info";
			BarButton = new ButtonInfo
			{
				Logo = Resources.SectionSettingsInfo,
				Tooltip = "Open Schedule Info",
				Action = () => { TabControl.SelectedTabPage = this; }
			};

			quarterSelectorControl.QuarterSelected += OnQuarterChanged;

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

				font = new Font(buttonXAvgRate.Font.FontFamily, buttonXAvgRate.Font.Size - 2, buttonXAvgRate.Font.Style);
				buttonXAvgRate.Font = font;
				buttonXDiscount.Font = font;
				buttonXNetRate.Font = font;
				buttonXTotalCPP.Font = font;
				buttonXTotalCost.Font = font;
				buttonXTotalPeriods.Font = font;
				buttonXTotalSpots.Font = font;
				buttonXTotalGRP.Font = font;
			}
		}

		public void LoadSectionData(ScheduleSection sectionData)
		{
			_sectionData = sectionData;

			_allowToSave = false;

			buttonXTotalCPP.Enabled = _sectionData.Parent.ScheduleSettings.UseDemo & !String.IsNullOrEmpty(_sectionData.Parent.ScheduleSettings.Demo);
			buttonXTotalGRP.Enabled = _sectionData.Parent.ScheduleSettings.UseDemo & !String.IsNullOrEmpty(_sectionData.Parent.ScheduleSettings.Demo);
			buttonXTotalCPP.Text = _sectionData.Parent.ScheduleSettings.DemoType == DemoType.Rtg ? "Overall CPP" : "Overall CPM";
			buttonXTotalGRP.Text = _sectionData.Parent.ScheduleSettings.DemoType == DemoType.Rtg ? "Total GRPs" : "Total Impr";

			buttonXTotalPeriods.Checked = _sectionData.ShowTotalPeriods;
			buttonXTotalPeriods.Text = String.Format("Total {0}s", _sectionData.Parent.ScheduleSettings.SelectedSpotType);
			buttonXTotalSpots.Checked = _sectionData.ShowTotalSpots;
			buttonXTotalGRP.Checked = _sectionData.ShowTotalGRP;
			buttonXTotalCPP.Checked = _sectionData.ShowTotalCPP;
			buttonXAvgRate.Checked = _sectionData.ShowAverageRate;
			buttonXTotalCost.Checked = _sectionData.ShowTotalRate;
			buttonXNetRate.Checked = _sectionData.ShowNetRate;
			buttonXDiscount.Checked = _sectionData.ShowDiscount;

			InitQuarters();
			UpdateQuarterState();

			_allowToSave = true;
		}

		private void OnSettingsChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			_sectionData.ShowTotalPeriods = buttonXTotalPeriods.Checked;
			_sectionData.ShowTotalSpots = buttonXTotalSpots.Checked;
			_sectionData.ShowTotalGRP = buttonXTotalGRP.Checked;
			_sectionData.ShowTotalCPP = buttonXTotalCPP.Checked;
			_sectionData.ShowAverageRate = buttonXAvgRate.Checked;
			_sectionData.ShowTotalRate = buttonXTotalCost.Checked;
			_sectionData.ShowNetRate = buttonXNetRate.Checked;
			_sectionData.ShowDiscount = buttonXDiscount.Checked;

			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}

		private void OnQuarterChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			var selectedQuarter = quarterSelectorControl.SelectedQuarter;
			_sectionData.Parent.SelectedQuarter = selectedQuarter?.DateAnchor;

			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = ScheduleSettingsType.Quarters });
		}


		private void InitQuarters()
		{
			labelControlQuarterSelectorTitle.Visible =
			quarterSelectorControl.Visible = _sectionData.ParentScheduleSettings.Quarters.Count > 0;
			quarterSelectorControl.InitControls(
				_sectionData.ParentScheduleSettings.Quarters,
				_sectionData.ParentScheduleSettings.Quarters.FirstOrDefault(q => q.DateAnchor == _sectionData.Parent.SelectedQuarter));
		}

		public void UpdateQuarterState()
		{
			quarterSelectorControl.Enabled = _sectionData.ShowSpots;
			if (!_sectionData.ShowSpots)
				quarterSelectorControl.InitControls(
				_sectionData.ParentScheduleSettings.Quarters,
				null);
		}
	}
}
