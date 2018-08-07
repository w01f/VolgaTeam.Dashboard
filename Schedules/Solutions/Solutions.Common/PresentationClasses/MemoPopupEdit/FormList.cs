using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.XtraGrid.Views.Grid;

namespace Asa.Solutions.Common.PresentationClasses.MemoPopupEdit
{
	public partial class FormList : MetroForm
	{
		private List<ListDataItem> _items;
		private ListDataItem _selectedItem;

		public ListDataItem SelectedItem => gridView.GetFocusedRow() as ListDataItem;

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

		public void LoadData(List<ListDataItem> items, ListDataItem selectedItem)
		{
			_items = items;
			_selectedItem = selectedItem;

			gridControl.DataSource = _items;
		}

		private void OnShown(object sender, EventArgs e)
		{
			var selectedListItem = _items.FirstOrDefault(item => String.Equals(item.Value, _selectedItem.Value, StringComparison.OrdinalIgnoreCase));
			gridView.FocusedRowHandle = _items.IndexOf(selectedListItem);
		}

		private void OnRowClick(object sender, RowClickEventArgs e)
		{
			if (e.Clicks == 2)
				DialogResult = DialogResult.OK;
		}
	}
}