using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using Asa.Core.Common;
using Asa.Core.OnlineSchedule;

namespace Asa.Core.MediaSchedule
{
	public abstract class ProgramSchedule
	{
		public RegularSchedule Parent { get; private set; }
		public DateTime? SelectedQuarter { get; set; }
		public DigitalLegend DigitalLegend { get; set; }
		public bool ApplySettingsForAll { get; set; }
		public List<ScheduleSection> Sections { get; private set; }

		public abstract int TotalPeriods { get; }

		#region Calculated Properies
		public int TotalActivePeriods
		{
			get
			{
				var defaultSection = Sections.FirstOrDefault();
				return defaultSection != null ? defaultSection.TotalActivePeriods : 0;
			}
		}
		public double TotalCPP
		{
			get { return Sections.Sum(s => s.TotalCPP); }
		}
		public double TotalGRP
		{
			get { return Sections.Sum(s => s.TotalGRP); }
		}
		public double AvgRate
		{
			get { return TotalSpots != 0 ? (TotalCost / TotalSpots) : 0; }
		}
		public double TotalCost
		{
			get { return Sections.Sum(s => s.TotalCost); }
		}
		public double NetRate
		{
			get { return TotalCost - Discount; }
		}
		public double Discount
		{
			get { return TotalCost * 0.15; }
		}
		public int TotalSpots
		{
			get { return Sections.Sum(s => s.TotalSpots); }
		}
		#endregion

		protected ProgramSchedule(RegularSchedule parent)
		{
			Parent = parent;
			DigitalLegend = new DigitalLegend();
			Sections = new List<ScheduleSection>();
		}

		public abstract ScheduleSection CreateSection();

		public static ProgramSchedule Create(RegularSchedule parent)
		{
			switch (parent.SelectedSpotType)
			{
				case SpotType.Week:
					return new WeekSchedule(parent);
				case SpotType.Month:
					return new MonthSchedule(parent);
				default:
					return null;
			}
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			if (SelectedQuarter.HasValue)
				result.AppendLine(@"<SelectedQuarter>" + SelectedQuarter.Value + @"</SelectedQuarter>");
			result.AppendLine(@"<DigitalLegend>" + DigitalLegend.Serialize() + @"</DigitalLegend>");
			result.AppendLine(@"<ApplySettingsForAll>" + ApplySettingsForAll + @"</ApplySettingsForAll>");
			foreach (var section in Sections)
				result.AppendLine(String.Format("<Section>{0}</Section>", section.Serialize()));
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "SelectedQuarter":
						{
							DateTime temp;
							if (DateTime.TryParse(childNode.InnerText, out temp))
								SelectedQuarter = temp;
						}
						break;
					case "DigitalLegend":
						DigitalLegend.Deserialize(childNode);
						break;
					case "ApplySettingsForAll":
						{
							bool temp;
							if (bool.TryParse(childNode.InnerText, out temp))
								ApplySettingsForAll = temp;
						}
						break;
					case "Section":
						var section = CreateSection();
						section.Deserialize(childNode);
						Sections.Add(section);
						break;
				}
		}

		public void RebuildSpots()
		{
			Sections.ForEach(section => section.RebuildSpots());
		}

		public void ChangeSectionPosition(int position, int newPosition)
		{
			if (position < 0 || position >= Sections.Count) return;
			var section = Sections[position];
			section.Index = newPosition - 0.5;
			RebuildSectionIndexes();
		}

