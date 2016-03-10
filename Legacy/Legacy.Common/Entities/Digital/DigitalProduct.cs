using System;
using System.Collections.Generic;
using System.Xml;
using Asa.Legacy.Common.Entities.Summary;

namespace Asa.Legacy.Common.Entities.Digital
{
	public class DigitalProduct
	{
		private string _name;
		private string _userDescription;
		private string _userComment;

		#region Basic Properties
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
		public CustomSummaryItem SummaryItem { get; private set; }
		#endregion

		#region Calculated Properties
		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public string Comment
		{
			get
			{
				if (!String.IsNullOrEmpty(_userDescription) && CommentManualEdit)
					return _userComment;
				return null;
			}
		}

		public string UserDescription
		{
			get
			{
				if (!String.IsNullOrEmpty(_userDescription) && DescriptionManualEdit)
					return _userDescription;
				return null;
			}
		}
		#endregion

		public DigitalProduct()
		{
			UniqueID = Guid.NewGuid();
			Category = string.Empty;
			Websites = new List<string>();
			AddtionalInfo = new List<ProductInfo>();
			PackageRecord = new ProductPackageRecord();
			SummaryItem = new CustomSummaryItem();
			RateType = "CPM";
			EnableLocation = true;
			EnableTarget = true;
			EnableRichMedia = true;
		}

