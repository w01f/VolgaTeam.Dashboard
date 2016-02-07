using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Asa.Business.Common.Entities.NonPersistent.Summary;
using Asa.Business.Common.Interfaces;
using Asa.Business.Online.Common;
using Asa.Business.Online.Configuration;
using Asa.Business.Online.Enums;
using Asa.Business.Online.Interfaces;
using Asa.Common.Core.Interfaces;
using Newtonsoft.Json;
using ListManager = Asa.Business.Online.Dictionaries.ListManager;

namespace Asa.Business.Online.Entities.NonPersistent
{
	public class DigitalProduct : ISummaryProduct, IJsonCloneable<DigitalProduct>
	{
		private string _name;
		private string _userDescription;
		private string _userComment;

		#region Basic Properties
		public DigitalProductsContent Parent { get; set; }
		public Guid UniqueID { get; set; }
		public double Index { get; set; }
		public string Category { get; set; }
		public string SubCategory { get; set; }
		public string RateType { get; set; }
		public string Location { get; set; }
		public int? Width { get; set; }
		public int? Height { get; set; }
		public bool EnableLocation { get; set; }
		public bool EnableTarget { get; set; }
		public bool EnableRichMedia { get; set; }
		public bool EnableRate { get; set; }
		public List<ProductInfo> AddtionalInfo { get; private set; }
		#endregion

		#region Additional Properties
		public string UserDefinedName { get; set; }
		public List<string> Websites { get; set; }
		public string DurationType { get; set; }
		public int? DurationValue { get; set; }
		public string Strength1 { get; set; }
		public string Strength2 { get; set; }
		public decimal? MonthlyInvestment { get; set; }
		public decimal? MonthlyImpressions { get; set; }
		public decimal? MonthlyCPM { get; set; }
		public decimal? TotalInvestment { get; set; }
		public decimal? TotalImpressions { get; set; }
		public decimal? TotalCPM { get; set; }
		public decimal? DefaultRate { get; set; }
		public FormulaType Formula { get; set; }
		public string InvestmentDetails { get; set; }

		public bool DescriptionManualEdit { get; set; }
		public bool ShowDimensions { get; set; }
		public bool ShowDescriptionTargeting { get; set; }
		public bool ShowDescriptionRichMedia { get; set; }
		public string Description { get; set; }

		public bool EnableComment { get; set; }
		public bool CommentManualEdit { get; set; }
		public bool ShowCommentTargeting { get; set; }
		public bool ShowCommentRichMedia { get; set; }
		#endregion

		#region Show Properties
		public bool ShowDuration { get; set; }
		public bool ShowAllPricingMonthly { get; set; }
		public bool ShowAllPricingTotal { get; set; }
		public bool ShowMonthlyInvestments { get; set; }
		public bool ShowMonthlyImpressions { get; set; }
		public bool ShowTotalInvestments { get; set; }
		public bool ShowTotalImpressions { get; set; }
		public bool ShowFlightDates { get; set; }

		public ProductPackageRecord PackageRecord { get; private set; }
		public DigitalProductAdPlanSettings AdPlanSettings { get; set; }
		public CustomSummaryItem SummaryItem { get; private set; }

		#endregion

		public DigitalProductOutputData OutputData { get; private set; }

		#region Calculated Properties

		public IDigitalScheduleSettings Settings
		{
			get { return Parent.ScheduleSettings; }
		}

		[JsonIgnore]
		public string Name
		{
			get { return _name; }
			set
			{
				string oldValue = _name;
				_name = value;
				if (string.IsNullOrEmpty(oldValue))
					ApplyDefaultValues();
			}
		}

		public string ExtendedName
		{
			get { return String.Format("{0}{1}", !String.IsNullOrEmpty(SubCategory) ? (SubCategory + " - ") : String.Empty, Name); }
		}

		public string FullName
		{
			get
			{
				return String.Format("{0} - {1}{2}",
					Name,
					Category,
					!String.IsNullOrEmpty(SubCategory) ? String.Format(@" / {0}", SubCategory) : String.Empty);
			}
		}

		private string CalculatedComment
		{
			get
			{
				var result = new List<string>();
				if (ShowCommentTargeting && EnableTarget && AddtionalInfo.Any(pi => pi.Type == ProductInfoType.Targeting && pi.Selected))
					result.AddRange(AddtionalInfo.Where(pi => pi.Type == ProductInfoType.Targeting && pi.Selected).Select(pi => pi.EditValue));
				if (ShowCommentRichMedia && EnableRichMedia && AddtionalInfo.Any(pi => pi.Type == ProductInfoType.RichMedia && pi.Selected))
					result.AddRange(AddtionalInfo.Where(pi => pi.Type == ProductInfoType.RichMedia && pi.Selected).Select(pi => pi.EditValue));
				return String.Join(Environment.NewLine, result);
			}
		}

