using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.Interop;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.Core.QuickShare
{
	public class PackageManager
	{
		private Package _currentPackage;

		public bool PackageLoaded { get; set; }

		public event EventHandler<Common.ScheduleSaveEventArgs> SettingsSaved;

		public string CurrentAdvertiser
		{
			get { return _currentPackage != null ? _currentPackage.BusinessName : null; }
		}

		public void OpenPackage(string packageName, bool create)
		{
			var packageFilePath = GetPackageFileName(packageName);
			if (create && File.Exists(packageFilePath))
				if (Utilities.Instance.ShowWarningQuestion(String.Format("An older Package is already saved with this same file name.\nDo you want to replace this file with a newer package?")) == DialogResult.Yes)
					File.Delete(packageFilePath);
			_currentPackage = new Package(packageFilePath);
			PackageLoaded = true;
		}

		public void OpenPackage(string packageFilePath)
		{
			_currentPackage = new Package(packageFilePath);
			PackageLoaded = true;
		}

		public string GetPackageFileName(string packageName)
		{
			return Path.Combine(MediaMetaData.Instance.SettingsManager.SaveFolder, packageName + ".xml");
		}

		public Package GetLocalPackage()
		{
			return new Package(_currentPackage.PackageFile.FullName);
		}

		public ShortPackage GetShortPackage()
		{
			return _currentPackage != null ? new ShortPackage(_currentPackage.PackageFile) : null;
		}

		public void SavePackage(Package localPackage, bool quickSave, Control sender)
		{
			localPackage.Save();
			_currentPackage = localPackage;
			if (SettingsSaved != null)
				SettingsSaved(sender, new Common.ScheduleSaveEventArgs(quickSave));
		}

		public static ShortPackage[] GetShortPackageList()
		{
			var saveFolder = new DirectoryInfo(MediaMetaData.Instance.SettingsManager.SaveFolder);
			if (saveFolder.Exists)
				return GetShortPackageList(saveFolder);
			return null;
		}

		public static ShortPackage[] GetShortPackageList(DirectoryInfo rootFolder)
		{
			var packages = rootFolder.GetFiles("*.xml").Select(file => new ShortPackage(file)).ToList();
			packages.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.ShortFileName, y.ShortFileName));
			return packages.ToArray();
		}

		public void RemoveInstance()
		{
			SettingsSaved = null;
		}
	}

	public class ShortPackage
	{
		private readonly FileInfo _packageFile;

		public ShortPackage(FileInfo file)
		{
			BusinessName = string.Empty;
			Status = MediaMetaData.Instance.ListManager.Statuses.FirstOrDefault();
			_packageFile = file;
			Load();
		}

		public string BusinessName { get; set; }
		public string Status { get; set; }

		public string ShortFileName
		{
			get { return _packageFile.Name.Replace(_packageFile.Extension, ""); }
		}

		public string FullFileName
		{
			get { return _packageFile.FullName; }
		}

		public DateTime LastModifiedDate
		{
			get { return _packageFile.LastWriteTime; }
		}

		private void Load()
		{
			if (!_packageFile.Exists) return;
			var document = new XmlDocument();
			document.Load(_packageFile.FullName);

			var node = document.SelectSingleNode(@"/Package/BusinessName");
			if (node != null)
				BusinessName = node.InnerText;

			node = document.SelectSingleNode(@"/Package/Status");
			if (node != null)
				Status = node.InnerText;
		}

		public void Save()
		{
			if (!_packageFile.Exists) return;
			try
			{
				var document = new XmlDocument();
				document.Load(_packageFile.FullName);

				var node = document.SelectSingleNode(@"/Package/Status");
				if (node != null)
					node.InnerText = Status;
				else
				{
					node = document.SelectSingleNode(@"/Package");
					if (node != null)
						node.InnerXml += (@"<Status>" + (Status != null ? Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
				}
				document.Save(_packageFile.FullName);
			}
			catch
			{
			}
		}
	}

	public class Package
	{
		public Package(string fileName)
		{
			BusinessName = string.Empty;
			DecisionMaker = string.Empty;
			ClientType = MediaMetaData.Instance.ListManager.ClientTypes.FirstOrDefault();
			AccountNumber = string.Empty;
			Status = MediaMetaData.Instance.ListManager.Statuses.FirstOrDefault();
			PresentationDate = DateTime.Now;
			MondayBased = true;

			Schedules = new List<PackageSchedule>();
			Schedules.Add(new PackageSchedule(this));

			_packageFile = new FileInfo(fileName);
			if (!File.Exists(fileName))
			{
				var xml = new StringBuilder();
				xml.AppendLine(@"<Package>");
				xml.AppendLine(@"<Status>" + (Status != null ? Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
				xml.AppendLine(@"</Package>");
				using (var sw = new StreamWriter(_packageFile.FullName, false))
				{
					sw.Write(xml);
					sw.Flush();
				}
				_packageFile = new FileInfo(fileName);
			}
			else
				Load();
		}

		private FileInfo _packageFile { get; set; }
		public bool IsNew { get; set; }
		public string BusinessName { get; set; }
		public string DecisionMaker { get; set; }
		public string ClientType { get; set; }
		public string AccountNumber { get; set; }
		public string Status { get; set; }
		public DateTime? PresentationDate { get; set; }
		public bool MondayBased { get; private set; }

		public List<PackageSchedule> Schedules { get; private set; }

		public string Name
		{
			get { return _packageFile.Name.Replace(_packageFile.Extension, ""); }
			set { _packageFile = new FileInfo(Path.Combine(_packageFile.Directory.FullName, value + ".xml")); }
		}

		public FileInfo PackageFile
		{
			get { return _packageFile; }
		}

		public DayOfWeek StartDayOfWeek
		{
			get { return MondayBased ? DayOfWeek.Monday : DayOfWeek.Sunday; }
		}

		public DayOfWeek EndDayOfWeek
		{
			get { return MondayBased ? DayOfWeek.Sunday : DayOfWeek.Saturday; }
		}

		private void Load()
		{
			if (!_packageFile.Exists) return;
			var document = new XmlDocument();
			document.Load(_packageFile.FullName);
			var node = document.SelectSingleNode(@"/Package/BusinessName");
			if (node != null)
				BusinessName = node.InnerText;

			node = document.SelectSingleNode(@"/Package/DecisionMaker");
			if (node != null)
				DecisionMaker = node.InnerText;

			node = document.SelectSingleNode(@"/Package/ClientType");
			if (node != null)
				ClientType = node.InnerText;

			node = document.SelectSingleNode(@"/Package/AccountNumber");
			if (node != null)
				AccountNumber = node.InnerText;

			node = document.SelectSingleNode(@"/Package/Status");
			if (node != null)
				Status = node.InnerText;

			node = document.SelectSingleNode(@"/Package/PresentationDate");
			DateTime tempDateTime;
			if (node != null)
				if (DateTime.TryParse(node.InnerText, out tempDateTime))
					PresentationDate = tempDateTime;

			node = document.SelectSingleNode(@"/Package/MondayBased");
			if (node != null)
			{
				bool temp;
				if (Boolean.TryParse(node.InnerText, out temp))
					MondayBased = temp;
			}

			var scheduleNodes = document.SelectNodes(@"/Package/Schedule").OfType<XmlNode>().ToList();
			if (scheduleNodes.Any())
			{
				Schedules.Clear();
				foreach (var scheduleNode in scheduleNodes)
					Schedules.Add(new PackageSchedule(this, scheduleNode));
			}
		}

		public void Save()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<Package>");
			xml.AppendLine(@"<BusinessName>" + BusinessName.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BusinessName>");
			if (!ListManager.Instance.Advertisers.Contains(BusinessName))
			{
				ListManager.Instance.Advertisers.Add(BusinessName);
				ListManager.Instance.SaveAdvertisers();
			}
			xml.AppendLine(@"<DecisionMaker>" + DecisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
			if (!ListManager.Instance.DecisionMakers.Contains(DecisionMaker))
			{
				ListManager.Instance.DecisionMakers.Add(DecisionMaker);
				ListManager.Instance.SaveDecisionMakers();
			}
			xml.AppendLine(@"<Status>" + (Status != null ? Status.Replace(@"&", "&#38;").Replace("\"", "&quot;") : string.Empty) + @"</Status>");
			xml.AppendLine(@"<ClientType>" + ClientType.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</ClientType>");
			xml.AppendLine(@"<AccountNumber>" + AccountNumber.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</AccountNumber>");
			if (PresentationDate.HasValue)
				xml.AppendLine(@"<PresentationDate>" + PresentationDate.Value + @"</PresentationDate>");
			xml.AppendLine(@"<MondayBased>" + MondayBased + @"</MondayBased>");
			foreach (var packageSchedule in Schedules)
			{
				packageSchedule.UpdateParentProperties();
				xml.AppendLine(String.Format(@"<Schedule>{0}</Schedule>", packageSchedule.Serialize()));
			}
			xml.AppendLine(@"</Package>");

			using (var sw = new StreamWriter(_packageFile.FullName, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		public void AddSchedule()
		{
			Schedules.Add(new PackageSchedule(this) { Index = Schedules.Count });
			RebuildScheduleIndexes();
		}

		public void DeleteSchedule(PackageSchedule targetSchedule)
		{
			if (targetSchedule == null) return;
			Schedules.Remove(targetSchedule);
			RebuildScheduleIndexes();
		}

		public void CloneSchedule(PackageSchedule targetSchedule)
		{
			if (targetSchedule == null) return;
			RebuildScheduleIndexes();
		}

		public void ChangeSchedulePosition(PackageSchedule targetSchedule, int newIndex)
		{
			if (targetSchedule == null) return;
			targetSchedule.Index = newIndex + 0.5m;
			RebuildScheduleIndexes();
		}

		private void RebuildScheduleIndexes()
		{
			Schedules.Sort((x, y) => x.Index.CompareTo(y.Index));
			for (int i = 0; i < Schedules.Count; i++)
				Schedules[i].Index = i;
		}
	}

	public sealed class PackageSchedule : Schedule
	{
		public Package Parent { get; private set; }
		public Guid Id { get; private set; }
		public override string Name { get; set; }
		public decimal Index { get; set; }

		#region Calculated properties
		public bool IsConfigured
		{
			get { return !String.IsNullOrEmpty(Name) && UserFlightDateStart.HasValue && UserFlightDateEnd.HasValue; }
		}

		public decimal DisplayedIndex
		{
			get { return Index + 1; }
		}

		public string TotalPeriods
		{
			get
			{
				if (Section == null) return null;
				if (Section.TotalPeriods == 0) return null;
				return String.Format("{0} {1}{2}", Section.TotalPeriods, SelectedSpotType, Section.TotalPeriods > 1 ? "s" : String.Empty);
			}
		}
		#endregion

		public PackageSchedule(Package parent, XmlNode node = null)
		{
			Parent = parent;
			Id = Guid.NewGuid();
			UpdateParentProperties();
			Deserialize(node);
			LoadQuarters();

			Dayparts.AddRange(MediaMetaData.Instance.ListManager.Dayparts.Where(x => !Dayparts.Select(y => y.Name).Contains(x.Name)));
			Stations.AddRange(MediaMetaData.Instance.ListManager.Stations.Where(x => !Stations.Select(y => y.Name).Contains(x.Name)));
		}

		public override void Deserialize(XmlNode rootNode)
		{
			if (rootNode == null) return;
			base.Deserialize(rootNode);
			var node = rootNode.SelectSingleNode(@"Id");
			{
				Guid temp;
				if (node != null && Guid.TryParse(node.InnerText, out temp))
					Id = temp;
			}
			node = rootNode.SelectSingleNode(@"Name");
			if (node != null)
				Name = node.InnerText;
			node = rootNode.SelectSingleNode(@"Index");
			if (node != null)
			{
				decimal temp;
				if (Decimal.TryParse(node.InnerText, out temp))
					Index = temp;
			}
		}

		public override StringBuilder Serialize()
		{
			var result = base.Serialize();
			result.AppendLine(@"<Id>" + Id + @"</Id>");
			if (!String.IsNullOrEmpty(Name))
				result.AppendLine(@"<Name>" + Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Name>");
			result.AppendLine(@"<Index>" + Index + @"</Index>");
			return result;
		}

		public void UpdateParentProperties()
		{
			BusinessName = Parent.BusinessName;
			DecisionMaker = Parent.DecisionMaker;
			ClientType = Parent.ClientType;
			AccountNumber = Parent.AccountNumber;
			Status = Parent.Status;
			PresentationDate = Parent.PresentationDate;
		}
	}
}
