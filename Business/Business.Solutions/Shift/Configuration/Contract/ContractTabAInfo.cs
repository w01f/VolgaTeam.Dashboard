using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Contract.TabA;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Contract
{
	public class ContractTabAInfo : ShiftTabWithHeaderInfo
	{
		public List<ListDataItem> Combo1Items { get; }
		public TextEditorConfiguration Combo1Configuration { get; set; }

		public List<ListDataItem> Combo2Items { get; }
		public TextEditorConfiguration Combo2Configuration { get; set; }

		public List<ProductInfo> Products { get; }

		public TabSelectorConfiguration TabSelector { get; set; }

		public ContractTabAInfo() : base(ShiftChildTabType.A, ShiftTopTabType.Contract)
		{
			Combo1Items = new List<ListDataItem>();
			Combo1Configuration = TextEditorConfiguration.Empty();
			Combo2Items = new List<ListDataItem>();
			Combo2Configuration = TextEditorConfiguration.Empty();

			Products = new List<ProductInfo>();

			TabSelector = TabSelectorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (resourceManager.DataSolutionsCommonFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSolutionsCommonFile.LocalPath);

				var itemInfoNodes = document.SelectNodes("//Products/Product")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var productNode in itemInfoNodes)
				{
					var fileName = productNode.SelectSingleNode("./ProductFile")?.InnerText;
					if (!String.IsNullOrWhiteSpace(fileName))
					{
						var filePath = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.LocalPath,
							fileName);
						if (File.Exists(filePath))
							Products.Add(ProductInfo.FromFile(
								productNode.SelectSingleNode("./Name")?.InnerText,
								filePath));
					}
				}
			}

			if (_resourceManager.DataContractPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(_resourceManager.DataContractPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT15A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT15AHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
						case "SHIFT15ACombo1":
							if (!String.IsNullOrEmpty(item.Value))
								Combo1Items.Add(item);
							break;
						case "SHIFT15ACombo2":
							if (!String.IsNullOrEmpty(item.Value))
								Combo2Items.Add(item);
							break;
					}
				}

				TabSelector = TabSelectorConfiguration.FromXml(node, "SHIFT15ATabStrip");

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT15AHeader");
				Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT15ACombo1");
				Combo2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT15ACombo2");

				var productMemoPopupConfiguration = TextEditorConfiguration.FromXml(node, "ProductMultiBox1");
				var productCombo1Configuration = TextEditorConfiguration.FromXml(node, "ProductCombo1");
				var productCombo2Configuration = TextEditorConfiguration.FromXml(node, "ProductCombo2");
				var productCombo3Configuration = TextEditorConfiguration.FromXml(node, "ProductCombo3");
				var productButtonConfiguration = ProductButtonConfiguration.FromXml(node, "ProductButtons");
				var productListConfiguration = FormListConfiguration.FromXml(node);

				foreach (var itemInfo in Products)
				{
					itemInfo.MemoPopup1Configuration = productMemoPopupConfiguration;
					itemInfo.Combo1Configuration = productCombo1Configuration;
					itemInfo.Combo2Configuration = productCombo2Configuration;
					itemInfo.Combo3Configuration = productCombo3Configuration;
					itemInfo.ButtonConfiguration = productButtonConfiguration;
					itemInfo.FormListConfiguration = productListConfiguration;
				}
			}
		}
	}
}
