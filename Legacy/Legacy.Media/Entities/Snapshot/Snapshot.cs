using System;
using System.Collections.Generic;
using System.Xml;
using Asa.Legacy.Common.Entities.Common;
using Asa.Legacy.Media.Entities.Schedule;

namespace Asa.Legacy.Media.Entities.Snapshot
{
	public class Snapshot
	{
		public Guid UniqueID { get; set; }
		public double Index { get; set; }
		public string Name { get; set; }
		public ImageSource Logo { get; set; }
		public string Comment { get; set; }
		public int? TotalWeeks { get; set; }

		public List<SnapshotProgram> Programs { get; private set; }
		public List<DateRange> ActiveWeeks { get; private set; }

		#region Options
		public bool ShowLineId { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowStation { get; set; }
		public bool ShowProgram { get; set; }
		public bool ShowDaypart { get; set; }
		public bool ShowLenght { get; set; }
		public bool ShowTime { get; set; }
		public bool ShowRate { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowSpotsX { get; set; }
		public bool ShowTotalRow { get; set; }
		public bool UseDecimalRates { get; set; }
		public bool ShowSpotsPerWeek { get; set; }

		public bool ShowTotalSpots { get; set; }
		public bool ShowAverageRate { get; set; }
		#endregion

		public ContractSettings ContractSettings { get; private set; }

		public Snapshot(RegularSchedule parent)
		{
			UniqueID = Guid.NewGuid();
			Programs = new List<SnapshotProgram>();
			ActiveWeeks = new List<DateRange>();
			ContractSettings = new ContractSettings();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool;

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
					case "Index":
						{
							double temp;
							if (Double.TryParse(childNode.InnerText, out temp))
								Index = temp;
						}
						break;
					case "Name":
						Name = childNode.InnerText;
						break;
					case "Logo":
						Logo = new ImageSource();
						Logo.Deserialize(childNode);
						break;
					case "Comment":
						Comment = childNode.InnerText;
						break;
					case "TotalWeeks":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								TotalWeeks = temp;
						}
						break;

					case "ShowLineId":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLineId = tempBool;
						break;
					case "ShowLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLogo = tempBool;
						break;
					case "ShowStation":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowStation = tempBool;
						break;
					case "ShowProgram":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowProgram = tempBool;
						break;
					case "ShowDaypart":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDaypart = tempBool;
						break;
					case "ShowLenght":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLenght = tempBool;
						break;
					case "ShowTime":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTime = tempBool;
						break;
					case "ShowRate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowRate = tempBool;
						break;
					case "ShowCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCost = tempBool;
						break;
					case "ShowTotalSpots":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalSpots = tempBool;
						break;
					case "ShowAverageRate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAverageRate = tempBool;
						break;
					case "ShowSpotsX":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSpotsX = tempBool;
						break;
					case "ShowTotalRow":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalRow = tempBool;
						break;
					case "UseDecimalRates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							UseDecimalRates = tempBool;
						break;
					case "ShowSpotsPerWeek":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSpotsPerWeek = tempBool;
						break;
					case "Programs":
						foreach (XmlNode programNode in childNode.ChildNodes)
						{
							var program = new SnapshotProgram();
							program.Deserialize(programNode);
							Programs.Add(program);
						}
						break;
					case "ActiveWeeks":
						foreach (XmlNode dataRangeNode in childNode.ChildNodes)
						{
							var dateRange = new DateRange();
							dateRange.Deserialize(dataRangeNode);
							ActiveWeeks.Add(dateRange);
						}
						break;
					case "ContractSettings":
						ContractSettings.Deserialize(childNode);
						break;
				}
		}
	}
}
