using System;

namespace CalendarBuilder.BusinessClasses
{
    public class DayCopyPaster
    {
        private CalendarDay _source = null;

        public event EventHandler<EventArgs> DayCopied;
        public event EventHandler<EventArgs> DayPasted;
        public event EventHandler<EventArgs> AfterInitialize;

        public void Init()
        {
            _source = null;
            if (this.AfterInitialize != null)
                this.AfterInitialize(null, null);
        }

        public void Copy(CalendarDay source)
        {
            _source = source;
            if (_source != null)
            {
                if (this.DayCopied != null)
                    this.DayCopied(null, null);
            }
        }

        public void Paste(CalendarDay[] destination)
        {
            if (_source != null)
            {
                if (AppManager.ShowWarningQuestion(string.Format("Do you want to paste data from {0} for selected days?", _source.Date.ToString("MM/dd/yy"))) == System.Windows.Forms.DialogResult.Yes)
                {
                    foreach (CalendarDay day in destination)
                    {
                        day.Comment1 = _source.Comment1;
                        day.Comment2 = _source.Comment2;
                        day.Digital = _source.Digital.Clone(day);
                        day.Newspaper = _source.Newspaper.Clone(day);
                    }
                    if (this.DayPasted != null)
                        this.DayPasted(null, null);
                }
            }
        }
    }
}
