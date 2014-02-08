using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.Calendar.Controls.PresentationClasses.SlideInfo;
using NewBizWiz.Calendar.Controls.PresentationClasses.Views;
using NewBizWiz.Calendar.Controls.PresentationClasses.Views.GridView;
using NewBizWiz.Calendar.Controls.PresentationClasses.Views.MonthView;
using NewBizWiz.Calendar.Controls.Properties;
using NewBizWiz.Calendar.Controls.ToolForms;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Calendar.Controls.PresentationClasses.Calendars
{
	[ToolboxItem(false)]
	public abstract partial class BaseCalendarControl : UserControl, ICalendarControl
	{
		protected BaseCalendarControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
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

			#region Slide Info Initialization
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
			if (control.GetType() == typeof(TextEdit) || control.GetType() == typeof(MemoEdit) || control.GetType() == typeof(ComboBoxEdit) || control.GetType() == typeof(LookUpEdit) || control.GetType() == typeof(DateEdit) || control.GetType() == typeof(CheckedListBoxControl) || control.GetType() == typeof(SpinEdit) || control.GetType() == typeof(CheckEdit) || control.GetType() == typeof(ImageListBoxControl)) return;
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
				SaveCalendarData();
			SlideInfo.Close(false);
		}

		public void ShowCalendar(bool gridView)
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
				UpdateOutputFunctions();
				Splash(false);
			}

			AllowToSave = true;
		}

		public virtual bool SaveCalendarData(string scheduleName = "")
		{
			SelectedView.Save();
			SlideInfo.SaveData();
			if (!string.IsNullOrEmpty(scheduleName))
				Schedule.Name = scheduleName;
			laCalendarName.Text = CalendarData.Schedule.Name;
			SettingsNotSaved = false;
			return true;
		}

		public virtual void LoadCalendar(bool quickLoad)
		{
			laAdvertiser.Text = CalendarData.Schedule.BusinessName;
			laCalendarWindow.Text = CalendarData.Schedule.FlightDateStart.HasValue && CalendarData.Schedule.FlightDateEnd.HasValue ? string.Format("{0} - {1}", new object[] { CalendarData.Schedule.FlightDateStart.Value.ToString("MM/dd/yy"), CalendarData.Schedule.FlightDateEnd.Value.ToString("MM/dd/yy") }) : string.Empty;
			laCalendarName.Text = CalendarData.Schedule.Name;

			MonthView.LoadData(quickLoad);
			GridView.LoadData(quickLoad);
			SlideInfo.LoadData();

			SettingsNotSaved = false;
		}

		protected void InitSlideInfo<TControl>() where TControl : ISlideInfoControl
		{
			SlideInfo = new SlideInfoWrapper(this, dockPanelSlideInfo);
			SlideInfo.InitControl<TControl>();
			AssignCloseActiveEditorsonOutSideClick(SlideInfo.ContainedControl);
			dockPanelSlideInfo.Controls.Add(SlideInfo.ContainedControl);
			SlideInfo.Shown += (sender, e) =>
			{
				bool temp = AllowToSave;
				AllowToSave = false;
				SlideInfoButton.Checked = true;
				AllowToSave = temp;
			};
			SlideInfo.Closed += (sender, e) =>
			{
				var temp = AllowToSave;
				AllowToSave = false;
				SlideInfoButton.Checked = false;
				AllowToSave = temp;
			};
			SlideInfo.ThemeChanged += (sender, e) =>
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
		#endregion

		#region Common Event Handlers
		private void dockManager_Sizing(object sender, SizingEventArgs e)
		{
			if ((e.Panel.Name.Equals("dockPanelDayProperties") || e.Panel.Name.Equals("dockPanelSlideInfo")) && (e.NewSize.Width < 300 || e.NewSize.Height < 650))
				e.Cancel = true;
		}
		#endregion

		public MonthViewControl MonthView { get; private set; }
		public GridViewControl GridView { get; private set; }

		#region ICalendarControl Members
		public IView SelectedView { get; private set; }

		public SlideInfoWrapper SlideInfo { get; private set; }

		public bool AllowToSave { get; set; }
		public bool SettingsNotSaved { get; set; }

		public abstract Core.Calendar.Calendar CalendarData { get; }

		public abstract CalendarSettings CalendarSettings { get; }

		public abstract List<ImageSource> DayImages { get; }
		#endregion

		public abstract ISchedule Schedule { get; }
		public abstract Form FormMain { get; }
		public abstract RibbonControl Ribbon { get; }
		public abstract ImageListBoxControl MonthList { get; }
		public abstract ButtonItem SlideInfoButton { get; }
		public abstract ButtonItem PreviewButton { get; }
		public abstract ButtonItem EmailButton { get; }
		public abstract ButtonItem PowerPointButton { get; }
		public abstract ButtonItem ThemeButton { get; }
		public abstract ButtonItem CopyButton { get; }
		public abstract ButtonItem PasteButton { get; }
		public abstract ButtonItem CloneButton { get; }

		public abstract void OpenHelp();
		public abstract void SaveSettings();
		protected abstract void PowerPointInternal(IEnumerable<CalendarOutputData> outputData);
		protected abstract void EmailInternal(IEnumerable<CalendarOutputData> outputData);
		protected abstract void PreviewInternal(IEnumerable<CalendarOutputData> outputData);
	}
}