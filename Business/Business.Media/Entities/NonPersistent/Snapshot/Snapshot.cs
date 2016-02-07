﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Media.Configuration;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Interfaces;
using Asa.Common.Core.Objects.Images;
using Asa.Common.Core.Objects.Output;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Snapshot
{
	public class Snapshot : IJsonCloneable<Snapshot>
	{
		public SnapshotContent Parent { get; private set; }
		public Guid UniqueID { get; set; }
		public double Index { get; set; }
		public string Name { get; set; }
		public ImageSource Logo { get; set; }
		public string Comment { get; set; }

		public List<SnapshotProgram> Programs { get; private set; }
		public List<DateRange> ActiveWeeks { get; private set; }

		#region Options
		public bool ShowLineId { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowStation { get; set; }
		public bool ShowProgram { get; set; }
		public bool ShowDaypart { get; set; }
		public bool ShowLenght { get; set; }
		public bool ShowTime { get; set; }
		public bool ShowRate { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowSpotsX { get; set; }
		public bool ShowTotalRow { get; set; }
		public bool UseDecimalRates { get; set; }
		public bool ShowSpotsPerWeek { get; set; }

		public bool ShowTotalSpots { get; set; }
		public bool ShowAverageRate { get; set; }
		#endregion

		public ContractSettings ContractSettings { get; private set; }

		#region Calculated Properies
		public int DisplayIndex
		{
			get { return (Int32)(Index + 1); }
		}

		public Image SmallLogo
		{
			get { return Logo != null ? Logo.TinyImage : null; }
		}

		public decimal AvgRate
		{
			get { return TotalSpots != 0 ? (TotalCost / TotalSpots) : 0; }
		}

		public decimal TotalCost
		{
			get { return Programs.Any() ? (Programs.Select(x => x.TotalCost).Sum()) : 0; }
		}


		private int? _totalWeeks;
		[JsonIgnore]
		public int TotalWeeks
		{
			get
			{
				if (_totalWeeks.HasValue) return _totalWeeks.Value;
				if (ActiveWeeks.Any()) return ActiveWeeks.Count;
				return Parent.ScheduleSettings.GetWeeks().Count();
			}
			set { _totalWeeks = value; }
		}

		public decimal TotalWeekCost
		{
			get { return TotalCost * (decimal)TotalWeeks; }
		}

		public int TotalSpots
		{
			get { return Programs.Any() ? Programs.Select(x => x.TotalSpots).Sum() : 0; }
		}

		public int TotalWeekSpots
		{
			get { return (int)(TotalSpots * TotalWeeks); }
		}
		#endregion

		[JsonConstructor]
		private Snapshot() { }

		public Snapshot(SnapshotContent parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Index = parent.Snapshots.Any() ? parent.Snapshots.Max(s => s.Index) + 1 : 0;
			Logo = MediaMetaData.Instance.ListManager.Images.Where(g => g.IsDefault).Select(g => g.Images.FirstOrDefault(i => i.IsDefault)).FirstOrDefault();
			Programs = new List<SnapshotProgram>();
			ActiveWeeks = new List<DateRange>();

			#region Options
			ShowLogo = MediaMetaData.Instance.ListManager.DefaultSnapshotSettings.ShowLogo;
			ShowStation = MediaMetaData.Instance.ListManager.DefaultSnapshotSettings.ShowStation;
			ShowProgram = MediaMetaData.Instance.ListManager.DefaultSnapshotSettings.ShowProgram;
			ShowDaypart = MediaMetaData.Instance.ListManager.DefaultSnapshotSettings.ShowDaypart;
			ShowLenght = MediaMetaData.Instance.ListManager.DefaultSnapshotSettings.ShowLenght;
			ShowTime = MediaMetaData.Instance.ListManager.DefaultSnapshotSettings.ShowTime;
			ShowRate = MediaMetaData.Instance.ListManager.DefaultSnapshotSettings.ShowRate;
			ShowCost = MediaMetaData.Instance.ListManager.DefaultSnapshotSettings.ShowCost;
			ShowTotalSpots = MediaMetaData.Instance.ListManager.DefaultSnapshotSettings.ShowTotalSpots;
			ShowAverageRate = MediaMetaData.Instance.ListManager.DefaultSnapshotSettings.ShowAverageRate;
			ShowSpotsX = MediaMetaData.Instance.ListManager.DefaultSnapshotSettings.ShowSpotsX;
			ShowLineId = MediaMetaData.Instance.ListManager.DefaultSnapshotSettings.ShowLineId;
			ShowTotalRow = MediaMetaData.Instance.ListManager.DefaultSnapshotSettings.ShowTotalRow;
			UseDecimalRates = MediaMetaData.Instance.ListManager.DefaultSnapshotSettings.UseDecimalRates;
			ShowSpotsPerWeek = MediaMetaData.Instance.ListManager.DefaultSnapshotSettings.ShowSpotsPerWeek;
			#endregion

			ContractSettings = new ContractSettings();
		}

		public void Dispose()
		{
			Programs.ForEach(p=>p.Dispose());
			Programs.Clear();

			ActiveWeeks.Clear();

			Logo.Dispose();
			Logo = null;

			ContractSettings = null;

			Parent = null;
		}

		public void AfterClone(Snapshot source, bool fullClone = true)
		{
			Parent = source.Parent;
			UniqueID = Guid.NewGuid();
			foreach (var snapshotProgram in Programs)
			{
				snapshotProgram.AfterClone(source.Programs.First(sourceProgram => sourceProgram.UniqueID == snapshotProgram.UniqueID));
				snapshotProgram.Parent = this;
			}
		}

		public void AddProgram()
		{
			var program = new SnapshotProgram(this);
			Programs.Add(program);
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
			Programs.Add(program.Clone<SnapshotProgram, SnapshotProgram>(fullClone));
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

		public void UpdateLogo()
		{
			if (Logo != null && !Logo.IsDefault) return;
			var defaultProgram = Programs.FirstOrDefault();
			if (defaultProgram == null || defaultProgram.SmallLogo == null) return;
			if (!Programs.All(p => p.Logo != null && p.SmallLogo.Compare(defaultProgram.SmallLogo))) return;
			Logo = defaultProgram.Logo.Clone<ImageSource, ImageSource>();
		}
	}
}
