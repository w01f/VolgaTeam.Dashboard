using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using Asa.Common.Core.Objects.Themes;
using Manina.Windows.Forms;

namespace Asa.Common.GUI.Themes
{
	public class ThemeAdaptor : ImageListView.ImageListViewItemAdaptor
	{
		private readonly List<Theme> _themes = new List<Theme>();

		public ThemeAdaptor(IEnumerable<Theme> themes)
		{
			_themes.AddRange(themes);
		}

		public override Image GetThumbnail(object key, Size size, UseEmbeddedThumbnails useEmbeddedThumbnails, bool useExifOrientation)
		{
			var guid = (Guid) key;
			return _themes.Where(i => i.Identifier == guid).Select(i => (Image)i.BrowseLogo.Clone()).FirstOrDefault();
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
			return _themes.Where(i => i.Identifier == guid).Select(i => i.LocalPath).FirstOrDefault();
		}

		public override Utility.Tuple<ColumnType, string, object>[] GetDetails(Object key)
		{
			return null;
		}

		public override void Dispose()
		{
			_themes.Clear();
		}
	}
}
