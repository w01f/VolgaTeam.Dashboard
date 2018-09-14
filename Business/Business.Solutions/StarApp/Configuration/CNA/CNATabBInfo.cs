using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.CNA
{
	public class CNATabBInfo : StarTabWithHeaderInfo
	{
		public override StarChildTabType TabType => StarChildTabType.B;

		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab2_B_Clipart1;
		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab2_B_Clipart2;

		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public CNATabBInfo() : base(StarTopTabType.CNA)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (!resourceManager.DataCNAPartBFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataCNAPartBFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP02B");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP02BHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP02BClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP02BClipart2");
		}
	}
}
