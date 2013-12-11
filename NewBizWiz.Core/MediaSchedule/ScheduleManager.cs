using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.Core.Calendar;

namespace NewBizWiz.Core.MediaSchedule
{
	public enum SpotType
	{
		Week = 0,
		Month
	}

	public enum DemoType
	{
		Rtg = 0,
		Imp
	}

	public class ScheduleManager
	{
		private Schedule _currentSchedule;
		private readonly Func<IEnumerable<BroadcastMonthTemplate>> _getBroadcastMonthTemplates;

		public bool ScheduleLoaded { get; set; }

		public event EventHandler<ScheduleSaveEventArgs> SettingsSaved;

		public ScheduleManager(Func<IEnumerable<BroadcastMonthTemplate>> getBroadcastMonthTemplates)
		{
			_getBroadcastMonthTemplates = getBroadcastMonthTemplates;
		}

		public void OpenSchedule(string scheduleName, bool create)
		{
			string scheduleFilePath = GetScheduleFileName(scheduleName);
			if (create && File.Exists(scheduleFilePath))
				if (Utilities.Instance.ShowWarningQuestion(string.Format("An older Schedule is already saved with this same file name.\nDo you want to replace this file with a newer schedule?", scheduleName)) == DialogResult.Yes)
					File.Delete(scheduleFilePath);
			_currentSchedule = new Schedule(scheduleFilePath, _getBroadcastMonthTemplates);
			ScheduleLoaded = true;
		}

		public void OpenSchedule(string scheduleFilePath)
		{
			_currentSchedule = new Schedule(scheduleFilePath, _getBroadcastMonthTemplates);
			ScheduleLoaded = true;
		}

		public string GetScheduleFileName(string scheduleName)
		{
			return Path.Combine(MediaMetaData.Instance.SettingsManager.SaveFolder, scheduleName + ".xml");
		}

		public Schedule GetLocalSchedule()
		{
			return new Schedule(_currentSchedule.ScheduleFile.FullName, _getBroadcastMonthTemplates);
		}

		public ShortSchedule GetShortSchedule()
		{
			return _currentSchedule != null ? new ShortSchedule(_currentSchedule.ScheduleFile) : null;
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
			var saveFolder = new DirectoryInfo(MediaMetaData.Instance.SettingsManager.SaveFolder);
			if (saveFolder.Exists)
				return GetShortScheduleList(saveFolder);
			return null;
		}

		public static ShortSchedule[] GetShortScheduleList(DirectoryInfo rootFolder)
		{
			return rootFolder.GetFiles("*.xml").Select(file => new ShortSchedule(file)).ToArray();
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
			Status = MediaMetaData.Instance.ListManager.Statuses.FirstOrDefault();
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
			if (_scheduleFile.Exists)
			{
				var document = new XmlDocument();
				document.Load(_scheduleFile.FullName);

				var node = document.SelectSingleNode(@"/Schedule/BusinessName");
				if (node != null)
					BusinessName = node.InnerText;

				node = document.SelectSingleNode(@"/Schedule/Status");
				if (node != null)
					Status = node.InnerText;
			}
		}

		public void Save()
		{
			if (_scheduleFile.Exists)
			{
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
				catch
				{
				}
			}
		}
	}

	public class Schedule : ISchedule
	{
		public Schedule(string fileName, Func<IEnumerable<BroadcastMonthTemplate>> getBroadcastMonthTemplates)
		{
			_getBroadcastMonthTemplates = getBroadcastMonthTemplates;

			BusinessName = string.Empty;
			DecisionMaker = string.Empty;
			ClientType = string.Empty;
			AccountNumber = string.Empty;
			Status = MediaMetaData.Instance.ListManager.Statuses.FirstOrDefault();
			UseDemo = false;
			ImportDemo = false;
			DemoType = DemoType.Imp;
			WeeklySchedule = new WeeklySection(this);
			MonthlySchedule = new MonthlySection(this);

			Dayparts = new List<Daypart>();
			Stations = new List<Station>();

			ViewSettings = new OnlineSchedule.ScheduleBuilderViewSettings();

			DigitalProducts = new List<DigitalProduct>();

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
			LoadCalendars();

			Dayparts.AddRange(MediaMetaData.Instance.ListManager.Dayparts.Where(x => !Dayparts.Select(y => y.Name).Contains(x.Name)));
			Stations.AddRange(MediaMetaData.Instance.ListManager.Stations.Where(x => !Stations.Select(y => y.Name).Contains(x.Name)));
		}

