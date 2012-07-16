using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Pabo.Calendar;

namespace AdScheduleBuilder.ToolForms
{
    public partial class FormCloneInsert : Form
    {
        private BusinessClasses.Insert _originalInsert;
        private List<DateItem> _selectedDates = new List<DateItem>();

        public DateTime[] SelectedDates
        {
            get
            {
                return _selectedDates.Select(x => x.Date).ToArray();
            }
        }

        public FormCloneInsert(BusinessClasses.Insert originalInsert)
        {
            InitializeComponent();
            _originalInsert = originalInsert;
            labelControlFlightDates.Text = string.Format(labelControlFlightDates.Text, string.Format("{0} - {1}", new object[] { _originalInsert.Parent.Parent.FlightDateStart.ToString("M/d/yy"), _originalInsert.Parent.Parent.FlightDateEnd.ToString("M/d/yy") }));
            laOriginalDate.Text = _originalInsert.Date.ToString(@"ddd, MM/dd/yy");
            laOriginalRate.Text = _originalInsert.FinalRate.ToString(@"$#,##0.00");
            checkEditHighlightWeekdays.Text = string.Format(checkEditHighlightWeekdays.Text, _originalInsert.Date.ToString("dddd"));
            buttonXAddAllWeekdays.Text = string.Format(buttonXAddAllWeekdays.Text, _originalInsert.Date.ToString("dddd"));
            checkEditColorRate.Visible = _originalInsert.Parent.ColorOption != BusinessClasses.ColorOptions.BlackWhite;
            monthCalendarClone.ActiveMonth.Month = _originalInsert.Date.Month;
            monthCalendarClone.ActiveMonth.Year = _originalInsert.Date.Year;
            monthCalendarClone.Header.TextColor = Color.Black;

            UpdateTotals();

            if ((base.CreateGraphics()).DpiX > 96)
            {
                laOriginalDate.Font = new Font(laOriginalDate.Font.FontFamily, laOriginalDate.Font.Size - 4, laOriginalDate.Font.Style);
                laOriginalRate.Font = new Font(laOriginalRate.Font.FontFamily, laOriginalRate.Font.Size - 3, laOriginalRate.Font.Style);
                laOptionsTitle.Font = new Font(laOptionsTitle.Font.FontFamily, laOptionsTitle.Font.Size - 2, laOptionsTitle.Font.Style);
                labelControlDayTitle.Font = new Font(labelControlDayTitle.Font.FontFamily, labelControlDayTitle.Font.Size - 2, labelControlDayTitle.Font.Style);
                labelControlFlightDates.Font = new Font(labelControlFlightDates.Font.FontFamily, labelControlFlightDates.Font.Size - 2, labelControlFlightDates.Font.Style);
                labelControlClonedNumber.Font = new Font(labelControlClonedNumber.Font.FontFamily, labelControlClonedNumber.Font.Size - 2, labelControlClonedNumber.Font.Style);
                labelControlClonedRate.Font = new Font(labelControlClonedRate.Font.FontFamily, labelControlClonedRate.Font.Size - 2, labelControlClonedRate.Font.Style);
                checkEditColorRate.Font = new Font(checkEditColorRate.Font.FontFamily, checkEditColorRate.Font.Size - 2, checkEditColorRate.Font.Style);
                checkEditComment.Font = new Font(checkEditComment.Font.FontFamily, checkEditComment.Font.Size - 2, checkEditComment.Font.Style);
                checkEditDeadline.Font = new Font(checkEditDeadline.Font.FontFamily, checkEditDeadline.Font.Size - 2, checkEditDeadline.Font.Style);
                checkEditDiscount.Font = new Font(checkEditDiscount.Font.FontFamily, checkEditDiscount.Font.Size - 2, checkEditDiscount.Font.Style);
                checkEditPCIRate.Font = new Font(checkEditPCIRate.Font.FontFamily, checkEditPCIRate.Font.Size - 2, checkEditPCIRate.Font.Style);
                checkEditSections.Font = new Font(checkEditSections.Font.FontFamily, checkEditSections.Font.Size - 2, checkEditSections.Font.Style);
                checkEditMechanicals.Font = new Font(checkEditMechanicals.Font.FontFamily, checkEditMechanicals.Font.Size - 2, checkEditMechanicals.Font.Style);
                checkEditHighlightWeekdays.Font = new Font(checkEditHighlightWeekdays.Font.FontFamily, checkEditHighlightWeekdays.Font.Size - 2, checkEditHighlightWeekdays.Font.Style);
                buttonXCancel.Font = new Font(buttonXCancel.Font.FontFamily, buttonXCancel.Font.Size - 2, buttonXCancel.Font.Style);
                buttonXAddAllWeekdays.Font = new Font(buttonXAddAllWeekdays.Font.FontFamily, buttonXAddAllWeekdays.Font.Size - 2, buttonXAddAllWeekdays.Font.Style);
                buttonXClearAll.Font = new Font(buttonXClearAll.Font.FontFamily, buttonXClearAll.Font.Size - 2, buttonXClearAll.Font.Style);
                buttonXOK.Font = new Font(buttonXOK.Font.FontFamily, buttonXOK.Font.Size - 2, buttonXOK.Font.Style);
            }
        }

