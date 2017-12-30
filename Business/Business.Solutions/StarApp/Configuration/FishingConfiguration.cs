﻿using System;
using System.Collections.Generic;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class FishingConfiguration
	{
		public List<SlideHeader> HeadersPartAItems { get; set; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }

		public List<SlideHeader> HeadersPartBItems { get; set; }
		public List<ComboboxItem> PartBCombo1Items { get; }
		public List<ComboboxItem> PartBCombo2Items { get; }
		public List<ComboboxItem> PartBCombo3Items { get; }
		public List<ComboboxItem> PartBCombo4Items { get; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }

		public List<SlideHeader> HeadersPartCItems { get; set; }

		public FishingConfiguration()
		{
			HeadersPartAItems = new List<SlideHeader>();
			PartAClipart1Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<SlideHeader>();
			PartBCombo1Items = new List<ComboboxItem>();
			PartBCombo2Items = new List<ComboboxItem>();
			PartBCombo3Items = new List<ComboboxItem>();
			PartBCombo4Items = new List<ComboboxItem>();
			PartBClipart1Configuration = new ClipartConfiguration();
			PartBClipart2Configuration = new ClipartConfiguration();

			HeadersPartCItems = new List<SlideHeader>();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataFishingPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataFishingPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP03A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP03AHeader":
							{
								var header = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(header.Value))
									HeadersPartAItems.Add(header);
							}
							break;
					}
				}

				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP03AClipart1");
			}

			if (resourceManager.DataFishingPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataFishingPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP03B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP03BHeader":
							{
								var header = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(header.Value))
									HeadersPartBItems.Add(header);
							}
							break;
						case "CP03BCombo1":
						{
							var item = ComboboxItem.FromXml(childNode);
							if (!String.IsNullOrEmpty(item.Value))
								PartBCombo1Items.Add(item);
						}
							break;
						case "CP03BCombo2":
						{
							var item = ComboboxItem.FromXml(childNode);
							if (!String.IsNullOrEmpty(item.Value))
								PartBCombo2Items.Add(item);
						}
							break;
						case "CP03BCombo3":
						{
							var item = ComboboxItem.FromXml(childNode);
							if (!String.IsNullOrEmpty(item.Value))
								PartBCombo3Items.Add(item);
						}
							break;
						case "CP03BCombo4":
						{
							var item = ComboboxItem.FromXml(childNode);
							if (!String.IsNullOrEmpty(item.Value))
								PartBCombo4Items.Add(item);
						}
							break;
					}
				}

				PartBClipart1Configuration = ClipartConfiguration.FromXml(node, "CP03BClipart1");
				PartBClipart2Configuration = ClipartConfiguration.FromXml(node, "CP03BClipart2");
			}

			if (resourceManager.DataFishingPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataFishingPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP03C");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP03CHeader":
							{
								var header = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(header.Value))
									HeadersPartCItems.Add(header);
							}
							break;
					}
				}
			}
		}
	}
}