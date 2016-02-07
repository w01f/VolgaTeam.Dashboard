using System;
using System.Text;
using System.Xml;

namespace Asa.Business.Media.Configuration
{
	public class CalendarToggleSettings
	{
		public bool ShowLogo { get; set; }
		public bool ShowBigDate { get; set; }


		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			result.AppendLine(@"<ShowBigDate>" + ShowBigDate + @"</ShowBigDate>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "ShowLogo":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowLogo = temp;
						}
						break;
					case "ShowBigDate":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowBigDate = temp;
						}
						break;
				}
		}

		public void ApplyValue(string propertyName, string value)
		{
			switch (propertyName)
			{
				case "Show Logo at Top of Slide":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowLogo = temp;
					}
					break;
				case "Show BIG Date numbers":
					{
						bool temp;
						if (Boolean.TryParse(value, out temp))
							ShowBigDate = temp;
					}
					break;
			}
		}
	}
}
