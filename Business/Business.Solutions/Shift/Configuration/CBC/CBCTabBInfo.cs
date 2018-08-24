using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.CBC
{
	public class CBCTabBInfo : ShiftTabWithHeaderInfo
	{
		public TabInfo Tab1Info { get; private set; }
		public TabInfo Tab2Info { get; private set; }
		public TabInfo Tab3Info { get; private set; }
		public TabInfo Tab4Info { get; private set; }
		public TabInfo Tab5Info { get; private set; }

		public CBCTabBInfo() : base(ShiftChildTabType.B) { }

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab8SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubBRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab8SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubBFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab8SubBBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubBBackgroundFile.LocalPath)
				: null;

			var steps = new List<StepInfo>();
			if (resourceManager.DataCBCCommonFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCBCCommonFile.LocalPath);

				var stepInfoNodes = document.SelectNodes("//CBC/Step")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var stepInfoNode in stepInfoNodes)
					steps.Add(StepInfo.FromXml(stepInfoNode));
			}

			if (resourceManager.DataCBCPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCBCPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT08B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT08BHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
					}
				}

				if (steps.Count == 5)
				{
					Tab1Info = TabInfo.FromXml(node, "SHIFT08B", steps.Single(step => step.Index == 1));
					Tab2Info = TabInfo.FromXml(node, "SHIFT08B", steps.Single(step => step.Index == 2));
					Tab3Info = TabInfo.FromXml(node, "SHIFT08B", steps.Single(step => step.Index == 3));
					Tab4Info = TabInfo.FromXml(node, "SHIFT08B", steps.Single(step => step.Index == 4));
					Tab5Info = TabInfo.FromXml(node, "SHIFT08B", steps.Single(step => step.Index == 5));
				}

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT08BHeader");
			}
		}
	}
}
