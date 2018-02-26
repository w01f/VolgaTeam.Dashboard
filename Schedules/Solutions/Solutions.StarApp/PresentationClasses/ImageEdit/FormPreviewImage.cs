using System.Drawing;
using DevComponents.DotNetBar.Metro;

namespace Asa.Solutions.StarApp.PresentationClasses.ImageEdit
{
	public partial class FormPreviewImage : MetroForm
	{
		public FormPreviewImage(Image targetImage)
		{
			InitializeComponent();
			pictureEditImage.Image = targetImage;
		}
	}
}