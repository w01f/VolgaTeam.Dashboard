using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TVScheduleBuilder.BusinessClasses
{
    class OutputManager
    {
        public static string MasterWizardsRootFolderPath = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Dashboard", System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles));
        private const string OneSheetsExcelBasedTemplatesFolderName = @"{0}\TV Slides\onesheets";
        private const string OneSheetsGridBasedTemplatesFolderName = @"{0}\TV Slides\tables";
        public const string OneSheetsExcelTemplatesFolderName = @"{0}\newlocaldirect.com\sync\Incoming\Slides\ExcelOutput{1}\TV Slides\{2} Programs\{3}";
        public const string OneSheetGridBasedTemplateFileName = @"{0} Programs\{0}-{1}.ppt";
        public const string OneSheetExcelBasedTemplateFileName = @"tvslide1.ppt";
        public const string OneSheetsExcelTemplateFileName = @"{0}.xls";

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
                return Path.Combine(MasterWizardsRootFolderPath, ConfigurationClasses.SettingsManager.Instance.SelectedWizard, string.Format(OneSheetsExcelBasedTemplatesFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        public string OneSheetGridBasedTemplatesFolderPath
        {
            get
            {
                return Path.Combine(MasterWizardsRootFolderPath, ConfigurationClasses.SettingsManager.Instance.SelectedWizard, string.Format(OneSheetsGridBasedTemplatesFolderName, ConfigurationClasses.SettingsManager.Instance.SlideFolder));
            }
        }

        private OutputManager()
        {
        }
    }

    public class OutputSchedule
    {
        private Schedule _parent = null;
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

        public List<OutputProgram> Programs { get; set; }
        public List<OutputTotalSpot> TotalSpots { get; set; }
        public Dictionary<string, string> Totals { get; set; }
        public Dictionary<string, string> ReplacementsList { get; set; }

        public string FlightDates
        {
            get
            {
                return _parent.FlightDates;
            }
        }

        public string RtgHeaderTitle
        {
            get
            {
                string result = string.Empty;
                if (_parent.RatingAsCPP)
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
                if (_parent.RatingAsCPP)
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
                if (_parent.RatingAsCPP)
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

        public OutputSchedule(Schedule parent)
        {
            _parent = parent;
            this.Title = string.Empty;
            this.Advertiser = string.Empty;
            this.DecisionMaker = string.Empty;
            this.Programs = new List<OutputProgram>();
            this.TotalSpots = new List<OutputTotalSpot>();
            this.Totals = new Dictionary<string, string>();
            this.ReplacementsList = new Dictionary<string, string>();
        }

        public void PopulateWeeklyScheduleReplacementsList()
        {
            string key = string.Empty;
            string value = string.Empty;
            List<string> temp = new List<string>();
            this.ReplacementsList.Clear();


            for (int i = 0; i < this.SpotsPerSlide; i++)
            {
                key = string.Format("MO {0}", (i + 1).ToString("00"));
                value = this.TotalSpots[i].Month + (char)13 + this.TotalSpots[i].Day;
                this.ReplacementsList.Add(key, value);
                key = string.Format("MO  {0}", (i + 1).ToString("00"));
                value = this.TotalSpots[i].Month + (char)13 + this.TotalSpots[i].Day;
                this.ReplacementsList.Add(key, value);
                key = string.Format("M0  {0}", (i + 1).ToString("00"));
                value = this.TotalSpots[i].Month + (char)13 + this.TotalSpots[i].Day;
                this.ReplacementsList.Add(key, value);

                key = string.Format("t{0}", new object[] { (i + 1).ToString() });
                value = this.TotalSpots[i].Value;
                this.ReplacementsList.Add(key, value);
            }
            for (int i = 0; i < this.ProgramsPerSlide; i++)
            {
                key = string.Format("Program{0}", (i + 1).ToString());
                value = this.Programs[i].Name;
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
                this.ReplacementsList.Add(key, value);

                key = string.Format("Rate_{0}", (i + 1).ToString());
                value = this.Programs[i].Rate;
                this.ReplacementsList.Add(key, value);

                if (this.ShowCost && this.ShowSpots)
                {
                    key = string.Format("Cost{0}", (i + 1).ToString());
                    value = this.Programs[i].TotalRate;
                    this.ReplacementsList.Add(key, value);

                    key = string.Format("Spots{0}", (i + 1).ToString());
                    value = this.Programs[i].TotalSpots;
                    this.ReplacementsList.Add(key, value);

                    key = "tspots";
                    value = _parent.WeeklySchedule.TotalSpots.ToString("#,##0");
                    if (!this.ReplacementsList.Keys.Contains(key))
                        this.ReplacementsList.Add(key, value);

                    key = "tcost";
                    value = _parent.WeeklySchedule.TotalCost.ToString("$#,##0");
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
                    value = _parent.WeeklySchedule.TotalSpots.ToString("#,##0");
                    if (!this.ReplacementsList.Keys.Contains(key))
                        this.ReplacementsList.Add(key, value);

                    key = string.Format("SpotsCost{0}", (i + 1).ToString());
                    value = this.Programs[i].TotalSpots;
                    this.ReplacementsList.Add(key, value);
                }
                else if (this.ShowCost)
                {
                    key = "SpotsCost";
                    value = "Total Cost";
                    if (!this.ReplacementsList.Keys.Contains(key))
                        this.ReplacementsList.Add(key, value);

                    key = "tspotscost";
                    value = _parent.WeeklySchedule.TotalCost.ToString("$#,##0");
                    if (!this.ReplacementsList.Keys.Contains(key))
                        this.ReplacementsList.Add(key, value);

                    key = string.Format("SpotsCost{0}", (i + 1).ToString());
                    value = this.Programs[i].TotalRate;
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
                }
                for (int j = 0; j < this.SpotsPerSlide; j++)
                {
                    key = string.Format("{0}{1}", new object[] { spotPrefix, (j + 1).ToString() });
                    value = this.Programs[i].Spots[j];
                    this.ReplacementsList.Add(key, value);
                }

                if(!string.IsNullOrEmpty(_parent.Demo))
                {
                    string demoSuffix = "a";
                    key = string.Format("RTG{0}{1}", new object[] { (i + 1).ToString(), demoSuffix });
                    value = this.Programs[i].DemoValue1;
                    this.ReplacementsList.Add(key, value);

                    key = string.Format("GRP{0}{1}", new object[] { (i + 1).ToString(), demoSuffix });
                    value = this.Programs[i].DemoValue2;
                    this.ReplacementsList.Add(key, value);

                    key = string.Format("CPP{0}{1}", new object[] { (i + 1).ToString(), demoSuffix });
                    value = this.Programs[i].DemoValue3;
                    this.ReplacementsList.Add(key, value);
                }
            }
        }

        public void PopulateMonthlyScheduleReplacementsList()
        {
            string key = string.Empty;
            string value = string.Empty;
            List<string> temp = new List<string>();
            this.ReplacementsList.Clear();


            for (int i = 0; i < this.SpotsPerSlide; i++)
            {
                key = string.Format("MO {0}", (i + 1).ToString("00"));
                value = this.TotalSpots[i].Month;
                this.ReplacementsList.Add(key, value);
                key = string.Format("MO  {0}", (i + 1).ToString("00"));
                value = this.TotalSpots[i].Month;
                this.ReplacementsList.Add(key, value);
                key = string.Format("M0  {0}", (i + 1).ToString("00"));
                value = this.TotalSpots[i].Month;
                this.ReplacementsList.Add(key, value);

                key = string.Format("t{0}", new object[] { (i + 1).ToString() });
                value = this.TotalSpots[i].Value;
                this.ReplacementsList.Add(key, value);
            }
            for (int i = 0; i < this.ProgramsPerSlide; i++)
            {
                key = string.Format("Program{0}", (i + 1).ToString());
                value = this.Programs[i].Name;
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
                this.ReplacementsList.Add(key, value);

                key = string.Format("Rate_{0}", (i + 1).ToString());
                value = this.Programs[i].Rate;
                this.ReplacementsList.Add(key, value);

                if (this.ShowCost && this.ShowSpots)
                {
                    key = string.Format("Cost{0}", (i + 1).ToString());
                    value = this.Programs[i].TotalRate;
                    this.ReplacementsList.Add(key, value);

                    key = string.Format("Spots{0}", (i + 1).ToString());
                    value = this.Programs[i].TotalSpots;
                    this.ReplacementsList.Add(key, value);

                    key = "tspots";
                    value = _parent.MonthlySchedule.TotalSpots.ToString("#,##0");
                    if (!this.ReplacementsList.Keys.Contains(key))
                        this.ReplacementsList.Add(key, value);

                    key = "tcost";
                    value = _parent.MonthlySchedule.TotalCost.ToString("$#,##0");
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
                    value = _parent.MonthlySchedule.TotalSpots.ToString("#,##0");
                    if (!this.ReplacementsList.Keys.Contains(key))
                        this.ReplacementsList.Add(key, value);

                    key = string.Format("SpotsCost{0}", (i + 1).ToString());
                    value = this.Programs[i].TotalSpots;
                    this.ReplacementsList.Add(key, value);
                }
                else if (this.ShowCost)
                {
                    key = "SpotsCost";
                    value = "Total Cost";
                    if (!this.ReplacementsList.Keys.Contains(key))
                        this.ReplacementsList.Add(key, value);

                    key = "tspotscost";
                    value = _parent.MonthlySchedule.TotalCost.ToString("$#,##0");
                    if (!this.ReplacementsList.Keys.Contains(key))
                        this.ReplacementsList.Add(key, value);

                    key = string.Format("SpotsCost{0}", (i + 1).ToString());
                    value = this.Programs[i].TotalRate;
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
                }
                for (int j = 0; j < this.SpotsPerSlide; j++)
                {
                    key = string.Format("{0}{1}", new object[] { spotPrefix, (j + 1).ToString() });
                    value = this.Programs[i].Spots[j];
                    this.ReplacementsList.Add(key, value);
                }

                if (!string.IsNullOrEmpty(_parent.Demo))
                {
                    string demoSuffix = "a";
                    key = string.Format("RTG{0}{1}", new object[] { (i + 1).ToString(), demoSuffix });
                    value = this.Programs[i].DemoValue1;
                    this.ReplacementsList.Add(key, value);

                    key = string.Format("GRP{0}{1}", new object[] { (i + 1).ToString(), demoSuffix });
                    value = this.Programs[i].DemoValue2;
                    this.ReplacementsList.Add(key, value);

                    key = string.Format("CPP{0}{1}", new object[] { (i + 1).ToString(), demoSuffix });
                    value = this.Programs[i].DemoValue3;
                    this.ReplacementsList.Add(key, value);
                }
            }
        }

        public int GetGridBasedSlideNumber()
        {
            int result = 0;
            BusinessClasses.OutputProgram program = this.Programs.FirstOrDefault();
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

    public class OutputProgram
    {
        public OutputSchedule Parent { get; private set; }
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
                    result = this.Parent.RtgHeaderTitle +": " +this.Rating;
                else if (this.Parent.ShowGRP)
                    result = this.Parent.GRPHeaderTitle +": " +this.GRP;
                else if (this.Parent.ShowGRP)
                    result = this.Parent.CPPHeaderTitle +": " +this.CPP;
                return result;
            }
        }
        public string DemoValue2
        {
            get
            {
                string result = string.Empty;
                if (this.Parent.ShowRating && this.Parent.ShowGRP)
                    result = this.Parent.GRPHeaderTitle +": " +this.GRP;
                else if (this.Parent.ShowCPP && (this.Parent.ShowRating || this.Parent.ShowGRP))
                    result = this.Parent.CPPHeaderTitle +": " +this.CPP;
                return result;
            }
        }
        public string DemoValue3
        {
            get
            {
                string result = string.Empty;
                if (this.Parent.ShowRating && this.Parent.ShowGRP && this.Parent.ShowCPP)
                    result = this.Parent.CPPHeaderTitle +": " +this.CPP;
                return result;
            }
        }

        public OutputProgram(OutputSchedule parent)
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

    public class OutputTotalSpot
    {
        public string Month { get; set; }
        public string Day { get; set; }
        public string Value { get; set; }

        public OutputTotalSpot()
        {
            this.Month = string.Empty;
            this.Day = string.Empty;
            this.Value = string.Empty;
        }
    }
}
