﻿using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Goals
{
	public class GoalsTabDInfo : ShiftTabWithHeaderInfo
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

		public GoalsTabDInfo() : base(ShiftChildTabType.D)
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

			RightLogo = resourceManager.LogoTab4SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubDRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab4SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubDFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab4SubDBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubDBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab4SubD1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab4SubD1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab4SubD2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab4SubD2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab4SubD3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab4SubD3File.LocalPath)
				: null;

			if (!resourceManager.DataGoalsPartDFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataGoalsPartDFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT04D");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT04DHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT04DMULTIBOX1":
						MemoPopup1DefaultItem = item;
						break;
					case "SHIFT04DMULTIBOX2":
						MemoPopup2DefaultItem = item;
						break;
					case "SHIFT04DMULTIBOX3":
						MemoPopup3DefaultItem = item;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT04DClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT04DClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT04DClipart3");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT04DHeader");
			MemoPopup1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT04DMULTIBOX1");
			MemoPopup2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT04DMULTIBOX2");
			MemoPopup3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT04DMULTIBOX3");
		}
	}
}