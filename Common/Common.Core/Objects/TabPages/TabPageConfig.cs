using System;
using System.Xml;

namespace Asa.Common.Core.Objects.TabPages
{
	public abstract class TabPageConfig
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public bool Visible { get; set; }
		public bool Enabled { get; set; }

		public virtual void Deserialize(XmlNode node)
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
						if (Boolean.TryParse(childNode.InnerText, out var temp))
							Enabled = temp;
						break;
					}
					case "Visible":
					{
						if (Boolean.TryParse(childNode.InnerText, out var temp))
							Visible = temp;
						break;
					}
				}
			}
		}
	}
}
