using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Entities.NonPersistent.Digital;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Entities.NonPersistent.Section.Summary;
using Asa.Business.Media.Entities.Persistent;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Interfaces;
using Asa.Common.Core.Objects.Images;
using Asa.Common.Core.Objects.Output;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Section.Content
{
	public abstract class ScheduleSection : IJsonCloneable<ScheduleSection>
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

		public ProgramScheduleContent Parent { get; set; }

		public Guid UniqueID { get; private set; }
		public string Name { get; set; }
		public double Index { get; set; }
		public List<Program> Programs { get; private set; }
		public SpotType SpotType { get; set; }

		public MediaDigitalInfo DigitalInfo { get; private set; }
		public SectionSummary Summary { get; private set; }
		public ContractSettings ContractSettings { get; private set; }

		[JsonIgnore]
		public DataTable DataSource { get; private set; }

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
		public bool CloneLineToTheEnd { get; set; }
		#endregion

		#region Calculated Properies
		public MediaSchedule ParentSchedule => Parent.Schedule;

		public MediaScheduleSettings ParentScheduleSettings => Parent.ScheduleSettings;

		public int DisplayIndex => (Int32)(Index + 1);

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
		public double TotalCPP => TotalGRP != 0 ? (TotalCost / (TotalGRP / (ParentScheduleSettings.DemoType == DemoType.Rtg ? 1 : 1000))) : 0;

		public double TotalGRP
		{
			get { return Programs.Count > 0 ? (Programs.Select(x => x.GRP).Sum()) : 0; }
		}
		public double AvgRate => TotalSpots != 0 ? (TotalCost / TotalSpots) : 0;

		public double TotalCost
		{
			get { return Programs.Count > 0 ? (Programs.Select(x => x.Cost).Sum()) : 0; }
		}
		public double NetRate => TotalCost - Discount;

		public double Discount => TotalCost * 0.15;

		public int TotalSpots
		{
			get { return Programs.Count > 0 ? Programs.Select(x => x.TotalSpots).Sum() : 0; }
		}
		#endregion

		[JsonConstructor]
		protected ScheduleSection() { }

		protected ScheduleSection(ProgramScheduleContent parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Index = parent.Sections.Any() ? parent.Sections.Max(s => s.Index) + 1 : 0;
			Programs = new List<Program>();
			DigitalInfo = new MediaDigitalInfo();
			Summary = new SectionSummary(this);
			ContractSettings = new ContractSettings();

			#region Options
			ShowRating = ParentScheduleSettings.UseDemo & !String.IsNullOrEmpty(ParentScheduleSettings.Demo);
			ShowTime = true;
			ShowDaypart = true;
			ShowDay = true;
			ShowStation = true;
			ShowProgram = true;
			ShowLenght = false;
			ShowCPP = ParentScheduleSettings.UseDemo & !String.IsNullOrEmpty(ParentScheduleSettings.Demo);
			ShowGRP = ParentScheduleSettings.UseDemo & !String.IsNullOrEmpty(ParentScheduleSettings.Demo);
			ShowSpots = true;
			ShowEmptySpots = false;
			ShowCost = true;
			ShowLogo = false;

			ShowTotalPeriods = true;
			ShowTotalSpots = true;
			ShowTotalGRP = ParentScheduleSettings.UseDemo & !String.IsNullOrEmpty(ParentScheduleSettings.Demo);
			ShowTotalCPP = ParentScheduleSettings.UseDemo & !String.IsNullOrEmpty(ParentScheduleSettings.Demo);
			ShowAverageRate = true;
			ShowTotalRate = true;
			ShowNetRate = false;
			ShowDiscount = false;
			#endregion
		}

		public void AfterCreate()
		{
			if (DigitalInfo == null)
				DigitalInfo = new MediaDigitalInfo();
			DigitalInfo.AfterCreate();

			if (Summary == null)
				Summary = new SectionSummary(this);
			Summary.AfterCreate();
		}

		public void AfterClone(ScheduleSection source, bool fullClone = true)
		{
			UniqueID = Guid.NewGuid();
			Programs.ForEach(p =>
			{
				p.AfterClone(source.Programs.First(sourceProgram => sourceProgram.UniqueID == p.UniqueID), fullClone);
				p.Parent = this;
			});
			Parent = source.Parent;
		}

		public void Dispose()
		{
			DataChanged = null;
			DataSource = null;

			DigitalInfo.Dispose();
			DigitalInfo = null;

			Summary.Dispose();
			Summary = null;

			ContractSettings = null;

			Programs.ForEach(p => p.Dispose());
			Programs.Clear();

			Parent = null;
		}

		public void GenerateDataSource()
		{
			DataSource?.Dispose();

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
					if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed && spotIndex == 0 && ParentScheduleSettings.FlightDateStart != ParentScheduleSettings.UserFlightDateStart)
					{
						isFullSpot = false;
						tooltip = String.Format("Partial Week Warning: {0}{2}The FIRST WEEK of your schedule is NOT a full 7 day week.{2}The Only Active Days in this week are {1}.", spot.Name, ParentScheduleSettings.StartWeekDays, Environment.NewLine);
					}
					else if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed && spotIndex == spotsCount - 1 && ParentScheduleSettings.FlightDateEnd != ParentScheduleSettings.UserFlightDateEnd)
					{
						isFullSpot = false;
						tooltip = String.Format("Partial Week Warning: {0}{2}The LAST WEEK of your schedule is NOT a full 7 day week.{2}The Only Active Days in this week are {1}.", spot.Name, ParentScheduleSettings.EndWeekDays, Environment.NewLine);
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
			string temp = string.Format("ISNULL({0},0) * {1} * {2}", ProgramRatingDataColumnName, ProgramTotalSpotDataColumnName, (ParentScheduleSettings.DemoType == DemoType.Rtg ? "1" : "1000"));
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
			temp = string.Format("IIF((Sum({1})/{2})<>0,Sum({0})/(Sum({1})/{2}),0)", ProgramCostDataColumnName, ProgramGRPDataColumnName, (ParentScheduleSettings.DemoType == DemoType.Rtg ? "1" : "1000"));
			column.Expression = temp;
			table.Columns.Add(column);

			#region Fill Programs Data

			foreach (var program in Programs)
			{
				var row = table.NewRow();
				row.BeginEdit();
				row[ProgramIndexDataColumnName] = (Int32)program.Index;
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
			var newProgram = program.Clone<Program, Program>(fullClone);
			Programs.Add(newProgram);
			if (CloneLineToTheEnd)
				newProgram.Index = Programs.Count;
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
			CloneLineToTheEnd = templateData.CloneLineToTheEnd;
		}
	}
}
