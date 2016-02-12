using System.Collections.Generic;
using System.Xml;
using Asa.Business.Dashboard.Configuration;

namespace Asa.Business.Dashboard.Dictionaries
{
	public class LeadoffStatementLists
	{
		public LeadoffStatementLists()
		{
			Headers = new List<string>();
			Statements = new List<string>();
			Load();
		}

		public List<string> Headers { get; set; }
		public List<string> Statements { get; set; }

		private void Load()
		{
			var document = new XmlDocument();
			document.Load(ResourceManager.Instance.DataLeadoffStatementFile.LocalPath);

			var node = document.SelectSingleNode(@"/LeadOff");
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
					case "Statement":
						foreach (XmlAttribute attribute in childNode.Attributes)
						{
							switch (attribute.Name)
							{
								case "Value":
									if (!string.IsNullOrEmpty(attribute.Value))
										Statements.Add(attribute.Value);
									break;
							}
						}
						break;
				}
			}
		}
	}
}
