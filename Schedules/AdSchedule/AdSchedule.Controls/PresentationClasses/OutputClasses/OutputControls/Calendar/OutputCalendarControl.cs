using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors.Controls;
using NewBizWiz.AdSchedule.Controls.BusinessClasses;
using NewBizWiz.AdSchedule.Controls.InteropClasses;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar.SettingsViewers;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms;
using NewBizWiz.AdSchedule.Controls.Properties;
using NewBizWiz.AdSchedule.Controls.ToolForms;
using NewBizWiz.CommonGUI.Preview;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.AdSchedule;
using NewBizWiz.Core.Common;
using ListManager = NewBizWiz.Core.AdSchedule.ListManager;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	[ToolboxItem(false)]
	public partial class OutputCalendarControl : UserControl, ICalendarControl
	{
		private readonly List<MonthViewControl> _monthViews = new List<MonthViewControl>();
		private MonthViewControl _selectedMonth;

		public OutputCalendarControl()
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			HelpToolTip = new SuperTooltipInfo("HELP", "", "Learn more about the Advertising Calendar", null, null, eTooltipColor.Gray);
			_monthViews = new List<MonthViewControl>();
			Inserts = new List<Insert>();
			BusinessWrapper.Instance.ScheduleManager.SettingsSaved += (sender, e) => Controller.Instance.FormMain.Invoke((MethodInvoker)delegate
			{
				if (sender != this)
					UpdateOutput(e.QuickSave);
			});
		}

		#region ICalendarControl Members
		public Schedule LocalSchedule { get; set; }
		public bool AllowToSave { get; set; }
		public List<Insert> Inserts { get; set; }
		public bool SettingsNotSaved { get; set; }
		public SuperTooltipInfo HelpToolTip { get; private set; }

		public virtual void UpdateMonthView()
		{
			if (AllowToSave && Controller.Instance.CalendarMonthList.Items.Count > 0)
			{
				ShowEmpty();
				pnCalendarView.Controls.Clear();
				DateTime selectedMonth = LocalSchedule.ScheduleMonths[Controller.Instance.CalendarMonthList.SelectedIndex];
				_selectedMonth = _monthViews.FirstOrDefault(x => x.Settings.Month.Month.Equals(selectedMonth.Month) && x.Settings.Month.Year.Equals(selectedMonth.Year));
				if (_selectedMonth == null)
				{
					_selectedMonth = new MonthViewControl(this);
					MonthCalendarViewSettings monthSettings = LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.FirstOrDefault(x => x.Month.Equals(selectedMonth));
					if (monthSettings == null)
					{
						monthSettings = new MonthCalendarViewSettings(LocalSchedule.ViewSettings.CalendarViewSettings);
						monthSettings.Month = selectedMonth;
						LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Add(monthSettings);
					}
					_selectedMonth.Settings = monthSettings;
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
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowAbbreviationOnly = Controller.Instance.Calendars.buttonXDayShowAbbreviation.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowAdSize = Controller.Instance.Calendars.buttonXDayShowAdSize.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowPageSize = Controller.Instance.Calendars.buttonXDayShowPageSize.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowPercentOfPage = Controller.Instance.Calendars.buttonXDayShowPercentOfPage.Checked && ListManager.Instance.ShareUnits.Count > 0;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowColor = Controller.Instance.Calendars.buttonXDayShowColor.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowCost = Controller.Instance.Calendars.buttonXDayShowCost.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowSection = Controller.Instance.Calendars.buttonXDayShowSection.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowBigDate = Controller.Instance.Calendars.buttonXDayShowBigDates.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowLegend = Controller.Instance.Calendars.buttonXMonthShowLegend.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowTitle = Controller.Instance.Calendars.buttonXMonthShowSlideTitle.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowBusinessName = Controller.Instance.Calendars.buttonXMonthShowBusinessName.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowDecisionMaker = Controller.Instance.Calendars.buttonXMonthShowDecisionMaker.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowLogo = Controller.Instance.Calendars.buttonXMonthShowLogo.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowTotalCost = Controller.Instance.Calendars.buttonXMonthShowTotalCost.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowAvgCost = Controller.Instance.Calendars.buttonXMonthShowAvgCost.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowTotalAds = Controller.Instance.Calendars.buttonXMonthShowTotalAds.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowActiveDays = Controller.Instance.Calendars.buttonXMonthShowActiveDays.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowComments = Controller.Instance.Calendars.buttonXMonthShowComment.Checked;
				LocalSchedule.ViewSettings.CalendarViewSettings.ShowDigital = Controller.Instance.Calendars.buttonXMonthShowDigital.Checked;

				if (Controller.Instance.Calendars.buttonXColorBlack.Checked)
				{
					LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor = "black";
				}
				else if (Controller.Instance.Calendars.buttonXColorBlue.Checked)
				{
					LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor = "blue";
				}
				else if (Controller.Instance.Calendars.buttonXColorGray.Checked)
				{
					LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor = "gray";
				}
				else if (Controller.Instance.Calendars.buttonXColorGreen.Checked)
				{
					LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor = "green";
				}
				else if (Controller.Instance.Calendars.buttonXColorOrange.Checked)
				{
					LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor = "orange";
				}
				else if (Controller.Instance.Calendars.buttonXColorTeal.Checked)
				{
					LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor = "teal";
				}
				_selectedMonth.RefreshData();

				SettingsNotSaved = true;
			}
		}

		public void ShowOutputOptions(ICalendarSettingsViewer settingsViewer)
		{
			if (_selectedMonth != null && settingsViewer != null)
			{
				using (var form = new FormCalendarOutputOptions(this))
				{
					foreach (MonthViewControl monthView in _monthViews)
					{
						MonthCalendarViewSettings settings = monthView.Settings.Clone();
						form.Settings.Add(settings);
					}
					form.SettingsViewer = settingsViewer;
					if (form.ShowDialog() == DialogResult.OK)
					{
						foreach (MonthCalendarViewSettings monthSettings in form.Settings)
						{
							MonthViewControl monthView = _monthViews.FirstOrDefault(x => x.Settings.Month.Equals(monthSettings.Month));
							if (monthView != null)
							{
								monthView.Settings.Comments = monthSettings.Comments;
								monthView.Settings.Logo = monthSettings.Logo;
								monthView.Settings.Title = monthSettings.Title;
								monthView.Settings.Legend.Clear();
								monthView.Settings.Legend.AddRange(monthSettings.Legend.ToArray());
								monthView.Settings.DigitalLegend = monthSettings.DigitalLegend;
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
			Controller.Instance.Calendars.buttonXDayShowSection.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableSection;
			Controller.Instance.Calendars.buttonXDayShowCost.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableCost;
			Controller.Instance.Calendars.buttonXDayShowColor.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableColor;
			Controller.Instance.Calendars.buttonXDayShowAbbreviation.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableAbbreviationOnly;
			Controller.Instance.Calendars.buttonXDayShowAdSize.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableAdSize;
			Controller.Instance.Calendars.buttonXDayShowPageSize.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnablePageSize;
			Controller.Instance.Calendars.buttonXDayShowPercentOfPage.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnablePercentOfPage & Controller.Instance.Calendars.buttonXDayShowPercentOfPage.Enabled;
			Controller.Instance.Calendars.buttonXDayShowBigDates.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableBigDate;

			Controller.Instance.Calendars.buttonXDayShowSection.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowSection;
			Controller.Instance.Calendars.buttonXDayShowCost.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowCost;
			Controller.Instance.Calendars.buttonXDayShowColor.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowColor;
			Controller.Instance.Calendars.buttonXDayShowAbbreviation.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowAbbreviationOnly;
			Controller.Instance.Calendars.buttonXDayShowAdSize.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowAdSize;
			Controller.Instance.Calendars.buttonXDayShowPageSize.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowPageSize;
			Controller.Instance.Calendars.buttonXDayShowPercentOfPage.Checked = ListManager.Instance.ShareUnits.Count > 0 && LocalSchedule.ViewSettings.CalendarViewSettings.ShowPercentOfPage & Controller.Instance.Calendars.buttonXDayShowPercentOfPage.Enabled;
			Controller.Instance.Calendars.buttonXDayShowBigDates.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowBigDate;

			Controller.Instance.Calendars.buttonXMonthShowSlideTitle.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableTitle;
			Controller.Instance.Calendars.buttonXMonthShowLogo.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableLogo;
			Controller.Instance.Calendars.buttonXMonthShowBusinessName.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableBusinessName;
			Controller.Instance.Calendars.buttonXMonthShowDecisionMaker.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableDecisionMaker;
			Controller.Instance.Calendars.buttonXMonthShowTotalCost.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableTotalCost;
			Controller.Instance.Calendars.buttonXMonthShowLegend.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableLegend;
			Controller.Instance.Calendars.buttonXMonthShowAvgCost.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableAvgCost;
			Controller.Instance.Calendars.buttonXMonthShowComment.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableComments;
			Controller.Instance.Calendars.buttonXMonthShowTotalAds.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableTotalAds;
			Controller.Instance.Calendars.buttonXMonthShowActiveDays.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableActiveDays;

			Controller.Instance.Calendars.buttonXMonthShowSlideTitle.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowTitle;
			Controller.Instance.Calendars.buttonXMonthShowLogo.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowLogo;
			Controller.Instance.Calendars.buttonXMonthShowBusinessName.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowBusinessName;
			Controller.Instance.Calendars.buttonXMonthShowDecisionMaker.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowDecisionMaker;
			Controller.Instance.Calendars.buttonXMonthShowTotalCost.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowTotalCost;
			Controller.Instance.Calendars.buttonXMonthShowLegend.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowLegend;
			Controller.Instance.Calendars.buttonXMonthShowAvgCost.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowAvgCost;
			Controller.Instance.Calendars.buttonXMonthShowComment.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowComments;
			Controller.Instance.Calendars.buttonXMonthShowTotalAds.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowTotalAds;
			Controller.Instance.Calendars.buttonXMonthShowActiveDays.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowActiveDays;
			Controller.Instance.Calendars.buttonXMonthShowDigital.Checked = LocalSchedule.ViewSettings.CalendarViewSettings.ShowDigital;

			Controller.Instance.Calendars.buttonXColorBlack.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableBlack;
			Controller.Instance.Calendars.buttonXColorBlue.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableBlue;
			Controller.Instance.Calendars.buttonXColorGray.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableGray;
			Controller.Instance.Calendars.buttonXColorGreen.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableGreen;
			Controller.Instance.Calendars.buttonXColorOrange.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableOrange;
			Controller.Instance.Calendars.buttonXColorTeal.Enabled = LocalSchedule.ViewSettings.CalendarViewSettings.EnableTeal;

			Controller.Instance.Calendars.buttonXColorBlack.Checked = false;
			Controller.Instance.Calendars.buttonXColorBlue.Checked = false;
			Controller.Instance.Calendars.buttonXColorGray.Checked = false;
			Controller.Instance.Calendars.buttonXColorGreen.Checked = false;
			Controller.Instance.Calendars.buttonXColorOrange.Checked = false;
			Controller.Instance.Calendars.buttonXColorTeal.Checked = false;
			switch (LocalSchedule.ViewSettings.CalendarViewSettings.SlideColor)
			{
				case "black":
					Controller.Instance.Calendars.buttonXColorBlack.Checked = true;
					break;
				case "blue":
					Controller.Instance.Calendars.buttonXColorBlue.Checked = true;
					break;
				case "gray":
					Controller.Instance.Calendars.buttonXColorGray.Checked = true;
					break;
				case "green":
					Controller.Instance.Calendars.buttonXColorGreen.Checked = true;
					break;
				case "orange":
					Controller.Instance.Calendars.buttonXColorOrange.Checked = true;
					break;
				case "teal":
					Controller.Instance.Calendars.buttonXColorTeal.Checked = true;
					break;
			}
			AllowToSave = true;
		}

		public void UpdateOutput(bool quickLoad)
		{
			LocalSchedule = BusinessWrapper.Instance.ScheduleManager.GetLocalSchedule();
			laAdvertiser.Text = String.Format("Advertiser: {0}{1}Campaign Dates: {2}", LocalSchedule.BusinessName + (!string.IsNullOrEmpty(LocalSchedule.AccountNumber) ? (" - " + LocalSchedule.AccountNumber) : string.Empty), Environment.NewLine, LocalSchedule.FlightDates);
			if (!quickLoad)
			{
				Inserts.Clear();
				foreach (PrintProduct publication in LocalSchedule.PrintProducts)
					Inserts.AddRange(publication.Inserts.Where(x => x.Date != DateTime.MinValue));

				ApplySettings();

				AllowToSave = false;

				PrepareMonthViews();

				Controller.Instance.CalendarMonthList.Items.Clear();
				Controller.Instance.CalendarMonthList.Items.AddRange(LocalSchedule.ScheduleMonths.Select(x => new ImageListBoxItem(x.ToString("MMM, yyyy"), 0)).ToArray());
				if (Controller.Instance.CalendarMonthList.Items.Count > 0)
					Controller.Instance.CalendarMonthList.SelectedIndex = 0;

				UpdateMonthView();
				AllowToSave = true;
			}
			else
				UpdateMonthSettings();

			SettingsNotSaved = false;
		}

		public void Export()
		{
			using (var form = new FormExportSchedule())
			{
				form.Text = LocalSchedule.Name;
				form.ScheduleName = String.Format("Ninja-{0}", DateTime.Now.ToString("MMddyy-hhmmtt"));
				if (form.ShowDialog() == DialogResult.OK)
				{
					Core.Calendar.ScheduleManager.ImportSchedule(LocalSchedule.ScheduleFile.FullName, form.ScheduleName);
				}
			}
		}

		private void ResetToDefault()
		{
			LocalSchedule.ViewSettings.CalendarViewSettings.ResetToDefault();
			ApplySettings();
			AllowToSave = false;
			UpdateMonthView();
			_selectedMonth.RefreshData();
			AllowToSave = true;
			SettingsNotSaved = false;
			Controller.Instance.SaveSchedule(LocalSchedule, false, true, this);
		}

		public void OpenHelp()
		{
			BusinessWrapper.Instance.HelpManager.OpenHelpLink("calendars");
		}
		#endregion

		private void PrepareMonthViews()
		{
			_monthViews.Clear();
			if (!LocalSchedule.FlightDateStart.HasValue || !LocalSchedule.FlightDateEnd.HasValue) return;
			var startDate = new DateTime(LocalSchedule.FlightDateStart.Value.Year, LocalSchedule.FlightDateStart.Value.Month, 1);
			var endDate = new DateTime(LocalSchedule.FlightDateEnd.Value.Year, LocalSchedule.FlightDateEnd.Value.Month, 1);
			MonthViewControl monthView = null;
			MonthCalendarViewSettings monthSettings = null;
			while (startDate < endDate)
			{
				monthView = new MonthViewControl(this);
				monthSettings = LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.FirstOrDefault(x => x.Month.Equals(startDate));
				if (monthSettings == null)
				{
					monthSettings = new MonthCalendarViewSettings(LocalSchedule.ViewSettings.CalendarViewSettings);
					monthSettings.Month = startDate;
					LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Add(monthSettings);
				}
				monthView.Settings = monthSettings;
				monthView.Init(startDate);
				_monthViews.Add(monthView);
				startDate = startDate.AddMonths(1);
			}
			monthView = new MonthViewControl(this);
			monthSettings = LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.FirstOrDefault(x => x.Month.Equals(endDate));
			if (monthSettings == null)
			{
				monthSettings = new MonthCalendarViewSettings(LocalSchedule.ViewSettings.CalendarViewSettings);
				monthSettings.Month = endDate;
				LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Add(monthSettings);
			}
			monthView.Settings = monthSettings;
			monthView.Init(endDate);
			_monthViews.Add(monthView);
		}

		private void UpdateMonthSettings()
		{
			foreach (MonthViewControl monthView in _monthViews)
			{
				MonthCalendarViewSettings monthSettings = LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.FirstOrDefault(x => x.Month.Equals(monthView.Month));
				if (monthSettings == null)
				{
					monthSettings = new MonthCalendarViewSettings(LocalSchedule.ViewSettings.CalendarViewSettings);
					monthSettings.Month = monthView.Month;
					LocalSchedule.ViewSettings.CalendarViewSettings.MonthCalendarViewSettingsList.Add(monthSettings);
				}
				monthView.Settings = monthSettings;
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

		private void hyperLinkEditReset_OpenLink(object sender, OpenLinkEventArgs e)
		{
			ResetToDefault();
			e.Handled = true;
		}

		#region Output Staff
		private void TrackOutput()
		{
			BusinessWrapper.Instance.ActivityManager.AddActivity(new OutputActivity(Controller.Instance.TabCalendar.Text, LocalSchedule.BusinessName, (decimal)LocalSchedule.PrintProducts.Sum(p => p.TotalFinalRate)));
		}

		public void PrintOutput()
		{
			if (_selectedMonth == null) return;
			var monthViews = _monthViews.Where(x => Inserts.Any(y => y.Date.HasValue && y.Date.Value.Year.Equals(x.Month.Year) && y.Date.Value.Month.Equals(x.Month.Month)));
			var selectedMonths = new List<MonthViewControl>();
			if (monthViews.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Months";
					foreach (var monthViewControl in monthViews)
					{
						var item = new CheckedListBoxItem(monthViewControl, monthViewControl.MonthText);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (monthViewControl == _selectedMonth)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedMonths.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<MonthViewControl>());
				}
			else
				selectedMonths.AddRange(monthViews);
			if (!selectedMonths.Any()) return;
			TrackOutput();
			using (var formProgress = new FormProgress())
			{
				Enabled = false;
				formProgress.TopMost = true;
				formProgress.laProgress.Text = "Creating Calendar slides…\nThis will take a few minutes…";
				formProgress.Show();
				Controller.Instance.ShowFloater(() =>
					{
						foreach (var monthView in monthViews)
							monthView.PrintOutput();
						Enabled = true;
						formProgress.Close();
					});
			}
		}

		public void Email()
		{
			var monthViews = _monthViews.Where(x => Inserts.Any(y => y.Date.HasValue && y.Date.Value.Year.Equals(x.Month.Year) && y.Date.Value.Month.Equals(x.Month.Month)));
			var selectedMonths = new List<MonthViewControl>();
			if (monthViews.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Months";
					foreach (var monthViewControl in monthViews)
					{
						var item = new CheckedListBoxItem(monthViewControl, monthViewControl.MonthText);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (monthViewControl == _selectedMonth)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedMonths.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<MonthViewControl>());
				}
			else
				selectedMonths.AddRange(monthViews);
			if (!selectedMonths.Any()) return;
			var previewGroups = new List<PreviewGroup>();
			using (var formProgress = new FormProgress())
			{
				Enabled = false;
				formProgress.TopMost = true;
				formProgress.laProgress.Text = "Creating Calendar slides…\nThis will take a few minutes…";
				formProgress.Show();
				foreach (var monthViewControl in selectedMonths)
				{
					var previewGroup = monthViewControl.GetPreviewGroup();
					AdSchedulePowerPointHelper.Instance.PrepareCalendarEmail(previewGroup.PresentationSourcePath, new[] { monthViewControl });
					previewGroups.Add(previewGroup);
				}
				Enabled = true;
				formProgress.Close();
			}
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formEmail = new FormEmail(AdSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager))
			{
				formEmail.Text = "Email this Advertising Calendar";
				formEmail.LoadGroups(previewGroups);
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				RegistryHelper.MainFormHandle = formEmail.Handle;
				RegistryHelper.MaximizeMainForm = false;
				formEmail.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
			}
		}

		public void Preview()
		{
			var monthViews = _monthViews.Where(x => Inserts.Any(y => y.Date.HasValue && y.Date.Value.Year.Equals(x.Month.Year) && y.Date.Value.Month.Equals(x.Month.Month)));
			var selectedMonths = new List<MonthViewControl>();
			if (monthViews.Count() > 1)
				using (var form = new FormSelectOutputItems())
				{
					form.Text = "Select Months";
					foreach (var monthViewControl in monthViews)
					{
						var item = new CheckedListBoxItem(monthViewControl, monthViewControl.MonthText);
						form.checkedListBoxControlOutputItems.Items.Add(item);
						if (monthViewControl == _selectedMonth)
							form.buttonXSelectCurrent.Tag = item;
					}
					form.checkedListBoxControlOutputItems.CheckAll();
					if (form.ShowDialog() == DialogResult.OK)
						selectedMonths.AddRange(form.checkedListBoxControlOutputItems.Items.
							OfType<CheckedListBoxItem>().
							Where(ci => ci.CheckState == CheckState.Checked).
							Select(ci => ci.Value).
							OfType<MonthViewControl>());
				}
			else
				selectedMonths.AddRange(monthViews);
			if (!selectedMonths.Any()) return;
			var previewGroups = new List<PreviewGroup>();
			using (var formProgress = new FormProgress())
			{
				Enabled = false;
				formProgress.laProgress.Text = "Creating Calendar slides…\nThis will take a few minutes…";
				formProgress.TopMost = true;
				formProgress.Show();
				foreach (var monthViewControl in selectedMonths)
				{
					var previewGroup = monthViewControl.GetPreviewGroup();
					AdSchedulePowerPointHelper.Instance.PrepareCalendarEmail(previewGroup.PresentationSourcePath, new[] { monthViewControl });
					previewGroups.Add(previewGroup);
				}
				Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
				Enabled = true;
				formProgress.Close();
			}
			if (!(previewGroups.Any() && previewGroups.All(pg => File.Exists(pg.PresentationSourcePath)))) return;
			using (var formPreview = new FormPreview(Controller.Instance.FormMain, AdSchedulePowerPointHelper.Instance, BusinessWrapper.Instance.HelpManager, Controller.Instance.ShowFloater, TrackOutput))
			{
				formPreview.Text = "Preview Advertising Calendar";
				formPreview.LoadGroups(previewGroups);
				RegistryHelper.MainFormHandle = formPreview.Handle;
				RegistryHelper.MaximizeMainForm = false;
				DialogResult previewResult = formPreview.ShowDialog();
				RegistryHelper.MaximizeMainForm = Controller.Instance.FormMain.WindowState == FormWindowState.Maximized;
				RegistryHelper.MainFormHandle = Controller.Instance.FormMain.Handle;
				if (previewResult != DialogResult.OK)
					Utilities.Instance.ActivateForm(Controller.Instance.FormMain.Handle, true, false);
			}
		}
		#endregion
	}
}