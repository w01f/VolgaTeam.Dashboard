using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraTab;
using Manina.Windows.Forms;
using Asa.CommonGUI.FavoriteImages;
using Asa.Core.Common;

namespace Asa.CommonGUI.ImageGallery
{
	//public partial class ImageGroupPage : UserControl
	public partial class ImageGroupPage : XtraTabPage
	{
		private ImageListView.HitInfo _menuHitInfo;
		public List<ImageSource> ImageSources { get; private set; }

		public event EventHandler<EventArgs> DoubleClicked;

		public ImageGroupPage(ImageSourceGroup imageSourceGroup)
		{
			InitializeComponent();
			Text = imageSourceGroup.Name;
			ImageSources = new List<ImageSource>();
			ImageSources.AddRange(imageSourceGroup.Images);
			LoadImages();
		}

		public Image SelectedImage
		{
			get { return SelectedImageSource != null ? SelectedImageSource.BigImage : null; }
		}

		public ImageSource SelectedImageSource
		{
			get
			{
				return imageListView.SelectedItems.Count > 0 ?
					imageListView.SelectedItems.Select(item => item.Tag as ImageSource).FirstOrDefault() :
					null;
			}
		}

		private void LoadImages()
		{
			imageListView.Items.Clear();
			imageListView.Items.AddRange(ImageSources.Select(ims => new ImageListViewItem(ims.FileName, ims.Name) { Tag = ims }).ToArray());
		}

		private void imageListView_ItemDoubleClick(object sender, ItemClickEventArgs e)
		{
			imageListView.ClearSelection();
			e.Item.Selected = true;
			if (DoubleClicked != null)
				DoubleClicked(this, EventArgs.Empty);
		}

		private void imageListView_ItemHover(object sender, ItemHoverEventArgs e)
		{
			toolTip.RemoveAll();
			var sourceItem = e.Item != null ? e.Item.Tag as ImageSource : null;
			if (sourceItem == null) return;
			var toolTipText = Path.GetFileName(sourceItem.FileName);
			toolTip.SetToolTip(imageListView, toolTipText);
		}

		private void imageListView_MouseMove(object sender, MouseEventArgs e)
		{
			imageListView.Focus();
		}

		private void imageListView_MouseDown(object sender, MouseEventArgs e)
		{
			_menuHitInfo = null;
			ImageListView.HitInfo hitInfo;
			imageListView.HitTest(new Point(e.X, e.Y), out hitInfo);
			if (ModifierKeys != Keys.None) return;
			if (!hitInfo.InItemArea) return;
			switch (e.Button)
			{
				case MouseButtons.Right:
					_menuHitInfo = hitInfo;
					contextMenuStrip.Show(MousePosition);
					break;
			}
		}

		private void contextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			e.Cancel = !(_menuHitInfo != null && _menuHitInfo.InItemArea && _menuHitInfo.ItemIndex >= 0);
		}

		private void toolStripMenuItemCopy_Click(object sender, EventArgs e)
		{
			var imageSource = imageListView.Items[_menuHitInfo.ItemIndex].Tag as ImageSource;
			if (imageSource == null || !imageSource.ContainsData) return;
			Clipboard.SetText(String.Format("<ImageSource>{0}</ImageSource>", imageSource.Serialize()), TextDataFormat.Html);
			_menuHitInfo = null;
		}

		private void toolStripMenuItemFavorites_Click(object sender, EventArgs e)
		{
			var imageSource = imageListView.Items[_menuHitInfo.ItemIndex].Tag as ImageSource;
			if (imageSource == null) return;
			var imageName = imageSource.Name;
			using (var form = new FormAddFavoriteImage(imageSource.BigImage, imageName, FavoriteImagesManager.Instance.Images.Select(i => i.Name.ToLower())))
			{
				form.Text = "Add Image to Favorites";
				form.laTitle.Text = "Save this Image in your Favorites folder for future presentations";
				if (form.ShowDialog() != DialogResult.OK) return;
				imageName = form.ImageName;
			}
			FavoriteImagesManager.Instance.SaveImage(imageSource.BigImage, imageName);
			Utilities.Instance.ShowInformation("Image successfully added to Favorites");
			_menuHitInfo = null;
		}
	}
}
