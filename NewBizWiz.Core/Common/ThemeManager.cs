using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace Asa.Core.Common
{
	public class ThemeManager
	{
		private readonly List<Theme> _themes = new List<Theme>();

		public Dictionary<SlideType, List<string>> ApprovedThemes { get; private set; }

		public event EventHandler<EventArgs> ThemesChanged;

		public ThemeManager()
		{
			ApprovedThemes = new Dictionary<SlideType, List<string>>();
		}

		private void LoadApprovedThemes(StorageDirectory root)
		{
			var contentFile = new StorageFile(root.GetParentFolder().RelativePathParts.Merge("ApprovedThemes.xml"));

			if (!contentFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(contentFile.LocalPath);

			foreach (var slideNode in document.SelectNodes(@"//Root/Slide").OfType<XmlNode>())
			{
				var slideAttribute = slideNode.Attributes["Name"];
				if (slideAttribute == null) continue;
				var slideType = SlideType.None;
				switch (slideAttribute.Value)
				{
					#region Dashboard
					case "Cleanslate":
						slideType = SlideType.Cleanslate;
						break;
					case "Cover":
						slideType = SlideType.Cover;
						break;
					case "Intro":
						slideType = SlideType.LeadoffStatement;
						break;
					case "NeedsAnalysis":
						slideType = SlideType.ClientGoals;
						break;
					case "TargetCustomers":
						slideType = SlideType.TargetCustomers;
						break;
					case "ClosingSummary":
						slideType = SlideType.SimpleSummary;
						break;
					#endregion

					#region Ad Schedule
					case "PrintWebPackage":
						slideType = SlideType.PrintWebPackage;
						break;
					case "PrintDigitalProduct":
						slideType = SlideType.PrintDigitalProduct;
						break;
					case "PrintBasicOverview":
						slideType = SlideType.PrintBasicOverview;
						break;
					case "PrintMultiSummary":
						slideType = SlideType.PrintMultiSummary;
						break;
					case "PrintSnapshot":
						slideType = SlideType.PrintSnapshot;
						break;
					case "PrintDetailedGrid":
						slideType = SlideType.PrintDetailedGrid;
						break;
					case "PrintMultiGrid":
						slideType = SlideType.PrintMultiGrid;
						break;
					case "PrintAdPlan":
						slideType = SlideType.PrintAdPlan;
						break;
					#endregion

					#region Online Schedule
					case "OnlineDigitalProduct":
						slideType = SlideType.OnlineDigitalProduct;
						break;
					case "OnlineWebPackage":
						slideType = SlideType.OnlineWebPackage;
						break;
					case "OnlineAdPlan":
						slideType = SlideType.OnlineAdPlan;
						break;
					#endregion

					#region TV Schedule
					case "TVDigitalProduct":
						slideType = SlideType.TVDigitalProduct;
						break;
					case "TVWebPackage":
						slideType = SlideType.TVWebPackage;
						break;
					case "TVWeeklySchedule":
						slideType = SlideType.TVWeeklySchedule;
						break;
					case "TVMonthlySchedule":
						slideType = SlideType.TVMonthlySchedule;
						break;
					case "TVSnapshot":
						slideType = SlideType.TVSnapshot;
						break;
					case "TVOptions":
						slideType = SlideType.TVOptions;
						break;
					#endregion

					#region Radio Schedule
					case "RadioDigitalProduct":
						slideType = SlideType.RadioDigitalProduct;
						break;
					case "RadioWebPackage":
						slideType = SlideType.RadioWebPackage;
						break;
					case "RadioWeeklySchedule":
						slideType = SlideType.RadioWeeklySchedule;
						break;
					case "RadioMonthlySchedule":
						slideType = SlideType.RadioMonthlySchedule;
						break;
					case "RadioSnapshot":
						slideType = SlideType.RadioSnapshot;
						break;
					case "RadioOptions":
						slideType = SlideType.RadioOptions;
						break;
					#endregion

					case "WebQuick":
						slideType = SlideType.WebQuick;
						break;

					#region Common
					case "Summary1":
						slideType = SlideType.Summary1;
						break;
					case "Summary2":
						slideType = SlideType.Summary2;
						break;
					case "Strategy":
						slideType = SlideType.Strategy;
						break;
					#endregion
				}
				if (slideType == SlideType.None) continue;
				foreach (var themeNode in slideNode.SelectNodes("Theme").OfType<XmlNode>())
				{
					if (!ApprovedThemes.ContainsKey(slideType))
						ApprovedThemes.Add(slideType, new List<string>());
					ApprovedThemes[slideType].Add(themeNode.InnerText);
				}
			}
		}

		public void Load()
		{
			_themes.Clear();
			var storageDirectory = new StorageDirectory(ResourceManager.Instance.ThemesFolder.RelativePathParts.Merge(PowerPointManager.Instance.SlideSettings.SlideMasterFolder));
			if (!storageDirectory.ExistsLocal()) return;

			LoadApprovedThemes(storageDirectory);

			foreach (var themeFolder in storageDirectory.GetLocalFolders())
			{
				var theme = new Theme(themeFolder);
				theme.Load();
				foreach (var approvedTheme in ApprovedThemes.Where(approvedTheme => approvedTheme.Value.Any(t => t.Equals(theme.Name))))
					theme.ApprovedSlides.Add(approvedTheme.Key);
				_themes.Add(theme);
			}
			_themes.Sort((x, y) => x.Order.CompareTo(y.Order));

			if (ThemesChanged != null)
				ThemesChanged(this, EventArgs.Empty);
		}

		public IEnumerable<Theme> GetThemes(SlideType slideType)
		{
			return _themes.Where(t => t.ApprovedSlides.Contains(slideType) || !ApprovedThemes.ContainsKey(slideType));
		}
	}

	public class Theme
	{
		private readonly StorageDirectory _root;
		private StorageFile _themeFile;

		public string Name { get; private set; }
		public int Order { get; private set; }
		public Image Logo { get; private set; }
		public Image BrowseLogo { get; private set; }
		public Image RibbonLogo { get; private set; }
		public List<SlideType> ApprovedSlides { get; private set; }

		public Theme(StorageDirectory root)
		{
			_root = root;
		}

		public override string ToString()
		{
			return Name;
		}

		public void Load()
		{
			var files = _root.GetLocalFiles().ToList();

			var titleFile = files.First(file => file.Name == "title.txt");
			Name = File.ReadAllText(titleFile.LocalPath).Trim();

			int tempInt;
			if (Int32.TryParse(Path.GetFileName(_root.LocalPath), out tempInt))
				Order = tempInt;

			var bigLogoFile = files.FirstOrDefault(file => file.Name == "preview.png");
			if (bigLogoFile != null)
			{
				Logo = new Bitmap(bigLogoFile.LocalPath);
				BrowseLogo = Logo.GetThumbnailImage(((Logo.Width * 144) / Logo.Height) + 10, 144, null, IntPtr.Zero);
				var borderedLogo = Logo.DrawBorder();
				RibbonLogo = borderedLogo.GetThumbnailImage(((borderedLogo.Width * 72) / borderedLogo.Height) + 10, 72, null, IntPtr.Zero);
			}

			_themeFile = files.FirstOrDefault(file => file.Extension == ".thmx");

			ApprovedSlides = new List<SlideType>();
		}

		public string GetThemePath()
		{
			return _themeFile.LocalPath;
		}
	}

	public class ThemeEventArgs : EventArgs
	{
		public Theme SelectedTheme { get; set; }
	}

	public class ThemeSaveHelper
	{
		private readonly ThemeManager _themeManager;
		private readonly Dictionary<SlideType, string> _selectedThemes = new Dictionary<SlideType, string>();

		public ThemeSaveHelper(ThemeManager themeManager)
		{
			_themeManager = themeManager;
			_selectedThemes = new Dictionary<SlideType, string>();
		}

		public Theme GetSelectedTheme(SlideType slideType)
		{
			if (!_themeManager.ApprovedThemes.ContainsKey(slideType))
				slideType = SlideType.None;
			var themes = _themeManager.GetThemes(slideType);
			return themes.FirstOrDefault(t => (_selectedThemes.ContainsKey(slideType) && t.Name.Equals(_selectedThemes[slideType])) || !_selectedThemes.ContainsKey(slideType));
		}

		public void SetSelectedTheme(SlideType slideType, string themeName)
		{
			if (!_themeManager.ApprovedThemes.ContainsKey(slideType))
				slideType = SlideType.None;

			if (_selectedThemes.ContainsKey(slideType))
				_selectedThemes[slideType] = themeName;
			else
				_selectedThemes.Add(slideType, themeName);
		}

		public void Deserialize(IEnumerable<XmlNode> nodes)
		{
			foreach (var childNode in nodes)
			{
				SlideType temp;
				if (!Enum.TryParse(childNode.Attributes["SlideType"].Value, out temp) || String.IsNullOrEmpty(childNode.Attributes["Theme"].Value)) continue;
				var themeName = childNode.Attributes["Theme"].Value;
				var availableThemsForSlideType = _themeManager.GetThemes(temp);
				if (availableThemsForSlideType.Any(t => t.Name.Equals(themeName)))
					_selectedThemes.Add(temp, themeName);
			}
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			foreach (var selectedTheme in _selectedThemes)
				result.AppendLine(String.Format("<SelectedTheme SlideType=\"{0}\" Theme=\"{1}\"/>", selectedTheme.Key, selectedTheme.Value));
			return result.ToString();
		}
	}
}
