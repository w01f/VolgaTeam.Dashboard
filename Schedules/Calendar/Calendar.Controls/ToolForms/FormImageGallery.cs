using System;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using Manina.Windows.Forms;
using NewBizWiz.Core.Common;
using ListManager = NewBizWiz.Core.AdSchedule.ListManager;

namespace NewBizWiz.Calendar.Controls.ToolForms
{
	public partial class FormImageGallery : MetroForm
	{
		public FormImageGallery()
		{
			InitializeComponent();
		}

		public ImageSource SelectedSource
		{
			get { return imageListView.SelectedItems.Select(item => item.Tag as ImageSource).FirstOrDefault(); }
		}

		private void FormImageGallery_Load(object sender, EventArgs e)
		{
			imageListView.Items.Clear();
			imageListView.Items.AddRange(ListManager.Instance.Images.SelectMany(g => g.Images).Select(ims => new ImageListViewItem(ims.FileName, ims.Name) { Tag = ims }).ToArray());
		}

		private void imageListView_ItemDoubleClick(object sender, Manina.Windows.Forms.ItemClickEventArgs e)
		{
			imageListView.ClearSelection();
			e.Item.Selected = true;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void imageListView_MouseMove(object sender, MouseEventArgs e)
		{
			imageListView.Focus();
		}
	}
}