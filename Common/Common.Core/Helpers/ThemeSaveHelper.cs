using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Themes;

namespace Asa.Common.Core.Helpers
{
	public class ThemeSaveHelper
	{
		private readonly ThemeManager _themeManager;
		private readonly List<SlideType> _availableSlideTypes = new List<SlideType>();
		private readonly Dictionary<SlideType, string> _selectedThemes = new Dictionary<SlideType, string>();

		public ThemeSaveHelper(ThemeManager themeManager, IEnumerable<SlideType> availableSlideTypes)
		{
			_themeManager = themeManager;
			_selectedThemes = new Dictionary<SlideType, string>();
			_availableSlideTypes.AddRange(availableSlideTypes);
		}

		public Theme GetSelectedTheme(SlideType slideType)
		{
			if (!_themeManager.ApprovedThemes.ContainsKey(slideType))
				slideType = SlideType.None;
			var themes = _themeManager.GetThemes(slideType);
			return themes.FirstOrDefault(t => (
				_selectedThemes.ContainsKey(slideType) && t.Name.Equals(_selectedThemes[slideType])) ||
				!_selectedThemes.ContainsKey(slideType));
		}

		public void SetSelectedTheme(SlideType slideType, string themeName, bool applyThemeForAllSlideTypes)
		{
			var processedSlideTypes = applyThemeForAllSlideTypes ? _availableSlideTypes.ToArray() : new[] { slideType };
			foreach (var processedSlideType in processedSlideTypes)
			{
				if (!(_themeManager.ApprovedThemes.ContainsKey(processedSlideType) &&
					_themeManager.ApprovedThemes[processedSlideType].Any(themeInfo => themeInfo.ThemName == themeName)))
					continue;

				if (_selectedThemes.ContainsKey(processedSlideType))
					_selectedThemes[processedSlideType] = themeName;
				else
					_selectedThemes.Add(processedSlideType, themeName);
			}
		}

		public void Deserialize(IEnumerable<XmlNode> nodes)
		{
			_selectedThemes.Clear();
			foreach (var childNode in nodes)
			{
				if (!Enum.TryParse(childNode.Attributes["SlideType"].Value, out SlideType temp) || String.IsNullOrEmpty(childNode.Attributes["Theme"].Value)) continue;
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
