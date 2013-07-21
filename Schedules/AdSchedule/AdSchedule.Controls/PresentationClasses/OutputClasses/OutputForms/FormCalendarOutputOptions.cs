using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls;
using NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls.Calendar.SettingsViewers;
using NewBizWiz.Core.AdSchedule;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputForms
{
	public partial class FormCalendarOutputOptions : Form
	{
		private MonthCalendarViewSettings _selectedMonthSettings;
		private OutputCalendarControl _calendarControl;

		public FormCalendarOutputOptions(OutputCalendarControl calendarControl)
		{
			InitializeComponent();
			_calendarControl = calendarControl;
			Settings = new List<MonthCalendarViewSettings>();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 3, laTitle.Font.Style);
				checkEditApplyForAll.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 2, laTitle.Font.Style);
				buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
				buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
			}
		}

		public List<MonthCalendarViewSettings> Settings { get; private set; }
		public ICalendarSettingsViewer SettingsViewer { get; set; }

		private void SaveSelectedMonthSettings()
		{
			if (SettingsViewer != null)
			{
				SettingsViewer.SaveSettings();
				if (checkEditApplyForAll.Checked)
					SettingsViewer.ApplySettingsForAll(Settings.ToArray());
			}
		}

		private void FormCalendarNotes_Load(object sender, EventArgs e)
		{
			listBoxControlMonth.Items.Clear();
			listBoxControlMonth.Items.AddRange(Settings.Select(x => x.Month.ToString("MMMM, yyyy")).ToArray());
			if (Settings.Count > 0)
				listBoxControlMonth.SelectedIndex = 0;
		}

		private void FormCalendarNotes_FormClosed(object sender, FormClosedEventArgs e)
		{
			SaveSelectedMonthSettings();
		}

		private void listBoxControlMonth_SelectedValueChanged(object sender, EventArgs e)
		{
			SaveSelectedMonthSettings();
			if (listBoxControlMonth.SelectedIndex >= 0 && SettingsViewer != null)
			{
				_selectedMonthSettings = Settings[listBoxControlMonth.SelectedIndex];
				SettingsViewer.LoadSettings(_calendarControl, _selectedMonthSettings);
				if (!pnSettingsViewer.Controls.Contains(SettingsViewer as Control))
					pnSettingsViewer.Controls.Add(SettingsViewer as Control);
				(SettingsViewer as Control).BringToFront();
				laTitle.Text = SettingsViewer.Title;
				pnApplyForAll.Visible = SettingsViewer.ShowApplyForAll;
				checkEditApplyForAll.Text = SettingsViewer.ApplyForAllText;
			}
		}
	}
}