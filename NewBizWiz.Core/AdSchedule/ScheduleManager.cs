using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.Core.OnlineSchedule;

namespace NewBizWiz.Core.AdSchedule
{
	public enum AdPricingStrategies
	{
		StandartPCI = 0,
		FlatModular,
		SharePage
	}

	public enum ColorOptions
	{
		BlackWhite = 0,
		SpotColor,
		FullColor
	}

	public enum ColorPricingType
	{
		CostPerAd = 0,
		PercentOfAdRate,
		ColorIncluded,
		CostPerInch,
		None
	}

	public enum CirculationType
	{
		Daily = 0,
		Sunday
	}

	public class ScheduleManager
	{
		private Schedule _currentSchedule;

		public bool ScheduleLoaded { get; set; }
		public event EventHandler<ScheduleSaveEventArgs> SettingsSaved;

		public void OpenSchedule(string scheduleName, bool create)
		{
			var scheduleFilePath = GetScheduleFileName(scheduleName);
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

		public static ShortSchedule[] GetShortScheduleList()
		{
			var saveFolder = new DirectoryInfo(SettingsManager.Instance.SaveFolder);
			if (saveFolder.Exists)
				return GetShortScheduleList(saveFolder);
			return null;
		}

		public static ShortSchedule[] GetShortScheduleList(DirectoryInfo rootFolder)
		{
			var scheduleList = rootFolder.GetFiles("*.xml").Select(file => new ShortSchedule(file)).ToList();
			scheduleList.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.ShortFileName, y.ShortFileName));
			return scheduleList.ToArray();
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
			get { return String.IsNullOrEmpty(_scheduleFile.Extension) ? _scheduleFile.Name : _scheduleFile.Name.Replace(_scheduleFile.Extension, ""); }
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
			if (!_scheduleFile.Exists) return;
			var document = new XmlDocument();
			document.Load(_scheduleFile.FullName);

			node = document.SelectSingleNode(@"/Schedule/BusinessName");
			if (node != null)
				BusinessName = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/Status");
			if (node != null)
				Status = node.InnerText;
		}

