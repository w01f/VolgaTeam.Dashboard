using System;
using System.Collections.Generic;
using System.Linq;
using Asa.Common.Core.Interfaces;
using Microsoft.Office.Interop.PowerPoint;

namespace Asa.Common.Core.Objects.Output
{
	public class TextGroup : ITextItem
	{
		public string Separator { get; private set; }
		public string BorderLeft { get; private set; }
		public string BorderRight { get; private set; }
		public List<ITextItem> Items { get; private set; }

		public TextGroup()
		{
			Items = new List<ITextItem>();
			Separator = " ";
		}

		public TextGroup(string separator, string borderLeft = "", string borderRight = "")
			: this()
		{
			Separator = separator;
			BorderLeft = borderLeft;
			BorderRight = borderRight;
		}

		public string SimpleText
		{
			get
			{
				var text = String.Join(Separator, Items.Select(it => it.SimpleText));
				return String.Format("{0}{1}{2}", BorderLeft, text, BorderRight);
			}
		}

		public string FormattedText
		{
			get
			{
				var text = String.Join(Separator, Items.Select(it => it.FormattedText));
				return String.Format("{0}{1}{2}", BorderLeft, text, BorderRight);
			}
		}

		public bool IsEqual(ITextItem targetItem)
		{
			var targetGroup = targetItem as TextGroup;
			if (targetGroup == null) return false;
			if (Separator != targetGroup.Separator) return false;
			if (Items.Count != targetGroup.Items.Count) return false;
			return Items.Select((t, i) => t.IsEqual(targetGroup.Items[i])).All(equal => equal);
		}

		public ITextItem Clone()
		{
			var textGroup = new TextGroup(Separator, BorderLeft, BorderRight);
			textGroup.Items.AddRange(Items.Select(i => i.Clone()));
			return textGroup;
		}

		public TextRange AddTextRange(TextRange beforeRange)
		{
			var lastRange = beforeRange;
			if (!String.IsNullOrEmpty(BorderLeft))
				lastRange = beforeRange.InsertAfter(BorderLeft);
			var lastItemIndex = Items.Count - 1;
			for (var i = 0; i <= lastItemIndex; i++)
			{
				lastRange = Items[i].AddTextRange(lastRange);
				if (!String.IsNullOrEmpty(Separator) && i < lastItemIndex)
					lastRange = beforeRange.InsertAfter(Separator);
			}
			if (!String.IsNullOrEmpty(BorderRight))
				lastRange = beforeRange.InsertAfter(BorderRight);
			return lastRange;
		}
	}
}
