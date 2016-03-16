using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Asa.Business.Media.Configuration;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Interfaces;
using Asa.Common.Core.Objects.Images;
using Asa.Common.Core.Objects.Output;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Option
{
	public class OptionSet : IJsonCloneable<OptionSet>
	{
		public const int DefaultPositionStation = 0;
		public const int DefaultPositionProgram = 1;
		public const int DefaultPositionDay = 2;
		public const int DefaultPositionTime = 3;
		public const int DefaultPositionLenght = 4;
		public const int DefaultPositionSpots = 5;
		public const int DefaultPositionRate = 6;
		public const int DefaultPositionCost = 7;

		public OptionsContent Parent { get; private set; }
		public Guid UniqueID { get; set; }
		public double Index { get; set; }
		public string Name { get; set; }
		public ImageSource Logo { get; set; }
		public string Comment { get; set; }
		public int? TotalPeriods { get; set; }
		public List<OptionProgram> Programs { get; private set; }

		public ContractSettings ContractSettings { get; private set; }

		#region Options
		public bool ShowLineId { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowStation { get; set; }
		public bool ShowProgram { get; set; }
		public bool ShowDay { get; set; }
		public bool ShowTime { get; set; }
		public bool ShowRate { get; set; }
		public bool ShowLenght { get; set; }
		public bool ShowSpots { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowSpotsX { get; set; }
		public bool UseDecimalRates { get; set; }
		public bool CloneLineToTheEnd { get; set; }

		public bool DefaultColumnPositions { get; set; }
		public int PositionStation { get; set; }
		public int PositionProgram { get; set; }
		public int PositionDay { get; set; }
		public int PositionTime { get; set; }
		public int PositionLenght { get; set; }
		public int PositionSpots { get; set; }
		public int PositionRate { get; set; }
		public int PositionCost { get; set; }

		public bool ShowTotalSpots { get; set; }
		public bool ShowTotalCost { get; set; }
		public bool ShowAverageRate { get; set; }

		public SpotType SpotType { get; set; }
		#endregion

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
			get { return Programs.Any() ? (Programs.Select(x => x.Cost ?? 0).Sum()) : 0; }
		}

		public decimal TotalPeriodCost
		{
			get { return TotalCost * (decimal)TotalPeriods; }
		}

		public int TotalSpots
		{
			get { return Programs.Any() ? Programs.Select(x => x.Spot ?? 0).Sum() : 0; }
		}

		public int TotalPeriodSpots
		{
			get { return (int)(TotalSpots * TotalPeriods); }
		}
		#endregion

		[JsonConstructor]
		private OptionSet() { }

		public OptionSet(OptionsContent parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Index = parent.Options.Any() ? parent.Options.Max(s => s.Index) + 1 : 0;
			Logo = MediaMetaData.Instance.ListManager.Images.Where(g => g.IsDefault).Select(g => g.Images.FirstOrDefault(i => i.IsDefault)).FirstOrDefault()?.Clone<ImageSource, ImageSource>();
			TotalPeriods = 1;
			Programs = new List<OptionProgram>();
			ContractSettings = new ContractSettings();

			#region Options
			ShowLineId = MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowLineId;
			ShowLogo = MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowLogo;
			ShowStation = MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowStation;
			ShowProgram = MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowProgram;
			ShowDay = MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowDay;
			ShowTime = MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowTime;
			ShowSpots = MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowWeeklySpots ||
				MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowMonthlySpots ||
				MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowTotalSpots;
			ShowRate = MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowRate;
			ShowLenght = MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowLenght;
			ShowCost = MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowCost;
			ShowTotalSpots = MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowTallySpots;
			ShowTotalCost = MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowTallyCost;
			ShowAverageRate = MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowAverageRate;
			ShowSpotsX = MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowSpotsX;
			UseDecimalRates = MediaMetaData.Instance.ListManager.DefaultOptionsSettings.UseDecimalRates;

			DefaultColumnPositions = true;
			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowStation)
			{
				PositionStation = DefaultPositionStation;
			}
			else
				PositionStation = -1;
			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowProgram)
			{
				PositionProgram = DefaultPositionProgram;
			}
			else
				PositionProgram = -1;
			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowDay)
			{
				PositionDay = DefaultPositionDay;
			}
			else
				PositionDay = -1;
			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowTime)
			{
				PositionTime = DefaultPositionTime;
			}
			else
				PositionTime = -1;
			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowLenght)
			{
				PositionLenght = DefaultPositionLenght;
			}
			else
				PositionLenght = -1;
			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowWeeklySpots || MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowMonthlySpots || MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowTotalSpots)
			{
				PositionSpots = DefaultPositionSpots;
			}
			else
				PositionSpots = -1;
			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowRate)
			{
				PositionRate = DefaultPositionRate;
			}
			else
				PositionRate = -1;
			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowCost)
			{
				PositionCost = DefaultPositionCost;
			}
			else
				PositionCost = -1;

			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowWeeklySpots)
				SpotType = SpotType.Week;
			else if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowMonthlySpots)
				SpotType = SpotType.Month;
			else if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowTotalSpots)
				SpotType = SpotType.Total;
			else
				SpotType = SpotType.Week;
			#endregion
		}

		public void Dispose()
		{
			Programs.ForEach(p => p.Dispose());
			Programs.Clear();

			Logo.Dispose();
			Logo = null;

			ContractSettings = null;

			Parent = null;
		}

		public void AfterClone(OptionSet source, bool fullClone = true)
		{
			UniqueID = Guid.NewGuid();
			Parent = source.Parent;
			foreach (var optionProgram in Programs)
			{
				optionProgram.AfterClone(source.Programs.First(sourceProgram => sourceProgram.UniqueID == optionProgram.UniqueID), fullClone);
				optionProgram.Parent = this;
			}
		}

		public void AddProgram()
		{
			var program = new OptionProgram(this);
			Programs.Add(program);
		}

		public void DeleteProgram(int programIndex)
		{
			if (programIndex < 0 || programIndex >= Programs.Count) return;
			var program = Programs[programIndex];
			Programs.Remove(program);
			RebuildProgramIndexes();
		}

		public void CloneProgram(int programIndex)
		{
			if (programIndex < 0 || programIndex >= Programs.Count) return;
			var program = Programs[programIndex];
			var newProgram = program.Clone<OptionProgram, OptionProgram>();
			Programs.Add(newProgram);
			if (CloneLineToTheEnd)
				newProgram.Index = Programs.Count;
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