        private void UpdateSelectedDates()
        {
            gridControlDays.DataSource = new BindingList<DateItem>(_selectedDates.ToArray());
            monthCalendarClone.Refresh();
            UpdateTotals();
        }

        private void UpdateTotals()
        {
            labelControlClonedNumber.Text = string.Format("Cloned Ads: <b>{0}</b>", _selectedDates.Count.ToString());
            if (checkEditPCIRate.Checked)
            {
                labelControlClonedNumber.Dock = DockStyle.None;
                labelControlClonedRate.Text = string.Format("Added Dollars: <b>{0}</b>", (_originalInsert.FinalRate * _selectedDates.Count).ToString("$#,##0.00"));
                labelControlClonedRate.Visible = true;
            }
            else
            {
                labelControlClonedNumber.Dock = DockStyle.Left;
                labelControlClonedRate.Text = string.Empty;
                labelControlClonedRate.Visible = false;
            }
        }

        private void AddSelectedDate(DateTime selectedDate)
        {
            DateItem dateItem = new DateItem();
            dateItem.Date = selectedDate;
            dateItem.BackColor1 = Color.Blue;
            _selectedDates.Add(dateItem);
            UpdateSelectedDates();
        }

        private void repositoryItemButtonEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            _selectedDates.RemoveAt(gridViewDays.GetDataSourceRowIndex(gridViewDays.FocusedRowHandle));
            UpdateSelectedDates();
        }

        private void monthCalendarClone_DayQueryInfo(object sender, DayQueryInfoEventArgs e)
        {
            if (_selectedDates.Select(x => x.Date).Contains(e.Date))
            {
                e.Info.BackColor1 = Color.Blue;
                e.Info.TextColor = Color.White;
                e.Info.DateColor = Color.White;
                e.OwnerDraw = true;
            }
            else if (e.Date == _originalInsert.Date)
            {
                e.Info.BackColor1 = Color.Green;
                e.Info.TextColor = Color.White;
                e.Info.DateColor = Color.White;
                e.OwnerDraw = true;
            }
            else if (e.Date.DayOfWeek == _originalInsert.Date.DayOfWeek && checkEditHighlightWeekdays.Checked && (e.Date >= _originalInsert.Parent.Parent.FlightDateStart && e.Date <= _originalInsert.Parent.Parent.FlightDateEnd))
            {
                e.Info.BoldedDate = true;
                e.OwnerDraw = true;
            }
            else if (!_originalInsert.Parent.AvailableDays.Contains(e.Date) || !(e.Date >= _originalInsert.Parent.Parent.FlightDateStart && e.Date <= _originalInsert.Parent.Parent.FlightDateEnd))
            {
                e.Info.TextColor = Color.Gray;
                e.Info.DateColor = Color.Gray;
                e.OwnerDraw = true;
                e.OwnerDraw = true;
            }
        }

        private void buttonXClearAll_Click(object sender, EventArgs e)
        {
            _selectedDates.Clear();
            UpdateSelectedDates();
        }

        private void buttonXAddAllWeekdays_Click(object sender, EventArgs e)
        {
            DateTime startDate = _originalInsert.Parent.Parent.FlightDateStart;
            while (!startDate.DayOfWeek.Equals(_originalInsert.Date.DayOfWeek))
                startDate = startDate.AddDays(1);
            while (startDate <= _originalInsert.Parent.Parent.FlightDateEnd)
            {
                //if (!_originalInsert.Parent.Inserts.Any(x => x.Date.Equals(startDate)))
                if (startDate != _originalInsert.Date)
                    AddSelectedDate(startDate.Date);
                startDate = startDate.AddDays(7);
            }
        }

        private void checkEditHighlightWeekdays_CheckedChanged(object sender, EventArgs e)
        {
            monthCalendarClone.Refresh();
        }

        private void monthCalendarClone_DayClick(object sender, DayClickEventArgs e)
        {
            DateTime temp;
            if (DateTime.TryParse(e.Date, out temp))
            {
                if ((temp >= _originalInsert.Parent.Parent.FlightDateStart && temp <= _originalInsert.Parent.Parent.FlightDateEnd) && _originalInsert.Parent.AvailableDays.Contains(temp))
                    AddSelectedDate(temp);
                else if (temp < _originalInsert.Parent.Parent.FlightDateStart || temp > _originalInsert.Parent.Parent.FlightDateEnd)
                    AppManager.ShowWarning("Pick a date that is in your Schedule Window…");
                else if (!_originalInsert.Parent.AvailableDays.Contains(temp))
                    AppManager.ShowWarning("This day is unavailable. Try another day...");
            }
        }

        private void checkEditPCIRate_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotals();
        }

        private void pbHelp_Click(object sender, EventArgs e)
        {
            BusinessClasses.HelpManager.Instance.OpenHelpLink("clone");
        }

        #region Picture Box Clicks Habdlers
        /// <summary>
        /// Buttonize the PictureBox 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)(sender);
            pic.Top += 1;
        }

        private void pictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            PictureBox pic = (PictureBox)(sender);
            pic.Top -= 1;
        }
        #endregion
    }
}
