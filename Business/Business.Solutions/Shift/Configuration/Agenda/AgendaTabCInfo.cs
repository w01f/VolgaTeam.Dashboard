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

		public List<ListDataItem> Combo1Items { get; }
		public List<ListDataItem> Combo2Items { get; }
		public List<ListDataItem> Combo3Items { get; }
		public List<ListDataItem> Combo4Items { get; }
		public List<ListDataItem> Combo5Items { get; }
		public List<ListDataItem> Combo6Items { get; }

		public ClipartConfiguration Clipart1Configuration { get; private set; }

		public AgendaTabCInfo() : base(ShiftChildTabType.C)
		{
			Combo1Items = new List<ListDataItem>();
			Combo2Items = new List<ListDataItem>();
			Combo3Items = new List<ListDataItem>();
			Combo4Items = new List<ListDataItem>();
			Combo5Items = new List<ListDataItem>();
			Combo6Items = new List<ListDataItem>();

			Clipart1Configuration = new ClipartConfiguration();
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

			EditorConfiguration = TextEditorConfiguration.FromXml(node);
		}
	}
}
