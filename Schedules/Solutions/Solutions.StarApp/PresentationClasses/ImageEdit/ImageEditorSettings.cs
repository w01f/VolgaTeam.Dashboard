using System.Drawing;

namespace Asa.Solutions.StarApp.PresentationClasses.ImageEdit
{
	class ImageEditorSettings
	{
		public Image DefaultImage { get; set; }
		public Image CurrentImage { get; set; }
		public string CurrentImageName { get; set; }

		public void ResetCurrent()
		{
			CurrentImage = null;
			CurrentImageName = null;
		}
	}
}
