using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.Utils;
using Asa.Core.Common;

namespace Asa.CommonGUI.ImageGallery
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