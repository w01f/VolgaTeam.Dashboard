using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Core.MediaSchedule
{
	public class ProgramStrategy
	{
		private readonly Schedule _parent;

		public bool ShowFavorites { get; set; }

		public bool ShowStation { get; set; }
		public bool ShowDescription { get; set; }

		public List<ProgramStrategyItem> Items { get; private set; }

		public IEnumerable<ProgramStrategyItem> EnabledItems
		{
			get { return Items.Where(i => i.Enabled).OrderBy(i => i.Order); }
		}

		public ProgramStrategy(Schedule parent)
		{
			_parent = parent;
			Items = new List<ProgramStrategyItem>();

			ShowFavorites = true;

			ShowStation = true;
			ShowDescription = true;
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<ShowFavorites>" + ShowFavorites + @"</ShowFavorites>");

			result.AppendLine(@"<ShowStation>" + ShowStation + @"</ShowStation>");
			result.AppendLine(@"<ShowDescription>" + ShowDescription + @"</ShowDescription>");

			foreach (var strategyItem in Items)
				result.AppendLine(@"<Item>" + strategyItem.Serialize() + @"</Item>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "ShowFavorites":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowFavorites = temp;
							break;
						}
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
				}
			}
			UpdateItems();
		}

		private void UpdateItems()
		{
			var sourceCollection = _parent.Section.Programs;
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
		private readonly ProgramStrategy _parent;

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

		public ProgramStrategyItem(ProgramStrategy programStrategy)
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