using System;
using AdSalesBrowser.SalesLibraryExtensions.SlideContent;

namespace AdSalesBrowser.SalesLibraryExtensions
{
	abstract class SlideContentData
	{
		public abstract SlideContentType ContentType { get; }
		public string OriginalFileName { get; private set; }
		public string OriginalFileUrl { get; private set; }

		public abstract void SwitchCurrentPart(int partIndex);

		public abstract string GetPartFileUrl();
		public abstract string GetPartFileName();

		public virtual void Load(object[] data)
		{
			OriginalFileName = data[1] as String;
			OriginalFileUrl = data[2] as String;
		}
	}
}
