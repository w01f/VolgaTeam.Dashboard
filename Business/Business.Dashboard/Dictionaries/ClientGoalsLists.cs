using System.Collections.Generic;
using System.Xml;
using Asa.Business.Dashboard.Configuration;

namespace Asa.Business.Dashboard.Dictionaries
{
	public class ClientGoalsLists
	{
		public ClientGoalsLists()
		{
			Headers = new List<string>();
			Goals = new List<string>();
			Load();
		}

		public List<string> Headers { get; set; }
		public List<string> Goals { get; set; }

		private void Load()
		{
			var document = new XmlDocument();
			document.Load(ResourceManager.Instance.DataClientGoalsFile.LocalPath);

			var node = document.SelectSingleNode(@"/ClientGoals");
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
					case "Goal":
						foreach (XmlAttribute attribute in childNode.Attributes)
						{
							switch (attribute.Name)
							{
								case "Value":
									if (!string.IsNullOrEmpty(attribute.Value))
										Goals.Add(attribute.Value);
									break;
							}
						}
						break;
				}
			}
		}
	}
}
