using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.CBC
{
	public class CBCTabEInfo : ShiftTabWithHeaderInfo
	{
		public TabInfo Tab5Info { get; private set; }

		public CBCTabEInfo() : base(ShiftChildTabType.E) { }

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab8SubERightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubERightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab8SubEFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubEFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab8SubEBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab8SubEBackgroundFile.LocalPath)
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

			if (resourceManager.DataCBCPartEFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCBCPartEFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT08E");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT08EHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
					}
				}

				if (steps.Count == 5)
				{
					Tab5Info = TabInfo.FromXml(node, "SHIFT08E", steps.Single(step => step.Index == 5));
				}

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT08EHeader");
			}
		}
	}
}
