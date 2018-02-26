using System;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Images;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;

namespace Asa.Solutions.StarApp.PresentationClasses.ImageEdit
{
	public partial class FormFavoritesOpen : MetroForm
	{
		public ImageSource SelectedImageSource => favoriteImagesControl.SelectedImageSource;

		public FormFavoritesOpen()
		{
			InitializeComponent();

			favoriteImagesControl.Init();
			favoriteImagesControl.OnImageDoubleClick += OnImageDoubleClick;

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}

		private void OnImageDoubleClick(Object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
		}
	}
}