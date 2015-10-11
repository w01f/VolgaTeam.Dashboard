using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Core.AdSchedule
{
	public class ListManager
	{
		private const string PrintStrategyFileName = @"Newspaper XML\Print Strategy.xml";

		private static readonly ListManager _instance = new ListManager();

		private ListManager()
		{
			PublicationSources = new List<PrintProductSource>();
			Readerships = new List<PrintProductSource>();
			PageSizes = new List<NameCodePair>();
			Mechanicals = new List<MechanicalType>();
			Notes = new List<NameCodePair>();
			OutputHeaders = new List<string>();
			ClientTypes = new List<string>();
			Sections = new List<NameCodePair>();
			Deadlines = new List<string>();
			Statuses = new List<string>();
			ShareUnits = new List<ShareUnit>();

			DefaultHomeViewSettings = new HomeViewSettings();

			DefaultPrintScheduleViewSettings = new PrintScheduleViewSettings();

			DefaultPublicationBasicOverviewSettings = new PublicationBasicOverviewSettings();
			DefaultPublicationMultiSummarySettings = new PublicationMultiSummarySettings();
			DefaultSnapshotViewSettings = new SnapshotViewSettings();

			DefaultDetailedGridColumnState = new GridColumnsState();
			DefaultDetailedGridAdNotesState = new AdNotesState();
			DefaultDetailedGridSlideBulletsState = new SlideBulletsState();
			DefaultDetailedGridSlideHeaderState = new SlideHeaderState();

			DefaultMultiGridColumnState = new GridColumnsState();
			DefaultMultiGridAdNotesState = new AdNotesState();
			DefaultMultiGridSlideBulletsState = new SlideBulletsState();
			DefaultMultiGridSlideHeaderState = new SlideHeaderState();

			DefaultCalendarViewSettings = new CalendarViewSettings();

			Images = new List<ImageSourceGroup>();
			LoadImages();

			LoadLists();

			if (DefaultPrintScheduleViewSettings.DefaultPCI)
				DefaultPricingStrategy = AdPricingStrategies.StandartPCI;
			else if (DefaultPrintScheduleViewSettings.DefaultFlat)
				DefaultPricingStrategy = AdPricingStrategies.FlatModular;
			else if (DefaultPrintScheduleViewSettings.DefaultShare)
				DefaultPricingStrategy = AdPricingStrategies.SharePage;

			if (DefaultPrintScheduleViewSettings.DefaultBlackWhite)
				DefaultColor = ColorOptions.BlackWhite;
			else if (DefaultPrintScheduleViewSettings.DefaultSpotColor)
				DefaultColor = ColorOptions.SpotColor;
			else if (DefaultPrintScheduleViewSettings.DefaultFullColor)
				DefaultColor = ColorOptions.FullColor;

			if (DefaultPrintScheduleViewSettings.DefaultCostPerAd)
				DefaultColorPricing = ColorPricingType.CostPerAd;
			else if (DefaultPrintScheduleViewSettings.DefaultPercentOfAd)
				DefaultColorPricing = ColorPricingType.PercentOfAdRate;
			else if (DefaultPrintScheduleViewSettings.DefaultColorIncluded)
				DefaultColorPricing = ColorPricingType.ColorIncluded;
			else if (DefaultPrintScheduleViewSettings.DefaultCostPerInch)
				DefaultColorPricing = ColorPricingType.CostPerInch;
		}

		public static ListManager Instance
		{
			get { return _instance; }
		}

		public DirectoryInfo BigImageFolder { get; set; }
		public DirectoryInfo SmallImageFolder { get; set; }
		public DirectoryInfo TinyImageFolder { get; set; }
		public DirectoryInfo XtraTinyImageFolder { get; set; }
		public List<ImageSourceGroup> Images { get; set; }

		public List<PrintProductSource> PublicationSources { get; set; }
		public List<PrintProductSource> Readerships { get; set; }
		public List<NameCodePair> PageSizes { get; set; }
		public List<NameCodePair> Notes { get; set; }
		public List<string> OutputHeaders { get; set; }
		public List<string> ClientTypes { get; set; }
		public List<NameCodePair> Sections { get; set; }
		public List<MechanicalType> Mechanicals { get; set; }
		public List<string> Deadlines { get; set; }
		public List<string> Statuses { get; set; }
		public List<ShareUnit> ShareUnits { get; set; }
		public int SelectedCommentsBorderValue { get; set; }
		public int SelectedSectionsBorderValue { get; set; }

		public AdPricingStrategies DefaultPricingStrategy { get; set; }
		public ColorPricingType DefaultColorPricing { get; set; }
		public ColorOptions DefaultColor { get; set; }

		public HomeViewSettings DefaultHomeViewSettings { get; private set; }

		public PrintScheduleViewSettings DefaultPrintScheduleViewSettings { get; private set; }

		public PublicationBasicOverviewSettings DefaultPublicationBasicOverviewSettings { get; private set; }
		public PublicationMultiSummarySettings DefaultPublicationMultiSummarySettings { get; private set; }
		public SnapshotViewSettings DefaultSnapshotViewSettings { get; private set; }

		public GridColumnsState DefaultDetailedGridColumnState { get; private set; }
		public AdNotesState DefaultDetailedGridAdNotesState { get; private set; }
		public SlideBulletsState DefaultDetailedGridSlideBulletsState { get; private set; }
		public SlideHeaderState DefaultDetailedGridSlideHeaderState { get; private set; }

		public GridColumnsState DefaultMultiGridColumnState { get; private set; }
		public AdNotesState DefaultMultiGridAdNotesState { get; private set; }
		public SlideBulletsState DefaultMultiGridSlideBulletsState { get; private set; }
		public SlideHeaderState DefaultMultiGridSlideHeaderState { get; private set; }

		public CalendarViewSettings DefaultCalendarViewSettings { get; private set; }

		private void LoadImages()
		{
			var imageFolderPath = String.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Artwork\PRINT\", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			string folderPath = Path.Combine(imageFolderPath, "Big Logos");
			if (Directory.Exists(folderPath))
				BigImageFolder = new DirectoryInfo(folderPath);

			folderPath = Path.Combine(imageFolderPath, "Small Logos");
			if (Directory.Exists(folderPath))
				SmallImageFolder = new DirectoryInfo(folderPath);

			folderPath = Path.Combine(imageFolderPath, "Tiny Logos");
			if (Directory.Exists(folderPath))
				TinyImageFolder = new DirectoryInfo(folderPath);

			folderPath = Path.Combine(imageFolderPath, "Xtra Tiny Logos");
			if (Directory.Exists(folderPath))
				XtraTinyImageFolder = new DirectoryInfo(folderPath);

			Images.Clear();
			//var defaultGroup = new ImageSourceGroup(imageFolderPath) { Name = "Gallery", Order = -1 };
			//if (defaultGroup.Images.Any())
			//	Images.Add(defaultGroup);
		}

		private void LoadPrintStrategy()
		{
			var filePath = string.Empty;

			PublicationSources.Clear();
			Readerships.Clear();
			PageSizes.Clear();
			Notes.Clear();
			OutputHeaders.Clear();
			ClientTypes.Clear();
			Sections.Clear();
			Mechanicals.Clear();
			Deadlines.Clear();
			Statuses.Clear();

			var defaultPublication = new PrintProductSource();
			filePath = Path.Combine(BigImageFolder.FullName, Common.ListManager.DefaultBigLogoFileName);
			defaultPublication.Name = "Default";
			defaultPublication.BigLogo = File.Exists(filePath) ? new Bitmap(filePath) : null;
			filePath = Path.Combine(SmallImageFolder.FullName, Common.ListManager.DefaultSmallLogoFileName);
			defaultPublication.SmallLogo = File.Exists(filePath) ? new Bitmap(filePath) : null;
			filePath = Path.Combine(TinyImageFolder.FullName, Common.ListManager.DefaultTinyLogoFileName);
			defaultPublication.TinyLogo = File.Exists(filePath) ? new Bitmap(filePath) : null;
			PublicationSources.Add(defaultPublication);


			var listPath = Path.Combine(Common.SettingsManager.Instance.SharedListFolder, PrintStrategyFileName);
			if (File.Exists(listPath))
			{
				var document = new XmlDocument();
				document.Load(listPath);

				var node = document.SelectSingleNode(@"/PrintStrategy");
				if (node != null)
				{
					foreach (XmlNode childeNode in node.ChildNodes)
					{
						int tempInt;
						switch (childeNode.Name)
						{
							case "Publication":
								var dailySource = new PrintProductSource();
								dailySource.Circulation = CirculationType.Daily;
								var sundaySource = new PrintProductSource();
								sundaySource.Circulation = CirculationType.Sunday;
								foreach (XmlAttribute attribute in childeNode.Attributes)
								{
									double tempDouble;
									bool tempBool;
									switch (attribute.Name)
									{
										case "Name":
											dailySource.Name = attribute.Value;
											sundaySource.Name = attribute.Value;
											break;
										case "Abbreviation":
											dailySource.Abbreviation = attribute.Value;
											sundaySource.Abbreviation = attribute.Value;
											break;
										case "BigLogo":
											dailySource.BigLogo = null;
											dailySource.BigLogoFileName = attribute.Value;
											filePath = Path.Combine(BigImageFolder.FullName, attribute.Value);
											if (!File.Exists(filePath))
												filePath = Path.Combine(BigImageFolder.FullName, Common.ListManager.DefaultBigLogoFileName);
											if (File.Exists(filePath))
												dailySource.BigLogo = new Bitmap(filePath);
											sundaySource.BigLogo = dailySource.BigLogo;
											sundaySource.BigLogoFileName = dailySource.BigLogoFileName;
											break;
										case "LittleLogo":
											dailySource.SmallLogo = null;
											dailySource.SmallLogoFileName = attribute.Value;
											filePath = Path.Combine(SmallImageFolder.FullName, attribute.Value);
											if (!File.Exists(filePath))
												filePath = Path.Combine(SmallImageFolder.FullName, Common.ListManager.DefaultSmallLogoFileName);
											if (File.Exists(filePath))
												dailySource.SmallLogo = new Bitmap(filePath);
											sundaySource.SmallLogo = dailySource.SmallLogo;
											sundaySource.SmallLogoFileName = dailySource.SmallLogoFileName;
											break;
										case "TinyLogo":
											dailySource.TinyLogo = null;
											dailySource.TinyLogoFileName = attribute.Value;
											filePath = Path.Combine(TinyImageFolder.FullName, attribute.Value);
											if (!File.Exists(filePath))
												filePath = Path.Combine(TinyImageFolder.FullName, Common.ListManager.DefaultTinyLogoFileName);
											if (File.Exists(filePath))
												dailySource.TinyLogo = new Bitmap(filePath);
											sundaySource.TinyLogo = dailySource.TinyLogo;
											sundaySource.TinyLogoFileName = dailySource.TinyLogoFileName;
											break;
										case "DailyCirculation":
											if (double.TryParse(attribute.Value, out tempDouble))
												dailySource.Delivery = tempDouble;
											break;
										case "DailyReadership":
											if (double.TryParse(attribute.Value, out tempDouble))
												dailySource.Readership = tempDouble;
											break;
										case "SundayCirculation":
											if (double.TryParse(attribute.Value, out tempDouble))
												sundaySource.Delivery = tempDouble;
											break;
										case "SundayReadership":
											if (double.TryParse(attribute.Value, out tempDouble))
												sundaySource.Readership = tempDouble;
											break;
										case "AllowSundaySelect":
											if (bool.TryParse(attribute.Value, out tempBool))
											{
												sundaySource.AllowSundaySelect = tempBool;
												dailySource.AllowSundaySelect = tempBool;
											}
											break;
										case "AllowMondaySelect":
											if (bool.TryParse(attribute.Value, out tempBool))
											{
												sundaySource.AllowMondaySelect = tempBool;
												dailySource.AllowMondaySelect = tempBool;
											}
											break;
										case "AllowTuesdaySelect":
											if (bool.TryParse(attribute.Value, out tempBool))
											{
												sundaySource.AllowTuesdaySelect = tempBool;
												dailySource.AllowTuesdaySelect = tempBool;
											}
											break;
										case "AllowWednesdaySelect":
											if (bool.TryParse(attribute.Value, out tempBool))
											{
												sundaySource.AllowWednesdaySelect = tempBool;
												dailySource.AllowWednesdaySelect = tempBool;
											}
											break;
										case "AllowThursdaySelect":
											if (bool.TryParse(attribute.Value, out tempBool))
											{
												sundaySource.AllowThursdaySelect = tempBool;
												dailySource.AllowThursdaySelect = tempBool;
											}
											break;
										case "AllowFridaySelect":
											if (bool.TryParse(attribute.Value, out tempBool))
											{
												sundaySource.AllowFridaySelect = tempBool;
												dailySource.AllowFridaySelect = tempBool;
											}
											break;
										case "AllowSaturdaySelect":
											if (bool.TryParse(attribute.Value, out tempBool))
											{
												sundaySource.AllowSaturdaySelect = tempBool;
												dailySource.AllowSaturdaySelect = tempBool;
											}
											break;
									}
								}
								PublicationSources.Add(dailySource);
								PublicationSources.Add(sundaySource);
								break;
							case "AdSize":
								var adSize = new NameCodePair();
								foreach (XmlAttribute attribute in childeNode.Attributes)
									switch (attribute.Name)
									{
										case "Name":
											adSize.Name = attribute.Value;
											break;
										case "Group":
											adSize.Code = attribute.Value;
											break;
									}
								PageSizes.Add(adSize);
								break;
							case "Note":
								var note = new NameCodePair();
								foreach (XmlAttribute attribute in childeNode.Attributes)
									switch (attribute.Name)
									{
										case "Value":
											note.Name = attribute.Value;
											break;
										case "Code":
											note.Code = attribute.Value;
											break;
									}
								if (!Notes.Select(x => x.Name).Contains(note.Name))
									Notes.Add(note);
								break;
							case "SelectedNotesBorderValue":
								if (int.TryParse(childeNode.InnerText, out tempInt))
									SelectedCommentsBorderValue = tempInt;
								break;
							case "Header":
								foreach (XmlAttribute attribute in childeNode.Attributes)
									switch (attribute.Name)
									{
										case "Value":
											if (!OutputHeaders.Contains(attribute.Value))
												OutputHeaders.Add(attribute.Value);
											break;
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
							case "Section":
								var section = new NameCodePair();
								foreach (XmlAttribute attribute in childeNode.Attributes)
									switch (attribute.Name)
									{
										case "Name":
											section.Name = attribute.Value;
											break;
										case "Abbreviation":
											section.Code = attribute.Value;
											break;
									}
								Sections.Add(section);
								break;
							case "SelectedSectionsBorderValue":
								if (int.TryParse(childeNode.InnerText, out tempInt))
									SelectedSectionsBorderValue = tempInt;
								break;
							case "Mechanicals":
								var mechanicalType = new MechanicalType();
								foreach (XmlAttribute attribute in childeNode.Attributes)
									switch (attribute.Name)
									{
										case "Name":
											mechanicalType.Name = attribute.Value;
											break;
									}
								foreach (XmlNode mechanicalNode in childeNode.ChildNodes)
								{
									var mechanicalItem = new MechanicalItem();
									foreach (XmlAttribute attribute in mechanicalNode.Attributes)
										switch (attribute.Name)
										{
											case "Name":
												mechanicalItem.Name = attribute.Value;
												break;
											case "Value":
												mechanicalItem.Value = attribute.Value;
												break;
										}
									if (mechanicalType.Items.Count(x => x.Name.Equals(mechanicalItem.Name)) == 0)
										mechanicalType.Items.Add(mechanicalItem);
								}
								if (Mechanicals.Count(x => x.Name.Equals(mechanicalType.Name)) == 0)
									Mechanicals.Add(mechanicalType);
								break;
							case "Deadline":
								foreach (XmlAttribute attribute in childeNode.Attributes)
									switch (attribute.Name)
									{
										case "Value":
											if (!Deadlines.Contains(attribute.Value))
												Deadlines.Add(attribute.Value);
											break;
									}
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
							case "ShareUnit":
								var shareUnit = new ShareUnit();
								foreach (XmlAttribute attribute in childeNode.Attributes)
									switch (attribute.Name)
									{
										case "RateCard":
											shareUnit.RateCard = attribute.Value;
											break;
										case "PercentOfPage":
											shareUnit.PercentOfPage = attribute.Value;
											break;
										case "Width":
											shareUnit.Width = attribute.Value;
											break;
										case "WidthMeasureUnit":
											shareUnit.WidthMeasureUnit = attribute.Value;
											break;
										case "Height":
											shareUnit.Height = attribute.Value;
											break;
										case "HeightMeasureUnit":
											shareUnit.HeightMeasureUnit = attribute.Value;
											break;
									}
								ShareUnits.Add(shareUnit);
								break;
							case "DefaultHomeViewSettings":
								DefaultHomeViewSettings.Deserialize(childeNode);
								break;
							case "DefaultPrintScheduleViewSettings":
								DefaultPrintScheduleViewSettings.Deserialize(childeNode);
								break;
							case "DefaultPublicationBasicOverviewSettings":
								DefaultPublicationBasicOverviewSettings.Deserialize(childeNode);
								break;
							case "DefaultPublicationMultiSummarySettings":
								DefaultPublicationMultiSummarySettings.Deserialize(childeNode);
								break;
							case "DefaultSnapshotViewSettings":
								DefaultSnapshotViewSettings.Deserialize(childeNode);
								break;
							case "DefaultDetailedGridColumnState":
								DefaultDetailedGridColumnState.Deserialize(childeNode);
								break;
							case "DefaultDetailedGridAdNotesState":
								DefaultDetailedGridAdNotesState.Deserialize(childeNode);
								break;
							case "DefaultDetailedGridSlideBulletsState":
								DefaultDetailedGridSlideBulletsState.Deserialize(childeNode);
								break;
							case "DefaultDetailedGridSlideHeaderState":
								DefaultDetailedGridSlideHeaderState.Deserialize(childeNode);
								break;
							case "DefaultMultiGridColumnState":
								DefaultMultiGridColumnState.Deserialize(childeNode);
								break;
							case "DefaultMultiGridAdNotesState":
								DefaultMultiGridAdNotesState.Deserialize(childeNode);
								break;
							case "DefaultMultiGridSlideBulletsState":
								DefaultMultiGridSlideBulletsState.Deserialize(childeNode);
								break;
							case "DefaultMultiGridSlideHeaderState":
								DefaultMultiGridSlideHeaderState.Deserialize(childeNode);
								break;
							case "DefaultCalendarViewSettings":
								DefaultCalendarViewSettings.Deserialize(childeNode);
								break;
						}
					}
				}
			}
			Readerships.AddRange(PublicationSources.Select(x => x.Clone() as PrintProductSource));
		}

		private void LoadLists()
		{
			LoadPrintStrategy();
		}
	}

	public class MechanicalType
	{
		public MechanicalType()
		{
			Name = string.Empty;
			Items = new List<MechanicalItem>();
		}

		public string Name { get; set; }
		public List<MechanicalItem> Items { get; set; }
	}

	public class MechanicalItem
	{
		public MechanicalItem()
		{
			Name = string.Empty;
			Value = string.Empty;
			Selected = false;
		}

		public string Name { get; set; }
		public string Value { get; set; }
		public bool Selected { get; set; }
	}

	public class ShareUnit
	{
		public ShareUnit()
		{
			RateCard = string.Empty;
			PercentOfPage = string.Empty;
			Width = string.Empty;
			WidthMeasureUnit = string.Empty;
			Height = string.Empty;
			HeightMeasureUnit = string.Empty;
		}

		public string RateCard { get; set; }
		public string PercentOfPage { get; set; }
		public string Width { get; set; }
		public string WidthMeasureUnit { get; set; }
		public string Height { get; set; }
		public string HeightMeasureUnit { get; set; }

		public string ShortWidthMeasure
		{
			get
			{
				switch (WidthMeasureUnit.ToLower())
				{
					case "columns":
						return " col.";
					case "inches":
						return "''";
					case "depth":
						return " depth";
					default:
						return string.Empty;
				}
			}
		}

		public string ShortHeightMeasure
		{
			get
			{
				switch (HeightMeasureUnit.ToLower())
				{
					case "columns":
						return " col.";
					case "inches":
						return "''";
					case "depth":
						return " depth";
					default:
						return string.Empty;
				}
			}
		}

		public double WidthValue
		{
			get
			{
				double temp;
				if (double.TryParse(Width, out temp))
					return temp;
				return 0;
			}
		}

		public double HeightValue
		{
			get
			{
				double temp;
				if (double.TryParse(Height, out temp))
					return temp;
				return 0;
			}
		}

		public string Dimensions
		{
			get { return !string.IsNullOrEmpty(Width) && !string.IsNullOrEmpty(Height) ? (string.Format("{0}{1} x {2}{3}", new object[] { WidthValue.ToString("#,##0.00"), ShortWidthMeasure, HeightValue.ToString("#,##0.00"), ShortHeightMeasure })) : string.Empty; }
		}
	}
}