using System;

namespace CalendarBuilder.PresentationClasses
{
    public class CopyPasteManager
    {
        public BusinessClasses.CalendarDay Source { get; private set; }
        
        public Views.IView ParentView { get; private set; }
        
        public event EventHandler<EventArgs> DayCopied;
        public event EventHandler<EventArgs> DayPasted;
        public event EventHandler<EventArgs> OnSetCopy;
        public event EventHandler<EventArgs> OnResetCopy;
        public event EventHandler<EventArgs> OnSetPaste;
        public event EventHandler<EventArgs> OnResetPaste;

        public CopyPasteManager(Views.IView parentView)
        {
            this.ParentView = parentView;
        }

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
            this.Source = null;
            if (this.OnResetPaste != null)
                this.OnResetPaste(null, null);
        }

        public void Copy(BusinessClasses.CalendarDay source)
        {
            this.Source = source;
            if (this.Source != null)
            {
                if (this.DayCopied != null)
                    this.DayCopied(null, null);
            }
        }

        public void Clone(BusinessClasses.CalendarDay source, BusinessClasses.CalendarDay[] destination, BusinessClasses.DayDataType dataToPaste = BusinessClasses.DayDataType.All)
        {
            if (source != null && destination != null)
            {
                foreach (BusinessClasses.CalendarDay day in destination)
                {
                    switch (dataToPaste)
                    {
                        case BusinessClasses.DayDataType.Comment:
                            day.Comment1 = source.Comment1;
                            day.Comment2 = source.Comment2;
                            break;
                        case BusinessClasses.DayDataType.Digital:
                            day.Digital = source.Digital.Clone(day);
                            break;
                        case BusinessClasses.DayDataType.Logo:
                            day.Logo = source.Logo.Clone(day);
                            break;
                        case BusinessClasses.DayDataType.Newspaper:
                            day.Newspaper = source.Newspaper.Clone(day);
                            break;
                        case BusinessClasses.DayDataType.All:
                            day.Comment1 = source.Comment1;
                            day.Comment2 = source.Comment2;
                            day.Digital = source.Digital.Clone(day);
                            day.Newspaper = source.Newspaper.Clone(day);
                            day.Logo = source.Logo.Clone(day);
                            break;
                    }
                }
                if (this.DayPasted != null)
                    this.DayPasted(null, null);

            }
        }

        public void Paste(BusinessClasses.CalendarDay[] destination, BusinessClasses.DayDataType dataToPaste = BusinessClasses.DayDataType.All)
        {
            if (this.Source != null && destination != null)
            {
                foreach (BusinessClasses.CalendarDay day in destination)
                {
                    switch (dataToPaste)
                    {
                        case BusinessClasses.DayDataType.Comment:
                            day.Comment1 = this.Source.Comment1;
                            day.Comment2 = this.Source.Comment2;
                            break;
                        case BusinessClasses.DayDataType.Digital:
                            day.Digital = this.Source.Digital.Clone(day);
                            break;
                        case BusinessClasses.DayDataType.Logo:
                            day.Logo = this.Source.Logo.Clone(day);
                            break;
                        case BusinessClasses.DayDataType.Newspaper:
                            day.Newspaper = this.Source.Newspaper.Clone(day);
                            break;
                        case BusinessClasses.DayDataType.All:
                            day.Comment1 = this.Source.Comment1;
                            day.Comment2 = this.Source.Comment2;
                            day.Digital = this.Source.Digital.Clone(day);
                            day.Newspaper = this.Source.Newspaper.Clone(day);
                            day.Logo = this.Source.Logo.Clone(day);
                            break;
                    }
                }
                if (this.DayPasted != null)
                    this.DayPasted(null, null);
            }
        }
    }
}
