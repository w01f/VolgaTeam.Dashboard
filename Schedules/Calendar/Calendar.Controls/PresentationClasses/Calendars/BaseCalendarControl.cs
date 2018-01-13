using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Calendar.Configuration;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Calendar.Interfaces;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Interfaces;
using Asa.Common.Core.Objects.Output;
using Asa.Common.GUI.ToolForms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Asa.Calendar.Controls.PresentationClasses.SlideInfo;
using Asa.Calendar.Controls.PresentationClasses.Views;
using Asa.Calendar.Controls.PresentationClasses.Views.MonthView;
using Asa.Schedules.Common.Controls.ContentEditors.Controls;
using DevExpress.XtraLayout.Utils;

namespace Asa.Calendar.Controls.PresentationClasses.Calendars
{
	[ToolboxItem(false)]
	//public abstract partial class BaseCalendarControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo> : UserControl
	public abstract partial class BaseCalendarControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo> : BasePartitionEditControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>, 
		ICalendarControl
		where TPartitionContet : BaseSchedulePartitionContent<TSchedule, TScheduleSettings>, ICalendarContent
		where TSchedule : ISchedule<TScheduleSettings>
		where TScheduleSettings : IBaseScheduleSettings
		where TChangeInfo : BaseScheduleChangeInfo
	{
		protected abstract Form FormMain { get; }
		protected abstract RibbonControl Ribbon { get; }

		#region ICalendarControl Members
		public bool AllowToSave { get; set; }
		public abstract CalendarSettings CalendarSettings { get; }
		public abstract CalendarSection ActiveCalendarSection { get; }
		public ICalendarContent CalendarContent => EditedContent;
		public IView CalendarView { get; private set; }
		public SlideInfoWrapper SlideInfo { get; private set; }

		public abstract ButtonItem CopyButton { get; }
		public abstract ButtonItem PasteButton { get; }
		public abstract ButtonItem CloneButton { get; }
		public abstract ButtonItem ResetButton { get; }
		#endregion

		protected BaseCalendarControl()
		{
			InitializeComponent();
		}

		#region BaseContentEditControl Override
		public override void InitControl()
		{
			base.InitControl();

			CalendarView = new MonthViewControl(this);
			CalendarView.DataSaved += OnDataChanged;
			CalendarView.SelectedMonthChanged += OnMonthListSelectedIndexChanged;
			pnMain.Controls.Add((Control)CalendarView);

			CopyButton.Click += OnCalendarCopyClick;
			PasteButton.Click += OnCalendarPasteClick;
			CloneButton.Click += OnCalendarCloneClick;

			retractableBarControl.ContentSize = retractableBarControl.Width;
		}

		protected override void UpdateEditedContet()
		{
			LoadCalendar();
		}

		protected override void ApplyChanges()
		{
			CalendarView.Save();
			SlideInfo.SaveData();
		}

		protected void LoadCalendar()
		{
			AllowToSave = false;

			CalendarView.LoadData();
			((Control)CalendarView).BringToFront();

			SlideInfo.LoadVisibilitySettings();
			SlideInfo.LoadData(CalendarView.SelectedMonthData);
			CalendarSettings.SelectedMonth = CalendarView.SelectedMonthData.Date;

			UpdateDataManagementAndOutputFunctions();

			AllowToSave = true;
		}
		#endregion

		#region ICalendarControl Members
		public abstract void OpenHelp(string key);
		public abstract void SaveSettings();
		public abstract ColorSchema GetColorSchema(string colorName);
		#endregion

		#region Common Methods
		public void Splash(bool show)
		{
			if (show)
			{
				layoutControlItemContainer.Visibility = LayoutVisibility.Never;
				emptySpaceItemSplash.Visibility = LayoutVisibility.Always;
			}
			else
			{
				emptySpaceItemSplash.Visibility = LayoutVisibility.Never;
				layoutControlItemContainer.Visibility = LayoutVisibility.Always;
			}
			Ribbon.Enabled = !show;
			SlideInfo.ContainedControl.Enabled = !show;
		}

		public void AssignCloseActiveEditorsonOutSideClick(Control control)
		{
			if (control.GetType() == typeof(TextEdit) ||
			    control.GetType() == typeof(MemoEdit) ||
			    control.GetType() == typeof(ComboBoxEdit) ||
			    control.GetType() == typeof(LookUpEdit) ||
			    control.GetType() == typeof(DateEdit) ||
			    control.GetType() == typeof(CheckedListBoxControl) ||
			    control.GetType() == typeof(SpinEdit) ||
			    control.GetType() == typeof(CheckEdit) ||
			    control.GetType() == typeof(ImageListBoxControl))
				return;
			control.Click += CloseActiveEditorsonOutSideClick;
			foreach (Control childControl in control.Controls)
				AssignCloseActiveEditorsonOutSideClick(childControl);
		}

		private void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			(CalendarView as Control)?.Focus();
		}

		protected void InitSlideInfo<TControl>() where TControl : ISlideInfoControl
		{
			SlideInfo = new SlideInfoWrapper(this, retractableBarControl);
			SlideInfo.InitControl<TControl>();
			AssignCloseActiveEditorsonOutSideClick(SlideInfo.ContainedControl);
			retractableBarControl.Content.Controls.Add(SlideInfo.ContainedControl);
			retractableBarControl.AddButtons(SlideInfo.SlideInfoControl.GetChapters());
			SlideInfo.PropertyChanged += (sender, e) =>
			{
				CalendarView.RefreshData();
				SettingsNotSaved = true;
			};
		}

