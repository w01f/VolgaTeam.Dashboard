﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Asa.Business.Solutions.StarApp.Configuration
{
	public class SolutionConfiguration
	{
		public List<SlideHeader> HeadersPartAItems { get; set; }
		public string PartASubHeader1DefaultValue { get; private set; }
		public ClipartConfiguration PartAClipart1Configuration { get; private set; }

		public List<SlideHeader> HeadersPartBItems { get; set; }
		public ClipartConfiguration PartBClipart1Configuration { get; private set; }
		public ClipartConfiguration PartBClipart2Configuration { get; private set; }
		public ClipartConfiguration PartBClipart3Configuration { get; private set; }

		public List<SlideHeader> HeadersPartCItems { get; set; }
		public ClipartConfiguration PartCClipart1Configuration { get; private set; }
		public ClipartConfiguration PartCClipart2Configuration { get; private set; }

		public List<SlideHeader> HeadersPartDItems { get; set; }
		public ClipartConfiguration PartDClipart1Configuration { get; private set; }

		public SolutionConfiguration()
		{
			HeadersPartAItems = new List<SlideHeader>();
			PartAClipart1Configuration = new ClipartConfiguration();

			HeadersPartBItems = new List<SlideHeader>();
			PartBClipart1Configuration = new ClipartConfiguration();
			PartBClipart2Configuration = new ClipartConfiguration();
			PartBClipart3Configuration = new ClipartConfiguration();

			HeadersPartCItems = new List<SlideHeader>();
			PartCClipart1Configuration = new ClipartConfiguration();
			PartCClipart2Configuration = new ClipartConfiguration();

			HeadersPartDItems = new List<SlideHeader>();
			PartDClipart1Configuration = new ClipartConfiguration();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataSolutionPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSolutionPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP10A");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP10AHeader":
							{
								var item = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartAItems.Add(item);
							}
							break;
						case "CP10ASubHeader1":
							PartASubHeader1DefaultValue = childNode.Attributes.OfType<XmlAttribute>().FirstOrDefault(a => String.Equals(a.Name, "Value"))?.Value;
							break;
					}
				}
				PartAClipart1Configuration = ClipartConfiguration.FromXml(node, "CP10AClipart1");
			}

			if (resourceManager.DataSolutionPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSolutionPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP10B");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP10BHeader":
							{
								var item = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartBItems.Add(item);
							}
							break;
					}
				}
				PartBClipart1Configuration = ClipartConfiguration.FromXml(node, "CP10BClipart1");
				PartBClipart2Configuration = ClipartConfiguration.FromXml(node, "CP10BClipart2");
				PartBClipart3Configuration = ClipartConfiguration.FromXml(node, "CP10BClipart3");
			}

			if (resourceManager.DataSolutionPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSolutionPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP10C");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP10CHeader":
							{
								var item = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartCItems.Add(item);
							}
							break;
					}
				}
				PartCClipart1Configuration = ClipartConfiguration.FromXml(node, "CP10CClipart1");
				PartCClipart2Configuration = ClipartConfiguration.FromXml(node, "CP10CClipart2");
			}

			if (resourceManager.DataSolutionPartDFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSolutionPartDFile.LocalPath);

				var node = document.SelectSingleNode(@"/CP10D");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "CP10DHeader":
							{
								var item = SlideHeader.FromXml(childNode);
								if (!String.IsNullOrEmpty(item.Value))
									HeadersPartDItems.Add(item);
							}
							break;
					}
				}
				PartDClipart1Configuration = ClipartConfiguration.FromXml(node, "CP10DClipart1");
			}
		}
	}
}