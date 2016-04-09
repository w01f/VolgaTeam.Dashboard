namespace AdSalesBrowser.SalesLibraryExtensions
{
	abstract class ContentLinkData : LinkData
	{
		public abstract void SwitchCurrentPart(int partIndex);

		public abstract string GetPartFileUrl();
		public abstract string GetPartFileName();
	}
}
