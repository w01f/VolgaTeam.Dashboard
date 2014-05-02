using System;
using System.Collections;
using System.Collections.Generic;
using NewBizWiz.Calendar.Controls.PresentationClasses.Views;
using NewBizWiz.Core.Calendar;

namespace NewBizWiz.Calendar.Controls.PresentationClasses
{
	public class CopyPasteManager
	{
		public CopyPasteManager(IView parentView)
		{
			ParentView = parentView;
		}

		public CalendarDay SourceDay { get; private set; }
		public CalendarNote SourceNote { get; private set; }

		public IView ParentView { get; private set; }

		public event EventHandler<EventArgs> DayCopied;
		public event EventHandler<EventArgs> DayPasted;

		public event EventHandler<EventArgs> NoteCopied;

		public event EventHandler<EventArgs> OnSetCopyDay;
		public event EventHandler<EventArgs> OnResetCopy;
		public event EventHandler<EventArgs> OnResetPaste;

		public void SetCopyDay()
		{
			if (OnSetCopyDay != null)
				OnSetCopyDay(null, null);
		}

		public void ResetCopy()
		{
			if (OnResetCopy != null)
				OnResetCopy(null, null);
		}

		public void ResetPaste()
		{
			SourceDay = null;
			SourceNote = null;
			if (OnResetPaste != null)
				OnResetPaste(null, null);
		}

		public void CopyDay(CalendarDay source)
		{
			SourceDay = source;
			if (SourceDay == null) return;
			if (DayCopied != null)
				DayCopied(null, null);
		}

		public void CloneDay(CalendarDay source, IEnumerable<CalendarDay> destination)
		{
			if (source == null || destination == null) return;
			foreach (var day in destination)
			{
				day.Comment = source.Comment;
				day.Logo = source.Logo.Clone();
			}
			if (DayPasted != null)
				DayPasted(null, null);
		}

		public void PasteDay(CalendarDay[] destination)
		{
			if (SourceDay == null || destination == null) return;
			foreach (var day in destination)
			{
				day.Comment = SourceDay.Comment;
				day.Logo = SourceDay.Logo.Clone();
			}
			if (DayPasted != null)
				DayPasted(null, null);
		}

		public void CopyNote(CalendarNote source)
		{
			SourceNote = source;
			if (SourceNote == null) return;
			if (NoteCopied != null)
				NoteCopied(null, null);
		}
	}
}