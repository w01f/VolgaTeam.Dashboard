using System;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevExpress.XtraEditors;
using NewBizWiz.Calendar.Controls.PresentationClasses.Calendars;
using NewBizWiz.Calendar.Controls.ToolForms;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Calendar.Controls.PresentationClasses
{
	public class CalendarVisualizer
	{
		private readonly CalendarControl _graphicCalendar = new CalendarControl();

		#region Operation Buttons
		public ImageListBoxControl MonthsListBoxControl { get; set; }
		public ButtonItem MonthViewButtonItem { get; set; }
		public ButtonItem GridViewButtonItem { get; set; }
		public ButtonItem SlideInfoButtonItem { get; set; }
		public ButtonItem CopyButtonItem { get; set; }
		public ButtonItem PasteButtonItem { get; set; }
		public ButtonItem CloneButtonItem { get; set; }
		public ButtonItem PreviewButtonItem { get; set; }
		public ButtonItem EmailButtonItem { get; set; }
		public ButtonItem PowerPointButtonItem { get; set; }

		#endregion

		public ICalendarControl SelectedCalendarControl { get; private set; }

		public void RemoveInstance()
		{
			try
			{
				_graphicCalendar.Dispose();
			}
			catch { }
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
			Controller.Instance.Ribbon.Focus();
		}

		public void LoadData()
		{
			_graphicCalendar.LoadCalendar(false);
		}

		public ICalendarControl SelectCalendar(Control container, bool gridView)
		{
			if (SelectedCalendarControl != null)
				SelectedCalendarControl.LeaveCalendar();
			SelectedCalendarControl = _graphicCalendar;
			if (gridView)
			{
				MonthsListBoxControl = Controller.Instance.GridMonthsList;
				SlideInfoButtonItem = Controller.Instance.GridSlideInfo;
				CopyButtonItem = Controller.Instance.GridCopy;
				PasteButtonItem = Controller.Instance.GridPaste;
				CloneButtonItem = Controller.Instance.GridClone;
				PreviewButtonItem = Controller.Instance.GridPreview;
				EmailButtonItem = Controller.Instance.GridEmail;
				PowerPointButtonItem = Controller.Instance.GridPowerPoint;
			}
			else
			{
				MonthsListBoxControl = Controller.Instance.CalendarMonthsList;
				SlideInfoButtonItem = Controller.Instance.CalendarSlideInfo;
				CopyButtonItem = Controller.Instance.CalendarCopy;
				PasteButtonItem = Controller.Instance.CalendarPaste;
				CloneButtonItem = Controller.Instance.CalendarClone;
				PreviewButtonItem = Controller.Instance.CalendarPreview;
				EmailButtonItem = Controller.Instance.CalendarEmail;
				PowerPointButtonItem = Controller.Instance.CalendarPowerPoint;
			}
			SelectedCalendarControl.ShowCalendar(gridView);
			SelectedCalendarControl.Splash(true);
			if (!container.Controls.Contains(SelectedCalendarControl as Control))
				container.Controls.Add(SelectedCalendarControl as Control);
			SelectedCalendarControl.Splash(false);
			return SelectedCalendarControl;
		}

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
			if (MonthsListBoxControl.SelectedIndex >= 0 && SelectedCalendarControl.AllowToSave)
			{
				SelectedCalendarControl.SlideInfo.LoadData(SelectedCalendarControl.CalendarData.Months[MonthsListBoxControl.SelectedIndex]);
				SelectedCalendarControl.Splash(true);
				SelectedCalendarControl.SelectedView.ChangeMonth(SelectedCalendarControl.CalendarData.Months[MonthsListBoxControl.SelectedIndex].Date);
				SelectedCalendarControl.Splash(false);
				SelectedCalendarControl.CalendarSettings.SelectedMonth = SelectedCalendarControl.CalendarData.Months[MonthsListBoxControl.SelectedIndex].Date;
			}
		}

		public void buttonItemCalendarSlideInfo_CheckedChanged(object sender, EventArgs e)
		{
			if (SelectedCalendarControl.AllowToSave)
			{
				if (SlideInfoButtonItem.Checked)
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
				Utilities.Instance.ShowInformation("Calendar Saved");
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
							Utilities.Instance.ShowInformation("Calendar was saved");
					}
					else
					{
						Utilities.Instance.ShowWarning("Calendar Name can't be empty");
					}
				}
			}
		}

		public void buttonItemCalendarPreview_Click(object sender, EventArgs e)
		{
			SelectedCalendarControl.SaveCalendarData();
			SelectedCalendarControl.Preview();
		}

		public void buttonItemCalendarPowerPoint_Click(object sender, EventArgs e)
		{
			SelectedCalendarControl.SaveCalendarData();
			SelectedCalendarControl.Print();
		}

		public void buttonItemCalendarEmail_Click(object sender, EventArgs e)
		{
			SelectedCalendarControl.SaveCalendarData();
			SelectedCalendarControl.Email();
		}

		public void buttonItemCalendarHelp_Click(object sender, EventArgs e)
		{
			SelectedCalendarControl.OpenHelp();
		}
		#endregion
	}
}