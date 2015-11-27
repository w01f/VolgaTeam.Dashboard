using System;
using System.Linq;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using Asa.Core.MediaSchedule;

namespace Asa.MediaSchedule.Controls.PresentationClasses.Calendar
{
	public class BroadcastCalendarControl : MediaCalendarControl
	{
		public BroadcastCalendarControl()
		{
			InitSlideInfo<BroadcastSlideInfoControl>();
			SlideInfo.PropertyChanged += (sender, e) =>
			{
				if (!(e is DataSourceChangedEventArgs)) return;
				CalendarData.Reset();
				SaveCalendarData(false);
				base.LoadCalendar(false);
				MonthList_SelectedIndexChanged(MonthList, EventArgs.Empty);
				SettingsNotSaved = true;
			};
		}

		public override Core.Calendar.Calendar CalendarData
		{
			get { return _localSchedule.BroadcastCalendar; }
		}

		public override ImageListBoxControl MonthList
		{
			get { return Controller.Instance.Calendar1MonthsList; }
		}

		public override ButtonItem PreviewButton
		{
			get { return Controller.Instance.Calendar1Preview; }
		}

		public override ButtonItem EmailButton
		{
			get { return Controller.Instance.Calendar1Email; }
		}

		public override ButtonItem PowerPointButton
		{
			get { return Controller.Instance.Calendar1PowerPoint; }
		}

		public override ButtonItem PdfButton
		{
			get { return Controller.Instance.Calendar1Pdf; }
		}

		public override ButtonItem CopyButton
		{
			get { return Controller.Instance.Calendar1Copy; }
		}

		public override ButtonItem PasteButton
		{
			get { return Controller.Instance.Calendar1Paste; }
		}

		public override ButtonItem CloneButton
		{
			get { return Controller.Instance.Calendar1Clone; }
		}

		protected override RibbonTabItem CalendarTab
		{
			get { return Controller.Instance.TabCalendar1; }
		}

		public override void LoadCalendar(bool quickLoad)
		{
			base.LoadCalendar(quickLoad);
			if (quickLoad) return;
			CalendarData.UpdateNotesCollection();
		}

		public override void ShowCalendar(bool gridView)
		{
			base.ShowCalendar(gridView);
			retractableBarControl.AddButtons(SlideInfo.SlideInfoControl.GetChapters());
		}

		public override void Help_Click(object sender, EventArgs e)
		{
			OpenHelp("calendar");
		}

		public override void UpdateOutputFunctions()
		{
			base.UpdateOutputFunctions();
			var enable = (_localSchedule.SelectedSpotType == SpotType.Week && 
				_localSchedule.ProgramSchedule.Sections.SelectMany(s => s.Programs).Any()) || 
				_localSchedule.Snapshots.Any(s => s.Programs.Count > 0);
			retractableBarControl.Visible = enable;
			MonthList.Enabled = enable;
			pnTop.Visible = enable;
			pnMain.Visible = enable;
			pictureBoxNoData.Image = Properties.Resources.CalendarDisabled;
			pictureBoxNoData.Visible = !enable;
			pictureBoxNoData.BringToFront();
		}
	}
}
