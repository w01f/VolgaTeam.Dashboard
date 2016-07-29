using System;

namespace AdSalesBrowser.SalesLibraryExtensions.FileLinks
{
	abstract class BaseLinkData
	{
		public abstract LinkType Type { get; }
		public string FileName { get; private set; }
		public string FileUrl { get; private set; }
		
		public virtual void Load(object[] data)
		{
			FileName = data[1] as String;
			FileUrl = data[2] as String;
		}
	}
}
