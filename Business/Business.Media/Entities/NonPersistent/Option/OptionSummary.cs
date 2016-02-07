using System.Linq;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Objects.Output;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Option
{
	public class OptionSummary
	{
		public OptionsContent Parent { get; private set; }
		public SpotType SpotType { get; set; }
		public bool ApplySettingsForAll { get; set; }

		public ContractSettings ContractSettings { get; private set; }

		#region Options
		public bool ShowLineId { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowCampaign { get; set; }
		public bool ShowComments { get; set; }
		public bool ShowSpots { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowTotalPeriods { get; set; }
		public bool ShowTotalCost { get; set; }
		public bool ShowTallySpots { get; set; }
		public bool ShowTallyCost { get; set; }
		public bool ShowSpotsX { get; set; }
		public bool UseDecimalRates { get; set; }
		#endregion

		#region Calculated Properties
		public bool Enabled
		{
			get { return Parent.Options.All(o => o.SpotType == SpotType); }
		}

		public decimal TotalCost
		{
			get { return Parent.Options.Any() ? Parent.Options.Select(x => x.TotalPeriodCost).Sum() : 0; }
		}

		public int TotalSpots
		{
			get { return Parent.Options.Any() ? Parent.Options.Select(x => x.TotalPeriodSpots).Sum() : 0; }
		}
		#endregion

		[JsonConstructor]
		private OptionSummary() { }

		public OptionSummary(OptionsContent parent)
		{
			Parent = parent;

			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowWeeklySpots)
				SpotType = SpotType.Week;
			else if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowMonthlySpots)
				SpotType = SpotType.Month;
			else if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowTotalSpots)
				SpotType = SpotType.Total;
			else
				SpotType = SpotType.Week;

			ApplySettingsForAll = false;

			ContractSettings = new ContractSettings();

			#region Options
			ShowLineId = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowLineId;
			ShowLogo = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowLogo;
			ShowCampaign = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowCampaign;
			ShowComments = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowComments;
			ShowTotalCost = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowTotalCost;
			ShowTallySpots = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowTallySpots;
			ShowTallyCost = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowTallyCost;
			ShowSpotsX = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowSpotsX;
			UseDecimalRates = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.UseDecimalRates;
			#endregion

			UpdateSpotType(true);
		}

		public void Dispose()
		{
			ContractSettings = null;
			Parent = null;
		}

		public void UpdateSpotType(bool force = false)
		{
			var spotType = SpotType;
			if (Parent.Options.Any())
			{
				if (Parent.Options.All(o => o.SpotType == SpotType.Week))
					spotType = SpotType.Week;
				else if (Parent.Options.All(o => o.SpotType == SpotType.Month))
					spotType = SpotType.Month;
				if (Parent.Options.All(o => o.SpotType == SpotType.Total))
					spotType = SpotType.Total;
			}
			if (spotType != SpotType || force)
			{
				SpotType = spotType;
				switch (SpotType)
				{
					case SpotType.Week:
						ShowSpots = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowWeeklySpots;
						ShowCost = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowWeeklyCost;
						ShowTotalPeriods = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowTotalWeeks;
						break;
					case SpotType.Month:
						ShowSpots = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowMonthlySpots;
						ShowCost = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowMonthlyCost;
						ShowTotalPeriods = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowTotalMonths;
						break;
					case SpotType.Total:
						ShowSpots = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowTotalSpots;
						ShowCost = false;
						ShowTotalPeriods = false;
						break;
				}
			}
		}
	}
}
