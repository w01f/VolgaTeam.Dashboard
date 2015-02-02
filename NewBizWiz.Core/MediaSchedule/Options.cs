using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Core.MediaSchedule
{
	public class OptionSet
	{
		public RegularSchedule Parent { get; private set; }
		public Guid UniqueID { get; set; }
		public double Index { get; set; }
		public string Name { get; set; }
		public ImageSource Logo { get; set; }
		public string Comment { get; set; }
		public int? TotalPeriods { get; set; }
		public List<OptionProgram> Programs { get; private set; }

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

		public int PositionStation { get; set; }
		public int PositionProgram { get; set; }
		public int PositionDay { get; set; }
		public int PositionTime { get; set; }
		public int PositionRate { get; set; }
		public int PositionLenght { get; set; }
		public int PositionSpots { get; set; }
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

		public OptionSet(RegularSchedule parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Index = parent.Options.Any() ? parent.Options.Max(s => s.Index) + 1 : 0;
			Logo = MediaMetaData.Instance.ListManager.Images.Where(g => g.IsDefault).Select(g => g.Images.FirstOrDefault(i => i.IsDefault)).FirstOrDefault();
			TotalPeriods = 1;
			Programs = new List<OptionProgram>();

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

			var position = 0;
			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowStation)
			{
				PositionStation = position;
				position++;
			}
			else
				PositionStation = -1;
			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowProgram)
			{
				PositionProgram = position;
				position++;
			}
			else
				PositionProgram = -1;
			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowDay)
			{
				PositionDay = position;
				position++;
			}
			else
				PositionDay = -1;
			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowTime)
			{
				PositionTime = position;
				position++;
			}
			else
				PositionTime = -1;
			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowWeeklySpots || MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowMonthlySpots || MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowTotalSpots)
			{
				PositionSpots = position;
				position++;
			}
			else
				PositionSpots = -1;
			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowRate)
			{
				PositionRate = position;
				position++;
			}
			else
				PositionRate = -1;
			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowLenght)
			{
				PositionLenght = position;
				position++;
			}
			else
				PositionLenght = -1;
			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowCost)
			{
				PositionCost = position;
				position++;
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

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<UniqueID>" + UniqueID + @"</UniqueID>");
			result.AppendLine(@"<Index>" + Index + @"</Index>");
			if (!String.IsNullOrEmpty(Name))
				result.AppendLine(@"<Name>" + Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Name>");
			if (Logo != null && Logo.ContainsData && !Logo.IsDefault)
				result.AppendLine(@"<Logo>" + Logo.Serialize() + @"</Logo>");
			if (!String.IsNullOrEmpty(Comment))
				result.AppendLine(@"<Comment>" + Comment.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Comment>");
			if (TotalPeriods.HasValue)
				result.AppendLine(@"<TotalPeriods>" + TotalPeriods.Value + @"</TotalPeriods>");

			#region Options
			result.AppendLine(@"<ShowLineId>" + ShowLineId + @"</ShowLineId>");
			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			result.AppendLine(@"<ShowStation>" + ShowStation + @"</ShowStation>");
			result.AppendLine(@"<ShowProgram>" + ShowProgram + @"</ShowProgram>");
			result.AppendLine(@"<ShowDay>" + ShowDay + @"</ShowDay>");
			result.AppendLine(@"<ShowLenght>" + ShowLenght + @"</ShowLenght>");
			result.AppendLine(@"<ShowTime>" + ShowTime + @"</ShowTime>");
			result.AppendLine(@"<ShowRate>" + ShowRate + @"</ShowRate>");
			result.AppendLine(@"<ShowSpots>" + ShowSpots + @"</ShowSpots>");
			result.AppendLine(@"<ShowCost>" + ShowCost + @"</ShowCost>");
			result.AppendLine(@"<ShowTotalSpots>" + ShowTotalSpots + @"</ShowTotalSpots>");
			result.AppendLine(@"<ShowTotalCost>" + ShowTotalCost + @"</ShowTotalCost>");
			result.AppendLine(@"<ShowAverageRate>" + ShowAverageRate + @"</ShowAverageRate>");
			result.AppendLine(@"<ShowSpotsX>" + ShowSpotsX + @"</ShowSpotsX>");
			result.AppendLine(@"<UseDecimalRates>" + UseDecimalRates + @"</UseDecimalRates>");

			result.AppendLine(@"<PositionStation>" + PositionStation + @"</PositionStation>");
			result.AppendLine(@"<PositionProgram>" + PositionProgram + @"</PositionProgram>");
			result.AppendLine(@"<PositionDay>" + PositionDay + @"</PositionDay>");
			result.AppendLine(@"<PositionLenght>" + PositionLenght + @"</PositionLenght>");
			result.AppendLine(@"<PositionTime>" + PositionTime + @"</PositionTime>");
			result.AppendLine(@"<PositionRate>" + PositionRate + @"</PositionRate>");
			result.AppendLine(@"<PositionSpots>" + PositionSpots + @"</PositionSpots>");
			result.AppendLine(@"<PositionCost>" + PositionCost + @"</PositionCost>");

			result.AppendLine(@"<SpotType>" + (Int32)SpotType + @"</SpotType>");
			#endregion

			result.AppendLine(@"<Programs>");
			foreach (var program in Programs)
				result.AppendLine(program.Serialize());
			result.AppendLine(@"</Programs>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "UniqueID":
						{
							Guid temp;
							if (Guid.TryParse(childNode.InnerText, out temp))
								UniqueID = temp;
						}
						break;
					case "Index":
						{
							double temp;
							if (Double.TryParse(childNode.InnerText, out temp))
								Index = temp;
						}
						break;
					case "Name":
						Name = childNode.InnerText;
						break;
					case "Logo":
						Logo = new ImageSource();
						Logo.Deserialize(childNode);
						break;
					case "Comment":
						Comment = childNode.InnerText;
						break;
					case "TotalPeriods":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								TotalPeriods = temp;
						}
						break;

					case "ShowLineId":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowLineId = temp;
						}
						break;
					case "ShowLogo":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowLogo = temp;
						}
						break;
					case "ShowStation":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowStation = temp;
						}
						break;
					case "ShowProgram":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowProgram = temp;
						}
						break;
					case "ShowDay":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowDay = temp;
						}
						break;
					case "ShowLenght":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowLenght = temp;
						}
						break;
					case "ShowTime":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTime = temp;
						}
						break;
					case "ShowRate":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowRate = temp;
						}
						break;
					case "ShowSpots":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowSpots = temp;
						}
						break;
					case "ShowCost":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowCost = temp;
						}
						break;
					case "ShowTotalSpots":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTotalSpots = temp;
						}
						break;
					case "ShowTotalCost":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowTotalCost = temp;
						}
						break;
					case "ShowAverageRate":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowAverageRate = temp;
						}
						break;
					case "ShowSpotsX":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								ShowSpotsX = temp;
						}
						break;
					case "UseDecimalRates":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								UseDecimalRates = temp;
						}
						break;

					case "PositionStation":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								PositionStation = temp;
						}
						break;
					case "PositionProgram":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								PositionProgram = temp;
						}
						break;
					case "PositionDay":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								PositionDay = temp;
						}
						break;
					case "PositionLenght":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								PositionLenght = temp;
						}
						break;
					case "PositionTime":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								PositionTime = temp;
						}
						break;
					case "PositionRate":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								PositionRate = temp;
						}
						break;
					case "PositionSpots":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								PositionSpots = temp;
						}
						break;
					case "PositionCost":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								PositionCost = temp;
						}
						break;

					case "SpotType":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								SpotType = (SpotType)temp;
						}
						break;

					case "Programs":
						foreach (XmlNode programNode in childNode.ChildNodes)
						{
							var program = new OptionProgram(this);
							program.Deserialize(programNode);
							Programs.Add(program);
						}
						break;
				}
		}

		public OptionSet Clone()
		{
			var newOptionSet = new OptionSet(Parent);
			var optionSerialized = new XmlDocument();
			optionSerialized.LoadXml(@"<Option>" + Serialize() + @"</Option>");
			newOptionSet.Deserialize(optionSerialized.SelectSingleNode("Option"));
			newOptionSet.UniqueID = Guid.NewGuid();
			return newOptionSet;
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
			Programs.Add(program.Clone());
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
			Logo = defaultProgram.Logo.Clone();
		}
	}

	public class OptionProgram
	{
		private string _name;

		public OptionSet Parent { get; private set; }
		public Guid UniqueID { get; set; }
		public decimal Index { get; set; }
		public string Station { get; set; }
		public ImageSource Logo { get; set; }
		public string Day { get; set; }
		public string Time { get; set; }
		public string Length { get; set; }
		public decimal? Rate { get; set; }
		public int? Spot { get; set; }

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

		public Image SmallLogo
		{
			get { return Logo != null ? Logo.TinyImage : null; }
		}

		public decimal? Cost
		{
			get { return Rate * Spot; }
		}
		#endregion

		public OptionProgram(OptionSet parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Index = Parent.Programs.Count + 1;
			Station = Parent.Parent.Stations.Count(x => x.Available) == 1 ? Parent.Parent.Stations.Where(x => x.Available).Select(x => x.Name).FirstOrDefault() : null;
			Logo = MediaMetaData.Instance.ListManager.Images.Where(g => g.IsDefault).Select(g => g.Images.FirstOrDefault(i => i.IsDefault)).FirstOrDefault();
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<Program>");
			result.AppendLine(@"<UniqueID>" + UniqueID + @"</UniqueID>");
			if (!String.IsNullOrEmpty(_name))
				result.AppendLine(@"<Name>" + _name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Name>");
			if (!String.IsNullOrEmpty(Station))
				result.AppendLine(@"<Station>" + Station.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Station>");
			if (Logo != null && Logo.ContainsData && !Logo.IsDefault)
				result.AppendLine(@"<Logo>" + Logo.Serialize() + @"</Logo>");
			if (!String.IsNullOrEmpty(Day))
				result.AppendLine(@"<Day>" + Day.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Day>");
			if (!String.IsNullOrEmpty(Length))
				result.AppendLine(@"<Length>" + Length.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Length>");
			if (!String.IsNullOrEmpty(Time))
				result.AppendLine(@"<Time>" + Time.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Time>");
			if (Rate.HasValue)
				result.AppendLine(@"<Rate>" + Rate.Value + @"</Rate>");
			if (Spot.HasValue)
				result.AppendLine(@"<Spot>" + Spot.Value + @"</Spot>");
			result.AppendLine(@"</Program>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "UniqueID":
						{
							Guid temp;
							if (Guid.TryParse(childNode.InnerText, out temp))
								UniqueID = temp;
						}
						break;
					case "Name":
						_name = childNode.InnerText;
						break;
					case "Station":
						Station = childNode.InnerText;
						break;
					case "Logo":
						Logo = new ImageSource();
						Logo.Deserialize(childNode);
						break;
					case "Day":
						Day = childNode.InnerText;
						break;
					case "Length":
						Length = childNode.InnerText;
						break;
					case "Time":
						Time = childNode.InnerText;
						break;
					case "Rate":
						{
							decimal temp;
							if (Decimal.TryParse(childNode.InnerText, out temp))
								Rate = temp;
						}
						break;
					case "Spot":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								Spot = temp;
						}
						break;
				}
		}

		public void ApplyDefaultValues()
		{
			var source = MediaMetaData.Instance.ListManager.SourcePrograms.FirstOrDefault(x => x.Name.Equals(_name));
			if (source == null) return;
			Day = source.Day;
			Time = source.Time;
		}

		public OptionProgram Clone()
		{
			var clone = new OptionProgram(Parent);
			clone.Name = Name;
			clone.Station = Station;
			clone.Logo = Logo != null ? Logo.Clone() : null;
			clone.Day = Day;
			clone.Length = Length;
			clone.Time = Time;
			clone.Rate = Rate;
			clone.Spot = Spot;
			return clone;
		}
	}

	public class OptionSummary
	{
		public RegularSchedule Parent { get; private set; }
		public SpotType SpotType { get; set; }
		public bool ApplySettingsForAll { get; set; }

		#region Options
		public bool ShowLineId { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowCampaign { get; set; }
		public bool ShowComments { get; set; }
		public bool ShowSpots { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowTotalPeriods { get; set; }
		public bool ShowTotalCost { get; set; }
		public bool ShowTallySpots { get; set; }
		public bool ShowTallyCost { get; set; }
		public bool ShowSpotsX { get; set; }
		public bool UseDecimalRates { get; set; }
		#endregion

		#region Calculated Properties
		public bool Enabled
		{
			get { return Parent.Options.All(o => o.SpotType == SpotType); }
		}

		public decimal TotalCost
		{
			get { return Parent.Options.Any() ? Parent.Options.Select(x => x.TotalPeriodCost).Sum() : 0; }
		}

		public int TotalSpots
		{
			get { return Parent.Options.Any() ? Parent.Options.Select(x => x.TotalPeriodSpots).Sum() : 0; }
		}
		#endregion

		public OptionSummary(RegularSchedule parent)
		{
			Parent = parent;

			if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowWeeklySpots)
				SpotType = SpotType.Week;
			else if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowMonthlySpots)
				SpotType = SpotType.Month;
			else if (MediaMetaData.Instance.ListManager.DefaultOptionsSettings.ShowTotalSpots)
				SpotType = SpotType.Total;
			else
				SpotType = SpotType.Week;

			ApplySettingsForAll = false;

			#region Options
			ShowLineId = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowLineId;
			ShowLogo = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowLogo;
			ShowCampaign = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowCampaign;
			ShowComments = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowComments;
			ShowTotalCost = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowTotalCost;
			ShowTallySpots = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowTallySpots;
			ShowTallyCost = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowTallyCost;
			ShowSpotsX = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowSpotsX;
			UseDecimalRates = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.UseDecimalRates;
			#endregion

			UpdateSpotType(true);
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<ApplySettingsForAll>" + ApplySettingsForAll + @"</ApplySettingsForAll>");

			#region Options
			result.AppendLine(@"<SpotType>" + (Int32)SpotType + @"</SpotType>");
			result.AppendLine(@"<ShowLineId>" + ShowLineId + @"</ShowLineId>");
			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			result.AppendLine(@"<ShowCampaign>" + ShowCampaign + @"</ShowCampaign>");
			result.AppendLine(@"<ShowComments>" + ShowComments + @"</ShowComments>");
			result.AppendLine(@"<ShowSpots>" + ShowSpots + @"</ShowSpots>");
			result.AppendLine(@"<ShowCost>" + ShowCost + @"</ShowCost>");
			result.AppendLine(@"<ShowTotalPeriods>" + ShowTotalPeriods + @"</ShowTotalPeriods>");
			result.AppendLine(@"<ShowTotalCost>" + ShowTotalCost + @"</ShowTotalCost>");
			result.AppendLine(@"<ShowTallySpots>" + ShowTallySpots + @"</ShowTallySpots>");
			result.AppendLine(@"<ShowTallyCost>" + ShowTallyCost + @"</ShowTallyCost>");
			result.AppendLine(@"<ShowSpotsX>" + ShowSpotsX + @"</ShowSpotsX>");
			result.AppendLine(@"<UseDecimalRates>" + UseDecimalRates + @"</UseDecimalRates>");
			#endregion

			return result.ToString();
		}

		public void UpdateSpotType(bool force = false)
		{
			var spotType = SpotType;
			if (Parent.Options.Any())
			{
				if (Parent.Options.All(o => o.SpotType == SpotType.Week))
					spotType = SpotType.Week;
				else if (Parent.Options.All(o => o.SpotType == SpotType.Month))
					spotType = SpotType.Month;
				if (Parent.Options.All(o => o.SpotType == SpotType.Total))
					spotType = SpotType.Total;
			}
			if (spotType != SpotType || force)
			{
				SpotType = spotType;
				switch (SpotType)
				{
					case SpotType.Week:
						ShowSpots = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowWeeklySpots;
						ShowCost = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowWeeklyCost;
						ShowTotalPeriods = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowTotalWeeks;
						break;
					case SpotType.Month:
						ShowSpots = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowMonthlySpots;
						ShowCost = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowMonthlyCost;
						ShowTotalPeriods = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowTotalMonths;
						break;
					case SpotType.Total:
						ShowSpots = MediaMetaData.Instance.ListManager.DefaultOptionsSummarySettings.ShowTotalSpots;
						ShowCost = false;
						ShowTotalPeriods = false;
						break;
				}
			}
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool;

			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "SpotType":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								SpotType = (SpotType)temp;
						}
						break;
					case "ApplySettingsForAll":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ApplySettingsForAll = tempBool;
						break;

					#region Options
					case "ShowLineId":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLineId = tempBool;
						break;
					case "ShowLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLogo = tempBool;
						break;
					case "ShowCampaign":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCampaign = tempBool;
						break;
					case "ShowComments":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowComments = tempBool;
						break;
					case "ShowSpots":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSpots = tempBool;
						break;
					case "ShowCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCost = tempBool;
						break;
					case "ShowTotalPeriods":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalPeriods = tempBool;
						break;
					case "ShowTotalCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalCost = tempBool;
						break;
					case "ShowTallySpots":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTallySpots = tempBool;
						break;
					case "ShowTallyCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTallyCost = tempBool;
						break;
					case "ShowSpotsX":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSpotsX = tempBool;
						break;
					case "UseDecimalRates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							UseDecimalRates = tempBool;
						break;
					#endregion
				}
		}
	}
}
