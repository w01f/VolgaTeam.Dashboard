using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.Calendar.Controls.BusinessClasses;
using NewBizWiz.Calendar.Controls.PresentationClasses;
using NewBizWiz.Calendar.Controls.ToolForms;
using NewBizWiz.CommonGUI.Floater;
using NewBizWiz.CommonGUI.ToolForms;
using NewBizWiz.Core.Calendar;

namespace NewBizWiz.Calendar.Controls
{
	public class Controller
	{
		private static readonly Controller _instance = new Controller();
		private Controller() { }
		public static Controller Instance
		{
			get { return _instance; }
		}

		public event EventHandler<EventArgs> ScheduleChanged;
		public event EventHandler<FloaterRequestedEventArgs> FloaterRequested;

		public Form FormMain { get; set; }
		public SuperTooltip Supertip { get; set; }
		public RibbonControl Ribbon { get; set; }
		public RibbonTabItem TabHome { get; set; }
		public RibbonTabItem TabCalendar { get; set; }
		public RibbonTabItem TabGrid { get; set; }

		public void Init()
		{
			HomeControl = new HomeControl();
			HomeHelp.Click += HomeControl.HomeHelp_Click;
			HomeSave.Click += HomeControl.HomeSave_Click;
			HomeSaveAs.Click += HomeControl.HomeSaveAs_Click;
			HomeBusinessName.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeDecisionMaker.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeClientType.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeAccountNumberText.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeAccountNumberCheck.CheckedChanged += HomeControl.checkBoxItemAccountNumber_CheckedChanged;
			HomePresentationDate.EditValueChanged += HomeControl.SchedulePropertyEditValueChanged;
			HomeFlightDatesStart.EditValueChanged += HomeControl.FlightDateStartEditValueChanged;
			HomeFlightDatesEnd.EditValueChanged += HomeControl.FlightDateEndEditValueChanged;
			HomeFlightDatesStart.EditValueChanged += HomeControl.CalcWeeksOnFlightDatesChange;
			HomeFlightDatesEnd.EditValueChanged += HomeControl.CalcWeeksOnFlightDatesChange;
			HomeFlightDatesStart.CloseUp += HomeControl.dateEditFlightDatesStart_CloseUp;
			HomeFlightDatesEnd.CloseUp += HomeControl.dateEditFlightDatesEnd_CloseUp;

			HomeBusinessName.TabIndex = 0;
			HomeBusinessName.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			HomeDecisionMaker.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			HomeClientType.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			HomePresentationDate.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			HomeFlightDatesStart.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;
			HomeFlightDatesEnd.KeyDown += HomeControl.SchedulePropertiesEditor_KeyDown;

			CalendarVisualizer = new CalendarVisualizer();
			CalendarMonthsList.SelectedIndexChanged += CalendarVisualizer.imageListBoxEditCalendar_SelectedIndexChanged;
			CalendarSlideInfo.CheckedChanged += CalendarVisualizer.buttonItemCalendarSlideInfo_CheckedChanged;
			CalendarCopy.Click += CalendarVisualizer.buttonItemCalendarCopy_Click;
			CalendarPaste.Click += CalendarVisualizer.buttonItemCalendarPaste_Click;
			CalendarClone.Click += CalendarVisualizer.buttonItemCalendarClone_Click;
			CalendarSave.Click += CalendarVisualizer.buttonItemCalendarSave_Click;
			CalendarSaveAs.Click += CalendarVisualizer.buttonItemCalendarSaveAs_Click;
			CalendarPreview.Click += CalendarVisualizer.buttonItemCalendarPreview_Click;
			CalendarPowerPoint.Click += CalendarVisualizer.buttonItemCalendarPowerPoint_Click;
			CalendarEmail.Click += CalendarVisualizer.buttonItemCalendarEmail_Click;
			CalendarHelp.Click += CalendarVisualizer.buttonItemCalendarHelp_Click;

			GridMonthsList.SelectedIndexChanged += CalendarVisualizer.imageListBoxEditCalendar_SelectedIndexChanged;
			GridSlideInfo.CheckedChanged += CalendarVisualizer.buttonItemCalendarSlideInfo_CheckedChanged;
			GridCopy.Click += CalendarVisualizer.buttonItemCalendarCopy_Click;
			GridPaste.Click += CalendarVisualizer.buttonItemCalendarPaste_Click;
			GridClone.Click += CalendarVisualizer.buttonItemCalendarClone_Click;
			GridSave.Click += CalendarVisualizer.buttonItemCalendarSave_Click;
			GridSaveAs.Click += CalendarVisualizer.buttonItemCalendarSaveAs_Click;
			GridPreview.Click += CalendarVisualizer.buttonItemCalendarPreview_Click;
			GridPowerPoint.Click += CalendarVisualizer.buttonItemCalendarPowerPoint_Click;
			GridEmail.Click += CalendarVisualizer.buttonItemCalendarEmail_Click;
			GridHelp.Click += CalendarVisualizer.buttonItemCalendarHelp_Click;

			ConfigureTabPages();
		}

