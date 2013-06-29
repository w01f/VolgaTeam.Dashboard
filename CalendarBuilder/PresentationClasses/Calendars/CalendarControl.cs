using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CalendarBuilder.BusinessClasses;
using CalendarBuilder.ConfigurationClasses;
using CalendarBuilder.InteropClasses;
using CalendarBuilder.PresentationClasses.DayProperties;
using CalendarBuilder.PresentationClasses.SlideInfo;
using CalendarBuilder.PresentationClasses.Views;
using CalendarBuilder.PresentationClasses.Views.GridView;
using CalendarBuilder.PresentationClasses.Views.MonthView;
using CalendarBuilder.Properties;
using CalendarBuilder.ToolForms;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors.Controls;

namespace CalendarBuilder.PresentationClasses.Calendars
{
	[ToolboxItem(false)]
	public partial class CalendarControl : UserControl, ICalendarControl
	{
		protected CalendarStyle _calendarStyle;
		protected Schedule _localSchedule = null;

		public CalendarControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			if ((base.CreateGraphics()).DpiX > 96) { }
			ScheduleManager.Instance.SettingsSaved += (sender, e) =>
														  {
															  if (sender != this)
															  {
																  LoadCalendar(e.QuickSave);
															  }
														  };

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

			#region Day Properties Initialization
			DayProperties = new DayPropertiesWrapper(this, dockPanelDayProperties);
			CalendarVisualizer.AssignCloseActiveEditorsonOutSideClick(DayProperties.ContainedControl);
			dockPanelDayProperties.Controls.Add(DayProperties.ContainedControl);
			DayProperties.Shown += (sender, e) => { SlideInfo.Close(); };
			DayProperties.Closed += (sender, e) => { };
			DayProperties.DataSaved += (sender, e) =>
										   {
											   MonthView.RefreshData();
											   GridView.RefreshData();
											   SlideInfo.LoadData(reload: true);
											   SettingsNotSaved = true;
										   };
			#endregion

			#region Slide Info Initialization
			SlideInfo = new SlideInfoWrapper(this, dockPanelSlideInfo);
			CalendarVisualizer.AssignCloseActiveEditorsonOutSideClick(SlideInfo.ContainedControl);
			dockPanelSlideInfo.Controls.Add(SlideInfo.ContainedControl);
			SlideInfo.Shown += (sender, e) =>
								   {
									   DayProperties.Close();
									   bool temp = AllowToSave;
									   AllowToSave = false;
									   CalendarVisualizer.Instance.SlideInfoButtonItem.Checked = true;
									   AllowToSave = temp;
								   };
			SlideInfo.Closed += (sender, e) =>
									{
										bool temp = AllowToSave;
										AllowToSave = false;
										CalendarVisualizer.Instance.SlideInfoButtonItem.Checked = false;
										AllowToSave = temp;
									};
			SlideInfo.DateSaved += (sender, e) => { SettingsNotSaved = true; };
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

		public void LeaveCalendar()
		{
			SlideInfo.Close(false);
			if (SettingsNotSaved || (SelectedView != null && SelectedView.SettingsNotSaved) || DayProperties.SettingsNotSaved || SlideInfo.SettingsNotSaved)
				SaveCalendarData();
		}

		public void ShowCalendar()
		{
			AllowToSave = false;
			CalendarVisualizer.Instance.MonthsListBoxControl.Items.Clear();
			CalendarVisualizer.Instance.MonthsListBoxControl.Items.AddRange(CalendarData.Months.Select(x => new ImageListBoxItem(x.Date.ToString("MMM, yyyy"), 0)).ToArray());
			if (CalendarVisualizer.Instance.MonthsListBoxControl.Items.Count > 0)
				CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex = 0;
			CalendarVisualizer.Instance.MonthViewButtonItem.Checked = !CalendarSettings.GridVisible;
			CalendarVisualizer.Instance.GridViewButtonItem.Checked = CalendarSettings.GridVisible;
			LoadView();
			SlideInfo.LoadData(month: CalendarData.Months[CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex]);
			SlideInfo.LoadVisibilitySettings();
			AllowToSave = true;
		}

		public bool SaveCalendarData(string scheduleName = "")
		{
			SelectedView.Save();
			SlideInfo.SaveData(force: true);
			if (_calendarStyle == CalendarStyle.Advanced)
				DayProperties.SaveData(force: true);
			if (!string.IsNullOrEmpty(scheduleName))
				_localSchedule.Name = scheduleName;
			ScheduleManager.Instance.SaveSchedule(_localSchedule, true, this);
			LoadCalendar(true);
			laCalendarName.Text = CalendarData.Schedule.Name;
			SettingsNotSaved = false;
			return true;
		}

