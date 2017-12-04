using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Common.Core.Dictionaries;
using Newtonsoft.Json;

namespace Asa.Business.Online.Entities.NonPersistent
{
	public class DigitalProductOutputData
	{
		private DigitalProduct _source;

		public string Header => "{0}";

		public string Websites => String.Join(", ", _source.Websites.ToArray());

		public string PresentationDate => _source.Settings.PresentationDate?.ToString("MM/dd/yy") ?? String.Empty;

		public string BusinessName => _source.Settings.BusinessName;

		public string DecisionMaker => _source.Settings.DecisionMaker;

		public string FlightDates
		{
			get
			{
				var campaignStack = new List<string>();
				if (_source.ShowFlightDates)
					campaignStack.Add(_source.Settings.FlightDates);
				if (!String.IsNullOrEmpty(DurationType) && !String.IsNullOrEmpty(DurationValue))
					campaignStack.Add(String.Format("{0} {1}", DurationValue, DurationType));
				return campaignStack.Any() ? String.Format("Campaign:  {0}", String.Join("      ", campaignStack)) : String.Empty;
			}
		}

		public string DurationValue => _source.DurationValue.HasValue && _source.ShowDuration ? _source.DurationValue.Value.ToString("#,##0") : String.Empty;

		public string DurationType => _source.ShowDuration ? _source.DurationType : String.Empty;

		public string ProductName => !String.IsNullOrEmpty(_source.UserDefinedName) ? _source.UserDefinedName : _source.ExtendedName;

		public string Description => _source.UserDescription;

		public decimal? Impressions
		{
			get
			{
				decimal? result = null;
				if (_source.ShowAllPricingTotal || _source.ShowAllPricingMonthly || _source.ShowMonthlyImpressions || _source.ShowTotalImpressions)
				{
					if (_source.ShowAllPricingMonthly && _source.MonthlyImpressionsCalculated.HasValue && _source.MonthlyImpressionsCalculated.Value > 0)
						result = _source.MonthlyImpressionsCalculated;
					else if (_source.ShowAllPricingTotal && _source.TotalImpressionsCalculated.HasValue && _source.TotalImpressionsCalculated.Value > 0)
						result = _source.TotalImpressionsCalculated;
					else if (_source.ShowMonthlyImpressions && _source.MonthlyImpressions.HasValue && _source.MonthlyImpressions.Value > 0)
						result = _source.MonthlyImpressions;
					else if (_source.ShowTotalImpressions && _source.TotalImpressions.HasValue && _source.TotalImpressions.Value > 0)
						result = _source.TotalImpressions;
				}
				return result;
			}
		}

		public decimal? Investments
		{
			get
			{
				decimal? result = null;
				if (_source.ShowAllPricingTotal || _source.ShowAllPricingMonthly || _source.ShowMonthlyInvestments || _source.ShowTotalInvestments)
				{
					if (_source.ShowAllPricingMonthly && _source.MonthlyInvestmentCalculated.HasValue && _source.MonthlyInvestmentCalculated.Value > 0)
						result = _source.MonthlyInvestmentCalculated;
					else if (_source.ShowAllPricingTotal && _source.TotalInvestmentCalculated.HasValue && _source.TotalInvestmentCalculated.Value > 0)
						result = _source.TotalInvestmentCalculated;
					else if (_source.ShowMonthlyInvestments && _source.MonthlyInvestment.HasValue && _source.MonthlyInvestment.Value > 0)
						result = _source.MonthlyInvestment;
					else if (_source.ShowTotalInvestments && _source.TotalInvestment.HasValue && _source.TotalInvestment.Value > 0)
						result = _source.TotalInvestment;
				}
				return result;
			}
		}

		public decimal? CPM
		{
			get
			{
				decimal? result = null;
				if (_source.ShowAllPricingTotal || _source.ShowAllPricingMonthly)
				{
					if (_source.ShowAllPricingMonthly && _source.MonthlyCPMCalculated.HasValue && _source.MonthlyCPMCalculated.Value > 0)
						result = _source.MonthlyCPMCalculated;
					else if (_source.ShowAllPricingTotal && _source.TotalCPMCalculated.HasValue && _source.TotalCPMCalculated.Value > 0)
						result = _source.TotalCPMCalculated;
				}
				return result;
			}
		}

