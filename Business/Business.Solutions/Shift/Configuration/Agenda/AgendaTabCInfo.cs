using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Agenda
{
	public class AgendaTabCInfo : ShiftTabWithHeaderInfo
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

		public List<ListDataItem> Combo6Items { get; }
		public TextEditorConfiguration Combo6Configuration { get; set; }

		public AgendaTabCInfo() : base(ShiftChildTabType.C)
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

			Combo6Items = new List<ListDataItem>();
			Combo6Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab3SubCRightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubCRightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab3SubCFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubCFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab3SubCBackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubCBackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab3SubC1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab3SubC1File.LocalPath)
				: null;

			if (!resourceManager.DataAgendaPartCFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataAgendaPartCFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT03C");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT03CHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT03CCombo1":
						if (!String.IsNullOrEmpty(item.Value))
							Combo1Items.Add(item);
						break;
					case "SHIFT03CCombo2":
						if (!String.IsNullOrEmpty(item.Value))
							Combo2Items.Add(item);
						break;
					case "SHIFT03CCombo3":
						if (!String.IsNullOrEmpty(item.Value))
							Combo3Items.Add(item);
						break;
					case "SHIFT03CCombo4":
						if (!String.IsNullOrEmpty(item.Value))
							Combo4Items.Add(item);
						break;
					case "SHIFT03CCombo5":
						if (!String.IsNullOrEmpty(item.Value))
							Combo5Items.Add(item);
						break;
					case "SHIFT03CCombo6":
						if (!String.IsNullOrEmpty(item.Value))
							Combo6Items.Add(item);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT03CClipart1");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT03CHeader");
			Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03CCombo1");
			Combo2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03CCombo2");
			Combo3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03CCombo3");
			Combo4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03CCombo4");
			Combo5Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03CCombo5");
			Combo6Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03CCombo6");
		}
	}
}
