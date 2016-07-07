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
			_selectedThemes.Clear();
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
