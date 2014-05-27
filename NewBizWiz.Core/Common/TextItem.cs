using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;

namespace NewBizWiz.Core.Common
{
	public interface ITextItem
	{
		string SimpleText { get; }
		string FormattedText { get; }
		bool IsEqual(ITextItem targetItem);
		ITextItem Clone();
		string Serialize();
		void Deserialize(XmlNode node);
		TextRange AddTextRange(TextRange beforeRange);
	}

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

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<Separator>" + Separator.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Separator>");
			result.AppendLine(@"<BorderLeft>" + BorderLeft.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BorderLeft>");
			result.AppendLine(@"<BorderRight>" + BorderRight.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</BorderRight>");
			foreach (var textItem in Items)
			{
				if (textItem is TextGroup)
					result.AppendLine(@"<TextGroup>" + textItem.Serialize() + @"</TextGroup>");
				else if (textItem is TextItem)
					result.AppendLine(@"<TextItem>" + textItem.Serialize() + @"</TextItem>");
			}
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Separator":
						Separator = childNode.InnerText;
						break;
					case "BorderLeft":
						BorderLeft = childNode.InnerText;
						break;
					case "BorderRight":
						BorderRight = childNode.InnerText;
						break;
					case "TextItem":
						{
							var textItem = new TextItem();
							textItem.Deserialize(childNode);
							Items.Add(textItem);
						}
						break;
					case "TextGroup":
						{
							var textItem = new TextGroup();
							textItem.Deserialize(childNode);
							Items.Add(textItem);
						}
						break;
				}
			}
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

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<Text>" + Text.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Text>");
			result.AppendLine(@"<IsBold>" + IsBold + @"</IsBold>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Text":
						Text = childNode.InnerText;
						break;
					case "IsBold":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								IsBold = temp;
						}
						break;
				}
			}
		}

		public TextRange AddTextRange(TextRange beforeRange)
		{
			var newRange = beforeRange.InsertAfter(Text);
			newRange.Font.Bold = IsBold ? MsoTriState.msoTrue : MsoTriState.msoFalse;
			return newRange;
		}
	}
}
