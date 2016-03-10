using System;
using System.Drawing;
using System.IO;
using System.Xml;

namespace Asa.Legacy.Media.Entities.Schedule
{
	public class Station
	{
		public string Name { get; set; }
		public Image Logo { get; set; }
		public bool Available { get; set; }

		public Station()
		{
			Name = string.Empty;
			Available = true;
		}
		
		public void Deserialize(XmlNode node)
		{
			foreach (XmlAttribute attribute in node.Attributes)
				switch (attribute.Name)
				{
					case "Name":
						Name = attribute.Value;
						break;
					case "Logo":
						Logo = string.IsNullOrEmpty(attribute.Value) ? null : new Bitmap(new MemoryStream(Convert.FromBase64String(attribute.Value)));
						break;
					case "Available":
						bool tempBool;
						if (bool.TryParse(attribute.Value, out tempBool))
							Available = tempBool;
						break;
				}
		}
	}
}
