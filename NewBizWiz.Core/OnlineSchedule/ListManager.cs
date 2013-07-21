using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;

namespace NewBizWiz.Core.OnlineSchedule
{
	public class ListManager
	{
		private const string OnlineStrategyFileName = @"Online XML\Online Strategy.xml";

		private static readonly ListManager _instance = new ListManager();

		private ListManager()
		{
			SlideHeaders = new List<string>();
			Websites = new List<string>();
			Strengths = new List<string>();
			Categories = new List<Category>();
			ProductSources = new List<ProductSource>();
			SlideSources = new List<SlideSource>();
			Statuses = new List<string>();
			LoadLists();
		}

		public List<string> SlideHeaders { get; set; }
		public List<string> Websites { get; set; }
		public List<string> Strengths { get; set; }
		public List<Category> Categories { get; set; }
		public List<ProductSource> ProductSources { get; set; }
		public List<SlideSource> SlideSources { get; set; }
		public List<string> Statuses { get; set; }
		public FormulaType DefaultFormula { get; set; }

		public static ListManager Instance
		{
			get { return _instance; }
		}

		private void LoadOnlineStrategy()
		{
			ProductSource productSource = null;
			SlideHeaders.Clear();
			Websites.Clear();
			Strengths.Clear();
			Categories.Clear();
			ProductSources.Clear();
			SlideSources.Clear();
			string listPath = Path.Combine(Common.SettingsManager.Instance.SharedListFolder, OnlineStrategyFileName);
			if (File.Exists(listPath))
			{
				var document = new XmlDocument();
				document.Load(listPath);

				XmlNode node = document.SelectSingleNode(@"/OnlineStrategy");
				if (node != null)
				{
					foreach (XmlNode childeNode in node.ChildNodes)
					{
						switch (childeNode.Name)
						{
							case "Header":
								foreach (XmlAttribute attribute in childeNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Value":
											if (!string.IsNullOrEmpty(attribute.Value) && !SlideHeaders.Contains(attribute.Value))
												SlideHeaders.Add(attribute.Value);
											break;
									}
								}
								break;
							case "Site":
								foreach (XmlAttribute attribute in childeNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Value":
											if (!string.IsNullOrEmpty(attribute.Value) && !Websites.Contains(attribute.Value))
												Websites.Add(attribute.Value);
											break;
									}
								}
								break;
							case "Strength":
								foreach (XmlAttribute attribute in childeNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Value":
											if (!string.IsNullOrEmpty(attribute.Value) && !Strengths.Contains(attribute.Value))
												Strengths.Add(attribute.Value);
											break;
									}
								}
								break;
							case "DefaultFormula":
								switch (childeNode.InnerText.ToLower().Trim())
								{
									case "cpm":
										DefaultFormula = FormulaType.CPM;
										break;
									case "investment":
										DefaultFormula = FormulaType.Investment;
										break;
									case "impressions":
										DefaultFormula = FormulaType.Impressions;
										break;
								}
								break;
							case "Category":
								var category = new Category();
								GetCategories(childeNode, ref category);
								if (!string.IsNullOrEmpty(category.Name))
									Categories.Add(category);
								break;
							case "Product":
								productSource = new ProductSource();
								GetProductProperties(childeNode, ref productSource);
								if (!string.IsNullOrEmpty(productSource.Name))
									ProductSources.Add(productSource);
								break;
							case "SlideSource":
								var slideSource = new SlideSource();
								GetSlideSourceProperties(childeNode, ref slideSource);
								if (!string.IsNullOrEmpty(slideSource.TemplateName))
									SlideSources.Add(slideSource);
								break;
							case "Status":
								foreach (XmlAttribute attribute in childeNode.Attributes)
									switch (attribute.Name)
									{
										case "Value":
											if (!Statuses.Contains(attribute.Value))
												Statuses.Add(attribute.Value);
											break;
									}
								break;
						}
					}
				}
			}
		}

		private void GetProductProperties(XmlNode node, ref ProductSource productSource)
		{
			int tempInt = 0;
			double tempDouble = 0;

			foreach (XmlAttribute attribute in node.Attributes)
			{
				switch (attribute.Name)
				{
					case "Name":
						productSource.Name = attribute.Value;
						break;
					case "Category":
						productSource.Category = Categories.Where(x => x.Name.Equals(attribute.Value)).FirstOrDefault();
						break;
					case "SubCategory":
						productSource.SubCategory = attribute.Value;
						break;
					case "RateType":
						switch (attribute.Value)
						{
							case "CPM":
								productSource.RateType = RateType.CPM;
								break;
							case "Fixed":
								productSource.RateType = RateType.Fixed;
								break;
						}
						break;
					case "Rate":
						if (double.TryParse(attribute.Value, out tempDouble))
							productSource.Rate = tempDouble;
						else
							productSource.Rate = null;
						break;
					case "Overview":
						productSource.Overview = attribute.Value;
						break;
					case "Width":
						if (int.TryParse(attribute.Value, out tempInt))
							productSource.Width = tempInt;
						else
							productSource.Width = null;
						break;
					case "Height":
						if (int.TryParse(attribute.Value, out tempInt))
							productSource.Height = tempInt;
						else
							productSource.Height = null;
						break;
				}
			}
		}

		private void GetCategories(XmlNode node, ref Category category)
		{
			foreach (XmlAttribute attribute in node.Attributes)
			{
				switch (attribute.Name)
				{
					case "Name":
						category.Name = attribute.Value;
						break;
					case "Logo":
						if (string.IsNullOrEmpty(attribute.Value))
							category.Logo = null;
						else
							category.Logo = new Bitmap(new MemoryStream(Convert.FromBase64String(attribute.Value)));
						break;
					case "TooltipTitle":
						category.TooltipTitle = attribute.Value;
						break;
					case "TooltipValue":
						category.TooltipValue = attribute.Value;
						break;
				}
			}
		}

		private void GetSlideSourceProperties(XmlNode node, ref SlideSource slideSource)
		{
			bool tempBool = false;

			foreach (XmlAttribute attribute in node.Attributes)
			{
				switch (attribute.Name)
				{
					case "Name":
						slideSource.TemplateName = attribute.Value;
						break;
					case "ShowActiveDays":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowActiveDays = tempBool;
						break;
					case "ShowAdRate":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowAdRate = tempBool;
						break;
					case "ShowBusinessName":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowBusinessName = tempBool;
						break;
					case "ShowComments":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowComments = tempBool;
						break;
					case "ShowDecisionMaker":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowDecisionMaker = tempBool;
						break;
					case "ShowDescription":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowDescription = tempBool;
						break;
					case "ShowDimensions":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowDimensions = tempBool;
						break;
					case "ShowDuration":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowDuration = tempBool;
						break;
					case "ShowFlightDates":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowFlightDates = tempBool;
						break;
					case "ShowImages":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowImages = tempBool;
						break;
					case "ShowMonthlyCPM":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowMonthlyCPM = tempBool;
						break;
					case "ShowMonthlyImpressions":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowMonthlyImpressions = tempBool;
						break;
					case "ShowMonthlyInvestment":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowMonthlyInvestment = tempBool;
						break;
					case "ShowPresentationDate":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowPresentationDate = tempBool;
						break;
					case "ShowProduct":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowProduct = tempBool;
						break;
					case "ShowScreenshot":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowScreenshot = tempBool;
						break;
					case "ShowSignature":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowSignature = tempBool;
						break;
					case "ShowTotalAds":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowTotalAds = tempBool;
						break;
					case "ShowTotalCPM":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowTotalCPM = tempBool;
						break;
					case "ShowTotalImpressions":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowTotalImpressions = tempBool;
						break;
					case "ShowTotalInvestment":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowTotalInvestment = tempBool;
						break;
					case "ShowWebsite":
						tempBool = false;
						bool.TryParse(attribute.Value, out tempBool);
						slideSource.ShowWebsite = tempBool;
						break;
				}
			}
		}

		private void LoadLists()
		{
			LoadOnlineStrategy();
		}
	}
}