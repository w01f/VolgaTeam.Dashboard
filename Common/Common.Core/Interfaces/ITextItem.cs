using Microsoft.Office.Interop.PowerPoint;

namespace Asa.Common.Core.Interfaces
{
	public interface ITextItem
	{
		string SimpleText { get; }
		string FormattedText { get; }
		bool IsEqual(ITextItem targetItem);
		ITextItem Clone();
		TextRange AddTextRange(TextRange beforeRange);
	}
}
