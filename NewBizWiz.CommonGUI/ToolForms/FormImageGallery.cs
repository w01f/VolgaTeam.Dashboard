using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Layout;
using NewBizWiz.Core.Common;

namespace NewBizWiz.CommonGUI.ToolForms
{
	public partial class FormImageGallery : Form
	{
		private readonly List<ImageSource> _images = new List<ImageSource>();

		public FormImageGallery(IEnumerable<ImageSource> imageSource)
		{
			InitializeComponent();
			_images.AddRange(imageSource);
			gridControlLogoGallery.DataSource = _images;
		}

		public Image SelectedImage
		{
			get { return SelectedImageSource != null ? SelectedImageSource.BigImage : null; }
			set
			{
				if (value != null)
				{
					var converter = TypeDescriptor.GetConverter(typeof(Bitmap));
					var encodedLogo = Convert.ToBase64String((byte[])converter.ConvertTo(value, typeof(byte[])));
					var selectedLogo = _images.FirstOrDefault(l => l.EncodedBigImage.Equals(encodedLogo));
					if (selectedLogo != null)
					{
						var index = _images.IndexOf(selectedLogo);
						layoutViewLogoGallery.FocusedRowHandle = layoutViewLogoGallery.GetRowHandle(index);
						return;
					}
				}
				layoutViewLogoGallery.FocusedRowHandle = 0;
			}
		}

		public ImageSource SelectedImageSource
		{
			get { return layoutViewLogoGallery.GetFocusedRow() as ImageSource; }
		}

		private void layoutViewLogoGallery_CustomFieldValueStyle(object sender, DevExpress.XtraGrid.Views.Layout.Events.LayoutViewFieldValueStyleEventArgs e)
		{
			var view = sender as LayoutView;
			if (view.FocusedRowHandle == e.RowHandle)
			{
				e.Appearance.BackColor = Color.Orange;
				e.Appearance.BackColor2 = Color.Orange;
			}
		}

		private void layoutViewLogoGallery_DoubleClick(object sender, EventArgs e)
		{
			var layoutView = sender as LayoutView;
			var hitInfo = layoutView.CalcHitInfo(layoutView.GridControl.PointToClient(MousePosition));
			if (hitInfo.InField)
				DialogResult = DialogResult.OK;
		}
	}
}