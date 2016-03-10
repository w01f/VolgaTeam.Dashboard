using System;
using System.Drawing;
using System.Xml;
using Asa.Legacy.Common.Entities.Calendar;
using Asa.Legacy.Common.Entities.Common;

namespace Asa.Legacy.Media.Entities.Calendar
{
	public class MediaDataNote : CalendarNote
	{
		public TextGroup MediaData { get; set; }
		public bool EditedByUser { get; private set; }
		public bool Splitted { get; set; }

		public override ITextItem Note
		{
			get { return _note ?? MediaData; }
		}

		public override Color BackgroundColor
		{
			get { return _backgroundColor; }
			set
			{
				_backgroundColor = value;
				EditedByUser = EditedByUser || _backgroundColor != DefaultBackgroundColor;
			}
		}

		public MediaDataNote() { }

		public override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "EditedByUser":
						{
							bool temp;
							if (Boolean.TryParse(childNode.InnerText, out temp))
								EditedByUser = temp;
						}
						break;
				}
			}
		}
	}
}
