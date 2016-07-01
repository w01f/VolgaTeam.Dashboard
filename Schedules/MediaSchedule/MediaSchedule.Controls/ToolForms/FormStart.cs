using System;
using System.Drawing;
using Asa.Business.Media.Configuration;
using DevComponents.DotNetBar.Metro;

namespace Asa.Media.Controls.ToolForms
{
	public partial class FormStart : MetroForm
	{
		public FormStart()
		{
			InitializeComponent();
			Text = String.Format(Text, MediaMetaData.Instance.DataTypeString);
			if (ResourceManager.Instance.MainAppRibbonLogoFile.ExistsLocal())
				pbLogo.Image = Image.FromFile(ResourceManager.Instance.MainAppRibbonLogoFile.LocalPath);
			if ((CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 3, laTitle.Font.Style);
				buttonXNew.Font = new Font(buttonXNew.Font.FontFamily, buttonXNew.Font.Size - 2, buttonXNew.Font.Style);
				buttonXOpen.Font = new Font(buttonXOpen.Font.FontFamily, buttonXOpen.Font.Size - 2, buttonXOpen.Font.Style);
				buttonXExit.Font = new Font(buttonXExit.Font.FontFamily, buttonXExit.Font.Size - 2, buttonXExit.Font.Style);
			}
		}
	}
}