		[JsonIgnore]
		public string Comment
		{
			get
			{
				if (!String.IsNullOrEmpty(_userDescription) && CommentManualEdit)
					return _userComment;
				return CalculatedComment;
			}
			set
			{
				_userComment = value != CalculatedComment && CommentManualEdit ? value : null;
			}
		}

		private string CalculatedDescription
		{
			get
			{
				var result = new List<string>();
				if (Width.HasValue && Height.HasValue && ShowDimensions)
					result.Add(String.Format("(Ad Dimensions: {0}{1})", Dimensions, !String.IsNullOrEmpty(Location) && !Location.Equals("N/A") ? String.Format(" Location - {0}", Location) : String.Empty));
				if (ShowDescriptionTargeting && EnableTarget && AddtionalInfo.Any(pi => pi.Type == ProductInfoType.Targeting && pi.Selected))
					result.AddRange(AddtionalInfo.Where(pi => pi.Type == ProductInfoType.Targeting && pi.Selected).Select(pi => pi.EditValue));
				if (ShowDescriptionRichMedia && EnableRichMedia && AddtionalInfo.Any(pi => pi.Type == ProductInfoType.RichMedia && pi.Selected))
					result.AddRange(AddtionalInfo.Where(pi => pi.Type == ProductInfoType.RichMedia && pi.Selected).Select(pi => pi.EditValue));
				if (!String.IsNullOrEmpty(Description))
					result.Add(String.Format("{1}{0}", Description, Environment.NewLine));
				return String.Join(Environment.NewLine, result);
			}
		}

		[JsonIgnore]
		public string UserDescription
		{
			get
			{
				if (!String.IsNullOrEmpty(_userDescription) && DescriptionManualEdit)
					return _userDescription;
				return CalculatedDescription;
			}
			set { _userDescription = value != CalculatedDescription && DescriptionManualEdit ? value : null; }
		}

		public string Dimensions
		{
			get
			{
				if (Width.HasValue && Height.HasValue)
					return Width.Value + " x " + Height.Value;
				return String.Empty;
			}
		}

		public decimal? MonthlyInvestmentCalculated
		{
			get
			{
				if (Formula == FormulaType.Investment)
					return MonthlyImpressions.HasValue && MonthlyCPMCalculated.HasValue ? Math.Round(MonthlyCPMCalculated.Value * (MonthlyImpressions.Value / 1000), 2) : (decimal?)null;
				return MonthlyInvestment;
			}
		}

		public decimal? TotalInvestmentCalculated
		{
			get
			{
				if (Formula == FormulaType.Investment)
					return TotalImpressions.HasValue && TotalCPMCalculated.HasValue ? Math.Round((TotalCPMCalculated.Value * (TotalImpressions.Value / 1000)), 2) : (decimal?)null;
				return TotalInvestment;
			}
		}

		public decimal? MonthlyImpressionsCalculated
		{
			get
			{
				if (Formula == FormulaType.Impressions)
					return MonthlyCPMCalculated.HasValue && MonthlyInvestment.HasValue ? (MonthlyCPMCalculated.Value != 0 ? Math.Round(((MonthlyInvestment.Value * 1000) / MonthlyCPMCalculated.Value), 0) : (decimal?)null) : null;
				return MonthlyImpressions;
			}
		}

		public decimal? TotalImpressionsCalculated
		{
			get
			{
				if (Formula == FormulaType.Impressions)
					return TotalCPMCalculated.HasValue && TotalInvestment.HasValue ? (TotalCPMCalculated.Value != 0 ? Math.Round(((TotalInvestment.Value * 1000) / TotalCPMCalculated.Value), 0) : (decimal?)null) : null;
				return TotalImpressions;
			}
		}

		public decimal? MonthlyCPMCalculated
		{
			get
			{
				if (Formula == FormulaType.CPM)
					return MonthlyImpressions.HasValue && MonthlyInvestment.HasValue ? (MonthlyImpressions.Value != 0 ? Math.Round(MonthlyInvestment.Value / (MonthlyImpressions.Value / 1000), 2) : (decimal?)null) : null;
				return !MonthlyCPM.HasValue && RateType.Equals("CPM") ? DefaultRate : MonthlyCPM;
			}
		}

		public decimal? TotalCPMCalculated
		{
			get
			{
				if (Formula == FormulaType.CPM)
					return TotalImpressions.HasValue && TotalInvestment.HasValue ? (TotalImpressions.Value != 0 ? Math.Round((TotalInvestment.Value / (TotalImpressions.Value / 1000)), 2) : (decimal?)null) : null;
				return !TotalCPM.HasValue && RateType.Equals("CPM") ? DefaultRate : TotalCPM;
			}
		}

