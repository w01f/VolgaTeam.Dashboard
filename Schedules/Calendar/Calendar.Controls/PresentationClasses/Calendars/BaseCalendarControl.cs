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
			var selectedMonth = CalendarData.Months[MonthList.SelectedIndex];
			foreach (var month in CalendarData.Months)
				month.OutputData.PrepareNotes();
			using (var form = new FormSelectCalendar())
			{
				form.Text = "Calendar Slide Output";
				form.pbLogo.Image = Resources.Calendar;
				form.laTitle.Text = "You have several Calendars available for your presentation…";
				form.buttonXCurrentPublication.Text = string.Format("Send ONLY {0} Calendar Slide to PowerPoint", selectedMonth.Date.ToString("MMMM, yyyy"));
				form.buttonXSelectedPublications.Text = "Send all of the Selected Calendars to PowerPoint";
				foreach (var month in CalendarData.Months.Where(y => y.Days.Any(z => z.ContainsData || z.HasNotes) || y.OutputData.Notes.Any()))
					form.checkedListBoxControlMonths.Items.Add(month, month.Date.ToString("MMMM, yyyy"), CheckState.Checked, true);
				RegistryHelper.MainFormHandle = form.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var result = form.ShowDialog();
				RegistryHelper.MaximizeMainForm = FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = FormMain.Handle;
				if (result == DialogResult.Cancel) return;
				IEnumerable<CalendarOutputData> outputData = null;
				switch (result)
				{
					case DialogResult.Yes:
						if (!selectedMonth.Days.Any(x => x.ContainsData || x.HasNotes) && !selectedMonth.OutputData.Notes.Any())
							if (Utilities.Instance.ShowWarningQuestion(string.Format("There are no records for {0}.\nDo you still want to send this slide to PowerPoint?", selectedMonth.Date.ToString("MMMM, yyyy"))) == DialogResult.No)
								return;
						outputData = new[] { selectedMonth.OutputData };
						break;
					case DialogResult.No:
						outputData = form.checkedListBoxControlMonths.Items.OfType<CheckedListBoxItem>().Where(it => it.CheckState == CheckState.Checked).Select(it => ((CalendarMonth)it.Value).OutputData);
						break;
				}
				PowerPointInternal(outputData);
			}
		}

		public void Email()
		{
			if (MonthList.SelectedIndex < 0) return;
			var selectedMonth = CalendarData.Months[MonthList.SelectedIndex];
			foreach (var month in CalendarData.Months)
				month.OutputData.PrepareNotes();
			using (var form = new FormSelectCalendar())
			{
				form.Text = "Calendar Email Output";
				form.pbLogo.Image = Resources.EmailBig;
				form.laTitle.Text = "You have several Calendars that can be ATTACHED to an email…";
				form.buttonXCurrentPublication.Text = string.Format("Attach just the {0} Calendar Slide to my email message", selectedMonth.Date.ToString("MMMM, yyyy"));
				form.buttonXSelectedPublications.Text = "Attach ALL Selected Calendars to my email message";
				foreach (var month in CalendarData.Months.Where(y => y.Days.Any(z => z.ContainsData || z.HasNotes) || y.OutputData.Notes.Any()))
					form.checkedListBoxControlMonths.Items.Add(month, month.Date.ToString("MMMM, yyyy"), CheckState.Checked, true);
				RegistryHelper.MainFormHandle = form.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var result = form.ShowDialog();
				RegistryHelper.MaximizeMainForm = FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = FormMain.Handle;
				if (result == DialogResult.Cancel) return;
				IEnumerable<CalendarOutputData> outputData = null;
				switch (result)
				{
					case DialogResult.Yes:
						if (!selectedMonth.Days.Any(x => x.ContainsData || x.HasNotes) && !selectedMonth.OutputData.Notes.Any())
							if (Utilities.Instance.ShowWarningQuestion(string.Format("There are no records for {0}.\nDo you still want to email this slide?", selectedMonth.Date.ToString("MMMM, yyyy"))) == DialogResult.No)
								return;
						outputData = new[] { selectedMonth.OutputData };
						break;
					case DialogResult.No:
						outputData = form.checkedListBoxControlMonths.Items.OfType<CheckedListBoxItem>().Where(it => it.CheckState == CheckState.Checked).Select(it => ((CalendarMonth)it.Value).OutputData);
						break;
				}
				EmailInternal(outputData);
			}
		}

		public void Preview()
		{
			if (MonthList.SelectedIndex < 0) return;
			var selectedMonth = CalendarData.Months[MonthList.SelectedIndex];
			foreach (var month in CalendarData.Months)
				month.OutputData.PrepareNotes();
			using (var form = new FormSelectCalendar())
			{
				form.Text = "Calendar Preview";
				form.pbLogo.Image = Resources.Preview;
				form.laTitle.Text = "You have several Calendars available for preview…";
				form.buttonXCurrentPublication.Text = string.Format("Preview just the {0} Calendar Slide", selectedMonth.Date.ToString("MMMM, yyyy"));
				form.buttonXSelectedPublications.Text = "Preview ALL Selected Calendars";
				foreach (var month in CalendarData.Months.Where(y => y.Days.Any(z => z.ContainsData || z.HasNotes) || y.OutputData.Notes.Any()))
					form.checkedListBoxControlMonths.Items.Add(month, month.Date.ToString("MMMM, yyyy"), CheckState.Checked, true);
				RegistryHelper.MainFormHandle = form.Handle;
				RegistryHelper.MaximizeMainForm = false;
				var result = form.ShowDialog();
				RegistryHelper.MaximizeMainForm = FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = FormMain.Handle;
				if (result == DialogResult.Cancel) return;
				IEnumerable<CalendarOutputData> outputData = null;
				switch (result)
				{
					case DialogResult.Yes:
						if (!selectedMonth.Days.Any(x => x.ContainsData || x.HasNotes) && !selectedMonth.OutputData.Notes.Any())
							if (Utilities.Instance.ShowWarningQuestion(string.Format("There are no records for {0}.\nDo you still want to preview this slide?", selectedMonth.Date.ToString("MMMM, yyyy"))) == DialogResult.No)
								return;
						outputData = new[] { selectedMonth.OutputData };
						break;
					case DialogResult.No:
						outputData = form.checkedListBoxControlMonths.Items.OfType<CheckedListBoxItem>().Where(it => it.CheckState == CheckState.Checked).Select(it => ((CalendarMonth)it.Value).OutputData);
						break;
				}
				PreviewInternal(outputData);
			}
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