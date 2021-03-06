﻿using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Video
{
	public class VideoTabDInfo : StarTabWithHeaderInfo
	{
		public override StarChildTabType TabType => StarChildTabType.D;

		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab8_D_Clipart1;

		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public VideoTabDInfo() : base(StarTopTabType.Video)
		{
			Clipart1Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (!resourceManager.DataVideoPartDFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataVideoPartDFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP08D");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP08DHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "CP08DSubheader1":
						if (item.IsPlaceholder)
							SubHeader1Placeholder = item.Value;
						else
							SubHeader1DefaultValue = item.Value;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP08DClipart1");
		}
	}
}
