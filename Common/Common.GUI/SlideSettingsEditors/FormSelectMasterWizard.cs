﻿using Asa.Common.Core.Helpers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;

namespace Asa.Common.GUI.SlideSettingsEditors
{
	public partial class FormSelectMasterWizard : MetroForm
	{
		public FormSelectMasterWizard()
		{
			InitializeComponent();

			layoutControlItemOK.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOK.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOK.MinSize = RectangleHelper.ScaleSize(layoutControlItemOK.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MaxSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemCancel.MinSize = RectangleHelper.ScaleSize(layoutControlItemCancel.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}
	}
}
