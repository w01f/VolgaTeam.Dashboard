using System;
using System.Linq;
using WebDAVClient.Model;

namespace Asa.Common.Core.Extensions
{
	public static class CommonExtensions
	{
		public static string GetName(this Item item)
		{
			return item.Href.Split(@"/"[0]).LastOrDefault(part => !String.IsNullOrEmpty(part));
		}
	}
}
