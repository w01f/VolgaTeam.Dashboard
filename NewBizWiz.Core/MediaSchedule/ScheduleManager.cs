using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Asa.Core.Common;
using Asa.Core.Interop;
using Asa.Core.OnlineSchedule;

namespace Asa.Core.MediaSchedule
{
	public class ScheduleManager
	{
		private RegularSchedule _currentSchedule;

		public bool ScheduleLoaded { get; set; }

		public event EventHandler<ScheduleSaveEventArgs> SettingsSaved;

		public string CurrentAdvertiser
		{
			get { return _currentSchedule != null ? _currentSchedule.BusinessName : null; }
		}

		public void OpenSchedule(string scheduleName, bool create)
		{
			string scheduleFilePath = GetScheduleFileName(scheduleName);
			if (create && File.Exists(scheduleFilePath))
				if (Utilities.Instance.ShowWarningQuestion(string.Format("An older Schedule is already saved with this same file name.\nDo you want to replace this file with a newer schedule?", scheduleName)) == DialogResult.Yes)
					File.Delete(scheduleFilePath);
			_currentSchedule = new RegularSchedule(scheduleFilePath);
			ScheduleLoaded = true;
		}

		public void OpenSchedule(string scheduleFilePath)
		{
			_currentSchedule = new RegularSchedule(scheduleFilePath);
			ScheduleLoaded = true;
		}

		public string GetScheduleFileName(string scheduleName)
		{
			return Path.Combine(AppProfileManager.Instance.AppSaveFolder.LocalPath, scheduleName + ".xml");
		}

		public RegularSchedule GetLocalSchedule()
		{
			return new RegularSchedule(_currentSchedule.ScheduleFile.FullName);
		}

		public ShortSchedule GetShortSchedule()
		{
			return _currentSchedule != null ? new ShortSchedule(_currentSchedule.ScheduleFile) : null;
		}

		public void SaveSchedule(RegularSchedule localSchedule, bool quickSave, bool updateDigital, bool calendarTypeChanged, Control sender)
		{
			localSchedule.Save();
			_currentSchedule = localSchedule;
			if (SettingsSaved != null)
				SettingsSaved(sender, new ScheduleSaveEventArgs(quickSave, updateDigital, calendarTypeChanged));
		}

		public static ShortSchedule[] GetShortScheduleList()
		{
			var saveFolder = new DirectoryInfo(AppProfileManager.Instance.AppSaveFolder.LocalPath);
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
			catch
			{
			}
		}
	}

	public abstract class Schedule : ISchedule
	{
		private DateTime? _userFlightDateStart;
		private DateTime? _userFlightDateEnd;

		public abstract string Name { get; set; }
		public bool IsNew { get; set; }
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
		public bool MondayBased { get; private set; }

		public SpotType SelectedSpotType { get; set; }

		public List<Daypart> Dayparts { get; private set; }
		public List<Station> Stations { get; private set; }

		public ScheduleBuilderViewSettings ViewSettings { get; set; }
		public IScheduleViewSettings SharedViewSettings
		{
			get { return ViewSettings; }
		}

		public List<Quarter> Quarters { get; private set; }

		public DateTime? UserFlightDateStart
		{
			get
			{
				return _userFlightDateStart;
			}
			set
			{
				_userFlightDateStart = value;
				FlightDateStart = value;
				if (!FlightDateStart.HasValue) return;
				while (FlightDateStart.Value.DayOfWeek != StartDayOfWeek)
					FlightDateStart = FlightDateStart.Value.AddDays(-1);
			}
		}

		public DateTime? UserFlightDateEnd
		{
			get
			{
				return _userFlightDateEnd;
			}
			set
			{
				_userFlightDateEnd = value;
				FlightDateEnd = value;
				if (!FlightDateEnd.HasValue) return;
				while (FlightDateEnd.Value.DayOfWeek != EndDayOfWeek)
					FlightDateEnd = FlightDateEnd.Value.AddDays(1);
			}
		}

		public string FlightDates
		{
			get
			{
				if (UserFlightDateStart.HasValue && UserFlightDateEnd.HasValue)
					return UserFlightDateStart.Value.ToString("MM/dd/yy") + " - " + UserFlightDateEnd.Value.ToString("MM/dd/yy");
				return string.Empty;
			}
		}

