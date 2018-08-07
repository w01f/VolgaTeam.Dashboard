using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Goals
{
	public class GoalsTabAInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public Image Clipart2Image { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public Image Clipart3Image { get; private set; }
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public ListDataItem Combo1DefaultItem { get; private set; }
		public TextEditorConfiguration Combo1Configuration { get; set; }

		public ListDataItem Combo2DefaultItem { get; private set; }
		public TextEditorConfiguration Combo2Configuration { get; set; }

		public ListDataItem Combo3DefaultItem { get; private set; }
		public TextEditorConfiguration Combo3Configuration { get; set; }

		public GoalsTabAInfo() : base(ShiftChildTabType.A)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();

			Combo1Configuration = TextEditorConfiguration.Empty();
			Combo2Configuration = TextEditorConfiguration.Empty();
			Combo3Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab4SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubARightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab4SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubAFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab4SubABackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab4SubABackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab4SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab4SubA1File.LocalPath)
				: null;
			Clipart2Image = resourceManager.ClipartTab4SubA2File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab4SubA2File.LocalPath)
				: null;
			Clipart3Image = resourceManager.ClipartTab4SubA3File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab4SubA3File.LocalPath)
				: null;
			
			if (!resourceManager.DataGoalsPartAFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataGoalsPartAFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT04A");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT04AHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT04AMULTIBOX1":
						Combo1DefaultItem = item;
						break;
					case "SHIFT04AMULTIBOX2":
						Combo2DefaultItem = item;
						break;
					case "SHIFT04AMULTIBOX3":
						Combo3DefaultItem = item;
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT04AClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT04AClipart2");
			Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT04AClipart3");
			
			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT04AHeader");
			Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT04AMULTIBOX1");
			Combo2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT04AMULTIBOX2");
			Combo3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT04AMULTIBOX3");
		}
	}
}
