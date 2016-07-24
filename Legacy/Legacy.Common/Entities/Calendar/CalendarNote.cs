using System;
using System.Drawing;
using System.Xml;
using Asa.Legacy.Common.Entities.Common;

namespace Asa.Legacy.Common.Entities.Calendar
{
	public abstract class CalendarNote
	{
		public static Color DefaultBackgroundColor = Color.LemonChiffon;

		protected ITextItem _note;
		protected Color _backgroundColor;

		public DateTime StartDay { get; set; }
		public DateTime FinishDay { get; set; }
		public bool UserAdded { get; set; }

		public virtual ITextItem Note
		{
			get { return _note; }
			set { _note = value; }
		}

		public virtual Color BackgroundColor
		{
			get { return _backgroundColor; }
			set { _backgroundColor = value; }
		}

		public int Length => FinishDay.Subtract(StartDay).Days;

		public Color ForeColor
		{
			get
			{
				var d = 0;
				var a = 1 - (0.299 * BackgroundColor.R + 0.587 * BackgroundColor.G + 0.114 * BackgroundColor.B) / 255;
				d = a < 0.5 ? 0 : 255;
				return Color.FromArgb(d, d, d);
			}
		}

		#region Output Data

		public float Top { get; set; }
		public float Bottom { get; set; }
		public float Left { get; set; }
		public float Right { get; set; }
		public float StaticHeight { get; set; }
		public float StaticWidth { get; set; }

		#endregion

		protected CalendarNote()
		{
			_backgroundColor = DefaultBackgroundColor;
			StaticWidth = StaticHeight = 25f;
		}

		public virtual void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				DateTime tempDate;
				switch (childNode.Name)
				{
					case "StartDay":
						if (DateTime.TryParse(childNode.InnerText, out tempDate))
							StartDay = tempDate;
						break;
					case "FinishDay":
						if (DateTime.TryParse(childNode.InnerText, out tempDate))
							FinishDay = tempDate;
						break;
					case "BackgroundColor":
						int tempInt;
						if (Int32.TryParse(childNode.InnerText, out tempInt))
							_backgroundColor = Color.FromArgb(tempInt);
						break;
					case "TextItem":
						{
							_note = new TextItem();
							_note.Deserialize(childNode);
						}
						break;
					case "TextGroup":
						{
							_note = new TextGroup();
							_note.Deserialize(childNode);
						}
						break;
				}
			}
		}
	}
}