		public int WeeksDuration
		{
			get
			{
				var result = 0;
				if (!Settings.FlightDateStart.HasValue || !Settings.FlightDateEnd.HasValue) return result;
				var diff = Settings.FlightDateEnd.Value.Subtract(Settings.FlightDateStart.Value);
				result = diff.Days / 7;
				return result;
			}
		}

		public int MonthDuraton
		{
			get
			{
				if (!Settings.FlightDateStart.HasValue || !Settings.FlightDateEnd.HasValue) return 0;
				return Math.Abs((Settings.FlightDateEnd.Value.Month - Settings.FlightDateStart.Value.Month) + 12 * (Settings.FlightDateEnd.Value.Year - Settings.FlightDateStart.Value.Year));
			}
		}

		public string ProductSummary
		{
			get
			{
				var result = new List<string>();
				if (MonthlyImpressionsCalculated.HasValue)
					result.Add(String.Format("Monthly Impressions: {0}", MonthlyImpressionsCalculated.Value.ToString("#,##0")));
				if (MonthlyCPMCalculated.HasValue)
					result.Add(String.Format("Monthly CPM: {0}", MonthlyCPMCalculated.Value.ToString("$#,###.00")));
				if (MonthlyInvestmentCalculated.HasValue)
					result.Add(String.Format("Monthly Investment: {0}", MonthlyInvestmentCalculated.Value.ToString("$#,###.00")));
				if (TotalImpressionsCalculated.HasValue)
					result.Add(String.Format("Total Impressions: {0}", TotalImpressionsCalculated.Value.ToString("#,##0")));
				if (TotalCPMCalculated.HasValue)
					result.Add(String.Format("Total CPM: {0}", TotalCPMCalculated.Value.ToString("$#,###.00")));
				if (TotalInvestmentCalculated.HasValue)
					result.Add(String.Format("Total Investment: {0}", TotalInvestmentCalculated.Value.ToString("$#,###.00")));
				result.Add(String.Format("Ad Campaign Flight Dates: {0}", Settings.FlightDates));
				return String.Join(", ", result.ToArray());
			}
		}

		public bool TargetingAvailable
		{
			get
			{
				return Settings.DigitalProductListViewSettings.EnableDigitalTargeting &&
					EnableTarget &&
					AddtionalInfo.Any(productInfo => productInfo.Type == ProductInfoType.Targeting &&
						productInfo.Selected);
			}
		}

		public bool RichMediaAvailable
		{
			get
			{
				return Settings.DigitalProductListViewSettings.EnableDigitalRichMedia &&
					EnableRichMedia &&
					AddtionalInfo.Any(productInfo => productInfo.Type == ProductInfoType.RichMedia &&
						productInfo.Selected);
			}
		}
		#endregion

		[JsonConstructor]
		private DigitalProduct() { }

		public DigitalProduct(DigitalProductsContent parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Category = string.Empty;
			Websites = new List<string>();
			AddtionalInfo = new List<ProductInfo>();
			PackageRecord = new ProductPackageRecord(this);
			AdPlanSettings = new DigitalProductAdPlanSettings();
			SummaryItem = new ProductSummaryItem(this);
			RateType = "CPM";
			EnableLocation = true;
			EnableTarget = true;
			EnableRichMedia = true;

			OutputData = new DigitalProductOutputData(this);

			ApplyDefaultView();
		}

		public void Dispose()
		{
			Websites.Clear();
			AddtionalInfo.Clear();

			PackageRecord.Dispose();
			PackageRecord = null;

			AdPlanSettings.Dispose();
			AdPlanSettings = null;

			SummaryItem = null;

			OutputData.Dispose();
			OutputData = null;

			Parent = null;
		}

		[OnDeserialized]
		public void AfterDeserialize(StreamingContext context)
		{
			if (String.IsNullOrEmpty(UserDefinedName))
				UserDefinedName = ExtendedName;
			UpdateAdditionlaInfo();
		}

		public void ApplyDefaultValues()
		{
			var source = ListManager.Instance.ProductSources.FirstOrDefault(x => x.Name.Equals(_name) && x.Category.Name.Equals(Category) && (x.SubCategory == SubCategory || String.IsNullOrEmpty(SubCategory)));
			if (source == null) return;
			RateType = source.RateType;
			DefaultRate = source.Rate;
			Width = source.Width;
			Height = source.Height;
			EnableTarget = source.EnableTarget;
			EnableLocation = source.EnableLocation;
			EnableRichMedia = source.EnableRichMedia;
			EnableRate = source.EnableRate;
			Description = source.Overview;
			Websites.AddRange(ListManager.Instance.Websites.Where(s => s == source.DefaultWebsite));
			UpdateAdditionlaInfo();
		}