		public DayOfWeek StartDayOfWeek
		{
			get { return MondayBased ? DayOfWeek.Monday : DayOfWeek.Sunday; }
		}

		public DayOfWeek EndDayOfWeek
		{
			get { return MondayBased ? DayOfWeek.Sunday : DayOfWeek.Saturday; }
		}

		public string StartWeekDays
		{
			get
			{
				if (!UserFlightDateStart.HasValue || UserFlightDateStart.Value.DayOfWeek == StartDayOfWeek) return String.Empty;
				var list = new List<string>();
				var date = UserFlightDateStart.Value;
				while (date.DayOfWeek != StartDayOfWeek)
				{
					list.Add(date.ToString("ddd"));
					date = date.AddDays(1);
				}
				return String.Join(", ", list);
			}
		}

		public string EndWeekDays
		{
			get
			{
				if (!UserFlightDateEnd.HasValue || UserFlightDateEnd.Value.DayOfWeek == EndDayOfWeek) return String.Empty;
				var list = new List<string>();
				var date = UserFlightDateEnd.Value;
				while (date.DayOfWeek != EndDayOfWeek)
				{
					list.Add(date.ToString("ddd"));
					date = date.AddDays(-1);
				}
				list.Reverse();
				return String.Join(", ", list);
			}
		}

		public string DisplayedSpotType
		{
			get { return String.Format("{0}ly", SelectedSpotType); }
			set
			{
				SpotType temp;
				if (!String.IsNullOrEmpty(value) && Enum.TryParse(value.Replace("ly", ""), true, out temp))
					SelectedSpotType = temp;
				else
					SelectedSpotType = SpotType.Week;
			}
		}

		public int TotalWeeks
		{
			get { return GetWeeks().Count(); }
		}

		protected Schedule()
		{
			BusinessName = string.Empty;
			DecisionMaker = string.Empty;
			ClientType = MediaMetaData.Instance.ListManager.ClientTypes.FirstOrDefault();
			AccountNumber = string.Empty;
			Status = MediaMetaData.Instance.ListManager.Statuses.FirstOrDefault();
			PresentationDate = DateTime.Now;
			UseDemo = false;
			ImportDemo = false;
			DemoType = DemoType.Imp;
			MondayBased = true;

			Dayparts = new List<Daypart>();
			Stations = new List<Station>();

			ViewSettings = new ScheduleBuilderViewSettings();
		}

		public virtual void Deserialize(XmlNode rootNode)
		{
			var node = rootNode.SelectSingleNode(@"BusinessName");
			if (node != null)
				BusinessName = node.InnerText;

			node = rootNode.SelectSingleNode(@"DecisionMaker");
			if (node != null)
				DecisionMaker = node.InnerText;

			node = rootNode.SelectSingleNode(@"ClientType");
			if (node != null)
				ClientType = node.InnerText;

			node = rootNode.SelectSingleNode(@"AccountNumber");
			if (node != null)
				AccountNumber = node.InnerText;

			node = rootNode.SelectSingleNode(@"Status");
			if (node != null)
				Status = node.InnerText;

			node = rootNode.SelectSingleNode(@"PresentationDate");
			DateTime tempDateTime;
			if (node != null)
				if (DateTime.TryParse(node.InnerText, out tempDateTime))
					PresentationDate = tempDateTime;

			node = rootNode.SelectSingleNode(@"FlightDateStart");
			if (node != null)
				if (DateTime.TryParse(node.InnerText, out tempDateTime))
					FlightDateStart = tempDateTime;

			node = rootNode.SelectSingleNode(@"FlightDateEnd");
			if (node != null)
				if (DateTime.TryParse(node.InnerText, out tempDateTime))
					FlightDateEnd = tempDateTime;

			node = rootNode.SelectSingleNode(@"UserFlightDateStart");
			if (node != null)
				if (DateTime.TryParse(node.InnerText, out tempDateTime))
					_userFlightDateStart = tempDateTime;
			if (!_userFlightDateStart.HasValue)
				_userFlightDateEnd = FlightDateStart;

			node = rootNode.SelectSingleNode(@"UserFlightDateEnd");
			if (node != null)
				if (DateTime.TryParse(node.InnerText, out tempDateTime))
					_userFlightDateEnd = tempDateTime;
			if (!_userFlightDateEnd.HasValue)
				_userFlightDateEnd = FlightDateEnd;

			node = rootNode.SelectSingleNode(@"UseDemo");
			bool tempBool;
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					UseDemo = tempBool;

			node = rootNode.SelectSingleNode(@"ImportDemo");
			if (node != null)
				if (bool.TryParse(node.InnerText, out tempBool))
					ImportDemo = tempBool;

			node = rootNode.SelectSingleNode(@"DemoType");
			int tempInt;
			if (node != null)
				if (int.TryParse(node.InnerText, out tempInt))
					DemoType = (DemoType)tempInt;

			node = rootNode.SelectSingleNode(@"MondayBased");
			if (node != null)
			{
				bool temp;
				if (Boolean.TryParse(node.InnerText, out temp))
					MondayBased = temp;
			}

			node = rootNode.SelectSingleNode(@"Demo");
			if (node != null)
				Demo = node.InnerText;

			node = rootNode.SelectSingleNode(@"Source");
			if (node != null)
				Source = node.InnerText;

			node = rootNode.SelectSingleNode(@"SelectedSpotType");
			if (node != null)
				if (int.TryParse(node.InnerText, out tempInt))
					SelectedSpotType = (SpotType)tempInt;

			node = rootNode.SelectSingleNode(@"Dayparts");
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

			node = rootNode.SelectSingleNode(@"Stations");
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

			node = rootNode.SelectSingleNode(@"ViewSettings");
			if (node != null)
			{
				ViewSettings.Deserialize(node);
			}
		}

