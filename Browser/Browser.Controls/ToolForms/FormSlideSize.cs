using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;

namespace Asa.Browser.Controls.ToolForms
{
	public partial class FormSlideSize : MetroForm
	{
		public FormSlideSize()
		{
			InitializeComponent();

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);
			layoutControlItemTitleLogo.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTitleLogo.MaxSize, scaleFactor);
			layoutControlItemTitleLogo.MinSize = RectangleHelper.ScaleSize(layoutControlItemTitleLogo.MinSize, scaleFactor);
			layoutControlItemTitleText.MaxSize = RectangleHelper.ScaleSize(layoutControlItemTitleText.MaxSize, scaleFactor);
			layoutControlItemTitleText.MinSize = RectangleHelper.ScaleSize(layoutControlItemTitleText.MinSize, scaleFactor);
			layoutControlItem169.MaxSize = RectangleHelper.ScaleSize(layoutControlItem169.MaxSize, scaleFactor);
			layoutControlItem169.MinSize = RectangleHelper.ScaleSize(layoutControlItem169.MinSize, scaleFactor);
			layoutControlItem43.MaxSize = RectangleHelper.ScaleSize(layoutControlItem43.MaxSize, scaleFactor);
			layoutControlItem43.MinSize = RectangleHelper.ScaleSize(layoutControlItem43.MinSize, scaleFactor);
			layoutControlItem34.MaxSize = RectangleHelper.ScaleSize(layoutControlItem34.MaxSize, scaleFactor);
			layoutControlItem34.MinSize = RectangleHelper.ScaleSize(layoutControlItem34.MinSize, scaleFactor);
		}

		private void OnSize169Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Yes;
		}

		private void OnSize43Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.No;
		}

		private void OnSize34Click(object sender, System.EventArgs e)
		{
			DialogResult = DialogResult.Retry;
		}
	}
}
