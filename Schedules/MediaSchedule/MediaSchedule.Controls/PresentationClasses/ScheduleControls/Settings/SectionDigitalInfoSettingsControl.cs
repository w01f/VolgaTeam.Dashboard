using System;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.PresentationClasses.Digital.DigitalInfo;
using Asa.Media.Controls.Properties;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	class SectionDigitalInfoSettingsControl : BaseDigitalInfoSettingsControl, ISectionSettingsControl
	{
		private ScheduleSection _sectionData;

		public int Order => 0;
		public bool IsAvailable => true;
		public ButtonInfo BarButton { get; }
		public ScheduleSettingsType SettingsType => ScheduleSettingsType.DigitalInfo;
		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		public SectionDigitalInfoSettingsControl()
		{
			BarButton = new ButtonInfo
			{
				Logo = Resources.SectionSettingsInfo,
				Tooltip = "Open Digital Settings",
				Action = () => { TabControl.SelectedTabPage = this; }
			};
		}

		protected override void RaiseDataChanged()
		{
			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}

		public void LoadSectionData(ScheduleSection sectionData)
		{
			_sectionData = sectionData;
			_digitalInfo = _sectionData.DigitalInfo;
			LoadData();
		}
	}
}
