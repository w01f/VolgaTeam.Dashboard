using System;
using System.Collections.Generic;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class MarketConfiguration
	{
		public List<SlideHeader> HeadersPartAItems { get; set; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }

		public List<SlideHeader> HeadersPartBItems { get; set; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }
		public ClipartConfiguration PartBClipart3Configuration { get; private set; }
		public ClipartConfiguration PartBClipart4Configuration { get; private set; }
		public ClipartConfiguration PartBClipart5Configuration { get; private set; }

		public List<SlideHeader> HeadersPartCItems { get; set; }
		public List<ComboboxItem> PartCCombo1Items { get; }
		public ClipartConfiguration PartCClipart1Configuration { get; private set; }
		public ClipartConfiguration PartCClipart2Configuration { get; private set; }
		public ClipartConfiguration PartCClipart3Configuration { get; private set; }
		public ClipartConfiguration PartCClipart4Configuration { get; private set; }

		public MarketConfiguration()
		{
			HeadersPartAItems = new List<SlideHeader>();
			PartAClipart1Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<SlideHeader>();
			PartBClipart1Configuration = new ClipartConfiguration();
			PartBClipart2Configuration = new ClipartConfiguration();
			PartBClipart3Configuration = new ClipartConfiguration();
			PartBClipart4Configuration = new ClipartConfiguration();
			PartBClipart5Configuration = new ClipartConfiguration();

			HeadersPartCItems = new List<SlideHeader>();
			PartCCombo1Items = new List<ComboboxItem>();
			PartCClipart1Configuration = new ClipartConfiguration();
			PartCClipart2Configuration = new ClipartConfiguration();
			PartCClipart3Configuration = new ClipartConfiguration();
			PartCClipart4Configuration = new ClipartConfiguration();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataMarketPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataMarketPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP07A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP07AHeader":
							{
								var item = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartAItems.Add(item);
							}
							break;
					}
				}

				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP07AClipart1");
			}

			if (resourceManager.DataMarketPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataMarketPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP07B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP07BHeader":
							{
								var item = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartBItems.Add(item);
							}
							break;
					}
				}

				PartBClipart1Configuration = ClipartConfiguration.FromXml(node, "CP07BClipart1");
				PartBClipart2Configuration = ClipartConfiguration.FromXml(node, "CP07BClipart2");
				PartBClipart3Configuration = ClipartConfiguration.FromXml(node, "CP07BClipart3");
				PartBClipart4Configuration = ClipartConfiguration.FromXml(node, "CP07BClipart4");
				PartBClipart5Configuration = ClipartConfiguration.FromXml(node, "CP07BClipart5");
			}

			if (resourceManager.DataMarketPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataMarketPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP07C");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP07CHeader":
							{
								var item = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartCItems.Add(item);
							}
							break;
						case "CP07CCombo1":
							{
								var item = ComboboxItem.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									PartCCombo1Items.Add(item);
							}
							break;
					}
				}

				PartCClipart1Configuration = ClipartConfiguration.FromXml(node, "CP07CClipart1");
				PartCClipart2Configuration = ClipartConfiguration.FromXml(node, "CP07CClipart2");
				PartCClipart3Configuration = ClipartConfiguration.FromXml(node, "CP07CClipart3");
				PartCClipart4Configuration = ClipartConfiguration.FromXml(node, "CP07CClipart4");
			}
		}
	}
}
