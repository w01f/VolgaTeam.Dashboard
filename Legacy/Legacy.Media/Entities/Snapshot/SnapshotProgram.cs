using System;
using System.Xml;
using Asa.Legacy.Common.Entities.Common;

namespace Asa.Legacy.Media.Entities.Snapshot
{
	public class SnapshotProgram
	{
		public string Name { get; set; }
		public Guid UniqueID { get; set; }
		public decimal Index { get; set; }
		public string Station { get; set; }
		public ImageSource Logo { get; set; }
		public string Daypart { get; set; }
		public string Length { get; set; }
		public string Time { get; set; }
		public decimal? Rate { get; set; }

		public int? MondaySpot { get; set; }
		public int? TuesdaySpot { get; set; }
		public int? WednesdaySpot { get; set; }
		public int? ThursdaySpot { get; set; }
		public int? FridaySpot { get; set; }
		public int? SaturdaySpot { get; set; }
		public int? SundaySpot { get; set; }

		public SnapshotProgram()
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
					case "Daypart":
						Daypart = childNode.InnerText;
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
					case "MondaySpot":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								MondaySpot = temp;
						}
						break;
					case "TuesdaySpot":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								TuesdaySpot = temp;
						}
						break;
					case "WednesdaySpot":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								WednesdaySpot = temp;
						}
						break;
					case "ThursdaySpot":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								ThursdaySpot = temp;
						}
						break;
					case "FridaySpot":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								FridaySpot = temp;
						}
						break;
					case "SaturdaySpot":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								SaturdaySpot = temp;
						}
						break;
					case "SundaySpot":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								SundaySpot = temp;
						}
						break;
				}
		}
	}
}
