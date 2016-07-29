using System;
using AdSalesBrowser.SalesLibraryExtensions.FileLinks;
using EO.WebBrowser;

namespace AdSalesBrowser.SalesLibraryExtensions.SlideContent
{
	class SlideContentExtension
	{
		public const string SendLinkDataFunctionName = "SalesLibraryExtensions_sendLinkData";
		public const string ReleaseLinkDataFunctionName = "SalesLibraryExtensions_releaseLinkData";
		public const string SwitchDataFunctionName = "SalesLibraryExtensions_switchPage";

		public SlideContentData CurrentSlideContentLinkData { get; private set; }
		public event EventHandler<EventArgs> ContetChanged;
		public bool ContentEnabled => CurrentSlideContentLinkData != null;

		private void LoadData(params object[] args)
		{
			var format = args[0] as string;
			switch (format)
			{
				case "ppt":
					CurrentSlideContentLinkData = new PowerPointData();
					break;
				case "video":
				case "mp4":
				case "wmv":
					CurrentSlideContentLinkData = new VideoData();
					break;
			}
			CurrentSlideContentLinkData?.Load(args);
			ContetChanged?.Invoke(this, EventArgs.Empty);
		}

		private void ReleaseData()
		{
			CurrentSlideContentLinkData = null;
			ContetChanged?.Invoke(this, EventArgs.Empty);
		}

		private void SwitchDocumentPage(params object[] args)
		{
			CurrentSlideContentLinkData?.SwitchCurrentPart(Int32.Parse(args[0].ToString()));
		}

		public void OnJavaScriptCall(object sender, JSExtInvokeArgs e)
		{
			switch (e.FunctionName)
			{
				case SendLinkDataFunctionName:
					LoadData(e.Arguments);
					break;
				case ReleaseLinkDataFunctionName:
					ReleaseData();
					break;
				case SwitchDataFunctionName:
					SwitchDocumentPage(e.Arguments);
					break;
			}
		}
	}
}
