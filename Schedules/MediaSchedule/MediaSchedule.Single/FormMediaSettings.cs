using System.Drawing;
using System.IO;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;

namespace Asa.Media.Single
{
    public partial class FormMediaSettings : MetroForm
    {
        public FormMediaSettings()
        {
            InitializeComponent();

            var logoFilePath = Path.Combine(ResourceManager.Instance.AppRootFolderPath, "launch.png");
            if (File.Exists(logoFilePath))
                pictureEditLogo.Image = Image.FromFile(logoFilePath);

            layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
            layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
            layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
            layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
        }
    }
}