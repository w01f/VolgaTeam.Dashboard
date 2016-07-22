using System;
using System.Collections.Generic;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Calendar.Controls.PresentationClasses.Views;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;

namespace Asa.Calendar.Controls.PresentationClasses
{
	public class CopyPasteManager
	{
		public CopyPasteManager(IView parentView)
		{
			ParentView = parentView;
			Init();
		}

		public CalendarDay SourceDay { get; private set; }
		public CalendarNote SourceNote { get; private set; }

		public IView ParentView { get; private set; }

		public event EventHandler<EventArgs> DayCopied;
		public event EventHandler<EventArgs> DayPasted;

		public event EventHandler<EventArgs> NoteCopied;

		public event EventHandler<EventArgs> CopyDaySet;
		public event EventHandler<EventArgs> CopyReset;
		public event EventHandler<EventArgs> PasteReset;

		private void Init()
		{
			CopyDaySet += OnCopyDaySet;
			CopyReset += OnCopyReset;
			PasteReset += OnPasteReset;
			DayCopied += OnDayCopied;
			DayPasted += OnDayPasted;
		}

		public void SetCopyDay()
		{
			CopyDaySet?.Invoke(null, null);
		}

		public void ResetCopy()
		{
			CopyReset?.Invoke(null, null);
		}

		public void ResetPaste()
		{
			SourceDay = null;
			SourceNote = null;
			PasteReset?.Invoke(null, null);
		}

		public void CopyDay(CalendarDay source)
		{
			SourceDay = source;
			if (SourceDay == null) return;
			DayCopied?.Invoke(null, null);
		}

		public void CloneDay(CalendarDay source, IEnumerable<CalendarDay> destination)
		{
			if (source == null || destination == null) return;
			foreach (var day in destination)
			{
				day.Comment = source.Comment;
				day.Logo = source.Logo.Clone<ImageSource, ImageSource>();
			}
			DayPasted?.Invoke(null, null);
		}

		public void PasteDay(CalendarDay[] destination)
		{
			if (SourceDay == null || destination == null) return;
			foreach (var day in destination)
			{
				day.Comment = SourceDay.Comment;
				day.Logo = SourceDay.Logo.Clone<ImageSource, ImageSource>();
			}
			DayPasted?.Invoke(null, null);
		}

		public void PasteImage(CalendarDay[] destination, ImageSource imageSource)
		{
			if (destination == null || imageSource == null) return;
			foreach (var day in destination)
				day.Logo = imageSource.Clone<ImageSource, ImageSource>();
			DayPasted?.Invoke(null, null);
		}

		public void CopyNote(CalendarNote source)
		{
			SourceNote = source;
			if (SourceNote == null) return;
			NoteCopied?.Invoke(null, null);
		}

		public void Release()
		{
			NoteCopied = null;
			CopyDaySet = null;
			CopyReset = null;
			PasteReset = null;
			DayCopied = null;
			DayPasted = null;
			Init();
		}

		private void OnCopyDaySet(object sender, EventArgs e)
		{
			ParentView.Calendar.CopyButton.Enabled = true;
			ParentView.Calendar.CloneButton.Enabled = true;
		}

		private void OnCopyReset(object sender, EventArgs e)
		{
			ParentView.Calendar.CopyButton.Enabled = false;
			ParentView.Calendar.CloneButton.Enabled = false;
		}

		private void OnPasteReset(object sender, EventArgs e)
		{
			ParentView.Calendar.PasteButton.Enabled = false;
		}

		private void OnDayCopied(object sender, EventArgs e)
		{
			ParentView.Calendar.PasteButton.Enabled = true;
		}

		private void OnDayPasted(object sender, EventArgs e)
		{
			ParentView.Calendar.SlideInfo.LoadData();
			ParentView.RefreshData();
			ParentView.SettingsNotSaved = true;
		}
	}
}