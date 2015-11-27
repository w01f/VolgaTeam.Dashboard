using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Xml;
using Asa.Core.Common;

namespace Asa.Core.MediaSchedule
{
	public enum SectionSummaryTypeEnum
	{
		None,
		Product,
		Strategy,
		Custom
	}

	public class SectionSummary
	{
		public ScheduleSection Parent { get; private set; }
		public SectionSummaryTypeEnum SummaryType { get; private set; }
		public ISectionSummaryContent Content { get; private set; }

		public SectionSummary(ScheduleSection parent)
		{
			Parent = parent;
			SummaryType = SectionSummaryTypeEnum.Product;
			Content = CreateContentBySummaryType();
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<SummaryType>" + SummaryType + @"</SummaryType>");
			result.AppendLine(@"<Content>" + Content.Serialize() + @"</Content>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "SummaryType":
						{
							SectionSummaryTypeEnum temp;
							if (Enum.TryParse(childNode.InnerText, out temp))
								SummaryType = temp;
						}
						break;
				}

			Content = CreateContentBySummaryType();
			var contentNode = node.SelectSingleNode("Content");
			if (contentNode != null)
				Content.Deserialize(contentNode);
			Content.SynchronizeSectionContent();
		}

		public void ChangeSummaryType(SectionSummaryTypeEnum newType)
		{
			if (newType == SummaryType) return;
			SummaryType = newType;
			Content = CreateContentBySummaryType();
			Content.SynchronizeSectionContent();
		}

		private ISectionSummaryContent CreateContentBySummaryType()
		{
			switch (SummaryType)
			{
				case SectionSummaryTypeEnum.Product:
					return new ProductSummaryContent(this);
				case SectionSummaryTypeEnum.Custom:
					{
						var content = new CustomSummaryContent(this);
						return content;
					}
				case SectionSummaryTypeEnum.Strategy:
					{
						var content = new StrategySummaryContent(this);
						return content;
					}
			}
			throw new ArgumentOutOfRangeException("Summary Type is undefined");
		}

	}

	public interface ISectionSummaryContent
	{
		SectionSummary Parent { get; }
		string Serialize();
		void Deserialize(XmlNode node);
		void SynchronizeSectionContent();
	}

	public class ProductSummaryContent : BaseSummarySettings, ISectionSummaryContent
	{
		public SectionSummary Parent { get; private set; }
		public List<ISummaryProduct> Items
		{
			get
			{
				var result = new List<ISummaryProduct>();
				result.AddRange(Parent.Parent.Programs);
				result.AddRange(Parent.Parent.ParentSchedule.DigitalProducts);
				return result;
			}
		}

		public ProductSummaryContent(SectionSummary parent)
		{
			Parent = parent;
		}

		public void SynchronizeSectionContent()
		{
		}
	}

	public class CustomSummaryContent : CustomSummarySettings, ISectionSummaryContent
	{
		public SectionSummary Parent { get; private set; }
		public bool IsDefaultSate { get; set; }

		public CustomSummaryContent(SectionSummary parent)
		{
			Parent = parent;
			IsDefaultSate = true;
		}

		public override string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(base.Serialize());
			result.AppendLine(@"<IsDefaultSate>" + IsDefaultSate + @"</IsDefaultSate>");
			return result.ToString();
		}

