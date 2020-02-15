using System.Drawing;

namespace Asa.Media.Resources
{
    public partial class ResourceContainer
    {
        public Image OptionsRibbonLogo => Ribbon.Options.Resource.New;
        public Image OptionsNoRecordsLogo => Controls.Options.Resource.DefaultOptions;
        public Image OptionsNoProgramsLogo => Controls.Options.Resource.DefaultProgram;
        public Image OptionsNoDigitalItemsLogo => Controls.Options.Resource.DefaultDigital;
        public Image OptionsNewPopupLogo => Controls.Options.Resource.PopupOptionsNew;
        public Image OptionsRetractableBarColumnsImage => Controls.Options.Resource.RetractableBarColumns;
        public Image OptionsRetractableBarDigitalImage => Controls.Options.Resource.RetractableBarDigitalInfo;
        public Image OptionsRetractableBarSummaryImage => Controls.Options.Resource.RetractableBarSummaryInfo;
        public Image OptionsRetractableBarColorsImage => Controls.Options.Resource.RetractableBarColors;
    }
}
