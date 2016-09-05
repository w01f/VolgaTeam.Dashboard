namespace AdSalesBrowser.SalesLibraryExtensions.LinkViewContent
{
	class ExcelContent : ViewContent, IPrintableContent
	{
		public override LinkContentType ContentType=>LinkContentType.Excel;
		public string PrintableFileUrl => OriginalFileUrl;
		public int? CurrentPage => null;
	}
}
