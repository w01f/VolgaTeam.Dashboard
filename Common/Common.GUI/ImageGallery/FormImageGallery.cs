using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;
using DevExpress.Utils;

namespace Asa.Common.GUI.ImageGallery
{
	public partial class FormImageGallery : MetroForm
	{
		private readonly IEnumerable<ImageSourceGroup> _imageGroups;

		public FormImageGallery(IEnumerable<ImageSourceGroup> imageGroups)
		{
			_imageGroups = imageGroups;
			InitializeComponent();

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}

		public Image SelectedImage { get; private set; }

		public ImageSource SelectedImageSource { get; private set; }

		private void OnGroupDoubleClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}

		private void FormImageGallery_Load(object sender, EventArgs e)
		{
			xtraTabControlGroups.TabPages.Clear();
			foreach (var imageGroup in _imageGroups)
			{
				var tabPage = new ImageGroupPage(imageGroup);
				tabPage.DoubleClicked += OnGroupDoubleClick;
				xtraTabControlGroups.TabPages.Add(tabPage);
			}
			xtraTabControlGroups.ShowTabHeader = _imageGroups.Count() > 1 ? DefaultBoolean.True : DefaultBoolean.False;
		}

		private void FormImageGallery_FormClosed(object sender, FormClosedEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				var selectedPage = xtraTabControlGroups.SelectedTabPage as ImageGroupPage;
				SelectedImage = selectedPage?.SelectedImage;
				SelectedImageSource = selectedPage?.SelectedImageSource;
			}
			foreach (var imageGroup in xtraTabControlGroups.TabPages.OfType<ImageGroupPage>())
				imageGroup.Release();
			xtraTabControlGroups.TabPages.Clear();
		}
	}
}