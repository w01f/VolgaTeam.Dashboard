using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using NewBizWiz.CommonGUI.Properties;
using NewBizWiz.Core.Common;

namespace NewBizWiz.CommonGUI.Themes
{
	public partial class FormThemeSelector : Form
	{
		private ThemeContainerControl _themeContainer;

		public Theme SelectedTheme
		{
			get { return _themeContainer.SelectedTheme; }
		}

		public FormThemeSelector()
		{
			InitializeComponent();
			laSlideSize.Text = String.Format(laSlideSize.Text, SettingsManager.Instance.Size);
		}

		public void LoadThemes(ThemeManager themeManager)
		{
			_themeContainer = new ThemeContainerControl();
			_themeContainer.ThemeChanged += (o, e) =>
			{
				laThemeName.Text = e.SelectedTheme != null ? e.SelectedTheme.Name : String.Empty;
			};
			_themeContainer.ThemeSelected += (o, e) =>
			{
				DialogResult = DialogResult.OK;
				Close();
			};
			_themeContainer.LoadThemes(themeManager.Themes);
			pnMain.Controls.Add(_themeContainer);
			_themeContainer.BringToFront();
		}

		public void SetSelectedTheme(string selectedTheme)
		{
			_themeContainer.SelectTheme(selectedTheme);
			laThemeName.Text = selectedTheme;
		}

		public static void Link(ButtonItem selectorButton, ThemeManager themeManager, string selectedThemeName, Action<Theme> themeSelected)
		{
			var themesExisted = themeManager.Themes.Any();
			selectorButton.ForeColor = Color.Black;
			selectorButton.ImagePosition = eImagePosition.Left;
			selectorButton.BeginGroup = themesExisted;
			selectorButton.Text = String.Empty;
			selectorButton.AutoExpandOnClick = false;
			if (themesExisted)
			{
				var currentTheme = themeManager.Themes.FirstOrDefault(t => t.Name.Equals(selectedThemeName) || String.IsNullOrEmpty(selectedThemeName)) ?? themeManager.Themes.FirstOrDefault();
				if (currentTheme == null) return;
				selectorButton.Image = currentTheme.RibbonLogo;
				(selectorButton.ContainerControl as RibbonBar).Text = String.Format("{0}", currentTheme.Name);
				if (selectorButton.Tag == null || String.IsNullOrEmpty(selectorButton.Tag.ToString()))
					selectorButton.Click += (obj, e) =>
					{
						using (var form = new FormThemeSelector())
						{
							form.LoadThemes(themeManager);
							form.Shown += (o, args) =>
							{
								form.SetSelectedTheme((selectorButton.Tag as Theme).Name);
							};
							if (form.ShowDialog() != DialogResult.OK) return;
							var selectedTheme = form.SelectedTheme;
							if (selectedTheme == null) return;
							selectorButton.Image = selectedTheme.RibbonLogo;
							(selectorButton.ContainerControl as RibbonBar).Text = String.Format("{0}", selectedTheme.Name);
							selectorButton.Tag = selectedTheme;
							themeSelected(selectedTheme);
						}
					};
				selectorButton.Tag = currentTheme;
			}
			else
				selectorButton.Image = Resources.OutputDisabled;
		}
	}
}
