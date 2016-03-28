using System;

namespace AdSalesBrowser.WebPage
{
	public class ClosePageEventArgs : EventArgs
	{
		public WebKitPage Page { get; set; }
		public bool NeedReleasePage { get; set; }
	}
}
