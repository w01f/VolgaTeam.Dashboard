using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Core.OnlineSchedule
{
	public enum RateType
	{
		CPM = 0,
		Fixed
	}

	public enum FormulaType
	{
		CPM = 0,
		Investment,
		Impressions
	}

	public class ScheduleManager
	{
		private Schedule _currentSchedule;
		public bool ScheduleLoaded { get; set; }
		public event EventHandler<SavingingEventArgs> SettingsSaved;

		public void OpenSchedule(string scheduleName, bool create)
		{
			string scheduleFilePath = GetScheduleFileName(scheduleName);
			if (create && File.Exists(scheduleFilePath))
				if (Utilities.Instance.ShowWarningQuestion(string.Format("An older Schedule is already saved with this same file name.\nDo you want to replace this file with a newer schedule?", scheduleName)) == DialogResult.Yes)
					File.Delete(scheduleFilePath);
			_currentSchedule = new Schedule(scheduleFilePath);
			ScheduleLoaded = true;
		}

		public void OpenSchedule(string scheduleFilePath)
		{
			_currentSchedule = new Schedule(scheduleFilePath);
			ScheduleLoaded = true;
		}

		public void CreateSchedule(string scheduleName)
		{
			string calendarFilePath = GetScheduleFileName(scheduleName);
			OpenSchedule(calendarFilePath);
		}

		public string GetScheduleFileName(string scheduleName)
		{
			return Path.Combine(SettingsManager.Instance.SaveFolder, scheduleName + ".xml");
		}

		public Schedule GetLocalSchedule()
		{
			return new Schedule(_currentSchedule.ScheduleFile.FullName);
		}

		public ShortSchedule GetShortSchedule()
		{
			return new ShortSchedule(_currentSchedule.ScheduleFile);
		}

		public void SaveSchedule(Schedule localSchedule, bool quickSave, Control sender)
		{
			localSchedule.Save();
			_currentSchedule = localSchedule;
			if (SettingsSaved != null)
				SettingsSaved(sender, new SavingingEventArgs(quickSave));
		}

		public static ShortSchedule[] GetShortScheduleList(DirectoryInfo rootFolder)
		{
			var scheduleList = new List<ShortSchedule>();
			foreach (FileInfo file in rootFolder.GetFiles("*.xml"))
			{
				var schedule = new ShortSchedule(file);
				scheduleList.Add(schedule);
			}
			return scheduleList.ToArray();
		}

		public static ShortSchedule[] GetShortScheduleList()
		{
			var saveFolder = new DirectoryInfo(SettingsManager.Instance.SaveFolder);
			if (saveFolder.Exists)
				return GetShortScheduleList(saveFolder);
			return null;
		}

		public void RemoveInstance()
		{
			SettingsSaved = null;
		}
	}

	public class SavingingEventArgs : EventArgs
	{
		public SavingingEventArgs(bool quickSave)
		{
			QuickSave = quickSave;
		}

		public bool QuickSave { get; set; }
	}

	public class ShortSchedule
	{
		private readonly FileInfo _scheduleFile;

		public ShortSchedule(FileInfo file)
		{
			BusinessName = string.Empty;
			Status = ListManager.Instance.Statuses.FirstOrDefault();
			_scheduleFile = file;
			Load();
		}

		public string BusinessName { get; set; }
		public string Status { get; set; }

		public string ShortFileName
		{
			get { return _scheduleFile.Name.Replace(_scheduleFile.Extension, ""); }
		}

		public string FullFileName
		{
			get { return _scheduleFile.FullName; }
		}

		public DateTime LastModifiedDate
		{
			get { return _scheduleFile.LastWriteTime; }
		}

		private void Load()
		{
			XmlNode node;
			if (_scheduleFile.Exists)
			{
				var document = new XmlDocument();
				document.Load(_scheduleFile.FullName);

				node = document.SelectSingleNode(@"/Schedule/BusinessName");
				if (node != null)
					BusinessName = node.InnerText;

				node = document.SelectSingleNode(@"/Schedule/Status");
				if (node != null)
					Status = node.InnerText;
			}
		}

		public void Save()
		{
			XmlNode node;
			if (_scheduleFile.Exists)
			{
				try
				{
					var document = new XmlDocument();
					document.Load(_scheduleFile.FullName);

					node = document.SelectSingleNode(@"/Schedule/Status");
					if (node != null)
						node.InnerText = Status;
					else
					{
						node = document.SelectSingleNode(@"/Schedule");
						if (node != null)
							node.InnerXml += (@"<Status>" + (Status != null ? Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
					}
					document.Save(_scheduleFile.FullName);
				}
				catch { }
			}
		}
	}

	public class Schedule : ISchedule
	{
		public Schedule(string fileName)
		{
			ClientType = string.Empty;
			AccountNumber = string.Empty;
			Status = ListManager.Instance.Statuses.FirstOrDefault();
			Products = new List<DigitalProduct>();
			ViewSettings = new ScheduleBuilderViewSettings();

			_scheduleFile = new FileInfo(fileName);
			if (!File.Exists(fileName))
			{
				var xml = new StringBuilder();
				xml.AppendLine(@"<Schedule>");
				xml.AppendLine(@"<Status>" + (Status != null ? Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
				xml.AppendLine(@"</Schedule>");
				using (var sw = new StreamWriter(_scheduleFile.FullName, false))
				{
					sw.Write(xml);
					sw.Flush();
				}
				_scheduleFile = new FileInfo(fileName);
			}
			else
				Load();
		}

		private FileInfo _scheduleFile { get; set; }
		public string Status { get; set; }
		public string ThemeName { get; set; }
		public string ClientType { get; set; }
		public string AccountNumber { get; set; }
		public bool ApplySettingsForeAllProducts { get; set; }
		public List<DigitalProduct> Products { get; set; }

		public ScheduleBuilderViewSettings ViewSettings { get; set; }

		public IScheduleViewSettings CommonViewSettings
		{
			get { return ViewSettings; }
		}

		public string Name
		{
			get { return _scheduleFile.Name.Replace(_scheduleFile.Extension, ""); }
			set { _scheduleFile = new FileInfo(Path.Combine(_scheduleFile.Directory.FullName, value + ".xml")); }
		}

		public FileInfo ScheduleFile
		{
			get { return _scheduleFile; }
		}

		public object PresentationDateObject
		{
			get
			{
				if (PresentationDate.Equals(DateTime.MaxValue) || PresentationDate.Equals(DateTime.MinValue))
				{
					return null;
				}
				else
					return PresentationDate;
			}
		}

		public object FlightDateStartObject
		{
			get
			{
				if (FlightDateStart.Equals(DateTime.MaxValue) || FlightDateStart.Equals(DateTime.MinValue))
				{
					return null;
				}
				else
					return FlightDateStart;
			}
		}

		public object FlightDateEndObject
		{
			get
			{
				if (FlightDateEnd.Equals(DateTime.MaxValue) || FlightDateEnd.Equals(DateTime.MinValue))
				{
					return null;
				}
				else
					return FlightDateEnd;
			}
		}

		public double MonthlyInvestment
		{
			get { return Products.Select(x => (x.MonthlyInvestment.HasValue ? x.MonthlyInvestment.Value : 0)).Sum(); }
		}

		public double MonthlyImpressions
		{
			get { return Products.Select(x => (x.MonthlyImpressions.HasValue ? x.MonthlyImpressions.Value : 0)).Sum(); }
		}

		public bool EnableMonthlyOnSummary
		{
			get
			{
				bool result = false;
				foreach (DigitalProduct product in Products)
					result = result | (product.MonthlyImpressions.HasValue || product.MonthlyInvestment.HasValue);
				return result;
			}
		}

		public double TotalInvestment
		{
			get { return Products.Select(x => (x.TotalInvestment.HasValue ? x.TotalInvestment.Value : 0)).Sum(); }
		}

		public double TotalImpressions
		{
			get { return Products.Select(x => (x.TotalImpressions.HasValue ? x.TotalImpressions.Value : 0)).Sum(); }
		}

		public bool EnableTotalOnSummary
		{
			get
			{
				bool result = false;
				foreach (DigitalProduct product in Products)
					result = result | (product.TotalImpressions.HasValue || product.TotalInvestment.HasValue);
				return result;
			}
		}

		#region ISchedule Members
		public string BusinessName { get; set; }
		public string DecisionMaker { get; set; }
		public DateTime PresentationDate { get; set; }
		public string FlightDates
		{
			get
			{
				if (FlightDateStart != DateTime.MinValue && FlightDateEnd != DateTime.MinValue)
					return FlightDateStart.ToString("MM/dd/yy") + " - " + FlightDateEnd.ToString("MM/dd/yy");
				else
					return string.Empty;
			}
		}

		public DateTime FlightDateStart { get; set; }
		public DateTime FlightDateEnd { get; set; }
		#endregion

		private void Load()
		{
			bool tempBool;
			DateTime tempDateTime;

			XmlNode node;
			if (_scheduleFile.Exists)
			{
				var document = new XmlDocument();
				document.Load(_scheduleFile.FullName);

				node = document.SelectSingleNode(@"/Schedule/BusinessName");
				if (node != null)
					BusinessName = node.InnerText;

				node = document.SelectSingleNode(@"/Schedule/DecisionMaker");
				if (node != null)
					DecisionMaker = node.InnerText;

				node = document.SelectSingleNode(@"/Schedule/Status");
				if (node != null)
					Status = node.InnerText;

				node = document.SelectSingleNode(@"/Schedule/ThemeName");
				if (node != null)
					ThemeName = node.InnerText;

				node = document.SelectSingleNode(@"/Schedule/ClientType");
				if (node != null)
					ClientType = node.InnerText;

				node = document.SelectSingleNode(@"/Schedule/AccountNumber");
				if (node != null)
					AccountNumber = node.InnerText;

				node = document.SelectSingleNode(@"/Schedule/PresentationDate");
				if (node != null)
				{
					tempDateTime = DateTime.MaxValue;
					DateTime.TryParse(node.InnerText, out tempDateTime);
					PresentationDate = tempDateTime;
				}

				node = document.SelectSingleNode(@"/Schedule/FlightDateStart");
				if (node != null)
				{
					tempDateTime = DateTime.MaxValue;
					DateTime.TryParse(node.InnerText, out tempDateTime);
					FlightDateStart = tempDateTime;
				}

				node = document.SelectSingleNode(@"/Schedule/FlightDateEnd");
				if (node != null)
				{
					tempDateTime = DateTime.MaxValue;
					DateTime.TryParse(node.InnerText, out tempDateTime);
					FlightDateEnd = tempDateTime;
				}

				node = document.SelectSingleNode(@"/Schedule/ApplySettingsForeAllProducts");
				if (node != null)
				{
					tempBool = false;
					bool.TryParse(node.InnerText, out tempBool);
					ApplySettingsForeAllProducts = tempBool;
				}

				node = document.SelectSingleNode(@"/Schedule/ViewSettings");
				if (node != null)
				{
					ViewSettings.Deserialize(node);
				}

				node = document.SelectSingleNode(@"/Schedule/ViewSettings");
				if (node != null)
				{
					ViewSettings.Deserialize(node);
				}

				node = document.SelectSingleNode(@"/Schedule/Products");
				if (node != null)
				{
					foreach (XmlNode productNode in node.ChildNodes)
					{
						var product = new DigitalProduct(this);
						product.Deserialize(productNode);
						Products.Add(product);
					}
				}
			}
		}

		public void Save()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
			var xml = new StringBuilder();

			xml.AppendLine(@"<Schedule>");

			xml.AppendLine(@"<BusinessName>" + BusinessName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BusinessName>");
			if (!Common.ListManager.Instance.Advertisers.Contains(BusinessName))
			{
				Common.ListManager.Instance.Advertisers.Add(BusinessName);
				Common.ListManager.Instance.SaveAdvertisers();
			}

			xml.AppendLine(@"<DecisionMaker>" + DecisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
			if (!Common.ListManager.Instance.DecisionMakers.Contains(DecisionMaker))
			{
				Common.ListManager.Instance.DecisionMakers.Add(DecisionMaker);
				Common.ListManager.Instance.SaveDecisionMakers();
			}
			xml.AppendLine(@"<ClientType>" + ClientType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ClientType>");
			xml.AppendLine(@"<AccountNumber>" + AccountNumber.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</AccountNumber>");
			xml.AppendLine(@"<Status>" + (Status != null ? Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
			xml.AppendLine(@"<ThemeName>" + (ThemeName != null ? ThemeName.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</ThemeName>");
			xml.AppendLine(@"<PresentationDate>" + PresentationDate.ToString() + @"</PresentationDate>");
			xml.AppendLine(@"<FlightDateStart>" + FlightDateStart.ToString() + @"</FlightDateStart>");
			xml.AppendLine(@"<FlightDateEnd>" + FlightDateEnd.ToString() + @"</FlightDateEnd>");
			xml.AppendLine(@"<ApplySettingsForeAllProducts>" + ApplySettingsForeAllProducts.ToString() + @"</ApplySettingsForeAllProducts>");

			xml.AppendLine(@"<ViewSettings>" + ViewSettings.Serialize() + @"</ViewSettings>");

			xml.AppendLine(@"<Products>");
			foreach (DigitalProduct product in Products)
			{
				xml.AppendLine(product.Serialize());
			}
			xml.AppendLine(@"</Products>");
			xml.AppendLine(@"</Schedule>");
			using (var sw = new StreamWriter(_scheduleFile.FullName, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		public void AddProduct(string categoryName)
		{
			var product = new DigitalProduct(this) { Index = Products.Count + 1, Category = categoryName };
			Products.Add(product);
		}

		public void UpProduct(int position)
		{
			if (position > 0)
			{
				Products[position].Index--;
				Products[position - 1].Index++;
				Products.Sort((x, y) => x.Index.CompareTo(y.Index));
			}
		}

		public void DownProduct(int position)
		{
			if (position < Products.Count - 1)
			{
				Products[position].Index++;
				Products[position + 1].Index--;
				Products.Sort((x, y) => x.Index.CompareTo(y.Index));
			}
		}

		public void RebuildProductIndexes()
		{
			for (int i = 0; i < Products.Count; i++)
				Products[i].Index = i + 1;
		}
	}

	public class DigitalProduct
	{
		private string _name;

		#region Basic Properties
		public ISchedule Parent { get; set; }
		public Guid UniqueID { get; set; }
		public int Index { get; set; }
		public string Category { get; set; }
		public string SubCategory { get; set; }
		public RateType RateType { get; set; }
		public int? Width { get; set; }
		public int? Height { get; set; }
		public string Description { get; set; }
		#endregion

		#region Additional Properties
		public string UserDefinedName { get; set; }
		public string SlideHeader { get; set; }
		public List<string> Websites { get; set; }
		public string CustomWebsite1 { get; set; }
		public string CustomWebsite2 { get; set; }
		public string CustomWebsite3 { get; set; }
		public string CustomWebsite4 { get; set; }
		public int? ActiveDays { get; set; }
		public int? TotalAds { get; set; }
		public string DurationType { get; set; }
		public int? DurationValue { get; set; }
		public string Strength1 { get; set; }
		public string Strength2 { get; set; }
		public string Comment { get; set; }
		public double? AdRate { get; set; }
		public double? MonthlyInvestment { get; set; }
		public double? MonthlyImpressions { get; set; }
		public double? MonthlyCPM { get; set; }
		public double? TotalInvestment { get; set; }
		public double? TotalImpressions { get; set; }
		public double? TotalCPM { get; set; }
		public double? DefaultRate { get; set; }
		public FormulaType Formula { get; set; }
		#endregion

		#region Show Properties
		private bool _defaultShowCPMButton = true;
		private bool _showCPMButton = true;
		public bool DefaultShowPresentationDate { get; set; }
		public bool DefaultShowBusinessName { get; set; }
		public bool DefaultShowDecisionMaker { get; set; }
		public bool DefaultShowWebsite { get; set; }
		public bool DefaultShowCustomWebsite1 { get; set; }
		public bool DefaultShowCustomWebsite2 { get; set; }
		public bool DefaultShowCustomWebsite3 { get; set; }
		public bool DefaultShowCustomWebsite4 { get; set; }
		public bool DefaultShowProduct { get; set; }
		public bool DefaultShowDescription { get; set; }
		public bool DefaultShowDimensions { get; set; }
		public bool DefaultShowFlightDates { get; set; }
		public bool DefaultShowActiveDays { get; set; }
		public bool DefaultShowTotalAds { get; set; }
		public bool DefaultShowAdRate { get; set; }
		public bool DefaultShowMonthlyInvestment { get; set; }
		public bool DefaultShowTotalInvestment { get; set; }
		public bool DefaultShowMonthlyImpressions { get; set; }
		public bool DefaultShowTotalImpressions { get; set; }
		public bool DefaultShowComments { get; set; }
		public bool DefaultShowDuration { get; set; }
		public bool DefaultShowCommentText { get; set; }
		public bool DefaultShowStrength1 { get; set; }
		public bool DefaultShowStrength2 { get; set; }
		public bool DefaultShowImages { get; set; }
		public bool DefaultShowScreenshot { get; set; }
		public bool DefaultShowSignature { get; set; }

		public bool ShowPresentationDate { get; set; }
		public bool ShowBusinessName { get; set; }
		public bool ShowDecisionMaker { get; set; }
		public bool ShowWebsite { get; set; }
		public bool ShowCustomWebsite1 { get; set; }
		public bool ShowCustomWebsite2 { get; set; }
		public bool ShowCustomWebsite3 { get; set; }
		public bool ShowCustomWebsite4 { get; set; }
		public bool ShowProduct { get; set; }
		public bool ShowDescription { get; set; }
		public bool ShowDimensions { get; set; }
		public bool ShowFlightDates { get; set; }
		public bool ShowActiveDays { get; set; }
		public bool ShowTotalAds { get; set; }
		public bool ShowAdRate { get; set; }
		public bool ShowMonthlyInvestment { get; set; }
		public bool ShowTotalInvestment { get; set; }
		public bool ShowMonthlyImpressions { get; set; }
		public bool ShowTotalImpressions { get; set; }
		public bool ShowComments { get; set; }
		public bool ShowDuration { get; set; }
		public bool ShowCommentText { get; set; }
		public bool ShowStrength1 { get; set; }
		public bool ShowStrength2 { get; set; }
		public bool ShowImages { get; set; }
		public bool ShowScreenshot { get; set; }
		public bool ShowSignature { get; set; }

		public ProductPackageRecord PackageRecord { get; private set; }
		public DigitalProductAdPlanSettings AdPlanSettings { get; set; }

		public bool ShowCPMButton
		{
			get { return (ShowMonthlyImpressions | ShowTotalImpressions) & _showCPMButton; }
			set { _showCPMButton = value; }
		}

		public bool EnableCPMButton
		{
			get { return ShowMonthlyImpressions | ShowTotalImpressions; }
		}

		public bool ShowMonthlyCPM
		{
			get { return ShowMonthlyInvestment & ShowMonthlyImpressions & _showCPMButton; }
		}

		public bool ShowTotalCPM
		{
			get { return ShowTotalInvestment & ShowTotalImpressions & _showCPMButton; }
		}
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

		public string ExtendedName
		{
			get { return String.Format("{0}{1}", !String.IsNullOrEmpty(SubCategory) ? (SubCategory + " - ") : String.Empty, Name); }
		}

		public string WebCategory
		{
			get { return string.Format("{0}{1}", new object[] { Category, !string.IsNullOrEmpty(SubCategory) ? (@"\" + SubCategory) : (ListManager.Instance.ProductSources.Where(x => x.Category.Name.Equals(Category) && !string.IsNullOrEmpty(x.SubCategory)).Count() > 0 ? " (select)" : string.Empty) }); }
			set { }
		}

		public string RateTypeText
		{
			get
			{
				switch (RateType)
				{
					case RateType.CPM:
						return "CPM";
					case RateType.Fixed:
						return "Fixed";
					default:
						return string.Empty;
				}
			}
			set
			{
				switch (value)
				{
					case "CPM":
						RateType = RateType.CPM;
						break;
					case "Fixed":
						RateType = RateType.Fixed;
						break;
				}
			}
		}

		public string Dimensions
		{
			get
			{
				if (Width.HasValue && Height.HasValue)
					return Width.Value.ToString() + " x " + Height.Value.ToString();
				return string.Empty;
			}
		}

		public double? MonthlyInvestmentCalculated
		{
			get
			{
				if (Formula == FormulaType.Investment)
					return MonthlyImpressions.HasValue && MonthlyCPMCalculated.HasValue ? Math.Round(MonthlyCPMCalculated.Value * (MonthlyImpressions.Value / 1000.00), 2) : (double?)null;
				return MonthlyInvestment;
			}
		}

		public double? TotalInvestmentCalculated
		{
			get
			{
				if (Formula == FormulaType.Investment)
					return TotalImpressions.HasValue && TotalCPMCalculated.HasValue ? Math.Round((TotalCPMCalculated.Value * (TotalImpressions.Value / 1000.00)), 2) : (double?)null;
				return TotalInvestment;
			}
		}

		public double? MonthlyImpressionsCalculated
		{
			get
			{
				if (Formula == FormulaType.Impressions)
					return MonthlyCPMCalculated.HasValue && MonthlyInvestment.HasValue ? (MonthlyCPMCalculated.Value != 0 ? Math.Round(((MonthlyInvestment.Value * 1000) / MonthlyCPMCalculated.Value), 0) : (double?)null) : null;
				return MonthlyImpressions;
			}
		}

		public double? TotalImpressionsCalculated
		{
			get
			{
				if (Formula == FormulaType.Impressions)
					return TotalCPMCalculated.HasValue && TotalInvestment.HasValue ? (TotalCPMCalculated.Value != 0 ? Math.Round(((TotalInvestment.Value * 1000) / TotalCPMCalculated.Value), 0) : (double?)null) : null;
				return TotalImpressions;
			}
		}

		public double? MonthlyCPMCalculated
		{
			get
			{
				if (Formula == FormulaType.CPM)
					return MonthlyImpressions.HasValue && MonthlyInvestment.HasValue ? (MonthlyImpressions.Value != 0 ? Math.Round(MonthlyInvestment.Value / (MonthlyImpressions.Value / 1000.00), 2) : (double?)null) : null;
				return !MonthlyCPM.HasValue && RateType == RateType.CPM ? DefaultRate : MonthlyCPM;
			}
		}

		public double? TotalCPMCalculated
		{
			get
			{
				if (Formula == FormulaType.CPM)
					return TotalImpressions.HasValue && TotalInvestment.HasValue ? (TotalImpressions.Value != 0 ? Math.Round((TotalInvestment.Value / (TotalImpressions.Value / 1000.00)), 2) : (double?)null) : null;
				return !TotalCPM.HasValue && RateType == RateType.CPM ? DefaultRate : TotalCPM;
			}
		}

		public int WeeksDuration
		{
			get
			{
				int result = 0;
				TimeSpan diff = Parent.FlightDateEnd.Subtract(Parent.FlightDateStart);
				result = diff.Days / 7;
				return result;
			}
		}

		public int MonthDuraton
		{
			get { return Math.Abs((Parent.FlightDateEnd.Month - Parent.FlightDateStart.Month) + 12 * (Parent.FlightDateEnd.Year - Parent.FlightDateStart.Year)); }
		}

		public IEnumerable<string> AllWebsites
		{
			get
			{
				var websites = new List<string>();
				websites.AddRange(Websites);
				if (ShowCustomWebsite1)
					websites.Add(CustomWebsite1);
				if (ShowCustomWebsite2)
					websites.Add(CustomWebsite2);
				if (ShowCustomWebsite3)
					websites.Add(CustomWebsite3);
				if (ShowCustomWebsite4)
					websites.Add(CustomWebsite4);
				return websites;
			}
		}

		public string ProductSummary
		{
			get
			{
				var result = new List<string>();
				if (MonthlyImpressionsCalculated.HasValue)
					result.Add(String.Format("Monthly Impressions: {0}", MonthlyImpressionsCalculated.Value.ToString("#,##0")));
				if (MonthlyCPMCalculated.HasValue)
					result.Add(String.Format("Monthly CPM: {0}", MonthlyCPMCalculated.Value.ToString("$#,###.00")));
				if (MonthlyInvestmentCalculated.HasValue)
					result.Add(String.Format("Monthly Investment: {0}", MonthlyInvestmentCalculated.Value.ToString("$#,###.00")));
				if (TotalImpressionsCalculated.HasValue)
					result.Add(String.Format("Total Impressions: {0}", TotalImpressionsCalculated.Value.ToString("#,##0")));
				if (TotalCPMCalculated.HasValue)
					result.Add(String.Format("Total CPM: {0}", TotalCPMCalculated.Value.ToString("$#,###.00")));
				if (TotalInvestmentCalculated.HasValue)
					result.Add(String.Format("Total Investment: {0}", TotalInvestmentCalculated.Value.ToString("$#,###.00")));
				result.Add(String.Format("Ad Campaign Flight Dates: {0}", Parent.FlightDates));
				return String.Join(", ", result.ToArray());
			}
		}
		#endregion

		public DigitalProduct(ISchedule parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Category = string.Empty;
			SubCategory = string.Empty;
			Description = string.Empty;
			Websites = new List<string>();
			CustomWebsite1 = string.Empty;
			CustomWebsite2 = string.Empty;
			CustomWebsite3 = string.Empty;
			CustomWebsite4 = string.Empty;
			Strength1 = string.Empty;
			Strength2 = string.Empty;
			Comment = string.Empty;
			DurationType = string.Empty;
			SlideHeader = string.Empty;

			DefaultShowPresentationDate = true;
			DefaultShowBusinessName = true;
			DefaultShowDecisionMaker = true;
			DefaultShowWebsite = true;
			DefaultShowCustomWebsite1 = false;
			DefaultShowCustomWebsite2 = false;
			DefaultShowCustomWebsite3 = false;
			DefaultShowCustomWebsite4 = false;
			DefaultShowProduct = true;
			DefaultShowDescription = true;
			DefaultShowDimensions = true;
			DefaultShowFlightDates = true;
			DefaultShowActiveDays = false;
			DefaultShowTotalAds = false;
			DefaultShowAdRate = false;
			DefaultShowMonthlyInvestment = true;
			DefaultShowTotalInvestment = false;
			DefaultShowMonthlyImpressions = true;
			DefaultShowTotalImpressions = false;
			DefaultShowComments = true;
			DefaultShowDuration = true;
			DefaultShowCommentText = true;
			DefaultShowStrength1 = true;
			DefaultShowStrength2 = true;
			DefaultShowImages = true;
			DefaultShowScreenshot = false;
			DefaultShowSignature = true;

			PackageRecord = new ProductPackageRecord(this);
			AdPlanSettings = new DigitalProductAdPlanSettings();

			ApplyDefaultView();
		}

		public string Serialize()
		{
			var xml = new StringBuilder();

			xml.Append(@"<Product ");

			#region Basic Properties
			xml.Append("Name = \"" + Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("UniqueID = \"" + UniqueID.ToString() + "\" ");
			xml.Append("Index = \"" + Index + "\" ");
			xml.Append("Category = \"" + Category.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("SubCategory = \"" + SubCategory.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("RateType = \"" + (int)RateType + "\" ");
			xml.Append("Width = \"" + (Width.HasValue ? Width.Value.ToString() : string.Empty) + "\" ");
			xml.Append("Height = \"" + (Height.HasValue ? Height.Value.ToString() : string.Empty) + "\" ");
			xml.Append("Description = \"" + Description.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			#endregion

			#region Additional Properties
			xml.Append("UserDefinedName = \"" + (String.IsNullOrEmpty(UserDefinedName) ? ExtendedName : UserDefinedName).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("SlideHeader = \"" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("CustomWebsite1 = \"" + CustomWebsite1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("CustomWebsite2 = \"" + CustomWebsite2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("CustomWebsite3 = \"" + CustomWebsite3.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("CustomWebsite4 = \"" + CustomWebsite4.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("ActiveDays = \"" + (ActiveDays.HasValue ? ActiveDays.Value.ToString() : string.Empty) + "\" ");
			xml.Append("TotalAds = \"" + (TotalAds.HasValue ? TotalAds.Value.ToString() : string.Empty) + "\" ");
			xml.Append("DurationType = \"" + DurationType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("DurationValue = \"" + (DurationValue.HasValue ? DurationValue.Value.ToString() : string.Empty) + "\" ");
			xml.Append("Strength1 = \"" + Strength1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("Strength2 = \"" + Strength2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("Comment = \"" + Comment.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("AdRate = \"" + (AdRate.HasValue ? AdRate.Value.ToString() : string.Empty) + "\" ");
			xml.Append("MonthlyInvestment = \"" + (MonthlyInvestment.HasValue ? MonthlyInvestment.Value.ToString() : string.Empty) + "\" ");
			xml.Append("MonthlyImpressions = \"" + (MonthlyImpressions.HasValue ? MonthlyImpressions.Value.ToString() : string.Empty) + "\" ");
			xml.Append("MonthlyCPM = \"" + (MonthlyCPM.HasValue ? MonthlyCPM.Value.ToString() : string.Empty) + "\" ");
			xml.Append("TotalInvestment = \"" + (TotalInvestment.HasValue ? TotalInvestment.Value.ToString() : string.Empty) + "\" ");
			xml.Append("TotalImpressions = \"" + (TotalImpressions.HasValue ? TotalImpressions.Value.ToString() : string.Empty) + "\" ");
			xml.Append("TotalCPM = \"" + (TotalCPM.HasValue ? TotalCPM.Value.ToString() : string.Empty) + "\" ");
			xml.Append("DefaultRate = \"" + (DefaultRate.HasValue ? DefaultRate.Value.ToString() : string.Empty) + "\" ");
			xml.Append("Formula = \"" + (int)Formula + "\" ");
			#endregion

			#region Show Properties
			xml.Append("ShowActiveDays = \"" + ShowActiveDays.ToString() + "\" ");
			xml.Append("ShowAdRate = \"" + ShowAdRate.ToString() + "\" ");
			xml.Append("ShowBusinessName = \"" + ShowBusinessName.ToString() + "\" ");
			xml.Append("ShowCommentText = \"" + ShowCommentText.ToString() + "\" ");
			xml.Append("ShowComments = \"" + ShowComments.ToString() + "\" ");
			xml.Append("ShowCPMButton = \"" + ShowCPMButton.ToString() + "\" ");
			xml.Append("ShowDecisionMaker = \"" + ShowDecisionMaker.ToString() + "\" ");
			xml.Append("ShowDescription = \"" + ShowDescription.ToString() + "\" ");
			xml.Append("ShowDimensions = \"" + ShowDimensions.ToString() + "\" ");
			xml.Append("ShowFlightDates = \"" + ShowFlightDates.ToString() + "\" ");
			xml.Append("ShowMonthlyImpressions = \"" + ShowMonthlyImpressions.ToString() + "\" ");
			xml.Append("ShowMonthlyInvestment = \"" + ShowMonthlyInvestment.ToString() + "\" ");
			xml.Append("ShowPresentationDate = \"" + ShowPresentationDate.ToString() + "\" ");
			xml.Append("ShowProduct = \"" + ShowProduct.ToString() + "\" ");
			xml.Append("ShowStrength1 = \"" + ShowStrength1.ToString() + "\" ");
			xml.Append("ShowStrength2 = \"" + ShowStrength2.ToString() + "\" ");
			xml.Append("ShowTotalAds = \"" + ShowTotalAds.ToString() + "\" ");
			xml.Append("ShowTotalImpressions = \"" + ShowTotalImpressions.ToString() + "\" ");
			xml.Append("ShowTotalInvestment = \"" + ShowTotalInvestment.ToString() + "\" ");
			xml.Append("ShowDuration = \"" + ShowDuration.ToString() + "\" ");
			xml.Append("ShowWebsite = \"" + ShowWebsite.ToString() + "\" ");
			xml.Append("ShowCustomWebsite1 = \"" + ShowCustomWebsite1.ToString() + "\" ");
			xml.Append("ShowCustomWebsite2 = \"" + ShowCustomWebsite2.ToString() + "\" ");
			xml.Append("ShowCustomWebsite3 = \"" + ShowCustomWebsite3.ToString() + "\" ");
			xml.Append("ShowCustomWebsite4 = \"" + ShowCustomWebsite4.ToString() + "\" ");
			xml.Append("ShowImages = \"" + ShowImages.ToString() + "\" ");
			xml.Append("ShowScreenshot = \"" + ShowScreenshot.ToString() + "\" ");
			xml.Append("ShowSignature = \"" + ShowSignature.ToString() + "\" ");
			#endregion

			xml.AppendLine(@">");
			foreach (string website in Websites)
				xml.AppendLine(@"<Website>" + website.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Website>");

			xml.AppendLine(@"<PackageRecord>" + PackageRecord.Serialize() + @"</PackageRecord>");
			xml.AppendLine(@"<AdPlanSettings>" + AdPlanSettings.Serialize() + @"</AdPlanSettings>");

			xml.AppendLine(@"</Product>");

			return xml.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			int tempInt;
			bool tempBool;
			double tempDouble;
			Guid tempGuid;

			foreach (XmlAttribute productAttribute in node.Attributes)
				switch (productAttribute.Name)
				{
					#region Basic Properties
					case "Name":
						Name = productAttribute.Value;
						break;
					case "UniqueID":
						if (Guid.TryParse(productAttribute.Value, out tempGuid))
							UniqueID = tempGuid;
						break;
					case "Index":
						if (int.TryParse(productAttribute.Value, out tempInt))
							Index = tempInt;
						break;
					case "Category":
						Category = productAttribute.Value;
						break;
					case "SubCategory":
						SubCategory = productAttribute.Value;
						break;
					case "RateType":
						if (int.TryParse(productAttribute.Value, out tempInt))
							RateType = (RateType)tempInt;
						break;
					case "Width":
						if (int.TryParse(productAttribute.Value, out tempInt))
							Width = tempInt;
						else
							Width = null;
						break;
					case "Height":
						if (int.TryParse(productAttribute.Value, out tempInt))
							Height = tempInt;
						else
							Height = null;
						break;
					case "Description":
						Description = productAttribute.Value;
						break;
					#endregion

					#region Additional Properties
					case "UserDefinedName":
						UserDefinedName = productAttribute.Value;
						break;
					case "SlideHeader":
						SlideHeader = productAttribute.Value;
						break;
					case "CustomWebsite1":
						CustomWebsite1 = productAttribute.Value;
						break;
					case "CustomWebsite2":
						CustomWebsite2 = productAttribute.Value;
						break;
					case "CustomWebsite3":
						CustomWebsite3 = productAttribute.Value;
						break;
					case "CustomWebsite4":
						CustomWebsite4 = productAttribute.Value;
						break;
					case "ActiveDays":
						if (int.TryParse(productAttribute.Value, out tempInt))
							ActiveDays = tempInt;
						else
							ActiveDays = null;
						break;
					case "TotalAds":
						if (int.TryParse(productAttribute.Value, out tempInt))
							TotalAds = tempInt;
						else
							TotalAds = null;
						break;
					case "DurationType":
						DurationType = productAttribute.Value;
						break;
					case "DurationValue":
						if (int.TryParse(productAttribute.Value, out tempInt))
							DurationValue = tempInt;
						else
							DurationValue = null;
						break;
					case "Strength1":
						Strength1 = productAttribute.Value;
						break;
					case "Strength2":
						Strength2 = productAttribute.Value;
						break;
					case "Comment":
						Comment = productAttribute.Value;
						break;
					case "AdRate":
						if (double.TryParse(productAttribute.Value, out tempDouble))
							AdRate = tempDouble;
						else
							AdRate = null;
						break;
					case "MonthlyInvestment":
						if (double.TryParse(productAttribute.Value, out tempDouble))
							MonthlyInvestment = tempDouble;
						else
							MonthlyInvestment = null;
						break;
					case "MonthlyImpressions":
						if (double.TryParse(productAttribute.Value, out tempDouble))
							MonthlyImpressions = tempDouble;
						else
							MonthlyImpressions = null;
						break;
					case "MonthlyCPM":
						if (double.TryParse(productAttribute.Value, out tempDouble))
							MonthlyCPM = tempDouble;
						else
							MonthlyCPM = null;
						break;
					case "TotalInvestment":
						if (double.TryParse(productAttribute.Value, out tempDouble))
							TotalInvestment = tempDouble;
						else
							TotalInvestment = null;
						break;
					case "TotalImpressions":
						if (double.TryParse(productAttribute.Value, out tempDouble))
							TotalImpressions = tempDouble;
						else
							TotalImpressions = null;
						break;
					case "TotalCPM":
						if (double.TryParse(productAttribute.Value, out tempDouble))
							TotalCPM = tempDouble;
						else
							TotalCPM = null;
						break;
					case "DefaultRate":
						if (double.TryParse(productAttribute.Value, out tempDouble))
							DefaultRate = tempDouble;
						else
							DefaultRate = null;
						break;

					case "Formula":
						if (int.TryParse(productAttribute.Value, out tempInt))
							Formula = (FormulaType)tempInt;
						break;
					#endregion

					#region Show Properties
					case "ShowBusinessName":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowBusinessName = tempBool;
						break;
					case "ShowDecisionMaker":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowDecisionMaker = tempBool;
						break;
					case "ShowPresentationDate":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowPresentationDate = tempBool;
						break;
					case "ShowWebsite":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowWebsite = tempBool;
						break;
					case "ShowCustomWebsite1":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowCustomWebsite1 = tempBool;
						break;
					case "ShowCustomWebsite2":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowCustomWebsite2 = tempBool;
						break;
					case "ShowCustomWebsite3":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowCustomWebsite3 = tempBool;
						break;
					case "ShowCustomWebsite4":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowCustomWebsite4 = tempBool;
						break;
					case "ShowActiveDays":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowActiveDays = tempBool;
						break;
					case "ShowAdRate":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowAdRate = tempBool;
						break;
					case "ShowComments":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowComments = tempBool;
						break;
					case "ShowCommentText":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowCommentText = tempBool;
						break;
					case "ShowCPMButton":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowCPMButton = tempBool;
						break;
					case "ShowDescription":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowDescription = tempBool;
						break;
					case "ShowDimensions":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowDimensions = tempBool;
						break;
					case "ShowFlightDates":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowFlightDates = tempBool;
						break;
					case "ShowMonthlyImpressions":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowMonthlyImpressions = tempBool;
						break;
					case "ShowMonthlyInvestment":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowMonthlyInvestment = tempBool;
						break;
					case "ShowProduct":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowProduct = tempBool;
						break;
					case "ShowStrength1":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowStrength1 = tempBool;
						break;
					case "ShowStrength2":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowStrength2 = tempBool;
						break;
					case "ShowTotalAds":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowTotalAds = tempBool;
						break;
					case "ShowTotalImpressions":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowTotalImpressions = tempBool;
						break;
					case "ShowTotalInvestment":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowTotalInvestment = tempBool;
						break;
					case "ShowDuration":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowDuration = tempBool;
						break;
					case "ShowImages":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowImages = tempBool;
						break;
					case "ShowScreenshot":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowScreenshot = tempBool;
						break;
					case "ShowSignature":
						if (bool.TryParse(productAttribute.Value, out tempBool))
							ShowSignature = tempBool;
						break;
					#endregion
				}
			Websites.Clear();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				if (childNode.Name.Equals("Website"))
					Websites.Add(childNode.InnerText);
				else if (childNode.Name.Equals("PackageRecord"))
					PackageRecord.Deserialize(childNode);
				else if (childNode.Name.Equals("AdPlanSettings"))
					AdPlanSettings.Deserialize(childNode);
			}
			if (Websites.Count == 0 && ListManager.Instance.Websites.Any())
				Websites.Add(ListManager.Instance.Websites.FirstOrDefault());
			if (String.IsNullOrEmpty(UserDefinedName))
				UserDefinedName = ExtendedName;
		}

		public void ApplyDefaultValues()
		{
			ProductSource source = ListManager.Instance.ProductSources.Where(x => x.Name.Equals(_name) && x.Category.Name.Equals(Category) && (x.SubCategory.Equals(SubCategory) || string.IsNullOrEmpty(SubCategory))).FirstOrDefault();
			if (source != null)
			{
				RateType = source.RateType;
				DefaultRate = source.Rate;
				Width = source.Width;
				Height = source.Height;
				Description = source.Overview;
			}
		}

		public void ApplyDefaultView()
		{
			_showCPMButton = _defaultShowCPMButton;
			ShowPresentationDate = DefaultShowPresentationDate;
			ShowBusinessName = DefaultShowBusinessName;
			ShowDecisionMaker = DefaultShowDecisionMaker;
			ShowWebsite = DefaultShowWebsite;
			ShowCustomWebsite1 = DefaultShowCustomWebsite1;
			ShowCustomWebsite2 = DefaultShowCustomWebsite2;
			ShowCustomWebsite3 = DefaultShowCustomWebsite3;
			ShowCustomWebsite4 = DefaultShowCustomWebsite4;
			ShowProduct = DefaultShowProduct;
			ShowDescription = DefaultShowDescription;
			ShowDimensions = DefaultShowDimensions;
			ShowFlightDates = DefaultShowFlightDates;
			ShowActiveDays = DefaultShowActiveDays;
			ShowTotalAds = DefaultShowTotalAds;
			ShowAdRate = DefaultShowAdRate;
			ShowMonthlyInvestment = DefaultShowMonthlyInvestment;
			ShowTotalInvestment = DefaultShowTotalInvestment;
			ShowMonthlyImpressions = DefaultShowMonthlyImpressions;
			ShowTotalImpressions = DefaultShowTotalImpressions;
			ShowComments = DefaultShowComments;
			ShowDuration = DefaultShowDuration;
			ShowCommentText = DefaultShowCommentText;
			ShowStrength1 = DefaultShowStrength1;
			ShowStrength2 = DefaultShowStrength2;
			ShowImages = DefaultShowImages;
			ShowScreenshot = DefaultShowScreenshot;
			ShowSignature = DefaultShowSignature;

			Formula = ListManager.Instance.DefaultFormula;
		}

		public string GetSlideSource(string outputTemplateFolderPath)
		{
			string templateName = string.Empty;
			SlideSource slideSource = ListManager.Instance.SlideSources.FirstOrDefault(x => x.ShowActiveDays == ShowActiveDays &&
																							x.ShowAdRate == ShowAdRate &&
																							x.ShowBusinessName == ShowBusinessName &&
																							x.ShowComments == ShowComments &&
																							x.ShowDecisionMaker == ShowDecisionMaker &&
																							x.ShowDescription == ShowDescription &&
																							x.ShowDimensions == ShowDimensions &&
																							x.ShowDuration == (ShowDuration & ShowFlightDates) &&
																							x.ShowFlightDates == ShowFlightDates &&
																							x.ShowImages == ShowImages &&
																							x.ShowMonthlyCPM == ShowMonthlyCPM &&
																							x.ShowMonthlyImpressions == ShowMonthlyImpressions &&
																							x.ShowMonthlyInvestment == ShowMonthlyInvestment &&
																							x.ShowPresentationDate == ShowPresentationDate &&
																							x.ShowProduct == ShowProduct &&
																							x.ShowScreenshot == ShowScreenshot &&
																							x.ShowSignature == ShowSignature &&
																							x.ShowTotalAds == ShowTotalAds &&
																							x.ShowTotalCPM == ShowTotalCPM &&
																							x.ShowTotalImpressions == ShowTotalImpressions &&
																							x.ShowTotalInvestment == ShowTotalInvestment &&
																							x.ShowWebsite == ShowWebsite);
			if (slideSource != null)
				templateName = Path.Combine(outputTemplateFolderPath, slideSource.TemplateName);
			return templateName;
		}
	}

	public class ProductPackageRecord
	{

		private string _category;
		private string _subCategory;
		private string _name;
		private string _info;
		private string _comments;
		private decimal? _rate;
		private decimal? _investment;
		private decimal? _impressions;
		private decimal? _cpm;

		public DigitalProduct Parent { get; private set; }
		public bool UseFormula { get; set; }

		public string Category
		{
			get
			{
				if (String.IsNullOrEmpty(_category))
					return Parent.Category;
				return _category;
			}
			set
			{
				if (!value.Equals(Parent.Category))
					_category = value;
			}
		}
		public string SubCategory
		{
			get
			{
				if (String.IsNullOrEmpty(_subCategory))
					return Parent.SubCategory;
				return _subCategory;
			}
			set
			{
				if (!value.Equals(Parent.SubCategory))
					_subCategory = value;
			}
		}
		public string Name
		{
			get
			{
				if (String.IsNullOrEmpty(_name))
					return Parent.Name;
				return _name;
			}
			set
			{
				if (!value.Equals(Parent.Name))
					_name = value;
			}
		}
		public string Info
		{
			get
			{
				return _info;
			}
			set
			{
				_info = value;
			}
		}
		public string Comments
		{
			get
			{
				return _comments;
			}
			set
			{
				_comments = value;
			}
		}
		public decimal? Rate
		{
			get
			{
				return _rate;
			}
			set
			{
				_rate = value;
			}
		}
		public decimal? Investment
		{
			get
			{
				return _investment;
			}
			set
			{
				_investment = value;
			}
		}
		public decimal? InvestmentCalculated
		{
			get
			{
				if (UseFormula && (Parent.Parent.CommonViewSettings.DigitalPackageSettings.ShowInvestment && Parent.Parent.CommonViewSettings.DigitalPackageSettings.ShowImpressions && Parent.Parent.CommonViewSettings.DigitalPackageSettings.ShowCPM) && Parent.Parent.CommonViewSettings.DigitalPackageSettings.Formula == FormulaType.Investment)
					Investment = CPMCalculated.HasValue && ImpressionsCalculated.HasValue ? Math.Round(CPMCalculated.Value * (ImpressionsCalculated.Value / 1000), 2) : (decimal?)null;
				return Investment;
			}
			set
			{
				Investment = value;
			}
		}
		public decimal? Impressions
		{
			get
			{
				return _impressions;
			}
			set
			{
				_impressions = value;
			}
		}
		public decimal? ImpressionsCalculated
		{
			get
			{
				if (UseFormula && (Parent.Parent.CommonViewSettings.DigitalPackageSettings.ShowInvestment && Parent.Parent.CommonViewSettings.DigitalPackageSettings.ShowImpressions && Parent.Parent.CommonViewSettings.DigitalPackageSettings.ShowCPM) && Parent.Parent.CommonViewSettings.DigitalPackageSettings.Formula == FormulaType.Impressions)
					Impressions = InvestmentCalculated.HasValue && CPMCalculated.HasValue && CPMCalculated.Value != 0 ? Math.Round(((InvestmentCalculated.Value * 1000) / CPMCalculated.Value), 0) : (decimal?)null;
				return Impressions;
			}
			set
			{
				Impressions = value;
			}
		}
		public decimal? CPM
		{
			get
			{
				return _cpm;
			}
			set
			{
				_cpm = value;
			}
		}
		public decimal? CPMCalculated
		{
			get
			{
				if (UseFormula && (Parent.Parent.CommonViewSettings.DigitalPackageSettings.ShowInvestment && Parent.Parent.CommonViewSettings.DigitalPackageSettings.ShowImpressions && Parent.Parent.CommonViewSettings.DigitalPackageSettings.ShowCPM) && Parent.Parent.CommonViewSettings.DigitalPackageSettings.Formula == FormulaType.CPM)
					CPM = InvestmentCalculated.HasValue && ImpressionsCalculated.HasValue && ImpressionsCalculated.Value != 0 ? Math.Round((InvestmentCalculated.Value / (ImpressionsCalculated.Value / 1000)), 2) : (decimal?)null;
				return CPM;
			}
			set
			{
				CPM = value;
			}
		}

		public ProductPackageRecord(DigitalProduct parent)
		{
			Parent = parent;
			ResetToDefault();
		}

		public string Serialize()
		{
			var xml = new StringBuilder();

			if (!String.IsNullOrEmpty(_category))
				xml.AppendLine(@"<Category>" + _category.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Category>");
			if (!String.IsNullOrEmpty(_subCategory))
				xml.AppendLine(@"<SubCategory>" + _subCategory.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SubCategory>");
			if (!String.IsNullOrEmpty(_name))
				xml.AppendLine(@"<Name>" + _name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Name>");
			if (!String.IsNullOrEmpty(_info))
				xml.AppendLine(@"<Info>" + _info.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Info>");
			if (!String.IsNullOrEmpty(_comments))
				xml.AppendLine(@"<Comments>" + _comments.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Comments>");
			if (_rate.HasValue)
				xml.AppendLine(@"<Rate>" + _rate.Value + @"</Rate>");
			if (Investment.HasValue)
				xml.AppendLine(@"<Investment>" + Investment.Value + @"</Investment>");
			if (Impressions.HasValue)
				xml.AppendLine(@"<Impressions>" + Impressions.Value + @"</Impressions>");
			if (CPM.HasValue)
				xml.AppendLine(@"<CPM>" + CPM.Value + @"</CPM>");
			xml.AppendLine(@"<UseFormula>" + UseFormula + @"</UseFormula>");

			return xml.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			decimal tempDecimal;
			bool tempBool;

			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "Category":
						_category = childNode.InnerText;
						break;
					case "SubCategory":
						_subCategory = childNode.InnerText;
						break;
					case "Name":
						_name = childNode.InnerText;
						break;
					case "Info":
						_info = childNode.InnerText;
						break;
					case "Comments":
						_comments = childNode.InnerText;
						break;
					case "Rate":
						if (Decimal.TryParse(childNode.InnerText, out tempDecimal))
							_rate = tempDecimal;
						break;
					case "Investment":
						if (Decimal.TryParse(childNode.InnerText, out tempDecimal))
							_investment = tempDecimal;
						break;
					case "Impressions":
						if (Decimal.TryParse(childNode.InnerText, out tempDecimal))
							_impressions = tempDecimal;
						break;
					case "CPM":
						if (Decimal.TryParse(childNode.InnerText, out tempDecimal))
							_cpm = tempDecimal;
						break;
					case "UseFormula":
						if (Boolean.TryParse(childNode.InnerText, out tempBool))
							UseFormula = tempBool;
						break;
				}
		}

		public void ResetToDefault()
		{
			_category = null;
			_subCategory = null;
			_name = null;
			_info = null;
			_comments = null;
			_rate = null;
			_investment = null;
			_impressions = null;
			_cpm = null;
			UseFormula = true;
		}
	}

	public class ProductSource
	{
		public ProductSource()
		{
			Name = string.Empty;
			SubCategory = string.Empty;
			Overview = string.Empty;
		}

		public string Name { get; set; }
		public Category Category { get; set; }
		public string SubCategory { get; set; }
		public RateType RateType { get; set; }
		public double? Rate { get; set; }
		public int? Width { get; set; }
		public int? Height { get; set; }
		public string Overview { get; set; }
	}

	public class Category
	{
		public string Name { get; set; }
		public Image Logo { get; set; }
		public string TooltipTitle { get; set; }
		public string TooltipValue { get; set; }
	}

	public class SlideSource
	{
		public SlideSource()
		{
			TemplateName = string.Empty;
		}

		public string TemplateName { get; set; }
		public bool ShowWebsite { get; set; }
		public bool ShowBusinessName { get; set; }
		public bool ShowDecisionMaker { get; set; }
		public bool ShowPresentationDate { get; set; }
		public bool ShowProduct { get; set; }
		public bool ShowDescription { get; set; }
		public bool ShowDimensions { get; set; }
		public bool ShowFlightDates { get; set; }
		public bool ShowActiveDays { get; set; }
		public bool ShowTotalAds { get; set; }
		public bool ShowAdRate { get; set; }
		public bool ShowMonthlyInvestment { get; set; }
		public bool ShowTotalInvestment { get; set; }
		public bool ShowMonthlyImpressions { get; set; }
		public bool ShowTotalImpressions { get; set; }
		public bool ShowMonthlyCPM { get; set; }
		public bool ShowTotalCPM { get; set; }
		public bool ShowComments { get; set; }
		public bool ShowDuration { get; set; }
		public bool ShowImages { get; set; }
		public bool ShowScreenshot { get; set; }
		public bool ShowSignature { get; set; }
	}
}