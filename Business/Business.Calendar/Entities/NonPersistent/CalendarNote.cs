using System;
using System.Drawing;
using Asa.Business.Calendar.Interfaces;
using Asa.Common.Core.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Calendar.Entities.NonPersistent
{
	public abstract class CalendarNote
	{
		public static Color DefaultBackgroundColor = Color.LemonChiffon;

		protected ITextItem _note;
		protected Color _backgroundColor;

		public ICalendarContent Parent { get; private set; }
		public DateTime StartDay { get; set; }
		public DateTime FinishDay { get; set; }
		public bool UserAdded { get; set; }

		public ICalendarContent ParentCalendar
		{
			get { return Parent; }
		}

		[JsonIgnore]
		public virtual ITextItem Note
		{
			get { return _note; }
			set { _note = value; }
		}

		[JsonIgnore]
		public virtual Color BackgroundColor
		{
			get { return _backgroundColor; }
			set { _backgroundColor = value; }
		}

		public int Length
		{
			get { return FinishDay.Subtract(StartDay).Days; }
		}

		public Color ForeColor
		{
			get
			{
				var a = 1 - (0.299 * BackgroundColor.R + 0.587 * BackgroundColor.G + 0.114 * BackgroundColor.B) / 255;
				var d = a < 0.5 ? 0 : 255;
				return Color.FromArgb(d, d, d);
			}
		}

		#region Output Data
		[JsonIgnore]
		public float Top { get; set; }
		[JsonIgnore]
		public float Bottom { get; set; }
		[JsonIgnore]
		public float Left { get; set; }
		[JsonIgnore]
		public float Right { get; set; }
		[JsonIgnore]
		public float StaticHeight { get; set; }
		[JsonIgnore]
		public float StaticWidth { get; set; }

		#endregion

		[JsonConstructor]
		protected CalendarNote()
		{
			_backgroundColor = DefaultBackgroundColor;
			StaticWidth = StaticHeight = 25f;
		}

		protected CalendarNote(ICalendarContent parent)
		{
			Parent = parent;
			_backgroundColor = DefaultBackgroundColor;

			StaticWidth = StaticHeight = 25f;
		}

		public virtual void Dispose()
		{
			_note = null;
			Parent = null;
		}
	}
}
