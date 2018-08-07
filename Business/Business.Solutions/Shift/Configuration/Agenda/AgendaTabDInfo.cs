using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Agenda
{
	public class AgendaTabDInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image { get; private set; }
		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public List<ListDataItem> Combo1Items { get; }
		public TextEditorConfiguration Combo1Configuration { get; set; }

		public List<ListDataItem> Combo2Items { get; }
		public TextEditorConfiguration Combo2Configuration { get; set; }

		public List<ListDataItem> Combo3Items { get; }
		public TextEditorConfiguration Combo3Configuration { get; set; }

		public List<ListDataItem> Combo4Items { get; }
		public TextEditorConfiguration Combo4Configuration { get; set; }

		public List<ListDataItem> Combo5Items { get; }
		public TextEditorConfiguration Combo5Configuration { get; set; }

		public AgendaTabDInfo() : base(ShiftChildTabType.D)
		{
			Clipart1Configuration = new ClipartConfiguration();

			Combo1Items = new List<ListDataItem>();
			Combo1Configuration = TextEditorConfiguration.Empty();

			Combo2Items = new List<ListDataItem>();
			Combo2Configuration = TextEditorConfiguration.Empty();

			Combo3Items = new List<ListDataItem>();
			Combo3Configuration = TextEditorConfiguration.Empty();

			Combo4Items = new List<ListDataItem>();
			Combo4Configuration = TextEditorConfiguration.Empty();

			Combo5Items = new List<ListDataItem>();
			Combo5Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab3SubDRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubDRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab3SubDFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubDFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab3SubDBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubDBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab3SubD1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab3SubD1File.LocalPath)
				: null;

			if (!resourceManager.DataAgendaPartDFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataAgendaPartDFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT03D");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT03DHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT03DCombo1":
						if (!String.IsNullOrEmpty(item.Value))
							Combo1Items.Add(item);
						break;
					case "SHIFT03DCombo2":
						if (!String.IsNullOrEmpty(item.Value))
							Combo2Items.Add(item);
						break;
					case "SHIFT03DCombo3":
						if (!String.IsNullOrEmpty(item.Value))
							Combo3Items.Add(item);
						break;
					case "SHIFT03DCombo4":
						if (!String.IsNullOrEmpty(item.Value))
							Combo4Items.Add(item);
						break;
					case "SHIFT03DCombo5":
						if (!String.IsNullOrEmpty(item.Value))
							Combo5Items.Add(item);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT03DClipart1");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT03DHeader");
			Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03DCombo1");
			Combo2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03DCombo2");
			Combo3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03DCombo3");
			Combo4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03DCombo4");
			Combo5Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03DCombo5");
		}
	}
}
