﻿using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Goals
{
	public class GoalsTabBInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public Image Clipart2Image { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public Image Clipart3Image { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public ListDataItem MemoPopup1DefaultItem { get; private set; }
		public TextEditorConfiguration MemoPopup1Configuration { get; set; }

		public ListDataItem MemoPopup2DefaultItem { get; private set; }
		public TextEditorConfiguration MemoPopup2Configuration { get; set; }

		public ListDataItem MemoPopup3DefaultItem { get; private set; }
		public TextEditorConfiguration MemoPopup3Configuration { get; set; }

		public GoalsTabBInfo() : base(ShiftChildTabType.B)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();

			MemoPopup1Configuration = TextEditorConfiguration.Empty();
			MemoPopup2Configuration = TextEditorConfiguration.Empty();
			MemoPopup3Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab4SubBRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubBRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab4SubBFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubBFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab4SubBBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubBBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab4SubB1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab4SubB1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab4SubB2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab4SubB2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab4SubB3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab4SubB3File.LocalPath)
				: null;
			
			if (!resourceManager.DataGoalsPartBFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataGoalsPartBFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT04B");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT04BHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT04BMULTIBOX1":
						MemoPopup1DefaultItem = item;
						break;
					case "SHIFT04BMULTIBOX2":
						MemoPopup2DefaultItem = item;
						break;
					case "SHIFT04BMULTIBOX3":
						MemoPopup3DefaultItem = item;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT04BClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT04BClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT04BClipart3");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT04BHeader");
			MemoPopup1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT04BMULTIBOX1");
			MemoPopup2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT04BMULTIBOX2");
			MemoPopup3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT04BMULTIBOX3");
		}
	}
}