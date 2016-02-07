using System.Collections.Generic;
using System.Linq;
using Asa.Business.Online.Entities.NonPersistent;
using Asa.Common.Core.Helpers;

namespace Asa.Business.Online.Common
{
	static class ProdutInfoHelper
	{
		public static IEnumerable<ProductInfo> MergeSet(this IEnumerable<ProductInfo> originalSet, IEnumerable<ProductInfo> customSet)
		{
			return originalSet.Select(o =>
			{
				var newItem = o.Clone<ProductInfo, ProductInfo>();
				newItem.ApplyValues(customSet.FirstOrDefault(c => c.Key == o.Key && c.Type == o.Type));
				return newItem;
			});
		}
	}
}
