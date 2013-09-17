using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using DevComponents.DotNetBar;

namespace NewBizWiz.Core.Common
{
	public class ThemeManager
	{
		public List<Theme> Themes { get; private set; }

		public ThemeManager(string rootPath)
		{
			Themes = new List<Theme>();
			foreach (var themeFolder in Directory.GetDirectories(rootPath))
				Themes.Add(new Theme(themeFolder));
			Themes.Sort((x, y) => x.Order.CompareTo(y.Order));
		}

		public void InitThemeControl(ButtonItem parent, string selectedThemeName, Action<Theme> themeSelected)
		{
			if (parent.SubItems.Count == 0)
			{
				parent.ForeColor = Color.Black;
				parent.PopupFont = new Font("Arial", 10, FontStyle.Bold);
				parent.PopupAnimation = ePopupAnimation.None;
				parent.ButtonStyle = eButtonStyle.ImageAndText;
				parent.ImagePosition = eImagePosition.Top;
				parent.BeginGroup = true;
				foreach (var theme in Themes)
				{
					var themeButton = new ButtonItem();
					themeButton.Image = theme.Logo;
					themeButton.Text = theme.Name;
					themeButton.ImagePaddingHorizontal = 8;
					themeButton.SubItemsExpandWidth = 14;
					themeButton.ForeColor = Color.Black;
					themeButton.Tag = theme;
					themeButton.Click += (o, e) =>
					{
						var button = o as ButtonItem;
						if (button == null) return;
						var selectedTheme = button.Tag as Theme;
						if (selectedTheme == null) return;
						parent.Image = selectedTheme.Logo;
						parent.Text = String.Format("{0}", selectedTheme.Name);
						themeSelected(selectedTheme);
					};
					parent.SubItems.Add(themeButton);
				}
			}
			var currentTheme = Themes.FirstOrDefault(t => t.Name.Equals(selectedThemeName) || String.IsNullOrEmpty(selectedThemeName));
			if (currentTheme == null)
				currentTheme = Themes.FirstOrDefault();
			parent.Image = currentTheme.Logo;
			parent.Text = String.Format("{0}", currentTheme.Name);
		}
	}

	public class Theme
	{
		public string Name { get; private set; }
		public int Order { get; private set; }
		public Image Logo { get; private set; }
		public string PotFilePath { get; private set; }
		public string ThemeFilePath { get; private set; }

		public Theme(string rootPath)
		{
			var titlePath = Path.Combine(rootPath, "title.txt");
			if (File.Exists(titlePath))
				Name = File.ReadAllText(titlePath).Trim();

			int tempInt;
			if (Int32.TryParse(Path.GetFileName(rootPath), out tempInt))
				Order = tempInt;

			var bigLogoPath = Path.Combine(rootPath, "preview.png");
			if (File.Exists(bigLogoPath))
				Logo = new Bitmap(bigLogoPath);

			PotFilePath = Directory.GetFiles(rootPath, "*.pot").FirstOrDefault();
			ThemeFilePath = Directory.GetFiles(rootPath, "*.thmx").FirstOrDefault();
		}
	}
}
