using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.CBC
{
	public class CBCTabAInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab8_A_Clipart1;
		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab8_A_Clipart2;
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public Image Clipart3Image => _resourceManager.GraphicResources?.Tab8_A_Clipart3;
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public List<ListDataItem> MemoPopup1Items { get; }
		public TextEditorConfiguration MemoPopup1Configuration { get; set; }

		public CBCTabAInfo() : base(ShiftChildTabType.A, ShiftTopTabType.CBC)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();

			MemoPopup1Items = new List<ListDataItem>();
			MemoPopup1Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (resourceManager.DataCBCPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCBCPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT08A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var item = ListDataItem.FromXml(childNode);
					switch (childNode.Name)
					{
						case "SHIFT08AHeader":
							if (!String.IsNullOrEmpty(item.Value))
								HeadersItems.Add(item);
							break;
						case "SHIFT08AMULTIBOX1":
							if (!String.IsNullOrEmpty(item.Value))
								MemoPopup1Items.Add(item);
							break;
					}
				}

				Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT08AClipart1");
				Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT08AClipart2");
				Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT08AClipart3");

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT08AHeader");
				MemoPopup1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT08AMultibox1");
			}
		}
	}
}
