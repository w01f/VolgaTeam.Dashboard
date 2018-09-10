using System;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Snapshot;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.BusinessClasses.Managers;
using DevExpress.Skins;
using DevExpress.XtraTab;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Settings
{
	//public partial class SummaryColumnSettingsControl : UserControl
	public partial class SummaryColumnSettingsControl : XtraTabPage, IContentSettingsControl
	{
		private bool _allowToSave;
		private SnapshotContent _content;

		public int Order => 1;
		public bool IsAvailable => true;
		public ButtonInfo BarButton { get; }
		public SnapshotSettingsType SettingsType => SnapshotSettingsType.Summary;
		public event EventHandler<SettingsChangedEventArgs> DataChanged;

		public SummaryColumnSettingsControl()
		{
			InitializeComponent();
			Text = "Info";
			BarButton = new ButtonInfo
			{
				Logo = BusinessObjects.Instance.ImageResourcesManager.SnapshotsRetractableBarSummaryImage ?? Properties.Resources.SectionSettingsInfo,
				Tooltip = "Open Schedule Info",
				Action = () => { TabControl.SelectedTabPage = this; }
			};
			
			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemLineId.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLineId.MaxSize, scaleFactor);
			layoutControlItemLineId.MinSize = RectangleHelper.ScaleSize(layoutControlItemLineId.MinSize, scaleFactor);
			layoutControlItemLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MaxSize, scaleFactor);
			layoutControlItemLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemLogo.MinSize, scaleFactor);
			layoutControlItemCampaign.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCampaign.MaxSize, scaleFactor);
			layoutControlItemCampaign.MinSize = RectangleHelper.ScaleSize(layoutControlItemCampaign.MinSize, scaleFactor);
			layoutControlItemComments.MaxSize = RectangleHelper.ScaleSize(layoutControlItemComments.MaxSize, scaleFactor);
			layoutControlItemComments.MinSize = RectangleHelper.ScaleSize(layoutControlItemComments.MinSize, scaleFactor);
			layoutControlItemSpots.MaxSize = RectangleHelper.ScaleSize(layoutControlItemSpots.MaxSize, scaleFactor);
			layoutControlItemSpots.MinSize = RectangleHelper.ScaleSize(layoutControlItemSpots.MinSize, scaleFactor);
			layoutControlItemCost.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCost.MaxSize, scaleFactor);
			layoutControlItemCost.MinSize = RectangleHelper.ScaleSize(layoutControlItemCost.MinSize, scaleFactor);
			layoutControlItemTotalWeeks.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTotalWeeks.MaxSize, scaleFactor);
			layoutControlItemTotalWeeks.MinSize = RectangleHelper.ScaleSize(layoutControlItemTotalWeeks.MinSize, scaleFactor);
			layoutControlItemTotalCost.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTotalCost.MaxSize, scaleFactor);
			layoutControlItemTotalCost.MinSize = RectangleHelper.ScaleSize(layoutControlItemTotalCost.MinSize, scaleFactor);
			layoutControlItemTallySpots.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTallySpots.MaxSize, scaleFactor);
			layoutControlItemTallySpots.MinSize = RectangleHelper.ScaleSize(layoutControlItemTallySpots.MinSize, scaleFactor);
			layoutControlItemTallyCost.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTallyCost.MaxSize, scaleFactor);
			layoutControlItemTallyCost.MinSize = RectangleHelper.ScaleSize(layoutControlItemTallyCost.MinSize, scaleFactor);
		}

		public void LoadContentData(SnapshotContent content)
		{
			_content = content;

			_allowToSave = false;

			buttonXLineId.Checked = _content.SnapshotSummary.ShowLineId;
			buttonXCampaign.Checked = _content.SnapshotSummary.ShowCampaign;
			buttonXComments.Checked = _content.SnapshotSummary.ShowComments;
			buttonXSpots.Checked = _content.SnapshotSummary.ShowSpots;
			buttonXCost.Checked = _content.SnapshotSummary.ShowCost;
			buttonXLogo.Checked = _content.SnapshotSummary.ShowLogo;
			buttonXTotalWeeks.Checked = _content.SnapshotSummary.ShowTotalWeeks;
			buttonXTotalCost.Checked = _content.SnapshotSummary.ShowTotalCost;
			buttonXTallySpots.Checked = _content.SnapshotSummary.ShowTallySpots;
			buttonXTallyCost.Checked = _content.SnapshotSummary.ShowTallyCost;

			_allowToSave = true;
		}

		private void OnSettingsChanged(object sender, EventArgs e)
		{
			if (!_allowToSave) return;

			_content.SnapshotSummary.ShowLineId = buttonXLineId.Checked;
			_content.SnapshotSummary.ShowCampaign = buttonXCampaign.Checked;
			_content.SnapshotSummary.ShowComments = buttonXComments.Checked;
			_content.SnapshotSummary.ShowSpots = buttonXSpots.Checked;
			_content.SnapshotSummary.ShowCost = buttonXCost.Checked;
			_content.SnapshotSummary.ShowLogo = buttonXLogo.Checked;
			_content.SnapshotSummary.ShowTotalWeeks = buttonXTotalWeeks.Checked;
			_content.SnapshotSummary.ShowTotalCost = buttonXTotalCost.Checked;
			_content.SnapshotSummary.ShowTallySpots = buttonXTallySpots.Checked;
			_content.SnapshotSummary.ShowTallyCost = buttonXTallyCost.Checked;

			DataChanged?.Invoke(this, new SettingsChangedEventArgs { ChangedSettingsType = SettingsType });
		}
	}
}