		public void Save()
		{
			XmlNode node;
			if (!_scheduleFile.Exists) return;
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

	public class Schedule : IDigitalSchedule, ISummarySchedule
	{
		public Schedule(string fileName)
		{
			ClientType = string.Empty;
			AccountNumber = string.Empty;
			Status = ListManager.Instance.Statuses.FirstOrDefault();
			PrintProducts = new List<PrintProduct>();
			DigitalProducts = new List<DigitalProduct>();
			ProductSummary = new BaseSummarySettings();
			CustomSummary = new AdFullSummarySettings(this);
			ViewSettings = new ScheduleBuilderViewSettings();
			DigitalProductSummary = new DigitalProductSummary();
			Calendar = new AdCalendar(this);

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


			if (ListManager.Instance.DefaultHomeViewSettings.EnableAccountNumber)
				AccountNumber = string.Empty;
		}

		private FileInfo _scheduleFile { get; set; }
		public bool IsNew { get; set; }
		public string BusinessName { get; set; }
		public string DecisionMaker { get; set; }
		public string ClientType { get; set; }
		public string AccountNumber { get; set; }
		public string Status { get; set; }
		public DateTime? PresentationDate { get; set; }
		public DateTime? FlightDateStart { get; set; }
		public DateTime? FlightDateEnd { get; set; }
		public List<PrintProduct> PrintProducts { get; set; }
		public List<DigitalProduct> DigitalProducts { get; set; }
		public DigitalProductSummary DigitalProductSummary { get; private set; }
		public AdCalendar Calendar { get; private set; }

		public ScheduleBuilderViewSettings ViewSettings { get; set; }
		public IScheduleViewSettings SharedViewSettings
		{
			get { return ViewSettings; }
		}

		public BaseSummarySettings ProductSummary { get; private set; }
		public CustomSummarySettings CustomSummary { get; private set; }

		public string Name
		{
			get { return _scheduleFile.Name.Replace(_scheduleFile.Extension, ""); }
			set { _scheduleFile = new FileInfo(Path.Combine(_scheduleFile.Directory.FullName, value + ".xml")); }
		}

		public FileInfo ScheduleFile
		{
			get { return _scheduleFile; }
		}

		public string FlightDates
		{
			get
			{
				if (FlightDateStart.HasValue && FlightDateEnd.HasValue)
					return FlightDateStart.Value.ToString("MM/dd/yy") + " - " + FlightDateEnd.Value.ToString("MM/dd/yy");
				return string.Empty;
			}
		}

		public IEnumerable<DateTime> ScheduleDates
		{
			get
			{
				var result = new List<DateTime>();
				if (!FlightDateStart.HasValue || !FlightDateEnd.HasValue) return result;
				var startDate = FlightDateStart.Value;
				while (startDate <= FlightDateEnd.Value)
				{
					result.Add(startDate);
					startDate = startDate.AddDays(1);
				}
				return result;
			}
		}

		public IEnumerable<ISummaryProduct> ProductSummaries
		{
			get
			{
				var result = new List<ISummaryProduct>();
				result.AddRange(PrintProducts);
				result.AddRange(DigitalProducts);
				return result;
			}
		}

		private void Load()
		{
			if (!_scheduleFile.Exists) return;
			var document = new XmlDocument();
			document.Load(_scheduleFile.FullName);

			var node = document.SelectSingleNode(@"/Schedule/BusinessName");
			if (node != null)
				BusinessName = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/DecisionMaker");
			if (node != null)
				DecisionMaker = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/ClientType");
			if (node != null)
				ClientType = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/AccountNumber");
			if (node != null)
				AccountNumber = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/Status");
			if (node != null)
				Status = node.InnerText;

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
			node = document.SelectSingleNode(@"/Schedule/ViewSettings");
			if (node != null)
			{
				ViewSettings.Deserialize(node);
			}
			node = document.SelectSingleNode(@"/Schedule/ProductSummary");
			if (node != null)
			{
				ProductSummary.Deserialize(node);
			}
			node = document.SelectSingleNode(@"/Schedule/CustomSummary");
			if (node != null)
			{
				CustomSummary.Deserialize(node);
			}
			node = document.SelectSingleNode(@"/Schedule/Publications");
			if (node != null)
			{
				foreach (XmlNode publicationNode in node.ChildNodes)
				{
					var publication = new PrintProduct(this);
					publication.Deserialize(publicationNode);
					PrintProducts.Add(publication);
				}
			}
			node = document.SelectSingleNode(@"/Schedule/DigitalProducts");
			if (node != null)
			{
				foreach (XmlNode publicationNode in node.ChildNodes)
				{
					var digitalProduct = new DigitalProduct(this);
					digitalProduct.Deserialize(publicationNode);
					DigitalProducts.Add(digitalProduct);
				}
			}
			node = document.SelectSingleNode(@"/Schedule/DigitalProductSummary");
			if (node != null)
			{
				DigitalProductSummary.Deserialize(node);
			}

			node = document.SelectSingleNode(@"/Schedule/Calendar");
			if (node != null)
			{
				Calendar.Deserialize(node);
			}
			else
			{
				Calendar.UpdateDaysCollection();
				Calendar.UpdateMonthCollection();
				Calendar.UpdateNotesCollection();
			}
		}

		public void Save()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<Schedule>");

			xml.AppendLine(@"<BusinessName>" + BusinessName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BusinessName>");
			Common.ListManager.Instance.Advertisers.Add(BusinessName);
			Common.ListManager.Instance.Advertisers.Save();

			xml.AppendLine(@"<DecisionMaker>" + DecisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
			Common.ListManager.Instance.DecisionMakers.Add(DecisionMaker);
			Common.ListManager.Instance.DecisionMakers.Save();

			xml.AppendLine(@"<ClientType>" + ClientType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ClientType>");
			xml.AppendLine(@"<AccountNumber>" + AccountNumber.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</AccountNumber>");
			xml.AppendLine(@"<Status>" + (Status != null ? Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : String.Empty) + @"</Status>");
			if (PresentationDate.HasValue)
				xml.AppendLine(@"<PresentationDate>" + PresentationDate + @"</PresentationDate>");
			if (FlightDateStart.HasValue)
				xml.AppendLine(@"<FlightDateStart>" + FlightDateStart + @"</FlightDateStart>");
			if (FlightDateEnd.HasValue)
				xml.AppendLine(@"<FlightDateEnd>" + FlightDateEnd + @"</FlightDateEnd>");

			xml.AppendLine(@"<ViewSettings>" + ViewSettings.Serialize() + @"</ViewSettings>");
			xml.AppendLine(@"<ProductSummary>" + ProductSummary.Serialize() + @"</ProductSummary>");
			xml.AppendLine(@"<CustomSummary>" + CustomSummary.Serialize() + @"</CustomSummary>");

			xml.AppendLine(@"<Publications>");
			foreach (PrintProduct publication in PrintProducts)
				xml.AppendLine(publication.Serialize());
			xml.AppendLine(@"</Publications>");
			xml.AppendLine(@"<DigitalProducts>");
			foreach (var digitalProduct in DigitalProducts)
				xml.AppendLine(digitalProduct.Serialize());
			xml.AppendLine(@"</DigitalProducts>");
			xml.AppendLine(@"<DigitalProductSummary>" + DigitalProductSummary.Serialize() + @"</DigitalProductSummary>");
			xml.AppendLine(@"<Calendar>" + Calendar.Serialize() + @"</Calendar>");

			xml.AppendLine(@"</Schedule>");

			using (var sw = new StreamWriter(_scheduleFile.FullName, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		public void AddPublication()
		{
			PrintProducts.Add(new PrintProduct(this));
		}

		public void UpPublication(int position)
		{
			if (position > 0)
			{
				PrintProducts[position].Index--;
				PrintProducts[position - 1].Index++;
				PrintProducts.Sort((x, y) => x.Index.CompareTo(y.Index));
			}
		}

		public void DownPublication(int position)
		{
			if (position < PrintProducts.Count - 1)
			{
				PrintProducts[position].Index++;
				PrintProducts[position + 1].Index--;
				PrintProducts.Sort((x, y) => x.Index.CompareTo(y.Index));
			}
		}

		public void RebuildPublicationIndexes()
		{
			for (int i = 0; i < PrintProducts.Count; i++)
				PrintProducts[i].Index = i + 1;
		}

		public void AddDigital(string categoryName)
		{
			DigitalProducts.Add(new DigitalProduct(this) { Index = DigitalProducts.Count + 1, Category = categoryName }); ;
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

		public string GetDigitalInfo(RequestDigitalInfoEventArgs args)
		{
			var result = new StringBuilder();
			if (args.ShowWebsites)
			{
				var compiledWebsites = String.Join(", ", DigitalProducts.SelectMany(p => p.Websites).Distinct());
				if (!String.IsNullOrEmpty(compiledWebsites))
					result.AppendLine(String.Format("{0}", compiledWebsites));
			}
			foreach (var product in DigitalProducts)
			{
				var temp = new List<string>();
				if (args.ShowProduct && !String.IsNullOrEmpty(product.UserDefinedName))
					temp.Add(product.UserDefinedName);
				if (args.ShowDimensions && !String.IsNullOrEmpty(product.Dimensions))
					temp.Add(product.Dimensions);
				if (args.ShowDates && product.DurationValue.HasValue)
					temp.Add(FlightDates);
				if (product.MonthlyImpressionsCalculated.HasValue || product.MonthlyCPMCalculated.HasValue || product.MonthlyInvestmentCalculated.HasValue)
				{
					var monthly = new List<string>();
					if (args.ShowImpressions && product.MonthlyImpressionsCalculated.HasValue)
						monthly.Add(String.Format("Imp: {0}", product.MonthlyImpressionsCalculated.Value.ToString("#,##0")));
					if (args.ShowInvestment && product.MonthlyInvestmentCalculated.HasValue)
						monthly.Add(String.Format("Inv: {0}", product.MonthlyInvestmentCalculated.Value.ToString("#,##0")));
					if (args.ShowCPM && product.MonthlyCPMCalculated.HasValue)
						monthly.Add(String.Format("CPM: {0}", product.MonthlyCPMCalculated.Value.ToString("#,##0")));
					if (monthly.Any())
						temp.Add(String.Format("(Monthly) {0}", String.Join(" ", monthly)));
				}
				if (product.TotalImpressionsCalculated.HasValue || product.TotalCPMCalculated.HasValue || product.TotalInvestmentCalculated.HasValue)
				{
					var total = new List<string>();
					if (args.ShowImpressions && product.TotalImpressionsCalculated.HasValue)
						total.Add(String.Format("Imp: {0}", product.TotalImpressionsCalculated.Value.ToString("#,##0")));
					if (args.ShowInvestment && product.TotalInvestmentCalculated.HasValue)
						total.Add(String.Format("Inv: {0}", product.TotalInvestmentCalculated.Value.ToString("#,##0")));
					if (args.ShowCPM && product.TotalCPMCalculated.HasValue)
						total.Add(String.Format("CPM: {0}", product.TotalCPMCalculated.Value.ToString("#,##0")));
					if (total.Any())
						temp.Add(String.Format("(Total) {0}", String.Join(" ", total)));
				}
				if (temp.Any())
					result.AppendLine(String.Format("[{0}]", String.Join(", ", temp.ToArray())));
			}
			return result.ToString();
		}

		public void ApplyDigitalLegendForAllViews(DigitalLegend digitalLegend)
		{
			ViewSettings.BasicOverviewViewSettings.DigitalLegend = digitalLegend.Clone();
			ViewSettings.MultiSummaryViewSettings.DigitalLegend = digitalLegend.Clone();
			ViewSettings.SnapshotViewSettings.DigitalLegend = digitalLegend.Clone();
			ViewSettings.DetailedGridViewSettings.DigitalLegend = digitalLegend.Clone();
			ViewSettings.MultiGridViewSettings.DigitalLegend = digitalLegend.Clone();
		}
	}

	public class PrintProduct : ISummaryProduct
	{
		private ColorOptions _colorOptions;
		private ColorPricingType _colorPricing;
		private string _name;

		public PrintProduct(Schedule parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();

			Inserts = new List<Insert>();
			Index = Parent.PrintProducts.Count + 1;
			Note = string.Empty;
			ViewSettings = new PublicationViewSettings();
			SizeOptions = new SizeOptions();
			SummaryItem = new ProductSummaryItem(this);

			ColorOption = ListManager.Instance.DefaultColor;
			AdPricingStrategy = ListManager.Instance.DefaultPricingStrategy;
			_colorPricing = ListManager.Instance.DefaultColorPricing;

			AllowSundaySelect = true;
			AllowMondaySelect = true;
			AllowTuesdaySelect = true;
			AllowWednesdaySelect = true;
			AllowThursdaySelect = true;
			AllowFridaySelect = true;
			AllowSaturdaySelect = true;
			AvailableDays = new List<DateTime>();
		}

		public Schedule Parent { get; set; }

		public Guid UniqueID { get; set; }
		public string Abbreviation { get; set; }
		public Image BigLogo { get; set; }
		public Image SmallLogo { get; set; }
		public Image TinyLogo { get; set; }
		public double? DailyDelivery { get; set; }
		public double? SundayDelivery { get; set; }
		public double? DailyReadership { get; set; }
		public double? SundayReadership { get; set; }
		public double Index { get; set; }
		public AdPricingStrategies AdPricingStrategy { get; set; }
		public List<Insert> Inserts { get; set; }
		public string Note { get; set; }

		public SizeOptions SizeOptions { get; set; }

		public PublicationViewSettings ViewSettings { get; set; }
		public CustomSummaryItem SummaryItem { get; private set; }

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

		public ColorOptions ColorOption
		{
			get { return _colorOptions; }
			set
			{
				_colorOptions = value;
				if (value == ColorOptions.BlackWhite)
				{
					ColorPricing = ColorPricingType.None;
					foreach (var insert in Inserts)
					{
						insert.ColorPricing = 0;
						insert.ColorPricingPercent = 0;
						insert.ColorInchRate = 0;
					}
				}
			}
		}

		public ColorPricingType ColorPricing
		{
			get { return _colorPricing; }
			set
			{
				_colorPricing = value;
				switch (value)
				{
					case ColorPricingType.CostPerAd:
					case ColorPricingType.None:
						foreach (var insert in Inserts)
						{
							insert.ColorPricingPercent = 0;
							insert.ColorInchRate = 0;
						}
						break;
					case ColorPricingType.PercentOfAdRate:
						foreach (var insert in Inserts)
						{
							insert.ColorPricing = 0;
							insert.ColorInchRate = 0;
						}
						break;
					case ColorPricingType.ColorIncluded:
						foreach (var insert in Inserts)
						{
							insert.ColorPricing = 0;
							insert.ColorPricingPercent = 0;
							insert.ColorInchRate = 0;
						}
						break;
					case ColorPricingType.CostPerInch:
						foreach (var insert in Inserts)
						{
							insert.ColorPricing = 0;
							insert.ColorPricingPercent = 0;
						}
						break;
				}
			}
		}

		public double? TotalSquare
		{
			get
			{
				if (SizeOptions.Square.HasValue)
					return SizeOptions.Square * TotalInserts;
				else
					return null;
			}
		}

		public double TotalInserts
		{
			get { return Inserts.Count(x => x.Date.HasValue); }
		}

		public double AvgADRate
		{
			get { return TotalInserts != 0 ? Inserts.Where(x => x.Date.HasValue).Sum(x => x.ADRate) / TotalInserts : 0; }
		}

		public double AvgPCIRate
		{
			get { return TotalInserts != 0 ? Inserts.Where(x => x.Date.HasValue).Sum(x => (x.PCIRate.HasValue ? x.PCIRate.Value : 0)) / TotalInserts : 0; }
		}

		public double TotalDiscountRate
		{
			get { return Inserts.Where(x => x.Date.HasValue).Sum(x => x.DiscountRate); }
		}

		public double TotalColorPricingCalculated
		{
			get { return Inserts.Where(x => x.Date.HasValue).Sum(x => x.ColorPricingCalculated); }
		}

		public double AvgFinalRate
		{
			get { return TotalInserts != 0 ? Inserts.Where(x => x.Date.HasValue).Sum(x => x.FinalRate) / TotalInserts : 0; }
		}

		public double TotalFinalRate
		{
			get { return Inserts.Where(x => x.Date.HasValue).Sum(x => x.FinalRate); }
		}

		public string InsertSuffix
		{
			get
			{
				char[] alpha = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
				int lettersNumbers = ((int)Index / 25) + 1;
				string result = string.Empty;
				for (int i = 0; i < lettersNumbers; i++)
					result += alpha[((int)Index - 1) - 25 * i].ToString();
				return result;
			}
		}

		public string InsertDates
		{
			get { return String.Join(", ", Inserts.Where(i => i.Date.HasValue).Select(i => i.Date.Value.ToString("MM/dd/yy"))); }
		}

		private string _encodedBigLogo;
		public string EncodedBigLogo
		{
			get
			{
				if (String.IsNullOrEmpty(_encodedBigLogo))
				{
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
					_encodedBigLogo = Convert.ToBase64String((byte[])converter.ConvertTo(BigLogo, typeof(byte[])));
				}
				return _encodedBigLogo;
			}
			set { _encodedBigLogo = value; }
		}

		#region Available Weekdays
		public bool AllowSundaySelect { get; set; }
		public bool AllowMondaySelect { get; set; }
		public bool AllowTuesdaySelect { get; set; }
		public bool AllowWednesdaySelect { get; set; }
		public bool AllowThursdaySelect { get; set; }
		public bool AllowFridaySelect { get; set; }
		public bool AllowSaturdaySelect { get; set; }
		public List<DateTime> AvailableDays { get; private set; }
		#endregion

		public string Serialize()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
			var xml = new StringBuilder();

			xml.Append(@"<Publication ");
			xml.Append("Name = \"" + Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("UniqueID = \"" + UniqueID + "\" ");
			xml.Append("Abbreviation = \"" + Abbreviation.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("DailyDelivery = \"" + (DailyDelivery ?? 0) + "\" ");
			xml.Append("DailyReadership = \"" + (DailyReadership ?? 0) + "\" ");
			xml.Append("SundayDelivery = \"" + (SundayDelivery ?? 0) + "\" ");
			xml.Append("SundayReadership = \"" + (SundayReadership ?? 0) + "\" ");
			xml.Append("Index = \"" + Index + "\" ");
			xml.Append("AdPricingStrategy = \"" + (int)AdPricingStrategy + "\" ");
			xml.Append("ColorOption = \"" + (int)ColorOption + "\" ");
			xml.Append("ColorPricing = \"" + (int)ColorPricing + "\" ");
			xml.Append("Note = \"" + Note.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("BigLogo = \"" + Convert.ToBase64String((byte[])converter.ConvertTo(BigLogo, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("SmallLogo = \"" + Convert.ToBase64String((byte[])converter.ConvertTo(SmallLogo, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("TinyLogo = \"" + Convert.ToBase64String((byte[])converter.ConvertTo(TinyLogo, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("AllowSundaySelect = \"" + AllowSundaySelect.ToString() + "\" ");
			xml.Append("AllowMondaySelect = \"" + AllowMondaySelect.ToString() + "\" ");
			xml.Append("AllowTuesdaySelect = \"" + AllowTuesdaySelect.ToString() + "\" ");
			xml.Append("AllowWednesdaySelect = \"" + AllowWednesdaySelect.ToString() + "\" ");
			xml.Append("AllowThursdaySelect = \"" + AllowThursdaySelect.ToString() + "\" ");
			xml.Append("AllowFridaySelect = \"" + AllowFridaySelect.ToString() + "\" ");
			xml.Append("AllowSaturdaySelect = \"" + AllowSaturdaySelect.ToString() + "\" ");
			xml.AppendLine(@">");
			xml.AppendLine(SizeOptions.Serialize());
			xml.AppendLine(@"<ViewSettings>" + ViewSettings.Serialize() + @"</ViewSettings>");
			xml.AppendLine(@"<SummaryItem>" + SummaryItem.Serialize() + @"</SummaryItem>");
			foreach (Insert insert in Inserts)
				xml.AppendLine(insert.Serialize());
			xml.AppendLine(@"</Publication>");

			return xml.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			int tempInt = 0;
			bool tempBool;
			double tempDouble;
			Guid tempGuid;

			foreach (XmlAttribute attribute in node.Attributes)
				switch (attribute.Name)
				{
					case "Name":
						Name = attribute.Value;
						break;
					case "UniqueID":
						if (Guid.TryParse(attribute.Value, out tempGuid))
							UniqueID = tempGuid;
						break;
					case "Abbreviation":
						Abbreviation = attribute.Value;
						break;
					case "DailyDelivery":
						tempDouble = 0;
						DailyDelivery = null;
						double.TryParse(attribute.Value, out tempDouble);
						if (tempDouble > 0)
							DailyDelivery = tempDouble;
						break;
					case "DailyReadership":
						tempDouble = 0;
						DailyReadership = null;
						double.TryParse(attribute.Value, out tempDouble);
						if (tempDouble > 0)
							DailyReadership = tempDouble;
						break;
					case "SundayDelivery":
						tempDouble = 0;
						SundayDelivery = null;
						double.TryParse(attribute.Value, out tempDouble);
						if (tempDouble > 0)
							SundayDelivery = tempDouble;
						break;
					case "SundayReadership":
						tempDouble = 0;
						SundayReadership = null;
						double.TryParse(attribute.Value, out tempDouble);
						if (tempDouble > 0)
							SundayReadership = tempDouble;
						break;
					case "Index":
						tempInt = 0;
						int.TryParse(attribute.Value, out tempInt);
						Index = tempInt;
						break;
					case "AdPricingStrategy":
						tempInt = 0;
						int.TryParse(attribute.Value, out tempInt);
						AdPricingStrategy = (AdPricingStrategies)tempInt;
						break;
					case "ColorOption":
						tempInt = 0;
						int.TryParse(attribute.Value, out tempInt);
						ColorOption = (ColorOptions)tempInt;
						break;
					case "ColorPricing":
						tempInt = 0;
						int.TryParse(attribute.Value, out tempInt);
						ColorPricing = (ColorPricingType)tempInt;
						break;
					case "Note":
						Note = attribute.Value;
						break;
					case "BigLogo":
						if (string.IsNullOrEmpty(attribute.Value))
							BigLogo = null;
						else
							BigLogo = new Bitmap(new MemoryStream(Convert.FromBase64String(attribute.Value)));
						break;
					case "SmallLogo":
						if (string.IsNullOrEmpty(attribute.Value))
							SmallLogo = null;
						else
							SmallLogo = new Bitmap(new MemoryStream(Convert.FromBase64String(attribute.Value)));
						break;
					case "TinyLogo":
						if (string.IsNullOrEmpty(attribute.Value))
							TinyLogo = null;
						else
							TinyLogo = new Bitmap(new MemoryStream(Convert.FromBase64String(attribute.Value)));
						break;

					case "AllowSundaySelect":
						if (bool.TryParse(attribute.Value, out tempBool))
							AllowSundaySelect = tempBool;
						break;
					case "AllowMondaySelect":
						if (bool.TryParse(attribute.Value, out tempBool))
							AllowMondaySelect = tempBool;
						break;
					case "AllowTuesdaySelect":
						if (bool.TryParse(attribute.Value, out tempBool))
							AllowTuesdaySelect = tempBool;
						break;
					case "AllowWednesdaySelect":
						if (bool.TryParse(attribute.Value, out tempBool))
							AllowWednesdaySelect = tempBool;
						break;
					case "AllowThursdaySelect":
						if (bool.TryParse(attribute.Value, out tempBool))
							AllowThursdaySelect = tempBool;
						break;
					case "AllowFridaySelect":
						if (bool.TryParse(attribute.Value, out tempBool))
							AllowFridaySelect = tempBool;
						break;
					case "AllowSaturdaySelect":
						if (bool.TryParse(attribute.Value, out tempBool))
							AllowSaturdaySelect = tempBool;
						break;
				}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "SizeOptions":
						SizeOptions.Deserialize(childNode);
						break;
					case "ViewSettings":
						ViewSettings.Deserialize(childNode);
						break;
					case "SummaryItem":
						SummaryItem.Deserialize(childNode);
						break;
					case "Insert":
						var insert = new Insert(this);
						insert.Deserialize(childNode);
						Inserts.Add(insert);
						break;
				}
			}

			if ((AdPricingStrategy == AdPricingStrategies.StandartPCI && !ListManager.Instance.DefaultPrintScheduleViewSettings.EnablePCI) ||
				(AdPricingStrategy == AdPricingStrategies.FlatModular && !ListManager.Instance.DefaultPrintScheduleViewSettings.EnableFlat) ||
				(AdPricingStrategy == AdPricingStrategies.SharePage && !ListManager.Instance.DefaultPrintScheduleViewSettings.EnableShare))
				AdPricingStrategy = ListManager.Instance.DefaultPricingStrategy;

			if ((ColorOption == ColorOptions.BlackWhite && !ListManager.Instance.DefaultPrintScheduleViewSettings.EnableBlackWhite) ||
				(ColorOption == ColorOptions.SpotColor && !ListManager.Instance.DefaultPrintScheduleViewSettings.EnableSpotColor) ||
				(ColorOption == ColorOptions.FullColor && !ListManager.Instance.DefaultPrintScheduleViewSettings.EnableFullColor))
				ColorOption = ListManager.Instance.DefaultColor;

			if ((ColorPricing == ColorPricingType.CostPerAd && !ListManager.Instance.DefaultPrintScheduleViewSettings.EnableCostPerAd) ||
				(ColorPricing == ColorPricingType.PercentOfAdRate && !ListManager.Instance.DefaultPrintScheduleViewSettings.EnablePercentOfAd) ||
				(ColorPricing == ColorPricingType.ColorIncluded && !ListManager.Instance.DefaultPrintScheduleViewSettings.EnableColorIncluded) ||
				(ColorPricing == ColorPricingType.CostPerInch && !ListManager.Instance.DefaultPrintScheduleViewSettings.EnableCostPerInch))
				ColorPricing = ListManager.Instance.DefaultColorPricing;
		}

		public void AddInsert()
		{
			Inserts.Add(new Insert(this));
		}

		public void SortInserts()
		{
			Inserts.Sort((x, y) => !x.Date.HasValue || !y.Date.HasValue || x.Date.Value.CompareTo(y.Date.Value) == 0 ? x.Index.CompareTo(y.Index) : x.Date.Value.CompareTo(y.Date.Value));
		}

		public void RebuildInserts()
		{
			var temp = new List<Insert>();
			temp.AddRange(Inserts);
			temp.Sort((x, y) => !x.Date.HasValue || !y.Date.HasValue || x.Date.Value.CompareTo(y.Date.Value) == 0 ? x.Index.CompareTo(y.Index) : x.Date.Value.CompareTo(y.Date.Value));
			if (!Parent.FlightDateStart.HasValue || !Parent.FlightDateEnd.HasValue) return;
			var startDate = Parent.FlightDateStart.Value;
			var endDate = startDate.AddDays(7);
			do
			{
				var insertsPerWeek =
					from insert in temp
					where insert.Date >= startDate && insert.Date < endDate
					select insert;
				for (int i = 0; i < insertsPerWeek.Count(); i++)
				{
					Inserts[Inserts.IndexOf(insertsPerWeek.ElementAt(i))].Index = i + 1;
				}
				startDate = endDate;
				endDate = startDate.AddDays(7);
			} while (startDate < Parent.FlightDateEnd);
		}

		public void CloneInsert(Insert originalInsert, DateTime[] cloneDates, bool copyPCIRate, bool copyDiscounts, bool copyColorRate, bool comment, bool section, bool deadline, bool mechanicals)
		{
			foreach (DateTime cloneDate in cloneDates)
			{
				var insert = new Insert(this);
				insert.Date = cloneDate;
				if (copyPCIRate)
				{
					insert.PCIRate = originalInsert.PCIRate;
					insert.ADRate = originalInsert.ADRate;
				}
				if (copyDiscounts)
					insert.Discounts = originalInsert.Discounts;
				if (copyColorRate)
				{
					insert.ColorPricing = originalInsert.ColorPricing;
					insert.ColorPricingPercent = originalInsert.ColorPricingPercent;
					insert.ColorInchRate = originalInsert.ColorInchRate;
				}
				if (comment)
				{
					insert.CustomComment = originalInsert.CustomComment;
					foreach (var originalComment in originalInsert.Comments)
					{
						var newComment = new NameCodePair();
						newComment.Name = originalComment.Name;
						newComment.Code = originalComment.Code;
						insert.Comments.Add(newComment);
					}
				}
				if (section)
				{
					insert.CustomSection = originalInsert.CustomSection;
					foreach (var originalSection in originalInsert.Sections)
					{
						var newSection = new NameCodePair();
						newSection.Name = originalSection.Name;
						newSection.Code = originalSection.Code;
						insert.Sections.Add(newSection);
					}
				}
				if (deadline)
					insert.Deadline = originalInsert.Deadline;
				if (mechanicals)
					insert.Mechanicals = originalInsert.Mechanicals;
				Inserts.Add(insert);
			}
			RebuildInserts();
		}

		public void CopyPCIRate(double value)
		{
			foreach (var insert in Inserts)
				insert.PCIRate = value;
		}

		public void CopyAdRate(double value)
		{
			foreach (var insert in Inserts)
				insert.ADRate = value;
		}

		public void CopyDiscounts(double value)
		{
			foreach (var insert in Inserts)
				insert.Discounts = value;
		}

		public void CopyColorRate(double value)
		{
			foreach (var insert in Inserts)
				insert.ColorPricing = value;
		}

		public void CopyColorRatePercent(double value)
		{
			foreach (var insert in Inserts)
				insert.ColorPricingPercent = value;
		}

		public void CopyColorPCI(double value)
		{
			foreach (var insert in Inserts)
				insert.ColorInchRate = value;
		}

		public void RefreshAvailableDays()
		{
			AvailableDays.Clear();
			if (!Parent.FlightDateStart.HasValue || !Parent.FlightDateEnd.HasValue) return;
			var dayInSchedule = Parent.FlightDateStart.Value;
			while (dayInSchedule <= Parent.FlightDateEnd.Value)
			{
				if ((dayInSchedule.DayOfWeek == DayOfWeek.Sunday && AllowSundaySelect) ||
					(dayInSchedule.DayOfWeek == DayOfWeek.Monday && AllowMondaySelect) ||
					(dayInSchedule.DayOfWeek == DayOfWeek.Tuesday && AllowTuesdaySelect) ||
					(dayInSchedule.DayOfWeek == DayOfWeek.Wednesday && AllowWednesdaySelect) ||
					(dayInSchedule.DayOfWeek == DayOfWeek.Thursday && AllowThursdaySelect) ||
					(dayInSchedule.DayOfWeek == DayOfWeek.Friday && AllowFridaySelect) ||
					(dayInSchedule.DayOfWeek == DayOfWeek.Saturday && AllowSaturdaySelect))
					AvailableDays.Add(dayInSchedule);
				dayInSchedule = dayInSchedule.AddDays(1);
			}
		}

		public void ApplyDefaultValues()
		{
			PrintProductSource[] sourceSet = ListManager.Instance.PublicationSources.Where(x => x.Name.Equals(_name)).ToArray();
			if (sourceSet.Length > 0)
			{
				BigLogo = sourceSet[0].BigLogo != null ? new Bitmap(sourceSet[0].BigLogo) : null;
				;
				SmallLogo = sourceSet[0].SmallLogo != null ? new Bitmap(sourceSet[0].SmallLogo) : null;
				TinyLogo = sourceSet[0].TinyLogo != null ? new Bitmap(sourceSet[0].TinyLogo) : null;
				Abbreviation = sourceSet[0].Abbreviation;
				DailyDelivery = sourceSet.Where(x => x.Circulation == CirculationType.Daily).Select(x => x.Delivery).FirstOrDefault();
				DailyReadership = sourceSet.Where(x => x.Circulation == CirculationType.Daily).Select(x => x.Readership).FirstOrDefault();
				SundayDelivery = sourceSet.Where(x => x.Circulation == CirculationType.Sunday).Select(x => x.Delivery).FirstOrDefault();
				SundayReadership = sourceSet.Where(x => x.Circulation == CirculationType.Sunday).Select(x => x.Readership).FirstOrDefault();
				AllowSundaySelect = sourceSet[0].AllowSundaySelect;
				AllowMondaySelect = sourceSet[0].AllowMondaySelect;
				AllowTuesdaySelect = sourceSet[0].AllowTuesdaySelect;
				AllowWednesdaySelect = sourceSet[0].AllowWednesdaySelect;
				AllowThursdaySelect = sourceSet[0].AllowThursdaySelect;
				AllowFridaySelect = sourceSet[0].AllowFridaySelect;
				AllowSaturdaySelect = sourceSet[0].AllowSaturdaySelect;
			}
			else
			{
				string filePath = Path.Combine(ListManager.Instance.BigImageFolder.FullName, Common.ListManager.DefaultBigLogoFileName);
				if (File.Exists(filePath))
					BigLogo = new Bitmap(filePath);
				else
					BigLogo = null;
				filePath = Path.Combine(ListManager.Instance.SmallImageFolder.FullName, Common.ListManager.DefaultSmallLogoFileName);
				if (File.Exists(filePath))
					SmallLogo = new Bitmap(filePath);
				else
					SmallLogo = null;
				filePath = Path.Combine(ListManager.Instance.TinyImageFolder.FullName, Common.ListManager.DefaultTinyLogoFileName);
				if (File.Exists(filePath))
					TinyLogo = new Bitmap(filePath);
				else
					TinyLogo = null;
				Abbreviation = _name.Substring(0, 3).ToUpper();
			}
		}

		public PrintProduct Clone()
		{
			var result = new PrintProduct(Parent);
			var document = new XmlDocument();
			document.LoadXml(Serialize());
			result.Deserialize(document.FirstChild);
			result.UniqueID = Guid.NewGuid();
			result.Index = Index + 0.5;
			Parent.PrintProducts.Add(result);
			Parent.PrintProducts.Sort((x, y) => x.Index.CompareTo(y.Index));
			Parent.RebuildPublicationIndexes();
			return result;
		}

		public decimal SummaryOrder
		{
			get { return (Decimal)Index; }
		}

		public string SummaryTitle
		{
			get { return Name; }
		}

		public string SummaryInfo
		{
			get
			{
				var values = new List<string>();
				if (SizeOptions.EnablePageSize && (!String.IsNullOrEmpty(SizeOptions.PageSizeAndGroup)))
					values.Add(String.Format("Ad Size: {0}", SizeOptions.PageSizeAndGroup));
				if (SizeOptions.EnableSquare && SizeOptions.Square.HasValue)
					values.Add(String.Format("{0}", SizeOptions.DimensionsShort));
				if (!String.IsNullOrEmpty(SizeOptions.PercentOfPage))
					values.Add(String.Format("{0}", SizeOptions.PercentOfPage));
				if (TotalInserts > 0)
					values.Add(String.Format("{0} Ads", TotalInserts));
				values.Add(String.Format("{0}", Parent.FlightDates));
				return String.Join(", ", values).Replace(Environment.NewLine, ", ");
			}
		}
	}

	public class Insert
	{
		private double _adRate;
		private double _pciRate;
		private DateTime? _date;

		public Insert(PrintProduct parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Index = 1;
			CustomSection = string.Empty;
			CustomComment = string.Empty;
			Deadline = string.Empty;
			Comments = new List<NameCodePair>();
			Sections = new List<NameCodePair>();
		}

		public PrintProduct Parent { get; private set; }
		public Guid UniqueID { get; set; }
		public int Index { get; set; }
		public double Discounts { get; set; }
		public double ColorPricing { get; set; }
		public double ColorInchRate { get; set; }
		public double ColorPricingPercent { get; set; }
		public string CustomComment { get; set; }
		public string CustomSection { get; set; }
		public string Deadline { get; set; }
		public string Mechanicals { get; set; }
		public List<NameCodePair> Comments { get; private set; }
		public List<NameCodePair> Sections { get; private set; }

		public Guid ParentID
		{
			get { return Parent.UniqueID; }
		}

		public string ID
		{
			get { return (Parent.Inserts.IndexOf(this) + 1).ToString("000") + Parent.InsertSuffix; }
		}

		public DateTime? Date
		{
			get
			{
				return _date;
			}
			set
			{
				_date = value;
				Parent.RebuildInserts();
			}
		}

		public double? PCIRate
		{
			get
			{
				switch (Parent.AdPricingStrategy)
				{
					case AdPricingStrategies.StandartPCI:
						return _pciRate;
					case AdPricingStrategies.FlatModular:
						if (Parent.SizeOptions.Square.HasValue && Parent.SizeOptions.Square.Value > 0)
							return _adRate / Parent.SizeOptions.Square.Value;
						else
							return null;
					case AdPricingStrategies.SharePage:
						return null;
					default:
						return null;
				}
			}
			set
			{
				if (Parent.AdPricingStrategy == AdPricingStrategies.StandartPCI && value.HasValue)
					_pciRate = value.Value;
			}
		}

		public double ADRate
		{
			get
			{
				switch (Parent.AdPricingStrategy)
				{
					case AdPricingStrategies.StandartPCI:
						if (Parent.SizeOptions.Square.HasValue)
							return _pciRate * Parent.SizeOptions.Square.Value;
						else
							return 0;
					case AdPricingStrategies.FlatModular:
					case AdPricingStrategies.SharePage:
						return _adRate;
					default:
						return 0;
				}
			}
			set
			{
				if (Parent.AdPricingStrategy == AdPricingStrategies.FlatModular || Parent.AdPricingStrategy == AdPricingStrategies.SharePage)
					_adRate = value;
			}
		}

		public double ColorPricingCalculated
		{
			get
			{
				switch (Parent.ColorPricing)
				{
					case ColorPricingType.CostPerAd:
						return ColorPricing;
					case ColorPricingType.PercentOfAdRate:
						return ADRate * (ColorPricingPercent / 100.00);
					case ColorPricingType.ColorIncluded:
						return 0;
					case ColorPricingType.CostPerInch:
						if (Parent.SizeOptions.Square.HasValue)
							return Parent.SizeOptions.Square.Value * ColorInchRate;
						else
							return 0;
					default:
						return 0;
				}
			}
		}

		public object ColorPricingObject
		{
			get
			{
				if (Parent.ColorOption == ColorOptions.BlackWhite)
					return "B-W";
				if (Parent.ColorPricing == ColorPricingType.ColorIncluded)
					return "Included";
				return ColorPricingCalculated;
			}
			set
			{
				if (value == null)
				{
					ColorPricing = 0;
				}
				else
				{
					double temp = 0;
					double.TryParse(value.ToString(), out temp);
					ColorPricing = temp;
				}
			}
		}

		public double DiscountRate
		{
			get
			{
				if (ListManager.Instance.DefaultPrintScheduleViewSettings.CalcDiscountBeforeColor)
					return ADRate * (Discounts / 100.00);
				return (ADRate + ColorPricingCalculated) * (Discounts / 100.00);
			}
		}

		public double FinalRate
		{
			get { return ADRate - DiscountRate + ColorPricingCalculated; }
		}

		public double? PublicationSquare
		{
			get { return Parent.SizeOptions.Square; }
		}

		public string FullComment
		{
			get
			{
				var comments = new List<string>();
				if (!string.IsNullOrEmpty(CustomComment))
					comments.Add(CustomComment);
				foreach (Common.NameCodePair comment in Comments)
				{
					if (!string.IsNullOrEmpty(comment.Code) && (Comments.Count + (!string.IsNullOrEmpty(CustomComment) ? 1 : 0)) >= ListManager.Instance.SelectedCommentsBorderValue)
						comments.Add(comment.Code);
					else if (!string.IsNullOrEmpty(comment.Name))
						comments.Add(comment.Name);
				}
				return string.Join(", ", comments.ToArray());
			}
		}

		public string FullSection
		{
			get
			{
				var sections = new List<string>();
				if (!string.IsNullOrEmpty(CustomSection))
					sections.Add(CustomSection);
				foreach (var section in Sections)
				{
					if (!string.IsNullOrEmpty(section.Code) && (Sections.Count + (!string.IsNullOrEmpty(CustomSection) ? 1 : 0)) >= ListManager.Instance.SelectedSectionsBorderValue)
						sections.Add(section.Code);
					else if (!string.IsNullOrEmpty(section.Name))
						sections.Add(section.Name);
				}
				return sections.Count > 0 ?
					string.Join(", ", sections.ToArray()) :
					string.Empty;
			}
		}

		#region Output Stuff
		public DateTime? EndDate
		{
			get { return Date.HasValue ? Date.Value.AddHours(1) : (DateTime?)null; }
		}

		public string PageSize
		{
			get { return Parent.SizeOptions.PageSizeAndGroup; }
		}

		public string PageSizeOutput
		{
			get { return !string.IsNullOrEmpty(Parent.SizeOptions.PageSizeAndGroup) ? Parent.SizeOptions.PageSizeAndGroup : "N/A"; }
		}

		public string PercentOfPage
		{
			get { return Parent.SizeOptions.PercentOfPage; }
		}

		public string PercentOfPageOutput
		{
			get { return !string.IsNullOrEmpty(Parent.SizeOptions.PercentOfPage) ? Parent.SizeOptions.PercentOfPage : "N/A"; }
		}

		public string MechanicalsOutput
		{
			get { return !string.IsNullOrEmpty(Mechanicals) ? Mechanicals : "N/A"; }
		}

		public string Publication
		{
			get { return Parent.Name; }
		}

		public string PublicationAbbreviation
		{
			get { return Parent.Abbreviation; }
		}

		public string SquareStringFormatted
		{
			get { return PublicationSquare.HasValue && PublicationSquare.Value > 0 ? (PublicationSquare.Value.ToString("#,###.00#")) : null; }
		}

		public string Dimensions
		{
			get { return Parent.SizeOptions.Dimensions; }
		}

		public string DimensionsOutput
		{
			get { return !string.IsNullOrEmpty(Parent.SizeOptions.Dimensions) ? Parent.SizeOptions.Dimensions : "N/A"; }
		}

		public string DimensionsShort
		{
			get { return Parent.SizeOptions.DimensionsShort; }
		}

		public string DeadlineForOutput
		{
			get
			{
				string result = string.Empty;
				if (!Date.HasValue) return result;
				if (Deadline.ToLower().Contains("day"))
				{
					var re = new Regex(@"\d+");
					Match m = re.Match(Deadline);
					if (m.Success)
					{
						int daysNumber = 0;
						if (int.TryParse(m.Value, out daysNumber))
						{
							DateTime deadline = Date.Value.AddDays(0 - daysNumber);
							while (deadline.DayOfWeek == DayOfWeek.Saturday || deadline.DayOfWeek == DayOfWeek.Sunday)
								deadline = deadline.AddDays(-1);
							result = deadline.ToString("ddd, MM/dd/yy");
						}
					}
				}
				else
					result = Deadline;
				return result;
			}
		}

		public string Delivery
		{
			get
			{
				string result = string.Empty;
				if (!Date.HasValue) return result;
				if (Date.Value.DayOfWeek == DayOfWeek.Sunday)
				{
					if (Parent.SundayDelivery != null)
						if (Parent.SundayDelivery.Value > 0)
							result = Parent.SundayDelivery.Value.ToString("#,##0");
				}
				else
				{
					if (Parent.DailyDelivery != null)
						if (Parent.DailyDelivery.Value > 0)
							result = Parent.DailyDelivery.Value.ToString("#,##0");
				}
				return result;
			}
		}

		public string Readership
		{
			get
			{
				string result = string.Empty;
				if (!Date.HasValue) return result;
				if (Date.Value.DayOfWeek == DayOfWeek.Sunday)
				{
					if (Parent.SundayReadership != null)
						if (Parent.SundayReadership.Value > 0)
							result = Parent.SundayReadership.Value.ToString("#,##0");
				}
				else
				{
					if (Parent.DailyReadership != null)
						if (Parent.DailyReadership.Value > 0)
							result = Parent.DailyReadership.Value.ToString("#,##0");
				}
				return result;
			}
		}

		public Image MultiGridLogo
		{
			get { return Parent.TinyLogo; }
		}

		public double PublicationIndex
		{
			get { return Parent.Index; }
		}

		public string PublicationColor
		{
			get
			{
				switch (Parent.ColorOption)
				{
					case ColorOptions.BlackWhite:
						return "bw";
					case ColorOptions.SpotColor:
						return "sc";
					case ColorOptions.FullColor:
						return "fc";
					default:
						return string.Empty;
				}
			}
		}
		#endregion

		public string Serialize()
		{
			var xml = new StringBuilder();

			xml.Append(@"<Insert ");
			xml.Append("Index = \"" + Index + "\" ");
			if (_date.HasValue)
				xml.Append("Date = \"" + _date + "\" ");
			xml.Append("PCIRate = \"" + (PCIRate.HasValue ? PCIRate.Value : 0) + "\" ");
			xml.Append("ADRate = \"" + ADRate + "\" ");
			xml.Append("Discounts = \"" + Discounts + "\" ");
			xml.Append("ColorPricing = \"" + ColorPricing + "\" ");
			xml.Append("ColorInchRate = \"" + ColorInchRate + "\" ");
			xml.Append("ColorPricingPercent = \"" + ColorPricingPercent + "\" ");
			xml.Append("CustomSection = \"" + CustomSection.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("CustomComment = \"" + CustomComment.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("Deadline = \"" + Deadline.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("Mechanicals = \"" + (Mechanicals != null ? Mechanicals.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + "\" ");
			xml.AppendLine(@">");
			xml.AppendLine(@"<Comments>");
			foreach (Common.NameCodePair comment in Comments)
				xml.AppendLine(comment.Serialize());
			xml.AppendLine(@"</Comments>");
			xml.AppendLine(@"<Sections>");
			foreach (Common.NameCodePair section in Sections)
				xml.AppendLine(section.Serialize());
			xml.AppendLine(@"</Sections>");
			xml.AppendLine(@"</Insert>");

			return xml.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			double tempDouble;

			foreach (XmlAttribute attribute in node.Attributes)
				switch (attribute.Name)
				{
					case "Index":
						int tempInt;
						if (int.TryParse(attribute.Value, out tempInt))
							Index = tempInt;
						break;
					case "Date":
						DateTime tempDateTime;
						if (DateTime.TryParse(attribute.Value, out tempDateTime) && tempDateTime != DateTime.MinValue && tempDateTime != DateTime.MaxValue)
							Date = tempDateTime;
						break;
					case "PCIRate":
						if (double.TryParse(attribute.Value, out tempDouble))
							PCIRate = tempDouble;
						break;
					case "ADRate":
						if (double.TryParse(attribute.Value, out tempDouble))
							ADRate = tempDouble;
						break;
					case "Discounts":
						if (double.TryParse(attribute.Value, out tempDouble))
							Discounts = tempDouble;
						break;
					case "ColorPricing":
						if (double.TryParse(attribute.Value, out tempDouble))
							ColorPricing = tempDouble;
						break;
					case "ColorInchRate":
						if (double.TryParse(attribute.Value, out tempDouble))
							ColorInchRate = tempDouble;
						break;
					case "ColorPricingPercent":
						if (double.TryParse(attribute.Value, out tempDouble))
							ColorPricingPercent = tempDouble;
						break;
					case "CustomSection":
						CustomSection = attribute.Value;
						break;
					case "CustomComment":
						CustomComment = attribute.Value;
						break;
					case "Deadline":
						Deadline = attribute.Value;
						break;
					case "Mechanicals":
						if (!string.IsNullOrEmpty(attribute.Value))
							Mechanicals = attribute.Value;
						break;
				}
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Comments":
						Comments.Clear();
						foreach (XmlNode commentNode in childNode.ChildNodes)
						{
							var comment = new NameCodePair();
							comment.Deserialize(commentNode);
							Comments.Add(comment);
						}
						break;
					case "Sections":
						Sections.Clear();
						foreach (XmlNode sectionNode in childNode.ChildNodes)
						{
							var section = new NameCodePair();
							section.Deserialize(sectionNode);
							Sections.Add(section);
						}
						break;
				}
			}
		}

		public void SaveRates()
		{
			if (Parent.AdPricingStrategy == AdPricingStrategies.StandartPCI)
				_adRate = Math.Round(ADRate, 2);
			else
				_pciRate = PCIRate.HasValue ? Math.Round(PCIRate.Value, 2) : 0;
		}

		public void ResetRates()
		{
			_pciRate = 0;
			_adRate = 0;
		}
	}

	public class SizeOptions
	{
		public SizeOptions()
		{
			ResetToDefaults(AdPricingStrategies.StandartPCI);
		}

		public double Width { get; set; }
		public double Height { get; set; }
		public string WidthMeasure { get; set; }
		public string HeightMeasure { get; set; }
		public bool EnableSquare { get; set; }
		public string PageSizeGroup { get; set; }
		public string PageSize { get; set; }
		public bool EnablePageSize { get; set; }
		public string RateCard { get; set; }
		public string PercentOfPage { get; set; }
		public bool EnableMechanicals { get; set; }
		public string Mechanicals { get; set; }

		#region Calculated Options
		public string ShortWidthMeasure
		{
			get
			{
				switch (WidthMeasure.ToLower())
				{
					case "columns":
						return " col.";
					case "inches":
						return "''";
					case "depth":
						return " depth";
					default:
						return string.Empty;
				}
			}
		}

		public string ShortHeightMeasure
		{
			get
			{
				switch (HeightMeasure.ToLower())
				{
					case "columns":
						return " col.";
					case "inches":
						return "''";
					case "depth":
						return " depth";
					default:
						return string.Empty;
				}
			}
		}

		public double? Square
		{
			get
			{
				if (Width > 0 && Height > 0)
					return Width * Height;
				return null;
			}
		}

		public string Dimensions
		{
			get { return Square.HasValue ? (string.Format("{0}{1} x {2}{3}", new object[] { Width.ToString("#,##0.00"), ShortWidthMeasure, Height.ToString("#,###.00#"), ShortHeightMeasure })) : null; }
		}

		public string DimensionsShort
		{
			get { return Square.HasValue ? (String.Format("{0}x{1}", Width.ToString("#,##0.00"), Height.ToString("#,###.00#"))) : "N/A"; }
		}

		public string PageSizeAndGroup
		{
			get
			{
				var pageSizeGroupValues = new List<string>();
				if (!String.IsNullOrEmpty(PageSizeGroup) && ListManager.Instance.PageSizes.Select(ps => ps.Code).Distinct().Count() > 1)
					pageSizeGroupValues.Add(PageSizeGroup);
				if (EnableMechanicals && !String.IsNullOrEmpty(Mechanicals))
					pageSizeGroupValues.Add(Mechanicals);
				return EnablePageSize && !String.IsNullOrEmpty(PageSize) ?
					String.Format("{0}{1}",
						PageSize,
						pageSizeGroupValues.Any() ? String.Format(" ({0})", String.Join("-", pageSizeGroupValues)) : String.Empty
						) :
					String.Empty;
			}
		}

		public string PageSizeOutput
		{
			get { return !String.IsNullOrEmpty(PageSizeAndGroup) ? PageSizeAndGroup : "N/A"; }
		}

		public string PercentOfPageOutput
		{
			get { return !string.IsNullOrEmpty(PercentOfPage) ? PercentOfPage : "N/A"; }
		}

		public ShareUnit RelatedShareUnit
		{
			get
			{
				var shareUnit = new ShareUnit();
				shareUnit.Width = Width.ToString("#,##0.00");
				shareUnit.WidthMeasureUnit = WidthMeasure;
				shareUnit.Height = Height.ToString("#,##0.00#");
				shareUnit.HeightMeasureUnit = HeightMeasure;
				return shareUnit;
			}
		}
		#endregion

		public string Serialize()
		{
			var xml = new StringBuilder();

			xml.Append(@"<SizeOptions ");
			xml.Append("Width = \"" + Width + "\" ");
			xml.Append("Height = \"" + Height + "\" ");
			xml.Append("WidthMeasure = \"" + WidthMeasure.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("HeightMeasure = \"" + HeightMeasure.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("EnableSquare = \"" + EnableSquare + "\" ");
			xml.Append("PageSizeGroup = \"" + (PageSizeGroup != null ? PageSizeGroup.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + "\" ");
			xml.Append("PageSize = \"" + (PageSize != null ? PageSize.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + "\" ");
			xml.Append("EnablePageSize = \"" + EnablePageSize + "\" ");
			xml.Append("RateCard = \"" + (RateCard != null ? RateCard.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + "\" ");
			xml.Append("PercentOfPage = \"" + (PercentOfPage != null ? PercentOfPage.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + "\" ");
			xml.Append("EnableMechanicals = \"" + EnableMechanicals + "\" ");
			xml.Append("Mechanicals = \"" + (!String.IsNullOrEmpty(Mechanicals) ? Mechanicals.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + "\" ");
			xml.AppendLine(@"/>");

			return xml.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool;
			double tempDouble;

			foreach (XmlAttribute attribute in node.Attributes)
				switch (attribute.Name)
				{
					case "Width":
						if (double.TryParse(attribute.Value, out tempDouble))
							Width = tempDouble;
						break;
					case "Height":
						if (double.TryParse(attribute.Value, out tempDouble))
							Height = tempDouble;
						break;
					case "WidthMeasure":
						WidthMeasure = attribute.Value;
						break;
					case "HeightMeasure":
						HeightMeasure = attribute.Value;
						break;
					case "EnableSquare":
						if (bool.TryParse(attribute.Value, out tempBool))
							EnableSquare = tempBool;
						break;
					case "PageSizeGroup":
						if (!string.IsNullOrEmpty(attribute.Value))
							PageSizeGroup = attribute.Value;
						break;
					case "PageSize":
						if (!string.IsNullOrEmpty(attribute.Value))
							PageSize = attribute.Value;
						break;
					case "EnablePageSize":
						if (bool.TryParse(attribute.Value, out tempBool))
							EnablePageSize = tempBool;
						break;
					case "RateCard":
						if (!string.IsNullOrEmpty(attribute.Value))
							RateCard = attribute.Value;
						break;
					case "PercentOfPage":
						if (!string.IsNullOrEmpty(attribute.Value))
							PercentOfPage = attribute.Value;
						break;
					case "EnableMechanicals":
						if (bool.TryParse(attribute.Value, out tempBool))
							EnableMechanicals = tempBool;
						break;
					case "Mechanicals":
						if (!string.IsNullOrEmpty(attribute.Value))
							Mechanicals = attribute.Value;
						break;
				}
		}

		public void ResetToDefaults(AdPricingStrategies pricing)
		{
			WidthMeasure = "Columns";
			HeightMeasure = "Inches";
			Width = 0;
			Height = 0;
			RateCard = null;
			PercentOfPage = null;
			EnablePageSize = true;
			EnableMechanicals = ListManager.Instance.DefaultPrintScheduleViewSettings.DefaultMechanicals;
			switch (pricing)
			{
				case AdPricingStrategies.StandartPCI:
					EnableSquare = true;
					break;
				case AdPricingStrategies.FlatModular:
					EnableSquare = false;
					break;
				case AdPricingStrategies.SharePage:
					EnableSquare = false;
					break;
			}
		}
	}

	public class PrintProductSource : ICloneable
	{
		public PrintProductSource()
		{
			Name = string.Empty;
			Abbreviation = string.Empty;
			BigLogo = null;
			SmallLogo = null;
			TinyLogo = null;
			Circulation = CirculationType.Daily;
			Delivery = null;
			Readership = null;
		}

		public string Name { get; set; }
		public string Abbreviation { get; set; }
		public Image BigLogo { get; set; }
		public string BigLogoFileName { get; set; }
		public Image SmallLogo { get; set; }
		public string SmallLogoFileName { get; set; }
		public Image TinyLogo { get; set; }
		public string TinyLogoFileName { get; set; }
		public CirculationType Circulation { get; set; }
		public double? Delivery { get; set; }
		public double? Readership { get; set; }

		public bool AllowSundaySelect { get; set; }
		public bool AllowMondaySelect { get; set; }
		public bool AllowTuesdaySelect { get; set; }
		public bool AllowWednesdaySelect { get; set; }
		public bool AllowThursdaySelect { get; set; }
		public bool AllowFridaySelect { get; set; }
		public bool AllowSaturdaySelect { get; set; }

		#region ICloneable Members
		public object Clone()
		{
			var clone = new PrintProductSource();
			clone.Name = Name;
			clone.Abbreviation = Abbreviation;
			clone.BigLogo = BigLogo;
			clone.SmallLogo = SmallLogo;
			clone.TinyLogo = TinyLogo;
			clone.Circulation = Circulation;
			clone.Delivery = Delivery;
			clone.Readership = Readership;
			return clone;
		}
		#endregion
	}

	public class AdCalendar : CalendarSundayBased
	{
		public AdCalendar(ISchedule parent) : base(parent) { }

		public override void Deserialize(XmlNode node)
		{
			Deserialize<AdCalendarMonth, AdCalendarDay,CalendarNote>(node, DayOfWeek.Sunday, DayOfWeek.Saturday);
		}

		public override void UpdateMonthCollection()
		{
			UpdateMonthCollection<AdCalendarMonth>();
		}

		public override void UpdateDaysCollection()
		{
			UpdateDaysCollection<AdCalendarDay>(DayOfWeek.Sunday, DayOfWeek.Saturday);
		}

		public override DateTime[][] GetDaysByWeek(DateTime start, DateTime end)
		{
			return GetDaysByWeek(start, end, DayOfWeek.Saturday);
		}

		public override IEnumerable<DateRange> CalculateDateRange(IEnumerable<DateTime> dates)
		{
			return CalculateDateRange(dates, DayOfWeek.Sunday, DayOfWeek.Saturday);
		}

		public void ResetToDefault()
		{
			foreach (var calendarMonth in Months)
				((AdCalendarOutputData)calendarMonth.OutputData).ResetToDefault();
			foreach (var calendarDay in Days)
				calendarDay.ClearData();
		}
	}

	public class AdCalendarMonth : CalendarMonthSundayBased
	{
		public AdCalendarMonth(Calendar.Calendar parent)
			: base(parent)
		{
			OutputData = new AdCalendarOutputData(this);
		}
	}

	public class AdCalendarDay : CalendarDaySundayBased
	{
		protected Schedule AdSchedule
		{
			get { return Parent.Schedule as Schedule; }
		}

		protected AdCalendarOutputData MonthOutputData
		{
			get { return Parent.Months.Where(m => m.DaysRangeBegin <= Date && m.DaysRangeEnd >= Date).Select(m => m.OutputData).OfType<AdCalendarOutputData>().FirstOrDefault(); }
		}

		public AdCalendarDay(Calendar.Calendar parent) : base(parent) { }

		public override string ImportedData
		{
			get
			{
				var customNoteConstructor = new StringBuilder();
				foreach (var publication in AdSchedule.PrintProducts)
					foreach (var insert in publication.Inserts.Where(x => x.Date.HasValue && x.Date.Value.Date == Date.Date))
					{
						var properties = new List<string>();
						if (!String.IsNullOrEmpty(insert.Publication))
							properties.Add(MonthOutputData.ShowAbbreviationOnly ? insert.PublicationAbbreviation : insert.Publication);
						if (!String.IsNullOrEmpty(insert.FullSection) && MonthOutputData.ShowSection)
							properties.Add(insert.FullSection);
						if (!String.IsNullOrEmpty(insert.DimensionsShort) && MonthOutputData.ShowAdSize)
							properties.Add(insert.DimensionsShort);
						if (!String.IsNullOrEmpty(insert.PageSize) && MonthOutputData.ShowPageSize)
							properties.Add(insert.PageSize);
						if (!String.IsNullOrEmpty(insert.PercentOfPage) && MonthOutputData.ShowPercentOfPage)
							properties.Add(insert.PercentOfPage);
						if (!String.IsNullOrEmpty(insert.PublicationColor) && MonthOutputData.ShowColor)
							properties.Add(insert.PublicationColor);
						if (MonthOutputData.ShowCost)
							properties.Add(insert.FinalRate.ToString("$#,##0"));
						customNoteConstructor.AppendLine(string.Join(", ", properties.ToArray()));
					}
				return customNoteConstructor.ToString();
			}
		}
	}

	public class AdCalendarOutputData : CalendarOutputData
	{
		public AdCalendarOutputData(CalendarMonth parent)
			: base(parent)
		{
			ApplyForAllInfo = true;
			ApplyForAllDetails = true;

			ResetToDefault();
		}

		#region Info
		public bool ShowSection { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowColor { get; set; }
		public bool ShowAbbreviationOnly { get; set; }
		public bool ShowAdSize { get; set; }
		public bool ShowPageSize { get; set; }
		public bool ShowPercentOfPage { get; set; }
		public bool ApplyForAllInfo { get; set; }
		#endregion

		#region Details
		public bool ShowPrintTotalCost { get; set; }
		public bool ShowDigitalTotalCost { get; set; }
		public double? PrintTotalCost { get; set; }
		public double? DigitalTotalCost { get; set; }

		private int? _activeDays;
		private int? _printAdsNumber;
		public bool ShowActiveDays { get; set; }
		public bool ShowPrintAdsNumber { get; set; }

		public bool ApplyForAllDetails { get; set; }
		#endregion

		#region Calculated Options
		public int PrintAdsNumber
		{
			get { return _printAdsNumber.HasValue ? _printAdsNumber.Value : 0; }
			set { _printAdsNumber = value; }
		}

		public int CalculatedActiveDays
		{
			get { return Parent.Days.Count(x => x.ContainsData || x.HasNotes); }
		}

		public int ActiveDays
		{
			get { return !_activeDays.HasValue ? CalculatedActiveDays : _activeDays.Value; }
			set
			{
				if (CalculatedActiveDays != value)
					_activeDays = value;
				else
					_activeDays = null;
			}
		}

		public override string TagA
		{
			get
			{
				var result = string.Empty;
				var tagValues = GetTotalTags();
				if (tagValues.Length > 0)
					result = tagValues[0];
				return result;
			}
		}

		public override string TagB
		{
			get
			{
				var result = string.Empty;
				var tagValues = GetTotalTags();
				if (tagValues.Length > 1)
					result = tagValues[1];
				return result;
			}
		}

		public override string TagC
		{
			get
			{
				var result = string.Empty;
				var tagValues = GetTotalTags();
				if (tagValues.Length > 2)
					result = tagValues[2];
				return result;
			}
		}

		public override string TagD
		{
			get
			{
				var result = string.Empty;
				var tagValues = GetTotalTags();
				if (tagValues.Length > 3)
					result = tagValues[3];
				return result;
			}
		}
		#endregion

		public override string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(base.Serialize());
			TypeDescriptor.GetConverter(typeof(Bitmap));

			#region Info
			result.AppendLine(@"<ShowSection>" + ShowSection + @"</ShowSection>");
			result.AppendLine(@"<ShowCost>" + ShowCost + @"</ShowCost>");
			result.AppendLine(@"<ShowColor>" + ShowColor + @"</ShowColor>");
			result.AppendLine(@"<ShowAbbreviationOnly>" + ShowAbbreviationOnly + @"</ShowAbbreviationOnly>");
			result.AppendLine(@"<ShowAdSize>" + ShowAdSize + @"</ShowAdSize>");
			result.AppendLine(@"<ShowPageSize>" + ShowPageSize + @"</ShowPageSize>");
			result.AppendLine(@"<ShowPercentOfPage>" + ShowPercentOfPage + @"</ShowPercentOfPage>");
			result.AppendLine(@"<ApplyForAllInfo>" + ApplyForAllInfo + @"</ApplyForAllInfo>");
			#endregion

			#region Details
			if (PrintTotalCost.HasValue)
				result.AppendLine(@"<PrintTotalCost>" + PrintTotalCost.Value + @"</PrintTotalCost>");
			result.AppendLine(@"<ShowPrintTotalCost>" + ShowPrintTotalCost + @"</ShowPrintTotalCost>");
			result.AppendLine(@"<ShowDigitalTotalCost>" + ShowDigitalTotalCost + @"</ShowDigitalTotalCost>");
			if (DigitalTotalCost.HasValue)
				result.AppendLine(@"<DigitalTotalCost>" + DigitalTotalCost.Value + @"</DigitalTotalCost>");
			if (_activeDays.HasValue)
				result.AppendLine(@"<ActiveDays>" + _activeDays.Value + @"</ActiveDays>");
			if (_printAdsNumber.HasValue)
				result.AppendLine(@"<PrintAdsNumber>" + _printAdsNumber.Value + @"</PrintAdsNumber>");
			result.AppendLine(@"<ShowActiveDays>" + ShowActiveDays + @"</ShowActiveDays>");
			result.AppendLine(@"<ShowPrintAdsNumber>" + ShowPrintAdsNumber + @"</ShowPrintAdsNumber>");
			result.AppendLine(@"<ApplyForAllDetails>" + ApplyForAllDetails + @"</ApplyForAllDetails>");
			#endregion

			return result.ToString();
		}

		public override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);

			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
				double tempDouble;
				int tempInt;
				switch (childNode.Name)
				{
					#region Info
					case "ShowSection":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSection = tempBool;
						break;
					case "ShowCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCost = tempBool;
						break;
					case "ShowColor":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowColor = tempBool;
						break;
					case "ShowAbbreviationOnly":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAbbreviationOnly = tempBool;
						break;
					case "ShowAdSize":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAdSize = tempBool;
						break;
					case "ShowPageSize":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPageSize = tempBool;
						break;
					case "ShowPercentOfPage":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPercentOfPage = tempBool;
						break;
					case "ApplyForAllInfo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ApplyForAllInfo = tempBool;
						break;
					#endregion

					#region Details
					case "PrintTotalCost":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							PrintTotalCost = tempDouble;
						break;
					case "ShowPrintTotalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPrintTotalCost = tempBool;
						break;
					case "ShowDigitalTotalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDigitalTotalCost = tempBool;
						break;
					case "DigitalTotalCost":
						if (double.TryParse(childNode.InnerText, out tempDouble))
							DigitalTotalCost = tempDouble;
						break;
					case "ActiveDays":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_activeDays = tempInt;
						break;
					case "PrintAdsNumber":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_printAdsNumber = tempInt;
						break;
					case "ShowActiveDays":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowActiveDays = tempBool;
						break;
					case "ShowPrintAdsNumber":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPrintAdsNumber = tempBool;
						break;
					case "ApplyForAllDetails":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ApplyForAllDetails = tempBool;
						break;
					#endregion
				}
			}
		}

		private string[] GetTotalTags()
		{
			var tagValues = new List<string>();
			if (ShowPrintTotalCost)
				tagValues.Add("Newspaper Investment: " + ((PrintTotalCost.HasValue ? PrintTotalCost.Value.ToString("$#,###.00") : string.Empty)));
			if (ShowDigitalTotalCost && DigitalTotalCost.HasValue)
				tagValues.Add("Digital Investment: " + DigitalTotalCost.Value.ToString("$#,###.00"));
			if (ShowActiveDays)
				tagValues.Add("# of Active Days: " + ActiveDays.ToString("#,##0"));
			if (ShowPrintAdsNumber)
				tagValues.Add("# of Newspaper Ads: " + PrintAdsNumber.ToString("#,##0"));
			return tagValues.ToArray();
		}

		public void ResetToDefault()
		{
			ShowSection = ListManager.Instance.DefaultCalendarViewSettings.ShowSection;
			ShowCost = ListManager.Instance.DefaultCalendarViewSettings.ShowCost;
			ShowColor = ListManager.Instance.DefaultCalendarViewSettings.ShowColor;
			ShowBigDate = ListManager.Instance.DefaultCalendarViewSettings.ShowBigDate;
			ShowAdSize = ListManager.Instance.DefaultCalendarViewSettings.ShowAdSize;
			ShowPageSize = ListManager.Instance.DefaultCalendarViewSettings.ShowPageSize;
			ShowPercentOfPage = ListManager.Instance.DefaultCalendarViewSettings.ShowPercentOfPage;
			ShowAbbreviationOnly = ListManager.Instance.DefaultCalendarViewSettings.ShowAbbreviationOnly;
			ShowCustomComment = ListManager.Instance.DefaultCalendarViewSettings.ShowComments;

			ShowPrintTotalCost = ListManager.Instance.DefaultCalendarViewSettings.ShowTotalCost;
			ShowDigitalTotalCost = ListManager.Instance.DefaultCalendarViewSettings.ShowTotalCost;
			ShowActiveDays = ListManager.Instance.DefaultCalendarViewSettings.ShowActiveDays;
			ShowPrintAdsNumber = ListManager.Instance.DefaultCalendarViewSettings.ShowTotalAds;

			ShowLogo = ListManager.Instance.DefaultCalendarViewSettings.ShowLogo;

			SlideColor = ListManager.Instance.DefaultCalendarViewSettings.SlideColor;
		}
	}

	public class AdFullSummarySettings : CustomSummarySettings
	{
		private readonly Schedule _parent;
		public bool IsDefaultSate { get; set; }

		public AdFullSummarySettings(Schedule parent)
		{
			_parent = parent;
			IsDefaultSate = true;
		}

		public override string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(base.Serialize());
			result.AppendLine(@"<IsDefaultSate>" + IsDefaultSate + @"</IsDefaultSate>");
			return result.ToString();
		}

		public override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "IsDefaultSate":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								IsDefaultSate = temp;
						}
						break;
				}
			}
		}

		public void UpdateItems()
		{
			if (!IsDefaultSate) return;
			if (Items.Count != 2) return;
			{
				var adSummaryItem = Items[0];
				adSummaryItem.ShowValue = true;
				adSummaryItem.Value = "Publishing & Community Papers";
				adSummaryItem.Description = String.Join(", ", _parent.PrintProducts.Select(printProduct =>
					String.Format("{0} - {1} - {2}x",
					printProduct.Name,
					printProduct.SizeOptions.PageSizeAndGroup,
					printProduct.Inserts.Count)));
				adSummaryItem.ShowDescription = true;
				adSummaryItem.ShowMonthly = false;
				adSummaryItem.Monthly = null;
				adSummaryItem.ShowTotal = false;
				adSummaryItem.Total = null;
			}
			{
				var digitalSummaryItem = Items[1];
				digitalSummaryItem.ShowValue = true;
				digitalSummaryItem.Value = "Digital Marketing Strategy";
				digitalSummaryItem.Description = String.Join(", ", _parent.DigitalProducts.Select(dp =>
					String.Format("({0}){1} - {2}",
					dp.Category,
					!String.IsNullOrEmpty(dp.SubCategory) ? (String.Format(" {0}", dp.SubCategory)) : String.Empty,
					dp.Name)));
				digitalSummaryItem.ShowDescription = true;
				digitalSummaryItem.ShowMonthly = false;
				digitalSummaryItem.Monthly = null;
				digitalSummaryItem.ShowTotal = false;
				digitalSummaryItem.Total = null;
			}
		}
	}
}
