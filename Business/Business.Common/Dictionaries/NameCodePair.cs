using System.Text;
using System.Xml;

namespace Asa.Business.Common.Dictionaries
{
	public class NameCodePair
	{
		public NameCodePair()
		{
			Name = string.Empty;
			Code = string.Empty;
		}

		public string Name { get; set; }
		public string Code { get; set; }

		public string Serialize()
		{
			var xml = new StringBuilder();

			xml.Append(@"<NameCodePair ");
			xml.Append("Name = \"" + Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("Code = \"" + Code.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.AppendLine(@"/>");

			return xml.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlAttribute attribute in node.Attributes)
				switch (attribute.Name)
				{
					case "Name":
						Name = attribute.Value;
						break;
					case "Code":
						Code = attribute.Value;
						break;
				}
		}
	}
}
