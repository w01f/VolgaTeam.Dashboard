using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Layout;
using NewBizWiz.Core.Common;

namespace NewBizWiz.CommonGUI.Themes
{
	[ToolboxItem(false)]
	public partial class ThemeContainerControl : UserControl
	{
		private readonly List<Theme> _themes = new List<Theme>();
		public Theme SelectedSlide
		{
			get { return layoutViewThemes.GetFocusedRow() as Theme; }
		}

		public ThemeContainerControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
		}

		public void LoadThemes(IEnumerable<Theme> slides)
		{
			_themes.Clear();
			_themes.AddRange(slides);
			gridControlThemes.DataSource = _themes;
		}

		public void SelectTheme(string themeName)
		{
			var themeToSelect = _themes.FirstOrDefault(s => s.Name.Equals(themeName));
			if (themeToSelect == null) return;
			layoutViewThemes.FocusedRowHandle = _themes.IndexOf(themeToSelect);
			layoutViewThemes.VisibleRecordIndex = layoutViewThemes.FocusedRowHandle;
		}

		private void layoutViewThemes_DoubleClick(object sender, System.EventArgs e)
		{
			var layoutView = sender as LayoutView;
			var hitInfo = layoutView.CalcHitInfo(layoutView.GridControl.PointToClient(MousePosition));
			if (!hitInfo.InCard) return;
			var theme = layoutView.GetRow(hitInfo.RowHandle) as Theme;
			if (theme == null) return;
			using (var form = new FormThemePreview())
			{
				form.Text = theme.Name;
				form.pictureBox.Image = theme.Logo;
				form.ShowDialog();
			}
		}

		private void layoutViewThemes_CustomFieldValueStyle(object sender, DevExpress.XtraGrid.Views.Layout.Events.LayoutViewFieldValueStyleEventArgs e)
		{
			var view = sender as LayoutView;
			if (view.FocusedRowHandle == e.RowHandle)
			{
				e.Appearance.BackColor = Color.NavajoWhite;
				e.Appearance.BackColor2 = Color.NavajoWhite;
			}
		}
	}
}
