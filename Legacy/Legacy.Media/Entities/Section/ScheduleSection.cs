using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Xml;
using Asa.Legacy.Common.Entities.Common;
using Asa.Legacy.Media.Entities.Schedule;
using Asa.Legacy.Media.Entities.Summary;

namespace Asa.Legacy.Media.Entities.Section
{
	public abstract class ScheduleSection
	{
		public ProgramSchedule Parent { get; private set; }
		public Guid UniqueID { get; set; }
		public string Name { get; set; }
		public double Index { get; set; }
		public List<Program> Programs { get; set; }
		public SpotType SpotType { get; set; }

		public DataTable DataSource { get; private set; }

		public SectionSummary Summary { get; private set; }
		public ContractSettings ContractSettings { get; private set; }

		public event EventHandler<EventArgs> DataChanged;

		#region Options
		public bool ShowRate { get; set; }
		public bool ShowRating { get; set; }
		public bool ShowTime { get; set; }
		public bool ShowDay { get; set; }
		public bool ShowDaypart { get; set; }
		public bool ShowStation { get; set; }
		public bool ShowProgram { get; set; }
		public bool ShowLenght { get; set; }
		public bool ShowCPP { get; set; }
		public bool ShowGRP { get; set; }
		public bool ShowSpots { get; set; }
		public bool ShowEmptySpots { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowLogo { get; set; }

		public bool ShowTotalPeriods { get; set; }
		public bool ShowTotalSpots { get; set; }
		public bool ShowTotalGRP { get; set; }
		public bool ShowTotalCPP { get; set; }
		public bool ShowAverageRate { get; set; }
		public bool ShowTotalRate { get; set; }
		public bool ShowNetRate { get; set; }
		public bool ShowDiscount { get; set; }

		public bool OutputPerQuater { get; set; }
		public int? OutputMaxPeriods { get; set; }
		public bool OutputNoBrackets { get; set; }
		public bool UseDecimalRates { get; set; }
		public bool UseGenericDateColumns { get; set; }
		#endregion

		protected ScheduleSection(ProgramSchedule parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Index = parent.Sections.Any() ? parent.Sections.Max(s => s.Index) + 1 : 0;
			Programs = new List<Program>();
			Summary = new SectionSummary(this);
			ContractSettings = new ContractSettings();

			#region Options
			ShowTime = true;
			ShowDaypart = true;
			ShowDay = true;
			ShowStation = true;
			ShowProgram = true;
			ShowLenght = false;
			ShowSpots = true;
			ShowEmptySpots = false;
			ShowCost = true;
			ShowLogo = false;

			ShowTotalPeriods = true;
			ShowTotalSpots = true;
			ShowAverageRate = true;
			ShowTotalRate = true;
			ShowNetRate = false;
			ShowDiscount = false;
			#endregion
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

					case "ShowAverageRate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAverageRate = tempBool;
						break;
					case "ShowCPP":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCPP = tempBool;
						break;
					case "ShowCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCost = tempBool;
						break;
					case "ShowDay":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDay = tempBool;
						break;
					case "ShowDaypart":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDaypart = tempBool;
						break;
					case "ShowDiscount":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDiscount = tempBool;
						break;
					case "ShowGRP":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowGRP = tempBool;
						break;
					case "ShowLenght":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLenght = tempBool;
						break;
					case "ShowLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLogo = tempBool;
						break;
					case "ShowNetRate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowNetRate = tempBool;
						break;
					case "ShowRate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowRate = tempBool;
						break;
					case "ShowRating":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowRating = tempBool;
						break;
					case "ShowStation":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowStation = tempBool;
						break;
					case "ShowProgram":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowProgram = tempBool;
						break;
					case "ShowTime":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTime = tempBool;
						break;
					case "ShowSpots":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSpots = tempBool;
						break;
					case "ShowEmptySpots":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowEmptySpots = tempBool;
						break;
					case "ShowTotalCPP":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalCPP = tempBool;
						break;
					case "ShowTotalGRP":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalGRP = tempBool;
						break;
					case "ShowTotalPeriods":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalPeriods = tempBool;
						break;
					case "ShowTotalRate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalRate = tempBool;
						break;
					case "ShowTotalSpots":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalSpots = tempBool;
						break;
					case "OutputPerQuater":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							OutputPerQuater = tempBool;
						break;
					case "OutputMaxPeriods":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								OutputMaxPeriods = temp;
						}
						break;
					case "OutputNoBrackets":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							OutputNoBrackets = tempBool;
						break;
					case "UseDecimalRates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							UseDecimalRates = tempBool;
						break;
					case "UseGenericDateColumns":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							UseGenericDateColumns = tempBool;
						break;
					case "Programs":
						foreach (XmlNode programNode in childNode.ChildNodes)
						{
							var program = new Program(this);
							program.Deserialize(programNode);
							Programs.Add(program);
						}
						break;
					case "Summary":
						Summary.Deserialize(childNode);
						break;
					case "ContractSettings":
						ContractSettings.Deserialize(childNode);
						break;
				}
		}
	}
}
