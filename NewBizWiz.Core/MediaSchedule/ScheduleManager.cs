﻿using System;
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
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.Core.OnlineSchedule;
using NewBizWiz.Core.Calendar;
using DataTable = System.Data.DataTable;

namespace NewBizWiz.Core.MediaSchedule
{
	public enum SpotType
	{
		Week = 0,
		Month,
		Total
	}

	public enum DemoType
	{
		Rtg = 0,
		Imp
	}

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
			return Path.Combine(MediaMetaData.Instance.SettingsManager.SaveFolder, scheduleName + ".xml");
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
			var saveFolder = new DirectoryInfo(MediaMetaData.Instance.SettingsManager.SaveFolder);
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

		private ScheduleSection _weeklySection;
		private ScheduleSection _monthlySection;

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

			_weeklySection = new WeeklySection(this);
			_monthlySection = new MonthlySection(this);

			ResetSection();

			Dayparts = new List<Daypart>();
			Stations = new List<Station>();

			ViewSettings = new ScheduleBuilderViewSettings();
		}

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
		public ScheduleSection Section { get; private set; }
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

			node = rootNode.SelectSingleNode(@"WeeklySection");
			if (node != null)
				_weeklySection.Deserialize(node);

			node = rootNode.SelectSingleNode(@"MonthlySection");
			if (node != null)
				_monthlySection.Deserialize(node);

			node = rootNode.SelectSingleNode(@"Section");
			if (node != null)
			{
				if (SelectedSpotType == SpotType.Week)
					_weeklySection.Deserialize(node);
				else if (SelectedSpotType == SpotType.Month)
					_monthlySection.Deserialize(node);
			}
			ResetSection();

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
			result.AppendLine(@"<WeeklySection>" + _weeklySection.Serialize() + @"</WeeklySection>");
			result.AppendLine(@"<MonthlySection>" + _monthlySection.Serialize() + @"</MonthlySection>");

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

		public virtual void ResetCalendarType(bool isMondayBased)
		{
			MondayBased = isMondayBased;
			LoadQuarters();
		}

		public void ResetSection()
		{
			switch (SelectedSpotType)
			{
				case SpotType.Week:
					if (_weeklySection == null)
						_weeklySection = ScheduleSection.GetSectionByType(this);
					Section = _weeklySection;
					break;
				case SpotType.Month:
					if (_monthlySection == null)
						_monthlySection = ScheduleSection.GetSectionByType(this);
					Section = _monthlySection;
					break;
			}
		}

		public void RebuildSectionSpots()
		{
			if (_weeklySection != null)
				_weeklySection.RebuildSpots();
			if (_monthlySection != null)
				_monthlySection.RebuildSpots();
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

	public class RegularSchedule : Schedule, IDigitalSchedule, ISummarySchedule
	{
		private FileInfo _scheduleFile { get; set; }
		public List<DigitalProduct> DigitalProducts { get; private set; }
		public DigitalProductSummary DigitalProductSummary { get; private set; }

		public BroadcastCalendar BroadcastCalendar { get; set; }
		public CustomCalendar CustomCalendar { get; set; }

		public BaseSummarySettings ProductSummary { get; private set; }
		public CustomSummarySettings CustomSummary { get; private set; }

		public ProgramStrategy ProgramStrategy { get; private set; }

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

		public IEnumerable<ISummaryProduct> ProductSummaries
		{
			get
			{
				var result = new List<ISummaryProduct>();
				result.AddRange(Section.Programs);
				result.AddRange(DigitalProducts);
				return result;
			}
		}

		public RegularSchedule(string fileName)
		{
			DigitalProducts = new List<DigitalProduct>();
			DigitalProductSummary = new DigitalProductSummary();

			ProductSummary = new BaseSummarySettings();
			CustomSummary = new MediaFullSummarySettings(this);

			ProgramStrategy = new ProgramStrategy(this);

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
			var node = rootNode.SelectSingleNode(@"DigitalProducts");
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

			node = rootNode.SelectSingleNode(@"ProductSummary");
			if (node != null)
			{
				ProductSummary.Deserialize(node);
			}

			node = rootNode.SelectSingleNode(@"CustomSummary");
			if (node != null)
			{
				CustomSummary.Deserialize(node);
			}

			node = rootNode.SelectSingleNode(@"ProgramStrategy");
			if (node != null)
			{
				ProgramStrategy.Deserialize(node);
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
			result.AppendLine(@"<DigitalProducts>");
			foreach (var digitalProduct in DigitalProducts)
				result.AppendLine(digitalProduct.Serialize());
			result.AppendLine(@"</DigitalProducts>");

			result.AppendLine(@"<BroadcastCalendar>" + BroadcastCalendar.Serialize() + @"</BroadcastCalendar>");
			result.AppendLine(@"<CustomCalendar>" + CustomCalendar.Serialize() + @"</CustomCalendar>");
			result.AppendLine(@"<DigitalProductSummary>" + DigitalProductSummary.Serialize() + @"</DigitalProductSummary>");
			result.AppendLine(@"<ProductSummary>" + ProductSummary.Serialize() + @"</ProductSummary>");
			result.AppendLine(@"<CustomSummary>" + CustomSummary.Serialize() + @"</CustomSummary>");
			result.AppendLine(@"<ProgramStrategy>" + ProgramStrategy.Serialize() + @"</ProgramStrategy>");

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
			Section.DigitalLegend = digitalLegend.Clone();
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

	public abstract class ScheduleSection
	{
		public const string ProgramDataTableName = "Programs";
		public const string ProgramIndexDataColumnName = "Index";
		public const string ProgramLogoImageDataColumnName = "LogoImage";
		public const string ProgramLogoSourceDataColumnName = "LogoSource";
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

		protected ScheduleSection(Schedule parent)
		{
			Parent = parent;
			Programs = new List<Program>();
			DigitalLegend = new DigitalLegend();

			#region Options

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
			ShowLogo = false;

			ShowTotalPeriods = true;
			ShowTotalSpots = true;
			ShowTotalGRP = true;
			ShowTotalCPP = true;
			ShowAverageRate = true;
			ShowTotalRate = true;
			ShowNetRate = false;
			ShowDiscount = false;

			ShowSelectedQuarter = false;
			#endregion
		}

		public Schedule Parent { get; private set; }
		public List<Program> Programs { get; set; }
		public SpotType SpotType { get; set; }

		public DataTable DataSource { get; private set; }

		public DigitalLegend DigitalLegend { get; set; }

		public event EventHandler<EventArgs> DataChanged;

		public static ScheduleSection GetSectionByType(Schedule parent)
		{
			switch (parent.SelectedSpotType)
			{
				case SpotType.Week:
					return new WeeklySection(parent);
				case SpotType.Month:
					return new MonthlySection(parent);
				default:
					return null;
			}
		}

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
			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			result.AppendLine(@"<ShowAverageRate>" + ShowAverageRate + @"</ShowAverageRate>");
			result.AppendLine(@"<ShowDiscount>" + ShowDiscount + @"</ShowDiscount>");
			result.AppendLine(@"<ShowNetRate>" + ShowNetRate + @"</ShowNetRate>");
			result.AppendLine(@"<ShowTotalCPP>" + ShowTotalCPP + @"</ShowTotalCPP>");
			result.AppendLine(@"<ShowTotalGRP>" + ShowTotalGRP + @"</ShowTotalGRP>");
			result.AppendLine(@"<ShowTotalPeriods>" + ShowTotalPeriods + @"</ShowTotalPeriods>");
			result.AppendLine(@"<ShowTotalRate>" + ShowTotalRate + @"</ShowTotalRate>");
			result.AppendLine(@"<ShowTotalSpots>" + ShowTotalSpots + @"</ShowTotalSpots>");
			result.AppendLine(@"<DigitalLegend>" + DigitalLegend.Serialize() + @"</DigitalLegend>");
			result.AppendLine(@"<ShowSelectedQuarter>" + ShowSelectedQuarter + @"</ShowSelectedQuarter>");
			if (SelectedQuarter.HasValue)
				result.AppendLine(@"<SelectedQuarter>" + SelectedQuarter.Value + @"</SelectedQuarter>");
			result.AppendLine(@"<OutputPerQuater>" + OutputPerQuater + @"</OutputPerQuater>");
			if (OutputMaxPeriods.HasValue)
				result.AppendLine(@"<OutputMaxPeriods>" + OutputMaxPeriods.Value + @"</OutputMaxPeriods>");
			result.AppendLine(@"<OutputNoBrackets>" + OutputNoBrackets + @"</OutputNoBrackets>");
			result.AppendLine(@"<UseDecimalRates>" + UseDecimalRates + @"</UseDecimalRates>");
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
					case "ShowLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLogo = tempBool;
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
					case "ShowSelectedQuarter":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSelectedQuarter = tempBool;
						break;
					case "SelectedQuarter":
						{
							DateTime temp;
							if (DateTime.TryParse(childNode.InnerText, out temp))
								SelectedQuarter = temp;
						}
						break;
					case "OutputPerQuater":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							OutputPerQuater = tempBool;
						break;
					case "OutputMaxPeriods":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								OutputMaxPeriods = temp;
						}
						break;
					case "OutputNoBrackets":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							OutputNoBrackets = tempBool;
						break;
					case "UseDecimalRates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							UseDecimalRates = tempBool;
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


			#region Generate Programs Table

			DataSource = new DataTable(ProgramDataTableName);
			var table = DataSource;


			var column = new DataColumn(ProgramIndexDataColumnName, typeof(int));
			table.Columns.Add(column);
			column = new DataColumn(ProgramLogoImageDataColumnName, typeof(Image));
			table.Columns.Add(column);
			column = new DataColumn(ProgramLogoSourceDataColumnName, typeof(string));
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

			if (Programs.Any())
			{
				var spotIndex = 0;
				var spots = Programs.First().Spots.ToList();
				var spotsCount = spots.Count;
				foreach (var spot in spots)
				{
					var columnName = ProgramSpotDataColumnNamePrefix + spotIndex;
					var tooltip = spot.FullColumnName;
					var isFullSpot = true;
					if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed && spotIndex == 0 && Parent.FlightDateStart != Parent.UserFlightDateStart)
					{
						isFullSpot = false;
						tooltip = String.Format("Partial Week Warning: {0}{2}The FIRST WEEK of your schedule is NOT a full 7 day week.{2}The Only Active Days in this week are {1}.", spot.Name, Parent.StartWeekDays, Environment.NewLine);
					}
					else if (MediaMetaData.Instance.ListManager.FlexFlightDatesAllowed && spotIndex == spotsCount - 1 && Parent.FlightDateEnd != Parent.UserFlightDateEnd)
					{
						isFullSpot = false;
						tooltip = String.Format("Partial Week Warning: {0}{2}The LAST WEEK of your schedule is NOT a full 7 day week.{2}The Only Active Days in this week are {1}.", spot.Name, Parent.EndWeekDays, Environment.NewLine);
					}
					column = new DataColumn(columnName, typeof(int)) { Caption = spot.ColumnName };
					totalSpotsExpression.Add(string.Format("ISNULL({0},0)", columnName));
					column.ExtendedProperties.Add("Tooltip", tooltip);
					column.ExtendedProperties.Add("SpotSettings", new object[] { spot.Quarter, spot.FullColumnName, isFullSpot });
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
			temp = string.Format("IIF((Sum({1})/{2})<>0,Sum({0})/(Sum({1})/{2}),0)", ProgramCostDataColumnName, ProgramGRPDataColumnName, (Parent.DemoType == DemoType.Rtg ? "1" : "1000"));
			column.Expression = temp;
			table.Columns.Add(column);

			#region Fill Programs Data

			foreach (var program in Programs)
			{
				var row = table.NewRow();
				row.BeginEdit();
				row[ProgramIndexDataColumnName] = program.Index.ToString();
				if (program.Logo != null && program.Logo.ContainsData)
				{
					row[ProgramLogoImageDataColumnName] = program.SmallLogo;
					row[ProgramLogoSourceDataColumnName] = program.Logo.Serialize();
				}
				else
				{
					row[ProgramLogoImageDataColumnName] = DBNull.Value;
					row[ProgramLogoSourceDataColumnName] = DBNull.Value;
				}
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
				for (var i = 0; i < program.Spots.Count; i++)
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
			#endregion
		}

		private void UpdateProgramsFromDataSource(DataRow row)
		{
			int tempInt;

			var index = -1;
			var temp = row[ProgramIndexDataColumnName] != DBNull.Value ? row[ProgramIndexDataColumnName].ToString() : string.Empty;
			if (int.TryParse(temp, out tempInt))
				index = tempInt;
			var program = Programs.FirstOrDefault(x => x.Index == index);
			if (program != null)
			{
				temp = row[ProgramLogoSourceDataColumnName] != DBNull.Value ? row[ProgramLogoSourceDataColumnName].ToString() : string.Empty;
				program.Logo = ImageSource.FromString(temp);
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
				double tempDouble;
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

		public void CloneProgram(int programIndex, bool fullClone)
		{
			if (programIndex < 0 || programIndex >= Programs.Count) return;
			var program = Programs[programIndex];
			Programs.Add(program.Clone(fullClone));
			RebuildProgramIndexes();
		}

		public void ChangeProgramPosition(int programIndex, int newIndex)
		{
			if (programIndex < 0 || programIndex >= Programs.Count) return;
			var program = Programs[programIndex];
			program.Index = newIndex + 0.5m;
			RebuildProgramIndexes();
		}

		private void RebuildProgramIndexes()
		{
			Programs.Sort((x, y) => x.Index.CompareTo(y.Index));
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
		public bool ShowLogo { get; set; }

		public bool ShowTotalPeriods { get; set; }
		public bool ShowTotalSpots { get; set; }
		public bool ShowTotalGRP { get; set; }
		public bool ShowTotalCPP { get; set; }
		public bool ShowAverageRate { get; set; }
		public bool ShowTotalRate { get; set; }
		public bool ShowNetRate { get; set; }
		public bool ShowDiscount { get; set; }

		public bool ShowSelectedQuarter { get; set; }
		public DateTime? SelectedQuarter { get; set; }
		public bool OutputPerQuater { get; set; }
		public int? OutputMaxPeriods { get; set; }
		public bool OutputNoBrackets { get; set; }
		public bool UseDecimalRates { get; set; }
		#endregion

		#region Calculated Properies
		public abstract int TotalPeriods { get; }

		public int TotalActivePeriods
		{
			get
			{
				var defaultprogram = Programs.FirstOrDefault();
				if (defaultprogram != null)
				{
					return ShowEmptySpots ? TotalPeriods : defaultprogram.SpotsNotEmpty.Length;
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

			ShowTime = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowTime;
			ShowDaypart = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowDaypart;
			ShowDay = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowDay;
			ShowStation = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowStation;
			ShowLenght = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowLenght;
			ShowRate = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowRate;
			ShowSpots = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowSpots;
			ShowCost = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowCost;
			ShowLogo = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowLogo;

			ShowTotalPeriods = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowTotalPeriods;
			ShowTotalSpots = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowTotalSpots;
			ShowAverageRate = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowAverageRate;
			ShowTotalRate = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowTotalRate;
			ShowNetRate = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowNetRate;
			ShowDiscount = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.ShowDiscount;

			OutputNoBrackets = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.OutputNoBrackets;
			UseDecimalRates = MediaMetaData.Instance.ListManager.DefaultWeeklyScheduleSettings.UseDecimalRates;
		}

		public override int TotalPeriods
		{
			get
			{
				var datesRange = Parent.FlightDateEnd - Parent.FlightDateStart;
				return datesRange.HasValue ? datesRange.Value.Days / 7 + 1 : 0;
			}
		}
	}

	public class MonthlySection : ScheduleSection
	{
		public MonthlySection(Schedule parent)
			: base(parent)
		{
			SpotType = SpotType.Month;

			ShowTime = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowTime;
			ShowDaypart = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowDaypart;
			ShowDay = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowDay;
			ShowStation = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowStation;
			ShowLenght = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowLenght;
			ShowRate = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowRate;
			ShowSpots = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowSpots;
			ShowCost = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowCost;

			ShowTotalPeriods = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowTotalPeriods;
			ShowTotalSpots = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowTotalSpots;
			ShowAverageRate = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowAverageRate;
			ShowTotalRate = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowTotalRate;
			ShowNetRate = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowNetRate;
			ShowDiscount = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.ShowDiscount;

			OutputNoBrackets = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.OutputNoBrackets;
			UseDecimalRates = MediaMetaData.Instance.ListManager.DefaultMonthlyScheduleSettings.UseDecimalRates;
		}

		public override int TotalPeriods
		{
			get
			{
				if (!Parent.FlightDateEnd.HasValue || !Parent.FlightDateStart.HasValue) return 0;
				return Math.Abs((Parent.FlightDateEnd.Value.Month - Parent.FlightDateStart.Value.Month) + 12 * (Parent.FlightDateEnd.Value.Year - Parent.FlightDateStart.Value.Year)) + 1;
			}
		}
	}

	public class Program : ISummaryProduct
	{
		private string _name;
		private string _day;

		#region Basic Properties

		public ScheduleSection Parent { get; set; }
		public Guid UniqueID { get; set; }
		public decimal Index { get; set; }
		public ImageSource Logo { get; set; }
		public string Station { get; set; }
		public string Daypart { get; set; }
		public string Time { get; set; }
		public string Length { get; set; }
		public double? Rate { get; set; }
		public double? Rating { get; set; }
		public List<Spot> Spots { get; set; }
		public CustomSummaryItem SummaryItem { get; private set; }

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

		public Image SmallLogo
		{
			get { return Logo != null ? Logo.TinyImage : null; }
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
			UniqueID = Guid.NewGuid();
			Index = Parent.Programs.Count + 1;
			Logo = MediaMetaData.Instance.ListManager.Images.Where(g => g.IsDefault).Select(g => g.Images.FirstOrDefault(i => i.IsDefault)).FirstOrDefault();
			Station = Parent.Parent.Stations.Count(x => x.Available) == 1 ? Parent.Parent.Stations.Where(x => x.Available).Select(x => x.Name).FirstOrDefault() : string.Empty;
			Daypart = string.Empty;
			Day = string.Empty;
			Time = string.Empty;
			Length = string.Empty;
			Spots = new List<Spot>();
			SummaryItem = new ProductSummaryItem(this);
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			if (!string.IsNullOrEmpty(_name))
			{
				result.Append(@"<Program ");
				result.Append("UniqueID = \"" + UniqueID + "\" ");
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
				if (Logo != null && Logo.ContainsData && !Logo.IsDefault)
					result.AppendLine(@"<Logo>" + Logo.Serialize() + @"</Logo>");
				result.AppendLine(@"<Spots>");
				foreach (Spot spot in Spots)
					result.AppendLine(@"<Spot>" + spot.Serialize() + @"</Spot>");
				result.AppendLine(@"</Spots>");
				result.AppendLine(@"<SummaryItem>" + SummaryItem.Serialize() + @"</SummaryItem>");
				result.AppendLine(@"</Program>");
			}
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			double tempDouble;
			Guid tempGuid;

			foreach (XmlAttribute programAttribute in node.Attributes)
				switch (programAttribute.Name)
				{
					case "Name":
						_name = programAttribute.Value;
						break;
					case "UniqueID":
						if (Guid.TryParse(programAttribute.Value, out tempGuid))
							UniqueID = tempGuid;
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
					case "SummaryItem":
						SummaryItem.Deserialize(childNode);
						break;
					case "Logo":
						Logo = new ImageSource();
						Logo.Deserialize(childNode);
						break;
				}
		}

		public void ApplyDefaultValues()
		{
			var source = MediaMetaData.Instance.ListManager.SourcePrograms.Where(x => x.Name.Equals(_name)).ToArray();
			if (source.Length <= 0) return;
			Daypart = source[0].Daypart;
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

		public Program Clone(bool fullClone)
		{
			var clone = new Program(Parent);
			clone.Name = Name;
			clone.Logo = Logo != null ? Logo.Clone() : null;
			clone.Station = Station;
			clone.Daypart = Daypart;
			clone.Day = Day;
			clone.Time = Time;
			clone.Length = Length;
			clone.Rate = Rate;
			clone.Rating = Rating;
			clone.Spots.AddRange(Spots.Select(s => s.Clone(clone, fullClone)));
			return clone;
		}

		public decimal SummaryOrder
		{
			get { return Index; }
		}

		public string SummaryTitle
		{
			get { return String.Format("{0}  -  {1}", Station, Name); }
		}

		public string SummaryInfo
		{
			get
			{
				var result = new List<string>();
				if (!String.IsNullOrEmpty(Daypart))
					result.Add(Daypart);
				if (!String.IsNullOrEmpty(Time))
					result.Add(Time);
				result.Add(String.Format("{0}x", Spots.Sum(sp => sp.Count)));
				return String.Join(", ", result);
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

		public string Name
		{
			get
			{
				switch (_parent.Parent.SpotType)
				{
					case SpotType.Month:
						return Date.ToString("MMMM");
					case SpotType.Week:
						return String.Format("{0} {1}", GetMonthAbbreviation(Date.Month), Date.Day.ToString("00"));
					default:
						return String.Empty;
				}
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
						return String.Empty;
				}
			}
		}

		public string FullColumnName
		{
			get
			{
				switch (_parent.Parent.SpotType)
				{
					case SpotType.Week:
						return String.Format("Week {0}", Name);
					default:
						return Name;
				}
			}
		}

		public TextGroup FormattedString
		{
			get
			{
				var textGroup = new TextGroup(", ", "[", "]");
				if (!Count.HasValue) return textGroup;

				var programNameGroup = new TextGroup("  -  ");
				programNameGroup.Items.Add(new TextItem(_parent.Station, true));
				programNameGroup.Items.Add(new TextItem(_parent.Name, false));
				textGroup.Items.Add(programNameGroup);

				if (!String.IsNullOrEmpty(_parent.Daypart))
					textGroup.Items.Add(new TextItem(_parent.Daypart, false));

				if (!String.IsNullOrEmpty(_parent.Time))
					textGroup.Items.Add(new TextItem(_parent.Time, false));

				textGroup.Items.Add(new TextItem(String.Format("{0}x {1}", Count.Value, _parent.Day), false));

				return textGroup;
			}
		}

		public Quarter Quarter
		{
			get { return _parent.Parent.Parent.Quarters.FirstOrDefault(q => q.DateStart <= Date && q.DateEnd >= Date); }
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

		public Spot Clone(Program parent, bool fullClone)
		{
			return new Spot(parent) { Date = Date, Count = fullClone ? Count : null };
		}
	}

	public abstract class MediaCalendar : Calendar.Calendar
	{
		protected readonly Schedule _parentSchedule;

		protected MediaCalendar(ISchedule parent)
			: base(parent)
		{
			_parentSchedule = parent as Schedule;
		}

		public override void UpdateDaysCollection()
		{
			if (_parentSchedule.MondayBased)
				UpdateDaysCollection<CalendarDayMondayBased>(_parentSchedule.StartDayOfWeek, _parentSchedule.EndDayOfWeek);
			else
				UpdateDaysCollection<CalendarDaySundayBased>(_parentSchedule.StartDayOfWeek, _parentSchedule.EndDayOfWeek);
		}

		public override IEnumerable<DateRange> CalculateDateRange(IEnumerable<DateTime> dates)
		{
			return CalculateDateRange(dates, _parentSchedule.StartDayOfWeek, _parentSchedule.EndDayOfWeek);
		}

		public override DateTime[][] GetDaysByWeek(DateTime start, DateTime end)
		{
			return GetDaysByWeek(start, end, _parentSchedule.EndDayOfWeek);
		}

		protected abstract void ApplyDefaultMonthSettings(CalendarMonth targetMonth);

		public override void UpdateMonthCollection()
		{
			if (!Schedule.FlightDateStart.HasValue || !Schedule.FlightDateEnd.HasValue)
			{
				Months.Clear();
				return;
			}
			var months = new List<CalendarMonth>();
			var startDate = Schedule.FlightDateStart.Value;
			var monthTemplates = _parentSchedule.MondayBased ? MediaMetaData.Instance.ListManager.MonthTemplatesMondayBased : MediaMetaData.Instance.ListManager.MonthTemplatesSundayBased;
			while (startDate <= Schedule.FlightDateEnd.Value)
			{
				var month = Months.FirstOrDefault(x => x.Date.Equals(startDate));
				if (month == null)
				{
					if (_parentSchedule.MondayBased)
						month = new CalendarMonthMediaMondayBased(this);
					else
						month = new CalendarMonthMediaSundayBased(this);
					ApplyDefaultMonthSettings(month);
				}
				var monthTemplate = monthTemplates.FirstOrDefault(mt => startDate >= mt.StartDate && startDate <= mt.EndDate);
				if (monthTemplate == null) continue;
				startDate = monthTemplate.Month.Value;
				month.Date = monthTemplate.Month.Value;
				month.DaysRangeBegin = monthTemplate.StartDate.Value;
				month.DaysRangeEnd = monthTemplate.EndDate.Value;
				month.Days.Clear();
				month.Days.AddRange(Days.Where(x => x.Date >= month.DaysRangeBegin && x.Date <= month.DaysRangeEnd));
				months.Add(month);
				startDate = startDate.AddMonths(1);
			}
			Months.Clear();
			Months.AddRange(months);
		}

		public override void Reset()
		{
			Days.Clear();
			Months.Clear();
			Notes.Clear();
			UpdateDaysCollection();
			UpdateMonthCollection();
			UpdateNotesCollection();
		}
	}

	public class BroadcastCalendar : MediaCalendar
	{
		public BroadcastCalendar(ISchedule parent) : base(parent) { }

		public override bool AllowCustomNotes
		{
			get { return false; }
		}

		public override void Deserialize(XmlNode node)
		{
			if (_parentSchedule.MondayBased)
				Deserialize<CalendarMonthMediaMondayBased, CalendarDayMondayBased, MediaDataNote>(node, _parentSchedule.StartDayOfWeek, _parentSchedule.EndDayOfWeek);
			else
				Deserialize<CalendarMonthMediaSundayBased, CalendarDaySundayBased, MediaDataNote>(node, _parentSchedule.StartDayOfWeek, _parentSchedule.EndDayOfWeek);
		}

		public override void UpdateNotesCollection()
		{
			const string noteSeparator = "   ";
			var notes = new List<MediaDataNote>();
			var scheduleSection = _parentSchedule.Section;
			if (Schedule.FlightDateStart.HasValue && Schedule.FlightDateEnd.HasValue)
			{
				notes.AddRange(scheduleSection.Programs.SelectMany(p => p.Spots)
				.Where(s => s.Count > 0 && s.StartDate.HasValue && s.EndDate.HasValue)
				.GroupBy(g => new { g.StartDate, g.EndDate })
				.Select(g => new MediaDataNote(this)
				{
					StartDay = g.Key.StartDate.Value,
					FinishDay = g.Key.EndDate.Value,
					MediaData = g.Select(sp => sp.FormattedString).Join(noteSeparator)
				}));
				bool needToSplit;
				notes.ForEach(n => n.Splitted = false);
				var splittedNotes = new List<MediaDataNote>(notes);
				do
				{
					needToSplit = false;
					foreach (var calendarNote in notes.OrderByDescending(n => n.Length))
					{
						if (calendarNote.Splitted) continue;
						var intersectedNote = splittedNotes.Where(sn => sn != calendarNote).OrderBy(n => n.Length).FirstOrDefault(mn =>
							(mn.StartDay >= calendarNote.StartDay && mn.StartDay <= calendarNote.FinishDay) ||
							(mn.FinishDay >= calendarNote.StartDay && mn.FinishDay <= calendarNote.FinishDay));
						if (intersectedNote == null) continue;
						needToSplit = true;
						if ((intersectedNote.StartDay >= calendarNote.StartDay && intersectedNote.StartDay <= calendarNote.FinishDay) &&
							(intersectedNote.FinishDay >= calendarNote.StartDay && intersectedNote.FinishDay <= calendarNote.FinishDay))
						{
							calendarNote.MediaData = new[] { calendarNote.MediaData, intersectedNote.MediaData }.Join(noteSeparator);
							splittedNotes.Remove(intersectedNote);
							intersectedNote.Splitted = true;
						}
						else if (intersectedNote.StartDay >= calendarNote.StartDay && intersectedNote.StartDay <= calendarNote.FinishDay)
						{
							splittedNotes.Add(new MediaDataNote(this)
							{
								StartDay = calendarNote.StartDay,
								FinishDay = intersectedNote.FinishDay,
								MediaData = new[] { calendarNote.MediaData, intersectedNote.MediaData }.Join(noteSeparator)
							});
							splittedNotes.Remove(calendarNote);
							splittedNotes.Remove(intersectedNote);
							intersectedNote.Splitted = true;
						}
						else if (intersectedNote.FinishDay >= calendarNote.StartDay && intersectedNote.FinishDay <= calendarNote.FinishDay)
						{
							splittedNotes.Add(new MediaDataNote(this)
							{
								StartDay = intersectedNote.StartDay,
								FinishDay = calendarNote.FinishDay,
								MediaData = new[] { calendarNote.MediaData, intersectedNote.MediaData }.Join(noteSeparator)
							});
							splittedNotes.Remove(calendarNote);
							splittedNotes.Remove(intersectedNote);
							intersectedNote.Splitted = true;
						}
					}
					notes.Clear();
					notes.AddRange(splittedNotes);
					notes.ForEach(n => n.Splitted = false);
				}
				while (needToSplit);

				foreach (var calendarNote in Notes.OfType<MediaDataNote>().Where(n => n.EditedByUser))
				{
					notes.Where(n => n.StartDay.Date == calendarNote.StartDay.Date && n.FinishDay.Date == calendarNote.FinishDay.Date).ToList().ForEach(n =>
					{
						n.Note = calendarNote.Note;
						n.BackgroundColor = calendarNote.BackgroundColor;
					});
				}
			}
			Notes.Clear();
			Notes.AddRange(notes);
			UpdateDayAndNoteLinks();
		}

		protected override void ApplyDefaultMonthSettings(CalendarMonth targetMonth)
		{
			targetMonth.OutputData.ShowLogo = MediaMetaData.Instance.ListManager.DefaultBroadcastCalendarSettings.ShowLogo;
			targetMonth.OutputData.ShowBigDate = MediaMetaData.Instance.ListManager.DefaultBroadcastCalendarSettings.ShowBigDate;
		}
	}

	public class CustomCalendar : MediaCalendar
	{
		public CustomCalendar(ISchedule parent) : base(parent) { }

		public override bool AllowCustomNotes
		{
			get { return true; }
		}

		public override void Deserialize(XmlNode node)
		{
			if (_parentSchedule.MondayBased)
				Deserialize<CalendarMonthMediaMondayBased, CalendarDayMondayBased, CommonCalendarNote>(node, _parentSchedule.StartDayOfWeek, _parentSchedule.EndDayOfWeek);
			else
				Deserialize<CalendarMonthMediaSundayBased, CalendarDaySundayBased, CommonCalendarNote>(node, _parentSchedule.StartDayOfWeek, _parentSchedule.EndDayOfWeek);
		}

		protected override void ApplyDefaultMonthSettings(CalendarMonth targetMonth)
		{
			targetMonth.OutputData.ShowLogo = MediaMetaData.Instance.ListManager.DefaultCustomCalendarSettings.ShowLogo;
			targetMonth.OutputData.ShowBigDate = MediaMetaData.Instance.ListManager.DefaultCustomCalendarSettings.ShowBigDate;
		}
	}

	public abstract class CalendarMonthMedia : CalendarMonth
	{
		protected CalendarMonthMedia(Calendar.Calendar parent)
			: base(parent)
		{
			OutputData = new MediaCalendarOutputData(this);
		}

		public override DateTime Date
		{
			get { return _date; }
			set { _date = value; }
		}
	}

	public class CalendarMonthMediaMondayBased : CalendarMonthMedia
	{
		public CalendarMonthMediaMondayBased(Calendar.Calendar parent) : base(parent) { }
	}

	public class CalendarMonthMediaSundayBased : CalendarMonthMedia
	{
		public CalendarMonthMediaSundayBased(Calendar.Calendar parent) : base(parent) { }
	}

	public class MediaCalendarOutputData : CalendarOutputData
	{
		public MediaCalendarOutputData(CalendarMonth parent)
			: base(parent)
		{
			ApplyForAllCustomComment = false;
			ShowLogo = false;
		}
	}

	public class MediaMonthTemplate
	{
		public DateTime? Month { get; set; }
		public DateTime? StartDate { get; set; }
		public DateTime? EndDate { get; set; }

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Month":
						{
							DateTime tempDateTime;
							if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
								Month = tempDateTime;
						}
						break;
					case "StartDate":
						{
							DateTime tempDateTime;
							if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
								StartDate = tempDateTime;
						}
						break;
					case "EndDate":
						{
							DateTime tempDateTime;
							if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
								EndDate = tempDateTime;
						}
						break;
				}
			}
		}
	}

	public class MediaDataNote : CalendarNote
	{
		public TextGroup MediaData { get; set; }
		public bool EditedByUser { get; private set; }
		public bool Splitted { get; set; }

		public override ITextItem Note
		{
			get { return _note ?? MediaData; }
			set
			{
				if (!MediaData.IsEqual(value))
					_note = value;
				EditedByUser = EditedByUser || _note != null;
			}
		}

		public override Color BackgroundColor
		{
			get { return _backgroundColor; }
			set
			{
				_backgroundColor = value;
				EditedByUser = EditedByUser || _backgroundColor != DefaultBackgroundColor;
			}
		}

		public MediaDataNote(BroadcastCalendar parent) : base(parent) { }

		public override string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(base.Serialize());
			result.AppendLine(@"<EditedByUser>" + EditedByUser + @"</EditedByUser>");
			return result.ToString();
		}

		public override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "EditedByUser":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								EditedByUser = temp;
						}
						break;
				}
			}
		}

		public void Reset()
		{
			_note = null;
			_backgroundColor = DefaultBackgroundColor;
			EditedByUser = false;
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

	public class MediaFullSummarySettings : CustomSummarySettings
	{
		private readonly RegularSchedule _parent;
		public bool IsDefaultSate { get; set; }

		public MediaFullSummarySettings(RegularSchedule parent)
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
			if (IsDefaultSate)
				UpdateItems();
		}

		public void UpdateItems()
		{
			if (!IsDefaultSate) return;
			if (Items.Count != 2) return;
			{
				var mediaSummaryItem = Items[0];
				mediaSummaryItem.ShowValue = true;
				mediaSummaryItem.Value = String.Format("Local {0} Campaign", MediaMetaData.Instance.DataTypeString);
				var description = new List<string>();
				description.Add(String.Format("Stations: {0}", String.Join(", ", _parent.Section.Programs.Select(p => p.Station).Distinct())));
				description.Add(String.Format("Dayparts: {0}", String.Join(", ", _parent.Section.Programs.Select(p => p.Daypart).Distinct())));
				description.Add(String.Format("Total Spots: {0}x", _parent.Section.Programs.Sum(p => p.Spots.Sum(sp => sp.Count))));
				if (_parent.Section.Programs.Any(p => p.Rate.HasValue))
					description.Add(String.Format("Avg Rate: {0}", _parent.Section.Programs.Where(p => p.Rate.HasValue).Average(p => p.Rate.Value).ToString("$#,##0")));
				mediaSummaryItem.Description = String.Join("  ", description);
				mediaSummaryItem.ShowDescription = true;
				mediaSummaryItem.ShowMonthly = false;
				mediaSummaryItem.Monthly = null;
				mediaSummaryItem.ShowTotal = false;
				mediaSummaryItem.Total = null;
			}
			{
				var digitalSummaryItem = Items[1];
				digitalSummaryItem.ShowValue = true;
				digitalSummaryItem.Value = "Digital Campaign";
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