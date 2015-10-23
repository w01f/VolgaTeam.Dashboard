using System;
using System.Text;
using System.Xml;

namespace Asa.Core.Common
{
	public class DateRange
	{
		public DateTime? StartDate { get; set; }
		public DateTime? FinishDate { get; set; }

		public string Range
		{
			get
			{
				if (!(StartDate.HasValue && FinishDate.HasValue)) return String.Empty;
				return String.Format("{0}-{1}", StartDate.Value.ToString("MM/dd/yy"), FinishDate.Value.ToString("MM/dd/yy"));
			}
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<DateRange>");
			if (StartDate.HasValue)
				result.AppendLine(@"<StartDate>" + StartDate.Value + @"</StartDate>");
			if (FinishDate.HasValue)
				result.AppendLine(@"<FinishDate>" + FinishDate.Value + @"</FinishDate>");
			result.AppendLine(@"</DateRange>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "StartDate":
						{
							DateTime temp;
							if (DateTime.TryParse(childNode.InnerText, out temp))
								StartDate = temp;
						}
						break;
					case "FinishDate":
						{
							DateTime temp;
							if (DateTime.TryParse(childNode.InnerText, out temp))
								FinishDate = temp;
						}
						break;
				}
		}
	}

}
