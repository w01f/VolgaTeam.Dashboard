using System;

namespace AdSalesBrowser.WebPage
{
	public class ClosePageEventArgs : EventArgs
	{
		public WebKitPage Page { get; set; }
	}
}
