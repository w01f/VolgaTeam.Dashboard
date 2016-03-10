using System.Collections.Generic;
using System.IO;
using System.Xml;
using Asa.Legacy.Common.Entities.Digital;
using Asa.Legacy.Media.Entities.Calendar;
using Asa.Legacy.Media.Entities.Options;
using Asa.Legacy.Media.Entities.Section;
using Asa.Legacy.Media.Entities.Snapshot;

namespace Asa.Legacy.Media.Entities.Schedule
{
	public class RegularSchedule : Schedule
	{
		private FileInfo _scheduleFile { get; set; }

		public ProgramSchedule ProgramSchedule { get; private set; }

		public List<DigitalProduct> DigitalProducts { get; private set; }
		public DigitalProductSummary DigitalProductSummary { get; private set; }

		public BroadcastCalendar BroadcastCalendar { get; set; }
		public CustomCalendar CustomCalendar { get; set; }

		public List<Snapshot.Snapshot> Snapshots { get; private set; }
		public SnapshotSummary SnapshotSummary { get; private set; }
		public List<OptionSet> Options { get; private set; }
		public OptionSummary OptionsSummary { get; private set; }

		public override string Name
		{
			get { return _scheduleFile.Name.Replace(_scheduleFile.Extension, ""); }
			set { _scheduleFile = new FileInfo(Path.Combine(_scheduleFile.Directory.FullName, value + ".xml")); }
		}

		public RegularSchedule(string fileName)
		{
			InitProgramSchedule();

			DigitalProducts = new List<DigitalProduct>();
			DigitalProductSummary = new DigitalProductSummary();

			Snapshots = new List<Snapshot.Snapshot>();
			SnapshotSummary = new SnapshotSummary(this);
			Options = new List<OptionSet>();
			OptionsSummary = new OptionSummary();

			_scheduleFile = new FileInfo(fileName);
			Load();

			LoadCalendars();
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

			node = rootNode.SelectSingleNode(@"WeeklySection");
			if (node != null && SelectedSpotType == SpotType.Week)
			{
				InitProgramSchedule();
				ProgramSchedule.DeserializeSection(node);
			}

			node = rootNode.SelectSingleNode(@"MonthlySection");
			if (node != null && SelectedSpotType == SpotType.Month)
			{
				InitProgramSchedule();
				ProgramSchedule.DeserializeSection(node);
			}

			node = rootNode.SelectSingleNode(@"DigitalProducts");
			if (node != null)
			{
				foreach (XmlNode productNode in node.ChildNodes)
				{
					var product = new DigitalProduct();
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
					var snapshot = new Snapshot.Snapshot(this);
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
		}

		public void InitProgramSchedule()
		{
			ProgramSchedule = ProgramSchedule.Create(this);
		}

		private void Load()
		{
			if (!_scheduleFile.Exists) return;
			var document = new XmlDocument();
			document.Load(_scheduleFile.FullName);
			Deserialize(document.SelectSingleNode(@"/Schedule"));
		}

		protected void LoadCalendars()
		{
			BroadcastCalendar = new BroadcastCalendar();
			CustomCalendar = new CustomCalendar();

			if (!_scheduleFile.Exists) return;
			var document = new XmlDocument();
			document.Load(_scheduleFile.FullName);

			var node = document.SelectSingleNode(@"/Schedule/BroadcastCalendar");
			if (node != null)
				BroadcastCalendar.Deserialize(node);

			node = document.SelectSingleNode(@"/Schedule/CustomCalendar");
			if (node != null)
				CustomCalendar.Deserialize(node);
		}
	}
}