		private FileInfo _scheduleFile { get; set; }
		private readonly Func<IEnumerable<BroadcastMonthTemplate>> _getBroadcastMonthTemplates;
		public bool IsNameNotAssigned { get; set; }
		public string BusinessName { get; set; }
		public string DecisionMaker { get; set; }
		public string ClientType { get; set; }
		public string AccountNumber { get; set; }
		public string Status { get; set; }
		public DateTime? PresentationDate { get; set; }
		public DateTime? FlightDateStart { get; set; }
		public DateTime? FlightDateEnd { get; set; }
		public bool UseDemo { get; set; }
		public bool ImportDemo { get; set; }
		public string Demo { get; set; }
		public string Source { get; set; }
		public DemoType DemoType { get; set; }

		public WeeklySection WeeklySchedule { get; set; }
		public MonthlySection MonthlySchedule { get; set; }

		public List<Daypart> Dayparts { get; private set; }
		public List<Station> Stations { get; private set; }

		public OnlineSchedule.ScheduleBuilderViewSettings ViewSettings { get; set; }
		public IScheduleViewSettings CommonViewSettings
		{
			get { return ViewSettings; }
		}

		public List<DigitalProduct> DigitalProducts { get; private set; }

		public BroadcastCalendar BroadcastCalendar { get; set; }

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

		public string ThemeName { get; set; }

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

			node = document.SelectSingleNode(@"/Schedule/UseDemo");
			bool tempBool;
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					UseDemo = tempBool;

			node = document.SelectSingleNode(@"/Schedule/ImportDemo");
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					ImportDemo = tempBool;

			node = document.SelectSingleNode(@"/Schedule/DemoType");
			int tempInt;
			if (node != null)
				if (int.TryParse(node.InnerText, out tempInt))
					DemoType = (DemoType)tempInt;

			node = document.SelectSingleNode(@"/Schedule/Demo");
			if (node != null)
				Demo = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/Source");
			if (node != null)
				Source = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/WeeklySection");
			if (node != null)
				WeeklySchedule.Deserialize(node);

			node = document.SelectSingleNode(@"/Schedule/MonthlySection");
			if (node != null)
				MonthlySchedule.Deserialize(node);

			node = document.SelectSingleNode(@"/Schedule/Dayparts");
			if (node != null)
			{
				Dayparts.Clear();
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var daypart = new Daypart();
					daypart.Deserialize(childNode);
					Dayparts.Add(daypart);
				}
			}

			node = document.SelectSingleNode(@"/Schedule/Stations");
			if (node != null)
			{
				Stations.Clear();
				foreach (XmlNode childNode in node.ChildNodes)
				{
					var station = new Station();
					station.Deserialize(childNode);
					Stations.Add(station);
				}
			}

			node = document.SelectSingleNode(@"/Schedule/ViewSettings");
			if (node != null)
			{
				ViewSettings.Deserialize(node);
			}