		public virtual StringBuilder Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<BusinessName>" + BusinessName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BusinessName>");
			Common.ListManager.Instance.Advertisers.Add(BusinessName);
			Common.ListManager.Instance.Advertisers.Save();

			result.AppendLine(@"<DecisionMaker>" + DecisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
			Common.ListManager.Instance.DecisionMakers.Add(DecisionMaker);
			Common.ListManager.Instance.DecisionMakers.Save();

			result.AppendLine(@"<Status>" + (Status != null ? Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
			result.AppendLine(@"<ClientType>" + ClientType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ClientType>");
			result.AppendLine(@"<AccountNumber>" + AccountNumber.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</AccountNumber>");
			if (PresentationDate.HasValue)
				result.AppendLine(@"<PresentationDate>" + PresentationDate.Value + @"</PresentationDate>");
			if (FlightDateStart.HasValue)
				result.AppendLine(@"<FlightDateStart>" + FlightDateStart.Value + @"</FlightDateStart>");
			if (FlightDateEnd.HasValue)
				result.AppendLine(@"<FlightDateEnd>" + FlightDateEnd.Value + @"</FlightDateEnd>");
			if (_userFlightDateStart.HasValue)
				result.AppendLine(@"<UserFlightDateStart>" + _userFlightDateStart.Value + @"</UserFlightDateStart>");
			if (_userFlightDateEnd.HasValue)
				result.AppendLine(@"<UserFlightDateEnd>" + _userFlightDateEnd.Value + @"</UserFlightDateEnd>");
			result.AppendLine(@"<UseDemo>" + UseDemo + @"</UseDemo>");
			result.AppendLine(@"<ImportDemo>" + ImportDemo + @"</ImportDemo>");
			result.AppendLine(@"<DemoType>" + (int)DemoType + @"</DemoType>");
			result.AppendLine(@"<MondayBased>" + MondayBased + @"</MondayBased>");
			if (!String.IsNullOrEmpty(Demo))
				result.AppendLine(@"<Demo>" + Demo.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Demo>");
			if (!String.IsNullOrEmpty(Source))
				result.AppendLine(@"<Source>" + Source.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Source>");
			result.AppendLine(@"<SelectedSpotType>" + (Int32)SelectedSpotType + @"</SelectedSpotType>");

			result.AppendLine(@"<Dayparts>");
			foreach (var daypart in Dayparts)
				result.AppendLine(daypart.Serialize());
			result.AppendLine(@"</Dayparts>");

			result.AppendLine(@"<Stations>");
			foreach (var station in Stations)
				result.AppendLine(station.Serialize());
			result.AppendLine(@"</Stations>");

			result.AppendLine(@"<ViewSettings>" + ViewSettings.Serialize() + @"</ViewSettings>");

			return result;
		}

		public IEnumerable<DateRange> GetWeeks()
		{
			var weeks = new List<DateRange>();
			if (FlightDateStart.HasValue && FlightDateEnd.HasValue)
			{
				var startDate = FlightDateStart.Value;
				while (startDate < FlightDateEnd)
				{
					weeks.Add(new DateRange()
					{
						StartDate = startDate,
						FinishDate = startDate.AddDays(6)
					});
					startDate = startDate.AddDays(7);
				}
			}
			return weeks;
		}

		public virtual void ResetCalendarType(bool isMondayBased)
		{
			MondayBased = isMondayBased;
			LoadQuarters();
		}

		protected void LoadQuarters()
		{
			Quarters = new List<Quarter>();
			if (!FlightDateStart.HasValue || !FlightDateEnd.HasValue) return;
			var targetMonths = (MondayBased ? MediaMetaData.Instance.ListManager.MonthTemplatesMondayBased : MediaMetaData.Instance.ListManager.MonthTemplatesSundayBased).Where(m => (m.StartDate <= FlightDateStart && m.EndDate >= FlightDateStart) || (m.StartDate <= FlightDateEnd && m.EndDate >= FlightDateEnd) || (m.StartDate >= FlightDateStart && m.EndDate <= FlightDateEnd)).ToList();
			if (!targetMonths.Any()) return;
			var startDate = FlightDateStart.Value;
			if (startDate.Month >= 1 && startDate.Month <= 3)
				startDate = new DateTime(startDate.Year, 1, 1);
			else if (startDate.Month >= 4 && startDate.Month <= 6)
				startDate = new DateTime(startDate.Year, 4, 1);
			else if (startDate.Month >= 7 && startDate.Month <= 9)
				startDate = new DateTime(startDate.Year, 7, 1);
			else if (startDate.Month >= 10 && startDate.Month <= 12)
				startDate = new DateTime(startDate.Year, 10, 1);
			while (startDate <= FlightDateEnd.Value)
			{
				var endDate = startDate.AddMonths(3);
				var quarter = new Quarter { DateAnchor = startDate };
				var quarterMonths = targetMonths.Where(m => (m.StartDate.Value.Day < 15 && m.StartDate.Value >= startDate && m.StartDate <= endDate) ||
					(m.StartDate.Value.Day > 15 && m.EndDate >= startDate && m.EndDate <= endDate)
					).OrderBy(m => m.Month).ToList();
				startDate = endDate;
				if (!quarterMonths.Any()) continue;
				quarter.DateStart = quarterMonths.First().StartDate.Value;
				quarter.DateEnd = quarterMonths.Last().EndDate.Value;
				Quarters.Add(quarter);
			}
		}
	}

	public class RegularSchedule : Schedule, IDigitalSchedule
	{
		private FileInfo _scheduleFile { get; set; }

		public ProgramSchedule ProgramSchedule { get; private set; }

		public List<DigitalProduct> DigitalProducts { get; private set; }
		public DigitalProductSummary DigitalProductSummary { get; private set; }

		public BroadcastCalendar BroadcastCalendar { get; set; }
		public CustomCalendar CustomCalendar { get; set; }

		public List<Snapshot> Snapshots { get; private set; }
		public SnapshotSummary SnapshotSummary { get; private set; }
		public List<OptionSet> Options { get; private set; }
		public OptionSummary OptionsSummary { get; private set; }

		public override string Name
		{
			get { return _scheduleFile.Name.Replace(_scheduleFile.Extension, ""); }
			set { _scheduleFile = new FileInfo(Path.Combine(_scheduleFile.Directory.FullName, value + ".xml")); }
		}

		public FileInfo ScheduleFile
		{
			get { return _scheduleFile; }
		}

		public RegularSchedule(string fileName)
		{
			InitProgramSchedule();

			DigitalProducts = new List<DigitalProduct>();
			DigitalProductSummary = new DigitalProductSummary();

			Snapshots = new List<Snapshot>();
			SnapshotSummary = new SnapshotSummary(this);
			Options = new List<OptionSet>();
			OptionsSummary = new OptionSummary(this);

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
			LoadQuarters();

			Dayparts.AddRange(MediaMetaData.Instance.ListManager.Dayparts.Where(x => !Dayparts.Select(y => y.Name).Contains(x.Name)));
			Stations.AddRange(MediaMetaData.Instance.ListManager.Stations.Where(x => !Stations.Select(y => y.Name).Contains(x.Name)));
		}

		public override void Deserialize(XmlNode rootNode)
		{
			base.Deserialize(rootNode);

			var node = rootNode.SelectSingleNode(@"ProgramSchedule");
			if (node != null)
			{
				InitProgramSchedule();
				ProgramSchedule.Deserialize(node);
			}

			node = rootNode.SelectSingleNode(@"DigitalProducts");
			if (node != null)
			{
				foreach (XmlNode productNode in node.ChildNodes)
				{
					var product = new DigitalProduct(this);
					product.Deserialize(productNode);
					DigitalProducts.Add(product);
				}
			}

			node = rootNode.SelectSingleNode(@"DigitalProductSummary");
			if (node != null)
			{
				DigitalProductSummary.Deserialize(node);
			}

			node = rootNode.SelectSingleNode(@"Snapshots");
			if (node != null)
			{
				foreach (XmlNode snapshotNode in node.ChildNodes)
				{
					var snapshot = new Snapshot(this);
					snapshot.Deserialize(snapshotNode);
					Snapshots.Add(snapshot);
				}
			}

			node = rootNode.SelectSingleNode(@"SnapshotSummary");
			if (node != null)
			{
				SnapshotSummary.Deserialize(node);
			}

			node = rootNode.SelectSingleNode(@"Options");
			if (node != null)
			{
				foreach (XmlNode optionSetNode in node.ChildNodes)
				{
					var optionSet = new OptionSet(this);
					optionSet.Deserialize(optionSetNode);
					Options.Add(optionSet);
				}
			}

			node = rootNode.SelectSingleNode(@"OptionsSummary");
			if (node != null)
			{
				OptionsSummary.Deserialize(node);
			}

			RebuildSnapshotIndexes();
			RebuildOptionSetIndexes();
		}

		public override StringBuilder Serialize()
		{
			var result = base.Serialize();

			result.AppendLine(String.Format("<ProgramSchedule>{0}</ProgramSchedule>", ProgramSchedule.Serialize()));

			result.AppendLine(@"<DigitalProducts>");
			foreach (var digitalProduct in DigitalProducts)
				result.AppendLine(digitalProduct.Serialize());
			result.AppendLine(@"</DigitalProducts>");

			result.AppendLine(@"<BroadcastCalendar>" + BroadcastCalendar.Serialize() + @"</BroadcastCalendar>");
			result.AppendLine(@"<CustomCalendar>" + CustomCalendar.Serialize() + @"</CustomCalendar>");
			result.AppendLine(@"<DigitalProductSummary>" + DigitalProductSummary.Serialize() + @"</DigitalProductSummary>");

			result.AppendLine(@"<Snapshots>");
			foreach (var snapshot in Snapshots)
				result.AppendLine(@"<Snapshot>" + snapshot.Serialize() + @"</Snapshot>");
			result.AppendLine(@"</Snapshots>");
			result.AppendLine(@"<SnapshotSummary>" + SnapshotSummary.Serialize() + @"</SnapshotSummary>");

			result.AppendLine(@"<Options>");
			foreach (var optionSet in Options)
				result.AppendLine(@"<Option>" + optionSet.Serialize() + @"</Option>");
			result.AppendLine(@"</Options>");
			result.AppendLine(@"<OptionsSummary>" + OptionsSummary.Serialize() + @"</OptionsSummary>");
			return result;
		}

		public void InitProgramSchedule()
		{
			ProgramSchedule = ProgramSchedule.Create(this);
		}

		public override void ResetCalendarType(bool isMondayBased)
		{
			base.ResetCalendarType(isMondayBased);
			BroadcastCalendar.Reset();
			CustomCalendar.Reset();
		}

		private void Load()
		{
			if (!_scheduleFile.Exists) return;
			var document = new XmlDocument();
			document.Load(_scheduleFile.FullName);
			Deserialize(document.SelectSingleNode(@"/Schedule"));
		}

		public void Save()
		{
			using (var sw = new StreamWriter(_scheduleFile.FullName, false))
			{
				sw.Write(@"<Schedule>{0}</Schedule>", Serialize());
				sw.Flush();
			}
			RemoteUploadManager.Upload(new StorageFile(AppProfileManager.Instance.AppSaveFolder.RelativePathParts.Merge(_scheduleFile.Name)));
			//AsyncHelper.RunSync(() => new StorageFile(AppProfileManager.Instance.AppSaveFolder.RelativePathParts.Merge(_scheduleFile.Name)).Upload());
		}

		protected void LoadCalendars()
		{
			BroadcastCalendar = new BroadcastCalendar(this);
			CustomCalendar = new CustomCalendar(this);

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
				BroadcastCalendar.UpdateDataSource();
				BroadcastCalendar.UpdateDaysCollection();
				BroadcastCalendar.UpdateMonthCollection();
				BroadcastCalendar.UpdateNotesCollection();
			}

			node = document.SelectSingleNode(@"/Schedule/CustomCalendar");
			if (node != null)
			{
				CustomCalendar.Deserialize(node);
			}
			else
			{
				CustomCalendar.UpdateDaysCollection();
				CustomCalendar.UpdateMonthCollection();
				CustomCalendar.UpdateNotesCollection();
			}
		}

		public void AddDigital(string categoryName)
		{
			DigitalProducts.Add(new DigitalProduct(this) { Index = DigitalProducts.Count + 1, Category = categoryName }); ;
		}

		public void UpDigital(int position)
		{
			if (position <= 0) return;
			DigitalProducts[position].Index--;
			DigitalProducts[position - 1].Index++;
			DigitalProducts.Sort((x, y) => x.Index.CompareTo(y.Index));
		}

		public void DownDigital(int position)
		{
			if (position >= DigitalProducts.Count - 1) return;
			DigitalProducts[position].Index++;
			DigitalProducts[position + 1].Index--;
			DigitalProducts.Sort((x, y) => x.Index.CompareTo(y.Index));
		}

		public void ChangeDigitalProductPosition(int position, int newPosition)
		{
			if (position < 0 || position >= DigitalProducts.Count) return;
			var product = DigitalProducts[position];
			product.Index = newPosition + 0.5;
			DigitalProducts.Sort((x, y) => x.Index.CompareTo(y.Index));
			RebuildDigitalProductIndexes();
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
				var compiledWebsites = String.Join(", ", DigitalProducts.SelectMany(p => p.Websites).Distinct());
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
			ProgramSchedule.DigitalLegend = digitalLegend.Clone();
		}

		public void ChangeSnapshotPosition(int position, int newPosition)
		{
			if (position < 0 || position >= Snapshots.Count) return;
			var snapshot = Snapshots[position];
			snapshot.Index = newPosition - 0.5;
			RebuildSnapshotIndexes();
		}

		public void RebuildSnapshotIndexes()
		{
			var i = 0;
			foreach (var snapshot in Snapshots.OrderBy(o => o.Index))
			{
				snapshot.Index = i;
				i++;
			}
			Snapshots.Sort((x, y) => x.Index.CompareTo(y.Index));
		}

		public void ChangeOptionSetPosition(int position, int newPosition)
		{
			if (position < 0 || position >= Options.Count) return;
			var optionSet = Options[position];
			optionSet.Index = newPosition - 0.5;
			RebuildOptionSetIndexes();
		}

		public void RebuildOptionSetIndexes()
		{
			var i = 0;
			foreach (var optionSet in Options.OrderBy(o => o.Index))
			{
				optionSet.Index = i;
				i++;
			}
			Options.Sort((x, y) => x.Index.CompareTo(y.Index));
		}
	}

	public class Quarter
	{
		public DateTime DateAnchor { get; set; }
		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }

		public int QuarterNumber
		{
			get
			{
				if (DateAnchor.Month >= 1 && DateAnchor.Month <= 3)
					return 1;
				if (DateAnchor.Month >= 4 && DateAnchor.Month <= 6)
					return 2;
				if (DateAnchor.Month >= 7 && DateAnchor.Month <= 9)
					return 3;
				if (DateAnchor.Month >= 10 && DateAnchor.Month <= 12)
					return 4;
				return 0;
			}
		}

		public override string ToString()
		{
			return String.Format("Q{0} {1}", QuarterNumber, DateAnchor.ToString("yy"));
		}
	}
}