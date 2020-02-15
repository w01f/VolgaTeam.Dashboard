using System.Drawing;

namespace Asa.Media.Resources
{
    public partial class ResourceContainer
    {
        public Image MainMenuNewImage => Ribbon.Menu.Resource.New;
        public Image MainMenuOpenImage => Ribbon.Menu.Resource.Open;
        public Image MainMenuSaveImage => Ribbon.Menu.Resource.Save;
        public Image MainMenuSaveAsImage => Ribbon.Menu.Resource.SaveAs;
        public Image MainMenuOutputPdfImage => Ribbon.Menu.Resource.SavePdf;
        public Image MainMenuEmailImage => Ribbon.Menu.Resource.SendEmail;
        public Image MainMenuSlideSettingsImage => Ribbon.Menu.Resource.Preferences;
        public Image MainMenuHelpImage => Ribbon.Menu.Resource.Help;
        public Image MainMenuExitImage => Ribbon.Menu.Resource.Exit;
    }
}
