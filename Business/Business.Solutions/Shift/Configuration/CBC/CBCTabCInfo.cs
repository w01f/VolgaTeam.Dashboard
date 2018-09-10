using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.CBC
{
	public class CBCTabCInfo : ShiftTabWithHeaderInfo
	{
		public TabInfo Tab1Info { get; private set; }
		public TabInfo Tab2Info { get; private set; }

		public CBCTabCInfo() : base(ShiftChildTabType.C, ShiftTopTabType.CBC) { }

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			var steps = new List<StepInfo>();
			if (resourceManager.DataCBCCommonFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCBCCommonFile.LocalPath);

				var stepInfoNodes = document.SelectNodes("//CBC/Step")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var stepInfoNode in stepInfoNodes)
					steps.Add(StepInfo.FromXml(stepInfoNode));
			}

			if (resourceManager.DataCBCPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCBCPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT08C");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT08CHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
					}
				}

				if (steps.Count == 5)
				{
					Tab1Info = TabInfo.FromXml(node, "SHIFT08C", steps.Single(step => step.Index == 1));
					Tab2Info = TabInfo.FromXml(node, "SHIFT08C", steps.Single(step => step.Index == 2));
				}

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT08CHeader");
			}
		}
	}
}
