using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Intro
{
	public class IntroTabCInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab2_C_Clipart1;
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab2_C_Clipart2;
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public Image Clipart3Image => _resourceManager.GraphicResources?.Tab2_C_Clipart3;
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public Image Clipart4Image => _resourceManager.GraphicResources?.Tab2_C_Clipart4;
		public ClipartConfiguration Clipart4Configuration { get; private set; }

		public IntroTabCInfo() : base(ShiftChildTabType.C, ShiftTopTabType.Intro)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();
			Clipart4Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (!resourceManager.DataIntroPartCFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataIntroPartCFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT02C");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT02CHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT02CClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT02CClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT02CClipart3");
			Clipart4Configuration = ClipartConfiguration.FromXml(node, "SHIFT02CClipart4");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT02CHeader");
		}
	}
}
