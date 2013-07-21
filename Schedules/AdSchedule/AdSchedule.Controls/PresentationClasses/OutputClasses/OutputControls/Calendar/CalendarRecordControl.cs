using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using NewBizWiz.Core.AdSchedule;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public abstract partial class CalendarRecordControl : LabelControl
	{
		protected int _insertsCount = 0;
		protected DayControl _parent = null;

		public CalendarRecordControl(DayControl parent)
		{
			InitializeComponent();
			_parent = parent;
			AllowHtmlString = true;
			UseMnemonic = false;
			AutoSizeMode = LabelAutoSizeMode.Vertical;
			Appearance.TextOptions.WordWrap = WordWrap.Wrap;
			Dock = DockStyle.Top;
			MouseMove += DayRecordControl_MouseMove;
			DoubleClick += DayRecordControl_DoubleClick;
		}

		public virtual void Init(int insertsCount)
		{
			_insertsCount = insertsCount;
			RefreshData();
		}

		protected abstract string GetControlText();

		public string GetOutputText()
		{
			return Regex.Replace(GetControlText(), @"<[^>]*>", string.Empty);
		}

		protected virtual void GetFont()
		{
			if (_insertsCount <= 3)
				Font = new Font("Arial", 10);
			else if (_insertsCount > 3 && _insertsCount <= 6)
				Font = new Font("Arial", 9);
			else if (_insertsCount > 6)
				Font = new Font("Arial", 8);
			Appearance.Font = Font;
			Appearance.Options.UseFont = true;
		}

		public void RefreshData()
		{
			GetFont();
			Text = GetControlText();
			Refresh();
		}

		private void DayRecordControl_MouseMove(object sender, MouseEventArgs e)
		{
			Parent.Focus();
		}

		protected virtual void DayRecordControl_DoubleClick(object sender, EventArgs e)
		{
			_parent.ShowDayView();
		}
	}

	public class DayRecordControl : CalendarRecordControl
	{
		private Insert _insert;
		private bool _isDayView;

		public DayRecordControl(DayControl parent)
			: base(parent) { }

		public void Init(Insert insert, int insertsCount, bool isDayView)
		{
			_isDayView = isDayView;
			_insert = insert;
			base.Init(insertsCount);
		}

		protected override string GetControlText()
		{
			string text = string.Empty;

			text += "<b>" + ((_insertsCount <= 1 || _isDayView) && !_parent.ParentMonth.Settings.Parent.ShowAbbreviationOnly ? _insert.Publication : _parent.ParentMonth.Settings.GetLegendCodeByDescription(_insert.Publication)) + "</b>";

			if (!string.IsNullOrEmpty(_insert.FullSection) && _parent.ParentMonth.Settings.Parent.ShowSection)
				text += " " + _insert.FullSection;

			if (!string.IsNullOrEmpty(_insert.DimensionsShort) && _parent.ParentMonth.Settings.Parent.ShowAdSize && _insert.PublicationSquare > 0)
				text += " " + _insert.DimensionsShort;

			if (!string.IsNullOrEmpty(_insert.PageSize) && _parent.ParentMonth.Settings.Parent.ShowPageSize)
				text += " " + _insert.PageSize;

			if (!string.IsNullOrEmpty(_insert.PercentOfPage) && _parent.ParentMonth.Settings.Parent.ShowPercentOfPage && ListManager.Instance.ShareUnits.Count > 0)
				text += " " + _insert.PercentOfPage;

			if (!string.IsNullOrEmpty(_insert.PublicationColor) && _parent.ParentMonth.Settings.Parent.ShowColor)
				text += " " + _insert.PublicationColor;

			if (_parent.ParentMonth.Settings.Parent.ShowCost)
				text += " " + (_insertsCount <= 3 || _insert.FinalRate < 1000 || _isDayView ? _insert.FinalRate.ToString("$#,##0") : ((_insert.FinalRate / 1000).ToString("$#,##0") + "k"));

			if (_isDayView)
				text += "<br> ";
			return text;
		}

		protected override void GetFont()
		{
			if (_isDayView)
			{
				Font = new Font("Arial", 10);
				Appearance.Font = Font;
				Appearance.Options.UseFont = true;
			}
			else
				base.GetFont();
		}

		protected override void DayRecordControl_DoubleClick(object sender, EventArgs e)
		{
			if (!_isDayView)
				_parent.ShowDayView();
		}
	}

	public class CustomNoteRecordControl : CalendarRecordControl
	{
		public CustomNoteRecordControl(DayControl parent)
			: base(parent) { }

		protected override string GetControlText()
		{
			var text = new List<string>();
			CalendarDayInfo dayCustomNote = _parent.ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayCustomNotes.Where(x => x.Day.Equals(_parent.Date)).FirstOrDefault();
			if (dayCustomNote != null && !string.IsNullOrEmpty(dayCustomNote.Info))
				text.Add(dayCustomNote.Info);
			CalendarDayInfo dayDeadline = _parent.ParentMonth.ParentCalendar.LocalSchedule.ViewSettings.CalendarViewSettings.DayDeadlines.Where(x => x.Day.Equals(_parent.Date)).FirstOrDefault();
			if (dayDeadline != null && !string.IsNullOrEmpty(dayDeadline.Info))
				text.Add(dayDeadline.Info);
			return string.Join(", ", text.ToArray());
		}
	}
}