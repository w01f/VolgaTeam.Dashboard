using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Asa.Core.Common;

namespace Asa.Core.OnlineSchedule
{
	public class ListManager
	{
		private static readonly ListManager _instance = new ListManager();

		private ListManager()
		{
			SlideHeaders = new List<string>();
			Websites = new List<string>();
			Strengths = new List<string>();
			Categories = new List<Category>();
			ProductSources = new List<ProductSource>();
			Statuses = new List<string>();
			PricingStrategies = new List<string>();
			ColumnPositions = new List<string>();
			Placeholders = new List<string>();
			SpecialLinksGroupName = String.Empty;
			SpecialLinkButtons = new List<SpecialLinkButton>();
			SpecialLinkBrowsers = new List<string>();
			TargetingRecods = new List<ProductInfo>();
			RichMediaRecods = new List<ProductInfo>();
			DefaultHomeViewSettings = new HomeViewSettings();
			DefaultDigitalProductSettings = new DigitalProductSettings();
			DefaultDigitalPackageSettings = new DigitalPackageSettings();

			Images = new List<ImageSourceGroup>();
		}

		public List<ImageSourceGroup> Images { get; set; }

		public List<string> SlideHeaders { get; private set; }
		public List<string> Websites { get; private set; }
		public List<string> Strengths { get; private set; }
		public List<string> PricingStrategies { get; private set; }
		public List<string> ColumnPositions { get; private set; }
		public List<string> Placeholders { get; private set; }
		public List<Category> Categories { get; private set; }
		public List<ProductSource> ProductSources { get; private set; }
		public List<string> Statuses { get; private set; }
		public FormulaType DefaultFormula { get; set; }
		public bool LockedMode { get; set; }
		public bool SpecialLinksEnable { get; set; }
		public string SpecialLinksGroupName { get; set; }
		public Image SpecialLinksGroupLogo { get; set; }
		public List<string> SpecialLinkBrowsers { get; private set; }
		public List<SpecialLinkButton> SpecialLinkButtons { get; private set; }
		public List<ProductInfo> TargetingRecods { get; set; }
		public List<ProductInfo> RichMediaRecods { get; set; }

		public HomeViewSettings DefaultHomeViewSettings { get; private set; }
		public DigitalProductSettings DefaultDigitalProductSettings { get; private set; }
		public DigitalPackageSettings DefaultDigitalPackageSettings { get; private set; }

		public static ListManager Instance
		{
			get { return _instance; }
		}

		public void Load(StorageFile listsSourceFile)
		{
			LoadOnlineStrategy(listsSourceFile);
			LoadImages();
		}

		private void LoadImages()
		{
			var imageFolder = new StorageDirectory(Common.ResourceManager.Instance.ArtworkFolder.RelativePathParts.Merge("DIGITAL"));
			Images.Clear();
			var defaultGroup = new ImageSourceGroup(imageFolder) { Name = "Gallery", Order = -1 };
			defaultGroup.LoadImages();
			if (defaultGroup.Images.Any())
				Images.Add(defaultGroup);
		}

