using System;
using System.Windows.Forms;
using Asa.Business.Solutions.Common.Entities.NonPersistent;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Slides;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;

namespace Asa.Solutions.Common.PresentationClasses.SlidesEdit
{
	public partial class FormSlides : MetroForm
	{
		public SlideMaster SelectedSlide => slidesContainerControl.SelectedSlide;

		public FormSlides(SolutionSlideManager slideManager, SlideMaster currentSlide)
		{
			InitializeComponent();

			Height = (Int32)(Screen.PrimaryScreen.Bounds.Height * 0.8);
			Left = (Screen.PrimaryScreen.Bounds.Width - Width) / 2;
			Top = (Screen.PrimaryScreen.Bounds.Height - Height) / 2;

			slidesContainerControl.InitSlides(slideManager, slideManager.ThumbnailSize);
			slidesContainerControl.SelectSlide(currentSlide);

			var scaleFactor = Utilities.GetScaleFactor(CreateGraphics().DpiX);

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, scaleFactor);
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, scaleFactor);
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, scaleFactor);
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, scaleFactor);
		}
	}
}