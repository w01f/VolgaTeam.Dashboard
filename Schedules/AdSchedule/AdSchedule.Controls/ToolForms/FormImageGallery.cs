using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using NewBizWiz.Core.AdSchedule;

namespace NewBizWiz.AdSchedule.Controls.ToolForms
{
	public partial class FormImageGallery : Form
	{
		private readonly List<PrintProductSource> _publicationSources = new List<PrintProductSource>();

		public FormImageGallery()
		{
			InitializeComponent();
		}

		public PrintProductSource SelectedSource
		{
			get
			{
				if (gridViewImageGallery.FocusedRowHandle >= 0)
					return _publicationSources[gridViewImageGallery.GetFocusedDataSourceRowIndex()];
				else
					return null;
			}
		}

		private void FormImageGallery_Load(object sender, EventArgs e)
		{
			_publicationSources.AddRange(ListManager.Instance.PublicationSources.Where(x => x.BigLogo != null && x.SmallLogo != null /*&& x.TinyLogo != null*/).GroupBy(x => x.BigLogoFileName).Select(x => x.First()));
			gridControlImageGallery.DataSource = _publicationSources;
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