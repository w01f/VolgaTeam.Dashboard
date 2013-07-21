using System.Drawing;

namespace CommandCentral.CommonClasses
{
    class Station
    {
        public string Name { get; set; }
        public Image Logo { get; set; }

        public Station()
        {
            this.Name = string.Empty;
        }
    }
}
