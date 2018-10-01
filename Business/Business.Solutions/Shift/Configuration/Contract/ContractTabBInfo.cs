using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Contract.TabB;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Contract
{
	public class ContractTabBInfo : ShiftTabWithHeaderInfo
	{
		public const string TermMonthlyInvestment = "monthly";
		public const string TermOneTimeInvestment = "one time cost";

		public TableConfiguration Table1Configuration { get; private set; }
		public TableConfiguration Table2Configuration { get; private set; }

		public CheckboxInfo SummaryCheckbox1 { get; private set; }
		public CheckboxInfo SummaryCheckbox3 { get; private set; }

		public List<ListDataItem> Table1Combo1AItems { get; }
		public List<ListDataItem> Table1Combo1CItems { get; }
		public List<ListDataItem> Table1Combo2AItems { get; }
		public List<ListDataItem> Table1Combo2CItems { get; }
		public List<ListDataItem> Table1Combo3AItems { get; }
		public List<ListDataItem> Table1Combo3CItems { get; }
		public List<ListDataItem> Table1Combo4AItems { get; }
		public List<ListDataItem> Table1Combo4CItems { get; }
		public List<ListDataItem> Table1Combo5AItems { get; }
		public List<ListDataItem> Table1Combo5CItems { get; }
		public List<ListDataItem> Table1Combo6AItems { get; }
		public List<ListDataItem> Table1Combo6CItems { get; }
		public List<ListDataItem> Table1Combo7AItems { get; }
		public List<ListDataItem> Table1Combo7CItems { get; }
		public List<ListDataItem> Table1Combo8AItems { get; }
		public List<ListDataItem> Table1Combo8CItems { get; }
		public List<ListDataItem> Table1Combo9AItems { get; }
		public List<ListDataItem> Table1Combo9CItems { get; }
		public List<ListDataItem> Table1Combo10AItems { get; }
		public List<ListDataItem> Table1Combo10CItems { get; }

		public List<ListDataItem> Table2Combo1AItems { get; }
		public List<ListDataItem> Table2Combo1CItems { get; }
		public List<ListDataItem> Table2Combo2AItems { get; }
		public List<ListDataItem> Table2Combo2CItems { get; }
		public List<ListDataItem> Table2Combo3AItems { get; }
		public List<ListDataItem> Table2Combo3CItems { get; }
		public List<ListDataItem> Table2Combo4AItems { get; }
		public List<ListDataItem> Table2Combo4CItems { get; }
		public List<ListDataItem> Table2Combo5AItems { get; }
		public List<ListDataItem> Table2Combo5CItems { get; }
		public List<ListDataItem> Table2Combo6AItems { get; }
		public List<ListDataItem> Table2Combo6CItems { get; }
		public List<ListDataItem> Table2Combo7AItems { get; }
		public List<ListDataItem> Table2Combo7CItems { get; }
		public List<ListDataItem> Table2Combo8AItems { get; }
		public List<ListDataItem> Table2Combo8CItems { get; }
		public List<ListDataItem> Table2Combo9AItems { get; }
		public List<ListDataItem> Table2Combo9CItems { get; }
		public List<ListDataItem> Table2Combo10AItems { get; }
		public List<ListDataItem> Table2Combo10CItems { get; }

		public List<List<ListDataItem>> Table1Column1Lists { get; }
		public List<List<ListDataItem>> Table1Column3Lists { get; }
		public List<List<ListDataItem>> Table2Column1Lists { get; }
		public List<List<ListDataItem>> Table2Column3Lists { get; }

		public List<ListDataItem> SummaryCombo2Items { get; }

		public TextEditorConfiguration TableComboConfiguration { get; set; }
		public TextEditorConfiguration SummaryConfiguration { get; set; }

		public ContractTabBInfo() : base(ShiftChildTabType.B, ShiftTopTabType.Contract)
		{
			Table1Configuration = TableConfiguration.Empty();
			Table2Configuration = TableConfiguration.Empty();

			Table1Combo1AItems = new List<ListDataItem>();
			Table1Combo1CItems = new List<ListDataItem>();
			Table1Combo2AItems = new List<ListDataItem>();
			Table1Combo2CItems = new List<ListDataItem>();
			Table1Combo3AItems = new List<ListDataItem>();
			Table1Combo3CItems = new List<ListDataItem>();
			Table1Combo4AItems = new List<ListDataItem>();
			Table1Combo4CItems = new List<ListDataItem>();
			Table1Combo5AItems = new List<ListDataItem>();
			Table1Combo5CItems = new List<ListDataItem>();
			Table1Combo6AItems = new List<ListDataItem>();
			Table1Combo6CItems = new List<ListDataItem>();
			Table1Combo7AItems = new List<ListDataItem>();
			Table1Combo7CItems = new List<ListDataItem>();
			Table1Combo8AItems = new List<ListDataItem>();
			Table1Combo8CItems = new List<ListDataItem>();
			Table1Combo9AItems = new List<ListDataItem>();
			Table1Combo9CItems = new List<ListDataItem>();
			Table1Combo10AItems = new List<ListDataItem>();
			Table1Combo10CItems = new List<ListDataItem>();

			Table2Combo1AItems = new List<ListDataItem>();
			Table2Combo1CItems = new List<ListDataItem>();
			Table2Combo2AItems = new List<ListDataItem>();
			Table2Combo2CItems = new List<ListDataItem>();
			Table2Combo3AItems = new List<ListDataItem>();
			Table2Combo3CItems = new List<ListDataItem>();
			Table2Combo4AItems = new List<ListDataItem>();
			Table2Combo4CItems = new List<ListDataItem>();
			Table2Combo5AItems = new List<ListDataItem>();
			Table2Combo5CItems = new List<ListDataItem>();
			Table2Combo6AItems = new List<ListDataItem>();
			Table2Combo6CItems = new List<ListDataItem>();
			Table2Combo7AItems = new List<ListDataItem>();
			Table2Combo7CItems = new List<ListDataItem>();
			Table2Combo8AItems = new List<ListDataItem>();
			Table2Combo8CItems = new List<ListDataItem>();
			Table2Combo9AItems = new List<ListDataItem>();
			Table2Combo9CItems = new List<ListDataItem>();
			Table2Combo10AItems = new List<ListDataItem>();
			Table2Combo10CItems = new List<ListDataItem>();

			SummaryCombo2Items = new List<ListDataItem>();

			SummaryCheckbox1 = CheckboxInfo.Empty();
			SummaryCheckbox3 = CheckboxInfo.Empty();

			TableComboConfiguration = TextEditorConfiguration.Empty();
			SummaryConfiguration = TextEditorConfiguration.Empty();

			Table1Column1Lists = new List<List<ListDataItem>>(new[]
			{
				Table1Combo1AItems,
				Table1Combo2AItems,
				Table1Combo3AItems,
				Table1Combo4AItems,
				Table1Combo5AItems,
				Table1Combo6AItems,
				Table1Combo7AItems,
				Table1Combo8AItems,
				Table1Combo9AItems,
				Table1Combo10AItems,
			});

			Table1Column3Lists = new List<List<ListDataItem>>(new[]
			{
				Table1Combo1CItems,
				Table1Combo2CItems,
				Table1Combo3CItems,
				Table1Combo4CItems,
				Table1Combo5CItems,
				Table1Combo6CItems,
				Table1Combo7CItems,
				Table1Combo8CItems,
				Table1Combo9CItems,
				Table1Combo10CItems,
			});

			Table2Column1Lists = new List<List<ListDataItem>>(new[]
			{
				Table2Combo1AItems,
				Table2Combo2AItems,
				Table2Combo3AItems,
				Table2Combo4AItems,
				Table2Combo5AItems,
				Table2Combo6AItems,
				Table2Combo7AItems,
				Table2Combo8AItems,
				Table2Combo9AItems,
				Table2Combo10AItems,
			});

			Table2Column3Lists = new List<List<ListDataItem>>(new[]
			{
				Table2Combo1CItems,
				Table2Combo2CItems,
				Table2Combo3CItems,
				Table2Combo4CItems,
				Table2Combo5CItems,
				Table2Combo6CItems,
				Table2Combo7CItems,
				Table2Combo8CItems,
				Table2Combo9CItems,
				Table2Combo10CItems,
			});
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (_resourceManager.DataContractPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(_resourceManager.DataContractPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT15B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT15BHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;

						case "SHIFT15BTab1Combo1A":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo1AItems.Add(item);
							break;
						case "SHIFT15BTab1Combo1C":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo1CItems.Add(item);
							break;
						case "SHIFT15BTab1Combo2A":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo2AItems.Add(item);
							break;
						case "SHIFT15BTab1Combo2C":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo2CItems.Add(item);
							break;
						case "SHIFT15BTab1Combo3A":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo3AItems.Add(item);
							break;
						case "SHIFT15BTab1Combo3C":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo3CItems.Add(item);
							break;
						case "SHIFT15BTab1Combo4A":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo4AItems.Add(item);
							break;
						case "SHIFT15BTab1Combo4C":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo4CItems.Add(item);
							break;
						case "SHIFT15BTab1Combo5A":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo5AItems.Add(item);
							break;
						case "SHIFT15BTab1Combo5C":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo5CItems.Add(item);
							break;
						case "SHIFT15BTab1Combo6A":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo6AItems.Add(item);
							break;
						case "SHIFT15BTab1Combo6C":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo6CItems.Add(item);
							break;
						case "SHIFT15BTab1Combo7A":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo7AItems.Add(item);
							break;
						case "SHIFT15BTab1Combo7C":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo7CItems.Add(item);
							break;
						case "SHIFT15BTab1Combo8A":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo8AItems.Add(item);
							break;
						case "SHIFT15BTab1Combo8C":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo8CItems.Add(item);
							break;
						case "SHIFT15BTab1Combo9A":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo9AItems.Add(item);
							break;
						case "SHIFT15BTab1Combo9C":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo9CItems.Add(item);
							break;
						case "SHIFT15BTab1Combo10A":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo10AItems.Add(item);
							break;
						case "SHIFT15BTab1Combo10C":
							if (!String.IsNullOrEmpty(item.Value))
								Table1Combo10CItems.Add(item);
							break;

						case "SHIFT15BTab2Combo1A":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo1AItems.Add(item);
							break;
						case "SHIFT15BTab2Combo1C":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo1CItems.Add(item);
							break;
						case "SHIFT15BTab2Combo2A":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo2AItems.Add(item);
							break;
						case "SHIFT15BTab2Combo2C":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo2CItems.Add(item);
							break;
						case "SHIFT15BTab2Combo3A":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo3AItems.Add(item);
							break;
						case "SHIFT15BTab2Combo3C":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo3CItems.Add(item);
							break;
						case "SHIFT15BTab2Combo4A":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo4AItems.Add(item);
							break;
						case "SHIFT15BTab2Combo4C":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo4CItems.Add(item);
							break;
						case "SHIFT15BTab2Combo5A":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo5AItems.Add(item);
							break;
						case "SHIFT15BTab2Combo5C":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo5CItems.Add(item);
							break;
						case "SHIFT15BTab2Combo6A":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo6AItems.Add(item);
							break;
						case "SHIFT15BTab2Combo6C":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo6CItems.Add(item);
							break;
						case "SHIFT15BTab2Combo7A":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo7AItems.Add(item);
							break;
						case "SHIFT15BTab2Combo7C":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo7CItems.Add(item);
							break;
						case "SHIFT15BTab2Combo8A":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo8AItems.Add(item);
							break;
						case "SHIFT15BTab2Combo8C":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo8CItems.Add(item);
							break;
						case "SHIFT15BTab2Combo9A":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo9AItems.Add(item);
							break;
						case "SHIFT15BTab2Combo9C":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo9CItems.Add(item);
							break;
						case "SHIFT15BTab2Combo10A":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo10AItems.Add(item);
							break;
						case "SHIFT15BTab2Combo10C":
							if (!String.IsNullOrEmpty(item.Value))
								Table2Combo10CItems.Add(item);
							break;

						case "SHIFT15BSummaryCheckbox1":
							SummaryCheckbox1 = CheckboxInfo.FromXml(childNode);
							break;
						case "SHIFT15BSummaryCheckbox2":
							SummaryCheckbox3 = CheckboxInfo.FromXml(childNode);
							break;
					}
				}

				Table1Configuration = TableConfiguration.FromXml(node, "SHIFT15BTab1");
				Table2Configuration = TableConfiguration.FromXml(node, "SHIFT15BTab2");

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT15BHeader");

				TableComboConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT15BTabCombos");
				SummaryConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT15BSummaryCombos");
			}

			if (resourceManager.DataAgreementCommonFile.ExistsLocal())
			{
				var table1Column1Combos = Table1Column1Lists;

				var table2Column1Combos = Table2Column1Lists;

				var column3Combos = Table1Column3Lists.Union(Table2Column3Lists).ToArray();

				var document = new XmlDocument();
				document.Load(resourceManager.DataAgreementCommonFile.LocalPath);

				var table1Column1Nodes = document.SelectNodes("//Settings/Television/Item")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var itemNode in table1Column1Nodes)
				{
					var nodeValue = itemNode?.InnerText;
					if (!String.IsNullOrWhiteSpace(nodeValue))
						foreach (var comboList in table1Column1Combos)
							if (!comboList.Any(item => String.Equals(nodeValue, item.Value, StringComparison.OrdinalIgnoreCase)))
								comboList.Add(ListDataItem.FromString(nodeValue));
				}

				var table2Column1Nodes = document.SelectNodes("//Settings/Digital/Item")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var itemNode in table2Column1Nodes)
				{
					var nodeValue = itemNode?.InnerText;
					if (!String.IsNullOrWhiteSpace(nodeValue))
						foreach (var comboList in table2Column1Combos)
							if (!comboList.Any(item => String.Equals(nodeValue, item.Value, StringComparison.OrdinalIgnoreCase)))
								comboList.Add(ListDataItem.FromString(nodeValue));
				}

				var column3Nodes = document.SelectNodes("//Settings/Term/Item")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var itemNode in column3Nodes)
				{
					var nodeValue = itemNode?.InnerText;
					if (!String.IsNullOrWhiteSpace(nodeValue))
						foreach (var comboList in column3Combos)
							if (!comboList.Any(item => String.Equals(nodeValue, item.Value, StringComparison.OrdinalIgnoreCase)))
								comboList.Add(ListDataItem.FromString(nodeValue));
				}

				var summaryCombo1Nodes = document.SelectNodes("//Settings/Months/Item")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var itemNode in summaryCombo1Nodes)
				{
					var nodeValue = itemNode?.InnerText;
					if (!String.IsNullOrWhiteSpace(nodeValue) &&
						!SummaryCombo2Items.Any(item => String.Equals(nodeValue, item.Value, StringComparison.OrdinalIgnoreCase)))
						SummaryCombo2Items.Add(ListDataItem.FromString(nodeValue));
				}
			}
		}
	}
}
