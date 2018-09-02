using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Agenda
{
	public class AgendaTabEInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public Image Clipart2Image { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public Image Clipart3Image { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public string Tab1Title { get; private set; }
		public string Tab2Title { get; private set; }
		public string Tab3Title { get; private set; }

		public List<ListDataItem> MemoPopup1Items { get; }
		public TextEditorConfiguration MemoPopup1Configuration { get; set; }

		public List<ListDataItem> MemoPopup2Items { get; }
		public TextEditorConfiguration MemoPopup2Configuration { get; set; }

		public List<ListDataItem> MemoPopup3Items { get; }
		public TextEditorConfiguration MemoPopup3Configuration { get; set; }

		public List<ListDataItem> MemoPopup4Items { get; }
		public TextEditorConfiguration MemoPopup4Configuration { get; set; }

		public List<ListDataItem> MemoPopup5Items { get; }
		public TextEditorConfiguration MemoPopup5Configuration { get; set; }

		public List<ListDataItem> MemoPopup6Items { get; }
		public TextEditorConfiguration MemoPopup6Configuration { get; set; }

		public List<ListDataItem> Combo1Items { get; }
		public List<ListDataItem> Combo2Items { get; }
		public List<ListDataItem> Combo3Items { get; }
		public List<ListDataItem> Combo4Items { get; }
		public List<ListDataItem> Combo5Items { get; }
		public List<ListDataItem> Combo6Items { get; }
		public List<ListDataItem> Combo7Items { get; }
		public TextEditorConfiguration ComboConfiguration { get; set; }

		public AgendaTabEInfo() : base(ShiftChildTabType.E)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();

			MemoPopup1Items = new List<ListDataItem>();
			MemoPopup1Configuration = TextEditorConfiguration.Empty();

			MemoPopup2Items = new List<ListDataItem>();
			MemoPopup2Configuration = TextEditorConfiguration.Empty();

			MemoPopup3Items = new List<ListDataItem>();
			MemoPopup3Configuration = TextEditorConfiguration.Empty();

			MemoPopup4Items = new List<ListDataItem>();
			MemoPopup4Configuration = TextEditorConfiguration.Empty();

			MemoPopup5Items = new List<ListDataItem>();
			MemoPopup5Configuration = TextEditorConfiguration.Empty();

			MemoPopup6Items = new List<ListDataItem>();
			MemoPopup6Configuration = TextEditorConfiguration.Empty();

			Combo1Items = new List<ListDataItem>();
			Combo2Items = new List<ListDataItem>();
			Combo3Items = new List<ListDataItem>();
			Combo4Items = new List<ListDataItem>();
			Combo5Items = new List<ListDataItem>();
			Combo6Items = new List<ListDataItem>();
			Combo7Items = new List<ListDataItem>();
			ComboConfiguration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab3SubERightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubERightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab3SubEFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubEFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab3SubEBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubEBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab3SubE1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab3SubE1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab3SubE2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab3SubE2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab3SubE3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab3SubE3File.LocalPath)
				: null;

			if (resourceManager.DataAgendaPartEFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataAgendaPartEFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT03E");
				if (node == null) return;

				Tab1Title = node.SelectSingleNode("./SHIFT03ETab1Name")?.Attributes
					?.OfType<XmlAttribute>()
					.FirstOrDefault(a => String.Equals(a.Name, "Value", StringComparison.OrdinalIgnoreCase))?.Value ?? "Goals";
				Tab2Title = node.SelectSingleNode("./SHIFT03ETab2Name")?.Attributes
					?.OfType<XmlAttribute>()
					.FirstOrDefault(a => String.Equals(a.Name, "Value", StringComparison.OrdinalIgnoreCase))?.Value ?? "Strategy";
				Tab3Title = node.SelectSingleNode("./SHIFT03ETab3Name")?.Attributes
					?.OfType<XmlAttribute>()
					.FirstOrDefault(a => String.Equals(a.Name, "Value", StringComparison.OrdinalIgnoreCase))?.Value ?? "Tactics";

				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT03EHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
						case "SHIFT03EMULTIBOX1":
							if (!String.IsNullOrEmpty(item.Value))
								MemoPopup1Items.Add(item);
							break;
						case "SHIFT03EMULTIBOX2":
							if (!String.IsNullOrEmpty(item.Value))
								MemoPopup2Items.Add(item);
							break;
						case "SHIFT03EMULTIBOX3":
							if (!String.IsNullOrEmpty(item.Value))
								MemoPopup3Items.Add(item);
							break;
						case "SHIFT03EMULTIBOX4":
							if (!String.IsNullOrEmpty(item.Value))
								MemoPopup4Items.Add(item);
							break;
						case "SHIFT03EMULTIBOX5":
							if (!String.IsNullOrEmpty(item.Value))
								MemoPopup5Items.Add(item);
							break;
						case "SHIFT03EMULTIBOX6":
							if (!String.IsNullOrEmpty(item.Value))
								MemoPopup6Items.Add(item);
							break;
						case "SHIFT03ECOMBO1":
							if (!String.IsNullOrEmpty(item.Value))
								Combo1Items.Add(item);
							break;
						case "SHIFT03ECOMBO2":
							if (!String.IsNullOrEmpty(item.Value))
								Combo2Items.Add(item);
							break;
						case "SHIFT03ECOMBO3":
							if (!String.IsNullOrEmpty(item.Value))
								Combo3Items.Add(item);
							break;
						case "SHIFT03ECOMBO4":
							if (!String.IsNullOrEmpty(item.Value))
								Combo4Items.Add(item);
							break;
						case "SHIFT03ECOMBO5":
							if (!String.IsNullOrEmpty(item.Value))
								Combo5Items.Add(item);
							break;
						case "SHIFT03ECOMBO6":
							if (!String.IsNullOrEmpty(item.Value))
								Combo6Items.Add(item);
							break;
						case "SHIFT03ECOMBO7":
							if (!String.IsNullOrEmpty(item.Value))
								Combo7Items.Add(item);
							break;
					}
				}

				Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT03EClipart1");
				Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT03EClipart2");
				Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT03EClipart3");

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT03EHeader");
				MemoPopup1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03EMULTIBOX1");
				MemoPopup2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03EMULTIBOX2");
				MemoPopup3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03EMULTIBOX3");
				MemoPopup4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03EMULTIBOX4");
				MemoPopup5Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03EMULTIBOX5");
				MemoPopup6Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03EMULTIBOX6");
				ComboConfiguration = TextEditorConfiguration.FromXml(node, "TAB3COMBOS");
			}

			if (resourceManager.DataClientGoalsFile.ExistsLocal())
			{
				var targetMemoPopupLists = new[]
				{
					MemoPopup1Items,
					MemoPopup2Items,
					MemoPopup3Items,
				};

				var document = new XmlDocument();
				document.Load(resourceManager.DataClientGoalsFile.LocalPath);

				var goalNodes = document.SelectNodes("//ClientGoals/Goal")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };

				foreach (var node in goalNodes)
				{
					var goalDataItem = ListDataItem.FromXml(node);
					foreach (var itemList in targetMemoPopupLists)
					{
						if (!itemList.Any(item => String.Equals(item.Value, goalDataItem.Value, StringComparison.OrdinalIgnoreCase)))
							itemList.Add(ListDataItem.Clone(goalDataItem));
					}
				}
			}

			if (resourceManager.DataApproachesCommonFile.ExistsLocal())
			{
				var targetMemoPopupLists = new[]
				{
					MemoPopup4Items,
					MemoPopup5Items,
					MemoPopup6Items,
				};

				var document = new XmlDocument();
				document.Load(resourceManager.DataApproachesCommonFile.LocalPath);

				var approachNodes = document.SelectNodes("//OurApproach/Approach/Name")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };

				foreach (var node in approachNodes)
				{
					foreach (var itemList in targetMemoPopupLists)
					{
						if (!itemList.Any(item => String.Equals(item.Value, node.InnerText, StringComparison.OrdinalIgnoreCase)))
							itemList.Add(ListDataItem.FromString(node.InnerText));
					}
				}
			}

			if (resourceManager.DataSolutionsCommonFile.ExistsLocal())
			{
				var targetComboItemLists = new[]
				{
					Combo1Items,
					Combo2Items,
					Combo3Items,
					Combo4Items,
					Combo5Items,
					Combo6Items,
					Combo7Items,
				};

				var document = new XmlDocument();
				document.Load(resourceManager.DataSolutionsCommonFile.LocalPath);

				var productNodes = document.SelectNodes("//Products/Product/Name")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };

				foreach (var node in productNodes)
				{
					foreach (var comboItemList in targetComboItemLists)
					{
						if (!comboItemList.Any(item => String.Equals(item.Value, node.InnerText, StringComparison.OrdinalIgnoreCase)))
							comboItemList.Add(ListDataItem.FromString(node.InnerText));
					}
				}
			}
		}
	}
}
