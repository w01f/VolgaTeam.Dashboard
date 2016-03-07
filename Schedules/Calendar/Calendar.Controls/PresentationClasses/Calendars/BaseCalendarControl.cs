using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Asa.Business.Calendar.Configuration;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Calendar.Interfaces;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Business.Common.Interfaces;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Output;
using Asa.Common.GUI.ContentEditors.Controls;
using Asa.Common.GUI.ToolForms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Asa.Calendar.Controls.PresentationClasses.SlideInfo;
using Asa.Calendar.Controls.PresentationClasses.Views;
using Asa.Calendar.Controls.PresentationClasses.Views.MonthView;

namespace Asa.Calendar.Controls.PresentationClasses.Calendars
{
	[ToolboxItem(false)]
	//public abstract partial class BaseCalendarControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo> : UserControl
	public abstract partial class BaseCalendarControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo> : BasePartitionEditControl<TPartitionContet, TSchedule, TScheduleSettings, TChangeInfo>, ICalendarControl
		where TPartitionContet : BaseSchedulePartitionContent<TSchedule, TScheduleSettings>, ICalendarContent
		where TSchedule : ISchedule<TScheduleSettings>
		where TScheduleSettings : IBaseScheduleSettings
		where TChangeInfo : BaseScheduleChangeInfo
	{
		protected abstract Form FormMain { get; }
		protected abstract RibbonControl Ribbon { get; }
		protected abstract ImageListBoxControl MonthList { get; }

		#region ICalendarControl Members
		public bool AllowToSave { get; set; }
		public abstract CalendarSettings CalendarSettings { get; }
		public ICalendarContent CalendarContent
		{
			get { return EditedContent; }
		}
		public IView CalendarView { get; private set; }
		public SlideInfoWrapper SlideInfo { get; private set; }

		public abstract ButtonItem CopyButton { get; }
		public abstract ButtonItem PasteButton { get; }
		public abstract ButtonItem CloneButton { get; }
		#endregion

		protected BaseCalendarControl()
		{
			InitializeComponent();
		}

		#region BasePartitionEditControl Override
		public override void InitControl()
		{
			base.InitControl();

			pnEmpty.Dock = DockStyle.Fill;
			pnMain.Dock = DockStyle.Fill;
			pictureBoxNoData.Dock = DockStyle.Fill;

			CalendarView = new MonthViewControl(this);
			CalendarView.DataSaved += OnDataChanged;
			pnMain.Controls.Add((Control)CalendarView);

			if ((CreateGraphics()).DpiX > 96)
			{
				hyperLinkEditReset.Font = new Font(hyperLinkEditReset.Font.FontFamily, hyperLinkEditReset.Font.Size - 2,
					hyperLinkEditReset.Font.Style);
			}

			MonthList.SelectedIndexChanged += OnMonthListSelectedIndexChanged;
			CopyButton.Click += OnCalendarCopyClick;
			PasteButton.Click += OnCalendarPasteClick;
			CloneButton.Click += OnCalendarCloneClick;
		}

		protected override void UpdateEditedContet()
		{
			AllowToSave = false;

			labelControlScheduleInfo.Text = String.Format("{0}   <color=gray><i>({1} {2})</i></color>",
				CalendarContent.Settings.BusinessName,
				CalendarContent.Settings.FlightDates,
				String.Format("{0} {1}s", CalendarContent.Settings.TotalWeeks, "week"));

			if (!CalendarContent.Months.Any()) return;

			MonthList.Items.AddRange(CalendarContent.Months.Select(x => new ImageListBoxItem(x.Date.ToString("MMM, yyyy"), 0)).ToArray());
			var selectedIndex = CalendarContent.Months
				.Select(m => m.Date)
				.ToList()
				.IndexOf(CalendarSettings.SelectedMonth);
			MonthList.SelectedIndex = selectedIndex > 0 ? selectedIndex : 0;

			CalendarView.LoadData();
			CalendarView = CalendarView;
			CalendarView.ChangeMonth(CalendarContent.Months[MonthList.SelectedIndex].Date);
			((Control)CalendarView).BringToFront();

			SlideInfo.LoadVisibilitySettings();
			SlideInfo.LoadData(CalendarContent.Months[MonthList.SelectedIndex], false);

			UpdateOutputFunctions();

			AllowToSave = true;
		}

		protected override void ApplyChanges()
		{
			CalendarView.Save();
			SlideInfo.SaveData();
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
			if (show) { pnEmpty.BringToFront(); }
			else { pnMain.BringToFront(); }
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
			MonthList.Focus();
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

		protected void OnDataChanged(object sender, EventArgs e)
		{
			SettingsNotSaved = true;
		}

		protected void ReleaseControls()
		{
			MonthList.Items.Clear();
			CalendarView.Release();
			SlideInfo.Release();
		}

		protected void OnMonthListSelectedIndexChanged(object sender, EventArgs e)
		{
			if (MonthList.SelectedIndex < 0 || !AllowToSave) return;
			SlideInfo.LoadData(CalendarContent.Months[MonthList.SelectedIndex]);
			Splash(true);
			Application.DoEvents();
			FormProgress.ShowProgress("Loading Data...", () => CalendarView.ChangeMonth(CalendarContent.Months[MonthList.SelectedIndex].Date));
			Splash(false);
			CalendarSettings.SelectedMonth = CalendarContent.Months[MonthList.SelectedIndex].Date;
		}
		protected void OnCalendarCopyClick(object sender, EventArgs e)
		{
			CalendarView.CopyDay();
		}

		protected void OnCalendarPasteClick(object sender, EventArgs e)
		{
			CalendarView.PasteDay();
		}

		protected void OnCalendarCloneClick(object sender, EventArgs e)
		{
			CalendarView.CloneDay();
		}
		#endregion

		#region Output Staff
		protected abstract void OutpuPowerPointSlides(IEnumerable<CalendarOutputData> outputData);
		protected abstract void EmailSlides(IEnumerable<CalendarOutputData> outputData);
		protected abstract void PreviewSlides(IEnumerable<CalendarOutputData> outputData);
		protected abstract void OutputPdfSlides(IEnumerable<CalendarOutputData> outputData);

		public abstract void UpdateOutputFunctions();

		protected virtual bool IsOutputEnabled
		{
			get { return CalendarContent.Months.SelectMany(m => m.Days).Any(d => d.ContainsData || d.HasNotes); }
		}

		public override void OutputPowerPoint()
		{
			if (MonthList.SelectedIndex < 0) return;
			var currentMonth = CalendarContent.Months[MonthList.SelectedIndex];
			var selectedMonths = new List<CalendarMonth>();
			foreach (var month in CalendarContent.Months)
				month.OutputData.PrepareNotes();
			if (CalendarContent.Months.Count > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Months";
					foreach (var month in CalendarContent.Months.Where(y => y.Days.Any(z => z.ContainsData || z.HasNotes) || y.OutputData.Notes.Any()))
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
				selectedMonths.AddRange(CalendarContent.Months);
			if (!selectedMonths.Any()) return;
			OutpuPowerPointSlides(selectedMonths.Select(m => m.OutputData));
		}

		public override void OutputPdf()
		{
			if (MonthList.SelectedIndex < 0) return;
			var currentMonth = CalendarContent.Months[MonthList.SelectedIndex];
			var selectedMonths = new List<CalendarMonth>();
			foreach (var month in CalendarContent.Months)
				month.OutputData.PrepareNotes();
			if (CalendarContent.Months.Count > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Months";
					foreach (var month in CalendarContent.Months.Where(y => y.Days.Any(z => z.ContainsData || z.HasNotes) || y.OutputData.Notes.Any()))
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
				selectedMonths.AddRange(CalendarContent.Months);
			if (!selectedMonths.Any()) return;
			OutputPdfSlides(selectedMonths.Select(m => m.OutputData));
		}

		public override void Preview()
		{
			if (MonthList.SelectedIndex < 0) return;
			var currentMonth = CalendarContent.Months[MonthList.SelectedIndex];
			var selectedMonths = new List<CalendarMonth>();
			foreach (var month in CalendarContent.Months)
				month.OutputData.PrepareNotes();
			if (CalendarContent.Months.Count > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Months";
					foreach (var month in CalendarContent.Months.Where(y => y.Days.Any(z => z.ContainsData || z.HasNotes) || y.OutputData.Notes.Any()))
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
				selectedMonths.AddRange(CalendarContent.Months);
			if (!selectedMonths.Any()) return;
			PreviewSlides(selectedMonths.Select(m => m.OutputData));
		}

		public override void Email()
		{
			if (MonthList.SelectedIndex < 0) return;
			var currentMonth = CalendarContent.Months[MonthList.SelectedIndex];
			var selectedMonths = new List<CalendarMonth>();
			foreach (var month in CalendarContent.Months)
				month.OutputData.PrepareNotes();
			if (CalendarContent.Months.Count > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Months";
					foreach (var month in CalendarContent.Months.Where(y => y.Days.Any(z => z.ContainsData || z.HasNotes) || y.OutputData.Notes.Any()))
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
				selectedMonths.AddRange(CalendarContent.Months);
			if (!selectedMonths.Any()) return;
			EmailSlides(selectedMonths.Select(m => m.OutputData));
		}
		#endregion
	}
}