using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Asa.Calendar.Controls.PresentationClasses.SlideInfo;
using Asa.Calendar.Controls.PresentationClasses.Views;
using Asa.Calendar.Controls.PresentationClasses.Views.GridView;
using Asa.Calendar.Controls.PresentationClasses.Views.MonthView;
using Asa.CommonGUI.ToolForms;
using Asa.Core.Calendar;
using Asa.Core.Common;

namespace Asa.Calendar.Controls.PresentationClasses.Calendars
{
	[ToolboxItem(false)]
	public partial class BaseCalendarControl : UserControl, ICalendarControl
	{
		public bool CalendarUpdated { get; set; }

		public BaseCalendarControl()
		{
			InitializeComponent();
			pnEmpty.Dock = DockStyle.Fill;
			pnMain.Dock = DockStyle.Fill;
			pictureBoxNoData.Dock = DockStyle.Fill;
			if ((CreateGraphics()).DpiX > 96) { }
			Splash(true);

			#region Month View Initialization
			MonthView = new MonthViewControl(this);
			MonthView.DataSaved += (sender, e) =>
									   {
										   GridView.RefreshData();
										   SettingsNotSaved = true;
									   };
			#endregion

			#region Grid  View Initialization
			GridView = new GridViewControl(this);
			GridView.DataSaved += (sender, e) =>
									  {
										  MonthView.RefreshData();
										  SettingsNotSaved = true;
									  };
			pnMain.Controls.Add(MonthView);
			pnMain.Controls.Add(GridView);
			#endregion
		}

		#region Common Methods
		public void Splash(bool show)
		{
			if (show)
			{
				pnEmpty.BringToFront();
			}
			else
			{
				pnMain.BringToFront();
			}
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
			Ribbon.Focus();
		}

		public void LeaveCalendar()
		{
			if (SettingsNotSaved || (SelectedView != null && SelectedView.SettingsNotSaved) || SlideInfo.SettingsNotSaved)
				SaveCalendarData(false);
		}

		public virtual void ShowCalendar(bool gridView)
		{
			AllowToSave = false;
			MonthList.Items.Clear();
			MonthList.Items.AddRange(CalendarData.Months.Select(x => new ImageListBoxItem(x.Date.ToString("MMM, yyyy"), 0)).ToArray());
			var selectedIndex = CalendarData.Months.Select(m => m.Date).ToList().IndexOf(CalendarSettings.SelectedMonth);
			selectedIndex = selectedIndex > 0 ? selectedIndex : 0;
			if (MonthList.Items.Count > 0)
				MonthList.SelectedIndex = selectedIndex;

			if (SelectedView != null)
			{
				if (SelectedView.SettingsNotSaved)
					SelectedView.Save();
				SelectedView.CopyPasteManager.ResetCopy();
				SelectedView.CopyPasteManager.ResetPaste();
			}
			if (gridView)
			{
				SelectedView = GridView;
				GridView.BringToFront();
			}
			else
			{
				SelectedView = MonthView;
				MonthView.BringToFront();
			}

			if (CalendarData.Months.Count > 0)
			{
				Splash(true);
				SelectedView.ChangeMonth(CalendarData.Months[MonthList.SelectedIndex].Date);
				SlideInfo.LoadData(CalendarData.Months[MonthList.SelectedIndex]);
				SlideInfo.LoadVisibilitySettings();
				Splash(false);
			}
			UpdateOutputFunctions();

			AllowToSave = true;
		}

		public virtual bool SaveCalendarData(bool byUser, string scheduleName = "")
		{
			SelectedView.Save();
			SlideInfo.SaveData();
			if (!string.IsNullOrEmpty(scheduleName))
				Schedule.Name = scheduleName;
			SettingsNotSaved = false;
			return true;
		}

		public virtual void LoadCalendar(bool quickLoad)
		{
			labelControlScheduleInfo.Text = String.Format("{0}   <color=gray><i>({1} {2})</i></color>",
				CalendarData.Schedule.BusinessName,
				CalendarData.Schedule.FlightDates,
				String.Format("{0} {1}s", CalendarData.Schedule.TotalWeeks, "week"));

			MonthView.LoadData(quickLoad);
			GridView.LoadData(quickLoad);
			SlideInfo.LoadData(allowToSave: false);

			SettingsNotSaved = false;
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
				MonthView.RefreshData();
				SettingsNotSaved = true;
			};
		}
		#endregion

		#region Output Staff
		public virtual void UpdateOutputFunctions()
		{
			var enable = CalendarData.Months.SelectMany(m => m.Days).Any(d => d.ContainsData || d.HasNotes);
			PreviewButton.Enabled = enable;
			EmailButton.Enabled = enable;
			PowerPointButton.Enabled = enable;
			if (PdfButton != null)
				PdfButton.Enabled = enable;
		}

