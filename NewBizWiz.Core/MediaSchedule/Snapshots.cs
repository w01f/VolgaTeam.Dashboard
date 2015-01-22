using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Core.MediaSchedule
{
	public class Snapshot
	{
		public RegularSchedule Parent { get; private set; }
		public Guid UniqueID { get; set; }
		public double Index { get; set; }
		public string Name { get; set; }
		public ImageSource Logo { get; set; }
		public string Comment { get; set; }
		public int? TotalWeeks { get; set; }

		public List<SnapshotProgram> Programs { get; private set; }

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

		public bool ShowTotalSpots { get; set; }
		public bool ShowAverageRate { get; set; }
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
			get { return Programs.Any() ? (Programs.Select(x => x.TotalCost).Sum()) : 0; }
		}

		public decimal TotalWeekCost
		{
			get { return TotalCost * (decimal)TotalWeeks; }
		}

		public int TotalSpots
		{
			get { return Programs.Any() ? Programs.Select(x => x.TotalSpots).Sum() : 0; }
		}
		#endregion

		public Snapshot(RegularSchedule parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Index = parent.Snapshots.Any() ? parent.Snapshots.Max(s => s.Index) + 1 : 0;
			Logo = MediaMetaData.Instance.ListManager.Images.FirstOrDefault(i => i.IsDefault);
			TotalWeeks = 1;
			Programs = new List<SnapshotProgram>();

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
			#endregion
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			result.AppendLine(@"<UniqueID>" + UniqueID + @"</UniqueID>");
			result.AppendLine(@"<Index>" + Index + @"</Index>");
			if (!String.IsNullOrEmpty(Name))
				result.AppendLine(@"<Name>" + Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Name>");
			if (Logo != null && Logo.ContainsData)
				result.AppendLine(@"<Logo>" + Logo.Serialize() + @"</Logo>");
			if (!String.IsNullOrEmpty(Comment))
				result.AppendLine(@"<Comment>" + Comment.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Comment>");
			if (TotalWeeks.HasValue)
				result.AppendLine(@"<TotalWeeks>" + TotalWeeks.Value + @"</TotalWeeks>");

			#region Options
			result.AppendLine(@"<ShowLineId>" + ShowLineId + @"</ShowLineId>");
			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			result.AppendLine(@"<ShowStation>" + ShowStation + @"</ShowStation>");
			result.AppendLine(@"<ShowProgram>" + ShowProgram + @"</ShowProgram>");
			result.AppendLine(@"<ShowDaypart>" + ShowDaypart + @"</ShowDaypart>");
			result.AppendLine(@"<ShowLenght>" + ShowLenght + @"</ShowLenght>");
			result.AppendLine(@"<ShowTime>" + ShowTime + @"</ShowTime>");
			result.AppendLine(@"<ShowRate>" + ShowRate + @"</ShowRate>");
			result.AppendLine(@"<ShowCost>" + ShowCost + @"</ShowCost>");
			result.AppendLine(@"<ShowTotalSpots>" + ShowTotalSpots + @"</ShowTotalSpots>");
			result.AppendLine(@"<ShowAverageRate>" + ShowAverageRate + @"</ShowAverageRate>");
			result.AppendLine(@"<ShowSpotsX>" + ShowSpotsX + @"</ShowSpotsX>");
			result.AppendLine(@"<ShowTotalRow>" + ShowTotalRow + @"</ShowTotalRow>");
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
					case "TotalWeeks":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								TotalWeeks = temp;
						}
						break;

					case "ShowLineId":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLineId = tempBool;
						break;
					case "ShowLogo":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLogo = tempBool;
						break;
					case "ShowStation":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowStation = tempBool;
						break;
					case "ShowProgram":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowProgram = tempBool;
						break;
					case "ShowDaypart":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowDaypart = tempBool;
						break;
					case "ShowLenght":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowLenght = tempBool;
						break;
					case "ShowTime":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTime = tempBool;
						break;
					case "ShowRate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowRate = tempBool;
						break;
					case "ShowCost":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowCost = tempBool;
						break;
					case "ShowTotalSpots":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalSpots = tempBool;
						break;
					case "ShowAverageRate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowAverageRate = tempBool;
						break;
					case "ShowSpotsX":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowSpotsX = tempBool;
						break;
					case "ShowTotalRow":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalRow = tempBool;
						break;
					case "UseDecimalRates":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							UseDecimalRates = tempBool;
						break;
					case "Programs":
						foreach (XmlNode programNode in childNode.ChildNodes)
						{
							var program = new SnapshotProgram(this);
							program.Deserialize(programNode);
							Programs.Add(program);
						}
						break;
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
	}

	public class SnapshotProgram
	{
		private string _name;

		public Snapshot Parent { get; private set; }
		public Guid UniqueID { get; set; }
		public decimal Index { get; set; }
		public string Station { get; set; }
		public ImageSource Logo { get; set; }
		public string Daypart { get; set; }
		public string Length { get; set; }
		public string Time { get; set; }
		public decimal? Rate { get; set; }

		public int? MondaySpot { get; set; }
		public int? TuesdaySpot { get; set; }
		public int? WednesdaySpot { get; set; }
		public int? ThursdaySpot { get; set; }
		public int? FridaySpot { get; set; }
		public int? SaturdaySpot { get; set; }
		public int? SundaySpot { get; set; }

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

		public decimal TotalCost
		{
			get { return (Rate.HasValue ? Rate.Value : 0) * TotalSpots; }
		}

		public int TotalSpots
		{
			get
			{
				return new[] 
				{ 
					MondaySpot, 
					TuesdaySpot, 
					WednesdaySpot, 
					ThursdaySpot, 
					FridaySpot, 
					SaturdaySpot, 
					SundaySpot 
				}
				.Select(v => v ?? 0)
				.Sum();
			}
		}
		#endregion

		public SnapshotProgram(Snapshot parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Index = Parent.Programs.Count + 1;
			Station = Parent.Parent.Stations.Count(x => x.Available) == 1 ? Parent.Parent.Stations.Where(x => x.Available).Select(x => x.Name).FirstOrDefault() : null;
			Logo = MediaMetaData.Instance.ListManager.Images.FirstOrDefault(i => i.IsDefault);
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
			if (Logo != null && Logo.ContainsData)
				result.AppendLine(@"<Logo>" + Logo.Serialize() + @"</Logo>");
			if (!String.IsNullOrEmpty(Daypart))
				result.AppendLine(@"<Daypart>" + Daypart.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Daypart>");
			if (!String.IsNullOrEmpty(Length))
				result.AppendLine(@"<Length>" + Length.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Length>");
			if (!String.IsNullOrEmpty(Time))
				result.AppendLine(@"<Time>" + Time.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Time>");
			if (Rate.HasValue)
				result.AppendLine(@"<Rate>" + Rate.Value + @"</Rate>");
			if (MondaySpot.HasValue)
				result.AppendLine(@"<MondaySpot>" + MondaySpot.Value + @"</MondaySpot>");
			if (TuesdaySpot.HasValue)
				result.AppendLine(@"<TuesdaySpot>" + TuesdaySpot.Value + @"</TuesdaySpot>");
			if (WednesdaySpot.HasValue)
				result.AppendLine(@"<WednesdaySpot>" + WednesdaySpot.Value + @"</WednesdaySpot>");
			if (ThursdaySpot.HasValue)
				result.AppendLine(@"<ThursdaySpot>" + ThursdaySpot.Value + @"</ThursdaySpot>");
			if (FridaySpot.HasValue)
				result.AppendLine(@"<FridaySpot>" + FridaySpot.Value + @"</FridaySpot>");
			if (SaturdaySpot.HasValue)
				result.AppendLine(@"<SaturdaySpot>" + SaturdaySpot.Value + @"</SaturdaySpot>");
			if (SundaySpot.HasValue)
				result.AppendLine(@"<SundaySpot>" + SundaySpot.Value + @"</SundaySpot>");
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
					case "Daypart":
						Daypart = childNode.InnerText;
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
					case "MondaySpot":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								MondaySpot = temp;
						}
						break;
					case "TuesdaySpot":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								TuesdaySpot = temp;
						}
						break;
					case "WednesdaySpot":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								WednesdaySpot = temp;
						}
						break;
					case "ThursdaySpot":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								ThursdaySpot = temp;
						}
						break;
					case "FridaySpot":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								FridaySpot = temp;
						}
						break;
					case "SaturdaySpot":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								SaturdaySpot = temp;
						}
						break;
					case "SundaySpot":
						{
							int temp;
							if (Int32.TryParse(childNode.InnerText, out temp))
								SundaySpot = temp;
						}
						break;
				}
		}

		public void ApplyDefaultValues()
		{
			var source = MediaMetaData.Instance.ListManager.SourcePrograms.FirstOrDefault(x => x.Name.Equals(_name));
			if (source == null) return;
			Daypart = source.Daypart;
			Time = source.Time;
		}

		public SnapshotProgram Clone(bool fullClone)
		{
			var clone = new SnapshotProgram(Parent);
			clone.Name = Name;
			clone.Station = Station;
			clone.Logo = Logo != null ? Logo.Clone() : null;
			clone.Daypart = Daypart;
			clone.Length = Length;
			clone.Time = Time;
			clone.Rate = Rate;
			if (fullClone)
			{
				clone.MondaySpot = MondaySpot;
				clone.TuesdaySpot = TuesdaySpot;
				clone.WednesdaySpot = WednesdaySpot;
				clone.ThursdaySpot = ThursdaySpot;
				clone.FridaySpot = FridaySpot;
				clone.SaturdaySpot = SaturdaySpot;
				clone.SundaySpot = SundaySpot;
			}
			return clone;
		}
	}

	public class SnapshotSummary
	{
		public RegularSchedule Parent { get; private set; }

		#region Options
		public bool ShowLineId { get; set; }
		public bool ShowLogo { get; set; }
		public bool ShowCampaign { get; set; }
		public bool ShowComments { get; set; }
		public bool ShowSpots { get; set; }
		public bool ShowCost { get; set; }
		public bool ShowTotalWeeks { get; set; }
		public bool ShowTotalCost { get; set; }
		public bool ShowTallySpots { get; set; }
		public bool ShowTallyCost { get; set; }
		public bool ShowSpotsX { get; set; }
		public bool UseDecimalRates { get; set; }
		#endregion

		#region Calculated Properties
		public decimal TotalCost
		{
			get { return Parent.Snapshots.Any() ? Parent.Snapshots.Select(x => x.TotalWeekCost).Sum() : 0; }
		}

		public int TotalSpots
		{
			get { return Parent.Snapshots.Any() ? Parent.Snapshots.Select(x => x.TotalSpots).Sum() : 0; }
		}
		#endregion

		public SnapshotSummary(RegularSchedule parent)
		{
			Parent = parent;

			#region Options
			ShowLineId = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowLineId;
			ShowLogo = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowLogo;
			ShowCampaign = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowCampaign;
			ShowComments = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowComments;
			ShowSpots = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowSpots;
			ShowCost = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowCost;
			ShowTotalWeeks = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowTotalWeeks;
			ShowTotalCost = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowTotalCost;
			ShowTallySpots = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowTallySpots;
			ShowTallyCost = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowTallyCost;
			ShowSpotsX = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.ShowSpotsX;
			UseDecimalRates = MediaMetaData.Instance.ListManager.DefaultSnapshotSummarySettings.UseDecimalRates;
			#endregion
		}

		public string Serialize()
		{
			var result = new StringBuilder();

			#region Options
			result.AppendLine(@"<ShowLineId>" + ShowLineId + @"</ShowLineId>");
			result.AppendLine(@"<ShowLogo>" + ShowLogo + @"</ShowLogo>");
			result.AppendLine(@"<ShowCampaign>" + ShowCampaign + @"</ShowCampaign>");
			result.AppendLine(@"<ShowComments>" + ShowComments + @"</ShowComments>");
			result.AppendLine(@"<ShowSpots>" + ShowSpots + @"</ShowSpots>");
			result.AppendLine(@"<ShowCost>" + ShowCost + @"</ShowCost>");
			result.AppendLine(@"<ShowTotalWeeks>" + ShowTotalWeeks + @"</ShowTotalWeeks>");
			result.AppendLine(@"<ShowTotalCost>" + ShowTotalCost + @"</ShowTotalCost>");
			result.AppendLine(@"<ShowTallySpots>" + ShowTallySpots + @"</ShowTallySpots>");
			result.AppendLine(@"<ShowTallyCost>" + ShowTallyCost + @"</ShowTallyCost>");
			result.AppendLine(@"<ShowSpotsX>" + ShowSpotsX + @"</ShowSpotsX>");
			result.AppendLine(@"<UseDecimalRates>" + UseDecimalRates + @"</UseDecimalRates>");
			#endregion

			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			bool tempBool;

			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
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
					case "ShowTotalWeeks":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowTotalWeeks = tempBool;
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
