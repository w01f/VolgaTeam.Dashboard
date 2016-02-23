using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Asa.Common.Core.Objects.Images;
using Manina.Windows.Forms;

namespace Asa.Common.GUI.ImageGallery
{
	public class ImageSourceAdaptor : ImageListView.ImageListViewItemAdaptor
	{
		private readonly List<ImageSource> _imageSources = new List<ImageSource>();

		public ImageSourceAdaptor(IEnumerable<ImageSource> imageSources)
		{
			_imageSources.AddRange(imageSources);
		}

		public override Image GetThumbnail(object key, Size size, UseEmbeddedThumbnails useEmbeddedThumbnails, bool useExifOrientation,
			bool useWIC)
		{
			var guid = (Guid) key;
			return _imageSources.Where(i => i.Identifier == guid).Select(i => (Image)i.TinyImage.Clone()).FirstOrDefault();
		}

		public override string GetUniqueIdentifier(object key, Size size, UseEmbeddedThumbnails useEmbeddedThumbnails,
			bool useExifOrientation, bool useWIC)
		{
			var guid = (Guid)key;
			return guid.ToString();
		}

		public override string GetSourceImage(object key)
		{
			var guid = (Guid)key;
			return _imageSources.Where(i => i.Identifier == guid).Select(i => i.FileName).FirstOrDefault();
		}

		public override Utility.Tuple<ColumnType, string, object>[] GetDetails(Object key, bool useWIC)
		{
			return null;
		}

		public override void Dispose()
		{
			_imageSources.Clear();
		}
	}
}
