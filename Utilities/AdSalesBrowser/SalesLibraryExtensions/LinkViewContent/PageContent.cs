using System;
using System.IO;
using System.Linq;
using EO.WebBrowser;

namespace AdSalesBrowser.SalesLibraryExtensions.LinkViewContent
{
	abstract class PageContent : ViewContent, IPrintableContent
	{
		protected string[] _partFileUrls;
		protected int _currentPartIndex;

		public string PrintableFileUrl => OriginalFileUrl;
		public int? CurrentPage => _currentPartIndex;

		public override void Load(object[] data)
		{
			base.Load(data);
			_partFileUrls = (from object item in (JSArray)data[3] select item.ToString()).ToArray();
		}
		
		public void SwitchCurrentPart(int partIndex)
		{
			_currentPartIndex = partIndex;
		}

		public string GetPartFileUrl()
		{
			return _partFileUrls[_currentPartIndex];
		}

		public string GetPartFileName()
		{
			return String.Format("{0}{1}{2}", Path.GetFileNameWithoutExtension(OriginalFileName), (_currentPartIndex + 1), Path.GetExtension(OriginalFileName));
		}
	}
}
