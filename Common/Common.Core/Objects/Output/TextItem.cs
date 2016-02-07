using System;
using Asa.Common.Core.Interfaces;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;

namespace Asa.Common.Core.Objects.Output
{
	public class TextItem : ITextItem
	{
		public string Text { get; private set; }
		public bool IsBold { get; private set; }

		public string SimpleText
		{
			get { return Text; }
		}

		public string FormattedText
		{
			get
			{
				if (String.IsNullOrEmpty(Text)) return Text;
				var formattedText = Text;
				if (IsBold)
					formattedText = String.Format("<b>{0}</b>", formattedText);
				return formattedText;
			}
		}

		public TextItem() { }

		public TextItem(string text, bool isBold)
			: this()
		{
			Text = text;
			IsBold = isBold;
		}

		public bool IsEqual(ITextItem targetItem)
		{
			var textItem = targetItem as TextItem;
			return textItem != null && Text == textItem.Text && IsBold == textItem.IsBold;
		}

		public ITextItem Clone()
		{
			return new TextItem(Text, IsBold);
		}

		public TextRange AddTextRange(TextRange beforeRange)
		{
			var newRange = beforeRange.InsertAfter(Text);
			newRange.Font.Bold = IsBold ? MsoTriState.msoTrue : MsoTriState.msoFalse;
			return newRange;
		}
	}
}
