using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CommandCentral.CommonClasses
{
    class ShareUnit
    {
        public string RateCard { get; set; }
        public string PercentOfPage { get; set; }
        public string Width { get; set; }
        public string WidthMeasureUnit { get; set; }
        public string Height { get; set; }
        public string HeightMeasureUnit { get; set; }


        public ShareUnit()
        {
            this.RateCard = string.Empty;
            this.PercentOfPage = string.Empty;
            this.Width = string.Empty;
            this.WidthMeasureUnit = string.Empty;
            this.Height = string.Empty;
            this.HeightMeasureUnit = string.Empty;
        }
    }
}
