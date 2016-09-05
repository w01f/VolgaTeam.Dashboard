using System;

namespace AdSalesBrowser.SalesLibraryExtensions.LinkViewContent
{
	public interface IPrintableContent
	{
		string PrintableFileUrl { get; }
		int? CurrentPage { get; }
	}
}
