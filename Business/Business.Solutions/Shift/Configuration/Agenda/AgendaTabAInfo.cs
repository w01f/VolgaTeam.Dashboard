using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Agenda
{
	public class AgendaTabAInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image { get; private set; }

		public List<ListDataItem> Combo1Items { get; }
		public List<ListDataItem> Combo2Items { get; }
		public List<ListDataItem> Combo3Items { get; }
		public List<ListDataItem> Combo4Items { get; }
		public List<ListDataItem> Combo5Items { get; }
		public List<ListDataItem> Combo6Items { get; }
		public List<ListDataItem> Combo7Items { get; }
		public List<ListDataItem> Combo8Items { get; }

		public ClipartConfiguration Clipart1Configuration { get; private set; }
		
		public AgendaTabAInfo() : base(ShiftChildTabType.A)
		{
			Combo1Items = new List<ListDataItem>();
			Combo2Items = new List<ListDataItem>();
			Combo3Items = new List<ListDataItem>();
			Combo4Items = new List<ListDataItem>();
			Combo5Items = new List<ListDataItem>();
			Combo6Items = new List<ListDataItem>();
			Combo7Items = new List<ListDataItem>();
			Combo8Items = new List<ListDataItem>();

			Clipart1Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab3SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubARightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab3SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubAFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab3SubABackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab3SubABackgroundFile.LocalPath)
				: null;

			Clipart1Image = resourceManager.ClipartTab3SubA1File.ExistsLocal()
				? Image.FromFile(resourceManager.ClipartTab3SubA1File.LocalPath)
				: null;

			if (!resourceManager.DataAgendaPartAFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataAgendaPartAFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT03A");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT03AHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT03ACombo1":
						if (!String.IsNullOrEmpty(item.Value))
							Combo1Items.Add(item);
						break;
					case "SHIFT03ACombo2":
						if (!String.IsNullOrEmpty(item.Value))
							Combo2Items.Add(item);
						break;
					case "SHIFT03ACombo3":
						if (!String.IsNullOrEmpty(item.Value))
							Combo3Items.Add(item);
						break;
					case "SHIFT03ACombo4":
						if (!String.IsNullOrEmpty(item.Value))
							Combo4Items.Add(item);
						break;
					case "SHIFT03ACombo5":
						if (!String.IsNullOrEmpty(item.Value))
							Combo5Items.Add(item);
						break;
					case "SHIFT03ACombo6":
						if (!String.IsNullOrEmpty(item.Value))
							Combo6Items.Add(item);
						break;
					case "SHIFT03ACombo7":
						if (!String.IsNullOrEmpty(item.Value))
							Combo7Items.Add(item);
						break;
					case "SHIFT03ACombo8":
						if (!String.IsNullOrEmpty(item.Value))
							Combo8Items.Add(item);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT03AClipart1");

			EditorConfiguration = TextEditorConfiguration.FromXml(node);
		}
	}
}
