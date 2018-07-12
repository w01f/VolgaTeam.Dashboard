using Asa.Business.Solutions.Common.Entities.NonPersistent;
using DevComponents.DotNetBar.Metro;

namespace Asa.Solutions.Common.PresentationClasses.ClipartEdit
{
	public partial class FormPreviewImage : MetroForm
	{
		public FormPreviewImage(ImageClipartObject clipartObject)
		{
			InitializeComponent();
			pictureEditImage.Image = clipartObject.Image;
		}
	}
}