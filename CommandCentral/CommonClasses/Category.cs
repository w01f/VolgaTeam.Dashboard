using System.Drawing;

namespace CommandCentral.CommonClasses
{
    class Category
    {
        public int Order { get; set; }
        public string Name { get; set; }
        public Image Logo { get; set; }
        public string TooltipTitle { get; set; }
        public string TooltipValue { get; set; }
    }
}
