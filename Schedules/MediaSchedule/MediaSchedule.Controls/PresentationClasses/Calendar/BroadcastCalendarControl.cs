using System;
using System.Linq;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Common.Enums;
using Asa.Business.Media.Entities.NonPersistent.Calendar;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.GUI.ToolForms;
using Asa.Media.Controls.BusinessClasses.Managers;
using DevComponents.DotNetBar;
using DevExpress.XtraLayout.Utils;

namespace Asa.Media.Controls.PresentationClasses.Calendar
{
	public class BroadcastCalendarControl : MediaCalendarControl
	{
		#region Properties
		private MediaScheduleSettings ScheduleSettings => Schedule.Settings;

		public override string Identifier => ContentIdentifiers.BroadcastCalendar;

		public override RibbonTabItem TabPage => Controller.Instance.TabCalendar1;

		protected override RibbonControl Ribbon => Controller.Instance.Ribbon;

		public override CalendarSection ActiveCalendarSection
		{
			get
			{
				switch (((BroadcastCalendar)EditedContent).DataSourceType)
				{
					case BroadcastDataTypeEnum.Schedule:
						return CalendarContent.Sections.OfType<ScheduleCalendarSection>().Single();
					case BroadcastDataTypeEnum.Snapshots:
						return CalendarContent.Sections.OfType<SnapshotCalendarSection>().Single();
					default:
						return CalendarContent.Sections.OfType<CustomDataCalendarSection>().Single();
				}
			}
		}

		public override ButtonItem CopyButton => Controller.Instance.Calendar1Copy;

		public override ButtonItem PasteButton => Controller.Instance.Calendar1Paste;

		public override ButtonItem CloneButton => Controller.Instance.Calendar1Clone;
		public override ButtonItem ResetButton => Controller.Instance.Calendar1Reset;
		#endregion

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

			Controller.Instance.Calendar1DataSourceSchedule.Tag = BroadcastDataTypeEnum.Schedule;
			Controller.Instance.Calendar1DataSourceSnapshots.Tag = BroadcastDataTypeEnum.Snapshots;
			Controller.Instance.Calendar1DataSourceEmpty.Tag = BroadcastDataTypeEnum.Empty;
			Controller.Instance.Calendar1DataSourceSchedule.Click += OnDataSourceButtonClick;
			Controller.Instance.Calendar1DataSourceSnapshots.Click += OnDataSourceButtonClick;
			Controller.Instance.Calendar1DataSourceEmpty.Click += OnDataSourceButtonClick;
			Controller.Instance.Calendar1DataSourceSchedule.CheckedChanged += OnDataSourceButtonCheckedChanged;
			Controller.Instance.Calendar1DataSourceSnapshots.CheckedChanged += OnDataSourceButtonCheckedChanged;
			Controller.Instance.Calendar1DataSourceEmpty.CheckedChanged += OnDataSourceButtonCheckedChanged;
		}

		protected override void UpdateEditedContet()
		{
			base.UpdateEditedContet();

			AllowToSave = false;

			Controller.Instance.Calendar1DataSourceSchedule.Enabled =
				ScheduleSettings.SelectedSpotType == SpotType.Week && Schedule.ProgramSchedule.TotalSpots > 0;
			Controller.Instance.Calendar1DataSourceSnapshots.Enabled =
				Schedule.SnapshotContent.Snapshots.Any(s => s.Programs.Count > 0);

			switch (((BroadcastCalendar)EditedContent).DataSourceType)
			{
				case BroadcastDataTypeEnum.Schedule:
					Controller.Instance.Calendar1DataSourceSchedule.Checked = true;
					Controller.Instance.Calendar1DataSourceSnapshots.Checked = false;
					Controller.Instance.Calendar1DataSourceEmpty.Checked = false;
					break;
				case BroadcastDataTypeEnum.Snapshots:
					Controller.Instance.Calendar1DataSourceSchedule.Checked = false;
					Controller.Instance.Calendar1DataSourceSnapshots.Checked = true;
					Controller.Instance.Calendar1DataSourceEmpty.Checked = false;
					break;
				case BroadcastDataTypeEnum.Undefined:
				case BroadcastDataTypeEnum.Empty:
					Controller.Instance.Calendar1DataSourceSchedule.Checked = false;
					Controller.Instance.Calendar1DataSourceSnapshots.Checked = false;
					Controller.Instance.Calendar1DataSourceEmpty.Checked = true;
					break;
			}
			AllowToSave = true;
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

		#region Data Source processing
		private void OnDataSourceButtonClick(object sender, EventArgs e)
		{
			var buttonItem = (ButtonItem)sender;
			if (buttonItem.Checked)
				return;
			Controller.Instance.Calendar1DataSourceSchedule.Checked = false;
			Controller.Instance.Calendar1DataSourceSnapshots.Checked = false;
			Controller.Instance.Calendar1DataSourceEmpty.Checked = false;
			buttonItem.Checked = true;
		}

		private void OnDataSourceButtonCheckedChanged(object sender, EventArgs e)
		{

			if (!AllowToSave) return;
			var buttonItem = (ButtonItem)sender;
			if (!buttonItem.Checked) return;
			ApplyChanges();
			((BroadcastCalendar)EditedContent).DataSourceType = (BroadcastDataTypeEnum)buttonItem.Tag;
			Splash(true);
			FormProgress.ShowProgress("Loading Data...", () =>
			{
				ReleaseControls();
				LoadCalendar();
			});
			Splash(false);
			SettingsNotSaved = true;
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

			if (enable)
			{
				layoutControlItemDefaultLogo.Visibility = LayoutVisibility.Never;
				layoutControlItemData.Visibility = LayoutVisibility.Always;
			}
			else
			{
				layoutControlItemData.Visibility = LayoutVisibility.Never;
				layoutControlItemDefaultLogo.Visibility = LayoutVisibility.Always;
			}

			pictureEditDefaultLogo.Image = BusinessObjects.Instance.ImageResourcesManager.CalendarNoDataLogo ?? Properties.Resources.CalendarDisabled;

			Controller.Instance.Calendar1DataSourceSchedule.Enabled = enable;
			Controller.Instance.Calendar1DataSourceSnapshots.Enabled = enable;
			Controller.Instance.Calendar1DataSourceEmpty.Enabled = enable;
			Controller.Instance.Calendar1Reset.Enabled = enable;

			Controller.Instance.Calendar1PowerPoint.Enabled = enable;
			Controller.Instance.MenuOutputPdfButton.Enabled = enable;
			Controller.Instance.MenuEmailButton.Enabled = enable;
		}
		#endregion
	}
}