		public void ApplyDefaultView()
		{
			UserDefinedName = ExtendedName;
			Strength1 = String.Empty;
			Strength2 = String.Empty;
			DurationType = String.Empty;
			DurationValue = null;
			MonthlyInvestment = null;
			MonthlyImpressions = null;
			TotalInvestment = null;
			TotalImpressions = null;
			Formula = ListManager.Instance.DefaultFormula;
			InvestmentDetails = null;

			DescriptionManualEdit = false;
			ShowDimensions = true;
			ShowDescriptionTargeting = false;
			ShowDescriptionRichMedia = false;
			_userDescription = null;
			EnableComment = false;
			CommentManualEdit = false;
			ShowCommentTargeting = false;
			ShowCommentRichMedia = false;
			_userComment = null;

			var source = ListManager.Instance.ProductSources.FirstOrDefault(x => x.Name.Equals(_name) && x.Category.Name.Equals(Category) && (x.SubCategory == SubCategory || String.IsNullOrEmpty(SubCategory)));
			if (source != null)
			{
				Description = source.Overview;
				DefaultRate = source.Rate;
				MonthlyCPM = DefaultRate;
				TotalCPM = DefaultRate;
			}

			var defaultViewSettings = ListManager.Instance.DefaultDigitalProductSettings;
			ShowFlightDates = defaultViewSettings.ShowFlightDates;
			ShowDuration = defaultViewSettings.ShowDuration;
			switch (defaultViewSettings.DefaultPricing)
			{
				case 1:
					ShowAllPricingMonthly = true;
					ShowAllPricingTotal = false;
					ShowMonthlyImpressions = false;
					ShowTotalImpressions = false;
					ShowMonthlyInvestments = false;
					ShowTotalInvestments = false;
					break;
				case 2:
					ShowAllPricingMonthly = false;
					ShowAllPricingTotal = true;
					ShowMonthlyImpressions = false;
					ShowTotalImpressions = false;
					ShowMonthlyInvestments = false;
					ShowTotalInvestments = false;
					break;
				case 3:
					ShowAllPricingMonthly = false;
					ShowAllPricingTotal = false;
					ShowMonthlyImpressions = true;
					ShowTotalImpressions = false;
					ShowMonthlyInvestments = false;
					ShowTotalInvestments = false;
					break;
				case 4:
					ShowAllPricingMonthly = false;
					ShowAllPricingTotal = false;
					ShowMonthlyImpressions = false;
					ShowTotalImpressions = false;
					ShowMonthlyInvestments = true;
					ShowTotalInvestments = false;
					break;
				default:
					ShowAllPricingMonthly = false;
					ShowAllPricingTotal = false;
					ShowMonthlyInvestments = false;
					ShowMonthlyImpressions = false;
					ShowTotalImpressions = false;
					ShowTotalInvestments = false;
					break;
			}
		}

		public void AfterClone(DigitalProduct source, bool fullClone = true)
		{
			UniqueID = Guid.NewGuid();
			Parent = source.Parent;
			Index = Index + 0.5;
			Parent.DigitalProducts.Add(this);
			Parent.DigitalProducts.Sort((x, y) => x.Index.CompareTo(y.Index));
			Parent.RebuildDigitalProductIndexes();
		}

		public void UpdateAdditionlaInfo()
		{
			var newAddtionalInfo = new List<ProductInfo>();
			if (EnableTarget)
				newAddtionalInfo.AddRange(ListManager.Instance.TargetingRecods.MergeSet(AddtionalInfo));
			if (EnableRichMedia)
				newAddtionalInfo.AddRange(ListManager.Instance.RichMediaRecods.MergeSet(AddtionalInfo));
			AddtionalInfo.Clear();
			AddtionalInfo.AddRange(newAddtionalInfo);
		}

		public decimal SummaryOrder
		{
			get { return (Decimal)Index; }
		}

		public string SummaryTitle
		{
			get { return String.Format("{0}{1}", Name, (!String.IsNullOrEmpty(Location) && Location != "N/A" ? String.Format(" ({0})", Location) : String.Empty)); }
		}

		public string SummaryInfo
		{
			get
			{
				var values = new List<string>();
				values.Add(FullName);
				if (!String.IsNullOrEmpty(OutputData.Websites))
					values.Add(OutputData.Websites);
				values.Add(String.Format("{0}", Settings.FlightDates));
				values.Add(String.Format("{0}", UserDescription));
				return String.Join(", ", values).Replace(Environment.NewLine, ", ");
			}
		}
	}
}
