using System;
using System.Collections.Generic;
using System.Text;

namespace CommandCentral.CommonClasses
{
	public class DigitalProductInfo
	{
		public bool Selected { get; set; }
		public string Group { get; set; }
		public List<string> Phrases { get; private set; }

		public DigitalProductInfo()
		{
			Phrases = new List<string>();
		}

		public string Serialize()
		{
			var xml = new StringBuilder();
			xml.AppendLine(@"<Selected>" + Selected + @"</Selected>");
			if (!String.IsNullOrEmpty(Group))
				xml.AppendLine(@"<Group>" + Group.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Group>");
			foreach (var phrase in Phrases)
			{
				xml.AppendLine(@"<Phrase>" + phrase.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Phrase>");
			}
			return xml.ToString();
		}
	}
}
