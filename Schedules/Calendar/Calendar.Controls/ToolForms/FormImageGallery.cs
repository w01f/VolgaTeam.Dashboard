using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Calendar.Controls.ToolForms
{
	public partial class FormImageGallery : Form
	{
		private readonly List<ImageSource> _imageSources = new List<ImageSource>();

		public FormImageGallery()
		{
			InitializeComponent();
		}

		public ImageSource SelectedSource
		{
			get
			{
				if (gridViewImageGallery.FocusedRowHandle >= 0)
					return _imageSources[gridViewImageGallery.GetFocusedDataSourceRowIndex()];
				return null;
			}
		}

		private void FormImageGallery_Load(object sender, EventArgs e)
		{
			_imageSources.AddRange(ListManager.Instance.Images);
			gridControlImageGallery.DataSource = _imageSources;
		}

		private void gridViewImageGallery_RowClick(object sender, RowClickEventArgs e)
		{
			if (e.Clicks > 1)
			{
				DialogResult = DialogResult.OK;
				Close();
			}
		}
	}
}