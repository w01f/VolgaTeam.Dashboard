using System;
using System.Xml;

namespace Asa.Common.Core.Objects.TabPages
{
	public class TabPageConfig
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public bool Visible { get; set; }
		public bool Enabled { get; set; }
		public int Order { get; set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Id":
						Id = childNode.InnerText;
						break;
					case "Name":
						Name = childNode.InnerText;
						break;
					case "Enabled":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								Enabled = temp;
							break;
						}
					case "Visible":
					{
						bool temp;
						if (Boolean.TryParse(childNode.InnerText, out temp))
							Visible = temp;
						break;
					}
					case "Order":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								Order = temp;
							break;
						}
				}
			}
		}
	}
}
