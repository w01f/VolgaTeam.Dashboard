using System.Linq;
using Asa.Business.Media.Configuration;
using Asa.Common.Core.Objects.Output;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Snapshot
{
	public class SnapshotSummary
	{
		public SnapshotContent Parent { get; private set; }

		public bool ApplySettingsForAll { get; set; }

		public ContractSettings ContractSettings { get; private set; }

		#region Options
		public bool ShowLineId { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowCampaign { get; set; }
		public bool ShowComments { get; set; }
		public bool ShowSpots { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowTotalWeeks { get; set; }
		public bool ShowTotalCost { get; set; }
		public bool ShowTallySpots { get; set; }
		public bool ShowTallyCost { get; set; }
		public bool ShowSpotsX { get; set; }
		public bool UseDecimalRates { get; set; }
		#endregion

		#region Calculated Properties
		public decimal TotalCost
		{
			get { return Parent.Snapshots.Any() ? Parent.Snapshots.Select(x => x.TotalCost).Sum() : 0; }
		}

		public int TotalSpots
		{
			get { return Parent.Snapshots.Any() ? Parent.Snapshots.Select(x => x.TotalSpots).Sum() : 0; }
		}
		#endregion

		[JsonConstructor]
		private SnapshotSummary() { }

		public SnapshotSummary(SnapshotContent parent)
		{
			Parent = parent;

			ApplySettingsForAll = MediaMetaData.Instance.ListManager.DefaultSnapshotSettings.UniversalToggles;

			ContractSettings = new ContractSettings();

			#region Options
			ShowLineId = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowLineId;
			ShowLogo = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowLogo;
			ShowCampaign = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowCampaign;
			ShowComments = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowComments;
			ShowSpots = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowSpots;
			ShowCost = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowCost;
			ShowTotalWeeks = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowTotalWeeks;
			ShowTotalCost = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowTotalCost;
			ShowTallySpots = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowTallySpots;
			ShowTallyCost = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowTallyCost;
			ShowSpotsX = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowSpotsX;
			UseDecimalRates = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.UseDecimalRates;
			#endregion
		}

		public void Dispose()
		{
			ContractSettings = null;
			Parent = null;
		}
	}
}
