using System;
using System.Collections.Generic;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class CNAConfiguration
	{
		public List<SlideHeader> HeadersPartAItems { get; set; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }

		public List<SlideHeader> HeadersPartBItems { get; set; }
		public List<ComboboxItem> PartBCombo1Items { get; }
		public List<ComboboxItem> PartBCombo2Items { get; }
		public List<ComboboxItem> PartBCombo3Items { get; }
		public List<ComboboxItem> PartBCombo4Items { get; }
		public List<ComboboxItem> PartBCombo5Items { get; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }

		public CNAConfiguration()
		{
			HeadersPartAItems = new List<SlideHeader>();
			PartAClipart1Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<SlideHeader>();
			PartBCombo1Items = new List<ComboboxItem>();
			PartBCombo2Items = new List<ComboboxItem>();
			PartBCombo3Items = new List<ComboboxItem>();
			PartBCombo4Items = new List<ComboboxItem>();
			PartBCombo5Items = new List<ComboboxItem>();
			PartBClipart1Configuration = new ClipartConfiguration();
			PartBClipart2Configuration = new ClipartConfiguration();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataCNAPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCNAPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP02A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP02AHeader":
							{
								var item = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartAItems.Add(item);
							}
							break;
					}
				}

				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP02AClipart1");
			}

			if (resourceManager.DataCNAPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataCNAPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP02B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP02BHeader":
							{
								var item = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartBItems.Add(item);
							}
							break;
						case "CP02BCombo1":
							{
								var item = ComboboxItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartBCombo1Items.Add(item);
							}
							break;
						case "CP02BCombo2":
							{
								var item = ComboboxItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartBCombo2Items.Add(item);
							}
							break;
						case "CP02BCombo3":
							{
								var item = ComboboxItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartBCombo3Items.Add(item);
							}
							break;
						case "CP02BCombo4":
							{
								var item = ComboboxItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartBCombo4Items.Add(item);
							}
							break;
						case "CP02BCombo5":
							{
								var item = ComboboxItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartBCombo5Items.Add(item);
							}
							break;
					}
				}

				PartBClipart1Configuration = ClipartConfiguration.FromXml(node, "CP02BClipart1");
				PartBClipart2Configuration = ClipartConfiguration.FromXml(node, "CP02BClipart2");
			}
		}
	}
}
