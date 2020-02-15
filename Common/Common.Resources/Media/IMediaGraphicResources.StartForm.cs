using System.Drawing;

namespace Asa.Common.Resources.Media
{
    public partial interface IMediaGraphicResources
    {
        Image StartFormBackgroundLogo { get; }
        Image StartFormLogo { get; }
        Image StartFormNewImage { get; }
        Image StartFormOpenImage { get; }
        Image StartFormQuickEditScheduleImage { get; }
        Image StartFormCancelImage { get; }
    }
}