		private void LoadOnlineStrategy(StorageFile listsSourceFile)
		{
			SlideHeaders.Clear();
			Websites.Clear();
			Strengths.Clear();
			Categories.Clear();
			ProductSources.Clear();
			if (!listsSourceFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(listsSourceFile.LocalPath);

			var node = document.SelectSingleNode(@"/OnlineStrategy");
			if (node == null) return;
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
					case "PricingStrategy":
						foreach (XmlAttribute attribute in childeNode.Attributes)
						{
							switch (attribute.Name)
							{
								case "Value":
									if (!string.IsNullOrEmpty(attribute.Value) && !PricingStrategies.Contains(attribute.Value))
										PricingStrategies.Add(attribute.Value);
									break;
							}
						}
						break;
					case "PositionColumn":
						foreach (XmlAttribute attribute in childeNode.Attributes)
						{
							switch (attribute.Name)
							{
								case "Value":
									if (!string.IsNullOrEmpty(attribute.Value) && !ColumnPositions.Contains(attribute.Value))
										ColumnPositions.Add(attribute.Value);
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
					case "LockedMode":
						{
							bool temp;
							if (Boolean.TryParse(childeNode.InnerText, out temp))
								LockedMode = temp;
						}
						break;
					case "SpecialLinksEnable":
						{
							bool temp;
							if (Boolean.TryParse(childeNode.InnerText, out temp))
								SpecialLinksEnable = temp;
						}
						break;
					case "SpecialButtonsGroupName":
						SpecialLinksGroupName = childeNode.InnerText;
						break;
					case "SpecialButtonsGroupLogo":
						if (string.IsNullOrEmpty(childeNode.InnerText))
							SpecialLinksGroupLogo = null;
						else
							SpecialLinksGroupLogo = new Bitmap(new MemoryStream(Convert.FromBase64String(childeNode.InnerText)));
						break;
					case "Browser":
						SpecialLinkBrowsers.Add(childeNode.InnerText);
						break;
					case "SpecialButton":
						var specialLinkButton = new SpecialLinkButton();
						GetSpecialButton(childeNode, ref specialLinkButton);
						if (!String.IsNullOrEmpty(specialLinkButton.Name) && !String.IsNullOrEmpty(specialLinkButton.Type) && specialLinkButton.Paths.Any())
							SpecialLinkButtons.Add(specialLinkButton);
						break;
					case "Placeholder":
						Placeholders.Add(childeNode.InnerText);
						break;
					case "Targeting":
						{
							var productInfo = new ProductInfo { Type = ProductInfoType.Targeting };
							productInfo.Deserialize(childeNode);
							TargetingRecods.Add(productInfo);
						}
						break;
					case "RichMedia":
						{
							var productInfo = new ProductInfo { Type = ProductInfoType.RichMedia };
							productInfo.Deserialize(childeNode);
							RichMediaRecods.Add(productInfo);
						}
						break;
					case "Category":
						var category = new Category();
						GetCategories(childeNode, ref category);
						if (!string.IsNullOrEmpty(category.Name))
							Categories.Add(category);
						break;
					case "Product":
						var productSource = new ProductSource();
						GetProductProperties(childeNode, ref productSource);
						if (!string.IsNullOrEmpty(productSource.Name))
							ProductSources.Add(productSource);
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
					case "DefaultHomeViewSettings":
						DefaultHomeViewSettings.Deserialize(childeNode);
						break;
					case "DefaultDigitalProductSettings":
						DefaultDigitalProductSettings.Deserialize(childeNode);
						break;
					case "DefaultDigitalPackageSettings":
						DefaultDigitalPackageSettings.Deserialize(childeNode);
						break;
				}
			}
		}

		private void GetProductProperties(XmlNode node, ref ProductSource productSource)
		{
			foreach (XmlAttribute attribute in node.Attributes)
			{
				int tempInt;
				switch (attribute.Name)
				{
					case "Name":
						productSource.Name = attribute.Value;
						break;
					case "Category":
						productSource.Category = Categories.FirstOrDefault(x => x.Name.Equals(attribute.Value));
						break;
					case "SubCategory":
						productSource.SubCategory = attribute.Value;
						break;
					case "RateType":
						productSource.RateType = attribute.Value;
						break;
					case "Rate":
						decimal tempDecimal;
						if (Decimal.TryParse(attribute.Value, out tempDecimal))
							productSource.Rate = tempDecimal;
						else
							productSource.Rate = null;
						break;
					case "Overview":
						productSource.Overview = attribute.Value;
						break;
					case "DefaultWebsite":
						productSource.DefaultWebsite = attribute.Value;
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
					case "EnableTarget":
						{
							bool temp;
							if (Boolean.TryParse(attribute.Value, out temp))
								productSource.EnableTarget = temp;
						}
						break;
					case "EnableLocation":
						{
							bool temp;
							if (Boolean.TryParse(attribute.Value, out temp))
								productSource.EnableLocation = temp;
						}
						break;
					case "EnableRichMedia":
						{
							bool temp;
							if (Boolean.TryParse(attribute.Value, out temp))
								productSource.EnableRichMedia = temp;
						}
						break;
					case "EnableRate":
						{
							bool temp;
							if (Boolean.TryParse(attribute.Value, out temp))
								productSource.EnableRate = temp;
						}
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

		private void GetSpecialButton(XmlNode node, ref SpecialLinkButton specialLinkButton)
		{
			foreach (XmlAttribute attribute in node.Attributes)
			{
				switch (attribute.Name)
				{
					case "Name":
						specialLinkButton.Name = attribute.Value;
						break;
					case "Type":
						specialLinkButton.Type = attribute.Value.ToUpper();
						break;
					case "Tooltip":
						specialLinkButton.Tooltip = attribute.Value;
						break;
					case "Image":
						if (string.IsNullOrEmpty(attribute.Value))
							specialLinkButton.Logo = null;
						else
							specialLinkButton.Logo = new Bitmap(new MemoryStream(Convert.FromBase64String(attribute.Value)));
						break;
				}
			}
			foreach (var childNode in node.ChildNodes.OfType<XmlNode>())
			{
				switch (childNode.Name)
				{
					case "Path":
						specialLinkButton.Paths.Add(childNode.InnerText);
						break;
				}
			}
		}
	}
}