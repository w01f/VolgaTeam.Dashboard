using System.Xml;

namespace Asa.Legacy.Media.Entities.Schedule
{
	public class Daypart
	{
		public string Name { get; set; }
		public string Code { get; set; }
		public bool Available { get; set; }

		public Daypart()
		{
			Name = string.Empty;
			Code = string.Empty;
			Available = true;
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool;
			foreach (XmlAttribute attribute in node.Attributes)
				switch (attribute.Name)
				{
					case "Name":
						Name = attribute.Value;
						break;
					case "Code":
						Code = attribute.Value;
						break;
					case "Available":
						if (bool.TryParse(attribute.Value, out tempBool))
							Available = tempBool;
						break;
				}
		}
	}
}
