using System;
using System.ComponentModel;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.Properties;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	[ToolboxItem(false)]
	//public partial class SectionDigitalSettingsControl : UserControl
	public partial class SectionDigitalSettingsControl : XtraTabPage, IContentSettingsControl
	{
		private ProgramScheduleContent _content;

		public int Order => 0;
		public bool IsAvailable => true;
		public ButtonInfo BarButton { get; }
		public ScheduleSettingsType SettingsType=>ScheduleSettingsType.Digital;

		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		public SectionDigitalSettingsControl()
		{
			InitializeComponent();
			Text = "Info";
			BarButton = new ButtonInfo
			{
				Logo = Resources.SectionSettingsInfo,
				Tooltip = "Open Digital Settings",
				Action = () => { TabControl.SelectedTabPage = this; }
			};
		}

		public void LoadContentData(ProgramScheduleContent content)
		{
			_content = content;
		}

		private void OnSettingsChanged(Object sender, EventArgs e)
		{
			//digitalInfoControl.SaveData();
			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}
	}
}
