using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Core.MediaSchedule
{
	public abstract class MediaListManager
	{
		protected abstract string StrategyFileName { get; }
		protected abstract string XmlRootPrefix { get; }
		protected abstract string MainImageFolderPath { get; }
		protected abstract string AdditionalImageFolderPath { get; }
		protected abstract string ProgramStrategyDefaultLogoPath { get; }

		protected MediaListManager()
		{
			SlideHeaders = new List<string>();
			ClientTypes = new List<string>();
			Lengths = new List<string>();
			Stations = new List<Station>();
			CustomDemos = new List<string>();
			Dayparts = new List<Daypart>();
			Times = new List<string>();
			Days = new List<string>();
			SourcePrograms = new List<SourceProgram>();
			Statuses = new List<string>();
			MonthTemplatesMondayBased = new List<MediaMonthTemplate>();
			MonthTemplatesSundayBased = new List<MediaMonthTemplate>();
			DefaultWeeklyScheduleSettings = new ScheduleSectionSettings();
			DefaultMonthlyScheduleSettings = new ScheduleSectionSettings();
			DefaultSnapshotSettings = new SnapshotSettings();
			DefaultSnapshotSummarySettings = new SnapshotSummarySettings();
			DefaultOptionsSettings = new OptionsSettings();
			DefaultOptionsSummarySettings = new OptionsSummarySettings();
			DefaultBroadcastCalendarSettings = new CalendarToggleSettings();
			DefaultCustomCalendarSettings = new CalendarToggleSettings();
			
			Images = new List<ImageSourceGroup>();
			LoadImages();

			LoadLists();
		}

		public DirectoryInfo BigImageFolder { get; set; }
		public DirectoryInfo SmallImageFolder { get; set; }
		public DirectoryInfo TinyImageFolder { get; set; }
		public DirectoryInfo XtraTinyImageFolder { get; set; }
		public List<ImageSourceGroup> Images { get; set; }
		public List<string> SlideHeaders { get; set; }
		public List<string> ClientTypes { get; set; }
		public List<string> Lengths { get; set; }
		public List<string> CustomDemos { get; set; }
		public List<string> Times { get; set; }
		public List<string> Days { get; set; }
		public List<Daypart> Dayparts { get; set; }
		public List<Station> Stations { get; set; }
		public List<SourceProgram> SourcePrograms { get; set; }
		public List<string> Statuses { get; set; }
		public List<MediaMonthTemplate> MonthTemplatesMondayBased { get; set; }
		public List<MediaMonthTemplate> MonthTemplatesSundayBased { get; set; }
		public ImageSource DefaultStrategyLogo { get; set; }
		public bool FlexFlightDatesAllowed { get; private set; }

		public ScheduleSectionSettings DefaultWeeklyScheduleSettings { get; private set; }
		public ScheduleSectionSettings DefaultMonthlyScheduleSettings { get; private set; }
		public SnapshotSettings DefaultSnapshotSettings { get; private set; }
		public SnapshotSummarySettings DefaultSnapshotSummarySettings { get; private set; }
		public OptionsSettings DefaultOptionsSettings { get; private set; }
		public OptionsSummarySettings DefaultOptionsSummarySettings { get; private set; }
		public CalendarToggleSettings DefaultBroadcastCalendarSettings { get; private set; }
		public CalendarToggleSettings DefaultCustomCalendarSettings { get; private set; }

		private void LoadStrategy()
		{
			SlideHeaders.Clear();
			ClientTypes.Clear();
			SourcePrograms.Clear();
			Lengths.Clear();
			Stations.Clear();
			CustomDemos.Clear();
			Dayparts.Clear();
			Times.Clear();
			var listPath = Path.Combine(SettingsManager.Instance.SharedListFolder, StrategyFileName);
			if (File.Exists(listPath))
			{
				var document = new XmlDocument();
				document.Load(listPath);

				XmlNode node = document.SelectSingleNode(String.Format(@"/{0}Strategy", XmlRootPrefix));
				if (node != null)
				{
					foreach (XmlNode childeNode in node.ChildNodes)
					{
						switch (childeNode.Name)
						{
							case "SlideHeader":
								foreach (XmlAttribute attribute in childeNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Value":
											if (!string.IsNullOrEmpty(attribute.Value) && !SlideHeaders.Contains(attribute.Value))
												SlideHeaders.Add(attribute.Value);
											break;
									}
								}
								break;
							case "ClientType":
								foreach (XmlAttribute attribute in childeNode.Attributes)
									switch (attribute.Name)
									{
										case "Value":
											if (!ClientTypes.Contains(attribute.Value))
												ClientTypes.Add(attribute.Value);
											break;
									}
								break;
							case "FlexFlightDatesAllowed":
								{
									bool temp;
									if (Boolean.TryParse(childeNode.InnerText, out temp))
										FlexFlightDatesAllowed = temp;
								}
								break;
							case "Daypart":
								var daypart = new Daypart();
								foreach (XmlAttribute attribute in childeNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Name":
											daypart.Name = attribute.Value;
											break;
										case "Code":
											daypart.Code = attribute.Value;
											break;
									}
								}
								if (!string.IsNullOrEmpty(daypart.Name))
									Dayparts.Add(daypart);
								break;
							case "CustomDemo":
								foreach (XmlAttribute attribute in childeNode.Attributes)
									switch (attribute.Name)
									{
										case "Value":
											if (!CustomDemos.Contains(attribute.Value))
												CustomDemos.Add(attribute.Value);
											break;
									}
								break;
							case "Lenght":
								foreach (XmlAttribute attribute in childeNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Value":
											if (!string.IsNullOrEmpty(attribute.Value) && !SlideHeaders.Contains(attribute.Value))
												Lengths.Add(attribute.Value);
											break;
									}
								}
								break;
							case "Station":
								var station = new Station();
								foreach (XmlAttribute attribute in childeNode.Attributes)
								{
									switch (attribute.Name)
									{
										case "Name":
											station.Name = attribute.Value;
											break;
										case "Logo":
											if (!string.IsNullOrEmpty(attribute.Value))
												station.Logo = new Bitmap(new MemoryStream(Convert.FromBase64String(attribute.Value)));
											break;
									}
								}
								if (!string.IsNullOrEmpty(station.Name))
									Stations.Add(station);
								break;
							case "Program":
								var sourceProgram = new SourceProgram();
								GetProgramProperties(childeNode, ref sourceProgram);
								if (!string.IsNullOrEmpty(sourceProgram.Name))
									SourcePrograms.Add(sourceProgram);
								break;
							case "Status":
								foreach (XmlAttribute attribute in childeNode.Attributes)
									switch (attribute.Name)
									{
										case "Value":
											if (!Statuses.Contains(attribute.Value))
												Statuses.Add(attribute.Value);
											break;
									}
								break;
							case "BroadcastMonthTemplate":
								var monthTemplate = new MediaMonthTemplate();
								monthTemplate.Deserialize(childeNode);
								MonthTemplatesMondayBased.Add(monthTemplate);
								MonthTemplatesSundayBased.Add(monthTemplate);
								break;
							case "DefaultWeeklyScheduleSettings":
								DefaultWeeklyScheduleSettings.Deserialize(childeNode);
								break;
							case "DefaultMonthlyScheduleSettings":
								DefaultMonthlyScheduleSettings.Deserialize(childeNode);
								break;
							case "DefaultSnapshotSettings":
								DefaultSnapshotSettings.Deserialize(childeNode);
								break;
							case "DefaultSnapshotSummarySettings":
								DefaultSnapshotSummarySettings.Deserialize(childeNode);
								break;
							case "DefaultOptionsSettings":
								DefaultOptionsSettings.Deserialize(childeNode);
								break;
							case "DefaultOptionsSummarySettings":
								DefaultOptionsSummarySettings.Deserialize(childeNode);
								break;
							case "DefaultBroadcastCalendarSettings":
								DefaultBroadcastCalendarSettings.Deserialize(childeNode);
								break;
							case "DefaultCustomCalendarSettings":
								DefaultCustomCalendarSettings.Deserialize(childeNode);
								break;
						}
					}
				}
			}
			if (SourcePrograms.Count > 0)
			{
				Times.AddRange(SourcePrograms.Select(x => x.Time).Distinct().ToArray());
				Days.AddRange(SourcePrograms.Select(x => x.Day).Distinct().ToArray());
			}
		}

		private void LoadImages()
		{
			Images.Clear();
			var defaultGroup = new ImageSourceGroup(MainImageFolderPath) { Name = "Gallery", Order = -1 };
			if (defaultGroup.Images.Any())
				Images.Add(defaultGroup);

			if (Directory.Exists(AdditionalImageFolderPath))
			{
				var contentDescriptionPath = Path.Combine(AdditionalImageFolderPath, "order.txt");
				if (File.Exists(contentDescriptionPath))
				{
					var groupNames = File.ReadAllLines(contentDescriptionPath);
					var groupIndex = 0;
					foreach (var groupName in groupNames)
					{
						if (String.IsNullOrEmpty(groupName)) continue;
						var groupFolderPath = Path.Combine(AdditionalImageFolderPath, groupName);
						if (!Directory.Exists(groupFolderPath)) continue;
						var imageGroup = new ImageSourceGroup(groupFolderPath);
						imageGroup.Name = groupName;
						imageGroup.Order = groupIndex;
						if (!imageGroup.Images.Any()) continue;
						Images.Add(imageGroup);
						groupIndex++;
					}
				}
			}

			DefaultStrategyLogo = ImageSource.FromImage(File.Exists(ProgramStrategyDefaultLogoPath) ? new Bitmap(ProgramStrategyDefaultLogoPath) : null);
		}

		private void GetProgramProperties(XmlNode node, ref SourceProgram sourceProgram)
		{
			foreach (XmlAttribute attribute in node.Attributes)
			{
				switch (attribute.Name)
				{
					case "Name":
						sourceProgram.Name = attribute.Value;
						break;
					case "Station":
						sourceProgram.Station = attribute.Value;
						break;
					case "Daypart":
						sourceProgram.Daypart = attribute.Value;
						break;
					case "Day":
						sourceProgram.Day = attribute.Value;
						break;
					case "Time":
						sourceProgram.Time = attribute.Value;
						break;
				}
			}
			foreach (XmlNode childNode in node.ChildNodes)
				switch (childNode.Name)
				{
					case "Demo":
						var demo = new Demo();
						foreach (XmlAttribute attribute in childNode.Attributes)
						{
							switch (attribute.Name)
							{
								case "Source":
									demo.Source = attribute.Value;
									break;
								case "DemoType":
									int tempInt;
									if (Int32.TryParse(attribute.Value, out tempInt))
										demo.DemoType = (DemoType)tempInt;
									break;
								case "Name":
									demo.Name = attribute.Value;
									break;
								case "Value":
									demo.Value = attribute.Value;
									break;
							}
						}
						if (!String.IsNullOrEmpty(demo.Name) && !String.IsNullOrEmpty(demo.Source) && !String.IsNullOrEmpty(demo.Value))
							sourceProgram.Demos.Add(demo);
						break;
				}
		}

		private void LoadLists()
		{
			LoadStrategy();
		}
	}

	public class TVListManager : MediaListManager
	{
		protected override string StrategyFileName
		{
			get { return @"TV XML\TV Strategy.xml"; }
		}

		protected override string XmlRootPrefix
		{
			get { return "TV"; }
		}

		protected override string MainImageFolderPath
		{
			get { return String.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Artwork\TV\", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}

		protected override string AdditionalImageFolderPath
		{
			get { return String.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Artwork\TV_2\", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}

		protected override string ProgramStrategyDefaultLogoPath
		{
			get { return String.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Artwork\TV\DefaultStrategyImage.png", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}
	}

	public class RadioListManager : MediaListManager
	{
		protected override string StrategyFileName
		{
			get { return @"Radio XML\Radio Strategy.xml"; }
		}

		protected override string XmlRootPrefix
		{
			get { return "Radio"; }
		}

		protected override string MainImageFolderPath
		{
			get { return String.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Artwork\Radio\", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}

		protected override string AdditionalImageFolderPath
		{
			get { return String.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Artwork\Radio_2\", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}

		protected override string ProgramStrategyDefaultLogoPath
		{
			get { return String.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Artwork\Radio\DefaultStrategyImage.png", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)); }
		}
	}
}