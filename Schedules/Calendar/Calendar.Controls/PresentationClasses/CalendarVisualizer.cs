using System;
using System.Windows.Forms;
using CalendarBuilder.BusinessClasses;
using CalendarBuilder.PresentationClasses.Calendars;
using CalendarBuilder.ToolForms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;

namespace CalendarBuilder.PresentationClasses
{
	public class CalendarVisualizer
	{
		private static CalendarVisualizer _instance;
		private readonly AdvancedCalendarControl _advancedCalendar = new AdvancedCalendarControl();
		private readonly GraphicCalendarControl _graphicCalendar = new GraphicCalendarControl();
		private readonly SimpleCalendarControl _simpleCalendar = new SimpleCalendarControl();

		#region Operation Buttons
		public ImageListBoxControl MonthsListBoxControl { get; set; }
		public ButtonItem MonthViewButtonItem { get; set; }
		public ButtonItem GridViewButtonItem { get; set; }
		public ButtonItem SlideInfoButtonItem { get; set; }
		public ButtonItem CopyButtonItem { get; set; }
		public ButtonItem PasteButtonItem { get; set; }
		public ButtonItem CloneButtonItem { get; set; }
		#endregion

		private CalendarVisualizer() { }

		public ICalendarControl SelectedCalendarControl { get; private set; }

		public static CalendarVisualizer Instance
		{
			get
			{
				if (_instance == null)
					_instance = new CalendarVisualizer();
				return _instance;
			}
		}

		public static void RemoveInstance()
		{
			try
			{
				_instance._advancedCalendar.Dispose();
				_instance._graphicCalendar.Dispose();
				_instance._simpleCalendar.Dispose();
			}
			catch { }
			finally
			{
				_instance = null;
			}
		}

		public static void AssignCloseActiveEditorsonOutSideClick(Control control)
		{
			if (control.GetType() != typeof(TextEdit) && control.GetType() != typeof(MemoEdit) && control.GetType() != typeof(ComboBoxEdit) && control.GetType() != typeof(LookUpEdit) && control.GetType() != typeof(DateEdit) && control.GetType() != typeof(CheckedListBoxControl) && control.GetType() != typeof(SpinEdit) && control.GetType() != typeof(CheckEdit) && control.GetType() != typeof(ImageListBoxControl))
			{
				control.Click += CloseActiveEditorsonOutSideClick;
				foreach (Control childControl in control.Controls)
					AssignCloseActiveEditorsonOutSideClick(childControl);
			}
		}

		private static void CloseActiveEditorsonOutSideClick(object sender, EventArgs e)
		{
			FormMain.Instance.ribbonControl.Focus();
		}

		public void LoadData()
		{
			_advancedCalendar.LoadCalendar(false);
			_graphicCalendar.LoadCalendar(false);
			_simpleCalendar.LoadCalendar(false);
		}

		public ICalendarControl SelectCalendar(Control container, CalendarStyle calendarStyle)
		{
			if (SelectedCalendarControl != null)
				SelectedCalendarControl.LeaveCalendar();
			switch (calendarStyle)
			{
				case CalendarStyle.Advanced:
					SelectedCalendarControl = _advancedCalendar;
					MonthsListBoxControl = FormMain.Instance.listBoxControlAdvancedCalendar;
					MonthViewButtonItem = FormMain.Instance.buttonItemAdvancedCalendarMonth;
					GridViewButtonItem = FormMain.Instance.buttonItemAdvancedCalendarGrid;
					SlideInfoButtonItem = FormMain.Instance.buttonItemAdvancedCalendarSlideInfo;
					CopyButtonItem = FormMain.Instance.buttonItemAdvancedCalendarCopy;
					PasteButtonItem = FormMain.Instance.buttonItemAdvancedCalendarPaste;
					CloneButtonItem = FormMain.Instance.buttonItemAdvancedCalendarClone;
					break;
				case CalendarStyle.Graphic:
					SelectedCalendarControl = _graphicCalendar;
					MonthsListBoxControl = FormMain.Instance.listBoxControlGraphicCalendar;
					MonthViewButtonItem = FormMain.Instance.buttonItemGraphicCalendarMonth;
					GridViewButtonItem = FormMain.Instance.buttonItemGraphicCalendarGrid;
					SlideInfoButtonItem = FormMain.Instance.buttonItemGraphicCalendarSlideInfo;
					CopyButtonItem = FormMain.Instance.buttonItemGraphicCalendarCopy;
					PasteButtonItem = FormMain.Instance.buttonItemGraphicCalendarPaste;
					CloneButtonItem = FormMain.Instance.buttonItemGraphicCalendarClone;
					break;
				case CalendarStyle.Simple:
					SelectedCalendarControl = _simpleCalendar;
					MonthsListBoxControl = FormMain.Instance.listBoxControlSimpleCalendar;
					MonthViewButtonItem = FormMain.Instance.buttonItemSimpleCalendarMonth;
					GridViewButtonItem = FormMain.Instance.buttonItemSimpleCalendarGrid;
					SlideInfoButtonItem = FormMain.Instance.buttonItemSimpleCalendarSlideInfo;
					CopyButtonItem = FormMain.Instance.buttonItemSimpleCalendarCopy;
					PasteButtonItem = FormMain.Instance.buttonItemSimpleCalendarPaste;
					CloneButtonItem = FormMain.Instance.buttonItemSimpleCalendarClone;
					break;
				default:
					SelectedCalendarControl = _advancedCalendar;
					break;
			}
			SelectedCalendarControl.ShowCalendar();
			SelectedCalendarControl.Splash(true);
			if (!container.Controls.Contains(SelectedCalendarControl as Control))
				container.Controls.Add(SelectedCalendarControl as Control);
			SelectedCalendarControl.Splash(false);
			return SelectedCalendarControl;
		}