		public void Deserialize(XmlNode node)
		{
			int tempInt;
			decimal tempDecimal;

			foreach (XmlAttribute productAttribute in node.Attributes)
				switch (productAttribute.Name)
				{
					#region Basic Properties
					case "Name":
						Name = productAttribute.Value;
						break;
					case "UniqueID":
						Guid tempGuid;
						if (Guid.TryParse(productAttribute.Value, out tempGuid))
							UniqueID = tempGuid;
						break;
					case "Index":
						if (int.TryParse(productAttribute.Value, out tempInt))
							Index = tempInt;
						break;
					case "Category":
						Category = productAttribute.Value;
						break;
					case "SubCategory":
						SubCategory = productAttribute.Value;
						break;
					case "RateType":
						RateType = productAttribute.Value;
						switch (RateType)
						{
							case "0":
								RateType = "CPM";
								break;
							case "1":
								RateType = "Fixed";
								break;
						}
						break;
					case "Location":
						Location = productAttribute.Value;
						break;
					case "Width":
						if (int.TryParse(productAttribute.Value, out tempInt))
							Width = tempInt;
						else
							Width = null;
						break;
					case "Height":
						if (int.TryParse(productAttribute.Value, out tempInt))
							Height = tempInt;
						else
							Height = null;
						break;
					case "EnableTarget":
						{
							bool temp;
							if (Boolean.TryParse(productAttribute.Value, out temp))
								EnableTarget = temp;
						}
						break;
					case "EnableLocation":
						{
							bool temp;
							if (Boolean.TryParse(productAttribute.Value, out temp))
								EnableLocation = temp;
						}
						break;
					case "EnableRichMedia":
						{
							bool temp;
							if (Boolean.TryParse(productAttribute.Value, out temp))
								EnableRichMedia = temp;
						}
						break;
					#endregion

					#region Additional Properties
					case "UserDefinedName":
						UserDefinedName = productAttribute.Value;
						break;
					case "DurationType":
						DurationType = productAttribute.Value;
						break;
					case "DurationValue":
						if (int.TryParse(productAttribute.Value, out tempInt))
							DurationValue = tempInt;
						else
							DurationValue = null;
						break;
					case "Strength1":
						Strength1 = productAttribute.Value;
						break;
					case "Strength2":
						Strength2 = productAttribute.Value;
						break;
					case "MonthlyInvestment":
						if (Decimal.TryParse(productAttribute.Value, out tempDecimal))
							MonthlyInvestment = tempDecimal;
						else
							MonthlyInvestment = null;
						break;
					case "MonthlyImpressions":
						if (Decimal.TryParse(productAttribute.Value, out tempDecimal))
							MonthlyImpressions = tempDecimal;
						else
							MonthlyImpressions = null;
						break;
					case "MonthlyCPM":
						if (Decimal.TryParse(productAttribute.Value, out tempDecimal))
							MonthlyCPM = tempDecimal;
						else
							MonthlyCPM = null;
						break;
					case "TotalInvestment":
						if (Decimal.TryParse(productAttribute.Value, out tempDecimal))
							TotalInvestment = tempDecimal;
						else
							TotalInvestment = null;
						break;
					case "TotalImpressions":
						if (Decimal.TryParse(productAttribute.Value, out tempDecimal))
							TotalImpressions = tempDecimal;
						else
							TotalImpressions = null;
						break;
					case "TotalCPM":
						if (Decimal.TryParse(productAttribute.Value, out tempDecimal))
							TotalCPM = tempDecimal;
						else
							TotalCPM = null;
						break;
					case "DefaultRate":
						if (Decimal.TryParse(productAttribute.Value, out tempDecimal))
							DefaultRate = tempDecimal;
						else
							DefaultRate = null;
						break;
					case "Formula":
						if (int.TryParse(productAttribute.Value, out tempInt))
							Formula = (FormulaType)tempInt;
						break;
					case "InvestmentDetails":
						InvestmentDetails = productAttribute.Value;
						break;

					case "EnableComment":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								EnableComment = tempBool;
						}
						break;
					case "CommentManualEdit":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								CommentManualEdit = tempBool;
						}
						break;
					case "ShowCommentTargeting":
						{
							bool temp;
							if (Boolean.TryParse(productAttribute.Value, out temp))
								ShowCommentTargeting = temp;
						}
						break;
					case "ShowCommentRichMedia":
						{
							bool temp;
							if (Boolean.TryParse(productAttribute.Value, out temp))
								ShowCommentRichMedia = temp;
						}
						break;
					case "Comment":
						_userComment = productAttribute.Value;
						break;
					case "DescriptionManualEdit":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								DescriptionManualEdit = tempBool;
						}
						break;
					case "ShowDimensions":
						{
							bool temp;
							if (Boolean.TryParse(productAttribute.Value, out temp))
								ShowDimensions = temp;
						}
						break;
					case "ShowDescriptionTargeting":
						{
							bool temp;
							if (Boolean.TryParse(productAttribute.Value, out temp))
								ShowDescriptionTargeting = temp;
						}
						break;
					case "ShowDescriptionRichMedia":
						{
							bool temp;
							if (Boolean.TryParse(productAttribute.Value, out temp))
								ShowDescriptionRichMedia = temp;
						}
						break;
					case "Description":
						Description = productAttribute.Value;
						break;
					case "UserDescription":
						_userDescription = productAttribute.Value;
						break;
					#endregion

					#region Show Properties
					case "ShowFlightDates":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								ShowFlightDates = tempBool;
						}
						break;
					case "ShowDuration":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								ShowDuration = tempBool;
						}
						break;
					case "ShowAllPricingMonthly":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								ShowAllPricingMonthly = tempBool;
						}
						break;
					case "ShowAllPricingTotal":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								ShowAllPricingTotal = tempBool;
						}
						break;
					case "ShowMonthlyInvestments":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								ShowMonthlyInvestments = tempBool;
						}
						break;
					case "ShowMonthlyImpressions":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								ShowMonthlyImpressions = tempBool;
						}
						break;
					case "ShowTotalInvestments":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								ShowTotalInvestments = tempBool;
						}
						break;
					case "ShowTotalImpressions":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								ShowTotalImpressions = tempBool;
						}
						break;
						#endregion
				}
			Websites.Clear();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Website":
						Websites.Add(childNode.InnerText);
						break;
					case "PackageRecord":
						PackageRecord.Deserialize(childNode);
						break;
					case "SummaryItem":
						SummaryItem.Deserialize(childNode);
						break;
					case "ProductInfo":
						var productInfo = new ProductInfo();
						productInfo.Deserialize(childNode);
						AddtionalInfo.Add(productInfo);
						break;
				}
			}
		}
	}
}
