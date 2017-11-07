using System.Drawing;
using Asa.Common.Core.Helpers;
using DevExpress.Skins;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Output
{
	public partial class FormOutputSettings : DevComponents.DotNetBar.Metro.MetroForm
	{
		public FormOutputSettings()
		{
			InitializeComponent();

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void FormOutputSettings_Load(object sender, System.EventArgs e)
		{
			checkEditCloneLineToTheEnd.ForeColor = checkEditCloneLineToTheEnd.Enabled ? Color.Black : Color.Gray;
		}
	}
}