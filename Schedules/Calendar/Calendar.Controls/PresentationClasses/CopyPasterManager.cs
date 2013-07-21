using System;

namespace CalendarBuilder.PresentationClasses
{
    public class CopyPasteManager
    {
        public BusinessClasses.CalendarDay SourceDay { get; private set; }
        public BusinessClasses.CalendarNote SourceNote { get; private set; }
        
        public Views.IView ParentView { get; private set; }
        
        public event EventHandler<EventArgs> DayCopied;
        public event EventHandler<EventArgs> DayPasted;

        public event EventHandler<EventArgs> NoteCopied;

        public event EventHandler<EventArgs> OnSetCopyDay;
        public event EventHandler<EventArgs> OnResetCopy;
        public event EventHandler<EventArgs> OnResetPaste;

        public CopyPasteManager(Views.IView parentView)
        {
            this.ParentView = parentView;
        }

        public void SetCopyDay()
        {
            if (this.OnSetCopyDay != null)
                this.OnSetCopyDay(null, null);
        }

        public void ResetCopy()
        {
            if (this.OnResetCopy != null)
                this.OnResetCopy(null, null);
        }

        public void ResetPaste()
        {
            this.SourceDay = null;
            this.SourceNote = null;
            if (this.OnResetPaste != null)
                this.OnResetPaste(null, null);
        }

        public void CopyDay(BusinessClasses.CalendarDay source)
        {
            this.SourceDay = source;
            if (this.SourceDay != null)
            {
                if (this.DayCopied != null)
                    this.DayCopied(null, null);
            }
        }

        public void CloneDay(BusinessClasses.CalendarDay source, BusinessClasses.CalendarDay[] destination, BusinessClasses.DayDataType dataToPaste = BusinessClasses.DayDataType.All)
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

        public void PasteDay(BusinessClasses.CalendarDay[] destination, BusinessClasses.DayDataType dataToPaste = BusinessClasses.DayDataType.All)
        {
            if (this.SourceDay != null && destination != null)
            {
                foreach (BusinessClasses.CalendarDay day in destination)
                {
                    switch (dataToPaste)
                    {
                        case BusinessClasses.DayDataType.Comment:
                            day.Comment1 = this.SourceDay.Comment1;
                            day.Comment2 = this.SourceDay.Comment2;
                            break;
                        case BusinessClasses.DayDataType.Digital:
                            day.Digital = this.SourceDay.Digital.Clone(day);
                            break;
                        case BusinessClasses.DayDataType.Logo:
                            day.Logo = this.SourceDay.Logo.Clone(day);
                            break;
                        case BusinessClasses.DayDataType.Newspaper:
                            day.Newspaper = this.SourceDay.Newspaper.Clone(day);
                            break;
                        case BusinessClasses.DayDataType.All:
                            day.Comment1 = this.SourceDay.Comment1;
                            day.Comment2 = this.SourceDay.Comment2;
                            day.Digital = this.SourceDay.Digital.Clone(day);
                            day.Newspaper = this.SourceDay.Newspaper.Clone(day);
                            day.Logo = this.SourceDay.Logo.Clone(day);
                            break;
                    }
                }
                if (this.DayPasted != null)
                    this.DayPasted(null, null);
            }
        }

        public void CopyNote(BusinessClasses.CalendarNote source)
        {
            this.SourceNote = source;
            if (this.SourceNote != null)
            {
                if (this.NoteCopied != null)
                    this.NoteCopied(null, null);
            }
        }

        //public void CloneNote(BusinessClasses.CalendarNote source, BusinessClasses.CalendarDay[] destination)
        //{
        //    if (source != null && destination != null)
        //    {
        //        foreach (BusinessClasses.CalendarDay day in destination)
        //        {
        //        }
        //        if (this.DayPasted != null)
        //            this.DayPasted(null, null);
        //    }
        //}
    }
}