		public override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "IsDefaultSate":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								IsDefaultSate = temp;
						}
						break;
				}
			}
		}

		public void SynchronizeSectionContent()
		{
			if (!IsDefaultSate) return;
			if (Items.Count != 2) return;
			{
				var mediaSummaryItem = Items[0];
				mediaSummaryItem.ShowValue = true;
				mediaSummaryItem.Value = String.Format("Local {0} Campaign", MediaMetaData.Instance.DataTypeString);
				var description = new List<string>();
				var programs = Parent.Parent.Programs.ToList();
				description.Add(String.Format("Stations: {0}", String.Join(", ", programs.Select(p => p.Station).Distinct())));
				description.Add(String.Format("Dayparts: {0}", String.Join(", ", programs.Select(p => p.Daypart).Distinct())));
				description.Add(String.Format("Total Spots: {0}x", programs.Sum(p => p.Spots.Sum(sp => sp.Count))));
				if (programs.Any(p => p.Rate.HasValue))
					description.Add(String.Format("Avg Rate: {0}", programs.Where(p => p.Rate.HasValue).Average(p => p.Rate.Value).ToString("$#,##0")));
				mediaSummaryItem.Description = String.Join("  ", description);
				mediaSummaryItem.ShowDescription = true;
				mediaSummaryItem.ShowMonthly = false;
				mediaSummaryItem.Monthly = null;
				mediaSummaryItem.ShowTotal = false;
				mediaSummaryItem.Total = null;
			}
			{
				var digitalSummaryItem = Items[1];
				digitalSummaryItem.ShowValue = true;
				digitalSummaryItem.Value = "Digital Campaign";
				digitalSummaryItem.Description = String.Join(", ", Parent.Parent.ParentSchedule.DigitalProducts.Select(dp =>
					String.Format("({0}){1} - {2}",
					dp.Category,
					!String.IsNullOrEmpty(dp.SubCategory) ? (String.Format(" {0}", dp.SubCategory)) : String.Empty,
					dp.Name)));
				digitalSummaryItem.ShowDescription = true;
				digitalSummaryItem.ShowMonthly = false;
				digitalSummaryItem.Monthly = null;
				digitalSummaryItem.ShowTotal = false;
				digitalSummaryItem.Total = null;
			}
		}
	}

	public class StrategySummaryContent : ISectionSummaryContent
	{
		public SectionSummary Parent { get; private set; }
		public bool ShowStation { get; set; }
		public bool ShowDescription { get; set; }

		public List<ProgramStrategyItem> Items { get; private set; }

		public ContractSettings ContractSettings { get; private set; }

		public IEnumerable<ProgramStrategyItem> EnabledItems
		{
			get { return Items.Where(i => i.Enabled).OrderBy(i => i.Order); }
		}

		public StrategySummaryContent(SectionSummary parent)
		{
			Parent = parent;
			Items = new List<ProgramStrategyItem>();
			ContractSettings = new ContractSettings();

			ShowStation = true;
			ShowDescription = true;
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<ShowStation>" + ShowStation + @"</ShowStation>");
			result.AppendLine(@"<ShowDescription>" + ShowDescription + @"</ShowDescription>");

			foreach (var strategyItem in Items)
				result.AppendLine(@"<Item>" + strategyItem.Serialize() + @"</Item>");

			if (ContractSettings.IsConfigured)
				result.AppendLine(String.Format("<ContractSettings>{0}</ContractSettings>", ContractSettings.Serialize()));

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "ShowStation":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowStation = temp;
							break;
						}
					case "ShowDescription":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowDescription = temp;
							break;
						}
					case "Item":
						{
							var item = new ProgramStrategyItem(this);
							item.Deserialize(childNode);
							Items.Add(item);
							break;
						}
					case "ContractSettings":
						ContractSettings.Deserialize(childNode);
						break;
				}
			}
		}

		public void SynchronizeSectionContent()
		{
			var sourceCollection = Parent.Parent.Programs.ToList();
			var maxOrder = Items.Any() ? Items.Max(i => i.Order) : 0;
			var groupedPrograms = sourceCollection.GroupBy(p => p.Name, (key, g) => new { Name = key, Station = String.Join(", ", g.Select(i => i.Station)), Spots = g.SelectMany(i => i.Spots).Sum(s => s.Count) });
			foreach (var program in groupedPrograms)
			{
				var strategyItem = Items.FirstOrDefault(si => si.Name == program.Name);
				if (strategyItem == null)
				{
					strategyItem = new ProgramStrategyItem(this)
					{
						Enabled = true,
						Name = program.Name,
						Order = maxOrder
					};
					Items.Add(strategyItem);
					maxOrder++;
				}
				strategyItem.Station = program.Station;
				strategyItem.Description = String.Format("{0}x", program.Spots);
			}
			Items.RemoveAll(i => !groupedPrograms.Any(gp => gp.Name == i.Name));
			ReorderItems();
		}

		private void ReorderItems()
		{
			var index = 0;
			foreach (var item in Items.OrderBy(i => i.Order).ToList())
			{
				item.Order = index;
				index++;
			}
			Items.Sort((x, y) => x.Order.CompareTo(y.Order));
		}

		public void ChangeItemsOrder(int sourceRowOrder, int targetRowOrder)
		{
			if (!(sourceRowOrder >= 0 && sourceRowOrder < Items.Count)) return;
			var item = Items[sourceRowOrder];
			item.Order = targetRowOrder - (Decimal)0.5;
			ReorderItems();
		}
	}

	public class ProgramStrategyItem
	{
		private readonly StrategySummaryContent _parent;

		public bool Enabled { get; set; }
		public string Name { get; set; }
		public string Station { get; set; }
		public string Description { get; set; }
		public decimal Order { get; set; }

		private ImageSource _logo;
		public ImageSource Logo
		{
			get
			{
				if (!Enabled)
					return DisabledLogo;
				return _logo.ContainsData ? _logo : MediaMetaData.Instance.ListManager.DefaultStrategyLogo;
			}
			set
			{
				if (!Enabled) return;
				_logo = value;
				_disabledLogo = null;
			}
		}

		private ImageSource _disabledLogo;

		public ProgramStrategyItem(StrategySummaryContent programStrategy)
		{
			_parent = programStrategy;
			_logo = new ImageSource();
		}

		private ImageSource DisabledLogo
		{
			get
			{
				if (_disabledLogo != null) return _disabledLogo;

				var sourceLogo = (_logo.BigImage ?? MediaMetaData.Instance.ListManager.DefaultStrategyLogo.BigImage).Clone() as Image;
				if (sourceLogo == null) return null;
				var disabledImage = new Bitmap(sourceLogo);
				using (var gr = Graphics.FromImage(disabledImage))
				using (var attributes = new ImageAttributes())
				{
					var matrix = new ColorMatrix();
					matrix.Matrix33 = 0.4f;
					attributes.SetColorMatrix(matrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
					gr.Clear(Color.FromKnownColor(KnownColor.ButtonFace));
					gr.DrawImage(sourceLogo, new Rectangle(0, 0, disabledImage.Width, disabledImage.Height), 0, 0, disabledImage.Width, disabledImage.Height, GraphicsUnit.Pixel, attributes);
					_disabledLogo = ImageSource.FromImage(disabledImage);
					return _disabledLogo;
				}
			}
		}

		public bool IsDefaultLogo
		{
			get { return _logo == null; }
		}

		public string CompiledName
		{
			get { return _parent.ShowStation ? String.Format("{0}{2}({1})", Name, Station, Environment.NewLine) : Name; }
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<Enabled>" + Enabled + @"</Enabled>");
			if (!String.IsNullOrEmpty(Name))
				result.AppendLine(@"<Name>" + Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Name>");
			if (!String.IsNullOrEmpty(Station))
				result.AppendLine(@"<Station>" + Station.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Station>");
			if (!String.IsNullOrEmpty(Description))
				result.AppendLine(@"<Description>" + Description.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Description>");
			result.AppendLine(@"<Order>" + Order + @"</Order>");
			if (_logo.ContainsData)
				result.AppendLine(@"<Logo>" + _logo.Serialize() + @"</Logo>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Enabled":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								Enabled = temp;
							break;
						}
					case "Name":
						Name = childNode.InnerText;
						break;
					case "Station":
						Station = childNode.InnerText;
						break;
					case "Description":
						Description = childNode.InnerText;
						break;
					case "Order":
						{
							decimal temp;
							if (Decimal.TryParse(childNode.InnerText, out temp))
								Order = temp;
							break;
						}
					case "Logo":
						_logo.Deserialize(childNode);
						break;
				}
			}
		}
	}

}
