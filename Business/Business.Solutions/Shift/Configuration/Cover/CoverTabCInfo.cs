﻿using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Cover
{
	public class CoverTabCInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab1_C_Clipart1;
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public TextEditorConfiguration SubHeader1Configuration { get; set; }

		public TextEditorConfiguration Calendar1Configuration { get; set; }

		public CoverTabCInfo() : base(ShiftChildTabType.C,ShiftTopTabType.Cover)
		{
			Clipart1Configuration = new ClipartConfiguration();
			SubHeader1Configuration = TextEditorConfiguration.Empty();
			Calendar1Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (!resourceManager.DataCoverPartCFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataCoverPartCFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT01C");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT01CHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT01CSubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT01CClipart1");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT01CHeader");
			SubHeader1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT01CSubheader1");
			Calendar1Configuration = TextEditorConfiguration.FromXml(node, "Calendar1");
		}
	}
}
