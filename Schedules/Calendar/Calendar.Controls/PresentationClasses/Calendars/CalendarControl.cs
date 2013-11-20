﻿using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.Calendar.Controls.BusinessClasses;
using NewBizWiz.Calendar.Controls.InteropClasses;
using NewBizWiz.Calendar.Controls.PresentationClasses.SlideInfo;
using NewBizWiz.Calendar.Controls.PresentationClasses.Views;
using NewBizWiz.Calendar.Controls.PresentationClasses.Views.GridView;
using NewBizWiz.Calendar.Controls.PresentationClasses.Views.MonthView;
using NewBizWiz.Calendar.Controls.Properties;
using NewBizWiz.Calendar.Controls.ToolForms;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Calendar;
using NewBizWiz.Core.Common;
using SettingsManager = NewBizWiz.Core.Calendar.SettingsManager;

namespace NewBizWiz.Calendar.Controls.PresentationClasses.Calendars
{
	[ToolboxItem(false)]
	public partial class CalendarControl : UserControl, ICalendarControl
	{
		protected Schedule _localSchedule = null;

		public CalendarControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			if ((base.CreateGraphics()).DpiX > 96) { }
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate
			{
				if (sender != this)
					LoadCalendar(e.QuickSave);
			});
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
			SlideInfo = new SlideInfoWrapper(this, dockPanelSlideInfo);
			CalendarVisualizer.AssignCloseActiveEditorsonOutSideClick(SlideInfo.ContainedControl);
			dockPanelSlideInfo.Controls.Add(SlideInfo.ContainedControl);
			SlideInfo.Shown += (sender, e) =>
			{
				bool temp = AllowToSave;
				AllowToSave = false;
				Controller.Instance.CalendarVisualizer.SlideInfoButtonItem.Checked = true;
				AllowToSave = temp;
			};
			SlideInfo.Closed += (sender, e) =>
			{
				bool temp = AllowToSave;
				AllowToSave = false;
				Controller.Instance.CalendarVisualizer.SlideInfoButtonItem.Checked = false;
				AllowToSave = temp;
			};
			SlideInfo.ThemeChanged += (sender, e) =>
			{
				MonthView.RefreshData();
			};
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
			if (SettingsNotSaved || (SelectedView != null && SelectedView.SettingsNotSaved) || SlideInfo.SettingsNotSaved)
				SaveCalendarData();
			SlideInfo.Close(false);
		}

		public void ShowCalendar(bool gridView)
		{
			AllowToSave = false;
			Controller.Instance.CalendarVisualizer.MonthsListBoxControl.Items.Clear();
			Controller.Instance.CalendarVisualizer.MonthsListBoxControl.Items.AddRange(CalendarData.Months.Select(x => new ImageListBoxItem(x.Date.ToString("MMM, yyyy"), 0)).ToArray());
			var selectedIndex = CalendarData.Months.Select(m => m.Date).ToList().IndexOf(CalendarSettings.SelectedMonth);
			selectedIndex = selectedIndex > 0 ? selectedIndex : 0;
			if (Controller.Instance.CalendarVisualizer.MonthsListBoxControl.Items.Count > 0)
				Controller.Instance.CalendarVisualizer.MonthsListBoxControl.SelectedIndex = selectedIndex;

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
				SelectedView.ChangeMonth(CalendarData.Months[Controller.Instance.CalendarVisualizer.MonthsListBoxControl.SelectedIndex].Date);
				SlideInfo.LoadData(CalendarData.Months[Controller.Instance.CalendarVisualizer.MonthsListBoxControl.SelectedIndex]);
				SlideInfo.LoadVisibilitySettings();
				UpdateOutputFunctions();
				Splash(false);
			}

			AllowToSave = true;
		}

		public bool SaveCalendarData(string scheduleName = "")
		{
			SelectedView.Save();
			SlideInfo.SaveData();
			if (!string.IsNullOrEmpty(scheduleName))
				_localSchedule.Name = scheduleName;
			Controller.Instance.SaveSchedule(_localSchedule, true, this);
			laCalendarName.Text = CalendarData.Schedule.Name;
			SettingsNotSaved = false;
			return true;
		}

		public void LoadCalendar(bool quickLoad)
		{
			_localSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			laAdvertiser.Text = CalendarData.Schedule.BusinessName;
			laCalendarWindow.Text = CalendarData.Schedule.FlightDateStart.HasValue && CalendarData.Schedule.FlightDateEnd.HasValue ? string.Format("{0} - {1}", new object[] { CalendarData.Schedule.FlightDateStart.Value.ToString("MM/dd/yy"), CalendarData.Schedule.FlightDateEnd.Value.ToString("MM/dd/yy") }) : string.Empty;
			laCalendarName.Text = CalendarData.Schedule.Name;

			MonthView.LoadData(quickLoad);
			GridView.LoadData(quickLoad);
			SlideInfo.LoadData();

			SettingsNotSaved = false;
		}
		#endregion

		#region Output Staff
		public void UpdateOutputFunctions()
		{
			var enable = CalendarData.Months.SelectMany(m => m.Days).Any(d => d.ContainsData || d.HasNotes);
			Controller.Instance.CalendarVisualizer.PreviewButtonItem.Enabled = enable;
			Controller.Instance.CalendarVisualizer.EmailButtonItem.Enabled = enable;
			Controller.Instance.CalendarVisualizer.PowerPointButtonItem.Enabled = enable;
		}

		public void Print()
		{
			if (Controller.Instance.CalendarVisualizer.MonthsListBoxControl.SelectedIndex >= 0)
			{
				CalendarMonth selectedMonth = CalendarData.Months[Controller.Instance.CalendarVisualizer.MonthsListBoxControl.SelectedIndex];
				foreach (CalendarMonth month in CalendarData.Months)
					month.OutputData.PrepareNotes();
				using (var form = new FormSelectCalendar())
				{
					form.Text = "Ad Calendar Slide Output";
					form.pbLogo.Image = Resources.Calendar;
					form.laTitle.Text = "You have several Calendars available for your presentation…";
					form.buttonXCurrentPublication.Text = string.Format("Send ONLY {0} Calendar Slide to PowerPoint", selectedMonth.Date.ToString("MMMM, yyyy"));
					form.buttonXSelectedPublications.Text = "Send all of the Selected Calendars to PowerPoint";
					foreach (var month in CalendarData.Months.Where(y => y.Days.Any(z => z.ContainsData || z.HasNotes) || y.OutputData.Notes.Any()))
						form.checkedListBoxControlMonths.Items.Add(month, month.Date.ToString("MMMM, yyyy"), CheckState.Checked, true);
					RegistryHelper.MainFormHandle = form.Handle;
					RegistryHelper.MaximizeMainForm = false;
					DialogResult result = form.ShowDialog();
					RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
					RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
					if (result != DialogResult.Cancel)
					{
						using (var formProgress = new FormProgress())
						{
							formProgress.TopMost = true;
							Controller.Instance.ShowFloater(() =>
							{
								if (result == DialogResult.Yes)
								{
									formProgress.laProgress.Text = "Creating your Calendar Slide…\nThis will take about 30 seconds…";
									if (!selectedMonth.Days.Any(x => x.ContainsData || x.HasNotes) && !selectedMonth.OutputData.Notes.Any())
										if (Utilities.Instance.ShowWarningQuestion(string.Format("There are no records for {0}.\nDo you still want to send this slide to PowerPoint?", selectedMonth.Date.ToString("MMMM, yyyy"))) == DialogResult.No)
											return;
									formProgress.Show();
									Enabled = false;
									CalendarPowerPointHelper.Instance.AppendCalendar(new[] { selectedMonth.OutputData });
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
												CalendarPowerPointHelper.Instance.AppendCalendar(new[] { month.OutputData });
										}
									}
								}
								Enabled = true;
								formProgress.Close();
							});
						}
					}
				}
			}
		}

		public void Email()
		{
			if (Controller.Instance.CalendarVisualizer.MonthsListBoxControl.SelectedIndex >= 0)
			{
				CalendarMonth selectedMonth = CalendarData.Months[Controller.Instance.CalendarVisualizer.MonthsListBoxControl.SelectedIndex];
				foreach (CalendarMonth month in CalendarData.Months)
					month.OutputData.PrepareNotes();
				using (var form = new FormSelectCalendar())
				{
					form.Text = "Ad Calendar Email Output";
					form.pbLogo.Image = Resources.EmailBig;
					form.laTitle.Text = "You have several Calendars that can be ATTACHED to an email…";
					form.buttonXCurrentPublication.Text = string.Format("Attach just the {0} Calendar Slide to my email message", selectedMonth.Date.ToString("MMMM, yyyy"));
					form.buttonXSelectedPublications.Text = "Attach ALL Selected Calendars to my email message";
					foreach (var month in CalendarData.Months.Where(y => y.Days.Any(z => z.ContainsData || z.HasNotes) || y.OutputData.Notes.Any()))
						form.checkedListBoxControlMonths.Items.Add(month, month.Date.ToString("MMMM, yyyy"), CheckState.Checked, true);
					RegistryHelper.MainFormHandle = form.Handle;
					RegistryHelper.MaximizeMainForm = false;
					DialogResult result = form.ShowDialog();
					RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
					RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
					if (result != DialogResult.Cancel)
					{
						using (var formProgress = new FormProgress())
						{
							formProgress.TopMost = true;
							string tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
							if (result == DialogResult.Yes)
							{
								formProgress.laProgress.Text = "Creating your Calendar Slide…\nThis will take about 30 seconds…";
								if (!selectedMonth.Days.Any(x => x.ContainsData || x.HasNotes) && !selectedMonth.OutputData.Notes.Any())
									if (Utilities.Instance.ShowWarningQuestion(string.Format("There are no records for {0}.\nDo you still want to Email this slide?", selectedMonth.Date.ToString("MMMM, yyyy"))) == DialogResult.No)
										return;
								formProgress.Show();
								Enabled = false;
								CalendarPowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, new[] { selectedMonth.OutputData });
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
								CalendarPowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, emailPages.ToArray());
							}
							Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
							Enabled = true;
							formProgress.Close();
							if (File.Exists(tempFileName))
								using (var formEmail = new FormEmail(Controller.Instance.FormMain, CalendarPowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
								{
									formEmail.Text = "Email this Calendar";
									formEmail.PresentationFile = tempFileName;
									RegistryHelper.MainFormHandle = formEmail.Handle;
									RegistryHelper.MaximizeMainForm = false;
									formEmail.ShowDialog();
									RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
									RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
								}
						}
					}
				}
			}
		}

		public void Preview()
		{
			if (Controller.Instance.CalendarVisualizer.MonthsListBoxControl.SelectedIndex >= 0)
			{
				var selectedMonth = CalendarData.Months[Controller.Instance.CalendarVisualizer.MonthsListBoxControl.SelectedIndex];
				foreach (CalendarMonth month in CalendarData.Months)
					month.OutputData.PrepareNotes();
				using (var form = new FormSelectCalendar())
				{
					form.Text = "Ad Calendar Preview";
					form.pbLogo.Image = Resources.Preview;
					form.laTitle.Text = "You have several Calendars available for preview…";
					form.buttonXCurrentPublication.Text = string.Format("Preview just the {0} Calendar Slide", selectedMonth.Date.ToString("MMMM, yyyy"));
					form.buttonXSelectedPublications.Text = "Preview ALL Selected Calendars";
					foreach (var month in CalendarData.Months.Where(y => y.Days.Any(z => z.ContainsData || z.HasNotes) || y.OutputData.Notes.Any()))
						form.checkedListBoxControlMonths.Items.Add(month, month.Date.ToString("MMMM, yyyy"), CheckState.Checked, true);
					RegistryHelper.MainFormHandle = form.Handle;
					RegistryHelper.MaximizeMainForm = false;
					DialogResult result = form.ShowDialog();
					RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
					RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
					if (result != DialogResult.Cancel)
					{
						using (var formProgress = new FormProgress())
						{
							formProgress.TopMost = true;
							string tempFileName = Path.Combine(Core.Common.SettingsManager.Instance.TempPath, Path.GetFileName(Path.GetTempFileName()));
							if (result == DialogResult.Yes)
							{
								formProgress.laProgress.Text = "Creating your Calendar Slide…\nThis will take about 30 seconds…";
								if (!selectedMonth.Days.Any(x => x.ContainsData || x.HasNotes) && !selectedMonth.OutputData.Notes.Any())
									if (Utilities.Instance.ShowWarningQuestion(string.Format("There are no records for {0}.\nDo you still want to Email this slide?", selectedMonth.Date.ToString("MMMM, yyyy"))) == DialogResult.No)
										return;
								formProgress.Show();
								Enabled = false;
								CalendarPowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, new[] { selectedMonth.OutputData });
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
								CalendarPowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, emailPages.ToArray());
							}
							Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
							Enabled = true;
							formProgress.Close();
							if (File.Exists(tempFileName))
								using (var formPreview = new FormPreview(Controller.Instance.FormMain, CalendarPowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater))
								{
									formPreview.Text = "Preview this Calendar";
									formPreview.PresentationFile = tempFileName;
									RegistryHelper.MainFormHandle = formPreview.Handle;
									RegistryHelper.MaximizeMainForm = false;
									DialogResult previewResult = formPreview.ShowDialog();
									RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
									RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
									if (previewResult != DialogResult.OK)
										Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
									else
										Utilities.Instance.ActivateMiniBar();
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
			BusinessWrapper.Instance.HelpManager.OpenHelpLink(SelectedView.GetType() == typeof(GridViewControl) ? "list" : "ninja");
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

		public Core.Calendar.Calendar CalendarData
		{
			get
			{
				return _localSchedule.GraphicCalendar;
			}
		}

		public CalendarSettings CalendarSettings
		{
			get
			{
				return SettingsManager.Instance.ViewSettings.GraphicCalendarSettings;

			}
		}
		#endregion
	}
}