using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Objects.Themes;
using Manina.Windows.Forms;

namespace Asa.Common.GUI.Themes
{
	[ToolboxItem(false)]
	public partial class ThemeContainerControl : UserControl
	{
		private readonly List<Theme> _themes = new List<Theme>();
		private ThemeAdaptor _themeAdaptor;
		public event EventHandler<ThemeEventArgs> ThemeChanged;
		public event EventHandler<ThemeEventArgs> ThemeSelected;
		public Theme SelectedTheme => themesListView.SelectedItems.Count > 0 ?
					themesListView.SelectedItems.Select(item => item.Tag as Theme).FirstOrDefault() :
					null;

		public ThemeContainerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public void LoadThemes(IEnumerable<Theme> themes)
		{
			_themes.Clear();
			_themes.AddRange(themes);
			var minOrder = _themes.Min(s => s.Order);
			_themeAdaptor = new ThemeAdaptor(_themes);
			themesListView.Items.Clear();
			themesListView.Items.AddRange(
				_themes
					.Select(theme => new ImageListViewItem(theme.Identifier)
					{
						Text = theme.Name,
						Tag = theme,
						Selected = theme.Order == minOrder,
					}).ToArray(),
				_themeAdaptor);
		}

		public void SelectTheme(string themeName)
		{
			themesListView.ClearSelection();
			var itemToSelect = themesListView.Items.FirstOrDefault(item => ((Theme)item.Tag).Name == themeName);
			if(itemToSelect!= null)
				itemToSelect.Selected = true;
		}

		public void Release()
		{
			themesListView.ClearSelection();
			themesListView.Items.Clear();
			_themeAdaptor.Dispose();
			_themeAdaptor = null;
			_themes.Clear();
		}

		private void OnListViewItemDoubleClick(object sender, ItemClickEventArgs e)
		{
			themesListView.ClearSelection();
			e.Item.Selected = true;
			ThemeSelected?.Invoke(this, new ThemeEventArgs { SelectedTheme = (Theme)e.Item.Tag });
		}
		
		private void OnListViewSelectionChanged(object sender, EventArgs e)
		{
			ThemeChanged?.Invoke(this, new ThemeEventArgs { SelectedTheme = SelectedTheme });
		}
	}
}
