using System;
using System.Collections.Generic;
using System.Xml;
using Asa.Business.Solutions.StarApp.Configuration;

namespace Asa.Business.Solutions.StarApp.Dictionaries
{
	public class SolutionLists
	{
		public List<SlideHeader> HeadersPartA { get; set; }
		public List<SlideHeader> HeadersPartB { get; set; }
		public List<SlideHeader> HeadersPartC { get; set; }
		public List<SlideHeader> HeadersPartD { get; set; }

		public SolutionLists()
		{
			HeadersPartA = new List<SlideHeader>();
			HeadersPartB = new List<SlideHeader>();
			HeadersPartC = new List<SlideHeader>();
			HeadersPartD = new List<SlideHeader>();
		}

		public void Load(ResourceManager resourceManager)
		{
			if (resourceManager.DataSolutionPartAFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSolutionPartAFile.LocalPath);

				var node = document.SelectSingleNode(@"/Solution");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "SlideHeader":
							var header = SlideHeader.FromXml(childNode);
							if (!String.IsNullOrEmpty(header.Value))
								HeadersPartA.Add(header);
							break;
					}
				}
			}

			if (resourceManager.DataSolutionPartBFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSolutionPartBFile.LocalPath);

				var node = document.SelectSingleNode(@"/Solution");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "SlideHeader":
							var header = SlideHeader.FromXml(childNode);
							if (!String.IsNullOrEmpty(header.Value))
								HeadersPartB.Add(header);
							break;
					}
				}
			}

			if (resourceManager.DataSolutionPartCFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSolutionPartCFile.LocalPath);

				var node = document.SelectSingleNode(@"/Solution");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "SlideHeader":
							var header = SlideHeader.FromXml(childNode);
							if (!String.IsNullOrEmpty(header.Value))
								HeadersPartC.Add(header);
							break;
					}
				}
			}

			if (resourceManager.DataSolutionPartDFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSolutionPartDFile.LocalPath);

				var node = document.SelectSingleNode(@"/Solution");
				if (node == null) return;
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "SlideHeader":
							var header = SlideHeader.FromXml(childNode);
							if (!String.IsNullOrEmpty(header.Value))
								HeadersPartD.Add(header);
							break;
					}
				}
			}
		}
	}
}
