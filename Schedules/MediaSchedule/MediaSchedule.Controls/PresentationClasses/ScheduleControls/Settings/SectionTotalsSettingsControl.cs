using System;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.BusinessClasses.Managers;
using Asa.Media.Controls.Properties;
using DevExpress.Skins;
using DevExpress.XtraLayout.Utils;
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
				Logo = BusinessObjects.Instance.ImageResourcesManager.ProgramScheduleRetractableBarTotalsImage ?? Resources.SectionSettingsInfo,
				Tooltip = "Open Schedule Info",
				Action = () => { TabControl.SelectedTabPage = this; }
			};

			quarterSelectorControl.QuarterSelected += OnQuarterChanged;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemAvgRate.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAvgRate.MaxSize, scaleFactor);
			layoutControlItemAvgRate.MinSize = RectangleHelper.ScaleSize(layoutControlItemAvgRate.MinSize, scaleFactor);
			layoutControlItemDiscount.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDiscount.MaxSize, scaleFactor);
			layoutControlItemDiscount.MinSize = RectangleHelper.ScaleSize(layoutControlItemDiscount.MinSize, scaleFactor);
			layoutControlItemNetRate.MaxSize = RectangleHelper.ScaleSize(layoutControlItemNetRate.MaxSize, scaleFactor);
			layoutControlItemNetRate.MinSize = RectangleHelper.ScaleSize(layoutControlItemNetRate.MinSize, scaleFactor);
			layoutControlItemTotalCPP.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTotalCPP.MaxSize, scaleFactor);
			layoutControlItemTotalCPP.MinSize = RectangleHelper.ScaleSize(layoutControlItemTotalCPP.MinSize, scaleFactor);
			layoutControlItemTotalCost.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTotalCost.MaxSize, scaleFactor);
			layoutControlItemTotalCost.MinSize = RectangleHelper.ScaleSize(layoutControlItemTotalCost.MinSize, scaleFactor);
			layoutControlItemTotalPeriods.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTotalPeriods.MaxSize, scaleFactor);
			layoutControlItemTotalPeriods.MinSize = RectangleHelper.ScaleSize(layoutControlItemTotalPeriods.MinSize, scaleFactor);
			layoutControlItemTotalSpots.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTotalSpots.MaxSize, scaleFactor);
			layoutControlItemTotalSpots.MinSize = RectangleHelper.ScaleSize(layoutControlItemTotalSpots.MinSize, scaleFactor);
			layoutControlItemTotalGRP.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTotalGRP.MaxSize, scaleFactor);
			layoutControlItemTotalGRP.MinSize = RectangleHelper.ScaleSize(layoutControlItemTotalGRP.MinSize, scaleFactor);
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
			layoutControlItemQuarterSelector.Visibility = _sectionData.ParentScheduleSettings.Quarters.Count > 1 ? LayoutVisibility.Always : LayoutVisibility.Never;
			quarterSelectorControl.InitControls(
				_sectionData.ParentScheduleSettings.Quarters,
				_sectionData.ParentScheduleSettings.Quarters.FirstOrDefault(q => q.DateAnchor == _sectionData.Parent.SelectedQuarter));
		}

		public void UpdateQuarterState()
		{
			layoutControlItemQuarterSelector.Enabled = _sectionData.ShowSpots;
			if (!_sectionData.ShowSpots)
				quarterSelectorControl.InitControls(
				_sectionData.ParentScheduleSettings.Quarters,
				null);
		}
	}
}
