using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.MediaSchedule.Controls.BusinessClasses
{
	public class OutputScheduleGridBased
	{
		private readonly ScheduleSection _parent;

		public OutputScheduleGridBased(ScheduleSection parent)
		{
			_parent = parent;
			Title = string.Empty;
			Advertiser = string.Empty;
			DecisionMaker = string.Empty;
			Programs = new List<OutputProgramGridBased>();
			TotalSpots = new List<OutputTotalSpot>();
			Totals = new Dictionary<string, string>();
			ReplacementsList = new Dictionary<string, string>();
		}

		public string Title { get; set; }
		public string Advertiser { get; set; }
		public string DecisionMaker { get; set; }
		public string Demo { get; set; }
		public string DigitalInfo { get; set; }
		public string Color { get; set; }
		public string Quarter { get; set; }

		public int ProgramsPerSlide { get; set; }
		public int SpotsPerSlide { get; set; }

		public string TotalCost { get; set; }
		public string TotalSpot { get; set; }
		public string TotalCPP { get; set; }
		public string TotalGRP { get; set; }

		public List<OutputProgramGridBased> Programs { get; set; }
		public List<OutputTotalSpot> TotalSpots { get; set; }
		public Dictionary<string, string> Totals { get; set; }
		public Dictionary<string, string> ReplacementsList { get; set; }

		public string FlightDates
		{
			get { return _parent.Parent.FlightDates; }
		}

		public string RtgHeaderTitle
		{
			get
			{
				string result = string.Empty;
				switch (_parent.Parent.DemoType)
				{
					case DemoType.Rtg:
						result = "Rtg";
						break;
					case DemoType.Imp:
						result = "(000s)";
						break;
				}
				return String.Format("{0}{1}", (!String.IsNullOrEmpty(_parent.Parent.Demo) ? String.Format("{0}{1}", _parent.Parent.Demo, (char)13) : String.Empty), result);
			}
		}

		public string CPPHeaderTitle
		{
			get
			{
				string result = string.Empty;
				switch (_parent.Parent.DemoType)
				{
					case DemoType.Rtg:
						result = "CPP";
						break;
					case DemoType.Imp:
						result = "CPM";
						break;
				}
				return result;
			}
		}

		public string GRPHeaderTitle
		{
			get
			{
				string result = string.Empty;
				switch (_parent.Parent.DemoType)
				{
					case DemoType.Rtg:
						result = "GRP";
						break;
					case DemoType.Imp:
						result = "IMPs";
						break;
				}
				return result;
			}
		}

		#region Show Options

		public bool ShowLength { get; set; }
		public bool ShowDay { get; set; }
		public bool ShowTime { get; set; }
		public bool ShowRating { get; set; }
		public bool ShowRates { get; set; }
		public bool ShowSpots { get; set; }
		public bool ShowTotalSpots { get; set; }
		public bool ShowGRP { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowCPP { get; set; }
		public bool ShowStation { get; set; }

		#endregion

		public void PopulateScheduleReplacementsList()
		{
			var key = string.Empty;
			var value = string.Empty;
			var temp = new List<string>();

			ReplacementsList.Clear();

			key = "Flightdates";
			value = FlightDates;
			if (!ReplacementsList.Keys.Contains(key))
				ReplacementsList.Add(key, value);
			key = "Advertiser  -  Decisionmaker";
			value = String.Format("{0}  -  {1}", Advertiser, DecisionMaker);
			if (!ReplacementsList.Keys.Contains(key))
				ReplacementsList.Add(key, value);
			if (!String.IsNullOrEmpty(Quarter))
			{
				key = "Program";
				value = String.Format("Program  ({0})", Quarter);
				if (!ReplacementsList.Keys.Contains(key))
					ReplacementsList.Add(key, value);
			}
			if (!ShowLength && SpotsPerSlide < 24)
			{
				key = "Length";
				value = "Delete Column";
				if (!ReplacementsList.Keys.Contains(key))
					ReplacementsList.Add(key, value);
			}
			if (!ShowDay && SpotsPerSlide < 20)
			{
				key = "Day";
				value = "Delete Column";
				if (!ReplacementsList.Keys.Contains(key))
					ReplacementsList.Add(key, value);
			}
			if (!ShowTime && SpotsPerSlide < 20)
			{
				key = "Time";
				value = "Delete Column";
				if (!ReplacementsList.Keys.Contains(key))
					ReplacementsList.Add(key, value);
			}
			if (!ShowRates)
			{
				key = "Rate";
				value = "Delete Column";
				if (!ReplacementsList.Keys.Contains(key))
					ReplacementsList.Add(key, value);
			}
			if (!ShowTotalSpots)
			{
				key = "Total     Spots";
				value = "Delete Column";
				if (!ReplacementsList.Keys.Contains(key))
					ReplacementsList.Add(key, value);
			}
			if (!ShowCost)
			{
				key = "Total    Cost";
				value = "Delete Column";
				if (!ReplacementsList.Keys.Contains(key))
					ReplacementsList.Add(key, value);
			}
			if (SpotsPerSlide < 16)
			{
				key = "Rtg";
				value = ShowRating ? RtgHeaderTitle : "Delete Column";
				if (!ReplacementsList.Keys.Contains(key))
					ReplacementsList.Add(key, value);
			}
			if (SpotsPerSlide < 14)
			{
				key = "Total    Points";
				value = ShowGRP ? GRPHeaderTitle : "Delete Column";
				if (!ReplacementsList.Keys.Contains(key))
					ReplacementsList.Add(key, value);
				key = "CPP";
				value = ShowCPP ? CPPHeaderTitle : "Delete Column";
				if (!ReplacementsList.Keys.Contains(key))
					ReplacementsList.Add(key, value);
			}
			key = "tspot";
			value = TotalSpot;
			if (!ReplacementsList.Keys.Contains(key))
				ReplacementsList.Add(key, value);
			key = "tcost";
			value = TotalCost;
			if (!ReplacementsList.Keys.Contains(key))
				ReplacementsList.Add(key, value);
			if (SpotsPerSlide < 14)
			{
				key = "totalpts";
				value = TotalGRP;
				if (!ReplacementsList.Keys.Contains(key))
					ReplacementsList.Add(key, value);
				key = "cppavg";
				value = TotalCPP;
				if (!ReplacementsList.Keys.Contains(key))
					ReplacementsList.Add(key, value);
			}

			var totalCount = Totals.Count;
			for (var i = 0; i < 3; i++)
			{
				var firsrtIndex = i * 2;
				var secondIndex = firsrtIndex + 1;
				key = String.Format("[info{0}] [info{1}]", firsrtIndex + 1, secondIndex + 1);

				temp.Clear();
				if (firsrtIndex < totalCount)
					temp.Add(String.Format("[{0} {1}]", Totals.ElementAt(firsrtIndex).Key, Totals.ElementAt(firsrtIndex).Value));
				if (secondIndex < totalCount)
					temp.Add(String.Format("[{0} {1}]", Totals.ElementAt(secondIndex).Key, Totals.ElementAt(secondIndex).Value));
				value = temp.Any() || (!String.IsNullOrEmpty(DigitalInfo) && i == 0) ?
					String.Format("{0}{1}",
					!String.IsNullOrEmpty(DigitalInfo) && i == 0 ? String.Format("{0}{1}", DigitalInfo, Environment.NewLine) : String.Empty,
						String.Join(" ", temp)) :
					"Delete Row";
				if (!ReplacementsList.Keys.Contains(key))
					ReplacementsList.Add(key, value);
			}

			var totalSpotsCount = TotalSpots.Count;
			for (var i = 0; i < SpotsPerSlide; i++)
			{
				key = string.Format("MO {0}", (i + 1).ToString("00"));
				if (i < totalSpotsCount && ShowSpots)
				{
					value = TotalSpots[i].Month + (char)13 + TotalSpots[i].Day;
					if (!ReplacementsList.Keys.Contains(key))
						ReplacementsList.Add(key, value);

					key = string.Format("t{0}", i + 1);
					value = TotalSpots[i].Value;
					if (!ReplacementsList.Keys.Contains(key))
						ReplacementsList.Add(key, value);
				}
				else
				{
					value = "Delete Column";
					if (!ReplacementsList.Keys.Contains(key))
						ReplacementsList.Add(key, value);
				}
				Application.DoEvents();
			}

			var progarmsCount = Programs.Count;
			for (var i = 0; i < ProgramsPerSlide; i++)
			{
				key = String.Format("[Station{0}] Program{0}", i + 1);
				if (i < progarmsCount)
				{
					var program = Programs[i];
					temp.Clear();
					if (ShowStation)
						temp.Add(String.Format("[{0}]", program.Station));
					temp.Add(program.Name);
					value = String.Join(" ", temp);
					if (!ReplacementsList.Keys.Contains(key))
						ReplacementsList.Add(key, value);

					key = String.Format("{0}", (i + 1).ToString("00"));
					value = program.LineID;
					if (!ReplacementsList.Keys.Contains(key))
						ReplacementsList.Add(key, value);

					if (SpotsPerSlide >= 24)
					{
						key = String.Format("len{0}     day{0}     time{0}     rtg{0}     pts{0}     cpp{0}", i + 1);
						temp.Clear();
						if (ShowLength)
							temp.Add(String.Format("{0}", program.Length));
						if (ShowDay)
							temp.Add(String.Format("{0}", program.Days));
						if (ShowTime)
							temp.Add(String.Format("{0}", program.Time));
						if (ShowRating)
							temp.Add(String.Format("{0}: {1}", RtgHeaderTitle.Replace(((char)13).ToString(), " "), program.Rating));
						if (ShowGRP)
							temp.Add(String.Format("{0}: {1}", GRPHeaderTitle, program.GRP));
						if (ShowCPP)
							temp.Add(String.Format("{0}: {1}", CPPHeaderTitle, program.CPP));
						value = String.Join("  ", temp);
						if (!ReplacementsList.Keys.Contains(key))
							ReplacementsList.Add(key, value);
					}
					if (SpotsPerSlide < 24)
					{
						key = String.Format("len{0}", (i + 1));
						value = ShowLength ? program.Length : String.Empty;
						if (!ReplacementsList.Keys.Contains(key))
							ReplacementsList.Add(key, value);

						if (SpotsPerSlide >= 20)
						{
							key = String.Format("day{0}     time{0}     rtg{0}     pts{0}     cpp{0}", i + 1);
							temp.Clear();
							if (ShowDay)
								temp.Add(String.Format("{0}", program.Days));
							if (ShowTime)
								temp.Add(String.Format("{0}", program.Time));
							if (ShowRating)
								temp.Add(String.Format("{0}: {1}", RtgHeaderTitle.Replace(((char)13).ToString(), " "), program.Rating));
							if (ShowGRP)
								temp.Add(String.Format("{0}: {1}", GRPHeaderTitle, program.GRP));
							if (ShowCPP)
								temp.Add(String.Format("{0}: {1}", CPPHeaderTitle, program.CPP));
							value = String.Join("  ", temp);
							if (!ReplacementsList.Keys.Contains(key))
								ReplacementsList.Add(key, value);
						}
					}
					if (SpotsPerSlide < 20)
					{
						key = String.Format("day{0}", (i + 1));
						value = ShowDay ? program.Days : String.Empty;
						if (!ReplacementsList.Keys.Contains(key))
							ReplacementsList.Add(key, value);
						key = String.Format("time{0}", (i + 1));
						value = ShowTime ? program.Time : String.Empty;
						if (!ReplacementsList.Keys.Contains(key))
							ReplacementsList.Add(key, value);

						if (SpotsPerSlide >= 16)
						{
							key = String.Format("rtg{0}     pts{0}     cpp{0}", i + 1);
							temp.Clear();
							if (ShowRating)
								temp.Add(String.Format("{0}: {1}", RtgHeaderTitle.Replace(((char)13).ToString(), " "), program.Rating));
							if (ShowGRP)
								temp.Add(String.Format("{0}: {1}", GRPHeaderTitle, program.GRP));
							if (ShowCPP)
								temp.Add(String.Format("{0}: {1}", CPPHeaderTitle, program.CPP));
							value = String.Join("  ", temp);
							if (!ReplacementsList.Keys.Contains(key))
								ReplacementsList.Add(key, value);
						}
					}
					if (SpotsPerSlide < 16)
					{
						key = String.Format("rtg{0}", (i + 1));
						value = ShowRating ? program.Rating : String.Empty;
						if (!ReplacementsList.Keys.Contains(key))
							ReplacementsList.Add(key, value);

						if (SpotsPerSlide >= 14)
						{
							key = String.Format("pts{0}     cpp{0}", i + 1);
							temp.Clear();
							if (ShowGRP)
								temp.Add(String.Format("{0}: {1}", GRPHeaderTitle, program.GRP));
							if (ShowCPP)
								temp.Add(String.Format("{0}: {1}", CPPHeaderTitle, program.CPP));
							value = String.Join("  ", temp);
							if (!ReplacementsList.Keys.Contains(key))
								ReplacementsList.Add(key, value);
						}
					}
					if (SpotsPerSlide < 14)
					{
						key = String.Format("pts{0}", (i + 1));
						value = ShowGRP ? program.GRP : String.Empty;
						if (!ReplacementsList.Keys.Contains(key))
							ReplacementsList.Add(key, value);
						key = String.Format("cpp{0}", (i + 1));
						value = ShowCPP ? program.CPP : String.Empty;
						if (!ReplacementsList.Keys.Contains(key))
							ReplacementsList.Add(key, value);
					}
					if (ShowRates)
					{
						key = String.Format("rt{0}", (i + 1));
						value = program.Rate;
						if (!ReplacementsList.Keys.Contains(key))
							ReplacementsList.Add(key, value);
					}
					if (ShowTotalSpots)
					{
						key = String.Format("sp{0}", (i + 1));
						value = program.TotalSpots;
						if (!ReplacementsList.Keys.Contains(key))
							ReplacementsList.Add(key, value);
					}
					if (ShowCost)
					{
						key = String.Format("cs{0}", (i + 1));
						value = program.TotalRate;
						if (!ReplacementsList.Keys.Contains(key))
							ReplacementsList.Add(key, value);
					}
					if (ShowSpots)
					{
						var spotsCount = program.Spots.Count;
						for (int j = 0; j < SpotsPerSlide; j++)
						{
							if (j < spotsCount)
							{
								var spotSuffix = "a";
								switch (j)
								{
									case 0:
										spotSuffix = "a";
										break;
									case 1:
										spotSuffix = "b";
										break;
									case 2:
										spotSuffix = "c";
										break;
									case 3:
										spotSuffix = "d";
										break;
									case 4:
										spotSuffix = "e";
										break;
									case 5:
										spotSuffix = "f";
										break;
									case 6:
										spotSuffix = "g";
										break;
									case 7:
										spotSuffix = "h";
										break;
									case 8:
										spotSuffix = "i";
										break;
									case 9:
										spotSuffix = "j";
										break;
									case 10:
										spotSuffix = "k";
										break;
									case 11:
										spotSuffix = "l";
										break;
									case 12:
										spotSuffix = "m";
										break;
									case 13:
										spotSuffix = "n";
										break;
									case 14:
										spotSuffix = "o";
										break;
									case 15:
										spotSuffix = "p";
										break;
									case 16:
										spotSuffix = "q";
										break;
									case 17:
										spotSuffix = "r";
										break;
									case 18:
										spotSuffix = "s";
										break;
									case 19:
										spotSuffix = "t";
										break;
									case 20:
										spotSuffix = "u";
										break;
									case 21:
										spotSuffix = "v";
										break;
									case 22:
										spotSuffix = "w";
										break;
									case 23:
										spotSuffix = "x";
										break;
									case 24:
										spotSuffix = "y";
										break;
									case 25:
										spotSuffix = "z";
										break;
								}
								key = string.Format("{0}{1}", (i + 1), spotSuffix);
								value = program.Spots[j];
								if (!ReplacementsList.Keys.Contains(key))
									ReplacementsList.Add(key, value);
								Application.DoEvents();
							}
							else
								break;
						}

					}
					Application.DoEvents();
				}
				else
				{
					value = "Delete Row";
					if (!ReplacementsList.Keys.Contains(key))
						ReplacementsList.Add(key, value);
				}
			}
		}
	}

	public class OutputProgramGridBased
	{
		public OutputProgramGridBased(OutputScheduleGridBased parent)
		{
			Parent = parent;
			Name = string.Empty;
			LineID = string.Empty;
			Station = string.Empty;
			Time = string.Empty;
			Days = string.Empty;
			Length = string.Empty;
			Rate = string.Empty;
			Rating = string.Empty;
			TotalRate = string.Empty;
			TotalSpots = string.Empty;
			CPP = string.Empty;
			GRP = string.Empty;
			Spots = new List<string>();
		}

		public OutputScheduleGridBased Parent { get; private set; }
		public string Name { get; set; }
		public string LineID { get; set; }
		public string Station { get; set; }
		public string Time { get; set; }
		public string Days { get; set; }
		public string Length { get; set; }
		public string Rate { get; set; }
		public string Rating { get; set; }
		public string TotalRate { get; set; }
		public string TotalSpots { get; set; }
		public string CPP { get; set; }
		public string GRP { get; set; }
		public List<string> Spots { get; set; }
	}

	public class OutputTotalSpot
	{
		public OutputTotalSpot()
		{
			Month = string.Empty;
			Day = string.Empty;
			Value = string.Empty;
		}

		public string Month { get; set; }
		public string Day { get; set; }
		public string Value { get; set; }

		public string HeaderTagText
		{
			get { return Month + (!string.IsNullOrEmpty(Day) ? (((char)13) + Day) : string.Empty); }
		}
	}

	public class SpotInterval
	{
		public int Start { get; set; }
		public int End { get; set; }
		public string Name { get; set; }
	}
}
