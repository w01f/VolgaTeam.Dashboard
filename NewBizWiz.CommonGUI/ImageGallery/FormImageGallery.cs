using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Utils;
using NewBizWiz.Core.Common;

namespace NewBizWiz.CommonGUI.ImageGallery
{
	public partial class FormImageGallery : MetroForm
	{
		public FormImageGallery(IEnumerable<ImageSourceGroup> imageGroups)
		{
			InitializeComponent();
			xtraTabControlGroups.TabPages.Clear();
			foreach (var imageGroup in imageGroups)
			{
				var tabPage = new ImageGroupPage(imageGroup);
				tabPage.DoubleClicked += OnGroupDoubleClick;
				xtraTabControlGroups.TabPages.Add(tabPage);
			}
			xtraTabControlGroups.ShowTabHeader = imageGroups.Count() > 1 ? DefaultBoolean.True : DefaultBoolean.False;
		}

		protected ImageGroupPage SelectedPage
		{
			get { return xtraTabControlGroups.SelectedTabPage as ImageGroupPage; }
		}

		public Image SelectedImage
		{
			get { return SelectedPage != null ? SelectedPage.SelectedImage : null; }
			set
			{
				if (value == null) return;
				var converter = TypeDescriptor.GetConverter(typeof(Bitmap));
				var encodedLogo = Convert.ToBase64String((byte[])converter.ConvertTo(value, typeof(byte[])));
				foreach (var imageSourceGroup in xtraTabControlGroups.TabPages.OfType<ImageGroupPage>())
				{
					var selectedLogo = imageSourceGroup.ImageSources.FirstOrDefault(l => l.EncodedBigImage.Equals(encodedLogo));
					if (selectedLogo != null)
					{
						var index = imageSourceGroup.ImageSources.IndexOf(selectedLogo);
						imageSourceGroup.layoutViewLogoGallery.FocusedRowHandle = imageSourceGroup.layoutViewLogoGallery.GetRowHandle(index);
						break;
					}
					return;
				}
			}
		}

		public ImageSource SelectedImageSource
		{
			get { return SelectedPage != null ? SelectedPage.SelectedImageSource : null; }
		}

		private void OnGroupDoubleClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}
	}
}