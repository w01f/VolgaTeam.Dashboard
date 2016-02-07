using System.Text;
using System.Xml;

namespace Asa.Business.Online.Configuration
{
	public class AdPlanViewSettings
	{
		public AdPlanViewSettings()
		{
			MoreSlides = true;
		}

		public bool MoreSlides { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<MoreSlides>" + MoreSlides + @"</MoreSlides>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "MoreSlides":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							MoreSlides = tempBool;
						break;
				}
			}
		}
	}
}
