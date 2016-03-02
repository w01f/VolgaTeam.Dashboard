using System;

namespace AdSalesBrowser
{
	public class ClosePageEventArgs : EventArgs
	{
		public IWebPage Page { get; set; }
	}
}
