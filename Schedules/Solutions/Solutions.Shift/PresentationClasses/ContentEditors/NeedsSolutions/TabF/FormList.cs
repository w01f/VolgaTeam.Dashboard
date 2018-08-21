using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Shift.Configuration.NeedsSolutions;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraGrid.Views.Grid;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.NeedsSolutions.TabF
{
	public partial class FormList : MetroForm
	{
		private List<SolutionsItemInfo> _items;
		private SolutionsItemInfo _defaultItem;

		public SolutionsItemInfo SelectedItem => (gridView.GetFocusedRow() as ListItemModel)?.Parent;

		public FormList()
		{
			InitializeComponent();

			Height = (Int32)(Screen.PrimaryScreen.Bounds.Height * 0.8);
			Left = (Screen.PrimaryScreen.Bounds.Width - Width) / 2;
			Top = (Screen.PrimaryScreen.Bounds.Height - Height) / 2;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);

			gridView.RowHeight = (Int32)(gridView.RowHeight * scaleFactor.Height);

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, scaleFactor);
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, scaleFactor);
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, scaleFactor);
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, scaleFactor);

			Shown += OnShown;
		}

		public void LoadData(List<SolutionsItemInfo> items, SolutionsItemInfo defaultItem, FormListConfiguration formConfiguration)
		{
			_items = items;
			_defaultItem = defaultItem;

			gridView.Appearance.Row.BackColor = formConfiguration.BackgroundColor;
			gridView.Appearance.RowSeparator.BackColor = formConfiguration.BackgroundColor;
			gridView.Appearance.Empty.BackColor = formConfiguration.BackgroundColor;

			gridControl.DataSource = _items
				.Select(item => ListItemModel.FromParent(item, formConfiguration))
				.ToList();
		}

		private void OnShown(object sender, EventArgs e)
		{
			var selectedListItem = _items.FirstOrDefault(item => String.Equals(item.Title, _defaultItem.Title, StringComparison.OrdinalIgnoreCase));
			gridView.FocusedRowHandle = _items.IndexOf(selectedListItem);
		}

		private void OnRowClick(object sender, RowClickEventArgs e)
		{
			if (e.Clicks == 2)
				DialogResult = DialogResult.OK;
		}

		internal class ListItemModel
		{
			public string Text { get; set; }
			public SolutionsItemInfo Parent { get; set; }

			public static ListItemModel FromParent(SolutionsItemInfo parentItem, FormListConfiguration styleConfiguration)
			{
				return new ListItemModel
				{
					Parent = parentItem,
					Text = String.Format("{0}{1}",
						String.Format("<p style=\"font-size: {1}pt; color: {2};\">{0}</p>", 
							parentItem.ButtonHeader,
							styleConfiguration.TopFontSize, 
							styleConfiguration.TopForeColor.ToHex()),
						String.Format("<p style=\"font-size: {2}pt; color: {3};\">{0}&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;({1})</p>", 
							parentItem.SubHeaderDefaultValue,
							parentItem.Title,
							styleConfiguration.BottomFontSize, 
							styleConfiguration.BottomForeColor.ToHex()))
				};
			}
		}
	}
}