			node = document.SelectSingleNode(@"/Schedule/DigitalProducts");
			if (node != null)
			{
				foreach (XmlNode productNode in node.ChildNodes)
				{
					var product = new DigitalProduct(this);
					product.Deserialize(productNode);
					DigitalProducts.Add(product);
				}
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
			xml.AppendLine(@"<Status>" + (Status != null ? Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
			xml.AppendLine(@"<ClientType>" + ClientType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ClientType>");
			xml.AppendLine(@"<AccountNumber>" + AccountNumber.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</AccountNumber>");
			if (PresentationDate.HasValue)
				xml.AppendLine(@"<PresentationDate>" + PresentationDate.Value + @"</PresentationDate>");
			if (FlightDateStart.HasValue)
				xml.AppendLine(@"<FlightDateStart>" + FlightDateStart.Value + @"</FlightDateStart>");
			if (FlightDateEnd.HasValue)
				xml.AppendLine(@"<FlightDateEnd>" + FlightDateEnd.Value + @"</FlightDateEnd>");
			xml.AppendLine(@"<UseDemo>" + UseDemo + @"</UseDemo>");
			xml.AppendLine(@"<ImportDemo>" + ImportDemo + @"</ImportDemo>");
			xml.AppendLine(@"<DemoType>" + (int)DemoType + @"</DemoType>");
			if (!String.IsNullOrEmpty(Demo))
				xml.AppendLine(@"<Demo>" + Demo.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Demo>");
			if (!String.IsNullOrEmpty(Source))
				xml.AppendLine(@"<Source>" + Source.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Source>");
			xml.AppendLine(@"<WeeklySection>" + WeeklySchedule.Serialize() + @"</WeeklySection>");
			xml.AppendLine(@"<MonthlySection>" + MonthlySchedule.Serialize() + @"</MonthlySection>");

			xml.AppendLine(@"<Dayparts>");
			foreach (var daypart in Dayparts)
				xml.AppendLine(daypart.Serialize());
			xml.AppendLine(@"</Dayparts>");

			xml.AppendLine(@"<Stations>");
			foreach (var station in Stations)
				xml.AppendLine(station.Serialize());
			xml.AppendLine(@"</Stations>");

			xml.AppendLine(@"<ViewSettings>" + ViewSettings.Serialize() + @"</ViewSettings>");

			xml.AppendLine(@"<DigitalProducts>");
			foreach (var digitalProduct in DigitalProducts)
				xml.AppendLine(digitalProduct.Serialize());
			xml.AppendLine(@"</DigitalProducts>");

			xml.AppendLine(@"<BroadcastCalendar>" + BroadcastCalendar.Serialize() + @"</BroadcastCalendar>");

			xml.AppendLine(@"</Schedule>");

			using (var sw = new StreamWriter(_scheduleFile.FullName, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		private void LoadCalendars()
		{
			BroadcastCalendar = new BroadcastCalendar(this, _getBroadcastMonthTemplates);
			if (!_scheduleFile.Exists) return;
			var document = new XmlDocument();
			document.Load(_scheduleFile.FullName);

			var node = document.SelectSingleNode(@"/Schedule/BroadcastCalendar");
			if (node != null)
			{
				BroadcastCalendar.Deserialize(node);
			}
			else
			{
				BroadcastCalendar.UpdateDaysCollection();
				BroadcastCalendar.UpdateMonthCollection();
				BroadcastCalendar.UpdateNotesCollection();
			}
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
			var result = new List<string>();
			if (args.ShowWebsites)
			{
				var compiledWebsites = String.Join(", ", DigitalProducts.SelectMany(p => p.AllWebsites).Distinct());
				if (!String.IsNullOrEmpty(compiledWebsites))
					result.Add(String.Format("{0}", compiledWebsites));
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
					result.Add(String.Format("[{0}]", String.Join(", ", temp.ToArray())));
			}
			return String.Join(args.Separator, result);
		}

		public void ApplyDigitalLegendForAllViews(DigitalLegend digitalLegend)
		{
			WeeklySchedule.DigitalLegend = digitalLegend.Clone();
			MonthlySchedule.DigitalLegend = digitalLegend.Clone();
		}
	}

	public class ScheduleSection
	{
		public const string ProgramDatasetName = "Schedule";
		public const string ProgramDataTableName = "Programs";
		public const string ProgramIndexDataColumnName = "Index";
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

		public ScheduleSection(Schedule parent)
		{
			Parent = parent;
			Programs = new List<Program>();
			DigitalLegend = new DigitalLegend();

			#region Options

			ShowRate = true;
			ShowRating = true;
			ShowTime = true;
			ShowDaypart = true;
			ShowDay = true;
			ShowStation = true;
			ShowLenght = false;
			ShowCPP = false;
			ShowGRP = false;
			ShowSpots = true;
			ShowEmptySpots = false;
			ShowCost = true;

			ShowTotalPeriods = true;
			ShowTotalSpots = true;
			ShowTotalGRP = true;
			ShowTotalCPP = true;
			ShowAverageRate = true;
			ShowTotalRate = true;
			ShowNetRate = false;
			ShowDiscount = false;

			#endregion
		}

		public Schedule Parent { get; private set; }
		public List<Program> Programs { get; set; }
		public SpotType SpotType { get; set; }

		public DataSet DataSource { get; private set; }

		public DigitalLegend DigitalLegend { get; set; }

		public event EventHandler<EventArgs> DataChanged;

		public string Serialize()
		{
			var result = new StringBuilder();

			#region Options

			result.AppendLine(@"<ShowRate>" + ShowRate + @"</ShowRate>");
			result.AppendLine(@"<ShowRating>" + ShowRating + @"</ShowRating>");
			result.AppendLine(@"<ShowDay>" + ShowDay + @"</ShowDay>");
			result.AppendLine(@"<ShowTime>" + ShowTime + @"</ShowTime>");
			result.AppendLine(@"<ShowDaypart>" + ShowDaypart + @"</ShowDaypart>");
			result.AppendLine(@"<ShowStation>" + ShowStation + @"</ShowStation>");
			result.AppendLine(@"<ShowLenght>" + ShowLenght + @"</ShowLenght>");
			result.AppendLine(@"<ShowCPP>" + ShowCPP + @"</ShowCPP>");
			result.AppendLine(@"<ShowGRP>" + ShowGRP + @"</ShowGRP>");
			result.AppendLine(@"<ShowSpots>" + ShowSpots + @"</ShowSpots>");
			result.AppendLine(@"<ShowEmptySpots>" + ShowEmptySpots + @"</ShowEmptySpots>");
			result.AppendLine(@"<ShowCost>" + ShowCost + @"</ShowCost>");
			result.AppendLine(@"<ShowAverageRate>" + ShowAverageRate + @"</ShowAverageRate>");
			result.AppendLine(@"<ShowDiscount>" + ShowDiscount + @"</ShowDiscount>");
			result.AppendLine(@"<ShowNetRate>" + ShowNetRate + @"</ShowNetRate>");
			result.AppendLine(@"<ShowTotalCPP>" + ShowTotalCPP + @"</ShowTotalCPP>");
			result.AppendLine(@"<ShowTotalGRP>" + ShowTotalGRP + @"</ShowTotalGRP>");
			result.AppendLine(@"<ShowTotalPeriods>" + ShowTotalPeriods + @"</ShowTotalPeriods>");
			result.AppendLine(@"<ShowTotalRate>" + ShowTotalRate + @"</ShowTotalRate>");
			result.AppendLine(@"<ShowTotalSpots>" + ShowTotalSpots + @"</ShowTotalSpots>");
			result.AppendLine(@"<DigitalLegend>" + DigitalLegend.Serialize() + @"</DigitalLegend>");

			#endregion

			result.AppendLine(@"<Programs>");
			foreach (var program in Programs)
				result.AppendLine(program.Serialize());
			result.AppendLine(@"</Programs>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool;

			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
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
					case "DigitalLegend":
						DigitalLegend.Deserialize(childNode);
						break;
					case "Programs":
						foreach (XmlNode programNode in childNode.ChildNodes)
						{
							var program = new Program(this);
							program.Deserialize(programNode);
							Programs.Add(program);
						}
						break;
				}
		}

		public void GenerateDataSource()
		{
			if (DataSource != null)
				DataSource.Dispose();

			DataSource = new DataSet(ProgramDatasetName);

			#region Generate Programs Table

			var table = new DataTable(ProgramDataTableName);

			var column = new DataColumn(ProgramIndexDataColumnName, typeof(int));
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

			if (Programs.FirstOrDefault() != null)
			{
				int spotIndex = 0;
				foreach (Spot spot in Programs.FirstOrDefault().Spots)
				{
					var columnName = ProgramSpotDataColumnNamePrefix + spotIndex;
					column = new DataColumn(columnName, typeof(int)) { Caption = spot.ColumnName };
					totalSpotsExpression.Add(string.Format("ISNULL({0},0)", columnName));
					table.Columns.Add(column);
					spotIndex++;
				}
			}

			column = new DataColumn(ProgramTotalSpotDataColumnName, typeof(int));
			if (totalSpotsExpression.Count > 0)
				column.Expression = string.Join(" + ", totalSpotsExpression.ToArray());
			table.Columns.Add(column);

			column = new DataColumn(ProgramGRPDataColumnName, typeof(double));
			string temp = string.Format("ISNULL({0},0) * {1} * {2}", ProgramRatingDataColumnName, ProgramTotalSpotDataColumnName, (Parent.DemoType == DemoType.Rtg ? "1" : "1000"));
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
			temp = string.Format("Sum({0})/(Sum({1})/{2})", ProgramCostDataColumnName, ProgramGRPDataColumnName, (Parent.DemoType == DemoType.Rtg ? "1" : "1000"));
			column.Expression = temp;
			table.Columns.Add(column);

			#region Fill Programs Data

			foreach (Program program in Programs)
			{
				DataRow row = table.NewRow();
				row.BeginEdit();
				row[ProgramIndexDataColumnName] = program.Index.ToString();
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
				for (int i = 0; i < program.Spots.Count; i++)
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

			DataSource.Tables.Add(table);

			#endregion
		}

		private void UpdateProgramsFromDataSource(DataRow row)
		{
			int tempInt = 0;
			string temp = string.Empty;

			int index = -1;
			temp = row[ProgramIndexDataColumnName] != DBNull.Value ? row[ProgramIndexDataColumnName].ToString() : string.Empty;
			if (int.TryParse(temp, out tempInt))
				index = tempInt;
			var program = Programs.FirstOrDefault(x => x.Index == index);
			if (program != null)
			{
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
				double tempDouble = 0;
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

		private void RebuildProgramIndexes()
		{
			for (int i = 0; i < Programs.Count; i++)
				Programs[i].Index = i + 1;
		}

		public void RebuildSpots()
		{
			foreach (var program in Programs)
				program.RebuildSpots();
		}

		#region Options

		public bool ShowRate { get; set; }
		public bool ShowRating { get; set; }
		public bool ShowTime { get; set; }
		public bool ShowDay { get; set; }
		public bool ShowDaypart { get; set; }
		public bool ShowStation { get; set; }
		public bool ShowLenght { get; set; }
		public bool ShowCPP { get; set; }
		public bool ShowGRP { get; set; }
		public bool ShowSpots { get; set; }
		public bool ShowEmptySpots { get; set; }
		public bool ShowCost { get; set; }

		public bool ShowTotalPeriods { get; set; }
		public bool ShowTotalSpots { get; set; }
		public bool ShowTotalGRP { get; set; }
		public bool ShowTotalCPP { get; set; }
		public bool ShowAverageRate { get; set; }
		public bool ShowTotalRate { get; set; }
		public bool ShowNetRate { get; set; }
		public bool ShowDiscount { get; set; }

		#endregion

		#region Calculated Properies

		public int TotalPeriods
		{
			get
			{
				Program defaultprogram = Programs.FirstOrDefault();
				if (defaultprogram != null)
				{
					return ShowEmptySpots ? defaultprogram.Spots.Count : defaultprogram.SpotsNotEmpty.Length;
				}
				return 0;
			}
		}

		public double TotalCPP
		{
			get { return TotalGRP != 0 ? (TotalCost / (TotalGRP / (Parent.DemoType == DemoType.Rtg ? 1 : 1000))) : 0; }
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
	}

	public class WeeklySection : ScheduleSection
	{
		public WeeklySection(Schedule parent)
			: base(parent)
		{
			SpotType = SpotType.Week;
		}
	}

	public class MonthlySection : ScheduleSection
	{
		public MonthlySection(Schedule parent)
			: base(parent)
		{
			SpotType = SpotType.Month;
		}
	}

	public class Program
	{
		private string _name;
		private string _day;

		#region Basic Properties

		public ScheduleSection Parent { get; set; }
		public int Index { get; set; }
		public string Station { get; set; }
		public string Daypart { get; set; }
		public string Time { get; set; }
		public string Length { get; set; }
		public double? Rate { get; set; }
		public double? Rating { get; set; }
		public List<Spot> Spots { get; set; }

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
			get { return (Rating.HasValue ? Rating.Value : 0) * TotalSpots * (Parent.Parent.DemoType == DemoType.Rtg ? 1 : 1000); }
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
			Index = Parent.Programs.Count + 1;
			Station = Parent.Parent.Stations.Count(x => x.Available) == 1 ? Parent.Parent.Stations.Where(x => x.Available).Select(x => x.Name).FirstOrDefault() : string.Empty;
			Daypart = string.Empty;
			Day = string.Empty;
			Time = string.Empty;
			Length = string.Empty;
			Spots = new List<Spot>();
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			if (!string.IsNullOrEmpty(_name))
			{
				result.Append(@"<Program ");
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

				result.AppendLine(@"<Spots>");
				foreach (Spot spot in Spots)
					result.AppendLine(@"<Spot>" + spot.Serialize() + @"</Spot>");
				result.AppendLine(@"</Spots>");

				result.AppendLine(@"</Program>");
			}
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			double tempDouble;

			foreach (XmlAttribute programAttribute in node.Attributes)
				switch (programAttribute.Name)
				{
					case "Name":
						_name = programAttribute.Value;
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
				}
		}

		public void ApplyDefaultValues()
		{
			var source = MediaMetaData.Instance.ListManager.SourcePrograms.Where(x => x.Name.Equals(_name)).ToArray();
			if (source.Length <= 0) return;
			Daypart = source[0].Day;
			Day = source[0].Day;
			Time = source[0].Time;
		}

		public void RebuildSpots()
		{
			Spots.Clear();
			if (!Parent.Parent.FlightDateStart.HasValue || !Parent.Parent.FlightDateEnd.HasValue) return;
			var spotDate = Parent.Parent.FlightDateStart.Value;
			var endDate = Parent.Parent.FlightDateEnd.Value;
			while (spotDate < endDate)
			{
				var spot = new Spot(this) { Date = spotDate };
				spotDate = Parent.SpotType == SpotType.Week ? spotDate.AddDays(7) : new DateTime(spotDate.AddMonths(1).Year, spotDate.AddMonths(1).Month, 1);
				Spots.Add(spot);
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

	public class Daypart
	{
		public Daypart()
		{
			Name = string.Empty;
			Code = string.Empty;
			Available = true;
		}

		public string Name { get; set; }
		public string Code { get; set; }
		public bool Available { get; set; }

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
	}

	public class Station
	{
		public Station()
		{
			Name = string.Empty;
			Available = true;
		}

		public string Name { get; set; }
		public Image Logo { get; set; }
		public bool Available { get; set; }

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

		public string ColumnName
		{
			get
			{
				switch (_parent.Parent.SpotType)
				{
					case SpotType.Month:
						return GetMonthAbbreviation(Date.Month);
					case SpotType.Week:
						return String.Format("{0}\n{1}", GetMonthAbbreviation(Date.Month), Date.Day.ToString("00"));
					default:
						return string.Empty;
				}
			}
		}

		public string DisplayString
		{
			get
			{
				var result = new List<string>();
				if (!Count.HasValue) return String.Empty;
				result.Add(_parent.Name);
				if (!String.IsNullOrEmpty(_parent.Daypart))
					result.Add(_parent.Daypart);
				if (!String.IsNullOrEmpty(_parent.Time))
					result.Add(_parent.Time);
				result.Add(String.Format("{0}x", Count.Value));
				return String.Format("[{0}]", String.Join(", ", result));
			}
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
	}

	public class BroadcastCalendar : CalendarMondayBased
	{
		private readonly Func<IEnumerable<BroadcastMonthTemplate>> _getBroadcastMonthTemplates;
		private Schedule _parentSchedule;

		public override bool AllowCustomNotes
		{
			get { return false; }
		}

		public BroadcastCalendar(ISchedule parent,
			Func<IEnumerable<BroadcastMonthTemplate>> getBroadcastMonthTemplates)
			: base(parent)
		{
			_parentSchedule = parent as Schedule;
			_getBroadcastMonthTemplates = getBroadcastMonthTemplates;
		}

		public override void UpdateMonthCollection()
		{
			if (!Schedule.FlightDateStart.HasValue || !Schedule.FlightDateEnd.HasValue)
			{
				Months.Clear();
				return;
			}
			var months = new List<CalendarMonth>();
			var startDate = Schedule.FlightDateStart.Value;
			var monthTemplates = _getBroadcastMonthTemplates();
			while (startDate <= Schedule.FlightDateEnd.Value)
			{
				var month = Months.FirstOrDefault(x => x.Date.Equals(startDate));
				if (month == null)
				{
					var monthTemplate = monthTemplates.FirstOrDefault(mt => startDate >= mt.StartDate && startDate <= mt.EndDate);
					if (monthTemplate != null)
					{
						month = new CalendarMonthMondayBased(this);
						startDate = monthTemplate.Month.Value;
						month.Date = monthTemplate.Month.Value;
						month.DaysRangeBegin = monthTemplate.StartDate.Value;
						month.DaysRangeEnd = monthTemplate.EndDate.Value;
					}
				}
				if (month == null) continue;
				month.Days.Clear();
				month.Days.AddRange(Days.Where(x => x.Date >= month.DaysRangeBegin && x.Date <= month.DaysRangeEnd));
				months.Add(month);
				startDate = startDate.AddMonths(1);
			}
			Months.Clear();
			Months.AddRange(months);
		}

		public override void UpdateNotesCollection()
		{
			const string noteSeparator = "   ";
			var notes = new List<CalendarNote>();
			if (Schedule.FlightDateStart.HasValue && Schedule.FlightDateEnd.HasValue)
			{
				notes.AddRange(_parentSchedule.WeeklySchedule.Programs
					.SelectMany(p => p.Spots)
					.Where(s => s.Count > 0 && s.StartDate.HasValue && s.EndDate.HasValue)
					.GroupBy(g => new { g.StartDate, g.EndDate })
					.Select(g => new CalendarNote(this)
					{
						StartDay = g.Key.StartDate.Value,
						FinishDay = g.Key.EndDate.Value,
						Note = String.Join(noteSeparator, g.Select(sp => sp.DisplayString)),
						ReadOnly = true
					}));

				bool needToSplit;
				var splittedNotes = new List<CalendarNote>(notes);
				do
				{
					needToSplit = false;
					foreach (var calendarNote in notes.OrderByDescending(n => n.Length))
					{
						var intersectedNote = splittedNotes.Where(sn => sn != calendarNote).OrderBy(n => n.Length).FirstOrDefault(mn =>
							(mn.StartDay >= calendarNote.StartDay && mn.StartDay <= calendarNote.FinishDay) ||
							(mn.FinishDay >= calendarNote.StartDay && mn.FinishDay <= calendarNote.FinishDay));
						if (intersectedNote == null) continue;
						needToSplit = true;
						if ((intersectedNote.StartDay >= calendarNote.StartDay && intersectedNote.StartDay <= calendarNote.FinishDay) &&
							(intersectedNote.FinishDay >= calendarNote.StartDay && intersectedNote.FinishDay <= calendarNote.FinishDay))
						{
							if (intersectedNote.StartDay > calendarNote.StartDay)
								splittedNotes.Add(new CalendarNote(this)
								{
									StartDay = calendarNote.StartDay,
									FinishDay = intersectedNote.StartDay.AddDays(-1),
									Note = calendarNote.Note,
									ReadOnly = true
								});
							if (intersectedNote.FinishDay < calendarNote.FinishDay)
								splittedNotes.Add(new CalendarNote(this)
								{
									StartDay = intersectedNote.FinishDay.AddDays(1),
									FinishDay = calendarNote.FinishDay,
									Note = calendarNote.Note,
									ReadOnly = true
								});
							intersectedNote.Note = String.Format("{0}{1}{2}", calendarNote.Note, noteSeparator, intersectedNote.Note);
							splittedNotes.Remove(calendarNote);
						}
						else if (intersectedNote.StartDay >= calendarNote.StartDay && intersectedNote.StartDay <= calendarNote.FinishDay)
						{
							splittedNotes.Add(new CalendarNote(this)
							{
								StartDay = calendarNote.StartDay,
								FinishDay = intersectedNote.StartDay.AddDays(-1),
								Note = calendarNote.Note,
								ReadOnly = true
							});
							intersectedNote.Note = String.Format("{0}{1}{2}", calendarNote.Note, noteSeparator, intersectedNote.Note);
							splittedNotes.Remove(calendarNote);
						}
						else if (intersectedNote.FinishDay >= calendarNote.StartDay && intersectedNote.FinishDay <= calendarNote.FinishDay)
						{
							splittedNotes.Add(new CalendarNote(this)
							{
								StartDay = intersectedNote.FinishDay.AddDays(1),
								FinishDay = calendarNote.FinishDay,
								Note = calendarNote.Note,
								ReadOnly = true
							});
							intersectedNote.Note = String.Format("{0}{1}{2}", calendarNote.Note, noteSeparator, intersectedNote.Note);
							splittedNotes.Remove(calendarNote);
						}
					}
					notes.Clear();
					notes.AddRange(splittedNotes);
				} while (needToSplit);

				if (Notes.Any(n => n.BackgroundColor != CalendarNote.DefaultBackgroundColor))
				{
					if (Notes.All(n => n.BackgroundColor == Notes.First().BackgroundColor))
					{
						notes.ForEach(note => note.BackgroundColor = Notes.First().BackgroundColor);
					}
					else
					{
						foreach (var note in notes)
						{
							var existedNote = Notes.FirstOrDefault(n => n.StartDay == note.StartDay && n.FinishDay == note.FinishDay);
							if (existedNote != null)
								note.BackgroundColor = existedNote.BackgroundColor;
						}
					}
				}
			}
			Notes.Clear();
			Notes.AddRange(notes);
			UpdateDayAndNoteLinks();
		}
	}

	public class BroadcastMonthTemplate
	{
		public DateTime? Month { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }
	}
}