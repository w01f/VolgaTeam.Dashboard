using System.Collections.Generic;
using System.Xml;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Common.Dictionaries
{
	public class TargetCustomersLists
	{
		public List<string> Headers { get; set; }
		public List<string> Demos { get; set; }
		public List<string> HHIs { get; set; }
		public List<string> Geographies { get; set; }

		public List<string> CombinedList { get; set; }

		public TargetCustomersLists()
		{
			Headers = new List<string>();
			Demos = new List<string>();
			HHIs = new List<string>();
			Geographies = new List<string>();
			CombinedList = new List<string>();
		}

		public void Load(StorageFile dataFile)
		{
			var document = new XmlDocument();
			document.Load(dataFile.LocalPath);

			var node = document.SelectSingleNode(@"/TargetCustomers");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "SlideHeader":
						foreach (XmlAttribute attribute in childNode.Attributes)
						{
							switch (attribute.Name)
							{
								case "Value":
									if (!string.IsNullOrEmpty(attribute.Value))
										Headers.Add(attribute.Value);
									break;
							}
						}
						break;
					case "Demo":
						foreach (XmlAttribute attribute in childNode.Attributes)
						{
							switch (attribute.Name)
							{
								case "Value":
									if (!string.IsNullOrEmpty(attribute.Value))
										Demos.Add(attribute.Value);
									break;
							}
						}
						break;
					case "HHI":
						foreach (XmlAttribute attribute in childNode.Attributes)
						{
							switch (attribute.Name)
							{
								case "Value":
									if (!string.IsNullOrEmpty(attribute.Value))
										HHIs.Add(attribute.Value);
									break;
							}
						}
						break;
					case "Geography":
						foreach (XmlAttribute attribute in childNode.Attributes)
						{
							switch (attribute.Name)
							{
								case "Value":
									if (!string.IsNullOrEmpty(attribute.Value))
										Geographies.Add(attribute.Value);
									break;
							}
						}
						break;
				}
			}

			CombinedList.AddRange(Demos);
			CombinedList.AddRange(HHIs);
			CombinedList.AddRange(Geographies);
		}
	}
}
