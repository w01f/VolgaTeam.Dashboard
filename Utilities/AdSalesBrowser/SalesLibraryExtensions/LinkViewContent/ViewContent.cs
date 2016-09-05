using System;

namespace AdSalesBrowser.SalesLibraryExtensions.LinkViewContent
{
	abstract class ViewContent
	{
		public abstract LinkContentType ContentType { get; }
		public string OriginalFileName { get; private set; }
		public string OriginalFileUrl { get; private set; }

		public virtual void Load(object[] data)
		{
			OriginalFileName = data[1] as String;
			OriginalFileUrl = data[2] as String;
		}
	}
}
