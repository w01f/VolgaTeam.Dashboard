using System;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
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
	//public partial class SectionColumnSettingsControl : UserControl, ISectionSettingsControl
	public partial class SectionColumnSettingsControl : XtraTabPage, ISectionSettingsControl
	{
		private bool _allowToSave;
		private ScheduleSection _sectionData;
		public int Order => 1;
		public bool IsAvailable => true;
		public ButtonInfo BarButton { get; }
		public ScheduleSettingsType SettingsType => ScheduleSettingsType.Columns;
		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		public SectionColumnSettingsControl()
		{
			InitializeComponent();
			Text = MediaMetaData.Instance.DataTypeString;
			BarButton = new ButtonInfo
			{
				Logo = BusinessObjects.Instance.ImageResourcesManager.ProgramScheduleRetractableBarColumnsImage ?? (MediaMetaData.Instance.DataType == MediaDataType.TVSchedule
					? Resources.SectionSettingsTV
					: Resources.SectionSettingsRadio),
				Tooltip = String.Format("Open {0} Schedule Settings",
					MediaMetaData.Instance.DataTypeString),
				Action = () => { TabControl.SelectedTabPage = this; }
			};

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemStation.MaxSize = RectangleHelper.ScaleSize(layoutControlItemStation.MaxSize, scaleFactor);
			layoutControlItemStation.MinSize = RectangleHelper.ScaleSize(layoutControlItemStation.MinSize, scaleFactor);
			layoutControlItemLength.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLength.MaxSize, scaleFactor);
			layoutControlItemLength.MinSize = RectangleHelper.ScaleSize(layoutControlItemLength.MinSize, scaleFactor);
			layoutControlItemDaypart.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDaypart.MaxSize, scaleFactor);
			layoutControlItemDaypart.MinSize = RectangleHelper.ScaleSize(layoutControlItemDaypart.MinSize, scaleFactor);
			layoutControlItemSpots.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSpots.MaxSize, scaleFactor);
			layoutControlItemSpots.MinSize = RectangleHelper.ScaleSize(layoutControlItemSpots.MinSize, scaleFactor);
			layoutControlItemDay.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDay.MaxSize, scaleFactor);
			layoutControlItemDay.MinSize = RectangleHelper.ScaleSize(layoutControlItemDay.MinSize, scaleFactor);
			layoutControlItemTime.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTime.MaxSize, scaleFactor);
			layoutControlItemTime.MinSize = RectangleHelper.ScaleSize(layoutControlItemTime.MinSize, scaleFactor);
			layoutControlItemProgram.MaxSize = RectangleHelper.ScaleSize(layoutControlItemProgram.MaxSize, scaleFactor);
			layoutControlItemProgram.MinSize = RectangleHelper.ScaleSize(layoutControlItemProgram.MinSize, scaleFactor);
			layoutControlItemCost.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCost.MaxSize, scaleFactor);
			layoutControlItemCost.MinSize = RectangleHelper.ScaleSize(layoutControlItemCost.MinSize, scaleFactor);
			layoutControlItemRate.MaxSize = RectangleHelper.ScaleSize(layoutControlItemRate.MaxSize, scaleFactor);
			layoutControlItemRate.MinSize = RectangleHelper.ScaleSize(layoutControlItemRate.MinSize, scaleFactor);
			layoutControlItemLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MaxSize, scaleFactor);
			layoutControlItemLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MinSize, scaleFactor);
			layoutControlItemCPP.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCPP.MaxSize, scaleFactor);
			layoutControlItemCPP.MinSize = RectangleHelper.ScaleSize(layoutControlItemCPP.MinSize, scaleFactor);
			layoutControlItemRating.MaxSize = RectangleHelper.ScaleSize(layoutControlItemRating.MaxSize, scaleFactor);
			layoutControlItemRating.MinSize = RectangleHelper.ScaleSize(layoutControlItemRating.MinSize, scaleFactor);
			layoutControlItemGRP.MaxSize = RectangleHelper.ScaleSize(layoutControlItemGRP.MaxSize, scaleFactor);
			layoutControlItemGRP.MinSize = RectangleHelper.ScaleSize(layoutControlItemGRP.MinSize, scaleFactor);
		}

		public void LoadSectionData(ScheduleSection sectionData)
		{
			_sectionData = sectionData;

			_allowToSave = false;

			buttonXRating.Enabled = _sectionData.Parent.ScheduleSettings.UseDemo & !String.IsNullOrEmpty(_sectionData.Parent.ScheduleSettings.Demo);
			buttonXRating.Text = _sectionData.Parent.ScheduleSettings.DemoType == DemoType.Rtg ? "Rtg" : "(000s)";
			buttonXCPP.Enabled = _sectionData.Parent.ScheduleSettings.UseDemo & !String.IsNullOrEmpty(_sectionData.Parent.ScheduleSettings.Demo);
			buttonXGRP.Enabled = _sectionData.Parent.ScheduleSettings.UseDemo & !String.IsNullOrEmpty(_sectionData.Parent.ScheduleSettings.Demo);
			buttonXCPP.Text = _sectionData.Parent.ScheduleSettings.DemoType == DemoType.Rtg ? "CPP" : "CPM";
			buttonXGRP.Text = _sectionData.Parent.ScheduleSettings.DemoType == DemoType.Rtg ? "GRPs" : "Impr";
			buttonXSpots.Text = String.Format("{0}s", _sectionData.Parent.ScheduleSettings.SelectedSpotType);

			checkEditApplyForAll.Checked = _sectionData.Parent.ApplySettingsForAll;

			buttonXRate.Checked = _sectionData.ShowRate;
			buttonXRating.Checked = _sectionData.ShowRating;
			buttonXCost.Checked = _sectionData.ShowCost;
			buttonXCPP.Checked = _sectionData.ShowCPP;
			buttonXDay.Checked = _sectionData.ShowDay;
			buttonXDaypart.Checked = _sectionData.ShowDaypart;
			buttonXGRP.Checked = _sectionData.ShowGRP;
			buttonXLength.Checked = _sectionData.ShowLenght;
			buttonXLogo.Checked = _sectionData.ShowLogo;
			buttonXStation.Checked = _sectionData.ShowStation;
			buttonXProgram.Checked = _sectionData.ShowProgram;
			buttonXTime.Checked = _sectionData.ShowTime;
			buttonXSpots.Checked = _sectionData.ShowSpots;

			_allowToSave = true;

			UpdateUniversalSettingsToggleVisibility();
		}

		public void UpdateUniversalSettingsToggleVisibility()
		{
			layoutControlItemApplyForAll.Visibility = _sectionData.Parent.Sections.Count > 1 ? LayoutVisibility.Always : LayoutVisibility.Never;
		}

		private void OnSettingsChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			_sectionData.Parent.ApplySettingsForAll = checkEditApplyForAll.Checked;
			_sectionData.ShowRate = buttonXRate.Checked;
			_sectionData.ShowRating = buttonXRating.Checked;
			_sectionData.ShowCost = buttonXCost.Checked;
			_sectionData.ShowCPP = buttonXCPP.Checked;
			_sectionData.ShowDay = buttonXDay.Checked;
			_sectionData.ShowDaypart = buttonXDaypart.Checked;
			_sectionData.ShowGRP = buttonXGRP.Checked;
			_sectionData.ShowLenght = buttonXLength.Checked;
			_sectionData.ShowLogo = buttonXLogo.Checked;
			_sectionData.ShowStation = buttonXStation.Checked;
			_sectionData.ShowProgram = buttonXProgram.Checked;
			_sectionData.ShowTime = buttonXTime.Checked;
			_sectionData.ShowSpots = buttonXSpots.Checked;

			if (!_sectionData.ShowSpots)
				_sectionData.Parent.SelectedQuarter = null;

			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}
	}
}
