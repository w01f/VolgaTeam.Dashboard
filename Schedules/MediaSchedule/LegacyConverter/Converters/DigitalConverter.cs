using System;
using Asa.Legacy.Media.Entities.Schedule;

namespace Asa.Media.LegacyConverter.Converters
{
	static class DigitalConverter
	{
		public static void ImportData(
			this Business.Online.Entities.NonPersistent.DigitalProductsContent target,
			RegularSchedule source)
		{
			foreach (var oldDigitalProduct in source.DigitalProducts)
			{
				var diigitalProduct = new Business.Online.Entities.NonPersistent.DigitalProduct(target);
				diigitalProduct.ImportData(oldDigitalProduct);
				target.DigitalProducts.Add(diigitalProduct);
			}

			target.DigitalProductSummary.ImportData(source.DigitalProductSummary);
		}

		private static void ImportData(
			this Business.Online.Entities.NonPersistent.DigitalProduct target,
			Legacy.Common.Entities.Digital.DigitalProduct source)
		{
			target.Index = source.Index;
			target.Category = source.Category;
			target.SubCategory = source.SubCategory;
			target.RateType = source.RateType;
			target.Location = source.Location;
			target.Width = source.Width;
			target.Height = source.Height;
			target.EnableLocation = source.EnableLocation;
			target.EnableTarget = source.EnableTarget;
			target.EnableRichMedia = source.EnableRichMedia;
			target.EnableRate = source.EnableRate;
			target.UserDefinedName = source.UserDefinedName;
			target.Websites.AddRange(source.Websites);
			target.DurationType = source.DurationType;
			target.DurationValue = source.DurationValue;
			target.Strength1 = source.Strength1;
			target.Strength2 = source.Strength2;
			target.MonthlyInvestment = source.MonthlyInvestment;
			target.MonthlyImpressions = source.MonthlyImpressions;
			target.MonthlyCPM = source.MonthlyCPM;
			target.TotalInvestment = source.TotalInvestment;
			target.TotalImpressions = source.TotalImpressions;
			target.TotalCPM = source.TotalCPM;
			target.DefaultRate = source.DefaultRate;
			target.Formula = (Business.Online.Enums.FormulaType)(Int32)source.Formula;
			target.InvestmentDetails = source.InvestmentDetails;

			target.DescriptionManualEdit = source.DescriptionManualEdit;
			target.ShowDimensions = source.ShowDimensions;
			target.ShowDescriptionTargeting = source.ShowDescriptionTargeting;
			target.ShowDescriptionRichMedia = source.ShowDescriptionRichMedia;
			target.Description = source.Description;

			target.EnableComment = source.EnableComment;
			target.CommentManualEdit = source.CommentManualEdit;
			target.ShowCommentTargeting = source.ShowCommentTargeting;
			target.ShowCommentRichMedia = source.ShowCommentRichMedia;

			target.ShowDuration = source.ShowDuration;
			target.ShowAllPricingMonthly = source.ShowAllPricingMonthly;
			target.ShowAllPricingTotal = source.ShowAllPricingTotal;
			target.ShowMonthlyInvestments = source.ShowMonthlyInvestments;
			target.ShowMonthlyImpressions = source.ShowMonthlyImpressions;
			target.ShowTotalInvestments = source.ShowTotalInvestments;
			target.ShowTotalImpressions = source.ShowTotalImpressions;
			target.ShowFlightDates = source.ShowFlightDates;

			target.UserDescription = source.UserDescription;
			target.Comment = source.Comment;

			foreach (var oldProductInfo in source.AddtionalInfo)
			{
				var productInfo = new Business.Online.Entities.NonPersistent.ProductInfo();
				productInfo.ImportData(oldProductInfo);
				target.AddtionalInfo.Add(productInfo);
			}

			target.PackageRecord.ImportData(source.PackageRecord);
		}

		private static void ImportData(
			this Business.Online.Entities.NonPersistent.ProductInfo target,
			Legacy.Common.Entities.Digital.ProductInfo source)
		{
			target.Type = (Business.Online.Enums.ProductInfoType)(Int32)source.Type;
			target.Selected = source.Selected;
			target.Group = source.Group;
			target.Phrases.AddRange(source.Phrases);
			target.UserValue = source.UserValue;
		}

		private static void ImportData(
			this Business.Online.Entities.NonPersistent.ProductPackageRecord target,
			Legacy.Common.Entities.Digital.ProductPackageRecord source)
		{
			target.Category = source.Category;
			target.SubCategory = source.SubCategory;
			target.Name = source.Name;
			target.Info = source.Info;
			//target.Comments = source.Comments;
			target.UseFormula = source.UseFormula;
			target.Rate = source.Rate;
			target.Impressions = source.Impressions;
			target.Investment = source.Investment;
			target.CPM = source.CPM;
		}

		private static void ImportData(
			this Business.Online.Entities.NonPersistent.DigitalProductSummary target,
			Legacy.Common.Entities.Digital.DigitalProductSummary source)
		{
			target.Statement = source.Statement;
			target.MonthlyInvestment = source.MonthlyInvestment;
			target.TotalInvestment = source.TotalInvestment;
		}
	}
}