		protected void ReleaseControls()
		{
			CalendarView.Release();
			SlideInfo.Release();
		}

		private void OnDataChanged(object sender, EventArgs e)
		{
			SettingsNotSaved = true;
		}

		private void OnMonthListSelectedIndexChanged(object sender, EventArgs e)
		{
			if (!AllowToSave) return;
			SlideInfo.LoadData(CalendarView.SelectedMonthData);
			CalendarSettings.SelectedMonth = CalendarView.SelectedMonthData.Date;
		}

		private void OnCalendarCopyClick(object sender, EventArgs e)
		{
			CalendarView.CopyDay();
		}

		private void OnCalendarPasteClick(object sender, EventArgs e)
		{
			CalendarView.PasteDay();
		}

		private void OnCalendarCloneClick(object sender, EventArgs e)
		{
			CalendarView.CloneDay();
		}
		#endregion

		#region Output Staff
		protected abstract void OutpuPowerPointSlides(IEnumerable<CalendarOutputData> outputData);
		protected abstract void EmailSlides(IEnumerable<CalendarOutputData> outputData);
		protected abstract void PreviewSlides(IEnumerable<CalendarOutputData> outputData);
		protected abstract void OutputPdfSlides(IEnumerable<CalendarOutputData> outputData);

		public abstract void UpdateDataManagementAndOutputFunctions();

		protected virtual bool IsOutputEnabled
		{
			get { return ActiveCalendarSection.Months.SelectMany(m => m.Days).Any(d => d.ContainsData || d.HasNotes); }
		}

		public override void OutputPowerPoint()
		{
			var currentMonth = CalendarView.SelectedMonthData;
			var selectedMonths = new List<CalendarMonth>();
			foreach (var month in ActiveCalendarSection.Months)
				month.OutputData.PrepareNotes();
			if (ActiveCalendarSection.Months.Count > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Months";
					foreach (var month in ActiveCalendarSection.Months.Where(y => y.Days.Any(z => z.ContainsData || z.HasNotes) || y.OutputData.Notes.Any()))
					{
						var item = new CheckedListBoxItem(month, month.OutputData.MonthText);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (month == currentMonth)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedMonths.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<CalendarMonth>());
				}
			else
				selectedMonths.AddRange(ActiveCalendarSection.Months);
			if (!selectedMonths.Any()) return;
			OutpuPowerPointSlides(selectedMonths.Select(m => m.OutputData));
		}

		public override void OutputPdf()
		{
			var currentMonth = CalendarView.SelectedMonthData;
			var selectedMonths = new List<CalendarMonth>();
			foreach (var month in ActiveCalendarSection.Months)
				month.OutputData.PrepareNotes();
			if (ActiveCalendarSection.Months.Count > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Months";
					foreach (var month in ActiveCalendarSection.Months.Where(y => y.Days.Any(z => z.ContainsData || z.HasNotes) || y.OutputData.Notes.Any()))
					{
						var item = new CheckedListBoxItem(month, month.OutputData.MonthText);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (month == currentMonth)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedMonths.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<CalendarMonth>());
				}
			else
				selectedMonths.AddRange(ActiveCalendarSection.Months);
			if (!selectedMonths.Any()) return;
			OutputPdfSlides(selectedMonths.Select(m => m.OutputData));
		}

		public override void Preview()
		{
			var currentMonth = CalendarView.SelectedMonthData;
			var selectedMonths = new List<CalendarMonth>();
			foreach (var month in ActiveCalendarSection.Months)
				month.OutputData.PrepareNotes();
			if (ActiveCalendarSection.Months.Count > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Months";
					foreach (var month in ActiveCalendarSection.Months.Where(y => y.Days.Any(z => z.ContainsData || z.HasNotes) || y.OutputData.Notes.Any()))
					{
						var item = new CheckedListBoxItem(month, month.OutputData.MonthText);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (month == currentMonth)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedMonths.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<CalendarMonth>());
				}
			else
				selectedMonths.AddRange(ActiveCalendarSection.Months);
			if (!selectedMonths.Any()) return;
			PreviewSlides(selectedMonths.Select(m => m.OutputData));
		}

		public override void Email()
		{
			var currentMonth = CalendarView.SelectedMonthData;
			var selectedMonths = new List<CalendarMonth>();
			foreach (var month in ActiveCalendarSection.Months)
				month.OutputData.PrepareNotes();
			if (ActiveCalendarSection.Months.Count > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Months";
					foreach (var month in ActiveCalendarSection.Months.Where(y => y.Days.Any(z => z.ContainsData || z.HasNotes) || y.OutputData.Notes.Any()))
					{
						var item = new CheckedListBoxItem(month, month.OutputData.MonthText);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (month == currentMonth)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedMonths.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<CalendarMonth>());
				}
			else
				selectedMonths.AddRange(ActiveCalendarSection.Months);
			if (!selectedMonths.Any()) return;
			EmailSlides(selectedMonths.Select(m => m.OutputData));
		}
		#endregion
	}
}