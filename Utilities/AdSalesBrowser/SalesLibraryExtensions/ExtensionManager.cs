using System;
using System.Text.RegularExpressions;
using AdSalesBrowser.Helpers;

namespace AdSalesBrowser.SalesLibraryExtensions
{
	class ExtensionManager
	{
		public const string ActivateFunctionName = "SalesLibraryExtensions_activate";
		public const string SendLinkDataFunctionName = "SalesLibraryExtensions_sendLinkData";
		public const string ReleaseLinkDataFunctionName = "SalesLibraryExtensions_releaseLinkData";
		public const string SwitchDataFunctionName = "SalesLibraryExtensions_switchPage";

		private string _url;

		public LinkData CurrentLinkData { get; private set; }
		public bool IsExtensionsActive { get; private set; }

		public event EventHandler<EventArgs> DataChanged;

		public bool Enabled => CurrentLinkData != null;

		public void Activate(string url)
		{
			_url = url;
			IsExtensionsActive = true;
		}

		public void LoadData(params object[] args)
		{
			var format = args[0] as string;
			switch (format)
			{
				case "ppt":
					CurrentLinkData = new PowerPointData();
					break;
				case "video":
				case "mp4":
				case "wmv":
					CurrentLinkData = new VideoData();
					break;
			}
			CurrentLinkData?.Load(args);
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		public void ReleaseData()
		{
			CurrentLinkData = null;
			DataChanged?.Invoke(this, EventArgs.Empty);
		}

		public void SwitchDocumentPage(params object[] args)
		{
			CurrentLinkData?.SwitchCurrentPart(Int32.Parse(args[0].ToString()));
		}

		public bool IsUrlExternal(string targetUrl)
		{
			var regexp = new Regex(UrlParseHelper.UrlParseRegExp);

			var currentUrlMatch = regexp.Match(_url);
			var targetUrlMatch = regexp.Match(targetUrl);

			var currentDomain = currentUrlMatch.Success && currentUrlMatch.Groups.Count >= 5 ? currentUrlMatch.Groups[4].Value : null;
			var targetDomain = targetUrlMatch.Success && targetUrlMatch.Groups.Count >= 5 ? targetUrlMatch.Groups[4].Value : null;
			if (String.IsNullOrEmpty(targetDomain))
				return false;
			if (!targetDomain.Equals(currentDomain, StringComparison.OrdinalIgnoreCase))
				return true;
			var targetPath = targetUrlMatch.Success && targetUrlMatch.Groups.Count >= 9 ? targetUrlMatch.Groups[8].Value : null;
			return !String.IsNullOrEmpty(targetPath) &&
				(targetPath.StartsWith("qpage", StringComparison.OrdinalIgnoreCase) ||
				targetPath.StartsWith("shortcuts/getSinglePage", StringComparison.OrdinalIgnoreCase));
		}
	}
}
