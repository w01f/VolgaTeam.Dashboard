using System;
using System.Text.RegularExpressions;

namespace AdSalesBrowser.Helpers
{
	static class YouTubeHelper
	{
		public static bool IsUrlYouTube(string targetUrl)
		{
			var regexp = new Regex(UrlParseHelper.UrlParseRegExp);

			var urlMatch = regexp.Match(targetUrl);

			var domain = urlMatch.Success && urlMatch.Groups.Count >= 5 ? urlMatch.Groups[4].Value : null;

			return "youtube.com".Equals(domain, StringComparison.OrdinalIgnoreCase);
		}
	}
}
