using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Enums;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.Properties;
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
				Logo = MediaMetaData.Instance.DataType == MediaDataType.TVSchedule
					? Resources.SectionSettingsTV
					: Resources.SectionSettingsRadio,
				Tooltip = String.Format("Open {0} Schedule Settings",
					MediaMetaData.Instance.DataTypeString),
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

				font = new Font(buttonXStation.Font.FontFamily, buttonXStation.Font.Size - 2, buttonXStation.Font.Style);
				buttonXStation.Font = font;
				buttonXCPP.Font = font;
				buttonXCost.Font = font;
				buttonXDay.Font = font;
				buttonXDaypart.Font = font;
				buttonXGRP.Font = font;
				buttonXLength.Font = font;
				buttonXLogo.Font = font;
				buttonXProgram.Font = font;
				buttonXRate.Font = font;
				buttonXRating.Font = font;
				buttonXSpots.Font = font;
				buttonXTime.Font = font;
			}
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
			pnApplyForAll.Visible = _sectionData.Parent.Sections.Count > 1;
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
