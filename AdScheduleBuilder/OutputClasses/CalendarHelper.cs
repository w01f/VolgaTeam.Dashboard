using System;
using System.Collections.Generic;

namespace AdScheduleBuilder.OutputClasses
{
    public class CalendarHelper
    {
        public static DateTime[][] GetDaysByWeek(DateTime start, DateTime end)
        {
            List<DateTime[]> weeks = new List<DateTime[]>();
            List<DateTime> week = new List<DateTime>();
            while (!(start > end))
            {
                week.Add(start);
                if (start.DayOfWeek == DayOfWeek.Saturday)
                {
                    weeks.Add(week.ToArray());
                    week.Clear();
                }
                start = start.AddDays(1);
            }
            if(week.Count>0)
                weeks.Add(week.ToArray());
            return weeks.ToArray();
        }

        public static int GetMonthIndex(string monthName)
        {
            switch (monthName.ToLower())
            { 
                case "january":
                    return 1;
                case "february":
                    return 2;
                case "march":
                    return 3;
                case "april":
                    return 4;
                case "may":
                    return 5;
                case "june":
                    return 6;
                case "july":
                    return 7;
                case "august":
                    return 8;
                case "september":
                    return 9;
                case "october":
                    return 10;
                case "november":
                    return 11;
                case "december":
                    return 12;
                default:
                    return 1;
            }
        }
    }

    public struct DayOutput
    {
        public int RecordsCount { get;set;}
        public bool HasNotes { get; set; }
        public string RecordsText { get; set; }
        public float FontSize
        {
            get 
            {
                if (this.HasNotes && this.RecordsCount == 1)
                {
                    return 9;
                }
                else if (this.HasNotes && this.RecordsCount > 1)
                {
                    return 7;
                }
                else
                {
                    switch (this.RecordsCount)
                    {
                        case 1:
                            return 12;
                        case 2:
                            return 11;
                        case 3:
                            return 8;
                        case 4:
                            return 7.5f;
                        case 5:
                        case 6:
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                            return 7;
                        case 11:
                            return 6;
                        case 12:
                            return 6;
                        default:
                            return 6;
                    }
                }
            }
        }
    }
}