		public void RemoveInstance()
		{
			HomeControl.Dispose();
			CalendarVisualizer.RemoveInstance();
			FloaterRequested = null;
		}

		public void LoadData()
		{
			HomeControl.LoadCalendar(false);
			CalendarVisualizer.LoadData();
		}

		public void UpdateScheduleTabs(bool enable)
		{
			TabCalendar.Enabled = enable;
			TabGrid.Enabled = enable;
		}

		private void ConfigureTabPages()
		{
			Ribbon.Items.Clear();
			var tabPages = new List<BaseItem>();
			foreach (var tabPageConfig in BusinessWrapper.Instance.TabPageManager.TabPageSettings)
			{
				switch (tabPageConfig.Id)
				{
					case "Home":
						TabHome.Text = tabPageConfig.Name;
						tabPages.Add(TabHome);
						break;
					case "Ninja Calendar":
						TabCalendar.Text = tabPageConfig.Name;
						tabPages.Add(TabCalendar);
						break;
					case "List View":
						TabGrid.Text = tabPageConfig.Name;
						tabPages.Add(TabGrid);
						break;
				}
			}
			Ribbon.Items.AddRange(tabPages.ToArray());
		}

		public void SaveSchedule(Schedule localSchedule, bool quickSave, Control sender)
		{
			using (var form = new FormProgress())
			{
				form.laProgress.Text = "Chill-Out for a few seconds...\nSaving settings...";
				form.TopMost = true;
				var thread = new Thread(delegate() { BusinessWrapper.Instance.ScheduleManager.SaveSchedule(localSchedule, quickSave, sender); });
				form.Show();
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				form.Close();
			}
			if (ScheduleChanged != null)
				ScheduleChanged(this, EventArgs.Empty);
		}

		public void ShowFloater(Action afterShow)
		{
			var args = new FloaterRequestedEventArgs { AfterShow = afterShow };
			if (FloaterRequested != null)
				FloaterRequested(null, args);
		}

		#region Command Controls

		#region Home
		public RibbonPanel HomePanel { get; set; }
		public ButtonItem HomeHelp { get; set; }
		public ButtonItem HomeSave { get; set; }
		public ButtonItem HomeSaveAs { get; set; }
		public TextEdit HomeAccountNumberText { get; set; }
		public CheckBoxItem HomeAccountNumberCheck { get; set; }
		public ComboBoxEdit HomeBusinessName { get; set; }
		public ComboBoxEdit HomeDecisionMaker { get; set; }
		public ComboBoxEdit HomeClientType { get; set; }
		public DateEdit HomePresentationDate { get; set; }
		public DateEdit HomeFlightDatesStart { get; set; }
		public DateEdit HomeFlightDatesEnd { get; set; }
		public LabelItem HomeWeeks { get; set; }
		#endregion

		#region Calendar
		public ImageListBoxControl CalendarMonthsList { get; set; }
		public ButtonItem CalendarSlideInfo { get; set; }
		public ButtonItem CalendarCopy { get; set; }
		public ButtonItem CalendarPaste { get; set; }
		public ButtonItem CalendarClone { get; set; }
		public ButtonItem CalendarHelp { get; set; }
		public ButtonItem CalendarSave { get; set; }
		public ButtonItem CalendarSaveAs { get; set; }
		public ButtonItem CalendarPreview { get; set; }
		public ButtonItem CalendarEmail { get; set; }
		public ButtonItem CalendarPowerPoint { get; set; }
		#endregion

		#region Calendar
		public ImageListBoxControl GridMonthsList { get; set; }
		public ButtonItem GridSlideInfo { get; set; }
		public ButtonItem GridCopy { get; set; }
		public ButtonItem GridPaste { get; set; }
		public ButtonItem GridClone { get; set; }
		public ButtonItem GridHelp { get; set; }
		public ButtonItem GridSave { get; set; }
		public ButtonItem GridSaveAs { get; set; }
		public ButtonItem GridPreview { get; set; }
		public ButtonItem GridEmail { get; set; }
		public ButtonItem GridPowerPoint { get; set; }
		#endregion
		#endregion

		#region Forms
		public HomeControl HomeControl { get; set; }
		public CalendarVisualizer CalendarVisualizer { get; set; }
		#endregion
	}
}