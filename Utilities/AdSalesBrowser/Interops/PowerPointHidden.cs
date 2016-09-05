using System;

namespace AdSalesBrowser.Interops
{
	public class PowerPointHidden : PowerPointProcessor, IDisposable
	{
		public void Dispose()
		{
			Disconnect(true);
		}
	}
}
