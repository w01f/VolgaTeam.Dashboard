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
using NewBizWiz.Core.Common;
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

		public static ShortSchedule[] GetShortScheduleList()
		{
			var saveFolder = new DirectoryInfo(SettingsManager.Instance.SaveFolder);
			if (saveFolder.Exists)
				return GetShortScheduleList(saveFolder);
			return null;
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
		private readonly List<DateTime> _scheduleMonths = new List<DateTime>();

		public Schedule(string fileName)
		{
			ClientType = string.Empty;
			AccountNumber = string.Empty;
			Status = ListManager.Instance.Statuses.FirstOrDefault();
			PrintProducts = new List<PrintProduct>();
			DigitalProducts = new List<DigitalProduct>();
			Summary = new SummarySettings();
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


			if (ListManager.Instance.DefaultHomeViewSettings.EnableAccountNumber)
				AccountNumber = string.Empty;
		}

		private FileInfo _scheduleFile { get; set; }
		public string BusinessName { get; set; }
		public string DecisionMaker { get; set; }
		public string ClientType { get; set; }
		public string AccountNumber { get; set; }
		public string Status { get; set; }
		public DateTime PresentationDate { get; set; }
		public DateTime FlightDateStart { get; set; }
		public DateTime FlightDateEnd { get; set; }
		public List<PrintProduct> PrintProducts { get; set; }
		public List<DigitalProduct> DigitalProducts { get; set; }

		public ScheduleBuilderViewSettings ViewSettings { get; set; }

		public SummarySettings Summary { get; set; }

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
				return FlightDateEnd;
			}
		}

		public string FlightDates
		{
			get
			{
				if (FlightDateStart != DateTime.MinValue && FlightDateEnd != DateTime.MinValue)
					return FlightDateStart.ToString("MM/dd/yy") + " - " + FlightDateEnd.ToString("MM/dd/yy");
				return string.Empty;
			}
		}

		public DateTime[] ScheduleMonths
		{
			get { return _scheduleMonths.ToArray(); }
		}

		private void Load()
		{
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
				if (node != null)
					if (DateTime.TryParse(node.InnerText, out tempDateTime))
						PresentationDate = tempDateTime;

				node = document.SelectSingleNode(@"/Schedule/FlightDateStart");
				if (node != null)
					if (DateTime.TryParse(node.InnerText, out tempDateTime))
						FlightDateStart = tempDateTime;

				node = document.SelectSingleNode(@"/Schedule/FlightDateEnd");
				if (node != null)
					if (DateTime.TryParse(node.InnerText, out tempDateTime))
						FlightDateEnd = tempDateTime;


				node = document.SelectSingleNode(@"/Schedule/ViewSettings");
				if (node != null)
				{
					ViewSettings.Deserialize(node);
				}

				node = document.SelectSingleNode(@"/Schedule/Summary");
				if (node != null)
				{
					Summary.Deserialize(node);
				}
				if (Summary.Items.Count == 0)
				{
					Summary.Items.Add(new SummaryItem { Order = 0 });
					Summary.Items.Add(new SummaryItem { Order = 1 });
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
			}

			UpdateScheduleMonths();
		}

		public void Save()
		{
			UpdateScheduleMonths();

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
			xml.AppendLine(@"<PresentationDate>" + PresentationDate.ToString() + @"</PresentationDate>");
			xml.AppendLine(@"<FlightDateStart>" + FlightDateStart.ToString() + @"</FlightDateStart>");
			xml.AppendLine(@"<FlightDateEnd>" + FlightDateEnd.ToString() + @"</FlightDateEnd>");

			xml.AppendLine(@"<ViewSettings>" + ViewSettings.Serialize() + @"</ViewSettings>");
			xml.AppendLine(@"<Summary>" + Summary.Serialize() + @"</Summary>");

			xml.AppendLine(@"<Publications>");
			foreach (PrintProduct publication in PrintProducts)
				xml.AppendLine(publication.Serialize());
			xml.AppendLine(@"</Publications>");
			xml.AppendLine(@"<DigitalProducts>");
			foreach (var digitalProduct in DigitalProducts)
				xml.AppendLine(digitalProduct.Serialize());
			xml.AppendLine(@"</DigitalProducts>");

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

		public void RebuildDigitalIndexes()
		{
			for (int i = 0; i < DigitalProducts.Count; i++)
				DigitalProducts[i].Index = i + 1;
		}

		private void UpdateScheduleMonths()
		{
			DateTime startDate = FlightDateStart;
			var endDate = new DateTime(FlightDateEnd.Month < 12 ? FlightDateEnd.Year : (FlightDateEnd.Year + 1), (FlightDateEnd.Month < 12 ? FlightDateEnd.Month + 1 : 1), 1);
			_scheduleMonths.Clear();
			while (startDate < endDate)
			{
				_scheduleMonths.Add(new DateTime(startDate.Year, startDate.Month, 1));
				startDate = startDate.AddMonths(1);
			}
		}

		public string GetDigitalInfo(RequestDigitalInfoEventArgs args)
		{
			var result = new StringBuilder();
			if (args.ShowWebsites)
			{
				var compiledWebsites = String.Join(", ", DigitalProducts.SelectMany(p => p.AllWebsites).Distinct());
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
					result.AppendLine(String.Join(", ", temp.ToArray()));
			}
			return result.ToString();
		}
	}

	public class PrintProduct
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
		public double ColorInchRate { get; set; }
		public string Note { get; set; }

		public SizeOptions SizeOptions { get; set; }

		public PublicationViewSettings ViewSettings { get; set; }

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
					ColorInchRate = 0;
					foreach (Insert insert in Inserts)
					{
						insert.ColorPricing = 0;
						insert.ColorPricingPercent = 0;
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
						foreach (Insert insert in Inserts)
							insert.ColorPricingPercent = 0;
						ColorInchRate = 0;
						break;
					case ColorPricingType.PercentOfAdRate:
						foreach (Insert insert in Inserts)
							insert.ColorPricing = 0;
						ColorInchRate = 0;
						break;
					case ColorPricingType.ColorIncluded:
						foreach (Insert insert in Inserts)
						{
							insert.ColorPricing = 0;
							insert.ColorPricingPercent = 0;
						}
						ColorInchRate = 0;
						break;
					case ColorPricingType.CostPerInch:
						foreach (Insert insert in Inserts)
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
			get { return Inserts.Count(x => x.DateObject != null); }
		}

		public double AvgADRate
		{
			get { return TotalInserts != 0 ? Inserts.Where(x => x.DateObject != null).Sum(x => x.ADRate) / TotalInserts : 0; }
		}

		public double AvgPCIRate
		{
			get { return TotalInserts != 0 ? Inserts.Where(x => x.DateObject != null).Sum(x => (x.PCIRate.HasValue ? x.PCIRate.Value : 0)) / TotalInserts : 0; }
		}

		public double TotalDiscountRate
		{
			get { return Inserts.Where(x => x.DateObject != null).Sum(x => x.DiscountRate); }
		}

		public double TotalColorPricingCalculated
		{
			get { return Inserts.Where(x => x.DateObject != null).Sum(x => x.ColorPricingCalculated); }
		}

		public double AvgFinalRate
		{
			get { return TotalInserts != 0 ? Inserts.Where(x => x.DateObject != null).Sum(x => x.FinalRate) / TotalInserts : 0; }
		}

		public double TotalFinalRate
		{
			get { return Inserts.Where(x => x.DateObject != null).Sum(x => x.FinalRate); }
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
			get { return String.Join(", ", Inserts.Where(i => i.DateObject != null).Select(i => i.Date.ToString("MM/dd/yy"))); }
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
			xml.Append("UniqueID = \"" + UniqueID.ToString() + "\" ");
			xml.Append("Abbreviation = \"" + Abbreviation.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("DailyDelivery = \"" + (DailyDelivery ?? 0) + "\" ");
			xml.Append("DailyReadership = \"" + (DailyReadership ?? 0) + "\" ");
			xml.Append("SundayDelivery = \"" + (SundayDelivery ?? 0) + "\" ");
			xml.Append("SundayReadership = \"" + (SundayReadership ?? 0) + "\" ");
			xml.Append("Index = \"" + Index + "\" ");
			xml.Append("AdPricingStrategy = \"" + (int)AdPricingStrategy + "\" ");
			xml.Append("ColorOption = \"" + (int)ColorOption + "\" ");
			xml.Append("ColorPricing = \"" + (int)ColorPricing + "\" ");
			xml.Append("ColorInchRate = \"" + ColorInchRate.ToString() + "\" ");
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
					case "ColorInchRate":
						tempDouble = 0;
						double.TryParse(attribute.Value, out tempDouble);
						ColorInchRate = tempDouble;
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
			Inserts.Sort((x, y) => x.Date.CompareTo(y.Date) == 0 ? x.Index.CompareTo(y.Index) : x.Date.CompareTo(y.Date));
		}

		public void RebuildInserts()
		{
			var temp = new List<Insert>();
			temp.AddRange(Inserts);
			temp.Sort((x, y) => x.Date.CompareTo(y.Date) == 0 ? x.Index.CompareTo(y.Index) : x.Date.CompareTo(y.Date));

			DateTime startDate = Parent.FlightDateStart;
			DateTime endDate = startDate.AddDays(7);
			do
			{
				IEnumerable<Insert> insertsPerWeek =
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
				}
				if (comment)
				{
					insert.CustomComment = originalInsert.CustomComment;
					foreach (Common.NameCodePair originalComment in originalInsert.Comments)
					{
						var newComment = new Common.NameCodePair();
						newComment.Name = originalComment.Name;
						newComment.Code = originalComment.Code;
						insert.Comments.Add(newComment);
					}
				}
				if (section)
				{
					insert.CustomSection = originalInsert.CustomSection;
					foreach (Common.NameCodePair originalSection in originalInsert.Sections)
					{
						var newSection = new Common.NameCodePair();
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
			foreach (Insert insert in Inserts)
				insert.PCIRate = value;
		}

		public void CopyAdRate(double value)
		{
			foreach (Insert insert in Inserts)
				insert.ADRate = value;
		}

		public void CopyDiscounts(double value)
		{
			foreach (Insert insert in Inserts)
				insert.Discounts = value;
		}

		public void CopyColorRate(double value)
		{
			foreach (Insert insert in Inserts)
				insert.ColorPricing = value;
		}

		public void CopyColorRatePercent(double value)
		{
			foreach (Insert insert in Inserts)
				insert.ColorPricingPercent = value;
		}

		public void RefreshAvailableDays()
		{
			AvailableDays.Clear();
			DateTime dayInSchedule = Parent.FlightDateStart;
			while (dayInSchedule <= Parent.FlightDateEnd)
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
	}

	public class Insert
	{
		private double _adRate;
		private double _pciRate;

		public Insert(PrintProduct parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Date = DateTime.MinValue;
			Index = 1;
			CustomSection = string.Empty;
			CustomComment = string.Empty;
			Deadline = string.Empty;
			Comments = new List<Common.NameCodePair>();
			Sections = new List<Common.NameCodePair>();
		}

		public PrintProduct Parent { get; private set; }
		public Guid UniqueID { get; set; }
		public int Index { get; set; }
		public DateTime Date { get; set; }
		public double Discounts { get; set; }
		public double ColorPricing { get; set; }
		public double ColorPricingPercent { get; set; }
		public string CustomComment { get; set; }
		public string CustomSection { get; set; }
		public string Deadline { get; set; }
		public string Mechanicals { get; set; }
		public List<Common.NameCodePair> Comments { get; private set; }
		public List<Common.NameCodePair> Sections { get; private set; }

		public Guid ParentID
		{
			get { return Parent.UniqueID; }
		}

		public string ID
		{
			get { return (Parent.Inserts.IndexOf(this) + 1).ToString("000") + Parent.InsertSuffix; }
		}

		public object DateObject
		{
			get
			{
				if (Date.Equals(DateTime.MaxValue) || Date.Equals(DateTime.MinValue))
					return null;
				else
					return Date;
			}
			set
			{
				if (value == null)
					Date = DateTime.MaxValue;
				else
					Date = (DateTime)value;
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
							return Parent.SizeOptions.Square.Value * Parent.ColorInchRate;
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
				else if (Parent.ColorPricing == ColorPricingType.ColorIncluded)
					return "Included";
				else
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
			get { return ADRate * (Discounts / 100.00); }
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
				foreach (Common.NameCodePair section in Sections)
				{
					if (!string.IsNullOrEmpty(section.Code) && (Sections.Count + (!string.IsNullOrEmpty(CustomSection) ? 1 : 0)) >= ListManager.Instance.SelectedSectionsBorderValue)
						sections.Add(section.Code);
					else if (!string.IsNullOrEmpty(section.Name))
						sections.Add(section.Name);
				}
				if (sections.Count > 0)
					return string.Join(", ", sections.ToArray());
				else
					return string.Empty;
			}
		}

		#region Output Stuff
		public DateTime EndDate
		{
			get { return Date.AddHours(1); }
		}

		public string PageSize
		{
			get { return Parent.SizeOptions.PageSize; }
		}

		public string PageSizeOutput
		{
			get { return !string.IsNullOrEmpty(Parent.SizeOptions.PageSize) ? Parent.SizeOptions.PageSize : "N/A"; }
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
				if (Deadline.ToLower().Contains("day"))
				{
					var re = new Regex(@"\d+");
					Match m = re.Match(Deadline);
					if (m.Success)
					{
						int daysNumber = 0;
						if (int.TryParse(m.Value, out daysNumber))
						{
							DateTime deadline = Date.AddDays(0 - daysNumber);
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
				if (Date.DayOfWeek == DayOfWeek.Sunday)
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
				if (Date.DayOfWeek == DayOfWeek.Sunday)
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
			xml.Append("Date = \"" + Date + "\" ");
			xml.Append("PCIRate = \"" + (PCIRate.HasValue ? PCIRate.Value : 0) + "\" ");
			xml.Append("ADRate = \"" + ADRate + "\" ");
			xml.Append("Discounts = \"" + Discounts + "\" ");
			xml.Append("ColorPricing = \"" + ColorPricing + "\" ");
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
			DateTime tempDateTime = DateTime.MinValue;
			int tempInt = 0;
			double tempDouble;

			foreach (XmlAttribute attribute in node.Attributes)
				switch (attribute.Name)
				{
					case "Index":
						if (int.TryParse(attribute.Value, out tempInt))
							Index = tempInt;
						break;
					case "Date":
						if (DateTime.TryParse(attribute.Value, out tempDateTime))
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
							var comment = new Common.NameCodePair();
							comment.Deserialize(commentNode);
							Comments.Add(comment);
						}
						break;
					case "Sections":
						Sections.Clear();
						foreach (XmlNode sectionNode in childNode.ChildNodes)
						{
							var section = new Common.NameCodePair();
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
		public string PageSize { get; set; }
		public bool EnablePageSize { get; set; }
		public string RateCard { get; set; }
		public string PercentOfPage { get; set; }

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
				else
					return null;
			}
		}

		public string Dimensions
		{
			get { return Square.HasValue ? (string.Format("{0}{1} x {2}{3}", new object[] { Width.ToString("#,##0.00"), ShortWidthMeasure, Height.ToString("#,###.00#"), ShortHeightMeasure })) : null; }
		}

		public string DimensionsShort
		{
			get { return Square.HasValue ? (string.Format("{0}x{1}", new object[] { Width.ToString("#,##0.00"), Height.ToString("#,###.00#") })) : "N/A"; }
		}

		public string PageSizeOutput
		{
			get { return !string.IsNullOrEmpty(PageSize) ? PageSize : "N/A"; }
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
			xml.Append("Width = \"" + Width.ToString() + "\" ");
			xml.Append("Height = \"" + Height.ToString() + "\" ");
			xml.Append("WidthMeasure = \"" + WidthMeasure.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("HeightMeasure = \"" + HeightMeasure.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("EnableSquare = \"" + EnableSquare.ToString() + "\" ");
			xml.Append("PageSize = \"" + (PageSize != null ? PageSize.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + "\" ");
			xml.Append("EnablePageSize = \"" + EnablePageSize.ToString() + "\" ");
			xml.Append("RateCard = \"" + (RateCard != null ? RateCard.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + "\" ");
			xml.Append("PercentOfPage = \"" + (PercentOfPage != null ? PercentOfPage.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + "\" ");
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

	public class SummarySettings
	{
		public SummarySettings()
		{
			ShowAdvertiser = true;
			ShowDecisionMaker = true;
			ShowPresentationDate = true;
			ShowFlightDates = true;
			ShowMonthly = true;
			ShowTotal = true;
			ShowSignature = true;
			EnableTotalsEdit = false;

			SlideHeader = string.Empty;
			MonthlyValue = 0;
			TotalValue = 0;

			Items = new List<SummaryItem>();
		}

		public bool ShowAdvertiser { get; set; }
		public bool ShowDecisionMaker { get; set; }
		public bool ShowPresentationDate { get; set; }
		public bool ShowFlightDates { get; set; }
		public bool ShowMonthly { get; set; }
		public bool ShowTotal { get; set; }
		public bool ShowSignature { get; set; }
		public bool EnableTotalsEdit { get; set; }

		public string SlideHeader { get; set; }
		public decimal MonthlyValue { get; set; }
		public decimal TotalValue { get; set; }

		public List<SummaryItem> Items { get; set; }

		public decimal TotalMonthly
		{
			get
			{
				var items = Items.Where(it => !String.IsNullOrEmpty(it.Value) && it.ShowMonthly);
				if (items.Any())
					return items.Sum(it => it.Monthly);
				return 0;
			}
		}

		public decimal TotalTotal
		{
			get
			{
				var items = Items.Where(it => !String.IsNullOrEmpty(it.Value) && it.ShowTotal);
				if (items.Any())
					return items.Sum(it => it.Total);
				return 0;
			}
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<ShowAdvertiser>" + ShowAdvertiser + @"</ShowAdvertiser>");
			result.AppendLine(@"<ShowDecisionMaker>" + ShowDecisionMaker + @"</ShowDecisionMaker>");
			result.AppendLine(@"<ShowPresentationDate>" + ShowPresentationDate + @"</ShowPresentationDate>");
			result.AppendLine(@"<ShowFlightDates>" + ShowFlightDates + @"</ShowFlightDates>");
			result.AppendLine(@"<ShowMonthly>" + ShowMonthly + @"</ShowMonthly>");
			result.AppendLine(@"<ShowTotal>" + ShowTotal + @"</ShowTotal>");
			result.AppendLine(@"<ShowSignature>" + ShowSignature + @"</ShowSignature>");
			result.AppendLine(@"<EnableTotalsEdit>" + EnableTotalsEdit + @"</EnableTotalsEdit>");

			result.AppendLine(@"<SlideHeader>" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
			result.AppendLine(@"<MonthlyValue>" + MonthlyValue + @"</MonthlyValue>");
			result.AppendLine(@"<TotalValue>" + TotalValue + @"</TotalValue>");

			result.AppendLine(@"<Items>");
			foreach (var item in Items.Where(it => it.Commited))
				result.AppendLine(@"<Item>" + item.Serialize() + @"</Item>");
			result.AppendLine(@"</Items>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool;
			decimal tempDecimal;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "ShowAdvertiser":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAdvertiser = tempBool;
						break;
					case "ShowDecisionMaker":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDecisionMaker = tempBool;
						break;
					case "ShowPresentationDate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPresentationDate = tempBool;
						break;
					case "ShowFlightDates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowFlightDates = tempBool;
						break;
					case "ShowMonthly":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowMonthly = tempBool;
						break;
					case "ShowTotal":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotal = tempBool;
						break;
					case "ShowSignature":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSignature = tempBool;
						break;
					case "EnableTotalsEdit":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							EnableTotalsEdit = tempBool;
						break;
					case "SlideHeader":
						SlideHeader = childNode.InnerText;
						break;
					case "MonthlyValue":
						if (decimal.TryParse(childNode.InnerText, out tempDecimal))
							MonthlyValue = tempDecimal;
						break;
					case "TotalValue":
						if (decimal.TryParse(childNode.InnerText, out tempDecimal))
							TotalValue = tempDecimal;
						break;
					case "Items":
						Items.Clear();
						foreach (XmlNode itemNode in childNode.ChildNodes)
						{
							var item = new SummaryItem();
							item.Deserialize(itemNode);
							Items.Add(item);
						}
						Items.Sort((x, y) => x.Order.CompareTo(y.Order));
						break;
				}
			}
		}

		public void ReorderItems(int shifIndex = -1)
		{
			var i = 0;
			foreach (var item in Items.OrderBy(it => it.Order))
			{
				if (i == shifIndex)
					i++;
				item.Order = i;
				i++;
			}
		}
	}

	public class SummaryItem
	{
		public SummaryItem()
		{
			ShowValue = true;
			ShowDescription = true;
			ShowMonthly = true;
			ShowTotal = true;

			Identifier = Guid.NewGuid().ToString();
			Order = 0;
		}

		public bool ShowValue { get; set; }
		public bool ShowDescription { get; set; }
		public bool ShowMonthly { get; set; }
		public bool ShowTotal { get; set; }

		public string Identifier { get; set; }
		public int Order { get; set; }
		public string Value { get; set; }
		public string Description { get; set; }
		public decimal Monthly { get; set; }
		public decimal Total { get; set; }

		public bool Commited { get; set; }

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<ShowDescription>" + ShowDescription + @"</ShowDescription>");
			result.AppendLine(@"<ShowMonthly>" + ShowMonthly + @"</ShowMonthly>");
			result.AppendLine(@"<ShowTotal>" + ShowTotal + @"</ShowTotal>");
			result.AppendLine(@"<ShowValue>" + ShowValue + @"</ShowValue>");

			result.AppendLine(@"<Identifier>" + Identifier + @"</Identifier>");
			result.AppendLine(@"<Order>" + Order + @"</Order>");
			if (!String.IsNullOrEmpty(Value))
				result.AppendLine(@"<Value>" + Value.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Value>");
			if (!String.IsNullOrEmpty(Description))
				result.AppendLine(@"<Description>" + Description.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Description>");
			result.AppendLine(@"<Monthly>" + Monthly + @"</Monthly>");
			result.AppendLine(@"<Total>" + Total + @"</Total>");

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool;
			int tempInt;
			decimal tempDecimal;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "ShowDescription":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDescription = tempBool;
						break;
					case "ShowMonthly":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowMonthly = tempBool;
						break;
					case "ShowTotal":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotal = tempBool;
						break;
					case "ShowValue":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowValue = tempBool;
						break;
					case "Identifier":
						Identifier = childNode.InnerText;
						break;
					case "Order":
						if (int.TryParse(childNode.InnerText, out tempInt))
							Order = tempInt;
						break;
					case "Monthly":
						if (decimal.TryParse(childNode.InnerText, out tempDecimal))
							Monthly = tempDecimal;
						break;
					case "Total":
						if (decimal.TryParse(childNode.InnerText, out tempDecimal))
							Total = tempDecimal;
						break;
					case "Value":
						Value = childNode.InnerText;
						break;
					case "Description":
						Description = childNode.InnerText;
						break;
				}
			}
			Commited = true;
		}
	}
}
