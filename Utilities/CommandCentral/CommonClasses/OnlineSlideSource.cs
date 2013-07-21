using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandCentral.CommonClasses
{
    class OnlineSlideSource
    {
        public string TemplateName { get; set; }
        public bool ShowWebsite { get; set; }
        public bool ShowBusinessName { get; set; }
        public bool ShowDecisionMaker { get; set; }
        public bool ShowPresentationDate { get; set; }
        public bool ShowProduct { get; set; }
        public bool ShowDescription { get; set; }
        public bool ShowDimensions { get; set; }
        public bool ShowFlightDates { get; set; }
        public bool ShowActiveDays { get; set; }
        public bool ShowTotalAds { get; set; }
        public bool ShowAdRate { get; set; }
        public bool ShowMonthlyInvestment { get; set; }
        public bool ShowTotalInvestment { get; set; }
        public bool ShowMonthlyImpressions { get; set; }
        public bool ShowTotalImpressions { get; set; }
        public bool ShowMonthlyCPM { get; set; }
        public bool ShowTotalCPM { get; set; }
        public bool ShowComments { get; set; }
        public bool ShowDuration { get; set; }
        public bool ShowImages { get; set; }
        public bool ShowScreenshot { get; set; }
        public bool ShowSignature { get; set; }

        public OnlineSlideSource()
        {
            this.TemplateName = string.Empty;
        }
    }
}
