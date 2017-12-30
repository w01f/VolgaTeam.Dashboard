﻿using System;
using System.Collections.Generic;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class CustomerConfiguration
	{
		public List<SlideHeader> HeadersPartAItems { get; set; }
		public List<ComboboxItem> PartACombo1Items { get; }
		public List<ComboboxItem> PartACombo2Items { get; }
		public List<ComboboxItem> PartACombo3Items { get; }
		public List<ComboboxItem> PartACombo4Items { get; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }
		public ClipartConfiguration PartAClipart2Configuration { get; private set; }

		public List<SlideHeader> HeadersPartBItems { get; set; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }

		public List<SlideHeader> HeadersPartCItems { get; set; }

		public CustomerConfiguration()
		{
			HeadersPartAItems = new List<SlideHeader>();
			PartACombo1Items = new List<ComboboxItem>();
			PartACombo2Items = new List<ComboboxItem>();
			PartACombo3Items = new List<ComboboxItem>();
			PartACombo4Items = new List<ComboboxItem>();
			PartAClipart1Configuration = new ClipartConfiguration();
			PartAClipart2Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<SlideHeader>();
			PartBClipart1Configuration = new ClipartConfiguration();
			PartBClipart2Configuration = new ClipartConfiguration();

			HeadersPartCItems = new List<SlideHeader>();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataCustomerPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCustomerPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP04A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP04AHeader":
							{
								var header = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(header.Value))
									HeadersPartAItems.Add(header);
							}
							break;
						case "CP04ACombo1":
							{
								var item = ComboboxItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartACombo1Items.Add(item);
							}
							break;
						case "CP04ACombo2":
							{
								var item = ComboboxItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartACombo2Items.Add(item);
							}
							break;
						case "CP04ACombo3":
							{
								var item = ComboboxItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartACombo3Items.Add(item);
							}
							break;
						case "CP04ACombo4":
							{
								var item = ComboboxItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartACombo4Items.Add(item);
							}
							break;
					}
				}


				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP04AClipart1");
				PartAClipart2Configuration = ClipartConfiguration.FromXml(node, "CP04AClipart2");
			}

			if (resourceManager.DataCustomerPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCustomerPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP04B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP04BHeader":
							{
								var header = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(header.Value))
									HeadersPartBItems.Add(header);
							}
							break;
					}
				}

				PartBClipart1Configuration = ClipartConfiguration.FromXml(node, "CP04BClipart1");
				PartBClipart2Configuration = ClipartConfiguration.FromXml(node, "CP04BClipart2");
			}

			if (resourceManager.DataCustomerPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCustomerPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP04C");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP04CHeader":
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