		public IEnumerable<NameCodePair> MonthlyData
		{
			get
			{
				var result = new List<NameCodePair>();
				if (_source.ShowAllPricingMonthly)
				{
					if (_source.MonthlyImpressionsCalculated.HasValue && _source.MonthlyImpressionsCalculated.Value > 0)
						result.Add(new NameCodePair { Name = "monthly impressions:", Code = _source.MonthlyImpressionsCalculated.Value.ToString("#,##0") });
					if (_source.MonthlyInvestmentCalculated.HasValue && _source.MonthlyInvestmentCalculated.Value > 0)
						result.Add(new NameCodePair { Name = "monthly investment:", Code = _source.MonthlyInvestmentCalculated.Value.ToString("$#,###.00") });
					if (_source.MonthlyCPMCalculated.HasValue && _source.MonthlyCPMCalculated.Value > 0)
						result.Add(new NameCodePair { Name = "cpm:", Code = _source.MonthlyCPMCalculated.Value.ToString("$#,###.00") });
				}
				else if (_source.ShowMonthlyInvestments && _source.MonthlyInvestment.HasValue)
				{
					result.Add(new NameCodePair { Name = "monthly investment:", Code = _source.MonthlyInvestment.Value.ToString("$#,###.00") });
				}
				else if (_source.ShowMonthlyImpressions && _source.MonthlyImpressions.HasValue)
				{
					result.Add(new NameCodePair { Name = "monthly impressions:", Code = _source.MonthlyImpressions.Value.ToString("#,##0") });
				}
				return result;
			}
		}

		public IEnumerable<NameCodePair> TotalData
		{
			get
			{
				var result = new List<NameCodePair>();
				if (_source.ShowAllPricingTotal)
				{
					if (_source.TotalImpressionsCalculated.HasValue && _source.TotalImpressionsCalculated.Value > 0)
						result.Add(new NameCodePair { Name = "total impressions:", Code = _source.TotalImpressionsCalculated.Value.ToString("#,##0") });
					if (_source.TotalInvestmentCalculated.HasValue && _source.TotalInvestmentCalculated.Value > 0)
						result.Add(new NameCodePair { Name = "total investment:", Code = _source.TotalInvestmentCalculated.Value.ToString("$#,###.00") });
					if (_source.TotalCPMCalculated.HasValue && _source.TotalCPMCalculated.Value > 0)
						result.Add(new NameCodePair { Name = "cpm:", Code = _source.TotalCPMCalculated.Value.ToString("$#,###.00") });
				}
				else if (_source.ShowTotalInvestments && _source.TotalInvestment.HasValue)
				{
					result.Add(new NameCodePair { Name = "total investment:", Code = _source.TotalInvestment.Value.ToString("$#,###.00") });
				}
				else if (_source.ShowTotalImpressions && _source.TotalImpressions.HasValue)
				{
					result.Add(new NameCodePair { Name = "total impressions:", Code = _source.TotalImpressions.Value.ToString("#,##0") });
				}
				return result;
			}
		}

		public string InvestmentDetails => _source.InvestmentDetails;

		public string Comment
		{
			get
			{
				var list = new List<string>();
				if (!String.IsNullOrEmpty(_source.Strength1))
					list.Add(_source.Strength1);
				if (!String.IsNullOrEmpty(_source.Strength2))
					list.Add(_source.Strength2);
				if (!String.IsNullOrEmpty(_source.Comment))
					list.Add(_source.Comment);
				return String.Join(". ", list.ToArray());
			}
		}

		public string[] GetSlideSource()
		{
			if (!String.IsNullOrEmpty(Comment))
			{
				if (MonthlyData.Any())
					return new[] { "comments", String.Format("monthly{0}.pptx", !String.IsNullOrEmpty(InvestmentDetails) ? "_inv" : String.Empty) };
				if (TotalData.Any())
					return new[] { "comments", String.Format("total{0}.pptx", !String.IsNullOrEmpty(InvestmentDetails) ? "_inv" : String.Empty) };
				return new[] { "comments", String.Format("none{0}.pptx", !String.IsNullOrEmpty(InvestmentDetails) ? "_inv" : String.Empty) };
			}
			else
			{
				if (MonthlyData.Any())
					return new[] { "no_comments", String.Format("monthly{0}.pptx", !String.IsNullOrEmpty(InvestmentDetails) ? "_inv" : String.Empty) };
				if (TotalData.Any())
					return new[] { "no_comments", String.Format("total{0}.pptx", !String.IsNullOrEmpty(InvestmentDetails) ? "_inv" : String.Empty) };
				return new[] { "no_comments", String.Format("none{0}.pptx", !String.IsNullOrEmpty(InvestmentDetails) ? "_inv" : String.Empty) };
			}
		}

		[JsonConstructor]
		private DigitalProductOutputData() { }

		public DigitalProductOutputData(DigitalProduct parent)
		{
			_source = parent;
		}

		public void Dispose()
		{
			_source = null;
		}
	}
}
