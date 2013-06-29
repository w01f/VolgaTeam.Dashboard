﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using AdScheduleBuilder.BusinessClasses;
using AdScheduleBuilder.ConfigurationClasses;
using AdScheduleBuilder.InteropClasses;
using AdScheduleBuilder.OutputClasses.OutputControls.Calendar.SettingsViewers;
using AdScheduleBuilder.OutputClasses.OutputForms;
using AdScheduleBuilder.Properties;
using AdScheduleBuilder.ToolForms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class OutputCalendarControl : UserControl, ICalendarControl
	{
		private static OutputCalendarControl _instance;
		private readonly List<MonthViewControl> _monthViews = new List<MonthViewControl>();
		private MonthViewControl _selectedMonth;

		protected OutputCalendarControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			HelpToolTip = new SuperTooltipInfo("HELP", "", "Learn more about the Advertising Calendar", null, null, eTooltipColor.Gray);
			_monthViews = new List<MonthViewControl>();
			Inserts = new List<Insert>();
			ScheduleManager.Instance.SettingsSaved += (sender, e) =>
														  {
															  if (sender != this)
																  UpdateOutput(e.QuickSave);
														  };
		}

		public static OutputCalendarControl Instance
		{
			get
			{
				if (_instance == null)
					_instance = new OutputCalendarControl();
				return _instance;
			}
		}

		#region ICalendarControl Members
		public Schedule LocalSchedule { get; set; }
		public bool AllowToSave { get; set; }
		public List<Insert> Inserts { get; set; }
		public bool SettingsNotSaved { get; set; }
		public SuperTooltipInfo HelpToolTip { get; private set; }

		public virtual void UpdateMonthView()
		{
			if (AllowToSave && comboBoxEditMonthSelector.Properties.Items.Count > 0)
			{
				ShowEmpty();
				pnCalendarView.Controls.Clear();
				DateTime selectedMonth = LocalSchedule.ScheduleMonths[comboBoxEditMonthSelector.SelectedIndex];
				_selectedMonth = _monthViews.Where(x => x.Settings.Month.Month.Equals(selectedMonth.Month) && x.Settings.Month.Year.Equals(selectedMonth.Year)).FirstOrDefault();
				if (_selectedMonth == null)
				{
					_selectedMonth = new MonthViewControl(this);
					MonthCalendarViewSettings monthSettings = LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Where(x => x.Month.Equals(selectedMonth)).FirstOrDefault();
					if (monthSettings == null)
					{
						monthSettings = new MonthCalendarViewSettings(LocalSchedule.ViewSettings.CalendarViewSettings);
						monthSettings.Month = selectedMonth;
						LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Add(monthSettings);
					}
					_selectedMonth.Settings = monthSettings;
					monthSettings.MonthView = _selectedMonth;
					_selectedMonth.Init(selectedMonth);
					_monthViews.Add(_selectedMonth);
				}
				_selectedMonth.RefreshData();
				pnCalendarView.Controls.Add(_selectedMonth);

				HideEmpty();
				Focus();
			}
		}

		public void UpdateToggledOptions()
		{
			if (AllowToSave && _selectedMonth != null)
			{
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowAbbreviationOnly = CalendarsControl.Instance.buttonXDayShowAbbreviation.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowAdSize = CalendarsControl.Instance.buttonXDayShowAdSize.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowPageSize = CalendarsControl.Instance.buttonXDayShowPageSize.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowPercentOfPage = CalendarsControl.Instance.buttonXDayShowPercentOfPage.Checked && ListManager.Instance.ShareUnits.Count > 0;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowColor = CalendarsControl.Instance.buttonXDayShowColor.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowCost = CalendarsControl.Instance.buttonXDayShowCost.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowSection = CalendarsControl.Instance.buttonXDayShowSection.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowBigDate = CalendarsControl.Instance.buttonXDayShowBigDates.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowLegend = CalendarsControl.Instance.buttonXMonthShowLegend.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowTitle = CalendarsControl.Instance.buttonXMonthShowSlideTitle.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowBusinessName = CalendarsControl.Instance.buttonXMonthShowBusinessName.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowDecisionMaker = CalendarsControl.Instance.buttonXMonthShowDecisionMaker.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowLogo = CalendarsControl.Instance.buttonXMonthShowLogo.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowTotalCost = CalendarsControl.Instance.buttonXMonthShowTotalCost.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowAvgCost = CalendarsControl.Instance.buttonXMonthShowAvgCost.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowTotalAds = CalendarsControl.Instance.buttonXMonthShowTotalAds.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowActiveDays = CalendarsControl.Instance.buttonXMonthShowActiveDays.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowComments = CalendarsControl.Instance.buttonXMonthShowComment.Checked;

				if (CalendarsControl.Instance.buttonXColorBlack.Checked)
					LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor = "black";
				else if (CalendarsControl.Instance.buttonXColorBlue.Checked)
					LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor = "blue";
				else if (CalendarsControl.Instance.buttonXColorGray.Checked)
					LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor = "gray";
				else if (CalendarsControl.Instance.buttonXColorGreen.Checked)
					LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor = "green";
				else if (CalendarsControl.Instance.buttonXColorOrange.Checked)
					LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor = "orange";
				else if (CalendarsControl.Instance.buttonXColorTeal.Checked)
					LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor = "teal";
				_selectedMonth.RefreshData();
				SettingsNotSaved = true;
			}
		}

		public void ShowOutputOptions(ICalendarSettingsViewer settingsViewer)
		{
			if (_selectedMonth != null && settingsViewer != null)
			{
				using (var form = new FormCalendarOutputOptions())
				{
					foreach (MonthViewControl monthView in _monthViews)
						form.Settings.Add(monthView.Settings.Clone());
					form.SettingsViewer = settingsViewer;
					if (form.ShowDialog() == DialogResult.OK)
					{
						foreach (MonthCalendarViewSettings monthSettings in form.Settings)
						{
							MonthViewControl monthView = _monthViews.Where(x => x.Settings.Month.Equals(monthSettings.Month)).FirstOrDefault();
							if (monthView != null)
							{
								monthView.Settings.Comments = monthSettings.Comments;
								monthView.Settings.Logo = monthSettings.Logo;
								monthView.Settings.Title = monthSettings.Title;
								monthView.Settings.Legend.Clear();
								monthView.Settings.Legend.AddRange(monthSettings.Legend.ToArray());
							}
						}
						UpdateMonthView();
						SettingsNotSaved = true;
					}
				}
			}
		}

		public void ApplySettings()
		{
			AllowToSave = false;
			CalendarsControl.Instance.buttonXDayShowSection.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableSection;
			CalendarsControl.Instance.buttonXDayShowCost.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableCost;
			CalendarsControl.Instance.buttonXDayShowColor.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableColor;
			CalendarsControl.Instance.buttonXDayShowAbbreviation.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableAbbreviationOnly;
			CalendarsControl.Instance.buttonXDayShowAdSize.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableAdSize;
			CalendarsControl.Instance.buttonXDayShowPageSize.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnablePageSize;
			CalendarsControl.Instance.buttonXDayShowPercentOfPage.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnablePercentOfPage & CalendarsControl.Instance.buttonXDayShowPercentOfPage.Enabled;
			CalendarsControl.Instance.buttonXDayShowBigDates.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableBigDate;

			CalendarsControl.Instance.buttonXDayShowSection.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowSection;
			CalendarsControl.Instance.buttonXDayShowCost.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowCost;
			CalendarsControl.Instance.buttonXDayShowColor.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowColor;
			CalendarsControl.Instance.buttonXDayShowAbbreviation.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowAbbreviationOnly;
			CalendarsControl.Instance.buttonXDayShowAdSize.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowAdSize;
			CalendarsControl.Instance.buttonXDayShowPageSize.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowPageSize;
			CalendarsControl.Instance.buttonXDayShowPercentOfPage.Checked = ListManager.Instance.ShareUnits.Count > 0 && LocalSchedule.ViewSettings.CalendarViewSettings.ShowPercentOfPage & CalendarsControl.Instance.buttonXDayShowPercentOfPage.Enabled;
			CalendarsControl.Instance.buttonXDayShowBigDates.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowBigDate;

			CalendarsControl.Instance.buttonXMonthShowSlideTitle.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableTitle;
			CalendarsControl.Instance.buttonXMonthShowLogo.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableLogo;
			CalendarsControl.Instance.buttonXMonthShowBusinessName.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableBusinessName;
			CalendarsControl.Instance.buttonXMonthShowDecisionMaker.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableDecisionMaker;
			CalendarsControl.Instance.buttonXMonthShowTotalCost.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableTotalCost;
			CalendarsControl.Instance.buttonXMonthShowLegend.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableLegend;
			CalendarsControl.Instance.buttonXMonthShowAvgCost.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableAvgCost;
			CalendarsControl.Instance.buttonXMonthShowComment.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableComments;
			CalendarsControl.Instance.buttonXMonthShowTotalAds.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableTotalAds;
			CalendarsControl.Instance.buttonXMonthShowActiveDays.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableActiveDays;

			CalendarsControl.Instance.buttonXMonthShowSlideTitle.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowTitle;
			CalendarsControl.Instance.buttonXMonthShowLogo.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowLogo;
			CalendarsControl.Instance.buttonXMonthShowBusinessName.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowBusinessName;
			CalendarsControl.Instance.buttonXMonthShowDecisionMaker.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowDecisionMaker;
			CalendarsControl.Instance.buttonXMonthShowTotalCost.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowTotalCost;
			CalendarsControl.Instance.buttonXMonthShowLegend.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowLegend;
			CalendarsControl.Instance.buttonXMonthShowAvgCost.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowAvgCost;
			CalendarsControl.Instance.buttonXMonthShowComment.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowComments;
			CalendarsControl.Instance.buttonXMonthShowTotalAds.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowTotalAds;
			CalendarsControl.Instance.buttonXMonthShowActiveDays.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowActiveDays;

			CalendarsControl.Instance.buttonXColorBlack.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableBlack;
			CalendarsControl.Instance.buttonXColorBlue.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableBlue;
			CalendarsControl.Instance.buttonXColorGray.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableGray;
			CalendarsControl.Instance.buttonXColorGreen.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableGreen;
			CalendarsControl.Instance.buttonXColorOrange.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableOrange;
			CalendarsControl.Instance.buttonXColorTeal.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableTeal;

			CalendarsControl.Instance.buttonXColorBlack.Checked = false;
			CalendarsControl.Instance.buttonXColorBlue.Checked = false;
			CalendarsControl.Instance.buttonXColorGray.Checked = false;
			CalendarsControl.Instance.buttonXColorGreen.Checked = false;
			CalendarsControl.Instance.buttonXColorOrange.Checked = false;
			CalendarsControl.Instance.buttonXColorTeal.Checked = false;
			switch (LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor)
			{
				case "black":
					CalendarsControl.Instance.buttonXColorBlack.Checked = true;
					break;
				case "blue":
					CalendarsControl.Instance.buttonXColorBlue.Checked = true;
					break;
				case "gray":
					CalendarsControl.Instance.buttonXColorGray.Checked = true;
					break;
				case "green":
					CalendarsControl.Instance.buttonXColorGreen.Checked = true;
					break;
				case "orange":
					CalendarsControl.Instance.buttonXColorOrange.Checked = true;
					break;
				case "teal":
					CalendarsControl.Instance.buttonXColorTeal.Checked = true;
					break;
			}
			AllowToSave = true;
		}

		public void UpdateOutput(bool quickLoad)
		{
			LocalSchedule = ScheduleManager.Instance.GetLocalSchedule();
			laScheduleWindow.Text = string.Format("{0} - {1}", new object[] { LocalSchedule.FlightDateStart.ToString("MM/dd/yy"), LocalSchedule.FlightDateEnd.ToString("MM/dd/yy") });
			laScheduleName.Text = LocalSchedule.Name;
			laAdvertiser.Text = LocalSchedule.BusinessName + (!string.IsNullOrEmpty(LocalSchedule.AccountNumber) ? (" - " + LocalSchedule.AccountNumber) : string.Empty);

			if (!quickLoad)
			{
				Inserts.Clear();
				foreach (Publication publication in LocalSchedule.Publications)
					Inserts.AddRange(publication.Inserts.Where(x => x.Date != DateTime.MinValue));

				ApplySettings();

				AllowToSave = false;

				PrepareMonthViews();

				comboBoxEditMonthSelector.Properties.Items.Clear();
				comboBoxEditMonthSelector.Properties.Items.AddRange(LocalSchedule.ScheduleMonths.Select(x => new ImageListBoxItem(x.ToString("MMM, yyyy"), 0)).ToArray());
				if (comboBoxEditMonthSelector.Properties.Items.Count > 0)
					comboBoxEditMonthSelector.SelectedIndex = 0;

				UpdateMonthView();
				AllowToSave = true;
			}
			else
				UpdateMonthSettings();

			SettingsNotSaved = false;
		}

		public void ResetToDefault()
		{
			LocalSchedule.ViewSettings.CalendarViewSettings.ResetToDefault();
			ApplySettings();
			AllowToSave = false;
			UpdateMonthView();
			_selectedMonth.RefreshData();
			AllowToSave = true;
		}

		public void OpenHelp()
		{
			HelpManager.Instance.OpenHelpLink("calendars");
		}
		#endregion

		public static void RemoveInstance()
		{
			try
			{
				_instance.Dispose();
			}
			catch { }
			finally
			{
				_instance = null;
			}
		}

		private void PrepareMonthViews()
		{
			_monthViews.Clear();
			var startDate = new DateTime(LocalSchedule.FlightDateStart.Year, LocalSchedule.FlightDateStart.Month, 1);
			var endDate = new DateTime(LocalSchedule.FlightDateEnd.Year, LocalSchedule.FlightDateEnd.Month, 1);
			MonthViewControl monthView = null;
			MonthCalendarViewSettings monthSettings = null;
			while (startDate < endDate)
			{
				monthView = new MonthViewControl(this);
				monthSettings = LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Where(x => x.Month.Equals(startDate)).FirstOrDefault();
				if (monthSettings == null)
				{
					monthSettings = new MonthCalendarViewSettings(LocalSchedule.ViewSettings.CalendarViewSettings);
					monthSettings.Month = startDate;
					LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Add(monthSettings);
				}
				monthView.Settings = monthSettings;
				monthSettings.MonthView = monthView;
				monthView.Init(startDate);
				_monthViews.Add(monthView);
				startDate = startDate.AddMonths(1);
			}
			monthView = new MonthViewControl(this);
			monthSettings = LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Where(x => x.Month.Equals(endDate)).FirstOrDefault();
			if (monthSettings == null)
			{
				monthSettings = new MonthCalendarViewSettings(LocalSchedule.ViewSettings.CalendarViewSettings);
				monthSettings.Month = endDate;
				LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Add(monthSettings);
			}
			monthView.Settings = monthSettings;
			monthSettings.MonthView = monthView;
			monthView.Init(endDate);
			_monthViews.Add(monthView);
		}

		private void UpdateMonthSettings()
		{
			foreach (MonthViewControl monthView in _monthViews)
			{
				MonthCalendarViewSettings monthSettings = LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Where(x => x.Month.Equals(monthView.Month)).FirstOrDefault();
				if (monthSettings == null)
				{
					monthSettings = new MonthCalendarViewSettings(LocalSchedule.ViewSettings.CalendarViewSettings);
					monthSettings.Month = monthView.Month;
					LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Add(monthSettings);
				}
				monthView.Settings = monthSettings;
				monthSettings.MonthView = monthView;
			}
		}

		public void ShowEmpty()
		{
			pnEmpty.BringToFront();
		}

		public void HideEmpty()
		{
			pnEmpty.SendToBack();
		}

		private void comboBoxEditMonthSelector_SelectedIndexChanged(object sender, EventArgs e)
		{
			UpdateMonthView();
		}

		#region Output Staff
		public void PrintOutput()
		{
			if (_selectedMonth != null)
			{
				using (var form = new FormSelectPublication())
				{
					form.Text = "Ad Calendar Slide Output";
					form.pbLogo.Image = Resources.Calendar;
					form.laTitle.Text = "You have several Advertising Calendars available for your presentation…";
					form.buttonXCurrentPublication.Text = string.Format("Send ONLY {0} Calendar Slide to PowerPoint", _selectedMonth.Settings.Month.ToString("MMMM, yyyy"));
					form.buttonXSelectedPublications.Text = "Send all of the Selected Ad Calendars to PowerPoint";
					foreach (MonthViewControl monthView in _monthViews.Where(x => Inserts.Where(y => y.Date.Year.Equals(x.Month.Year) && y.Date.Month.Equals(x.Month.Month)).Count() > 0))
						form.checkedListBoxControlPublications.Items.Add(monthView, monthView.Settings.Month.ToString("MMMM, yyyy"), CheckState.Checked, true);
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
								if (Inserts.Where(y => y.Date.Year.Equals(_selectedMonth.Month.Year) && y.Date.Month.Equals(_selectedMonth.Month.Month)).Count() == 0)
									if (AppManager.ShowWarningQuestion(string.Format("There are no Ads scheduled for {0}.\nDo you still want to send this slide to PowerPoint?", _selectedMonth.Month.ToString("MMMM, yyyy"))) == DialogResult.No)
										return;
								formProgress.Show();
								Enabled = false;
								_selectedMonth.PrintOutput();
							}
							else if (result == DialogResult.No)
							{
								formProgress.laProgress.Text = form.checkedListBoxControlPublications.CheckedItems.Count == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Several Calendar slides…\nThis will take a few minutes…";
								formProgress.Show();
								Enabled = false;
								foreach (CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
								{
									if (item.CheckState == CheckState.Checked)
									{
										var monthView = item.Value as MonthViewControl;
										if (monthView != null)
											monthView.PrintOutput();
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
			using (var form = new FormSelectPublication())
			{
				form.Text = "Ad Calendar Email Output";
				form.pbLogo.Image = Resources.EmailBig;
				form.laTitle.Text = "You have several Advertising Calendars that can be ATTACHED to an email…";
				form.buttonXCurrentPublication.Text = string.Format("Attach just the {0} Calendar Slide to my email message", _selectedMonth.Settings.Month.ToString("MMMM, yyyy"));
				form.buttonXSelectedPublications.Text = "Attach ALL Selected Ad Calendars to my email message";
				foreach (MonthViewControl monthView in _monthViews.Where(x => Inserts.Where(y => y.Date.Year.Equals(x.Month.Year) && y.Date.Month.Equals(x.Month.Month)).Count() > 0))
					form.checkedListBoxControlPublications.Items.Add(monthView, monthView.Settings.Month.ToString("MMMM, yyyy"), CheckState.Checked, true);
				RegistryHelper.MainFormHandle = form.Handle;
				RegistryHelper.MaximizeMainForm = false;
				DialogResult result = form.ShowDialog();
				RegistryHelper.MaximizeMainForm = true;
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
							if (Inserts.Where(y => y.Date.Year.Equals(_selectedMonth.Month.Year) && y.Date.Month.Equals(_selectedMonth.Month.Month)).Count() == 0)
								if (AppManager.ShowWarningQuestion(string.Format("There are no Ads scheduled for {0}.\nDo you still want to Email this slide?", _selectedMonth.Month.ToString("MMMM, yyyy"))) == DialogResult.No)
									return;
							formProgress.Show();
							Enabled = false;
							PowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, new[] { _selectedMonth });
						}
						else if (result == DialogResult.No)
						{
							formProgress.laProgress.Text = form.checkedListBoxControlPublications.CheckedItems.Count == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Several Calendar slides…\nThis will take a few minutes…";
							formProgress.Show();
							Enabled = false;
							var emailPages = new List<MonthViewControl>();
							foreach (CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
							{
								if (item.CheckState == CheckState.Checked)
								{
									var monthView = item.Value as MonthViewControl;
									if (monthView != null)
										emailPages.Add(monthView);
								}
							}
							PowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, emailPages.ToArray());
						}
						Enabled = true;
						formProgress.Close();
						if (File.Exists(tempFileName))
							using (var formEmail = new FormEmail())
							{
								formEmail.Text = "Email this Advertising Calendar";
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

		public void Preview()
		{
			using (var form = new FormSelectPublication())
			{
				form.Text = "Ad Calendar Preview";
				form.pbLogo.Image = Resources.PreviewCalendar;
				form.laTitle.Text = "You have several Advertising Calendars that can be ATTACHED to an email…";
				form.buttonXCurrentPublication.Text = string.Format("Preview just the {0} Calendar Slide", _selectedMonth.Settings.Month.ToString("MMMM, yyyy"));
				form.buttonXSelectedPublications.Text = "Preview ALL Selected Ad Calendars";
				foreach (MonthViewControl monthView in _monthViews.Where(x => Inserts.Where(y => y.Date.Year.Equals(x.Month.Year) && y.Date.Month.Equals(x.Month.Month)).Count() > 0))
					form.checkedListBoxControlPublications.Items.Add(monthView, monthView.Settings.Month.ToString("MMMM, yyyy"), CheckState.Checked, true);
				RegistryHelper.MainFormHandle = form.Handle;
				RegistryHelper.MaximizeMainForm = false;
				DialogResult result = form.ShowDialog();
				RegistryHelper.MaximizeMainForm = true;
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
							if (Inserts.Where(y => y.Date.Year.Equals(_selectedMonth.Month.Year) && y.Date.Month.Equals(_selectedMonth.Month.Month)).Count() == 0)
								if (AppManager.ShowWarningQuestion(string.Format("There are no Ads scheduled for {0}.\nDo you still want to create this slide?", _selectedMonth.Month.ToString("MMMM, yyyy"))) == DialogResult.No)
									return;
							formProgress.Show();
							Enabled = false;
							PowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, new[] { _selectedMonth });
						}
						else if (result == DialogResult.No)
						{
							formProgress.laProgress.Text = form.checkedListBoxControlPublications.CheckedItems.Count == 2 ? "Creating 2 (two) Calendar slides…\nThis will take about a minute…" : "Creating Several Calendar slides…\nThis will take a few minutes…";
							formProgress.Show();
							Enabled = false;
							var emailPages = new List<MonthViewControl>();
							foreach (CheckedListBoxItem item in form.checkedListBoxControlPublications.Items)
							{
								if (item.CheckState == CheckState.Checked)
								{
									var monthView = item.Value as MonthViewControl;
									if (monthView != null)
										emailPages.Add(monthView);
								}
							}
							PowerPointHelper.Instance.PrepareCalendarEmail(tempFileName, emailPages.ToArray());
						}
						Enabled = true;
						formProgress.Close();
						if (File.Exists(tempFileName))
							using (var formPreview = new FormPreview())
							{
								formPreview.Text = "Preview Advertising Calendar";
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
		#endregion
	}
}