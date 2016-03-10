using System;
using System.Collections.Generic;
using System.Xml;
using Asa.Legacy.Common.Entities.Common;
using Asa.Legacy.Common.Entities.Summary;

namespace Asa.Legacy.Media.Entities.Section
{
	public class Program
	{
		private string _name;
		private string _day;

		#region Basic Properties

		public ScheduleSection Parent { get; set; }
		public Guid UniqueID { get; set; }
		public decimal Index { get; set; }
		public ImageSource Logo { get; set; }
		public string Station { get; set; }
		public string Daypart { get; set; }
		public string Time { get; set; }
		public string Length { get; set; }
		public double? Rate { get; set; }
		public double? Rating { get; set; }
		public List<Spot> Spots { get; set; }
		public CustomSummaryItem SummaryItem { get; private set; }

		#endregion

		#region Calculated Properties

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}

		public string Day
		{
			get { return _day; }
			set { _day = value; }
		}
		#endregion

		public Program(ScheduleSection parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Index = Parent.Programs.Count + 1;
			Daypart = string.Empty;
			Day = string.Empty;
			Time = string.Empty;
			Length = string.Empty;
			Spots = new List<Spot>();
			SummaryItem = new CustomSummaryItem();
		}

		public void Deserialize(XmlNode node)
		{
			double tempDouble;
			Guid tempGuid;

			foreach (XmlAttribute programAttribute in node.Attributes)
				switch (programAttribute.Name)
				{
					case "Name":
						_name = programAttribute.Value;
						break;
					case "UniqueID":
						if (Guid.TryParse(programAttribute.Value, out tempGuid))
							UniqueID = tempGuid;
						break;
					case "Station":
						Station = programAttribute.Value;
						break;
					case "Daypart":
						Daypart = programAttribute.Value;
						break;
					case "Day":
						Day = programAttribute.Value;
						break;
					case "Time":
						Time = programAttribute.Value;
						break;
					case "Length":
						Length = programAttribute.Value;
						break;
					case "Rate":
						if (double.TryParse(programAttribute.Value, out tempDouble))
							Rate = tempDouble;
						break;
					case "Rating":
						if (double.TryParse(programAttribute.Value, out tempDouble))
							Rating = tempDouble;
						break;
				}
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "Spots":
						foreach (XmlNode spotNode in childNode.ChildNodes)
						{
							var spot = new Spot(this);
							spot.Deserialize(spotNode);
							Spots.Add(spot);
						}
						break;
					case "SummaryItem":
						SummaryItem.Deserialize(childNode);
						break;
					case "Logo":
						Logo = new ImageSource();
						Logo.Deserialize(childNode);
						break;
				}
		}
	}
}
