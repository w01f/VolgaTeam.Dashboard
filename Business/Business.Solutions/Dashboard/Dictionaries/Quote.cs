using System.Text;
using System.Xml;

namespace Asa.Business.Solutions.Dashboard.Dictionaries
{
	public class Quote
	{
		public string Text { get; set; }
		public string Author { get; set; }

		public bool IsSet => !string.IsNullOrEmpty((Text + Author).Trim());

		public Quote()
		{
			Text = string.Empty;
			Author = string.Empty;
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<Text>" + Text.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Text>");
			result.AppendLine(@"<Author>" + Author.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Author>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Text":
						Text = childNode.InnerText;
						break;
					case "Author":
						Author = childNode.InnerText;
						break;
				}
			}
		}
	}
}