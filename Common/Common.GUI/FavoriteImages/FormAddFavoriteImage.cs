using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;

namespace Asa.Common.GUI.FavoriteImages
{
	public partial class FormAddFavoriteImage : MetroForm
	{
		private readonly List<string> _existedNames = new List<string>();

		public FormAddFavoriteImage(Image targetImage, string defaultName, IEnumerable<string> existedNames)
		{
			InitializeComponent();
			textEditImageName.EditValue = defaultName;
			pictureEditImage.Image = targetImage;
			_existedNames.AddRange(existedNames);

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}

		public string ImageName
		{
			get
			{
				if (textEditImageName.EditValue != null)
					return textEditImageName.EditValue.ToString();
				return null;
			}
		}

		private void textEditScheduleName_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode != Keys.Enter) return;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void FormAddFavoriteImage_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult != DialogResult.OK) return;
			if (String.IsNullOrEmpty(ImageName))
			{
				PopupMessageHelper.Instance.ShowWarning("Image Name can't be empty");
				e.Cancel = true;
			}
			else if (_existedNames.Contains(ImageName.ToLower()))
			{
				PopupMessageHelper.Instance.ShowWarning("Image must have unique name");
				e.Cancel = true;
			}
			else
				e.Cancel = false;
		}
	}
}