using System;
using System.Xml;
using Asa.Legacy.Common.Entities.Common;

namespace Asa.Legacy.Media.Entities.Options
{
	public class OptionProgram
	{
		public string Name { get; set; }
		public Guid UniqueID { get; set; }
		public decimal Index { get; set; }
		public string Station { get; set; }
		public ImageSource Logo { get; set; }
		public string Day { get; set; }
		public string Time { get; set; }
		public string Length { get; set; }
		public decimal? Rate { get; set; }
		public int? Spot { get; set; }

		public OptionProgram()
		{
			UniqueID = Guid.NewGuid();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "UniqueID":
						{
							Guid temp;
							if (Guid.TryParse(childNode.InnerText, out temp))
								UniqueID = temp;
						}
						break;
					case "Name":
						Name = childNode.InnerText;
						break;
					case "Station":
						Station = childNode.InnerText;
						break;
					case "Logo":
						Logo = new ImageSource();
						Logo.Deserialize(childNode);
						break;
					case "Day":
						Day = childNode.InnerText;
						break;
					case "Length":
						Length = childNode.InnerText;
						break;
					case "Time":
						Time = childNode.InnerText;
						break;
					case "Rate":
						{
							decimal temp;
							if (Decimal.TryParse(childNode.InnerText, out temp))
								Rate = temp;
						}
						break;
					case "Spot":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								Spot = temp;
						}
						break;
				}
		}
	}
}
