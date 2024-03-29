﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Agenda
{
	public class AgendaTabBInfo : ShiftTabWithHeaderInfo
	{
		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab3_B_Clipart1;
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

		public List<ListDataItem> Combo7Items { get; }
		public TextEditorConfiguration Combo7Configuration { get; set; }

		public List<ListDataItem> Combo8Items { get; }
		public TextEditorConfiguration Combo8Configuration { get; set; }

		public AgendaTabBInfo() : base(ShiftChildTabType.B, ShiftTopTabType.Agenda)
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

			Combo7Items = new List<ListDataItem>();
			Combo7Configuration = TextEditorConfiguration.Empty();

			Combo8Items = new List<ListDataItem>();
			Combo8Configuration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (!resourceManager.DataAgendaPartBFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(resourceManager.DataAgendaPartBFile.LocalPath);

			var node = document.SelectSingleNode(@"/SHIFT03B");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "SHIFT03BHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
					case "SHIFT03BCombo1":
						if (!String.IsNullOrEmpty(item.Value))
							Combo1Items.Add(item);
						break;
					case "SHIFT03BCombo2":
						if (!String.IsNullOrEmpty(item.Value))
							Combo2Items.Add(item);
						break;
					case "SHIFT03BCombo3":
						if (!String.IsNullOrEmpty(item.Value))
							Combo3Items.Add(item);
						break;
					case "SHIFT03BCombo4":
						if (!String.IsNullOrEmpty(item.Value))
							Combo4Items.Add(item);
						break;
					case "SHIFT03BCombo5":
						if (!String.IsNullOrEmpty(item.Value))
							Combo5Items.Add(item);
						break;
					case "SHIFT03BCombo6":
						if (!String.IsNullOrEmpty(item.Value))
							Combo6Items.Add(item);
						break;
					case "SHIFT03BCombo7":
						if (!String.IsNullOrEmpty(item.Value))
							Combo7Items.Add(item);
						break;
					case "SHIFT03BCombo8":
						if (!String.IsNullOrEmpty(item.Value))
							Combo8Items.Add(item);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "SHIFT03BClipart1");

			CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
			HeadersEditorConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT03BHeader");
			Combo1Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03BCombo1");
			Combo2Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03BCombo2");
			Combo3Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03BCombo3");
			Combo4Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03BCombo4");
			Combo5Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03BCombo5");
			Combo6Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03BCombo6");
			Combo7Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03BCombo7");
			Combo8Configuration = TextEditorConfiguration.FromXml(node, "SHIFT03BCombo8");
		}
	}
}
