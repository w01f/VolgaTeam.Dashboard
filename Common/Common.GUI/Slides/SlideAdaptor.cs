using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Asa.Common.Core.Objects.Slides;
using Manina.Windows.Forms;

namespace Asa.Common.GUI.Slides
{
	public class SlideAdaptor : ImageListView.ImageListViewItemAdaptor
	{
		private readonly List<SlideMaster> _slideMasters = new List<SlideMaster>();

		public SlideAdaptor(IEnumerable<SlideMaster> slideMasters)
		{
			_slideMasters.AddRange(slideMasters);
		}

		public override Image GetThumbnail(object key, Size size, UseEmbeddedThumbnails useEmbeddedThumbnails, bool useExifOrientation)
		{
			var guid = (Guid) key;
			return _slideMasters.Where(i => i.Identifier == guid).Select(i => (Image)i.BrowseLogo.Clone()).FirstOrDefault();
		}

		public override string GetUniqueIdentifier(object key, Size size, UseEmbeddedThumbnails useEmbeddedThumbnails,
			bool useExifOrientation)
		{
			var guid = (Guid)key;
			return guid.ToString();
		}

		public override string GetSourceImage(object key)
		{
			var guid = (Guid)key;
			return _slideMasters.Where(i => i.Identifier == guid).Select(i => i.LogoFile.LocalPath).FirstOrDefault();
		}

		public override Utility.Tuple<ColumnType, string, object>[] GetDetails(Object key)
		{
			return null;
		}

		public override void Dispose()
		{
			_slideMasters.Clear();
		}
	}
}