		public void RebuildSectionIndexes()
		{
			var i = 0;
			foreach (var snapshot in Sections.OrderBy(o => o.Index))
			{
				snapshot.Index = i;
				i++;
			}
			Sections.Sort((x, y) => x.Index.CompareTo(y.Index));
		}
	}

	public class WeekSchedule : ProgramSchedule
	{
		public override int TotalPeriods
		{
			get
			{
				var datesRange = Parent.FlightDateEnd - Parent.FlightDateStart;
				return datesRange.HasValue ? datesRange.Value.Days / 7 + 1 : 0;
			}
		}

		public override ScheduleSection CreateSection()
		{
			return new WeeklySection(this);
		}

		public WeekSchedule(RegularSchedule parent) : base(parent) { }
	}

	public class MonthSchedule : ProgramSchedule
	{
		public override int TotalPeriods
		{
			get
			{
				if (!Parent.FlightDateEnd.HasValue || !Parent.FlightDateStart.HasValue) return 0;
				return Math.Abs((Parent.FlightDateEnd.Value.Month - Parent.FlightDateStart.Value.Month) + 12 * (Parent.FlightDateEnd.Value.Year - Parent.FlightDateStart.Value.Year)) + 1;
			}
		}

		public MonthSchedule(RegularSchedule parent) : base(parent) { }

		public override ScheduleSection CreateSection()
		{
			return new MonthlySection(this);
		}
	}

	public abstract class ScheduleSection
	{
		public const string ProgramDataTableName = "Programs";
		public const string ProgramIndexDataColumnName = "Index";
		public const string ProgramLogoImageDataColumnName = "LogoImage";
		public const string ProgramLogoSourceDataColumnName = "LogoSource";
		public const string ProgramStationDataColumnName = "Station";
		public const string ProgramNameDataColumnName = "Name";
		public const string ProgramDayDataColumnName = "Day";
		public const string ProgramDaypartDataColumnName = "Daypart";
		public const string ProgramTimeDataColumnName = "Time";
		public const string ProgramLengthDataColumnName = "Length";
		public const string ProgramRateDataColumnName = "Rate";
		public const string ProgramRatingDataColumnName = "Rating";
		public const string ProgramCPPDataColumnName = "CPP";
		public const string ProgramGRPDataColumnName = "GRP";
		public const string ProgramSpotDataColumnNamePrefix = "Spot";
		public const string ProgramTotalSpotDataColumnName = "TotalSpts";
		public const string ProgramCostDataColumnName = "Cost";
		public const string ProgramTotalCPPDataColumnName = "TotalCPP";

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

		#region Calculated Properies
		public RegularSchedule ParentSchedule
		{
			get { return Parent.Parent; }
		}
		public int DisplayIndex
		{
			get { return (Int32)(Index + 1); }
		}
		public int TotalActivePeriods
		{
			get
			{
				var defaultprogram = Programs.FirstOrDefault();
				if (defaultprogram != null)
				{
					return ShowEmptySpots ? Parent.TotalPeriods : defaultprogram.SpotsNotEmpty.Length;
				}
				return 0;
			}
		}
		public double TotalCPP
		{
			get { return TotalGRP != 0 ? (TotalCost / (TotalGRP / (ParentSchedule.DemoType == DemoType.Rtg ? 1 : 1000))) : 0; }
		}
		public double TotalGRP
		{
			get { return Programs.Count > 0 ? (Programs.Select(x => x.GRP).Sum()) : 0; }
		}
		public double AvgRate
		{
			get { return TotalSpots != 0 ? (TotalCost / TotalSpots) : 0; }
		}
		public double TotalCost
		{
			get { return Programs.Count > 0 ? (Programs.Select(x => x.Cost).Sum()) : 0; }
		}
		public double NetRate
		{
			get { return TotalCost - Discount; }
		}
		public double Discount
		{
			get { return TotalCost * 0.15; }
		}
		public int TotalSpots
		{
			get { return Programs.Count > 0 ? Programs.Select(x => x.TotalSpots).Sum() : 0; }
		}
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
			ShowRating = ParentSchedule.UseDemo & !String.IsNullOrEmpty(ParentSchedule.Demo);
			ShowTime = true;
			ShowDaypart = true;
			ShowDay = true;
			ShowStation = true;
			ShowProgram = true;
			ShowLenght = false;
			ShowCPP = ParentSchedule.UseDemo & !String.IsNullOrEmpty(ParentSchedule.Demo);
			ShowGRP = ParentSchedule.UseDemo & !String.IsNullOrEmpty(ParentSchedule.Demo);
			ShowSpots = true;
			ShowEmptySpots = false;
			ShowCost = true;
			ShowLogo = false;

			ShowTotalPeriods = true;
			ShowTotalSpots = true;
			ShowTotalGRP = ParentSchedule.UseDemo & !String.IsNullOrEmpty(ParentSchedule.Demo);
			ShowTotalCPP = ParentSchedule.UseDemo & !String.IsNullOrEmpty(ParentSchedule.Demo);
			ShowAverageRate = true;
			ShowTotalRate = true;
			ShowNetRate = false;
			ShowDiscount = false;
			#endregion
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<UniqueID>" + UniqueID + @"</UniqueID>");
			result.AppendLine(@"<Index>" + Index + @"</Index>");
			if (!String.IsNullOrEmpty(Name))
				result.AppendLine(@"<Name>" + Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Name>");

			#region Options
			result.AppendLine(@"<ShowRate>" + ShowRate + @"</ShowRate>");
			result.AppendLine(@"<ShowRating>" + ShowRating + @"</ShowRating>");
			result.AppendLine(@"<ShowDay>" + ShowDay + @"</ShowDay>");
			result.AppendLine(@"<ShowTime>" + ShowTime + @"</ShowTime>");
			result.AppendLine(@"<ShowDaypart>" + ShowDaypart + @"</ShowDaypart>");
			result.AppendLine(@"<ShowStation>" + ShowStation + @"</ShowStation>");
			result.AppendLine(@"<ShowProgram>" + ShowProgram + @"</ShowProgram>");
			result.AppendLine(@"<ShowLenght>" + ShowLenght + @"</ShowLenght>");
			result.AppendLine(@"<ShowCPP>" + ShowCPP + @"</ShowCPP>");
			result.AppendLine(@"<ShowGRP>" + ShowGRP + @"</ShowGRP>");
			result.AppendLine(@"<ShowSpots>" + ShowSpots + @"</ShowSpots>");
			result.AppendLine(@"<ShowEmptySpots>" + ShowEmptySpots + @"</ShowEmptySpots>");
			result.AppendLine(@"<ShowCost>" + ShowCost + @"</ShowCost>");
			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			result.AppendLine(@"<ShowAverageRate>" + ShowAverageRate + @"</ShowAverageRate>");
			result.AppendLine(@"<ShowDiscount>" + ShowDiscount + @"</ShowDiscount>");
			result.AppendLine(@"<ShowNetRate>" + ShowNetRate + @"</ShowNetRate>");
			result.AppendLine(@"<ShowTotalCPP>" + ShowTotalCPP + @"</ShowTotalCPP>");
			result.AppendLine(@"<ShowTotalGRP>" + ShowTotalGRP + @"</ShowTotalGRP>");
			result.AppendLine(@"<ShowTotalPeriods>" + ShowTotalPeriods + @"</ShowTotalPeriods>");
			result.AppendLine(@"<ShowTotalRate>" + ShowTotalRate + @"</ShowTotalRate>");
			result.AppendLine(@"<ShowTotalSpots>" + ShowTotalSpots + @"</ShowTotalSpots>");
			result.AppendLine(@"<OutputPerQuater>" + OutputPerQuater + @"</OutputPerQuater>");
			if (OutputMaxPeriods.HasValue)
				result.AppendLine(@"<OutputMaxPeriods>" + OutputMaxPeriods.Value + @"</OutputMaxPeriods>");
			result.AppendLine(@"<OutputNoBrackets>" + OutputNoBrackets + @"</OutputNoBrackets>");
			result.AppendLine(@"<UseDecimalRates>" + UseDecimalRates + @"</UseDecimalRates>");
			result.AppendLine(@"<UseGenericDateColumns>" + UseGenericDateColumns + @"</UseGenericDateColumns>");
			#endregion

			result.AppendLine(@"<Programs>");
			foreach (var program in Programs)
				result.AppendLine(program.Serialize());
			result.AppendLine(@"</Programs>");

			result.AppendLine(String.Format("<Summary>{0}</Summary>", Summary.Serialize()));

			if (ContractSettings.IsConfigured)
				result.AppendLine(String.Format("<ContractSettings>{0}</ContractSettings>", ContractSettings.Serialize()));

			return result.ToString();
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

		public ScheduleSection Clone()
		{
			var newSection = Parent.CreateSection();
			var sectionSerialized = new XmlDocument();
			sectionSerialized.LoadXml(@"<Section>" + Serialize() + @"</Section>");
			newSection.Deserialize(sectionSerialized.SelectSingleNode("Section"));
			newSection.UniqueID = Guid.NewGuid();
			return newSection;
		}

		public void GenerateDataSource()
		{
			if (DataSource != null)
				DataSource.Dispose();


			#region Generate Programs Table

			DataSource = new DataTable(ProgramDataTableName);
			var table = DataSource;


			var column = new DataColumn(ProgramIndexDataColumnName, typeof(int));
			table.Columns.Add(column);
			column = new DataColumn(ProgramLogoImageDataColumnName, typeof(Image));
			table.Columns.Add(column);
			column = new DataColumn(ProgramLogoSourceDataColumnName, typeof(string));
			table.Columns.Add(column);
			column = new DataColumn(ProgramStationDataColumnName, typeof(string));
			table.Columns.Add(column);
			column = new DataColumn(ProgramNameDataColumnName, typeof(string));
			table.Columns.Add(column);
			column = new DataColumn(ProgramDayDataColumnName, typeof(string));
			table.Columns.Add(column);
			column = new DataColumn(ProgramDaypartDataColumnName, typeof(string));
			table.Columns.Add(column);
			column = new DataColumn(ProgramTimeDataColumnName, typeof(string));
			table.Columns.Add(column);
			column = new DataColumn(ProgramLengthDataColumnName, typeof(string));
			table.Columns.Add(column);
			column = new DataColumn(ProgramRateDataColumnName, typeof(double));
			table.Columns.Add(column);
			column = new DataColumn(ProgramRatingDataColumnName, typeof(double));
			table.Columns.Add(column);
			var totalSpotsExpression = new List<string>();

			if (Programs.Any())
			{
				var spotIndex = 0;
				var spots = Programs.First().Spots.ToList();
				var spotsCount = spots.Count;
				foreach (var spot in spots)
				{
					var columnName = ProgramSpotDataColumnNamePrefix + spotIndex;
					var tooltip = spot.FullColumnName;
					var isFullSpot = true;
					if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed && spotIndex == 0 && ParentSchedule.FlightDateStart != ParentSchedule.UserFlightDateStart)
					{
						isFullSpot = false;
						tooltip = String.Format("Partial Week Warning: {0}{2}The FIRST WEEK of your schedule is NOT a full 7 day week.{2}The Only Active Days in this week are {1}.", spot.Name, ParentSchedule.StartWeekDays, Environment.NewLine);
					}
					else if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed && spotIndex == spotsCount - 1 && ParentSchedule.FlightDateEnd != ParentSchedule.UserFlightDateEnd)
					{
						isFullSpot = false;
						tooltip = String.Format("Partial Week Warning: {0}{2}The LAST WEEK of your schedule is NOT a full 7 day week.{2}The Only Active Days in this week are {1}.", spot.Name, ParentSchedule.EndWeekDays, Environment.NewLine);
					}
					column = new DataColumn(columnName, typeof(int)) { Caption = spot.ColumnName };
					totalSpotsExpression.Add(string.Format("ISNULL({0},0)", columnName));
					column.ExtendedProperties.Add("Tooltip", tooltip);
					column.ExtendedProperties.Add("SpotSettings", new object[] { spot.Quarter, spot.FullColumnName, isFullSpot });
					table.Columns.Add(column);
					spotIndex++;
				}
			}

			column = new DataColumn(ProgramTotalSpotDataColumnName, typeof(int));
			if (totalSpotsExpression.Count > 0)
				column.Expression = string.Join(" + ", totalSpotsExpression.ToArray());
			table.Columns.Add(column);

			column = new DataColumn(ProgramGRPDataColumnName, typeof(double));
			string temp = string.Format("ISNULL({0},0) * {1} * {2}", ProgramRatingDataColumnName, ProgramTotalSpotDataColumnName, (ParentSchedule.DemoType == DemoType.Rtg ? "1" : "1000"));
			column.Expression = temp;
			table.Columns.Add(column);

			column = new DataColumn(ProgramCostDataColumnName, typeof(double));
			temp = string.Format("ISNULL({0},0) * {1}", ProgramRateDataColumnName, ProgramTotalSpotDataColumnName);
			column.Expression = temp;
			table.Columns.Add(column);

			column = new DataColumn(ProgramCPPDataColumnName, typeof(double));
			temp = string.Format("IIF(ISNULL({0},0) <> 0, (ISNULL({1},0)/ISNULL({0},0)), 0)", ProgramRatingDataColumnName, ProgramRateDataColumnName);
			column.Expression = temp;
			table.Columns.Add(column);

			column = new DataColumn(ProgramTotalCPPDataColumnName, typeof(double));
			temp = string.Format("IIF((Sum({1})/{2})<>0,Sum({0})/(Sum({1})/{2}),0)", ProgramCostDataColumnName, ProgramGRPDataColumnName, (ParentSchedule.DemoType == DemoType.Rtg ? "1" : "1000"));
			column.Expression = temp;
			table.Columns.Add(column);

			#region Fill Programs Data

			foreach (var program in Programs)
			{
				var row = table.NewRow();
				row.BeginEdit();
				row[ProgramIndexDataColumnName] = program.Index.ToString();
				if (program.Logo != null && program.Logo.ContainsData)
				{
					row[ProgramLogoImageDataColumnName] = program.SmallLogo;
					row[ProgramLogoSourceDataColumnName] = program.Logo.Serialize();
				}
				else
				{
					row[ProgramLogoImageDataColumnName] = DBNull.Value;
					row[ProgramLogoSourceDataColumnName] = DBNull.Value;
				}
				row[ProgramNameDataColumnName] = program.Name;
				row[ProgramStationDataColumnName] = program.Station;
				row[ProgramDayDataColumnName] = program.Day;
				row[ProgramDaypartDataColumnName] = program.Daypart;
				row[ProgramTimeDataColumnName] = program.Time;
				row[ProgramLengthDataColumnName] = program.Length;
				if (program.Rate.HasValue)
					row[ProgramRateDataColumnName] = program.Rate;
				else
					row[ProgramRateDataColumnName] = DBNull.Value;
				if (program.Rating.HasValue)
					row[ProgramRatingDataColumnName] = program.Rating;
				else
					row[ProgramRatingDataColumnName] = DBNull.Value;
				for (var i = 0; i < program.Spots.Count; i++)
				{
					if (program.Spots[i].Count.HasValue)
						row[ProgramSpotDataColumnNamePrefix + i] = program.Spots[i].Count;
					else
						row[ProgramSpotDataColumnNamePrefix + i] = DBNull.Value;
				}
				row.EndEdit();
				table.Rows.Add(row);
			}

			#endregion

			table.RowChanged += (sender, e) => UpdateProgramsFromDataSource(e.Row);
			#endregion
		}

		private void UpdateProgramsFromDataSource(DataRow row)
		{
			int tempInt;

			var index = -1;
			var temp = row[ProgramIndexDataColumnName] != DBNull.Value ? row[ProgramIndexDataColumnName].ToString() : string.Empty;
			if (int.TryParse(temp, out tempInt))
				index = tempInt;
			var program = Programs.FirstOrDefault(x => x.Index == index);
			if (program != null)
			{
				temp = row[ProgramLogoSourceDataColumnName] != DBNull.Value ? row[ProgramLogoSourceDataColumnName].ToString() : string.Empty;
				program.Logo = ImageSource.FromString(temp);
				temp = row[ProgramNameDataColumnName] != DBNull.Value ? row[ProgramNameDataColumnName].ToString() : string.Empty;
				program.Name = temp;
				temp = row[ProgramStationDataColumnName] != DBNull.Value ? row[ProgramStationDataColumnName].ToString() : string.Empty;
				program.Station = temp;
				temp = row[ProgramDayDataColumnName] != DBNull.Value ? row[ProgramDayDataColumnName].ToString() : string.Empty;
				program.Day = temp;
				temp = row[ProgramDaypartDataColumnName] != DBNull.Value ? row[ProgramDaypartDataColumnName].ToString() : string.Empty;
				program.Daypart = temp;
				temp = row[ProgramTimeDataColumnName] != DBNull.Value ? row[ProgramTimeDataColumnName].ToString() : string.Empty;
				program.Time = temp;
				temp = row[ProgramLengthDataColumnName] != DBNull.Value ? row[ProgramLengthDataColumnName].ToString() : string.Empty;
				program.Length = temp;
				temp = row[ProgramRateDataColumnName] != DBNull.Value ? row[ProgramRateDataColumnName].ToString() : string.Empty;
				double tempDouble;
				if (double.TryParse(temp, out tempDouble))
					program.Rate = tempDouble;
				else
					program.Rate = null;
				temp = row[ProgramRatingDataColumnName] != DBNull.Value ? row[ProgramRatingDataColumnName].ToString() : string.Empty;
				if (double.TryParse(temp, out tempDouble))
					program.Rating = tempDouble;
				else
					program.Rating = null;
				for (int i = 0; i < program.Spots.Count; i++)
				{
					temp = row[ProgramSpotDataColumnNamePrefix + i] != DBNull.Value ? row[ProgramSpotDataColumnNamePrefix + i].ToString() : string.Empty;
					if (int.TryParse(temp, out tempInt))
						program.Spots[i].Count = tempInt != 0 ? tempInt : (int?)null;
					else
						program.Spots[i].Count = null;
				}
			}

			if (DataChanged != null)
				DataChanged(null, new EventArgs());
		}

		public void AddProgram()
		{
			var program = new Program(this);
			Programs.Add(program);
			program.RebuildSpots();
		}

		public void DeleteProgram(int programIndex)
		{
			if (programIndex < 0 || programIndex >= Programs.Count) return;
			var program = Programs[programIndex];
			Programs.Remove(program);
			RebuildProgramIndexes();
		}

		public void CloneProgram(int programIndex, bool fullClone)
		{
			if (programIndex < 0 || programIndex >= Programs.Count) return;
			var program = Programs[programIndex];
			Programs.Add(program.Clone(fullClone));
			RebuildProgramIndexes();
		}

		public void ChangeProgramPosition(int programIndex, int newIndex)
		{
			if (programIndex < 0 || programIndex >= Programs.Count) return;
			var program = Programs[programIndex];
			program.Index = newIndex + 0.5m;
			RebuildProgramIndexes();
		}

		private void RebuildProgramIndexes()
		{
			Programs.Sort((x, y) => x.Index.CompareTo(y.Index));
			for (int i = 0; i < Programs.Count; i++)
				Programs[i].Index = i + 1;
		}

		public void RebuildSpots()
		{
			foreach (var program in Programs)
				program.RebuildSpots();
		}

		public void ApplyFromTemplate(ScheduleSection templateData)
		{
			ShowRating = templateData.ShowRating;
			ShowTime = templateData.ShowTime;
			ShowDaypart = templateData.ShowDaypart;
			ShowDay = templateData.ShowDay;
			ShowStation = templateData.ShowStation;
			ShowProgram = templateData.ShowProgram;
			ShowLenght = templateData.ShowLenght;
			ShowCPP = templateData.ShowCPP;
			ShowGRP = templateData.ShowGRP;
			ShowSpots = templateData.ShowSpots;
			ShowEmptySpots = templateData.ShowEmptySpots;
			ShowCost = templateData.ShowCost;
			ShowLogo = templateData.ShowLogo;

			ShowTotalPeriods = templateData.ShowTotalPeriods;
			ShowTotalSpots = templateData.ShowTotalSpots;
			ShowTotalGRP = templateData.ShowTotalGRP;
			ShowTotalCPP = templateData.ShowTotalCPP;
			ShowAverageRate = templateData.ShowAverageRate;
			ShowTotalRate = templateData.ShowTotalRate;
			ShowNetRate = templateData.ShowNetRate;
			ShowDiscount = templateData.ShowDiscount;

			ShowEmptySpots = templateData.ShowEmptySpots;
			OutputNoBrackets = templateData.OutputNoBrackets;
			UseGenericDateColumns = templateData.UseGenericDateColumns;
			UseDecimalRates = templateData.UseDecimalRates;
			OutputPerQuater = templateData.OutputPerQuater;
			OutputMaxPeriods = templateData.OutputMaxPeriods;
		}
	}

	public class WeeklySection : ScheduleSection
	{
		public WeeklySection(ProgramSchedule parent)
			: base(parent)
		{
			SpotType = SpotType.Week;

			ShowTime = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowTime;
			ShowDaypart = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowDaypart;
			ShowDay = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowDay;
			ShowStation = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowStation;
			ShowProgram = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowProgram;
			ShowLenght = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowLenght;
			ShowRate = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowRate;
			ShowSpots = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowSpots;
			ShowCost = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowCost;
			ShowLogo = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowLogo;

			ShowTotalPeriods = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowTotalPeriods;
			ShowTotalSpots = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowTotalSpots;
			ShowAverageRate = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowAverageRate;
			ShowTotalRate = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowTotalRate;
			ShowNetRate = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowNetRate;
			ShowDiscount = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowDiscount;

			OutputNoBrackets = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.OutputNoBrackets;
			UseDecimalRates = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.UseDecimalRates;
			UseGenericDateColumns = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.UseGenericDateColumns;
		}
	}

	public class MonthlySection : ScheduleSection
	{
		public MonthlySection(ProgramSchedule parent)
			: base(parent)
		{
			SpotType = SpotType.Month;

			ShowTime = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowTime;
			ShowDaypart = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowDaypart;
			ShowDay = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowDay;
			ShowStation = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowStation;
			ShowProgram = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowProgram;
			ShowLenght = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowLenght;
			ShowRate = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowRate;
			ShowSpots = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowSpots;
			ShowCost = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowCost;

			ShowTotalPeriods = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowTotalPeriods;
			ShowTotalSpots = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowTotalSpots;
			ShowAverageRate = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowAverageRate;
			ShowTotalRate = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowTotalRate;
			ShowNetRate = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowNetRate;
			ShowDiscount = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowDiscount;

			OutputNoBrackets = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.OutputNoBrackets;
			UseDecimalRates = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.UseDecimalRates;
			UseGenericDateColumns = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.UseGenericDateColumns;
		}
	}

	public class Program : ISummaryProduct
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
			set
			{
				string oldValue = _name;
				_name = value;
				if (string.IsNullOrEmpty(oldValue))
					ApplyDefaultValues();
			}
		}

		public string Day
		{
			get { return _day; }
			set
			{
				_day = value;
				_weekDays = null;
			}
		}

		public Image SmallLogo
		{
			get { return Logo != null ? Logo.TinyImage : null; }
		}

		public double CPP
		{
			get
			{
				double result = 0;
				if (Rate.HasValue && Rating.HasValue)
					if (Rating.Value != 0)
						result = Rate.Value / Rating.Value;
				return result;
			}
		}

		public double GRP
		{
			get { return (Rating.HasValue ? Rating.Value : 0) * TotalSpots * (Parent.ParentSchedule.DemoType == DemoType.Rtg ? 1 : 1000); }
		}

		public double Cost
		{
			get { return (Rate.HasValue ? Rate.Value : 0) * TotalSpots; }
		}

		public int TotalSpots
		{
			get { return Spots.Select(x => x.Count.HasValue ? x.Count.Value : 0).Sum(); }
		}

		public Spot[] SpotsNotEmpty
		{
			get { return Spots.Where(x => Parent.Programs.Select(z => z.Spots.FirstOrDefault(y => y.Date.Equals(x.Date))).Sum(w => w.Count) > 0).ToArray(); }
		}

		private IEnumerable<DayOfWeek> _weekDays;
		public IEnumerable<DayOfWeek> WeekDays
		{
			get
			{
				if (_weekDays != null) return _weekDays;
				var weekdays = new List<DayOfWeek>();
				if (String.IsNullOrEmpty(Day))
				{
					_weekDays = weekdays;
					return weekdays;
				}
				var regexp = new Regex("(?<=[A-Z])(?=[A-Z][a-z])|(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])");
				foreach (var weekPart in regexp.Split(Day))
				{
					if (new[] { "M", "Mo", "Mon", "Mn" }.Any(d => weekPart.Equals(d)))
						weekdays.Add(DayOfWeek.Monday);
					else if (new[] { "T", "Tu", "Tue", "Tues" }.Any(d => weekPart.Equals(d)))
						weekdays.Add(DayOfWeek.Tuesday);
					else if (new[] { "W", "We", "Wed", "Wd" }.Any(d => weekPart.Equals(d)))
						weekdays.Add(DayOfWeek.Wednesday);
					else if (new[] { "Th", "Thu", "Thur", "Tr", "Thurs" }.Any(d => weekPart.Equals(d)))
						weekdays.Add(DayOfWeek.Thursday);
					else if (new[] { "F", "Fr", "Fri" }.Any(d => weekPart.Equals(d)))
						weekdays.Add(DayOfWeek.Friday);
					else if (new[] { "Sa", "Sat", "St" }.Any(d => weekPart.Equals(d)))
						weekdays.Add(DayOfWeek.Saturday);
					else if (new[] { "Su", "Sun", "Sn" }.Any(d => weekPart.Equals(d)))
						weekdays.Add(DayOfWeek.Sunday);
				}
				_weekDays = weekdays;
				return _weekDays;
			}
		}

		#endregion

		public Program(ScheduleSection parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Index = Parent.Programs.Count + 1;
			Logo = MediaMetaData.Instance.ListManager.Images.Where(g => g.IsDefault).Select(g => g.Images.FirstOrDefault(i => i.IsDefault)).FirstOrDefault();
			Station = Parent.ParentSchedule.Stations.Count(x => x.Available) == 1 ? Parent.ParentSchedule.Stations.Where(x => x.Available).Select(x => x.Name).FirstOrDefault() : string.Empty;
			Daypart = string.Empty;
			Day = string.Empty;
			Time = string.Empty;
			Length = string.Empty;
			Spots = new List<Spot>();
			SummaryItem = new ProductSummaryItem(this);
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.Append(@"<Program ");
			result.Append("UniqueID = \"" + UniqueID + "\" ");
			if (!String.IsNullOrEmpty(_name))
				result.Append("Name = \"" + _name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			result.Append("Station = \"" + Station.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			result.Append("Daypart = \"" + Daypart.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			result.Append("Day = \"" + Day.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			result.Append("Time = \"" + Time.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			result.Append("Length = \"" + Length.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			if (Rate.HasValue)
				result.Append("Rate = \"" + Rate.Value + "\" ");
			if (Rating.HasValue)
				result.Append("Rating = \"" + Rating.Value + "\" ");
			result.AppendLine(@">");
			if (Logo != null && Logo.ContainsData && !Logo.IsDefault)
				result.AppendLine(@"<Logo>" + Logo.Serialize() + @"</Logo>");
			result.AppendLine(@"<Spots>");
			foreach (Spot spot in Spots)
				result.AppendLine(@"<Spot>" + spot.Serialize() + @"</Spot>");
			result.AppendLine(@"</Spots>");
			result.AppendLine(@"<SummaryItem>" + SummaryItem.Serialize() + @"</SummaryItem>");
			result.AppendLine(@"</Program>");
			return result.ToString();
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

		public void ApplyDefaultValues()
		{
			var source = MediaMetaData.Instance.ListManager.SourcePrograms.Where(x => x.Name.Equals(_name)).ToArray();
			if (source.Length <= 0) return;
			Daypart = source[0].Daypart;
			Day = source[0].Day;
			Time = source[0].Time;
		}

		public void RebuildSpots()
		{
			Spots.Clear();
			if (!Parent.ParentSchedule.FlightDateStart.HasValue || !Parent.ParentSchedule.FlightDateEnd.HasValue) return;
			var spotDate = Parent.ParentSchedule.FlightDateStart.Value;
			var endDate = Parent.ParentSchedule.FlightDateEnd.Value;
			while (spotDate < endDate)
			{
				var spot = new Spot(this) { Date = spotDate };
				spotDate = Parent.SpotType == SpotType.Week ? spotDate.AddDays(7) : new DateTime(spotDate.AddMonths(1).Year, spotDate.AddMonths(1).Month, 1);
				Spots.Add(spot);
			}
		}

		public Program Clone(bool fullClone)
		{
			var clone = new Program(Parent);
			clone.Name = Name;
			clone.Logo = Logo != null ? Logo.Clone() : null;
			clone.Station = Station;
			clone.Daypart = Daypart;
			clone.Day = Day;
			clone.Time = Time;
			clone.Length = Length;
			clone.Rate = Rate;
			clone.Rating = Rating;
			clone.Spots.AddRange(Spots.Select(s => s.Clone(clone, fullClone)));
			return clone;
		}

		public decimal SummaryOrder
		{
			get { return Index; }
		}

		public string SummaryTitle
		{
			get { return String.Format("{0}  -  {1}", Station, Name); }
		}

		public string SummaryInfo
		{
			get
			{
				var result = new List<string>();
				if (!String.IsNullOrEmpty(Daypart))
					result.Add(Daypart);
				if (!String.IsNullOrEmpty(Time))
					result.Add(Time);
				result.Add(String.Format("{0}x", Spots.Sum(sp => sp.Count)));
				return String.Join(", ", result);
			}
		}
	}

	public class SourceProgram
	{
		public SourceProgram()
		{
			Id = Guid.NewGuid().ToString();
			Name = string.Empty;
			Station = string.Empty;
			Daypart = string.Empty;
			Day = string.Empty;
			Time = string.Empty;
			Demos = new List<Demo>();
		}

		public string Id { get; set; }
		public string Name { get; set; }
		public string Station { get; set; }
		public string Daypart { get; set; }
		public string Day { get; set; }
		public string Time { get; set; }
		public List<Demo> Demos { get; set; }
	}

	public class Demo
	{
		public Demo()
		{
			Name = String.Empty;
			Source = String.Empty;
			Value = String.Empty;
		}

		public string Source { get; set; }
		public DemoType DemoType { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }

		public string DisplayString
		{
			get { return String.Format("{0} {1}", DemoType == DemoType.Rtg ? "Rtg" : "(000s)", Name); }
		}

		public override string ToString()
		{
			return DisplayString;
		}
	}

	public class Daypart : IConvertible
	{
		public string Name { get; set; }
		public string Code { get; set; }
		public bool Available { get; set; }

		public Daypart()
		{
			Name = string.Empty;
			Code = string.Empty;
			Available = true;
		}

		public override string ToString()
		{
			return Code;
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.Append(@"<Daypart ");
			result.Append("Name = \"" + Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			result.Append("Code = \"" + Code.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			result.Append("Available = \"" + Available + "\" ");
			result.AppendLine(@"/>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool;
			foreach (XmlAttribute attribute in node.Attributes)
				switch (attribute.Name)
				{
					case "Name":
						Name = attribute.Value;
						break;
					case "Code":
						Code = attribute.Value;
						break;
					case "Available":
						if (bool.TryParse(attribute.Value, out tempBool))
							Available = tempBool;
						break;
				}
		}

		#region IConvertible Memebers
		public TypeCode GetTypeCode()
		{
			return Type.GetTypeCode(GetType());
		}

		public bool ToBoolean(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public char ToChar(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public sbyte ToSByte(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public byte ToByte(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public short ToInt16(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public ushort ToUInt16(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public int ToInt32(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public uint ToUInt32(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public long ToInt64(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public ulong ToUInt64(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public float ToSingle(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public double ToDouble(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public decimal ToDecimal(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public DateTime ToDateTime(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public string ToString(IFormatProvider provider)
		{
			return ToString();
		}

		public object ToType(Type conversionType, IFormatProvider provider)
		{
			throw new NotImplementedException();
		}
		#endregion
	}

	public class Station : IConvertible
	{
		public string Name { get; set; }
		public Image Logo { get; set; }
		public bool Available { get; set; }

		public Station()
		{
			Name = string.Empty;
			Available = true;
		}

		public override string ToString()
		{
			return Name;
		}

		public string Serialize()
		{
			var converter = TypeDescriptor.GetConverter(typeof(Bitmap));
			var result = new StringBuilder();
			result.Append(@"<Station ");
			result.Append("Name = \"" + Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			result.Append("Logo = \"" + Convert.ToBase64String((byte[])converter.ConvertTo(Logo, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			result.Append("Available = \"" + Available + "\" ");
			result.AppendLine(@"/>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlAttribute attribute in node.Attributes)
				switch (attribute.Name)
				{
					case "Name":
						Name = attribute.Value;
						break;
					case "Logo":
						Logo = string.IsNullOrEmpty(attribute.Value) ? null : new Bitmap(new MemoryStream(Convert.FromBase64String(attribute.Value)));
						break;
					case "Available":
						bool tempBool;
						if (bool.TryParse(attribute.Value, out tempBool))
							Available = tempBool;
						break;
				}
		}

		#region IConvertible Memebers
		public TypeCode GetTypeCode()
		{
			return Type.GetTypeCode(GetType());
		}

		public bool ToBoolean(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public char ToChar(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public sbyte ToSByte(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public byte ToByte(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public short ToInt16(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public ushort ToUInt16(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public int ToInt32(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public uint ToUInt32(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public long ToInt64(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public ulong ToUInt64(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public float ToSingle(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public double ToDouble(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public decimal ToDecimal(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public DateTime ToDateTime(IFormatProvider provider)
		{
			throw new NotImplementedException();
		}

		public string ToString(IFormatProvider provider)
		{
			return ToString();
		}

		public object ToType(Type conversionType, IFormatProvider provider)
		{
			throw new NotImplementedException();
		}
		#endregion
	}

	public class Spot
	{
		private readonly Program _parent;

		public Spot(Program parent)
		{
			_parent = parent;
		}

		public DateTime Date { get; set; }
		public int? Count { get; set; }

		private IEnumerable<DateTime> DateRange
		{
			get
			{
				var dateRange = new List<DateTime>();
				foreach (var dayOfWeek in _parent.WeekDays)
				{
					var date = Date;
					while (date.DayOfWeek != dayOfWeek)
						date = date.AddDays(1);
					dateRange.Add(date);
				}
				dateRange.Sort();
				return dateRange;
			}
		}

		public DateTime? StartDate
		{
			get
			{
				return DateRange.FirstOrDefault();
			}
		}

		public DateTime? EndDate
		{
			get
			{
				return DateRange.LastOrDefault();
			}
		}

		public string Name
		{
			get
			{
				switch (_parent.Parent.SpotType)
				{
					case SpotType.Month:
						return Date.ToString("MMMM");
					case SpotType.Week:
						return String.Format("{0} {1}", GetMonthAbbreviation(Date.Month), Date.Day.ToString("00"));
					default:
						return String.Empty;
				}
			}
		}

		public string ColumnName
		{
			get
			{
				if (_parent.Parent.UseGenericDateColumns)
					return String.Format("{0}{2}{1}",
						_parent.Parent.SpotType == SpotType.Week ? "wk" : "mo",
						_parent.Spots.IndexOf(this) + 1,
						Environment.NewLine);
				switch (_parent.Parent.SpotType)
				{
					case SpotType.Month:
						return GetMonthAbbreviation(Date.Month);
					case SpotType.Week:
						return String.Format("{0}\n{1}", GetMonthAbbreviation(Date.Month), Date.Day.ToString("00"));
					default:
						return String.Empty;
				}
			}
		}

		public string FullColumnName
		{
			get
			{
				switch (_parent.Parent.SpotType)
				{
					case SpotType.Week:
						return String.Format("Week {0}", Name);
					default:
						return Name;
				}
			}
		}

		public TextGroup FormattedString
		{
			get
			{
				var textGroup = new TextGroup(", ", "[", "]");
				if (!Count.HasValue) return textGroup;

				var programNameGroup = new TextGroup("  -  ");
				programNameGroup.Items.Add(new TextItem(_parent.Station, true));
				programNameGroup.Items.Add(new TextItem(_parent.Name, false));
				textGroup.Items.Add(programNameGroup);

				if (!String.IsNullOrEmpty(_parent.Daypart))
					textGroup.Items.Add(new TextItem(_parent.Daypart, false));

				if (!String.IsNullOrEmpty(_parent.Time))
					textGroup.Items.Add(new TextItem(_parent.Time, false));

				textGroup.Items.Add(new TextItem(String.Format("{0}x {1}", Count.Value, _parent.Day), false));

				return textGroup;
			}
		}

		public Quarter Quarter
		{
			get { return _parent.Parent.ParentSchedule.Quarters.FirstOrDefault(q => q.DateStart <= Date && q.DateEnd >= Date); }
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<Date>" + Date + @"</Date>");
			if (Count.HasValue)
				result.AppendLine(@"<Count>" + Count.Value + @"</Count>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Date":
						DateTime tempDateTime;
						if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
							Date = tempDateTime;
						break;
					case "Count":
						int tempInt;
						if (int.TryParse(childNode.InnerText, out tempInt))
							Count = tempInt;
						break;
				}
			}
		}

		public static string GetMonthAbbreviation(int monthNumber)
		{
			var result = string.Empty;
			switch (monthNumber)
			{
				case 1:
					result = "JA";
					break;
				case 2:
					result = "FE";
					break;
				case 3:
					result = "MR";
					break;
				case 4:
					result = "AP";
					break;
				case 5:
					result = "MY";
					break;
				case 6:
					result = "JN";
					break;
				case 7:
					result = "JL";
					break;
				case 8:
					result = "AU";
					break;
				case 9:
					result = "SE";
					break;
				case 10:
					result = "OC";
					break;
				case 11:
					result = "NV";
					break;
				case 12:
					result = "DE";
					break;
			}
			return result;
		}

		public Spot Clone(Program parent, bool fullClone)
		{
			return new Spot(parent) { Date = Date, Count = fullClone ? Count : null };
		}
	}
}
