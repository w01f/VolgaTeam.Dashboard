using System;
using System.Collections.Generic;
using Asa.Common.Core.Interfaces;
using Asa.Common.Core.Objects.Output;

namespace Asa.Common.Core.Extensions
{
	public static class OutputExtensions
	{
		public static TextGroup Join(this IEnumerable<ITextItem> textItems, string separator = "", string borderLeft = "", string borderRight = "")
		{
			var textGroup = new TextGroup(separator, borderLeft, borderRight);
			textGroup.Items.AddRange(textItems);
			return textGroup;
		}
	}
}
