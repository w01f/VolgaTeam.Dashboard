using System;
using System.Drawing;
using Asa.Business.Media.Entities.NonPersistent.Snapshot;
using Asa.Common.GUI.RetractableBar;
using Asa.Media.Controls.Properties;
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
				Logo = Resources.SectionSettingsInfo,
				Tooltip = "Open Schedule Info",
				Action = () => { TabControl.SelectedTabPage = this; }
			};
			if (CreateGraphics().DpiX > 96)
			{
				var font = new Font(buttonXCampaign.Font.FontFamily, buttonXCampaign.Font.Size - 2,
					buttonXCampaign.Font.Style);
				buttonXCampaign.Font = font;
				buttonXComments.Font = font;
				buttonXCost.Font = font;
				buttonXLineId.Font = font;
				buttonXLogo.Font = font;
				buttonXSpots.Font = font;
				buttonXTallyCost.Font = font;
				buttonXTallySpots.Font = font;
				buttonXTotalCost.Font = font;
				buttonXTotalWeeks.Font = font;
			}
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
