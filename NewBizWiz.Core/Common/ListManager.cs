using System.IO;
using System.Text;
using System.Xml;

namespace NewBizWiz.Core.Common
{
	public class ListManager
	{
		private static readonly ListManager _instance = new ListManager();
		public const string DefaultBigLogoFileName = @"Default.png";
		public const string DefaultSmallLogoFileName = @"Default2.png";
		public const string DefaultTinyLogoFileName = @"Default3.png";

		private ListManager()
		{
			Advertisers = new AdvertisersManager();
			DecisionMakers = new DecisionMakersManager();
		}

		public static ListManager Instance
		{
			get { return _instance; }
		}

		public AdvertisersManager Advertisers { get; private set; }
		public DecisionMakersManager DecisionMakers { get; private set; }

		public void Init()
		{
			Advertisers.Load();
			DecisionMakers.Load();
		}
	}

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
