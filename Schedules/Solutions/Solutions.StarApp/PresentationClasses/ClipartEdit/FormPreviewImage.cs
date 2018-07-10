using Asa.Business.Solutions.Common.Entities.NonPersistent;
using DevComponents.DotNetBar.Metro;

namespace Asa.Solutions.StarApp.PresentationClasses.ClipartEdit
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