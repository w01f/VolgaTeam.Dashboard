using System;
using System.Xml;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public class MediaMonthTemplate
	{
		public DateTime? Month { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Month":
						{
							DateTime tempDateTime;
							if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
								Month = tempDateTime;
						}
						break;
					case "StartDate":
						{
							DateTime tempDateTime;
							if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
								StartDate = tempDateTime;
						}
						break;
					case "EndDate":
						{
							DateTime tempDateTime;
							if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
								EndDate = tempDateTime;
						}
						break;
				}
			}
		}
	}
}
