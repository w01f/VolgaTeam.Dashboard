using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Solution
{
	public class SolutionTabBInfo : StarTabWithHeaderInfo
	{
		public override StarChildTabType TabType => StarChildTabType.B;

		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab10_B_Clipart1;
		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab10_B_Clipart2;
		public Image Clipart3Image => _resourceManager.GraphicResources?.Tab10_B_Clipart3;

		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public SolutionTabBInfo() : base(StarTopTabType.Solution)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (!resourceManager.DataSolutionPartBFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataSolutionPartBFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP10B");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP10BHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP10BSubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
				}
			}
			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP10BClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP10BClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "CP10BClipart3");
		}
	}
}