		public void LoadCalendar(bool quickLoad)
		{
			_localSchedule = ScheduleManager.Instance.GetLocalSchedule();

			laAdvertiser.Text = CalendarData.Schedule.BusinessName;
			laCalendarWindow.Text = CalendarData.Schedule.FlightDateStart.HasValue && CalendarData.Schedule.FlightDateEnd.HasValue ? string.Format("{0} - {1}", new object[] { CalendarData.Schedule.FlightDateStart.Value.ToString("MM/dd/yy"), CalendarData.Schedule.FlightDateEnd.Value.ToString("MM/dd/yy") }) : string.Empty;
			laCalendarName.Text = CalendarData.Schedule.Name;

			MonthView.LoadData(quickLoad);
			GridView.LoadData(quickLoad);

			MonthView.Decorate(_calendarStyle);
			GridView.Decorate(_calendarStyle);
			DayProperties.Decorate(_calendarStyle);
			SlideInfo.Decorate(_calendarStyle);

			DayProperties.Close();

			SettingsNotSaved = false;
		}
		#endregion

		#region View Methods
		public void LoadView()
		{
			bool temp = AllowToSave;
			AllowToSave = false;
			if (SelectedView != null)
			{
				if (SelectedView.SettingsNotSaved)
					SelectedView.Save();
				SelectedView.CopyPasteManager.ResetCopy();
				SelectedView.CopyPasteManager.ResetPaste();
			}
			if (CalendarSettings.GridVisible)
			{
				SelectedView = GridView;
				GridView.BringToFront();
			}
			else
			{
				SelectedView = MonthView;
				MonthView.BringToFront();
			}
			Splash(true);
			SelectedView.ChangeMonth(CalendarData.Months[CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex].Date);
			Splash(false);
			AllowToSave = temp;
		}

		public void SaveView()
		{
			SelectedView.Save();
			CalendarSettings.GridVisible = CalendarVisualizer.Instance.GridViewButtonItem.Checked;
			SettingsManager.Instance.ViewSettings.Save();
			LoadView();
		}
		#endregion

		#region Output Staff
		public void Print()
		{
			if (CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex >= 0)
			{
				CalendarMonth selectedMonth = CalendarData.Months[CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex];
				foreach (CalendarMonth month in CalendarData.Months)
					month.OutputData.PrepareNotes();
				using (var form = new FormSelectCalendar())
				{
					form.Text = "Ad Calendar Slide Output";
					form.pbLogo.Image = Resources.Calendar;
					form.laTitle.Text = "You have several Calendars available for your presentation…";
					form.buttonXCurrentPublication.Text = string.Format("Send ONLY {0} Calendar Slide to PowerPoint", selectedMonth.Date.ToString("MMMM, yyyy"));
					form.buttonXSelectedPublications.Text = "Send all of the Selected Calendars to PowerPoint";
					foreach (CalendarMonth month in CalendarData.Months.Where(y => y.Days.Where(z => z.ContainsData).Count() > 0 || y.OutputData.Notes.Count > 0))
						form.checkedListBoxControlMonths.Items.Add(month, month.Date.ToString("MMMM, yyyy"), CheckState.Checked, true);
					RegistryHelper.MainFormHandle = form.Handle;
					RegistryHelper.MaximizeMainForm = false;
					DialogResult result = form.ShowDialog();
					RegistryHelper.MaximizeMainForm = FormMain.Instance.IsMaximized;
					RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
					if (result != DialogResult.Cancel)
					{
						using (var formProgress = new FormProgress())
						{
							formProgress.TopMost = true;
							if (result == DialogResult.Yes)
							{
								formProgress.laProgress.Text = "Creating your Calendar Slide…\nThis will take about 30 seconds…";
								if (selectedMonth.Days.Where(x => x.ContainsData).Count() == 0 && selectedMonth.OutputData.Notes.Count == 0)
									if (AppManager.ShowWarningQuestion(string.Format("There are no records for {0}.\nDo you still want to send this slide to PowerPoint?", selectedMonth.Date.ToString("MMMM, yyyy"))) == DialogResult.No)
										return;
								formProgress.Show();
								Enabled = false;
								PowerPointHelper.Instance.AppendCalendar(selectedMonth.OutputData);
							}
							else if (result == DialogResult.No)
							{
								formProgress.laProgress.Text = form.checkedListBoxControlMonths.CheckedItems.Count == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Several Calendar slides…\nThis will take a few minutes…";
								formProgress.Show();
								Enabled = false;
								foreach (CheckedListBoxItem item in form.checkedListBoxControlMonths.Items)
								{
									if (item.CheckState == CheckState.Checked)
									{
										var month = item.Value as CalendarMonth;
										if (month != null)
											PowerPointHelper.Instance.AppendCalendar(month.OutputData);
									}
								}
							}
							Enabled = true;
							formProgress.Close();
						}
						using (var formOutput = new FormSlideOutput())
						{
							if (formOutput.ShowDialog() != DialogResult.OK)
								AppManager.ActivateForm(FormMain.Instance.Handle, FormMain.Instance.IsMaximized, false);
							else
							{
								AppManager.ActivatePowerPoint();
								AppManager.ActivateMiniBar();
							}
						}
					}
				}
			}
		}

