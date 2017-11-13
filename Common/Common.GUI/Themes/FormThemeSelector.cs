using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Interfaces;
using Asa.Common.Core.Objects.Themes;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using Asa.Common.GUI.Properties;
using DevExpress.Skins;

namespace Asa.Common.GUI.Themes
{
	public partial class FormThemeSelector : MetroForm
	{
		private ThemeContainerControl _themeContainer;

		public Theme SelectedTheme => _themeContainer.SelectedTheme;

		public bool ApplyThemeForAllSlideTypes
		{
			get { return checkEditApplyThemeForAllSlideTypes.Checked; }
			set { checkEditApplyThemeForAllSlideTypes.Checked = value; }
		}

		public FormThemeSelector()
		{
			InitializeComponent();

			Closed += OnFormClosed;

			simpleLabelItemSlideSize.Text = String.Format(simpleLabelItemSlideSize.Text,
				SlideSettingsManager.Instance.SlideSettings.SizeFormatted);

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void OnFormClosed(Object sender, EventArgs e)
		{
			pnMain.Controls.Clear();
			_themeContainer.Release();
		}

		public void LoadThemes(IEnumerable<Theme> themes)
		{
			_themeContainer = new ThemeContainerControl();
			_themeContainer.ThemeChanged += (o, e) =>
			{
				simpleLabelItemThemName.Text = String.Format("<b><size=+4>{0}</size></b>", e.SelectedTheme != null ? e.SelectedTheme.Name : String.Empty);
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
			simpleLabelItemThemName.Text = String.Format("<b><size=+4>{0}</size></b>", selectedTheme);
		}

		public static void Link(
			ButtonItem selectorButton,
			IEnumerable<Theme> themes,
			string selectedThemeName,
			IThemeSettingsContainer settingsContainer,
			Action<Theme, bool> themeSelected)
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
				if (selectorButton.Tag == null)
				{
					selectorButton.Tag = new ThemeButtonInfo();
					selectorButton.Click += (button, e) =>
					{
						((ThemeButtonInfo)((ButtonItem)button).Tag).ClickHandler?.Invoke();
					};
				}
				((ThemeButtonInfo)selectorButton.Tag).CurrentTheme = currentTheme;
				((ThemeButtonInfo)selectorButton.Tag).ClickHandler = () =>
			   {
				   using (var form = new FormThemeSelector())
				   {
					   form.ApplyThemeForAllSlideTypes = settingsContainer.ApplyThemeForAllSlideTypes;
					   form.LoadThemes(themes);
					   form.Shown += (o, args) =>
					   {
						   form.SetSelectedTheme(((ThemeButtonInfo)selectorButton.Tag).CurrentTheme.Name);
					   };
					   if (form.ShowDialog() != DialogResult.OK) return;
					   var selectedTheme = form.SelectedTheme;
					   if (selectedTheme == null) return;
					   selectorButton.Image = selectedTheme.RibbonLogo;
					   ((RibbonBar)selectorButton.ContainerControl).Text = String.Format("{0}", selectedTheme.Name);
					   ((ThemeButtonInfo)selectorButton.Tag).CurrentTheme = selectedTheme;
					   themeSelected(selectedTheme, form.ApplyThemeForAllSlideTypes);
				   }
			   };
			}
			else
				selectorButton.Image = Resources.OutputDisabled;
		}
	}
}
