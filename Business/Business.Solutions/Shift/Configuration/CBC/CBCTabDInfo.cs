using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.CBC
{
	public class CBCTabDInfo : ShiftTabWithHeaderInfo
	{
		public TabInfo Tab3Info { get; private set; }
		public TabInfo Tab4Info { get; private set; }

		public CBCTabDInfo() : base(ShiftChildTabType.D) { }

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab8SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubDRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab8SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubDFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab8SubDBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubDBackgroundFile.LocalPath)
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

			if (resourceManager.DataCBCPartDFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCBCPartDFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT08D");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT08DHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
					}
				}

				if (steps.Count == 5)
				{
					Tab3Info = TabInfo.FromXml(node, "SHIFT08D", steps.Single(step => step.Index == 3));
					Tab4Info = TabInfo.FromXml(node, "SHIFT08D", steps.Single(step => step.Index == 4));
				}

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT08DHeader");
			}
		}
	}
}
