using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TVScheduleBuilder.BusinessClasses
{
	class OutputManager
	{
		public static string ScheduleBuildersRootFolderPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\ScheduleBuilders", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
		private const string OneSheetsExcelBasedTemplatesFolderName = @"{0}\TV Slides\onesheets";
		private const string OneSheetsTableBasedTemplatesFolderName = @"{0}\TV Slides\tables";
		private const string OneSheetsTagsBasedTemplatesFolderName = @"{0}\TV Slides\tags";
		public const string OneSheetsExcelTemplatesFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\ExcelOutput{1}\TV Slides\{2} Programs\{3}";
		public const string OneSheetTableBasedTemplateFileName = @"{0} Programs\{0}-{1}.ppt";
		public const string OneSheetExcelBasedTemplateFileName = @"tvslide1.ppt";
		public const string OneSheetsExcelTemplateFileName = @"{0}.xls";
		public const string OneSheetsTagsBasedTemlateFileName = @"BlueYellow\{0}columns\{1}programs\{1}p-{2}w-{0}c.ppt";

		private static OutputManager _instance = new OutputManager();

		public static OutputManager Instance
		{
			get
			{
				return _instance;
			}
		}

		public string OneSheetExcelBasedTemplatesFolderPath
		{
			get
			{
				return Path.Combine(ScheduleBuildersRootFolderPath, string.Format(OneSheetsExcelBasedTemplatesFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
			}
		}

		public string OneSheetTableBasedTemplatesFolderPath
		{
			get
			{
				return Path.Combine(ScheduleBuildersRootFolderPath, string.Format(OneSheetsTableBasedTemplatesFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
			}
		}

		public string OneSheetTagsBasedTemplatesFolderPath
		{
			get
			{
				return Path.Combine(ScheduleBuildersRootFolderPath, string.Format(OneSheetsTagsBasedTemplatesFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
			}
		}

		private OutputManager()
		{
		}
	}

	public class OutputScheduleGridBased
	{
		private ScheduleSection _parent = null;
		public string Title { get; set; }
		public string Advertiser { get; set; }
		public string DecisionMaker { get; set; }
		public string Demo { get; set; }

		public int ProgramsPerSlide { get; set; }
		public int SpotsPerSlide { get; set; }

		public string TotalCost { get; set; }
		public string TotalSpot { get; set; }
		public string TotalCPP { get; set; }
		public string TotalGRP { get; set; }

		#region Show Options
		public bool ShowRates { get; set; }
		public bool ShowRating { get; set; }
		public bool ShowCPP { get; set; }
		public bool ShowGRP { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowSpots { get; set; }
		public bool ShowStation { get; set; }
		public bool ShowDaypart { get; set; }
		public bool ShowDay { get; set; }
		public bool ShowTime { get; set; }
		public bool ShowLength { get; set; }
		public bool ShowTotalSpots { get; set; }
		public bool ShowTotalInvestment { get; set; }
		public bool ShowDiscount { get; set; }
		public bool ShowNetCost { get; set; }
		#endregion

		public List<OutputProgramGridBased> Programs { get; set; }
		public List<OutputTotalSpot> TotalSpots { get; set; }
		public Dictionary<string, string> Totals { get; set; }
		public Dictionary<string, string> ReplacementsList { get; set; }

		public string FlightDates
		{
			get
			{
				return _parent.Parent.FlightDates;
			}
		}

		public string RtgHeaderTitle
		{
			get
			{
				string result = string.Empty;
				if (_parent.Parent.RatingAsCPP)
					result = "Rtg";
				else
					result = "000s";
				return result;
			}
		}

		public string CPPHeaderTitle
		{
			get
			{
				string result = string.Empty;
				if (_parent.Parent.RatingAsCPP)
					result = "CPP";
				else
					result = "CPM";
				return result;
			}
		}

		public string GRPHeaderTitle
		{
			get
			{
				string result = string.Empty;
				if (_parent.Parent.RatingAsCPP)
					result = "GRP";
				else
					result = "IMPs";
				return result;
			}
		}

		public string ExcelTemplatesSubFolderName
		{
			get
			{
				string result = string.Empty;
				if (this.ShowRates && this.ShowRating)
					result = "Rates and Ratings";
				else if (this.ShowRates || this.ShowRating)
					result = "Rates or Ratings";
				else
					result = "No Rates No Ratings";
				return result;
			}
		}

		public string ExcelTemplateFileName
		{
			get
			{
				string result = string.Empty;
				if (this.ShowCPP && this.ShowGRP && this.ShowCost)
					result = "1_GRP&TOTAL&CPP";
				else if ((this.ShowCPP && this.ShowGRP) || (this.ShowCost && this.ShowCPP) || (this.ShowCost && this.ShowGRP))
					result = "2_GRP&TOTALorGRP&CPP";
				else if (this.ShowCPP || this.ShowGRP || this.ShowCost)
					result = "3_GRPorTOTALorCPP";
				else
					result = "4_NoGRP&NoTOTAL&NoCPP";
				return result;
			}
		}

		public OutputScheduleGridBased(ScheduleSection parent)
		{
			_parent = parent;
			this.Title = string.Empty;
			this.Advertiser = string.Empty;
			this.DecisionMaker = string.Empty;
			this.Programs = new List<OutputProgramGridBased>();
			this.TotalSpots = new List<OutputTotalSpot>();
			this.Totals = new Dictionary<string, string>();
			this.ReplacementsList = new Dictionary<string, string>();
		}

		public void PopulateScheduleReplacementsList()
		{
			string key = string.Empty;
			string value = string.Empty;
			List<string> temp = new List<string>();
			this.ReplacementsList.Clear();


			for (int i = 0; i < this.SpotsPerSlide; i++)
			{
				key = string.Format("MO {0}", (i + 1).ToString("00"));
				value = this.TotalSpots[i].Month + (char)13 + this.TotalSpots[i].Day;
				if (!this.ReplacementsList.Keys.Contains(key))
					this.ReplacementsList.Add(key, value);
				key = string.Format("MO  {0}", (i + 1).ToString("00"));
				value = this.TotalSpots[i].Month + (char)13 + this.TotalSpots[i].Day;
				if (!this.ReplacementsList.Keys.Contains(key))
					this.ReplacementsList.Add(key, value);
				key = string.Format("M0  {0}", (i + 1).ToString("00"));
				value = this.TotalSpots[i].Month + (char)13 + this.TotalSpots[i].Day;
				if (!this.ReplacementsList.Keys.Contains(key))
					this.ReplacementsList.Add(key, value);

				key = string.Format("t{0}", new object[] { (i + 1).ToString() });
				value = this.TotalSpots[i].Value;
				if (!this.ReplacementsList.Keys.Contains(key))
					this.ReplacementsList.Add(key, value);

				Application.DoEvents();
			}
			for (int i = 0; i < this.ProgramsPerSlide; i++)
			{
				key = string.Format("Program{0}", (i + 1).ToString());
				value = this.Programs[i].Name;
				if (!this.ReplacementsList.Keys.Contains(key))
					this.ReplacementsList.Add(key, value);

				key = string.Format("Station{0}-dt{0}-Length{0}", (i + 1).ToString());
				temp.Clear();
				if (this.ShowStation)
					temp.Add(this.Programs[i].Station + "   ");
				if (this.ShowDay)
					temp.Add(this.Programs[i].Days);
				if (this.ShowDay && this.ShowTime)
					temp.Add("-");
				if (this.ShowTime)
					temp.Add(this.Programs[i].Time);
				if (this.ShowLength)
					temp.Add("    " + this.Programs[i].Length);
				value = string.Join("", temp.ToArray());
				if (!this.ReplacementsList.Keys.Contains(key))
					this.ReplacementsList.Add(key, value);

				key = string.Format("Rate_{0}", (i + 1).ToString());
				value = this.Programs[i].Rate;
				if (!this.ReplacementsList.Keys.Contains(key))
					this.ReplacementsList.Add(key, value);

				if (this.ShowCost && this.ShowSpots)
				{
					key = string.Format("Cost{0}", (i + 1).ToString());
					value = this.Programs[i].TotalRate;
					if (!this.ReplacementsList.Keys.Contains(key))
						this.ReplacementsList.Add(key, value);

					key = string.Format("Spots{0}", (i + 1).ToString());
					value = this.Programs[i].TotalSpots;
					if (!this.ReplacementsList.Keys.Contains(key))
						this.ReplacementsList.Add(key, value);

					key = "tspots";
					value = _parent.TotalSpots.ToString("#,##0");
					if (!this.ReplacementsList.Keys.Contains(key))
						this.ReplacementsList.Add(key, value);

					key = "tcost";
					value = _parent.TotalCost.ToString("$#,##0");
					if (!this.ReplacementsList.Keys.Contains(key))
						this.ReplacementsList.Add(key, value);
				}
				else if (this.ShowSpots)
				{
					key = "SpotsCost";
					value = "Total Spots";
					if (!this.ReplacementsList.Keys.Contains(key))
						this.ReplacementsList.Add(key, value);

					key = "tspotscost";
					value = _parent.TotalSpots.ToString("#,##0");
					if (!this.ReplacementsList.Keys.Contains(key))
						this.ReplacementsList.Add(key, value);

					key = string.Format("SpotsCost{0}", (i + 1).ToString());
					value = this.Programs[i].TotalSpots;
					if (!this.ReplacementsList.Keys.Contains(key))
						this.ReplacementsList.Add(key, value);
				}
				else if (this.ShowCost)
				{
					key = "SpotsCost";
					value = "Total Cost";
					if (!this.ReplacementsList.Keys.Contains(key))
						this.ReplacementsList.Add(key, value);

					key = "tspotscost";
					value = _parent.TotalCost.ToString("$#,##0");
					if (!this.ReplacementsList.Keys.Contains(key))
						this.ReplacementsList.Add(key, value);

					key = string.Format("SpotsCost{0}", (i + 1).ToString());
					value = this.Programs[i].TotalRate;
					if (!this.ReplacementsList.Keys.Contains(key))
						this.ReplacementsList.Add(key, value);
				}
				string spotPrefix = "a";
				switch (i)
				{
					case 0:
						spotPrefix = "a";
						break;
					case 1:
						spotPrefix = "b";
						break;
					case 2:
						spotPrefix = "c";
						break;
					case 3:
						spotPrefix = "d";
						break;
					case 4:
						spotPrefix = "e";
						break;
					case 5:
						spotPrefix = "f";
						break;
					case 6:
						spotPrefix = "g";
						break;
					case 7:
						spotPrefix = "h";
						break;
					case 8:
						spotPrefix = "i";
						break;
					case 9:
						spotPrefix = "j";
						break;
					case 10:
						spotPrefix = "k";
						break;
					case 11:
						spotPrefix = "l";
						break;
					case 12:
						spotPrefix = "m";
						break;
					case 13:
						spotPrefix = "n";
						break;
					case 14:
						spotPrefix = "o";
						break;
					case 15:
						spotPrefix = "p";
						break;
					case 16:
						spotPrefix = "q";
						break;
					case 17:
						spotPrefix = "r";
						break;
					case 18:
						spotPrefix = "s";
						break;
					case 19:
						spotPrefix = "t";
						break;
					case 20:
						spotPrefix = "u";
						break;
					case 21:
						spotPrefix = "v";
						break;
					case 22:
						spotPrefix = "w";
						break;
					case 23:
						spotPrefix = "x";
						break;
					case 24:
						spotPrefix = "y";
						break;
					case 25:
						spotPrefix = "z";
						break;
				}
				for (int j = 0; j < this.SpotsPerSlide; j++)
				{
					key = string.Format("{0}{1}", new object[] { spotPrefix, (j + 1).ToString() });
					value = this.Programs[i].Spots[j];
					if (!this.ReplacementsList.Keys.Contains(key))
						this.ReplacementsList.Add(key, value);
					Application.DoEvents();
				}

				if (!string.IsNullOrEmpty(_parent.Parent.Demo))
				{
					string demoSuffix = "a";
					key = string.Format("RTG{0}{1}", new object[] { (i + 1).ToString(), demoSuffix });
					value = this.Programs[i].DemoValue1;
					if (!this.ReplacementsList.Keys.Contains(key))
						this.ReplacementsList.Add(key, value);

					key = string.Format("GRP{0}{1}", new object[] { (i + 1).ToString(), demoSuffix });
					value = this.Programs[i].DemoValue2;
					if (!this.ReplacementsList.Keys.Contains(key))
						this.ReplacementsList.Add(key, value);

					key = string.Format("CPP{0}{1}", new object[] { (i + 1).ToString(), demoSuffix });
					value = this.Programs[i].DemoValue3;
					if (!this.ReplacementsList.Keys.Contains(key))
						this.ReplacementsList.Add(key, value);
				}
				Application.DoEvents();
			}
		}

		public int GetGridBasedSlideNumber()
		{
			int result = 0;
			BusinessClasses.OutputProgramGridBased program = this.Programs.FirstOrDefault();
			if (program != null)
			{
				if (this.ShowRates)
				{
					if (string.IsNullOrEmpty(this.Demo))
					{
						if (this.ShowSpots && this.ShowCost)
							result = 1;
						else if (this.ShowSpots || this.ShowCost)
							result = 2;
						else
							result = 3;
					}
					else
					{
						if (this.ShowSpots && this.ShowCost)
							result = 4;
						else if (this.ShowSpots || this.ShowCost)
							result = 5;
						else
							result = 6;
					}
				}
				else
				{
					if (string.IsNullOrEmpty(this.Demo))
					{
						if (this.ShowSpots && this.ShowCost)
							result = 7;
						else if (this.ShowSpots || this.ShowCost)
							result = 8;
						else
							result = 9;
					}
					else
					{
						if (this.ShowSpots && this.ShowCost)
							result = 10;
						else if (this.ShowSpots || this.ShowCost)
							result = 11;
						else
							result = 12;
					}
				}
			}
			return result;
		}
	}

	public class OutputScheduleTagsBased
	{
		private ScheduleSection _parent = null;
		public string Advertiser { get; set; }
		public string DecisionMaker { get; set; }

		public int ProgramsPerSlide { get; set; }
		public int SpotsPerSlide { get; set; }
		public int ColumnsPerSlide { get; set; }

		public string TotalCost { get; set; }
		public string TotalSpot { get; set; }
		public string TotalCPP { get; set; }
		public string TotalGRP { get; set; }

		#region Show Options
		public bool ShowRates { get; set; }
		public bool ShowRating { get; set; }
		public bool ShowCPP { get; set; }
		public bool ShowGRP { get; set; }
		public bool ShowTotalCost { get; set; }
		public bool ShowWeeks { get; set; }

		public bool ShowTotalSpots { get; set; }
		public bool ShowTotalInvestment { get; set; }
		public bool ShowDiscount { get; set; }
		public bool ShowNetCost { get; set; }
		#endregion

		public List<OutputProgramTagsBased> Programs { get; set; }
		public List<OutputTotalSpot> TotalSpots { get; set; }

		public string FlightDates
		{
			get
			{
				return _parent.Parent.FlightDates;
			}
		}

		public string RtgHeaderTitle
		{
			get
			{
				string result = _parent.Parent.RatingAsCPP ? "Rtg" : "000s";
				return result;
			}
		}

		public string CPPHeaderTitle
		{
			get
			{
				string result = _parent.Parent.RatingAsCPP ? "CPP" : "CPM";
				return result;
			}
		}

		public string GRPHeaderTitle
		{
			get
			{
				string result = _parent.Parent.RatingAsCPP ? "GRP" : "IMPs";
				return result;
			}
		}

		public string Item4Title
		{
			get
			{
				if (!string.IsNullOrEmpty(this.TotalSpot) && !string.IsNullOrEmpty(this.TotalGRP) && !string.IsNullOrEmpty(this.TotalCost) && !string.IsNullOrEmpty(this.TotalCPP))
					return this.CPPHeaderTitle;
				else
					return string.Empty;
			}
		}

		public string Item3Title
		{
			get
			{
				if (!string.IsNullOrEmpty(this.TotalCost) && !string.IsNullOrEmpty(this.TotalGRP) && !string.IsNullOrEmpty(this.TotalSpot))
					return "Total$";
				else if (!string.IsNullOrEmpty(this.TotalCPP) && (!string.IsNullOrEmpty(this.TotalSpot) | !string.IsNullOrEmpty(this.TotalGRP)) && ((!string.IsNullOrEmpty(this.TotalCost) & !string.IsNullOrEmpty(this.TotalSpot)) | !string.IsNullOrEmpty(this.TotalGRP)))
					return this.CPPHeaderTitle;
				else
					return string.Empty;
			}
		}

		public string Item2Title
		{
			get
			{
				if (!string.IsNullOrEmpty(this.TotalSpot) && !string.IsNullOrEmpty(this.TotalGRP))
					return this.GRPHeaderTitle;
				else if (!string.IsNullOrEmpty(this.TotalCost) && (!string.IsNullOrEmpty(this.TotalGRP) | !string.IsNullOrEmpty(this.TotalSpot)))
					return "Total$";
				else if (!string.IsNullOrEmpty(this.TotalCPP) && (!string.IsNullOrEmpty(this.TotalSpot) | !string.IsNullOrEmpty(this.TotalCost) | !string.IsNullOrEmpty(this.TotalGRP)))
					return this.CPPHeaderTitle;
				else
					return string.Empty;
			}
		}

		public string Item1Title
		{
			get
			{
				if (!string.IsNullOrEmpty(this.TotalSpot))
					return "Spots";
				else if (!string.IsNullOrEmpty(this.TotalGRP))
					return this.GRPHeaderTitle;
				else if (!string.IsNullOrEmpty(this.TotalCost))
					return "Total$";
				else if (!string.IsNullOrEmpty(this.TotalCPP))
					return this.CPPHeaderTitle;
				else
					return string.Empty;
			}
		}

		public string Item4Value
		{
			get
			{
				if (!string.IsNullOrEmpty(this.TotalSpot) && !string.IsNullOrEmpty(this.TotalGRP) && !string.IsNullOrEmpty(this.TotalCost) && !string.IsNullOrEmpty(this.TotalCPP))
					return this.TotalCPP;
				else
					return string.Empty;
			}
		}

		public string Item3Value
		{
			get
			{
				if (!string.IsNullOrEmpty(this.TotalCost) && !string.IsNullOrEmpty(this.TotalGRP) && !string.IsNullOrEmpty(this.TotalSpot))
					return this.TotalCost;
				else if (!string.IsNullOrEmpty(this.TotalCPP) && (!string.IsNullOrEmpty(this.TotalSpot) | !string.IsNullOrEmpty(this.TotalGRP)) && ((!string.IsNullOrEmpty(this.TotalCost) & !string.IsNullOrEmpty(this.TotalSpot)) | !string.IsNullOrEmpty(this.TotalGRP)))
					return this.TotalCPP;
				else
					return string.Empty;
			}
		}

		public string Item2Value
		{
			get
			{
				if (!string.IsNullOrEmpty(this.TotalSpot) && !string.IsNullOrEmpty(this.TotalGRP))
					return this.TotalGRP;
				else if (!string.IsNullOrEmpty(this.TotalCost) && (!string.IsNullOrEmpty(this.TotalGRP) | !string.IsNullOrEmpty(this.TotalSpot)))
					return this.TotalCost;
				else if (!string.IsNullOrEmpty(this.TotalCPP) && (!string.IsNullOrEmpty(this.TotalSpot) | !string.IsNullOrEmpty(this.TotalCost) | !string.IsNullOrEmpty(this.TotalGRP)))
					return this.TotalCPP;
				else
					return string.Empty;
			}
		}

		public string Item1Value
		{
			get
			{
				if (!string.IsNullOrEmpty(this.TotalSpot))
					return this.TotalSpot;
				else if (!string.IsNullOrEmpty(this.TotalGRP))
					return this.TotalGRP;
				else if (!string.IsNullOrEmpty(this.TotalCost))
					return this.TotalCost;
				else if (!string.IsNullOrEmpty(this.TotalCPP))
					return this.TotalCPP;
				else
					return string.Empty;
			}
		}

		public string TotalTitleToRight1
		{
			get
			{
				if (this.ShowNetCost && this.ShowDiscount && this.ShowTotalSpots && this.ShowTotalInvestment)
					return "Total Spots:";
				else
					return string.Empty;
			}
		}

		public string TotalTitleToRight2
		{
			get
			{
				if (this.ShowDiscount && this.ShowTotalInvestment && this.ShowNetCost)
					return "Total Cost:";
				else if (this.ShowTotalSpots && (this.ShowNetCost | this.ShowDiscount) && ((this.ShowDiscount & this.ShowNetCost) | this.ShowTotalInvestment))
					return "Total Spots:";
				else
					return string.Empty;
			}
		}

		public string TotalTitleToRight3
		{
			get
			{
				if (this.ShowNetCost && this.ShowDiscount)
					return "Agency Discount:";
				else if (this.ShowTotalInvestment && (this.ShowDiscount | this.ShowNetCost))
					return "Total Cost:";
				else if (this.ShowTotalSpots && (this.ShowNetCost | this.ShowDiscount | this.ShowTotalInvestment))
					return "Total Spots:";
				else
					return string.Empty;
			}
		}

		public string TotalTitleToRight4
		{
			get
			{
				if (this.ShowNetCost)
					return "Net Cost:";
				else if (this.ShowDiscount)
					return "Agency Discount:";
				else if (this.ShowTotalInvestment)
					return "Total Cost:";
				else if (this.ShowTotalSpots)
					return "Total Spots:";
				else
					return string.Empty;
			}
		}

		public string TotalValueToRight1
		{
			get
			{
				if (this.ShowNetCost && this.ShowDiscount && this.ShowTotalSpots && this.ShowTotalInvestment)
					return _parent.TotalSpots.ToString("#,##0");
				else
					return string.Empty;
			}
		}

		public string TotalValueToRight2
		{
			get
			{
				if (this.ShowDiscount && this.ShowTotalInvestment && this.ShowNetCost)
					return _parent.TotalCost.ToString("$#,##0");
				else if (this.ShowTotalSpots && (this.ShowNetCost | this.ShowDiscount) && ((this.ShowDiscount & this.ShowNetCost) | this.ShowTotalInvestment))
					return _parent.TotalSpots.ToString("#,##0");
				else
					return string.Empty;
			}
		}

		public string TotalValueToRight3
		{
			get
			{
				if (this.ShowNetCost && this.ShowDiscount)
					return _parent.Discount.ToString("$#,###.00");
				else if (this.ShowTotalInvestment && (this.ShowDiscount | this.ShowNetCost))
					return _parent.TotalCost.ToString("$#,##0");
				else if (this.ShowTotalSpots && (this.ShowNetCost | this.ShowDiscount | this.ShowTotalInvestment))
					return _parent.TotalSpots.ToString("#,##0");
				else
					return string.Empty;
			}
		}

		public string TotalValueToRight4
		{
			get
			{
				if (this.ShowNetCost)
					return _parent.NetRate.ToString("$#,###.00");
				else if (this.ShowDiscount)
					return _parent.Discount.ToString("$#,###.00");
				else if (this.ShowTotalInvestment)
					return _parent.TotalCost.ToString("$#,##0");
				else if (this.ShowTotalSpots)
					return _parent.TotalSpots.ToString("#,##0");
				else
					return string.Empty;
			}
		}

		public string TotalTitleToLeft1
		{
			get
			{
				if (this.ShowTotalSpots)
					return "Total Spots:";
				else if (this.ShowNetCost)
					return "Total Cost:";
				else if (this.ShowTotalInvestment)
					return "Agency Discount:";
				else if (this.ShowDiscount)
					return "Net Cost:";
				else
					return string.Empty;
			}
		}

		public string TotalTitleToLeft2
		{
			get
			{
				if (this.ShowTotalSpots && this.ShowTotalInvestment)
					return "Total Cost:";
				else if (this.ShowDiscount && (this.ShowTotalInvestment | this.ShowTotalSpots))
					return "Agency Discount:";
				else if (this.ShowNetCost && (this.ShowTotalSpots | this.ShowTotalInvestment | this.ShowDiscount))
					return "Net Cost:";
				else
					return string.Empty;
			}
		}

		public string TotalTitleToLeft3
		{
			get
			{
				if (this.ShowTotalSpots && this.ShowTotalInvestment && this.ShowDiscount)
					return "Agency Discount:";
				else if (this.ShowNetCost && (this.ShowTotalSpots | this.ShowTotalInvestment) && ((this.ShowTotalSpots & this.ShowTotalInvestment) | this.ShowDiscount))
					return "Net Cost:";
				else
					return string.Empty;
			}
		}

		public string TotalTitleToLeft4
		{
			get
			{
				if (this.ShowNetCost && this.ShowDiscount && this.ShowTotalSpots && this.ShowTotalInvestment)
					return "Net Cost:";
				else
					return string.Empty;
			}
		}

		public string TotalValueToLeft1
		{
			get
			{
				if (this.ShowTotalSpots)
					return _parent.TotalSpots.ToString("#,##0");
				else if (this.ShowNetCost)
					return _parent.TotalCost.ToString("$#,##0");
				else if (this.ShowTotalInvestment)
					return _parent.Discount.ToString("$#,###.00");
				else if (this.ShowDiscount)
					return _parent.NetRate.ToString("$#,###.00");
				else
					return string.Empty;
			}
		}

		public string TotalValueToLeft2
		{
			get
			{
				if (this.ShowTotalSpots && this.ShowTotalInvestment)
					return _parent.TotalCost.ToString("$#,##0");
				else if (this.ShowDiscount && (this.ShowTotalInvestment | this.ShowTotalSpots))
					return _parent.Discount.ToString("$#,###.00");
				else if (this.ShowNetCost && (this.ShowTotalSpots | this.ShowTotalInvestment | this.ShowDiscount))
					return _parent.NetRate.ToString("$#,###.00");
				else
					return string.Empty;
			}
		}

		public string TotalValueToLeft3
		{
			get
			{
				if (this.ShowTotalSpots && this.ShowTotalInvestment && this.ShowDiscount)
					return _parent.Discount.ToString("$#,###.00");
				else if (this.ShowNetCost && (this.ShowTotalSpots | this.ShowTotalInvestment) && ((this.ShowTotalSpots & this.ShowTotalInvestment) | this.ShowDiscount))
					return _parent.NetRate.ToString("$#,###.00");
				else
					return string.Empty;
			}
		}

		public string TotalValueToLeft4
		{
			get
			{
				if (this.ShowNetCost && this.ShowDiscount && this.ShowTotalSpots && this.ShowTotalInvestment)
					return _parent.NetRate.ToString("$#,###.00");
				else
					return string.Empty;
			}
		}

		public OutputScheduleTagsBased(ScheduleSection parent)
		{
			_parent = parent;
			this.Advertiser = string.Empty;
			this.DecisionMaker = string.Empty;

			this.Programs = new List<OutputProgramTagsBased>();
			this.TotalSpots = new List<OutputTotalSpot>();
		}

		public string GetSlideTemplateName()
		{
			string result = string.Empty;
			result = string.Format(BusinessClasses.OutputManager.OneSheetsTagsBasedTemlateFileName, new string[] { this.ColumnsPerSlide.ToString(), this.ProgramsPerSlide.ToString(), this.SpotsPerSlide.ToString() });
			return result;
		}
	}

	public class OutputProgramGridBased
	{
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

		public string DemoValue1
		{
			get
			{
				string result = string.Empty;
				if (this.Parent.ShowRating)
					result = this.Parent.RtgHeaderTitle + ": " + this.Rating;
				else if (this.Parent.ShowGRP)
					result = this.Parent.GRPHeaderTitle + ": " + this.GRP;
				else if (this.Parent.ShowCPP)
					result = this.Parent.CPPHeaderTitle + ": " + this.CPP;
				return result;
			}
		}
		public string DemoValue2
		{
			get
			{
				string result = string.Empty;
				if (this.Parent.ShowRating && this.Parent.ShowGRP)
					result = this.Parent.GRPHeaderTitle + ": " + this.GRP;
				else if (this.Parent.ShowCPP && (this.Parent.ShowRating || this.Parent.ShowGRP))
					result = this.Parent.CPPHeaderTitle + ": " + this.CPP;
				return result;
			}
		}
		public string DemoValue3
		{
			get
			{
				string result = string.Empty;
				if (this.Parent.ShowRating && this.Parent.ShowGRP && this.Parent.ShowCPP)
					result = this.Parent.CPPHeaderTitle + ": " + this.CPP;
				return result;
			}
		}

		public OutputProgramGridBased(OutputScheduleGridBased parent)
		{
			this.Parent = parent;
			this.Name = string.Empty;
			this.LineID = string.Empty;
			this.Station = string.Empty;
			this.Time = string.Empty;
			this.Days = string.Empty;
			this.Length = string.Empty;
			this.Rate = string.Empty;
			this.Rating = string.Empty;
			this.TotalRate = string.Empty;
			this.TotalSpots = string.Empty;
			this.CPP = string.Empty;
			this.CPP = string.Empty;
			this.Spots = new List<string>();
		}
	}

	public class OutputProgramTagsBased
	{
		public string Name { get; set; }
		public string LineID { get; set; }
		public string Properties { get; set; }

		public string TotalRate { get; set; }
		public string TotalSpots { get; set; }
		public string CPP { get; set; }
		public string GRP { get; set; }

		public List<string> Spots { get; set; }

		public string Item4Value
		{
			get
			{
				if (!string.IsNullOrEmpty(this.TotalSpots) && !string.IsNullOrEmpty(this.GRP) && !string.IsNullOrEmpty(this.TotalRate) && !string.IsNullOrEmpty(this.CPP))
					return this.CPP;
				else
					return string.Empty;
			}
		}

		public string Item3Value
		{
			get
			{
				if (!string.IsNullOrEmpty(this.TotalRate) && !string.IsNullOrEmpty(this.GRP) && !string.IsNullOrEmpty(this.TotalSpots))
					return this.TotalRate;
				else if (!string.IsNullOrEmpty(this.CPP) && (!string.IsNullOrEmpty(this.TotalSpots) | !string.IsNullOrEmpty(this.GRP)) && ((!string.IsNullOrEmpty(this.TotalRate) & !string.IsNullOrEmpty(this.TotalSpots)) | !string.IsNullOrEmpty(this.GRP)))
					return this.CPP;
				else
					return string.Empty;
			}
		}

		public string Item2Value
		{
			get
			{
				if (!string.IsNullOrEmpty(this.TotalSpots) && !string.IsNullOrEmpty(this.GRP))
					return this.GRP;
				else if (!string.IsNullOrEmpty(this.TotalRate) && (!string.IsNullOrEmpty(this.GRP) | !string.IsNullOrEmpty(this.TotalSpots)))
					return this.TotalRate;
				else if (!string.IsNullOrEmpty(this.CPP) && (!string.IsNullOrEmpty(this.TotalSpots) | !string.IsNullOrEmpty(this.TotalRate) | !string.IsNullOrEmpty(this.GRP)))
					return this.CPP;
				else
					return string.Empty;
			}
		}

		public string Item1Value
		{
			get
			{
				if (!string.IsNullOrEmpty(this.TotalSpots))
					return this.TotalSpots;
				else if (!string.IsNullOrEmpty(this.GRP))
					return this.GRP;
				else if (!string.IsNullOrEmpty(this.TotalRate))
					return this.TotalRate;
				else if (!string.IsNullOrEmpty(this.CPP))
					return this.CPP;
				else
					return string.Empty;
			}
		}

		public OutputProgramTagsBased()
		{
			this.Name = string.Empty;
			this.LineID = string.Empty;
			this.Properties = string.Empty;
			this.TotalRate = string.Empty;
			this.TotalSpots = string.Empty;
			this.CPP = string.Empty;
			this.GRP = string.Empty;

			this.Spots = new List<string>();
		}
	}

	public class OutputTotalSpot
	{
		public string Month { get; set; }
		public string Day { get; set; }
		public string Value { get; set; }

		public string HeaderTagText
		{
			get
			{
				return this.Month + ((char)13).ToString() + this.Day;
			}
		}

		public OutputTotalSpot()
		{
			this.Month = string.Empty;
			this.Day = string.Empty;
			this.Value = string.Empty;
		}
	}
}
