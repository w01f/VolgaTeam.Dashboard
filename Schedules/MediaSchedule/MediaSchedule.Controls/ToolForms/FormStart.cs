using System;
using Asa.Business.Media.Configuration;
using Asa.Common.Core.Helpers;
using Asa.Media.Controls.BusinessClasses.Managers;
using DevComponents.DotNetBar.Metro;
using DevExpress.Skins;

namespace Asa.Media.Controls.ToolForms
{
	public partial class FormStart : MetroForm
	{
		public FormStart()
		{
			InitializeComponent();
			Text = String.Format(Text, MediaMetaData.Instance.DataTypeString);
			pictureEditLogo.Image = BusinessObjects.Instance.ImageResourcesManager.MainAppRibbonLogo ?? pictureEditLogo.Image;
			layoutControlItemNew.MaxSize = RectangleHelper.ScaleSize(layoutControlItemNew.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemNew.MinSize = RectangleHelper.ScaleSize(layoutControlItemNew.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpen.MaxSize = RectangleHelper.ScaleSize(layoutControlItemOpen.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemOpen.MinSize = RectangleHelper.ScaleSize(layoutControlItemOpen.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemExit.MaxSize = RectangleHelper.ScaleSize(layoutControlItemExit.MaxSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
			layoutControlItemExit.MinSize = RectangleHelper.ScaleSize(layoutControlItemExit.MinSize, Utilities.GetScaleFactor(CreateGraphics().DpiX));
		}
	}
}