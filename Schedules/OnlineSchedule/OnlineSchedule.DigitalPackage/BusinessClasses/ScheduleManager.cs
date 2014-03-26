using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.OnlineSchedule;

namespace NewBizWiz.OnlineSchedule.DigitalPackage.BusinessClasses
{
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
			SettingsManager.Instance.LastOpenSchedule = _currentSchedule.Name;
			SettingsManager.Instance.SaveSettings();
			ScheduleLoaded = true;
		}

		public void CreateSchedule(string scheduleName)
		{
			string sheduleFilePath = !String.IsNullOrEmpty(scheduleName) ? GetScheduleFileName(scheduleName) : null;
			OpenSchedule(sheduleFilePath);
		}

		public string GetScheduleFileName(string scheduleName)
		{
			return Path.Combine(SettingsManager.Instance.SaveFolder, scheduleName + ".xml");
		}

		public Schedule GetLocalSchedule()
		{
			return new Schedule(_currentSchedule.ScheduleFile != null ? _currentSchedule.ScheduleFile.FullName : String.Empty);
		}

		public ShortSchedule GetShortSchedule()
		{
			return _currentSchedule != null ? new ShortSchedule(_currentSchedule.ScheduleFile) : null;
		}

		public void SaveSchedule(Schedule localSchedule, bool quickSave, Control sender)
		{
			if (localSchedule.IsNew) return;
			localSchedule.Save();
			_currentSchedule = localSchedule;
			SettingsManager.Instance.LastOpenSchedule = _currentSchedule.Name;
			SettingsManager.Instance.SaveSettings();
			if (SettingsSaved != null)
				SettingsSaved(sender, new SavingingEventArgs(quickSave));
		}

		public static ShortSchedule[] GetShortScheduleList(DirectoryInfo rootFolder)
		{
			return rootFolder.GetFiles("*.xml").Select(file => new ShortSchedule(file)).ToArray();
		}

		public static IEnumerable<ShortSchedule> GetShortScheduleList()
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
			_scheduleFile = file;
		}

		public string ShortFileName
		{
			get { return _scheduleFile != null ? _scheduleFile.Name.Replace(_scheduleFile.Extension, "") : String.Empty; }
		}

		public string FullFileName
		{
			get { return _scheduleFile != null ? _scheduleFile.FullName : String.Empty; }
		}

		public DateTime LastModifiedDate
		{
			get { return _scheduleFile != null ? _scheduleFile.LastWriteTime : DateTime.MinValue; }
		}
	}

	public class Schedule : IDigitalSchedule
	{
		public Schedule(string fileName)
		{
			IsNew = true;
			DigitalProducts = new List<DigitalProduct>();
			ViewSettings = new ScheduleBuilderViewSettings();
			if (!String.IsNullOrEmpty(fileName))
				_scheduleFile = new FileInfo(fileName);
			Load();
		}

		private FileInfo _scheduleFile { get; set; }
		public bool IsNew { get; set; }
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
			get { return _scheduleFile != null ? _scheduleFile.Name.Replace(_scheduleFile.Extension, "") : String.Empty; }
			set { _scheduleFile = new FileInfo(Path.Combine(SettingsManager.Instance.SaveFolder, value + ".xml")); }
		}

		public FileInfo ScheduleFile
		{
			get { return _scheduleFile; }
		}

		public decimal MonthlyInvestment
		{
			get { return DigitalProducts.Select(x => (x.MonthlyInvestment.HasValue ? x.MonthlyInvestment.Value : 0)).Sum(); }
		}

		public decimal MonthlyImpressions
		{
			get { return DigitalProducts.Select(x => (x.MonthlyImpressions.HasValue ? x.MonthlyImpressions.Value : 0)).Sum(); }
		}

		public decimal TotalInvestment
		{
			get { return DigitalProducts.Select(x => (x.TotalInvestment.HasValue ? x.TotalInvestment.Value : 0)).Sum(); }
		}

		public decimal TotalImpressions
		{
			get { return DigitalProducts.Select(x => (x.TotalImpressions.HasValue ? x.TotalImpressions.Value : 0)).Sum(); }
		}

		#region ISchedule Members
		public string BusinessName { get; set; }
		public string DecisionMaker { get; set; }
		public string AccountNumber { get; set; }
		public DateTime? PresentationDate { get; set; }
		public string FlightDates
		{
			get
			{
				if (FlightDateStart.HasValue && FlightDateEnd.HasValue)
					return FlightDateStart.Value.ToString("MM/dd/yy") + " - " + FlightDateEnd.Value.ToString("MM/dd/yy");
				return string.Empty;
			}
		}
		public DateTime? FlightDateStart { get; set; }
		public DateTime? FlightDateEnd { get; set; }
		#endregion

		private void Load()
		{
			if (_scheduleFile == null || !_scheduleFile.Exists) return;
			IsNew = false;

			var document = new XmlDocument();
			document.Load(_scheduleFile.FullName);

			var node = document.SelectSingleNode(@"/Schedule/BusinessName");
			if (node != null)
				BusinessName = node.InnerText;

			node = document.SelectSingleNode(@"/Schedule/DecisionMaker");
			if (node != null)
				DecisionMaker = node.InnerText;

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
				bool tempBool;
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
		}

		public void Save()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<Schedule>");

			if (!String.IsNullOrEmpty(BusinessName))
			{
				xml.AppendLine(@"<BusinessName>" + BusinessName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BusinessName>");
				if (!Core.Common.ListManager.Instance.Advertisers.Contains(BusinessName))
				{
					Core.Common.ListManager.Instance.Advertisers.Add(BusinessName);
					Core.Common.ListManager.Instance.SaveAdvertisers();
				}
			}

			if (!String.IsNullOrEmpty(DecisionMaker))
			{
				xml.AppendLine(@"<DecisionMaker>" + DecisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
				if (!Core.Common.ListManager.Instance.DecisionMakers.Contains(DecisionMaker))
				{
					Core.Common.ListManager.Instance.DecisionMakers.Add(DecisionMaker);
					Core.Common.ListManager.Instance.SaveDecisionMakers();
				}
			}
			xml.AppendLine(@"<PresentationDate>" + PresentationDate + @"</PresentationDate>");
			xml.AppendLine(@"<FlightDateStart>" + FlightDateStart + @"</FlightDateStart>");
			xml.AppendLine(@"<FlightDateEnd>" + FlightDateEnd + @"</FlightDateEnd>");
			xml.AppendLine(@"<ApplySettingsForeAllProducts>" + ApplySettingsForeAllProducts.ToString() + @"</ApplySettingsForeAllProducts>");

			xml.AppendLine(@"<ViewSettings>" + ViewSettings.Serialize() + @"</ViewSettings>");

			xml.AppendLine(@"<Products>");
			foreach (DigitalProduct product in DigitalProducts)
			{
				xml.AppendLine(product.Serialize());
			}
			xml.AppendLine(@"</Products>");
			xml.AppendLine(@"</Schedule>");
			if (_scheduleFile != null)
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
			throw new NotImplementedException();
		}

		public void DownDigital(int position)
		{
			throw new NotImplementedException();
		}

		public void RebuildDigitalProductIndexes()
		{
			for (int i = 0; i < DigitalProducts.Count; i++)
				DigitalProducts[i].Index = i + 1;
		}
	}
}