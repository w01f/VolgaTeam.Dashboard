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
		public Schedule Parent { get; private set; }
		public Guid UniqueID { get; set; }
		public double Index { get; set; }
		public string Name { get; set; }
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
		public decimal AvgRate
		{
			get { return TotalSpots != 0 ? (TotalCost / TotalSpots) : 0; }
		}

		public decimal TotalCost
		{
			get { return Programs.Any() ? (Programs.Select(x => x.Cost ?? 0).Sum()) : 0; }
		}

		public int TotalSpots
		{
			get { return Programs.Any() ? Programs.Select(x => x.Spot ?? 0).Sum() : 0; }
		}

		#endregion

		public OptionSet(RegularSchedule parent)
		{
			Parent = parent;
			UniqueID = Guid.NewGuid();
			Index = parent.Snapshots.Any() ? parent.Snapshots.Max(s => s.Index) + 1 : 0;
			Programs = new List<OptionProgram>();

			#region Options
			ShowLineId = true;
			ShowLogo = false;
			ShowStation = true;
			ShowProgram = true;
			ShowDay = true;
			ShowTime = true;
			ShowSpots = true;
			ShowRate = true;
			ShowLenght = true;
			ShowCost = false;
			ShowTotalSpots = false;
			ShowTotalCost = false;
			ShowAverageRate = false;
			ShowSpotsX = true;
			UseDecimalRates = false;

			PositionStation = 0;
			PositionProgram = 1;
			PositionDay = 2;
			PositionTime = 3;
			PositionSpots = 4;
			PositionRate = 5;
			PositionLenght = 6;
			PositionCost = -1;

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
}
