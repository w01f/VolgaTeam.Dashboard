using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Business.Media.Configuration;
using Asa.Business.Online.Configuration;
using Asa.Business.Online.Dictionaries;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.BusinessClasses.Managers;
using DevExpress.Skins;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.Digital.Settings
{
	//public partial class DigitalListSettingsControl : UserControl, ISettingsControl
	public partial class DigitalListSettingsControl : XtraTabPage, ISettingsControl
	{
		private bool _allowToSave;
		private DigitalProductsContent _content;
		public int Order => 1;
		public ButtonInfo BarButton { get; }
		public DigitalSettingsType SettingsType => DigitalSettingsType.ProductList;

		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		public DigitalListSettingsControl()
		{
			InitializeComponent();
			Text = "Info";
			BarButton = new ButtonInfo
			{
				Logo = BusinessObjects.Instance.ImageResourcesManager.DigitalRetractableBarListImage ?? Properties.Resources.DigitalSettingsList,
				Tooltip = String.Format("Open {0} Schedule Settings",
					MediaMetaData.Instance.DataTypeString),
				Action = () => { TabControl.SelectedTabPage = this; }
			};

			buttonXDimensions.Text = DigitalControlsConfiguration.WrapTitle(ListManager.Instance.DefaultControlsConfiguration.ListSettingsDimensionTitle ?? buttonXDimensions.Text);
			buttonXRichMedia.Text = DigitalControlsConfiguration.WrapTitle(ListManager.Instance.DefaultControlsConfiguration.ListSettingsRichMediaTitle ?? buttonXRichMedia.Text);
			buttonXStrategy.Text = DigitalControlsConfiguration.WrapTitle(ListManager.Instance.DefaultControlsConfiguration.ListSettingsStrategyTitle ?? buttonXStrategy.Text);
			buttonXLocation.Text = DigitalControlsConfiguration.WrapTitle(ListManager.Instance.DefaultControlsConfiguration.ListSettingsLocationTitle ?? buttonXLocation.Text);
			buttonXTargeting.Text = DigitalControlsConfiguration.WrapTitle(ListManager.Instance.DefaultControlsConfiguration.ListSettingsTargetingTitle ?? buttonXTargeting.Text);

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemDimensions.MaxSize = RectangleHelper.ScaleSize(layoutControlItemDimensions.MaxSize, scaleFactor);
			layoutControlItemDimensions.MinSize = RectangleHelper.ScaleSize(layoutControlItemDimensions.MinSize, scaleFactor);
			layoutControlItemRichMedia.MaxSize = RectangleHelper.ScaleSize(layoutControlItemRichMedia.MaxSize, scaleFactor);
			layoutControlItemRichMedia.MinSize = RectangleHelper.ScaleSize(layoutControlItemRichMedia.MinSize, scaleFactor);
			layoutControlItemStrategy.MaxSize = RectangleHelper.ScaleSize(layoutControlItemStrategy.MaxSize, scaleFactor);
			layoutControlItemStrategy.MinSize = RectangleHelper.ScaleSize(layoutControlItemStrategy.MinSize, scaleFactor);
			layoutControlItemLocation.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLocation.MaxSize, scaleFactor);
			layoutControlItemLocation.MinSize = RectangleHelper.ScaleSize(layoutControlItemLocation.MinSize, scaleFactor);
			layoutControlItemTargeting.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTargeting.MaxSize, scaleFactor);
			layoutControlItemTargeting.MinSize = RectangleHelper.ScaleSize(layoutControlItemTargeting.MinSize, scaleFactor);
		}

		public void LoadContentData(DigitalProductsContent content)
		{
			_content = content;

			_allowToSave = false;
			buttonXDimensions.Enabled = _content.ScheduleSettings.DigitalProductListViewSettings.EnableDigitalDimensions;
			buttonXLocation.Enabled = _content.ScheduleSettings.DigitalProductListViewSettings.EnableDigitalLocation;
			buttonXStrategy.Enabled = _content.ScheduleSettings.DigitalProductListViewSettings.EnableDigitalStrategy;
			buttonXTargeting.Enabled = _content.ScheduleSettings.DigitalProductListViewSettings.EnableDigitalTargeting;
			buttonXRichMedia.Enabled = _content.ScheduleSettings.DigitalProductListViewSettings.EnableDigitalRichMedia;

			buttonXDimensions.Checked = _content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalDimensions;
			buttonXLocation.Checked = _content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalLocation;
			buttonXStrategy.Checked = _content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalStrategy;
			buttonXTargeting.Checked = _content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalTargeting;
			buttonXRichMedia.Checked = _content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalRichMedia;
			_allowToSave = true;
		}

		private void OnSettingsChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			_content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalDimensions = buttonXDimensions.Checked;
			_content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalLocation = buttonXLocation.Checked;
			_content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalStrategy = buttonXStrategy.Checked;
			_content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalTargeting = buttonXTargeting.Checked;
			_content.ScheduleSettings.DigitalProductListViewSettings.ShowDigitalRichMedia = buttonXRichMedia.Checked;

			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}
	}
}
