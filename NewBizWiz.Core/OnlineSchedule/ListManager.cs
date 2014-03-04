using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using NewBizWiz.Core.Common;

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
			Statuses = new List<string>();
			PricingStrategies = new List<string>();
			ColumnPositions = new List<string>();
			SpecialLinksGroupName = String.Empty;
			SpecialLinkButtons = new List<SpecialLinkButton>();

			string imageFolderPath = String.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Artwork\DIGITAL\", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			string folderPath = Path.Combine(imageFolderPath, "Big Logos");
			if (Directory.Exists(folderPath))
				BigImageFolder = new DirectoryInfo(folderPath);

			folderPath = Path.Combine(imageFolderPath, "Small Logos");
			if (Directory.Exists(folderPath))
				SmallImageFolder = new DirectoryInfo(folderPath);

			folderPath = Path.Combine(imageFolderPath, "Tiny Logos");
			if (Directory.Exists(folderPath))
				TinyImageFolder = new DirectoryInfo(folderPath);

			folderPath = Path.Combine(imageFolderPath, "Xtra Tiny Logos");
			if (Directory.Exists(folderPath))
				XtraTinyImageFolder = new DirectoryInfo(folderPath);
			Images = new List<ImageSource>();

			LoadImages();
			LoadLists();
		}

		public DirectoryInfo BigImageFolder { get; set; }
		public DirectoryInfo SmallImageFolder { get; set; }
		public DirectoryInfo TinyImageFolder { get; set; }
		public DirectoryInfo XtraTinyImageFolder { get; set; }
		public List<ImageSource> Images { get; set; }

		public List<string> SlideHeaders { get; set; }
		public List<string> Websites { get; set; }
		public List<string> Strengths { get; set; }
		public List<string> PricingStrategies { get; set; }
		public List<string> ColumnPositions { get; set; }
		public List<Category> Categories { get; set; }
		public List<ProductSource> ProductSources { get; set; }
		public List<string> Statuses { get; set; }
		public FormulaType DefaultFormula { get; set; }
		public bool LockedMode { get; set; }
		public string SpecialLinksGroupName { get; set; }
		public List<SpecialLinkButton> SpecialLinkButtons { get; set; }

		public static ListManager Instance
		{
			get { return _instance; }
		}

		private void LoadImages()
		{
			Images.Clear();
			if (BigImageFolder == null || SmallImageFolder == null || TinyImageFolder == null || XtraTinyImageFolder == null) return;
			foreach (var bigImageFile in BigImageFolder.GetFiles("*.png"))
			{
				string imageFileName = Path.GetFileNameWithoutExtension(bigImageFile.FullName);
				string imageFileExtension = Path.GetExtension(bigImageFile.FullName);

				string smallImageFilePath = Path.Combine(SmallImageFolder.FullName, string.Format("{0}2{1}", new[] { imageFileName, imageFileExtension }));
				string tinyImageFilePath = Path.Combine(TinyImageFolder.FullName, string.Format("{0}3{1}", new[] { imageFileName, imageFileExtension }));
				string xtraTinyImageFilePath = Path.Combine(XtraTinyImageFolder.FullName, string.Format("{0}4{1}", new[] { imageFileName, imageFileExtension }));
				if (File.Exists(smallImageFilePath) && File.Exists(tinyImageFilePath) && File.Exists(xtraTinyImageFilePath))
				{
					var imageSource = new ImageSource();
					imageSource.IsDefault = bigImageFile.Name.ToLower().Contains("default");
					imageSource.BigImage = new Bitmap(bigImageFile.FullName);
					imageSource.SmallImage = new Bitmap(smallImageFilePath);
					imageSource.TinyImage = new Bitmap(tinyImageFilePath);
					imageSource.XtraTinyImage = new Bitmap(xtraTinyImageFilePath);
					Images.Add(imageSource);
				}
			}
		}

		private void LoadOnlineStrategy()
		{
			SlideHeaders.Clear();
			Websites.Clear();
			Strengths.Clear();
			Categories.Clear();
			ProductSources.Clear();
			var listPath = Path.Combine(Common.SettingsManager.Instance.SharedListFolder, OnlineStrategyFileName);
			if (!File.Exists(listPath)) return;
			var document = new XmlDocument();
			document.Load(listPath);

			var node = document.SelectSingleNode(@"/OnlineStrategy");
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
						case "SpecialButtonsGroupName":
							SpecialLinksGroupName = childeNode.InnerText;
							break;
						case "SpecialButton":
							var specialLinkButton = new SpecialLinkButton();
							GetSpecialButton(childeNode, ref specialLinkButton);
							if (!String.IsNullOrEmpty(specialLinkButton.Name) && !String.IsNullOrEmpty(specialLinkButton.Type) && specialLinkButton.Paths.Any())
								SpecialLinkButtons.Add(specialLinkButton);
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
					}
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

		private void LoadLists()
		{
			LoadOnlineStrategy();
		}
	}
}