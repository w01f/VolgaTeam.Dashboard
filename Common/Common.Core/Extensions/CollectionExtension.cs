using System.Collections.Generic;

namespace Asa.Common.Core.Extensions
{
	public static class CollectionExtension
	{
		public static T[] Merge<T>(this T[] first, IEnumerable<T> second) where T : class
		{
			var mergedList = new List<T>(first);
			mergedList.AddRange(second);
			return mergedList.ToArray();
		}

		public static T[] Merge<T>(this T[] first, T second) where T : class
		{
			var mergedList = new List<T>(first) { second };
			return mergedList.ToArray();
		}
	}
}
