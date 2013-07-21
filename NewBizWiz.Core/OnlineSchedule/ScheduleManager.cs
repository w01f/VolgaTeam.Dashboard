using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
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

		public event EventHandler<SavingingEventArgs> SettingsSaved;

		public void OpenSchedule(string scheduleName, bool create)
		{
			string scheduleFilePath = GetScheduleFileName(scheduleName);
			if (create && File.Exists(scheduleFilePath))
				if (Utilities.Instance.ShowWarningQuestion(string.Format("An older Schedule is already saved with this same file name.\nDo you want to replace this file with a newer schedule?", scheduleName)) == DialogResult.Yes)
					File.Delete(scheduleFilePath);
			_currentSchedule = new Schedule(scheduleFilePath);
		}

		public void OpenSchedule(string scheduleFilePath)
		{
			_currentSchedule = new Schedule(scheduleFilePath);
		}

		public string GetScheduleFileName(string scheduleName)
		{
			return Path.Combine(SettingsManager.Instance.SaveFolder, scheduleName + ".xml");
		}

		public Schedule GetLocalSchedule()
		{
			return new Schedule(_currentSchedule.ScheduleFile.FullName);
		}

		public void SaveSchedule(Schedule localSchedule, bool quickSave, Control sender)
		{
			localSchedule.Save();
			_currentSchedule = localSchedule;
			if (SettingsSaved != null)
				SettingsSaved(sender, new SavingingEventArgs(quickSave));
		}

		public ShortSchedule[] GetShortScheduleList(DirectoryInfo rootFolder)
		{
			var scheduleList = new List<ShortSchedule>();
			foreach (FileInfo file in rootFolder.GetFiles("*.xml"))
			{
				var schedule = new ShortSchedule(file);
				if (!string.IsNullOrEmpty(schedule.BusinessName))
					scheduleList.Add(schedule);
			}
			return scheduleList.ToArray();
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
			Status = ListManager.Instance.Statuses.FirstOrDefault();
			Products = new List<DigitalProduct>();
			ProductPackage = new ProductPackage(this);
			ProductSummarySettings = new ProductSummarySettings();
			ProductBundleSettings = new ProductBundleSettings();

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
		public string BusinessName { get; set; }
		public string DecisionMaker { get; set; }
		public string Status { get; set; }
		public DateTime PresentationDate { get; set; }
		public bool ApplySettingsForeAllProducts { get; set; }
		public List<DigitalProduct> Products { get; set; }
		public ProductPackage ProductPackage { get; set; }
		public ProductSummarySettings ProductSummarySettings { get; set; }
		public ProductBundleSettings ProductBundleSettings { get; set; }

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

				node = document.SelectSingleNode(@"/Schedule/ProductSummarySettings");
				if (node != null)
					ProductSummarySettings.Deserialize(node);
				node = document.SelectSingleNode(@"/Schedule/ProductBundleSettings");
				if (node != null)
					ProductBundleSettings.Deserialize(node);
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

				node = document.SelectSingleNode(@"/Schedule/ProductPackage");
				if (node != null)
				{
					ProductPackage.Deserialize(node);
				}
			}
			if (string.IsNullOrEmpty(ProductPackage.Description))
				ProductPackage.UpdateWebProducts();
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
			xml.AppendLine(@"<Status>" + (Status != null ? Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
			xml.AppendLine(@"<PresentationDate>" + PresentationDate.ToString() + @"</PresentationDate>");
			xml.AppendLine(@"<FlightDateStart>" + FlightDateStart.ToString() + @"</FlightDateStart>");
			xml.AppendLine(@"<FlightDateEnd>" + FlightDateEnd.ToString() + @"</FlightDateEnd>");
			xml.AppendLine(@"<ApplySettingsForeAllProducts>" + ApplySettingsForeAllProducts.ToString() + @"</ApplySettingsForeAllProducts>");
			xml.AppendLine(@"<ProductSummarySettings>" + ProductSummarySettings.Serialize() + @"</ProductSummarySettings>");
			xml.AppendLine(@"<ProductBundleSettings>" + ProductBundleSettings.Serialize() + @"</ProductBundleSettings>");

			xml.AppendLine(@"<Products>");
			foreach (DigitalProduct product in Products)
			{
				xml.AppendLine(product.Serialize());
			}
			xml.AppendLine(@"</Products>");
			xml.AppendLine(ProductPackage.Serialize());
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


		private bool _showCPMButton = true;
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
			get
			{
				return String.Format("{0}{1}", !String.IsNullOrEmpty(SubCategory) ? (SubCategory + " - ") : String.Empty, Name);
			}
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
				else
					return string.Empty;
			}
		}

		public double? MonthlyInvestmentCalculated
		{
			get
			{
				if (Formula == FormulaType.Investment)
					return MonthlyImpressions.HasValue && MonthlyCPMCalculated.HasValue ? Math.Round(MonthlyCPMCalculated.Value * (MonthlyImpressions.Value / 1000.00), 2) : (double?)null;
				else
					return MonthlyInvestment;
			}
		}

		public double? TotalInvestmentCalculated
		{
			get
			{
				if (Formula == FormulaType.Investment)
					return TotalImpressions.HasValue && TotalCPMCalculated.HasValue ? Math.Round((TotalCPMCalculated.Value * (TotalImpressions.Value / 1000.00)), 2) : (double?)null;
				else
					return TotalInvestment;
			}
		}

		public double? MonthlyImpressionsCalculated
		{
			get
			{
				if (Formula == FormulaType.Impressions)
					return MonthlyCPMCalculated.HasValue && MonthlyInvestment.HasValue ? (MonthlyCPMCalculated.Value != 0 ? Math.Round(((MonthlyInvestment.Value * 1000) / MonthlyCPMCalculated.Value), 0) : (double?)null) : null;
				else
					return MonthlyImpressions;
			}
		}

		public double? TotalImpressionsCalculated
		{
			get
			{
				if (Formula == FormulaType.Impressions)
					return TotalCPMCalculated.HasValue && TotalInvestment.HasValue ? (TotalCPMCalculated.Value != 0 ? Math.Round(((TotalInvestment.Value * 1000) / TotalCPMCalculated.Value), 0) : (double?)null) : null;
				else
					return TotalImpressions;
			}
		}

		public double? MonthlyCPMCalculated
		{
			get
			{
				if (Formula == FormulaType.CPM)
					return MonthlyImpressions.HasValue && MonthlyInvestment.HasValue ? (MonthlyImpressions.Value != 0 ? Math.Round(MonthlyInvestment.Value / (MonthlyImpressions.Value / 1000.00), 2) : (double?)null) : null;
				else
					return !MonthlyCPM.HasValue && RateType == RateType.CPM ? DefaultRate : MonthlyCPM;
			}
		}

		public double? TotalCPMCalculated
		{
			get
			{
				if (Formula == FormulaType.CPM)
					return TotalImpressions.HasValue && TotalInvestment.HasValue ? (TotalImpressions.Value != 0 ? Math.Round((TotalInvestment.Value / (TotalImpressions.Value / 1000.00)), 2) : (double?)null) : null;
				else
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

		public string AllWebsites
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
				return string.Join(", ", websites.ToArray());
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

			ApplyDefaultView();
		}

		public string Serialize()
		{
			var xml = new StringBuilder();

			xml.Append(@"<Product ");

			#region Basic Properties
			xml.Append("Name = \"" + Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
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
			xml.AppendLine(@"</Product>");

			return xml.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			int tempInt;
			bool tempBool;
			double tempDouble;

			foreach (XmlAttribute productAttribute in node.Attributes)
				switch (productAttribute.Name)
				{
					#region Basic Properties
					case "Name":
						Name = productAttribute.Value;
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
			foreach (XmlNode websiteNode in node.ChildNodes)
			{
				if (websiteNode.Name.Equals("Website"))
					Websites.Add(websiteNode.InnerText);
			}
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
			SlideSource slideSource = ListManager.Instance.SlideSources.Where(x => x.ShowActiveDays == ShowActiveDays &&
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
																				   x.ShowWebsite == ShowWebsite).FirstOrDefault();
			if (slideSource != null)
				templateName = Path.Combine(outputTemplateFolderPath, slideSource.TemplateName);
			return templateName;
		}
	}

	public class ProductPackage
	{
		#region Basic Properties
		public string Name { get; set; }
		public Schedule Parent { get; set; }
		public Guid UniqueID { get; set; }
		#endregion

		#region Additional Properties
		public string SlideHeader { get; set; }
		public List<string> Websites { get; set; }
		public string CustomWebsite1 { get; set; }
		public string CustomWebsite2 { get; set; }
		public string Description { get; set; }
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
		public FormulaType Formula { get; set; }
		#endregion

		#region Show Properties
		private bool _defaultShowCPMButton = true;
		public bool DefaultShowPresentationDate { get; set; }
		public bool DefaultShowBusinessName { get; set; }
		public bool DefaultShowDecisionMaker { get; set; }
		public bool DefaultShowWebsite { get; set; }
		public bool DefaultShowCustomWebsite1 { get; set; }
		public bool DefaultShowCustomWebsite2 { get; set; }
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

		private bool _showCPMButton = true;
		public bool ShowPresentationDate { get; set; }
		public bool ShowBusinessName { get; set; }
		public bool ShowDecisionMaker { get; set; }
		public bool ShowWebsite { get; set; }
		public bool ShowCustomWebsite1 { get; set; }
		public bool ShowCustomWebsite2 { get; set; }
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
		public double? MonthlyInvestmentCalculated
		{
			get
			{
				if (Formula == FormulaType.Investment)
					return MonthlyImpressions.HasValue && MonthlyCPM.HasValue ? (MonthlyCPM.Value * (MonthlyImpressions.Value / 1000.00)) : (double?)null;
				else
					return MonthlyInvestment;
			}
		}

		public double? TotalInvestmentCalculated
		{
			get
			{
				if (Formula == FormulaType.Investment)
					return TotalImpressions.HasValue && TotalCPM.HasValue ? (TotalCPM.Value * (TotalImpressions.Value / 1000.00)) : (double?)null;
				else
					return TotalInvestment;
			}
		}

		public double? MonthlyImpressionsCalculated
		{
			get
			{
				if (Formula == FormulaType.Impressions)
					return MonthlyCPM.HasValue && MonthlyInvestment.HasValue ? (MonthlyCPM.Value != 0 ? ((MonthlyInvestment.Value * 1000) / MonthlyCPM.Value) : (double?)null) : null;
				else
					return MonthlyImpressions;
			}
		}

		public double? TotalImpressionsCalculated
		{
			get
			{
				if (Formula == FormulaType.Impressions)
					return TotalCPM.HasValue && TotalInvestment.HasValue ? (TotalCPM.Value != 0 ? ((TotalInvestment.Value * 1000) / TotalCPM.Value) : (double?)null) : null;
				else
					return TotalImpressions;
			}
		}

		public double? MonthlyCPMCalculated
		{
			get
			{
				if (Formula == FormulaType.CPM)
					return MonthlyImpressions.HasValue && MonthlyInvestment.HasValue ? (MonthlyImpressions.Value != 0 ? (MonthlyInvestment.Value / (MonthlyImpressions.Value / 1000.00)) : (double?)null) : null;
				else
					return MonthlyCPM;
			}
		}

		public double? TotalCPMCalculated
		{
			get
			{
				if (Formula == FormulaType.CPM)
					return TotalImpressions.HasValue && TotalInvestment.HasValue ? (TotalImpressions.Value != 0 ? (TotalInvestment.Value / (TotalImpressions.Value / 1000.00)) : (double?)null) : null;
				else
					return TotalCPM;
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

		public string AllWebsites
		{
			get
			{
				var websites = new List<string>();
				websites.AddRange(Websites);
				if (ShowCustomWebsite1)
					websites.Add(CustomWebsite1);
				if (ShowCustomWebsite2)
					websites.Add(CustomWebsite2);
				return string.Join(", ", websites.ToArray());
			}
		}
		#endregion

		public ProductPackage(Schedule parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Name = "Web Products";
			Websites = new List<string>();
			CustomWebsite1 = string.Empty;
			CustomWebsite2 = string.Empty;
			Strength1 = string.Empty;
			Strength2 = string.Empty;
			Comment = string.Empty;
			Description = string.Empty;
			DurationType = string.Empty;
			SlideHeader = string.Empty;

			ApplyDefaultView();
		}

		public string Serialize()
		{
			var xml = new StringBuilder();

			xml.Append(@"<ProductPackage ");

			#region Additional Properties
			xml.Append("SlideHeader = \"" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("CustomWebsite1 = \"" + CustomWebsite1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("CustomWebsite2 = \"" + CustomWebsite2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("ActiveDays = \"" + (ActiveDays.HasValue ? ActiveDays.Value.ToString() : string.Empty) + "\" ");
			xml.Append("TotalAds = \"" + (TotalAds.HasValue ? TotalAds.Value.ToString() : string.Empty) + "\" ");
			xml.Append("Description = \"" + Description.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
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
			xml.Append("ShowFlightDates = \"" + ShowFlightDates.ToString() + "\" ");
			xml.Append("ShowMonthlyImpressions = \"" + ShowMonthlyImpressions.ToString() + "\" ");
			xml.Append("ShowMonthlyInvestment = \"" + ShowMonthlyInvestment.ToString() + "\" ");
			xml.Append("ShowPresentationDate = \"" + ShowPresentationDate.ToString() + "\" ");
			xml.Append("ShowStrength1 = \"" + ShowStrength1.ToString() + "\" ");
			xml.Append("ShowStrength2 = \"" + ShowStrength2.ToString() + "\" ");
			xml.Append("ShowTotalAds = \"" + ShowTotalAds.ToString() + "\" ");
			xml.Append("ShowTotalImpressions = \"" + ShowTotalImpressions.ToString() + "\" ");
			xml.Append("ShowTotalInvestment = \"" + ShowTotalInvestment.ToString() + "\" ");
			xml.Append("ShowDuration = \"" + ShowDuration.ToString() + "\" ");
			xml.Append("ShowWebsite = \"" + ShowWebsite.ToString() + "\" ");
			xml.Append("ShowCustomWebsite1 = \"" + ShowCustomWebsite1.ToString() + "\" ");
			xml.Append("ShowCustomWebsite2 = \"" + ShowCustomWebsite2.ToString() + "\" ");
			xml.Append("ShowImages = \"" + ShowImages.ToString() + "\" ");
			xml.Append("ShowScreenshot = \"" + ShowScreenshot.ToString() + "\" ");
			xml.Append("ShowSignature = \"" + ShowSignature.ToString() + "\" ");
			#endregion

			xml.AppendLine(@">");
			foreach (string website in Websites)
				xml.AppendLine(@"<Website>" + website.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Website>");
			xml.AppendLine(@"</ProductPackage>");

			return xml.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			int tempInt;
			bool tempBool;
			double tempDouble;

			foreach (XmlAttribute productAttribute in node.Attributes)
				switch (productAttribute.Name)
				{
					#region Additional Properties
					case "SlideHeader":
						SlideHeader = productAttribute.Value;
						break;
					case "CustomWebsite1":
						CustomWebsite1 = productAttribute.Value;
						break;
					case "CustomWebsite2":
						CustomWebsite2 = productAttribute.Value;
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
					case "Description":
						Description = productAttribute.Value;
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
			foreach (XmlNode websiteNode in node.ChildNodes)
			{
				if (websiteNode.Name.Equals("Website"))
					Websites.Add(websiteNode.InnerText);
			}
		}

		public void UpdateWebProducts()
		{
			var result = new List<string>();
			if (Parent.Products.Count > 0)
				result.AddRange(Parent.Products.Take(Parent.Products.Count > 6 ? 6 : Parent.Products.Count).Select(x => x.Name));
			Description = result.Count > 0 ? ("Web Package: " + string.Join(", ", result.ToArray())) : string.Empty;
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
			SlideSource slideSource = ListManager.Instance.SlideSources.Where(x => x.ShowActiveDays == ShowActiveDays &&
																				   x.ShowAdRate == ShowAdRate &&
																				   x.ShowBusinessName == ShowBusinessName &&
																				   x.ShowComments == ShowComments &&
																				   x.ShowDecisionMaker == ShowDecisionMaker &&
																				   x.ShowDuration == (ShowDuration & ShowFlightDates) &&
																				   x.ShowFlightDates == ShowFlightDates &&
																				   x.ShowImages == ShowImages &&
																				   x.ShowMonthlyCPM == ShowMonthlyCPM &&
																				   x.ShowMonthlyImpressions == ShowMonthlyImpressions &&
																				   x.ShowMonthlyInvestment == ShowMonthlyInvestment &&
																				   x.ShowPresentationDate == ShowPresentationDate &&
																				   x.ShowProduct &&
																				   x.ShowScreenshot == ShowScreenshot &&
																				   x.ShowSignature == ShowSignature &&
																				   x.ShowTotalAds == ShowTotalAds &&
																				   x.ShowTotalCPM == ShowTotalCPM &&
																				   x.ShowTotalImpressions == ShowTotalImpressions &&
																				   x.ShowTotalInvestment == ShowTotalInvestment &&
																				   x.ShowWebsite == ShowWebsite).FirstOrDefault();
			if (slideSource != null)
				templateName = Path.Combine(outputTemplateFolderPath, slideSource.TemplateName);
			return templateName;
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

	public class ProductSummarySettings
	{
		public ProductSummarySettings()
		{
			ShowWebsites = true;
			ShowDimensions = true;
			ShowImpressions = true;
			ShowTotalAds = true;
			ShowActiveDays = true;
			ShowAdRate = true;
			ShowInvestment = true;
			ShowCPM = true;

			ShowMonthlyImpressions = true;
			ShowTotalImpressions = true;
			ShowMonthlyInvestment = true;
			ShowTotalInvestment = true;

			ShowTotalsOnLastOnly = false;

			SlideHeader = string.Empty;

			#region Output Settings
			TotalHeader1 = string.Empty;
			TotalValue1 = string.Empty;
			TotalHeader2 = string.Empty;
			TotalValue2 = string.Empty;
			TotalHeader3 = string.Empty;
			TotalValue3 = string.Empty;
			TotalHeader4 = string.Empty;
			TotalValue4 = string.Empty;
			#endregion
		}

		public bool ShowWebsites { get; set; }
		public bool ShowDimensions { get; set; }
		public bool ShowImpressions { get; set; }
		public bool ShowTotalAds { get; set; }
		public bool ShowActiveDays { get; set; }
		public bool ShowAdRate { get; set; }
		public bool ShowInvestment { get; set; }
		public bool ShowCPM { get; set; }

		public bool ShowMonthlyImpressions { get; set; }
		public bool ShowTotalImpressions { get; set; }
		public bool ShowMonthlyInvestment { get; set; }
		public bool ShowTotalInvestment { get; set; }

		public bool ShowTotalsOnLastOnly { get; set; }

		public string SlideHeader { get; set; }

		#region Output Settings
		public string TotalHeader1 { get; set; }
		public string TotalValue1 { get; set; }
		public string TotalHeader2 { get; set; }
		public string TotalValue2 { get; set; }
		public string TotalHeader3 { get; set; }
		public string TotalValue3 { get; set; }
		public string TotalHeader4 { get; set; }
		public string TotalValue4 { get; set; }
		#endregion

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<ShowActiveDays>" + ShowActiveDays.ToString() + @"</ShowActiveDays>");
			result.AppendLine(@"<ShowAdRate>" + ShowAdRate.ToString() + @"</ShowAdRate>");
			result.AppendLine(@"<ShowCPM>" + ShowCPM.ToString() + @"</ShowCPM>");
			result.AppendLine(@"<ShowDimensions>" + ShowDimensions.ToString() + @"</ShowDimensions>");
			result.AppendLine(@"<ShowImpressions>" + ShowImpressions.ToString() + @"</ShowImpressions>");
			result.AppendLine(@"<ShowInvestment>" + ShowInvestment.ToString() + @"</ShowInvestment>");
			result.AppendLine(@"<ShowMonthlyImpressions>" + ShowMonthlyImpressions.ToString() + @"</ShowMonthlyImpressions>");
			result.AppendLine(@"<ShowMonthlyInvestment>" + ShowMonthlyInvestment.ToString() + @"</ShowMonthlyInvestment>");
			result.AppendLine(@"<ShowTotalAds>" + ShowTotalAds.ToString() + @"</ShowTotalAds>");
			result.AppendLine(@"<ShowTotalImpressions>" + ShowTotalImpressions.ToString() + @"</ShowTotalImpressions>");
			result.AppendLine(@"<ShowTotalInvestment>" + ShowTotalInvestment.ToString() + @"</ShowTotalInvestment>");
			result.AppendLine(@"<ShowWebsites>" + ShowWebsites.ToString() + @"</ShowWebsites>");
			result.AppendLine(@"<ShowTotalsOnLastOnly>" + ShowTotalsOnLastOnly.ToString() + @"</ShowTotalsOnLastOnly>");
			result.AppendLine(@"<SlideHeader>" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "ShowActiveDays":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowActiveDays = tempBool;
						break;
					case "ShowAdRate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAdRate = tempBool;
						break;
					case "ShowCPM":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCPM = tempBool;
						break;
					case "ShowDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDimensions = tempBool;
						break;
					case "ShowImpressions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowImpressions = tempBool;
						break;
					case "ShowInvestment":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowInvestment = tempBool;
						break;
					case "ShowMonthlyImpressions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowMonthlyImpressions = tempBool;
						break;
					case "ShowMonthlyInvestment":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowMonthlyInvestment = tempBool;
						break;
					case "ShowTotalAds":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalAds = tempBool;
						break;
					case "ShowTotalImpressions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalImpressions = tempBool;
						break;
					case "ShowTotalInvestment":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalInvestment = tempBool;
						break;
					case "ShowWebsites":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowWebsites = tempBool;
						break;
					case "ShowTotalsOnLastOnly":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalsOnLastOnly = tempBool;
						break;
					case "SlideHeader":
						SlideHeader = childNode.InnerText;
						break;
				}
			}
		}
	}

	public class ProductBundleSettings
	{
		public ProductBundleSettings()
		{
			ShowWebsites = true;
			ShowDimensions = true;
			ShowTotalAds = true;
			ShowActiveDays = true;
			ShowAdRate = true;

			ShowMonthlyImpressions = true;
			ShowTotalImpressions = true;
			ShowMonthlyInvestment = true;
			ShowTotalInvestment = true;

			ShowTotalsOnLastOnly = false;

			SlideHeader = string.Empty;

			#region Output Settings
			TotalHeader1 = string.Empty;
			TotalValue1 = string.Empty;
			TotalCPM1 = string.Empty;
			TotalHeader2 = string.Empty;
			TotalValue2 = string.Empty;
			TotalCPM2 = string.Empty;
			TotalHeader3 = string.Empty;
			TotalValue3 = string.Empty;
			TotalCPM3 = string.Empty;
			TotalHeader4 = string.Empty;
			TotalValue4 = string.Empty;
			TotalCPM4 = string.Empty;
			#endregion
		}

		public bool ShowWebsites { get; set; }
		public bool ShowDimensions { get; set; }
		public bool ShowTotalAds { get; set; }
		public bool ShowActiveDays { get; set; }
		public bool ShowAdRate { get; set; }

		public bool ShowMonthlyImpressions { get; set; }
		public bool ShowTotalImpressions { get; set; }
		public bool ShowMonthlyInvestment { get; set; }
		public bool ShowTotalInvestment { get; set; }
		public bool ShowMonthlyCPM { get; set; }
		public bool ShowTotalCPM { get; set; }

		public bool ShowTotalsOnLastOnly { get; set; }

		public string SlideHeader { get; set; }
		public double? TotalMonthlyImpressions { get; set; }
		public double? TotalMonthlyInvestments { get; set; }
		public double? TotalImpressions { get; set; }
		public double? TotalInvestments { get; set; }

		#region Output Settings
		public string TotalHeader1 { get; set; }
		public string TotalValue1 { get; set; }
		public string TotalCPM1 { get; set; }
		public string TotalHeader2 { get; set; }
		public string TotalValue2 { get; set; }
		public string TotalCPM2 { get; set; }
		public string TotalHeader3 { get; set; }
		public string TotalValue3 { get; set; }
		public string TotalCPM3 { get; set; }
		public string TotalHeader4 { get; set; }
		public string TotalValue4 { get; set; }
		public string TotalCPM4 { get; set; }
		#endregion

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<ShowActiveDays>" + ShowActiveDays.ToString() + @"</ShowActiveDays>");
			result.AppendLine(@"<ShowAdRate>" + ShowAdRate.ToString() + @"</ShowAdRate>");
			result.AppendLine(@"<ShowDimensions>" + ShowDimensions.ToString() + @"</ShowDimensions>");
			result.AppendLine(@"<ShowMonthlyImpressions>" + ShowMonthlyImpressions.ToString() + @"</ShowMonthlyImpressions>");
			result.AppendLine(@"<ShowMonthlyInvestment>" + ShowMonthlyInvestment.ToString() + @"</ShowMonthlyInvestment>");
			result.AppendLine(@"<ShowTotalAds>" + ShowTotalAds.ToString() + @"</ShowTotalAds>");
			result.AppendLine(@"<ShowTotalImpressions>" + ShowTotalImpressions.ToString() + @"</ShowTotalImpressions>");
			result.AppendLine(@"<ShowTotalInvestment>" + ShowTotalInvestment.ToString() + @"</ShowTotalInvestment>");
			result.AppendLine(@"<ShowWebsites>" + ShowWebsites.ToString() + @"</ShowWebsites>");
			result.AppendLine(@"<ShowMonthlyCPM>" + ShowMonthlyCPM.ToString() + @"</ShowMonthlyCPM>");
			result.AppendLine(@"<ShowTotalCPM>" + ShowTotalCPM.ToString() + @"</ShowTotalCPM>");
			result.AppendLine(@"<ShowTotalsOnLastOnly>" + ShowTotalsOnLastOnly.ToString() + @"</ShowTotalsOnLastOnly>");
			result.AppendLine(@"<SlideHeader>" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
			result.AppendLine(@"<TotalMonthlyImpressions>" + (TotalMonthlyImpressions.HasValue ? TotalMonthlyImpressions.Value.ToString() : string.Empty) + @"</TotalMonthlyImpressions>");
			result.AppendLine(@"<TotalMonthlyInvestments>" + (TotalMonthlyInvestments.HasValue ? TotalMonthlyInvestments.Value.ToString() : string.Empty) + @"</TotalMonthlyInvestments>");
			result.AppendLine(@"<TotalImpressions>" + (TotalImpressions.HasValue ? TotalImpressions.Value.ToString() : string.Empty) + @"</TotalImpressions>");
			result.AppendLine(@"<TotalInvestments>" + (TotalInvestments.HasValue ? TotalInvestments.Value.ToString() : string.Empty) + @"</TotalInvestments>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool = false;
			double tempDouble = 0;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "ShowActiveDays":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowActiveDays = tempBool;
						break;
					case "ShowAdRate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAdRate = tempBool;
						break;
					case "ShowDimensions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDimensions = tempBool;
						break;
					case "ShowMonthlyImpressions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowMonthlyImpressions = tempBool;
						break;
					case "ShowMonthlyInvestment":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowMonthlyInvestment = tempBool;
						break;
					case "ShowTotalAds":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalAds = tempBool;
						break;
					case "ShowTotalImpressions":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalImpressions = tempBool;
						break;
					case "ShowTotalInvestment":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalInvestment = tempBool;
						break;
					case "ShowWebsites":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowWebsites = tempBool;
						break;
					case "ShowMonthlyCPM":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowMonthlyCPM = tempBool;
						break;
					case "ShowTotalCPM":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalCPM = tempBool;
						break;
					case "ShowTotalsOnLastOnly":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalsOnLastOnly = tempBool;
						break;
					case "SlideHeader":
						SlideHeader = childNode.InnerText;
						break;
					case "TotalMonthlyImpressions":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							TotalMonthlyImpressions = tempDouble;
						else
							TotalMonthlyImpressions = null;
						break;
					case "TotalMonthlyInvestments":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							TotalMonthlyInvestments = tempDouble;
						else
							TotalMonthlyInvestments = null;
						break;
					case "TotalImpressions":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							TotalImpressions = tempDouble;
						else
							TotalImpressions = null;
						break;
					case "TotalInvestments":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							TotalInvestments = tempDouble;
						else
							TotalInvestments = null;
						break;
				}
			}
		}
	}
}