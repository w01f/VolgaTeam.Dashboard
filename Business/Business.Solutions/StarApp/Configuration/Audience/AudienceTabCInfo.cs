using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Audience
{
	public class AudienceTabCInfo : StarTabWithHeaderInfo
	{
		public override StarChildTabType TabType => StarChildTabType.C;

		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab9_C_Clipart1;
		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab9_C_Clipart2;
		public Image Clipart3Image => _resourceManager.GraphicResources?.Tab9_C_Clipart3;
		public Image Clipart4Image => _resourceManager.GraphicResources?.Tab9_C_Clipart4;

		public List<ListDataItem> Combo1Items { get; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }
		public ClipartConfiguration Clipart4Configuration { get; private set; }

		public AudienceTabCInfo() : base(StarTopTabType.Audience)
		{
			Combo1Items = new List<ListDataItem>();
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
			Clipart4Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (!resourceManager.DataAudiencePartCFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataAudiencePartCFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP09C");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP09CHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP09CCombo1":
						if (!String.IsNullOrEmpty(item.Value))
							Combo1Items.Add(item);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP09CClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP09CClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "CP09CClipart3");
			Clipart4Configuration = ClipartConfiguration.FromXml(node, "CP09CClipart4");
		}
	}
}
