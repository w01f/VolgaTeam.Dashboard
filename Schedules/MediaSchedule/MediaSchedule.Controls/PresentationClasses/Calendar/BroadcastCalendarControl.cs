using System.Linq;
using Asa.Business.Common.Enums;
using Asa.Business.Media.Entities.NonPersistent.Calendar;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Helpers;
using Asa.Media.Controls.BusinessClasses.Managers;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;

namespace Asa.Media.Controls.PresentationClasses.Calendar
{
	public class BroadcastCalendarControl : MediaCalendarControl
	{
		public override string Identifier => ContentIdentifiers.BroadcastCalendar;

		public override RibbonTabItem TabPage => Controller.Instance.TabCalendar1;

		protected override RibbonControl Ribbon => Controller.Instance.Ribbon;

		protected override ImageListBoxControl MonthList => Controller.Instance.Calendar1MonthsList;

		public override ButtonItem CopyButton => Controller.Instance.Calendar1Copy;

		public override ButtonItem PasteButton => Controller.Instance.Calendar1Paste;

		public override ButtonItem CloneButton => Controller.Instance.Calendar1Clone;
		public override ButtonItem ResetButton => Controller.Instance.Calendar1Reset;

		#region BaseContentEditControl Override
		protected override bool IsContentChanged => EditedContent == null || (ContentUpdateInfo.ChangeInfo.WholeScheduleChanged ||
		                                                                      ContentUpdateInfo.ChangeInfo.ScheduleDatesChanged ||
		                                                                      ContentUpdateInfo.ChangeInfo.CalendarTypeChanged ||
		                                                                      ContentUpdateInfo.ChangeInfo.SpotTypeChanged ||
		                                                                      ContentUpdateInfo.ChangeInfo.ProgramScheduleChanged ||
		                                                                      ContentUpdateInfo.ChangeInfo.SnapshotsChanged);

		public override void InitControl()
		{
			base.InitControl();
			InitSlideInfo<BroadcastSlideInfoControl>();
			SlideInfo.PropertyChanged += (sender, e) =>
			{
				if (!(e is DataSourceChangedEventArgs)) return;
				Reset();
			};
		}

		protected override void UpdateEditedContet()
		{
			base.UpdateEditedContet();
			retractableBarControl.AddButtons(SlideInfo.SlideInfoControl.GetChapters());
		}

		protected override void SaveData()
		{
			base.SaveData();
			Schedule.ApplySchedulePartitionContent(
				SchedulePartitionType.BroadcastCalendar,
				((BroadcastCalendar)EditedContent).Clone<BroadcastCalendar, BroadcastCalendar>());
		}

		public override MediaCalendar GetEditedCalendar()
		{
			return Schedule.GetSchedulePartitionContent<BroadcastCalendar>(
				SchedulePartitionType.BroadcastCalendar)
				.Clone<BroadcastCalendar, BroadcastCalendar>();
		}

		public override void GetHelp()
		{
			OpenHelp("calendar");
		}
		#endregion

		#region Output Stuff
		protected override bool IsOutputEnabled
		{
			get
			{
				return base.IsOutputEnabled &&
					   (Schedule.Settings.SelectedSpotType == SpotType.Week &&
						Schedule.ProgramSchedule.Sections.SelectMany(s => s.Programs).Any()) ||
					   Schedule.SnapshotContent.Snapshots.Any(s => s.Programs.Count > 0);
			}
		}

		public override void UpdateDataManagementAndOutputFunctions()
		{
			var enable = IsOutputEnabled;

			retractableBarControl.Visible = enable;
			MonthList.Enabled = enable;
			pnTop.Visible = enable;
			pnMain.Visible = enable;
			pictureBoxNoData.Image = Properties.Resources.CalendarDisabled;
			pictureBoxNoData.Visible = !enable;
			pictureBoxNoData.BringToFront();

			Controller.Instance.Calendar1Reset.Enabled = enable;

			Controller.Instance.Calendar1PowerPoint.Enabled = enable;
			Controller.Instance.Calendar1Pdf.Enabled = enable;
			Controller.Instance.Calendar1Preview.Enabled = enable;
			Controller.Instance.Calendar1Email.Enabled = enable;
		}
		#endregion
	}
}
