using System;

namespace CalendarBuilder.BusinessClasses
{
    public class DayCopyPaster
    {
        private CalendarDay _source = null;

        public event EventHandler<EventArgs> DayCopied;
        public event EventHandler<EventArgs> DayPasted;
        public event EventHandler<EventArgs> OnSetCopy;
        public event EventHandler<EventArgs> OnResetCopy;
        public event EventHandler<EventArgs> OnSetPaste;
        public event EventHandler<EventArgs> OnResetPaste;

        public void SetCopy()
        {
            if (this.OnSetCopy != null)
                this.OnSetCopy(null, null);
        }

        public void SetPaste()
        {
            if (this.OnSetPaste != null)
                this.OnSetPaste(null, null);
        }

        public void ResetCopy()
        {
            if (this.OnResetCopy != null)
                this.OnResetCopy(null, null);
        }

        public void ResetPaste()
        {
            _source = null;
            if (this.OnResetPaste != null)
                this.OnResetPaste(null, null);
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

        public void Clone(CalendarDay source, CalendarDay[] destination)
        {
            if (source != null && destination != null)
            {
                foreach (CalendarDay day in destination)
                {
                    day.Comment1 = source.Comment1;
                    day.Comment2 = source.Comment2;
                    day.Digital = source.Digital.Clone(day);
                    day.Newspaper = source.Newspaper.Clone(day);
                }
                if (this.DayPasted != null)
                    this.DayPasted(null, null);

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
