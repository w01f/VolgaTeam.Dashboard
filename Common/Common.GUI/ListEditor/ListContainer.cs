﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using DevExpress.Skins;
using DevExpress.XtraGrid;
using DevExpress.XtraTab;

namespace Asa.Common.GUI.ListEditor
{
	//public partial class ListContainer<T> : UserControl where T : class
	public partial class ListContainer<T> : XtraTabPage where T : class
	{
		private readonly List<T> _dataSource = new List<T>();
		public ListContainer(string listName, IEnumerable<T> records)
		{
			InitializeComponent();
			Text = listName;
			_dataSource.AddRange(records);
			gridControl.DataSource = _dataSource;

			layoutControlItemAdd.MaxSize = RectangleHelper.ScaleSize(layoutControlItemAdd.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemAdd.MinSize = RectangleHelper.ScaleSize(layoutControlItemAdd.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void repositoryItemButtonEditButtons_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			if (gridView.FocusedRowHandle == GridControl.InvalidRowHandle) return;
			gridView.DeleteRow(gridView.FocusedRowHandle);
		}

		private void buttonXAdd_Click(object sender, EventArgs e)
		{
			_dataSource.Add(Activator.CreateInstance<T>());
			gridControl.DataSource = _dataSource;
			gridView.RefreshData();
		}

		public IEnumerable<T> GetRecords()
		{
			return _dataSource;
		}

		private void gridControl_MouseMove(object sender, MouseEventArgs e)
		{
			gridView.Focus();
		}
	}
}