		public void Email()
		{
			if (CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex >= 0)
			{
				CalendarMonth selectedMonth = CalendarData.Months[CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex];
				foreach (CalendarMonth month in CalendarData.Months)
					month.OutputData.PrepareNotes();
				using (var form = new FormSelectCalendar())
				{
					form.Text = "Ad Calendar Email Output";
					form.pbLogo.Image = Resources.EmailBig;
					form.laTitle.Text = "You have several Calendars that can be ATTACHED to an email…";
					form.buttonXCurrentPublication.Text = string.Format("Attach just the {0} Calendar Slide to my email message", selectedMonth.Date.ToString("MMMM, yyyy"));
					form.buttonXSelectedPublications.Text = "Attach ALL Selected Calendars to my email message";
					foreach (CalendarMonth month in CalendarData.Months.Where(y => y.Days.Where(z => z.ContainsData).Count() > 0 || y.OutputData.Notes.Count > 0))
						form.checkedListBoxControlMonths.Items.Add(month, month.Date.ToString("MMMM, yyyy"), CheckState.Checked, true);
					RegistryHelper.MainFormHandle = form.Handle;
					RegistryHelper.MaximizeMainForm = false;
					DialogResult result = form.ShowDialog();
					RegistryHelper.MaximizeMainForm = FormMain.Instance.IsMaximized;
					RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
					if (result != DialogResult.Cancel)
					{
						using (var formProgress = new FormProgress())
						{
							formProgress.TopMost = true;
							string tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
							if (result == DialogResult.Yes)
							{
								formProgress.laProgress.Text = "Creating your Calendar Slide…\nThis will take about 30 seconds…";
								if (selectedMonth.Days.Where(x => x.ContainsData).Count() == 0)
									if (AppManager.ShowWarningQuestion(string.Format("There are no records for {0}.\nDo you still want to Email this slide?", selectedMonth.Date.ToString("MMMM, yyyy"))) == DialogResult.No)
										return;
								formProgress.Show();
								Enabled = false;
								PowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, new[] { selectedMonth.OutputData });
							}
							else if (result == DialogResult.No)
							{
								formProgress.laProgress.Text = form.checkedListBoxControlMonths.CheckedItems.Count == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Several Calendar slides…\nThis will take a few minutes…";
								formProgress.Show();
								Enabled = false;
								var emailPages = new List<CalendarOutputData>();
								foreach (CheckedListBoxItem item in form.checkedListBoxControlMonths.Items)
								{
									if (item.CheckState == CheckState.Checked)
									{
										var month = item.Value as CalendarMonth;
										if (month != null)
											emailPages.Add(month.OutputData);
									}
								}
								PowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, emailPages.ToArray());
							}
							Enabled = true;
							formProgress.Close();
							if (File.Exists(tempFileName))
								using (var formEmail = new FormEmail())
								{
									formEmail.Text = "Email this Calendar";
									formEmail.PresentationFile = tempFileName;
									RegistryHelper.MainFormHandle = formEmail.Handle;
									RegistryHelper.MaximizeMainForm = false;
									formEmail.ShowDialog();
									RegistryHelper.MaximizeMainForm = FormMain.Instance.IsMaximized;
									RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
								}
						}
					}
				}
			}
		}

		public void Preview()
		{
			if (CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex >= 0)
			{
				CalendarMonth selectedMonth = CalendarData.Months[CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex];
				foreach (CalendarMonth month in CalendarData.Months)
					month.OutputData.PrepareNotes();
				using (var form = new FormSelectCalendar())
				{
					form.Text = "Ad Calendar Preview";
					form.pbLogo.Image = Resources.Preview;
					form.laTitle.Text = "You have several Calendars available for preview…";
					form.buttonXCurrentPublication.Text = string.Format("Preview just the {0} Calendar Slide", selectedMonth.Date.ToString("MMMM, yyyy"));
					form.buttonXSelectedPublications.Text = "Preview ALL Selected Calendars";
					foreach (CalendarMonth month in CalendarData.Months.Where(y => y.Days.Where(z => z.ContainsData).Count() > 0 || y.OutputData.Notes.Count > 0))
						form.checkedListBoxControlMonths.Items.Add(month, month.Date.ToString("MMMM, yyyy"), CheckState.Checked, true);
					RegistryHelper.MainFormHandle = form.Handle;
					RegistryHelper.MaximizeMainForm = false;
					DialogResult result = form.ShowDialog();
					RegistryHelper.MaximizeMainForm = FormMain.Instance.IsMaximized;
					RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
					if (result != DialogResult.Cancel)
					{
						using (var formProgress = new FormProgress())
						{
							formProgress.TopMost = true;
							string tempFileName = Path.Combine(SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
							if (result == DialogResult.Yes)
							{
								formProgress.laProgress.Text = "Creating your Calendar Slide…\nThis will take about 30 seconds…";
								if (selectedMonth.Days.Where(x => x.ContainsData).Count() == 0)
									if (AppManager.ShowWarningQuestion(string.Format("There are no records for {0}.\nDo you still want to Email this slide?", selectedMonth.Date.ToString("MMMM, yyyy"))) == DialogResult.No)
										return;
								formProgress.Show();
								Enabled = false;
								PowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, new[] { selectedMonth.OutputData });
							}
							else if (result == DialogResult.No)
							{
								formProgress.laProgress.Text = form.checkedListBoxControlMonths.CheckedItems.Count == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Several Calendar slides…\nThis will take a few minutes…";
								formProgress.Show();
								Enabled = false;
								var emailPages = new List<CalendarOutputData>();
								foreach (CheckedListBoxItem item in form.checkedListBoxControlMonths.Items)
								{
									if (item.CheckState == CheckState.Checked)
									{
										var month = item.Value as CalendarMonth;
										if (month != null)
											emailPages.Add(month.OutputData);
									}
								}
								PowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, emailPages.ToArray());
							}
							Enabled = true;
							formProgress.Close();
							if (File.Exists(tempFileName))
								using (var formPreview = new FormPreview())
								{
									formPreview.Text = "Preview this Calendar";
									formPreview.PresentationFile = tempFileName;
									RegistryHelper.MainFormHandle = formPreview.Handle;
									RegistryHelper.MaximizeMainForm = false;
									DialogResult previewResult = formPreview.ShowDialog();
									RegistryHelper.MaximizeMainForm = FormMain.Instance.IsMaximized;
									RegistryHelper.MainFormHandle = FormMain.Instance.Handle;
									if (previewResult != DialogResult.OK)
										AppManager.ActivateForm(FormMain.Instance.Handle, true, false);
									else
									{
										AppManager.ActivatePowerPoint();
										AppManager.ActivateMiniBar();
									}
								}
						}
					}
				}
			}
		}
		#endregion

		#region Other Ribbon Operations
		public void OpenHelp()
		{
			switch (_calendarStyle)
			{
				case CalendarStyle.Advanced:
					HelpManager.Instance.OpenHelpLink(SelectedView.GetType() == typeof(GridViewControl) ? "nerdgrid" : "nerdcal");
					break;
				case CalendarStyle.Graphic:
					HelpManager.Instance.OpenHelpLink(SelectedView.GetType() == typeof(GridViewControl) ? "coolgrid" : "coolcal");
					break;
				case CalendarStyle.Simple:
					HelpManager.Instance.OpenHelpLink(SelectedView.GetType() == typeof(GridViewControl) ? "easygrid" : "easycal");
					break;
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

		public DayPropertiesWrapper DayProperties { get; private set; }
		public SlideInfoWrapper SlideInfo { get; private set; }

		public bool AllowToSave { get; set; }
		public bool SettingsNotSaved { get; set; }

		public Calendar CalendarData
		{
			get
			{
				switch (_calendarStyle)
				{
					case CalendarStyle.Advanced:
						return _localSchedule.AdvancedCalendar;
					case CalendarStyle.Graphic:
						return _localSchedule.GraphicCalendar;
					case CalendarStyle.Simple:
						return _localSchedule.SimpleCalendar;
					default:
						return null;
				}
			}
		}

		public CalendarSettings CalendarSettings
		{
			get
			{
				switch (_calendarStyle)
				{
					case CalendarStyle.Advanced:
						return SettingsManager.Instance.ViewSettings.AdvancedCalendarSettings;
					case CalendarStyle.Graphic:
						return SettingsManager.Instance.ViewSettings.GraphicCalendarSettings;
					case CalendarStyle.Simple:
						return SettingsManager.Instance.ViewSettings.SimpleCalendarSettings;
					default:
						return null;
				}
			}
		}
		#endregion
	}
}