		public void Print()
		{
			if (MonthList.SelectedIndex < 0) return;
			var currentMonth = CalendarData.Months[MonthList.SelectedIndex];
			var selectedMonths = new List<CalendarMonth>();
			foreach (var month in CalendarData.Months)
				month.OutputData.PrepareNotes();
			if (CalendarData.Months.Count > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Months";
					foreach (var month in CalendarData.Months.Where(y => y.Days.Any(z => z.ContainsData || z.HasNotes) || y.OutputData.Notes.Any()))
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
				selectedMonths.AddRange(CalendarData.Months);
			if (!selectedMonths.Any()) return;
			PowerPointInternal(selectedMonths.Select(m => m.OutputData));
		}

		public void Email()
		{
			if (MonthList.SelectedIndex < 0) return;
			var currentMonth = CalendarData.Months[MonthList.SelectedIndex];
			var selectedMonths = new List<CalendarMonth>();
			foreach (var month in CalendarData.Months)
				month.OutputData.PrepareNotes();
			if (CalendarData.Months.Count > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Months";
					foreach (var month in CalendarData.Months.Where(y => y.Days.Any(z => z.ContainsData || z.HasNotes) || y.OutputData.Notes.Any()))
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
				selectedMonths.AddRange(CalendarData.Months);
			if (!selectedMonths.Any()) return;
			EmailInternal(selectedMonths.Select(m => m.OutputData));
		}

		public void Preview()
		{
			if (MonthList.SelectedIndex < 0) return;
			var currentMonth = CalendarData.Months[MonthList.SelectedIndex];
			var selectedMonths = new List<CalendarMonth>();
			foreach (var month in CalendarData.Months)
				month.OutputData.PrepareNotes();
			if (CalendarData.Months.Count > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Months";
					foreach (var month in CalendarData.Months.Where(y => y.Days.Any(z => z.ContainsData || z.HasNotes) || y.OutputData.Notes.Any()))
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
				selectedMonths.AddRange(CalendarData.Months);
			if (!selectedMonths.Any()) return;
			PreviewInternal(selectedMonths.Select(m => m.OutputData));
		}

		public void PrintPdf()
		{
			if (MonthList.SelectedIndex < 0) return;
			var currentMonth = CalendarData.Months[MonthList.SelectedIndex];
			var selectedMonths = new List<CalendarMonth>();
			foreach (var month in CalendarData.Months)
				month.OutputData.PrepareNotes();
			if (CalendarData.Months.Count > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Months";
					foreach (var month in CalendarData.Months.Where(y => y.Days.Any(z => z.ContainsData || z.HasNotes) || y.OutputData.Notes.Any()))
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
				selectedMonths.AddRange(CalendarData.Months);
			if (!selectedMonths.Any()) return;
			PdfInternal(selectedMonths.Select(m => m.OutputData));
		}
		#endregion

		public MonthViewControl MonthView { get; private set; }
		public GridViewControl GridView { get; private set; }

		#region ICalendarControl Members
		public IView SelectedView { get; private set; }

		public SlideInfoWrapper SlideInfo { get; private set; }

		public bool AllowToSave { get; set; }
		public bool SettingsNotSaved { get; set; }

		public virtual Core.Calendar.Calendar CalendarData
		{
			get { return null; }
		}

		public virtual CalendarSettings CalendarSettings
		{
			get { return null; }
		}
		#endregion

		public virtual ISchedule Schedule
		{
			get { return null; }
		}

		public virtual Form FormMain
		{
			get { return null; }
		}

		public virtual RibbonControl Ribbon
		{
			get { return null; }
		}

		public virtual ImageListBoxControl MonthList
		{
			get { return null; }
		}

		public virtual ButtonItem PreviewButton
		{
			get { return null; }
		}

		public virtual ButtonItem EmailButton
		{
			get { return null; }
		}

		public virtual ButtonItem PowerPointButton
		{
			get { return null; }
		}

		public virtual ButtonItem PdfButton
		{
			get { return null; }
		}

		public virtual ButtonItem ThemeButton
		{
			get { return null; }
		}

		public virtual ButtonItem CopyButton
		{
			get { return null; }
		}

		public virtual ButtonItem PasteButton
		{
			get { return null; }
		}

		public virtual ButtonItem CloneButton
		{
			get { return null; }
		}

		public virtual void OpenHelp(string key)
		{
			throw new NotImplementedException();
		}
		public virtual void SaveSettings()
		{
			throw new NotImplementedException();
		}
		public virtual ColorSchema GetColorSchema(string colorName)
		{
			throw new NotImplementedException();
		}
		protected virtual void PowerPointInternal(IEnumerable<CalendarOutputData> outputData)
		{
			throw new NotImplementedException();
		}
		protected virtual void EmailInternal(IEnumerable<CalendarOutputData> outputData)
		{
			throw new NotImplementedException();
		}
		protected virtual void PreviewInternal(IEnumerable<CalendarOutputData> outputData)
		{
			throw new NotImplementedException();
		}
		protected virtual void PdfInternal(IEnumerable<CalendarOutputData> outputData)
		{
			throw new NotImplementedException();
		}
	}
}