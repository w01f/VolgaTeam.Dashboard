using System;
using System.Text.RegularExpressions;
using AdSalesBrowser.Helpers;
using AdSalesBrowser.SalesLibraryExtensions.FileLinks;
using AdSalesBrowser.SalesLibraryExtensions.SlideContent;
using EO.WebBrowser;
using Microsoft.Win32;

namespace AdSalesBrowser.SalesLibraryExtensions
{
	class ExtensionsManager
	{
		public const string ActivateFunctionName = "SalesLibraryExtensions_activate";

		private readonly string _url;

		public bool IsExtensionsActive { get; private set; }
		public SlideContentExtension SlideContentExtension { get; }
		public LinkOpenExtension LinkOpenExtension { get; }

		public ExtensionsManager(string url)
		{
			_url = url;
			SlideContentExtension = new SlideContentExtension();
			LinkOpenExtension = new LinkOpenExtension();
		}

		public void Activate()
		{
			IsExtensionsActive = true;
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
				targetPath.Contains("public_links") ||
				targetPath.StartsWith("shortcuts/getSinglePage", StringComparison.OrdinalIgnoreCase));
		}

		public static void MakeUrlTrusted(string targetUrl)
		{
			const string domainsKeyLocation = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings\ZoneMap\Domains";
			const int trustedSiteZone = 0x2;

			var regexp = new Regex(UrlParseHelper.UrlParseRegExp);
			var targetUrlMatch = regexp.Match(targetUrl);
			var targetDomain = targetUrlMatch.Success && targetUrlMatch.Groups.Count >= 5 ? targetUrlMatch.Groups[4].Value : null;

			try
			{
				RegistryKey key = Registry.CurrentUser.OpenSubKey(domainsKeyLocation, RegistryKeyPermissionCheck.ReadWriteSubTree).CreateSubKey(targetDomain, RegistryKeyPermissionCheck.ReadWriteSubTree);
				key?.SetValue("http", trustedSiteZone, RegistryValueKind.DWord);
			}
			catch { }
		}

		public void OnJavaScriptCall(object sender, JSExtInvokeArgs e)
		{
			switch (e.FunctionName)
			{
				case ActivateFunctionName:
					Activate();
					break;
				default:
					SlideContentExtension.OnJavaScriptCall(sender, e);
					LinkOpenExtension.OnJavaScriptCall(sender, e);
					break;
			}
		}

	}
}
