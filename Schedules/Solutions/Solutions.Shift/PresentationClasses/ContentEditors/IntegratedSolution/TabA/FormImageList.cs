using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using Manina.Windows.Forms;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.TabA
{
	public partial class FormImageList : MetroForm
	{
		private List<string> _files;
		private string _defaultFilePath;

		public string SelectedFile => imageListView.SelectedItems.Count > 0 ?
			imageListView.SelectedItems.Select(item => item.FileName).FirstOrDefault() :
			null;

		public FormImageList()
		{
			InitializeComponent();

			Height = (Int32)(Screen.PrimaryScreen.Bounds.Height * 0.8);
			Left = (Screen.PrimaryScreen.Bounds.Width - Width) / 2;
			Top = (Screen.PrimaryScreen.Bounds.Height - Height) / 2;

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, scaleFactor);
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, scaleFactor);
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, scaleFactor);
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, scaleFactor);
		}

		public void LoadData(List<string> files, string defaultFilePath)
		{
			_files = files;
			_defaultFilePath = defaultFilePath;

			imageListView.Items.AddRange(_files.Select(file => new ImageListViewItem(file)
			{
				Selected = String.Equals(file, _defaultFilePath, StringComparison.OrdinalIgnoreCase)
			}).ToArray());
		}

		private void OnImageItemDoubleClick(object sender, ItemClickEventArgs e)
		{
			DialogResult = DialogResult.OK;
		}
	}
}