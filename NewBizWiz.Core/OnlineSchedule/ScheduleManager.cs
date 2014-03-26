using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.Office.Core;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;

namespace NewBizWiz.Core.OnlineSchedule
{
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
		public event EventHandler<ScheduleSaveEventArgs> SettingsSaved;

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
				SettingsSaved(sender, new ScheduleSaveEventArgs(quickSave));
		}

		public static ShortSchedule[] GetShortScheduleList(DirectoryInfo rootFolder)
		{
			var scheduleList = rootFolder.GetFiles("*.xml").Select(file => new ShortSchedule(file)).ToList();
			scheduleList.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.ShortFileName, y.ShortFileName));
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
			if (!_scheduleFile.Exists) return;
			var document = new XmlDocument();
			document.Load(_scheduleFile.FullName);

			var node = document.SelectSingleNode(@"/Schedule/BusinessName");
			if (node != null)
				BusinessName = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/Status");
			if (node != null)
				Status = node.InnerText;
		}

		public void Save()
		{
			if (!_scheduleFile.Exists) return;
			try
			{
				var document = new XmlDocument();
				document.Load(_scheduleFile.FullName);

				var node = document.SelectSingleNode(@"/Schedule/Status");
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

	public class Schedule : IDigitalSchedule
	{
		public Schedule(string fileName)
		{
			ClientType = string.Empty;
			AccountNumber = string.Empty;
			Status = ListManager.Instance.Statuses.FirstOrDefault();
			DigitalProducts = new List<DigitalProduct>();
			ViewSettings = new ScheduleBuilderViewSettings();
			DigitalProductSummary = new DigitalProductSummary();

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
		public string ClientType { get; set; }
		public string AccountNumber { get; set; }
		public bool ApplySettingsForeAllProducts { get; set; }
		public List<DigitalProduct> DigitalProducts { get; set; }
		public DigitalProductSummary DigitalProductSummary { get; private set; }

		public ScheduleBuilderViewSettings ViewSettings { get; set; }

		public IScheduleViewSettings SharedViewSettings
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
					return null;
				return PresentationDate;
			}
		}

		public object FlightDateStartObject
		{
			get
			{
				if (FlightDateStart.Equals(DateTime.MaxValue) || FlightDateStart.Equals(DateTime.MinValue))
					return null;
				return FlightDateStart;
			}
		}

		public object FlightDateEndObject
		{
			get
			{
				if (FlightDateEnd.Equals(DateTime.MaxValue) || FlightDateEnd.Equals(DateTime.MinValue))
					return null;
				return FlightDateEnd;
			}
		}

		public decimal MonthlyInvestment
		{
			get { return DigitalProducts.Select(x => (x.MonthlyInvestment.HasValue ? x.MonthlyInvestment.Value : 0)).Sum(); }
		}

		public decimal MonthlyImpressions
		{
			get { return DigitalProducts.Select(x => (x.MonthlyImpressions.HasValue ? x.MonthlyImpressions.Value : 0)).Sum(); }
		}

		public bool EnableMonthlyOnSummary
		{
			get
			{
				bool result = false;
				foreach (var product in DigitalProducts)
					result = result | (product.MonthlyImpressions.HasValue || product.MonthlyInvestment.HasValue); return result;
			}
		}

		public decimal TotalInvestment
		{
			get { return DigitalProducts.Select(x => (x.TotalInvestment.HasValue ? x.TotalInvestment.Value : 0)).Sum(); }
		}

		public decimal TotalImpressions
		{
			get { return DigitalProducts.Select(x => (x.TotalImpressions.HasValue ? x.TotalImpressions.Value : 0)).Sum(); }
		}

		public bool EnableTotalOnSummary
		{
			get
			{
				bool result = false;
				foreach (var product in DigitalProducts)
					result = result | (product.TotalImpressions.HasValue || product.TotalInvestment.HasValue);
				return result;
			}
		}

		#region ISchedule Members

		public bool IsNew { get; set; }
		public string BusinessName { get; set; }
		public string DecisionMaker { get; set; }
		public DateTime? PresentationDate { get; set; }
		public string FlightDates
		{
			get
			{
				if (FlightDateStart.HasValue && FlightDateEnd.HasValue)
					return FlightDateStart.Value.ToString("MM/dd/yy") + " - " + FlightDateEnd.Value.ToString("MM/dd/yy");
				else
					return string.Empty;
			}
		}

		public DateTime? FlightDateStart { get; set; }
		public DateTime? FlightDateEnd { get; set; }
		#endregion

		private void Load()
		{
			if (!_scheduleFile.Exists) return;
			var document = new XmlDocument();
			document.Load(_scheduleFile.FullName);

			XmlNode node = document.SelectSingleNode(@"/Schedule/BusinessName");
			if (node != null)
				BusinessName = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/DecisionMaker");
			if (node != null)
				DecisionMaker = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/Status");
			if (node != null)
				Status = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/ClientType");
			if (node != null)
				ClientType = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/AccountNumber");
			if (node != null)
				AccountNumber = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/PresentationDate");
			DateTime tempDateTime;
			if (node != null)
			{
				if (DateTime.TryParse(node.InnerText, out tempDateTime))
					PresentationDate = tempDateTime;
			}

			node = document.SelectSingleNode(@"/Schedule/FlightDateStart");
			if (node != null)
			{
				if (DateTime.TryParse(node.InnerText, out tempDateTime))
					FlightDateStart = tempDateTime;
			}

			node = document.SelectSingleNode(@"/Schedule/FlightDateEnd");
			if (node != null)
			{
				if (DateTime.TryParse(node.InnerText, out tempDateTime))
					FlightDateEnd = tempDateTime;
			}

			node = document.SelectSingleNode(@"/Schedule/ApplySettingsForeAllProducts");
			if (node != null)
			{
				bool tempBool = false;
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
					DigitalProducts.Add(product);
				}
			}

			node = document.SelectSingleNode(@"/Schedule/DigitalProductSummary");
			if (node != null)
			{
				DigitalProductSummary.Deserialize(node);
			}
		}

		public void Save()
		{
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
			if (PresentationDate.HasValue)
				xml.AppendLine(@"<PresentationDate>" + PresentationDate + @"</PresentationDate>");
			if (FlightDateStart.HasValue)
				xml.AppendLine(@"<FlightDateStart>" + FlightDateStart + @"</FlightDateStart>");
			if (FlightDateEnd.HasValue)
				xml.AppendLine(@"<FlightDateEnd>" + FlightDateEnd + @"</FlightDateEnd>");
			xml.AppendLine(@"<ApplySettingsForeAllProducts>" + ApplySettingsForeAllProducts.ToString() + @"</ApplySettingsForeAllProducts>");

			xml.AppendLine(@"<ViewSettings>" + ViewSettings.Serialize() + @"</ViewSettings>");

			xml.AppendLine(@"<Products>");
			foreach (DigitalProduct product in DigitalProducts)
			{
				xml.AppendLine(product.Serialize());
			}
			xml.AppendLine(@"</Products>");
			xml.AppendLine(@"<DigitalProductSummary>" + DigitalProductSummary.Serialize() + @"</DigitalProductSummary>");
			xml.AppendLine(@"</Schedule>");
			using (var sw = new StreamWriter(_scheduleFile.FullName, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		public void AddDigital(string categoryName)
		{
			var product = new DigitalProduct(this) { Index = DigitalProducts.Count + 1, Category = categoryName };
			DigitalProducts.Add(product);
		}

		public void UpDigital(int position)
		{
			if (position > 0)
			{
				DigitalProducts[position].Index--;
				DigitalProducts[position - 1].Index++;
				DigitalProducts.Sort((x, y) => x.Index.CompareTo(y.Index));
			}
		}

		public void DownDigital(int position)
		{
			if (position < DigitalProducts.Count - 1)
			{
				DigitalProducts[position].Index++;
				DigitalProducts[position + 1].Index--;
				DigitalProducts.Sort((x, y) => x.Index.CompareTo(y.Index));
			}
		}

		public void RebuildDigitalProductIndexes()
		{
			for (int i = 0; i < DigitalProducts.Count; i++)
				DigitalProducts[i].Index = i + 1;
		}
	}

	public class DigitalProduct : ISummaryProduct
	{
		private string _name;
		private string _userDescription;
		private string _userComment;

		#region Basic Properties
		public IDigitalSchedule Parent { get; set; }
		public Guid UniqueID { get; set; }
		public double Index { get; set; }
		public string Category { get; set; }
		public string SubCategory { get; set; }
		public string RateType { get; set; }
		public string Location { get; set; }
		public int? Width { get; set; }
		public int? Height { get; set; }
		public bool EnableLocation { get; set; }
		public bool EnableTarget { get; set; }
		public bool EnableRichMedia { get; set; }
		public List<ProductInfo> AddtionalInfo { get; private set; }
		#endregion

		#region Additional Properties
		public string UserDefinedName { get; set; }
		public List<string> Websites { get; set; }
		public string DurationType { get; set; }
		public int? DurationValue { get; set; }
		public string Strength1 { get; set; }
		public string Strength2 { get; set; }
		public decimal? MonthlyInvestment { get; set; }
		public decimal? MonthlyImpressions { get; set; }
		public decimal? MonthlyCPM { get; set; }
		public decimal? TotalInvestment { get; set; }
		public decimal? TotalImpressions { get; set; }
		public decimal? TotalCPM { get; set; }
		public decimal? DefaultRate { get; set; }
		public FormulaType Formula { get; set; }
		public string InvestmentDetails { get; set; }

		public bool DescriptionManualEdit { get; set; }
		public bool ShowDimensions { get; set; }
		public bool ShowDescriptionTargeting { get; set; }
		public bool ShowDescriptionRichMedia { get; set; }
		public string Description { get; set; }

		public bool EnableComment { get; set; }
		public bool CommentManualEdit { get; set; }
		public bool ShowCommentTargeting { get; set; }
		public bool ShowCommentRichMedia { get; set; }
		#endregion

		#region Show Properties
		public bool ShowDuration { get; set; }
		public bool ShowAllPricingMonthly { get; set; }
		public bool ShowAllPricingTotal { get; set; }
		public bool ShowMonthlyInvestments { get; set; }
		public bool ShowMonthlyImpressions { get; set; }
		public bool ShowTotalInvestments { get; set; }
		public bool ShowTotalImpressions { get; set; }
		public bool ShowFlightDates { get; set; }

		public ProductPackageRecord PackageRecord { get; private set; }
		public DigitalProductAdPlanSettings AdPlanSettings { get; set; }
		public SummaryItem SummaryItem { get; private set; }

		#endregion

		public Output OutputData { get; private set; }

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

		public string FullName
		{
			get
			{
				return String.Format("{0} - {1}{2}",
					Name,
					Category,
					!String.IsNullOrEmpty(SubCategory) ? String.Format(@" / {0}", SubCategory) : String.Empty);
			}
		}

		private string CalculatedComment
		{
			get
			{
				var result = new List<string>();
				if (ShowCommentTargeting && EnableTarget && AddtionalInfo.Any(pi => pi.Type == ProductInfoType.Targeting && pi.Selected))
					result.AddRange(AddtionalInfo.Where(pi => pi.Type == ProductInfoType.Targeting && pi.Selected).Select(pi => pi.EditValue));
				if (ShowCommentRichMedia && EnableRichMedia && AddtionalInfo.Any(pi => pi.Type == ProductInfoType.RichMedia && pi.Selected))
					result.AddRange(AddtionalInfo.Where(pi => pi.Type == ProductInfoType.RichMedia && pi.Selected).Select(pi => pi.EditValue));
				return String.Join(Environment.NewLine, result);
			}
		}

		public string Comment
		{
			get
			{
				if (!String.IsNullOrEmpty(_userDescription) && CommentManualEdit)
					return _userComment;
				return CalculatedComment;
			}
			set
			{
				_userComment = value != CalculatedComment && CommentManualEdit ? value : null;
			}
		}

		private string CalculatedDescription
		{
			get
			{
				var result = new List<string>();
				if (Width.HasValue && Height.HasValue && ShowDimensions)
					result.Add(String.Format("(Ad Dimensions: {0}{1})", Dimensions, !String.IsNullOrEmpty(Location) && !Location.Equals("N/A") ? String.Format(" Location - {0}", Location) : String.Empty));
				if (ShowDescriptionTargeting && EnableTarget && AddtionalInfo.Any(pi => pi.Type == ProductInfoType.Targeting && pi.Selected))
					result.AddRange(AddtionalInfo.Where(pi => pi.Type == ProductInfoType.Targeting && pi.Selected).Select(pi => pi.EditValue));
				if (ShowDescriptionRichMedia && EnableRichMedia && AddtionalInfo.Any(pi => pi.Type == ProductInfoType.RichMedia && pi.Selected))
					result.AddRange(AddtionalInfo.Where(pi => pi.Type == ProductInfoType.RichMedia && pi.Selected).Select(pi => pi.EditValue));
				if (!String.IsNullOrEmpty(Description))
					result.Add(String.Format("{1}{0}", Description, Environment.NewLine));
				return String.Join(Environment.NewLine, result);
			}
		}

		public string UserDescription
		{
			get
			{
				if (!String.IsNullOrEmpty(_userDescription) && DescriptionManualEdit)
					return _userDescription;
				return CalculatedDescription;
			}
			set { _userDescription = value != CalculatedDescription && DescriptionManualEdit ? value : null; }
		}

		public string Dimensions
		{
			get
			{
				if (Width.HasValue && Height.HasValue)
					return Width.Value + " x " + Height.Value;
				return String.Empty;
			}
		}

		public decimal? MonthlyInvestmentCalculated
		{
			get
			{
				if (Formula == FormulaType.Investment)
					return MonthlyImpressions.HasValue && MonthlyCPMCalculated.HasValue ? Math.Round(MonthlyCPMCalculated.Value * (MonthlyImpressions.Value / 1000), 2) : (decimal?)null;
				return MonthlyInvestment;
			}
		}

		public decimal? TotalInvestmentCalculated
		{
			get
			{
				if (Formula == FormulaType.Investment)
					return TotalImpressions.HasValue && TotalCPMCalculated.HasValue ? Math.Round((TotalCPMCalculated.Value * (TotalImpressions.Value / 1000)), 2) : (decimal?)null;
				return TotalInvestment;
			}
		}

		public decimal? MonthlyImpressionsCalculated
		{
			get
			{
				if (Formula == FormulaType.Impressions)
					return MonthlyCPMCalculated.HasValue && MonthlyInvestment.HasValue ? (MonthlyCPMCalculated.Value != 0 ? Math.Round(((MonthlyInvestment.Value * 1000) / MonthlyCPMCalculated.Value), 0) : (decimal?)null) : null;
				return MonthlyImpressions;
			}
		}

		public decimal? TotalImpressionsCalculated
		{
			get
			{
				if (Formula == FormulaType.Impressions)
					return TotalCPMCalculated.HasValue && TotalInvestment.HasValue ? (TotalCPMCalculated.Value != 0 ? Math.Round(((TotalInvestment.Value * 1000) / TotalCPMCalculated.Value), 0) : (decimal?)null) : null;
				return TotalImpressions;
			}
		}

		public decimal? MonthlyCPMCalculated
		{
			get
			{
				if (Formula == FormulaType.CPM)
					return MonthlyImpressions.HasValue && MonthlyInvestment.HasValue ? (MonthlyImpressions.Value != 0 ? Math.Round(MonthlyInvestment.Value / (MonthlyImpressions.Value / 1000), 2) : (decimal?)null) : null;
				return !MonthlyCPM.HasValue && RateType.Equals("CPM") ? DefaultRate : MonthlyCPM;
			}
		}

		public decimal? TotalCPMCalculated
		{
			get
			{
				if (Formula == FormulaType.CPM)
					return TotalImpressions.HasValue && TotalInvestment.HasValue ? (TotalImpressions.Value != 0 ? Math.Round((TotalInvestment.Value / (TotalImpressions.Value / 1000)), 2) : (decimal?)null) : null;
				return !TotalCPM.HasValue && RateType.Equals("CPM") ? DefaultRate : TotalCPM;
			}
		}

		public int WeeksDuration
		{
			get
			{
				var result = 0;
				if (!Parent.FlightDateStart.HasValue || !Parent.FlightDateEnd.HasValue) return result;
				var diff = Parent.FlightDateEnd.Value.Subtract(Parent.FlightDateStart.Value);
				result = diff.Days / 7;
				return result;
			}
		}

		public int MonthDuraton
		{
			get
			{
				if (!Parent.FlightDateStart.HasValue || !Parent.FlightDateEnd.HasValue) return 0;
				return Math.Abs((Parent.FlightDateEnd.Value.Month - Parent.FlightDateStart.Value.Month) + 12 * (Parent.FlightDateEnd.Value.Year - Parent.FlightDateStart.Value.Year));
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

		public bool TargetingAvailable
		{
			get { return EnableTarget && AddtionalInfo.Any(productInfo => productInfo.Type == ProductInfoType.Targeting && productInfo.Selected); }
		}

		public bool RichMediaAvailable
		{
			get { return EnableRichMedia && AddtionalInfo.Any(productInfo => productInfo.Type == ProductInfoType.RichMedia && productInfo.Selected); }
		}
		#endregion

		public DigitalProduct(IDigitalSchedule parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Category = string.Empty;
			Websites = new List<string>();
			AddtionalInfo = new List<ProductInfo>();
			PackageRecord = new ProductPackageRecord(this);
			AdPlanSettings = new DigitalProductAdPlanSettings();
			SummaryItem = new SummaryItem(this);
			RateType = "CPM";
			EnableLocation = true;
			EnableTarget = true;
			EnableRichMedia = true;

			OutputData = new Output(this);

			ApplyDefaultView();
		}

		public string Serialize()
		{
			var xml = new StringBuilder();

			xml.Append(@"<Product ");

			#region Basic Properties
			if (!String.IsNullOrEmpty(Name))
				xml.Append("Name = \"" + Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("UniqueID = \"" + UniqueID + "\" ");
			xml.Append("Index = \"" + Index + "\" ");
			if (!String.IsNullOrEmpty(Category))
				xml.Append("Category = \"" + Category.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			if (!String.IsNullOrEmpty(SubCategory))
				xml.Append("SubCategory = \"" + SubCategory.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			if (!String.IsNullOrEmpty(RateType))
				xml.Append("RateType = \"" + RateType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			if (!String.IsNullOrEmpty(Location))
				xml.Append("Location = \"" + Location.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("Width = \"" + (Width.HasValue ? Width.Value.ToString() : string.Empty) + "\" ");
			xml.Append("Height = \"" + (Height.HasValue ? Height.Value.ToString() : string.Empty) + "\" ");
			xml.Append("EnableTarget = \"" + EnableTarget + "\" ");
			xml.Append("EnableLocation = \"" + EnableLocation + "\" ");
			xml.Append("EnableRichMedia = \"" + EnableRichMedia + "\" ");
			#endregion

			#region Additional Properties
			xml.Append("UserDefinedName = \"" + (String.IsNullOrEmpty(UserDefinedName) ? ExtendedName : UserDefinedName).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("DurationType = \"" + DurationType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("DurationValue = \"" + (DurationValue.HasValue ? DurationValue.Value.ToString() : string.Empty) + "\" ");
			xml.Append("Strength1 = \"" + Strength1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("Strength2 = \"" + Strength2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("EnableComment = \"" + EnableComment + "\" ");
			xml.Append("CommentManualEdit = \"" + CommentManualEdit + "\" ");
			xml.Append("ShowCommentTargeting = \"" + ShowCommentTargeting + "\" ");
			xml.Append("ShowCommentRichMedia = \"" + ShowCommentRichMedia + "\" ");
			if (!String.IsNullOrEmpty(_userComment))
				xml.Append("Comment = \"" + _userComment.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("DescriptionManualEdit = \"" + DescriptionManualEdit + "\" ");
			xml.Append("ShowDimensions = \"" + ShowDimensions + "\" ");
			xml.Append("ShowDescriptionTargeting = \"" + ShowDescriptionTargeting + "\" ");
			xml.Append("ShowDescriptionRichMedia = \"" + ShowDescriptionRichMedia + "\" ");
			if (!String.IsNullOrEmpty(Description))
				xml.Append("Description = \"" + Description.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			if (!String.IsNullOrEmpty(_userDescription))
				xml.Append("UserDescription = \"" + _userDescription.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			if (MonthlyInvestment.HasValue)
				xml.Append("MonthlyInvestment = \"" + MonthlyInvestment.Value + "\" ");
			if (MonthlyImpressions.HasValue)
				xml.Append("MonthlyImpressions = \"" + MonthlyImpressions.Value + "\" ");
			if (MonthlyCPM.HasValue && MonthlyCPM.Value != DefaultRate)
				xml.Append("MonthlyCPM = \"" + MonthlyCPM.Value + "\" ");
			if (TotalInvestment.HasValue)
				xml.Append("TotalInvestment = \"" + TotalInvestment.Value + "\" ");
			if (TotalImpressions.HasValue)
				xml.Append("TotalImpressions = \"" + TotalImpressions.Value + "\" ");
			if (TotalCPM.HasValue && TotalCPM.Value != DefaultRate)
				xml.Append("TotalCPM = \"" + TotalCPM.Value + "\" ");
			xml.Append("DefaultRate = \"" + (DefaultRate.HasValue ? DefaultRate.Value.ToString() : string.Empty) + "\" ");
			xml.Append("Formula = \"" + (int)Formula + "\" ");
			if (!String.IsNullOrEmpty(InvestmentDetails))
				xml.Append("InvestmentDetails = \"" + InvestmentDetails.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			#endregion

			#region Show Properties
			xml.Append("ShowFlightDates = \"" + ShowFlightDates + "\" ");
			xml.Append("ShowDuration = \"" + ShowDuration + "\" ");
			xml.Append("ShowAllPricingMonthly = \"" + ShowAllPricingMonthly + "\" ");
			xml.Append("ShowAllPricingTotal = \"" + ShowAllPricingTotal + "\" ");
			xml.Append("ShowMonthlyInvestments = \"" + ShowMonthlyInvestments + "\" ");
			xml.Append("ShowMonthlyImpressions = \"" + ShowMonthlyImpressions + "\" ");
			xml.Append("ShowTotalInvestments = \"" + ShowTotalInvestments + "\" ");
			xml.Append("ShowTotalImpressions = \"" + ShowTotalImpressions + "\" ");
			#endregion

			xml.AppendLine(@">");
			foreach (string website in Websites)
				xml.AppendLine(@"<Website>" + website.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Website>");

			xml.AppendLine(@"<PackageRecord>" + PackageRecord.Serialize() + @"</PackageRecord>");
			xml.AppendLine(@"<AdPlanSettings>" + AdPlanSettings.Serialize() + @"</AdPlanSettings>");
			xml.AppendLine(@"<SummaryItem>" + SummaryItem.Serialize() + @"</SummaryItem>");

			foreach (var productInfo in AddtionalInfo)
				xml.AppendLine(@"<ProductInfo>" + productInfo.Serialize() + @"</ProductInfo>");

			xml.AppendLine(@"</Product>");

			return xml.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			int tempInt;
			decimal tempDecimal;

			foreach (XmlAttribute productAttribute in node.Attributes)
				switch (productAttribute.Name)
				{
					#region Basic Properties
					case "Name":
						Name = productAttribute.Value;
						break;
					case "UniqueID":
						Guid tempGuid;
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
						RateType = productAttribute.Value;
						switch (RateType)
						{
							case "0":
								RateType = "CPM";
								break;
							case "1":
								RateType = "Fixed";
								break;
						}
						break;
					case "Location":
						Location = productAttribute.Value;
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
					case "EnableTarget":
						{
							bool temp;
							if (Boolean.TryParse(productAttribute.Value, out temp))
								EnableTarget = temp;
						}
						break;
					case "EnableLocation":
						{
							bool temp;
							if (Boolean.TryParse(productAttribute.Value, out temp))
								EnableLocation = temp;
						}
						break;
					case "EnableRichMedia":
						{
							bool temp;
							if (Boolean.TryParse(productAttribute.Value, out temp))
								EnableRichMedia = temp;
						}
						break;
					#endregion

					#region Additional Properties
					case "UserDefinedName":
						UserDefinedName = productAttribute.Value;
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
					case "MonthlyInvestment":
						if (Decimal.TryParse(productAttribute.Value, out tempDecimal))
							MonthlyInvestment = tempDecimal;
						else
							MonthlyInvestment = null;
						break;
					case "MonthlyImpressions":
						if (Decimal.TryParse(productAttribute.Value, out tempDecimal))
							MonthlyImpressions = tempDecimal;
						else
							MonthlyImpressions = null;
						break;
					case "MonthlyCPM":
						if (Decimal.TryParse(productAttribute.Value, out tempDecimal))
							MonthlyCPM = tempDecimal;
						else
							MonthlyCPM = null;
						break;
					case "TotalInvestment":
						if (Decimal.TryParse(productAttribute.Value, out tempDecimal))
							TotalInvestment = tempDecimal;
						else
							TotalInvestment = null;
						break;
					case "TotalImpressions":
						if (Decimal.TryParse(productAttribute.Value, out tempDecimal))
							TotalImpressions = tempDecimal;
						else
							TotalImpressions = null;
						break;
					case "TotalCPM":
						if (Decimal.TryParse(productAttribute.Value, out tempDecimal))
							TotalCPM = tempDecimal;
						else
							TotalCPM = null;
						break;
					case "DefaultRate":
						if (Decimal.TryParse(productAttribute.Value, out tempDecimal))
							DefaultRate = tempDecimal;
						else
							DefaultRate = null;
						break;
					case "Formula":
						if (int.TryParse(productAttribute.Value, out tempInt))
							Formula = (FormulaType)tempInt;
						break;
					case "InvestmentDetails":
						InvestmentDetails = productAttribute.Value;
						break;

					case "EnableComment":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								EnableComment = tempBool;
						}
						break;
					case "CommentManualEdit":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								CommentManualEdit = tempBool;
						}
						break;
					case "ShowCommentTargeting":
						{
							bool temp;
							if (Boolean.TryParse(productAttribute.Value, out temp))
								ShowCommentTargeting = temp;
						}
						break;
					case "ShowCommentRichMedia":
						{
							bool temp;
							if (Boolean.TryParse(productAttribute.Value, out temp))
								ShowCommentRichMedia = temp;
						}
						break;
					case "Comment":
						_userComment = productAttribute.Value;
						break;
					case "DescriptionManualEdit":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								DescriptionManualEdit = tempBool;
						}
						break;
					case "ShowDimensions":
						{
							bool temp;
							if (Boolean.TryParse(productAttribute.Value, out temp))
								ShowDimensions = temp;
						}
						break;
					case "ShowDescriptionTargeting":
						{
							bool temp;
							if (Boolean.TryParse(productAttribute.Value, out temp))
								ShowDescriptionTargeting = temp;
						}
						break;
					case "ShowDescriptionRichMedia":
						{
							bool temp;
							if (Boolean.TryParse(productAttribute.Value, out temp))
								ShowDescriptionRichMedia = temp;
						}
						break;
					case "Description":
						Description = productAttribute.Value;
						break;
					case "UserDescription":
						_userDescription = productAttribute.Value;
						break;
					#endregion

					#region Show Properties
					case "ShowFlightDates":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								ShowFlightDates = tempBool;
						}
						break;
					case "ShowDuration":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								ShowDuration = tempBool;
						}
						break;
					case "ShowAllPricingMonthly":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								ShowAllPricingMonthly = tempBool;
						}
						break;
					case "ShowAllPricingTotal":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								ShowAllPricingTotal = tempBool;
						}
						break;
					case "ShowMonthlyInvestments":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								ShowMonthlyInvestments = tempBool;
						}
						break;
					case "ShowMonthlyImpressions":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								ShowMonthlyImpressions = tempBool;
						}
						break;
					case "ShowTotalInvestments":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								ShowTotalInvestments = tempBool;
						}
						break;
					case "ShowTotalImpressions":
						{
							bool tempBool;
							if (bool.TryParse(productAttribute.Value, out tempBool))
								ShowTotalImpressions = tempBool;
						}
						break;
					#endregion
				}
			Websites.Clear();
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Website":
						Websites.Add(childNode.InnerText);
						break;
					case "PackageRecord":
						PackageRecord.Deserialize(childNode);
						break;
					case "AdPlanSettings":
						AdPlanSettings.Deserialize(childNode);
						break;
					case "SummaryItem":
						SummaryItem.Deserialize(childNode);
						break;
					case "ProductInfo":
						var productInfo = new ProductInfo();
						productInfo.Deserialize(childNode);
						AddtionalInfo.Add(productInfo);
						break;
				}
			}
			if (Websites.Count == 0 && ListManager.Instance.Websites.Any())
				Websites.Add(ListManager.Instance.Websites.FirstOrDefault());
			if (String.IsNullOrEmpty(UserDefinedName))
				UserDefinedName = ExtendedName;
			UpdateAdditionlaInfo();
		}

		public void ApplyDefaultValues()
		{
			var source = ListManager.Instance.ProductSources.FirstOrDefault(x => x.Name.Equals(_name) && x.Category.Name.Equals(Category) && (x.SubCategory == SubCategory || String.IsNullOrEmpty(SubCategory)));
			if (source == null) return;
			RateType = source.RateType;
			DefaultRate = source.Rate;
			Width = source.Width;
			Height = source.Height;
			EnableTarget = source.EnableTarget;
			EnableLocation = source.EnableLocation;
			EnableRichMedia = source.EnableRichMedia;
			Description = source.Overview;
			UpdateAdditionlaInfo();
		}

		public void ApplyDefaultView()
		{
			UserDefinedName = ExtendedName;
			Strength1 = String.Empty;
			Strength2 = String.Empty;
			DurationType = String.Empty;
			DurationValue = null;
			MonthlyInvestment = null;
			MonthlyImpressions = null;
			TotalInvestment = null;
			TotalImpressions = null;
			Formula = ListManager.Instance.DefaultFormula;
			InvestmentDetails = null;

			DescriptionManualEdit = false;
			ShowDimensions = true;
			ShowDescriptionTargeting = false;
			ShowDescriptionRichMedia = false;
			_userDescription = null;
			EnableComment = false;
			CommentManualEdit = false;
			ShowCommentTargeting = false;
			ShowCommentRichMedia = false;
			_userComment = null;

			var source = ListManager.Instance.ProductSources.FirstOrDefault(x => x.Name.Equals(_name) && x.Category.Name.Equals(Category) && (x.SubCategory == SubCategory || String.IsNullOrEmpty(SubCategory)));
			if (source != null)
			{
				Description = source.Overview;
				DefaultRate = source.Rate;
				MonthlyCPM = DefaultRate;
				TotalCPM = DefaultRate;
			}

			var defaultViewSettings = ListManager.Instance.DefaultDigitalProductSettings;
			ShowFlightDates = defaultViewSettings.ShowFlightDates;
			ShowDuration = defaultViewSettings.ShowDuration;
			switch (defaultViewSettings.DefaultPricing)
			{
				case 1:
					ShowAllPricingMonthly = true;
					ShowAllPricingTotal = false;
					ShowMonthlyImpressions = false;
					ShowTotalImpressions = false;
					ShowMonthlyInvestments = false;
					ShowTotalInvestments = false;
					break;
				case 2:
					ShowAllPricingMonthly = false;
					ShowAllPricingTotal = true;
					ShowMonthlyImpressions = false;
					ShowTotalImpressions = false;
					ShowMonthlyInvestments = false;
					ShowTotalInvestments = false;
					break;
				case 3:
					ShowAllPricingMonthly = false;
					ShowAllPricingTotal = false;
					ShowMonthlyImpressions = true;
					ShowTotalImpressions = false;
					ShowMonthlyInvestments = false;
					ShowTotalInvestments = false;
					break;
				case 4:
					ShowAllPricingMonthly = false;
					ShowAllPricingTotal = false;
					ShowMonthlyImpressions = false;
					ShowTotalImpressions = false;
					ShowMonthlyInvestments = true;
					ShowTotalInvestments = false;
					break;
				default:
					ShowAllPricingMonthly = false;
					ShowAllPricingTotal = false;
					ShowMonthlyInvestments = false;
					ShowMonthlyImpressions = false;
					ShowTotalImpressions = false;
					ShowTotalInvestments = false;
					break;
			}
		}

		public DigitalProduct Clone()
		{
			var result = new DigitalProduct(Parent);
			var document = new XmlDocument();
			document.LoadXml(Serialize());
			result.Deserialize(document.FirstChild);
			result.UniqueID = Guid.NewGuid();
			result.Index = Index + 0.5;
			Parent.DigitalProducts.Add(result);
			Parent.DigitalProducts.Sort((x, y) => x.Index.CompareTo(y.Index));
			Parent.RebuildDigitalProductIndexes();
			return result;
		}

		public void UpdateAdditionlaInfo()
		{
			var newAddtionalInfo = new List<ProductInfo>();
			if (EnableTarget)
				newAddtionalInfo.AddRange(ListManager.Instance.TargetingRecods.MergeSet(AddtionalInfo));
			if (EnableRichMedia)
				newAddtionalInfo.AddRange(ListManager.Instance.RichMediaRecods.MergeSet(AddtionalInfo));
			AddtionalInfo.Clear();
			AddtionalInfo.AddRange(newAddtionalInfo);
		}

		public class Output
		{
			private readonly DigitalProduct source;

			public Output(DigitalProduct parent)
			{
				source = parent;
			}

			public string Header
			{
				get { return "{0}"; }
			}

			public string Websites
			{
				get { return String.Join(", ", source.Websites.ToArray()); }
			}

			public string PresentationDate
			{
				get { return source.Parent.PresentationDate.HasValue ? source.Parent.PresentationDate.Value.ToString("MM/dd/yy") : String.Empty; }
			}

			public string BusinessName
			{
				get { return source.Parent.BusinessName; }
			}

			public string DecisionMaker
			{
				get { return source.Parent.DecisionMaker; }
			}

			public string FlightDates
			{
				get
				{
					var campaignStack = new List<string>();
					if (source.ShowFlightDates)
						campaignStack.Add(source.Parent.FlightDates);
					if (!String.IsNullOrEmpty(DurationType) && !String.IsNullOrEmpty(DurationValue))
						campaignStack.Add(String.Format("{0} {1}", DurationValue, DurationType));
					return campaignStack.Any() ? String.Format("Campaign:  {0}", String.Join("      ", campaignStack)) : String.Empty;
				}
			}

			public string DurationValue
			{
				get { return source.DurationValue.HasValue && source.ShowDuration ? source.DurationValue.Value.ToString("#,##0") : String.Empty; }
			}

			public string DurationType
			{
				get { return source.ShowDuration ? source.DurationType : String.Empty; }
			}

			public string ProductName
			{
				get { return source.UserDefinedName; }
			}

			public string Description
			{
				get { return source.UserDescription; }
			}

			public decimal? Impressions
			{
				get
				{
					decimal? result = null;
					if (source.ShowAllPricingTotal || source.ShowAllPricingMonthly || source.ShowMonthlyImpressions || source.ShowTotalImpressions)
					{
						if (source.ShowAllPricingMonthly && source.MonthlyImpressionsCalculated.HasValue && source.MonthlyImpressionsCalculated.Value > 0)
							result = source.MonthlyImpressionsCalculated;
						else if (source.ShowAllPricingTotal && source.TotalImpressionsCalculated.HasValue && source.TotalImpressionsCalculated.Value > 0)
							result = source.TotalImpressionsCalculated;
						else if (source.ShowMonthlyImpressions && source.MonthlyImpressions.HasValue && source.MonthlyImpressions.Value > 0)
							result = source.MonthlyImpressions;
						else if (source.ShowTotalImpressions && source.TotalImpressions.HasValue && source.TotalImpressions.Value > 0)
							result = source.TotalImpressions;
					}
					return result;
				}
			}

			public decimal? Investments
			{
				get
				{
					decimal? result = null;
					if (source.ShowAllPricingTotal || source.ShowAllPricingMonthly || source.ShowMonthlyInvestments || source.ShowTotalInvestments)
					{
						if (source.ShowAllPricingMonthly && source.MonthlyInvestmentCalculated.HasValue && source.MonthlyInvestmentCalculated.Value > 0)
							result = source.MonthlyInvestmentCalculated;
						else if (source.ShowAllPricingTotal && source.TotalInvestmentCalculated.HasValue && source.TotalInvestmentCalculated.Value > 0)
							result = source.TotalInvestmentCalculated;
						else if (source.ShowMonthlyInvestments && source.MonthlyInvestment.HasValue && source.MonthlyInvestment.Value > 0)
							result = source.MonthlyInvestment;
						else if (source.ShowTotalInvestments && source.TotalInvestment.HasValue && source.TotalInvestment.Value > 0)
							result = source.TotalInvestment;
					}
					return result;
				}
			}

			public decimal? CPM
			{
				get
				{
					decimal? result = null;
					if (source.ShowAllPricingTotal || source.ShowAllPricingMonthly)
					{
						if (source.ShowAllPricingMonthly && source.MonthlyCPMCalculated.HasValue && source.MonthlyCPMCalculated.Value > 0)
							result = source.MonthlyCPMCalculated;
						else if (source.ShowAllPricingTotal && source.TotalCPMCalculated.HasValue && source.TotalCPMCalculated.Value > 0)
							result = source.TotalCPMCalculated;
					}
					return result;
				}
			}

			public IEnumerable<NameCodePair> MonthlyData
			{
				get
				{
					var result = new List<NameCodePair>();
					if (source.ShowAllPricingMonthly)
					{
						if (source.MonthlyImpressionsCalculated.HasValue && source.MonthlyImpressionsCalculated.Value > 0)
							result.Add(new NameCodePair { Name = "monthly impressions:", Code = source.MonthlyImpressionsCalculated.Value.ToString("#,##0") });
						if (source.MonthlyInvestmentCalculated.HasValue && source.MonthlyInvestmentCalculated.Value > 0)
							result.Add(new NameCodePair { Name = "monthly investment:", Code = source.MonthlyInvestmentCalculated.Value.ToString("$#,###.00") });
						if (source.MonthlyCPMCalculated.HasValue && source.MonthlyCPMCalculated.Value > 0)
							result.Add(new NameCodePair { Name = "cpm:", Code = source.MonthlyCPMCalculated.Value.ToString("$#,###.00") });
					}
					else if (source.ShowMonthlyInvestments && source.MonthlyInvestment.HasValue)
					{
						result.Add(new NameCodePair { Name = "monthly investment:", Code = source.MonthlyInvestment.Value.ToString("$#,###.00") });
					}
					else if (source.ShowMonthlyImpressions && source.MonthlyImpressions.HasValue)
					{
						result.Add(new NameCodePair { Name = "monthly impressions:", Code = source.MonthlyImpressions.Value.ToString("#,##0") });
					}
					return result;
				}
			}

			public IEnumerable<NameCodePair> TotalData
			{
				get
				{
					var result = new List<NameCodePair>();
					if (source.ShowAllPricingTotal)
					{
						if (source.TotalImpressionsCalculated.HasValue && source.TotalImpressionsCalculated.Value > 0)
							result.Add(new NameCodePair { Name = "total impressions:", Code = source.TotalImpressionsCalculated.Value.ToString("#,##0") });
						if (source.TotalInvestmentCalculated.HasValue && source.TotalInvestmentCalculated.Value > 0)
							result.Add(new NameCodePair { Name = "total investment:", Code = source.TotalInvestmentCalculated.Value.ToString("$#,###.00") });
						if (source.TotalCPMCalculated.HasValue && source.TotalCPMCalculated.Value > 0)
							result.Add(new NameCodePair { Name = "cpm:", Code = source.TotalCPMCalculated.Value.ToString("$#,###.00") });
					}
					else if (source.ShowTotalInvestments && source.TotalInvestment.HasValue)
					{
						result.Add(new NameCodePair { Name = "total investment:", Code = source.TotalInvestment.Value.ToString("$#,###.00") });
					}
					else if (source.ShowTotalImpressions && source.TotalImpressions.HasValue)
					{
						result.Add(new NameCodePair { Name = "total impressions:", Code = source.TotalImpressions.Value.ToString("#,##0") });
					}
					return result;
				}
			}

			public string InvestmentDetails
			{
				get { return source.InvestmentDetails; }
			}

			public string Comment
			{
				get
				{
					var list = new List<string>();
					if (!String.IsNullOrEmpty(source.Strength1))
						list.Add(source.Strength1);
					if (!String.IsNullOrEmpty(source.Strength2))
						list.Add(source.Strength2);
					if (!String.IsNullOrEmpty(source.Comment))
						list.Add(source.Comment);
					return String.Join(". ", list.ToArray());
				}
			}

			public string GetSlideSource(string outputTemplateFolderPath)
			{
				if (!String.IsNullOrEmpty(Comment))
				{
					if (MonthlyData.Any())
						return Path.Combine(outputTemplateFolderPath, "comments", String.Format("monthly{0}.ppt", !String.IsNullOrEmpty(InvestmentDetails) ? "_inv" : String.Empty));
					if (TotalData.Any())
						return Path.Combine(outputTemplateFolderPath, "comments", String.Format("total{0}.ppt", !String.IsNullOrEmpty(InvestmentDetails) ? "_inv" : String.Empty));
					return Path.Combine(outputTemplateFolderPath, "comments", String.Format("none{0}.ppt", !String.IsNullOrEmpty(InvestmentDetails) ? "_inv" : String.Empty));
				}
				else
				{
					if (MonthlyData.Any())
						return Path.Combine(outputTemplateFolderPath, "no_comments", String.Format("monthly{0}.ppt", !String.IsNullOrEmpty(InvestmentDetails) ? "_inv" : String.Empty));
					if (TotalData.Any())
						return Path.Combine(outputTemplateFolderPath, "no_comments", String.Format("total{0}.ppt", !String.IsNullOrEmpty(InvestmentDetails) ? "_inv" : String.Empty));
					return Path.Combine(outputTemplateFolderPath, "no_comments", String.Format("none{0}.ppt", !String.IsNullOrEmpty(InvestmentDetails) ? "_inv" : String.Empty));
				}
			}
		}

		public string SummaryTitle
		{
			get { return String.Format("{0}{1}", Name, (!String.IsNullOrEmpty(Location) && Location != "N/A" ? String.Format(" ({0})", Location) : String.Empty)); }
		}

		public string SummaryInfo
		{
			get
			{
				var values = new List<string>();
				values.Add(FullName);
				if (!String.IsNullOrEmpty(OutputData.Websites))
					values.Add(OutputData.Websites);
				values.Add(String.Format("{0}", Parent.FlightDates));
				values.Add(String.Format("{0}", UserDescription));
				return String.Join(", ", values).Replace(Environment.NewLine, ", ");
			}
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
				if (_category == null)
					return Parent.Category;
				return _category;
			}
			set
			{
				if (String.IsNullOrEmpty(Parent.Category) || !Parent.Category.Equals(value))
					_category = value;
			}
		}
		public string SubCategory
		{
			get
			{
				if (_subCategory == null)
					return Parent.SubCategory;
				return _subCategory;
			}
			set
			{
				if (String.IsNullOrEmpty(Parent.SubCategory) || !Parent.SubCategory.Equals(value))
					_subCategory = value;
			}
		}
		public string Name
		{
			get
			{
				if (_name == null)
					return Parent.Name;
				return _name;
			}
			set
			{
				if (String.IsNullOrEmpty(Parent.Name) || !Parent.Name.Equals(value))
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
				if (UseFormula && (Parent.Parent.SharedViewSettings.DigitalPackageSettings.ShowInvestment && Parent.Parent.SharedViewSettings.DigitalPackageSettings.ShowImpressions && Parent.Parent.SharedViewSettings.DigitalPackageSettings.ShowCPM) && Parent.Parent.SharedViewSettings.DigitalPackageSettings.Formula == FormulaType.Investment)
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
				if (UseFormula && (Parent.Parent.SharedViewSettings.DigitalPackageSettings.ShowInvestment && Parent.Parent.SharedViewSettings.DigitalPackageSettings.ShowImpressions && Parent.Parent.SharedViewSettings.DigitalPackageSettings.ShowCPM) && Parent.Parent.SharedViewSettings.DigitalPackageSettings.Formula == FormulaType.Impressions)
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
				if (UseFormula && (Parent.Parent.SharedViewSettings.DigitalPackageSettings.ShowInvestment && Parent.Parent.SharedViewSettings.DigitalPackageSettings.ShowImpressions && Parent.Parent.SharedViewSettings.DigitalPackageSettings.ShowCPM) && Parent.Parent.SharedViewSettings.DigitalPackageSettings.Formula == FormulaType.CPM)
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

			if (_category != null)
				xml.AppendLine(@"<Category>" + _category.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Category>");
			if (_subCategory != null)
				xml.AppendLine(@"<SubCategory>" + _subCategory.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SubCategory>");
			if (_name != null)
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
						bool tempBool;
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
			RateType = "CPM";
			EnableLocation = true;
			EnableTarget = true;
			EnableRichMedia = true;
		}

		public string Name { get; set; }
		public Category Category { get; set; }
		public string SubCategory { get; set; }
		public string RateType { get; set; }
		public Decimal? Rate { get; set; }
		public int? Width { get; set; }
		public int? Height { get; set; }
		public bool EnableLocation { get; set; }
		public bool EnableTarget { get; set; }
		public bool EnableRichMedia { get; set; }
		public string Overview { get; set; }
	}

	public class Category
	{
		public string Name { get; set; }
		public Image Logo { get; set; }
		public string TooltipTitle { get; set; }
		public string TooltipValue { get; set; }
	}
}