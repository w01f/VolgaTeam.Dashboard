﻿using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Cover
{
	public class CoverTabCInfo : ShiftChildTabInfo
	{
		public Image Clipart1Image { get; private set; }

		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public string SubHeader1DefaultValue { get; private set; }
		public string SubHeader1Placeholder { get; private set; }

		public CoverTabCInfo() : base(ShiftChildTabType.C)
		{
			Clipart1Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab1SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubCRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab1SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubCFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab1SubCBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab1SubCBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab1SubC1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab1SubC1File.LocalPath)
				: null;

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

			EditorConfiguration = TextEditorConfiguration.FromXml(node);
		}
	}
}
