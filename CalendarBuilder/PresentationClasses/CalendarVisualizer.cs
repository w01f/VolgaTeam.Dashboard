using System;
using System.Windows.Forms;

namespace CalendarBuilder.PresentationClasses
{
    public class CalendarVisualizer
    {
        private static CalendarVisualizer _instance = null;
        private Calendars.AdvancedCalendarControl _advancedCalendar = new Calendars.AdvancedCalendarControl();
        private Calendars.GraphicCalendarControl _graphicCalendar = new Calendars.GraphicCalendarControl();
        private Calendars.SimpleCalendarControl _simpleCalendar = new Calendars.SimpleCalendarControl();

        #region Operation Buttons
        public DevExpress.XtraEditors.ImageListBoxControl MonthsListBoxControl { get; set; }
        public DevComponents.DotNetBar.ButtonItem MonthViewButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem GridViewButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem SlideInfoButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem CopyButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem PasteButtonItem { get; set; }
        public DevComponents.DotNetBar.ButtonItem CloneButtonItem { get; set; }
        #endregion

        public Calendars.ICalendarControl SelectedCalendarControl { get; private set; }

        public static CalendarVisualizer Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CalendarVisualizer();
                return _instance;
            }
        }

        private CalendarVisualizer()
        {
        }

        public static void RemoveInstance()
        {
            try
            {
                _instance._advancedCalendar.Dispose();
                _instance._graphicCalendar.Dispose();
                _instance._simpleCalendar.Dispose();
            }
            catch
            {
            }
            finally
            {
                _instance = null;
            }
        }

        public static void AssignCloseActiveEditorsonOutSideClick(Control control)
        {
            if (control.GetType() != typeof(DevExpress.XtraEditors.TextEdit) && control.GetType() != typeof(DevExpress.XtraEditors.MemoEdit) && control.GetType() != typeof(DevExpress.XtraEditors.ComboBoxEdit) && control.GetType() != typeof(DevExpress.XtraEditors.LookUpEdit) && control.GetType() != typeof(DevExpress.XtraEditors.DateEdit) && control.GetType() != typeof(DevExpress.XtraEditors.CheckedListBoxControl) && control.GetType() != typeof(DevExpress.XtraEditors.SpinEdit) && control.GetType() != typeof(DevExpress.XtraEditors.CheckEdit) && control.GetType() != typeof(DevExpress.XtraEditors.ImageListBoxControl))
            {
                control.Click += new EventHandler(CloseActiveEditorsonOutSideClick);
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

        public Calendars.ICalendarControl SelectCalendar(Control container, BusinessClasses.CalendarStyle calendarStyle)
        {
            if (this.SelectedCalendarControl != null)
                this.SelectedCalendarControl.LeaveCalendar();
            switch (calendarStyle)
            {
                case BusinessClasses.CalendarStyle.Advanced:
                    this.SelectedCalendarControl = _advancedCalendar;
                    this.MonthsListBoxControl = FormMain.Instance.listBoxControlAdvancedCalendar;
                    this.MonthViewButtonItem = FormMain.Instance.buttonItemAdvancedCalendarMonth;
                    this.GridViewButtonItem = FormMain.Instance.buttonItemAdvancedCalendarGrid;
                    this.SlideInfoButtonItem = FormMain.Instance.buttonItemAdvancedCalendarSlideInfo;
                    this.CopyButtonItem = FormMain.Instance.buttonItemAdvancedCalendarCopy;
                    this.PasteButtonItem = FormMain.Instance.buttonItemAdvancedCalendarPaste;
                    this.CloneButtonItem = FormMain.Instance.buttonItemAdvancedCalendarClone;
                    break;
                case BusinessClasses.CalendarStyle.Graphic:
                    this.SelectedCalendarControl = _graphicCalendar;
                    this.MonthsListBoxControl = FormMain.Instance.listBoxControlGraphicCalendar;
                    this.MonthViewButtonItem = FormMain.Instance.buttonItemGraphicCalendarMonth;
                    this.GridViewButtonItem = FormMain.Instance.buttonItemGraphicCalendarGrid;
                    this.SlideInfoButtonItem = FormMain.Instance.buttonItemGraphicCalendarSlideInfo;
                    this.CopyButtonItem = FormMain.Instance.buttonItemGraphicCalendarCopy;
                    this.PasteButtonItem = FormMain.Instance.buttonItemGraphicCalendarPaste;
                    this.CloneButtonItem = FormMain.Instance.buttonItemGraphicCalendarClone;
                    break;
                case BusinessClasses.CalendarStyle.Simple:
                    this.SelectedCalendarControl = _simpleCalendar;
                    this.MonthsListBoxControl = FormMain.Instance.listBoxControlSimpleCalendar;
                    this.MonthViewButtonItem = FormMain.Instance.buttonItemSimpleCalendarMonth;
                    this.GridViewButtonItem = FormMain.Instance.buttonItemSimpleCalendarGrid;
                    this.SlideInfoButtonItem = FormMain.Instance.buttonItemSimpleCalendarSlideInfo;
                    this.CopyButtonItem = FormMain.Instance.buttonItemSimpleCalendarCopy;
                    this.PasteButtonItem = FormMain.Instance.buttonItemSimpleCalendarPaste;
                    this.CloneButtonItem = FormMain.Instance.buttonItemSimpleCalendarClone;
                    break;
                default:
                    this.SelectedCalendarControl = _advancedCalendar;
                    break;
            }
            this.SelectedCalendarControl.ShowCalendar();
            this.SelectedCalendarControl.Splash(true);
            if (!container.Controls.Contains(this.SelectedCalendarControl as Control))
                container.Controls.Add(this.SelectedCalendarControl as Control);
            this.SelectedCalendarControl.Splash(false);
            return this.SelectedCalendarControl;
        }

        #region View Event Handlers
        public void buttonItemCalendarView_Click(object sender, EventArgs e)
        {
            CalendarVisualizer.Instance.GridViewButtonItem.Checked = false;
            CalendarVisualizer.Instance.MonthViewButtonItem.Checked = false;
            (sender as DevComponents.DotNetBar.ButtonItem).Checked = true;
        }

        public void buttonItemCalendarView_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SelectedCalendarControl.AllowToSave)
                this.SelectedCalendarControl.SaveView();
        }
        #endregion

        #region Copy-Paste Methods and Event Handlers
        public void buttonItemCalendarCopy_Click(object sender, EventArgs e)
        {
            this.SelectedCalendarControl.SelectedView.CopyDay();
        }

        public void buttonItemCalendarPaste_Click(object sender, EventArgs e)
        {
            this.SelectedCalendarControl.SelectedView.PasteDay();
        }

        public void buttonItemCalendarClone_Click(object sender, EventArgs e)
        {
            this.SelectedCalendarControl.SelectedView.CloneDay();
        }
        #endregion

        #region Ribbon Operations Events
        public void imageListBoxEditCalendar_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex >= 0 && this.SelectedCalendarControl.AllowToSave)
            {
                this.SelectedCalendarControl.DayProperties.Close();
                this.SelectedCalendarControl.SlideInfo.LoadData(month: this.SelectedCalendarControl.CalendarData.Months[CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex]);
                this.SelectedCalendarControl.Splash(true);
                this.SelectedCalendarControl.SelectedView.ChangeMonth(this.SelectedCalendarControl.CalendarData.Months[CalendarVisualizer.Instance.MonthsListBoxControl.SelectedIndex].Date);
                this.SelectedCalendarControl.Splash(false);
            }
        }

        public void buttonItemCalendarSlideInfo_CheckedChanged(object sender, EventArgs e)
        {
            if (this.SelectedCalendarControl.AllowToSave)
            {
                if (CalendarVisualizer.Instance.SlideInfoButtonItem.Checked)
                {
                    this.SelectedCalendarControl.Splash(true);
                    this.SelectedCalendarControl.SlideInfo.Show();
                    this.SelectedCalendarControl.Splash(false);
                }
                else
                {
                    this.SelectedCalendarControl.Splash(true);
                    this.SelectedCalendarControl.SlideInfo.Close();
                    this.SelectedCalendarControl.Splash(false);
                }
            }
        }

        public void buttonItemCalendarSave_Click(object sender, EventArgs e)
        {
            if (this.SelectedCalendarControl.SaveCalendarData())
                AppManager.ShowInformation("Calendar Saved");
        }

        public void buttonItemCalendarSaveAs_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.InitialDirectory = ConfigurationClasses.SettingsManager.Instance.SaveFolder;
                dialog.Title = "Save Calendar As...";
                dialog.Filter = "Calendar Files|*.xml";
                dialog.FileName = this.SelectedCalendarControl.CalendarData.Schedule.Name + ".xml";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    if (this.SelectedCalendarControl.SaveCalendarData(dialog.FileName.Replace(".xml", "")))
                        AppManager.ShowInformation("Calendar was saved");
                }
            }
        }

        public void buttonItemCalendarPreview_Click(object sender, EventArgs e)
        {
            this.SelectedCalendarControl.Preview();
        }

        public void buttonItemCalendarPowerPoint_Click(object sender, EventArgs e)
        {
            this.SelectedCalendarControl.Print();
        }

        public void buttonItemCalendarEmail_Click(object sender, EventArgs e)
        {
            this.SelectedCalendarControl.Email();
        }

        public void buttonItemCalendarHelp_Click(object sender, EventArgs e)
        {
            this.SelectedCalendarControl.OpenHelp();
        }
        #endregion
    }
}
