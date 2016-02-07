using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using Asa.Common.GUI.Properties;

namespace Asa.Common.GUI.Themes
{
	public partial class FormThemeSelector : MetroForm
	{
		private ThemeContainerControl _themeContainer;

		public Theme SelectedTheme
		{
			get { return _themeContainer.SelectedTheme; }
		}

		public FormThemeSelector()
		{
			InitializeComponent();
			laSlideSize.Text = String.Format(laSlideSize.Text, PowerPointManager.Instance.SlideSettings.SizeFormatted);
		}

		public void LoadThemes(IEnumerable<Theme> themes)
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
			_themeContainer.LoadThemes(themes);
			pnMain.Controls.Add(_themeContainer);
			_themeContainer.BringToFront();
		}

		public void SetSelectedTheme(string selectedTheme)
		{
			_themeContainer.SelectTheme(selectedTheme);
			laThemeName.Text = selectedTheme;
		}

		public static void Link(ButtonItem selectorButton, IEnumerable<Theme> themes , string selectedThemeName, Action<Theme> themeSelected)
		{
			var themesExisted = themes.Any();
			selectorButton.ForeColor = Color.Black;
			selectorButton.ImagePosition = eImagePosition.Left;
			selectorButton.Text = String.Empty;
			selectorButton.AutoExpandOnClick = false;
			if (themesExisted)
			{
				var currentTheme = themes.FirstOrDefault(t => t.Name.Equals(selectedThemeName) || String.IsNullOrEmpty(selectedThemeName)) ?? themes.FirstOrDefault();
				if (currentTheme == null) return;
				selectorButton.Image = currentTheme.RibbonLogo;
				((RibbonBar)selectorButton.ContainerControl).Text = String.Format("{0}", currentTheme.Name);
				if (selectorButton.Tag == null || String.IsNullOrEmpty(selectorButton.Tag.ToString()))
					selectorButton.Click += (obj, e) =>
					{
						using (var form = new FormThemeSelector())
						{
							form.LoadThemes(themes);
							form.Shown += (o, args) =>
							{
								form.SetSelectedTheme(((Theme)selectorButton.Tag).Name);
							};
							if (form.ShowDialog() != DialogResult.OK) return;
							var selectedTheme = form.SelectedTheme;
							if (selectedTheme == null) return;
							selectorButton.Image = selectedTheme.RibbonLogo;
							((RibbonBar)selectorButton.ContainerControl).Text = String.Format("{0}", selectedTheme.Name);
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
