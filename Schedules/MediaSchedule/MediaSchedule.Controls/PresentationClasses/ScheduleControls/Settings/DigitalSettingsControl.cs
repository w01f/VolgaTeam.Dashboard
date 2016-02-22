using System;
using System.Linq;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.Properties;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	public partial class DigitalSettingsControl : XtraTabPage, IContentSettingsControl
	//public partial class DigitalSettingsControl : UserControl, IContentSettingsControl
	{
		private ProgramScheduleContent _content;
		public int Order => 2;

		public bool IsAvailable => (Controller.Instance.TabDigitalProduct.Visible ||
				Controller.Instance.TabDigitalPackage.Visible) &&
			_content != null &&
			_content.Schedule.DigitalProductsContent.DigitalProducts.Any();

		public ButtonInfo BarButton { get; }
		public ScheduleSettingsType SettingsType => ScheduleSettingsType.DigitalInfo;
		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		public DigitalSettingsControl()
		{
			InitializeComponent();
			Text = "Digital";
			BarButton = new ButtonInfo
			{
				Logo = Resources.SectionSettingsDigital,
				Tooltip = "Open Digital Settings",
				Action = () => { TabControl.SelectedTabPage = this; }
			};

			digitalInfoControl.RequestDefaultInfo += (o, e) =>
			{
				((BaseEdit)e.Editor).EditValue = _content.Schedule.DigitalProductsContent.GetDigitalInfo(e);
				((BaseEdit)e.Editor).Tag = ((BaseEdit)e.Editor).EditValue;
			};

			digitalInfoControl.SettingsChanged += OnSettingsChanged;
		}

		public void LoadContentData(ProgramScheduleContent content)
		{
			_content = content;
			digitalInfoControl.InitData(_content.DigitalLegend);
		}

		private void OnSettingsChanged(Object sender, EventArgs e)
		{
			digitalInfoControl.SaveData();
			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}
	}
}
