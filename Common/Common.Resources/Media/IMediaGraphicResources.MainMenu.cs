using System.Drawing;

namespace Asa.Common.Resources.Media
{
    public partial interface IMediaGraphicResources
    {
        Image MainMenuNewImage { get; }
        Image MainMenuOpenImage { get; }
        Image MainMenuSaveImage { get; }
        Image MainMenuSaveAsImage { get; }
        Image MainMenuOutputPdfImage { get; }
        Image MainMenuEmailImage { get; }
        Image MainMenuSlideSettingsImage { get; }
        Image MainMenuHelpImage { get; }
        Image MainMenuExitImage { get; }
    }
}
