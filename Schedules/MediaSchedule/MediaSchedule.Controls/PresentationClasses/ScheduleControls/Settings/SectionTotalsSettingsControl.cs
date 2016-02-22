using System;
using System.Drawing;
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

			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(buttonXAvgRate.Font.FontFamily, buttonXAvgRate.Font.Size - 2, buttonXAvgRate.Font.Style);
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
	}
}
