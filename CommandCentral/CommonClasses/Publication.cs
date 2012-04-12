using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandCentral.CommonClasses
{
    class Publication
    {
        public string Name { get; set; }
        public string SortOrder { get; set; }
        public string Abbreviation { get; set; }
        public string BigLogo { get; set; }
        public string LittleLogo { get; set; }
        public string TinyLogo { get; set; }
        public string DailyCirculation { get; set; }
        public string DailyReadership { get; set; }
        public string SundayCirculation { get; set; }
        public string SundayReadership { get; set; }

        public bool AllowSundaySelect { get; set; }
        public bool AllowMondaySelect { get; set; }
        public bool AllowTuesdaySelect { get; set; }
        public bool AllowWednesdaySelect { get; set; }
        public bool AllowThursdaySelect { get; set; }
        public bool AllowFridaySelect { get; set; }
        public bool AllowSaturdaySelect { get; set; }

        public Publication()
        {
            this.Name = string.Empty;
            this.SortOrder = string.Empty;
            this.Abbreviation = string.Empty;
            this.BigLogo = string.Empty;
            this.LittleLogo = string.Empty;
            this.TinyLogo = string.Empty;
            this.DailyCirculation = string.Empty;
            this.DailyReadership = string.Empty;
            this.SundayCirculation = string.Empty;
            this.SundayReadership = string.Empty;
        }
    }
}