		#region View Event Handlers
		public void buttonItemCalendarView_Click(object sender, EventArgs e)
		{
			Instance.GridViewButtonItem.Checked = false;
			Instance.MonthViewButtonItem.Checked = false;
			(sender as ButtonItem).Checked = true;
		}

		public void buttonItemCalendarView_CheckedChanged(object sender, EventArgs e)
		{
			if (SelectedCalendarControl.AllowToSave)
				SelectedCalendarControl.SaveView();
		}
		#endregion

		#region Copy-Paste Methods and Event Handlers
		public void buttonItemCalendarCopy_Click(object sender, EventArgs e)
		{
			SelectedCalendarControl.SelectedView.CopyDay();
		}

		public void buttonItemCalendarPaste_Click(object sender, EventArgs e)
		{
			SelectedCalendarControl.SelectedView.PasteDay();
		}

		public void buttonItemCalendarClone_Click(object sender, EventArgs e)
		{
			SelectedCalendarControl.SelectedView.CloneDay();
		}
		#endregion

		#region Ribbon Operations Events
		public void imageListBoxEditCalendar_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (Instance.MonthsListBoxControl.SelectedIndex >= 0 && SelectedCalendarControl.AllowToSave)
			{
				SelectedCalendarControl.DayProperties.Close();
				SelectedCalendarControl.SlideInfo.LoadData(month: SelectedCalendarControl.CalendarData.Months[Instance.MonthsListBoxControl.SelectedIndex]);
				SelectedCalendarControl.Splash(true);
				SelectedCalendarControl.SelectedView.ChangeMonth(SelectedCalendarControl.CalendarData.Months[Instance.MonthsListBoxControl.SelectedIndex].Date);
				SelectedCalendarControl.Splash(false);
			}
		}

		public void buttonItemCalendarSlideInfo_CheckedChanged(object sender, EventArgs e)
		{
			if (SelectedCalendarControl.AllowToSave)
			{
				if (Instance.SlideInfoButtonItem.Checked)
				{
					SelectedCalendarControl.Splash(true);
					SelectedCalendarControl.SlideInfo.Show();
					SelectedCalendarControl.Splash(false);
				}
				else
				{
					SelectedCalendarControl.Splash(true);
					SelectedCalendarControl.SlideInfo.Close();
					SelectedCalendarControl.Splash(false);
				}
			}
		}

		public void buttonItemCalendarSave_Click(object sender, EventArgs e)
		{
			if (SelectedCalendarControl.SaveCalendarData())
				AppManager.ShowInformation("Calendar Saved");
		}

		public void buttonItemCalendarSaveAs_Click(object sender, EventArgs e)
		{
			using (var from = new FormNewCalendar())
			{
				from.Text = "Save Calendar";
				from.laLogo.Text = "Please set a new name for your Calendar:";
				if (from.ShowDialog() == DialogResult.OK)
				{
					if (!string.IsNullOrEmpty(from.ScheduleName))
					{
						if (SelectedCalendarControl.SaveCalendarData(from.ScheduleName))
							AppManager.ShowInformation("Calendar was saved");
					}
					else
					{
						AppManager.ShowWarning("Calendar Name can't be empty");
					}
				}
			}
		}

		public void buttonItemCalendarPreview_Click(object sender, EventArgs e)
		{
			SelectedCalendarControl.Preview();
		}

		public void buttonItemCalendarPowerPoint_Click(object sender, EventArgs e)
		{
			SelectedCalendarControl.Print();
		}

		public void buttonItemCalendarEmail_Click(object sender, EventArgs e)
		{
			SelectedCalendarControl.Email();
		}

		public void buttonItemCalendarHelp_Click(object sender, EventArgs e)
		{
			SelectedCalendarControl.OpenHelp();
		}
		#endregion
	}
}