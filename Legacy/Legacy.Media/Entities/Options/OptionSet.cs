using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Asa.Legacy.Common.Entities.Common;
using Asa.Legacy.Media.Entities.Schedule;

namespace Asa.Legacy.Media.Entities.Options
{
	public class OptionSet
	{
		public Guid UniqueID { get; set; }
		public double Index { get; set; }
		public string Name { get; set; }
		public ImageSource Logo { get; set; }
		public string Comment { get; set; }
		public int? TotalPeriods { get; set; }
		public List<OptionProgram> Programs { get; private set; }

		public ContractSettings ContractSettings { get; private set; }

		#region Options
		public bool ShowLineId { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowStation { get; set; }
		public bool ShowProgram { get; set; }
		public bool ShowDay { get; set; }
		public bool ShowTime { get; set; }
		public bool ShowRate { get; set; }
		public bool ShowLenght { get; set; }
		public bool ShowSpots { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowSpotsX { get; set; }
		public bool UseDecimalRates { get; set; }

		public int PositionStation { get; set; }
		public int PositionProgram { get; set; }
		public int PositionDay { get; set; }
		public int PositionTime { get; set; }
		public int PositionRate { get; set; }
		public int PositionLenght { get; set; }
		public int PositionSpots { get; set; }
		public int PositionCost { get; set; }

		public bool ShowTotalSpots { get; set; }
		public bool ShowTotalCost { get; set; }
		public bool ShowAverageRate { get; set; }

		public SpotType SpotType { get; set; }
		#endregion

		public OptionSet(RegularSchedule parent)
		{
			UniqueID = Guid.NewGuid();
			TotalPeriods = 1;
			Programs = new List<OptionProgram>();
			ContractSettings = new ContractSettings();
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
					case "TotalPeriods":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								TotalPeriods = temp;
						}
						break;

					case "ShowLineId":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowLineId = temp;
						}
						break;
					case "ShowLogo":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowLogo = temp;
						}
						break;
					case "ShowStation":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowStation = temp;
						}
						break;
					case "ShowProgram":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowProgram = temp;
						}
						break;
					case "ShowDay":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowDay = temp;
						}
						break;
					case "ShowLenght":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowLenght = temp;
						}
						break;
					case "ShowTime":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTime = temp;
						}
						break;
					case "ShowRate":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowRate = temp;
						}
						break;
					case "ShowSpots":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowSpots = temp;
						}
						break;
					case "ShowCost":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowCost = temp;
						}
						break;
					case "ShowTotalSpots":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTotalSpots = temp;
						}
						break;
					case "ShowTotalCost":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTotalCost = temp;
						}
						break;
					case "ShowAverageRate":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowAverageRate = temp;
						}
						break;
					case "ShowSpotsX":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowSpotsX = temp;
						}
						break;
					case "UseDecimalRates":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								UseDecimalRates = temp;
						}
						break;

					case "PositionStation":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								PositionStation = temp;
						}
						break;
					case "PositionProgram":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								PositionProgram = temp;
						}
						break;
					case "PositionDay":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								PositionDay = temp;
						}
						break;
					case "PositionLenght":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								PositionLenght = temp;
						}
						break;
					case "PositionTime":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								PositionTime = temp;
						}
						break;
					case "PositionRate":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								PositionRate = temp;
						}
						break;
					case "PositionSpots":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								PositionSpots = temp;
						}
						break;
					case "PositionCost":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								PositionCost = temp;
						}
						break;

					case "SpotType":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								SpotType = (SpotType)temp;
						}
						break;

					case "Programs":
						foreach (XmlNode programNode in childNode.ChildNodes)
						{
							var program = new OptionProgram();
							program.Deserialize(programNode);
							Programs.Add(program);
						}
						break;

					case "ContractSettings":
						ContractSettings.Deserialize(childNode);
						break;
				}
		}
	}
}
