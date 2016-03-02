using System;

namespace AdSalesBrowser
{
	public interface IWebPage
	{
		event EventHandler<NewPageEventArgs> OnNavigateNewPage;
		event EventHandler<ClosePageEventArgs> OnClosePage;
		void Navigate();
	}
}
