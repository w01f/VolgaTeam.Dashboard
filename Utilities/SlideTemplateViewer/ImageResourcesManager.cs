using System.Drawing;
using System.IO;
using Asa.Business.Dashboard.Configuration;

namespace Asa.SlideTemplateViewer
{
	public class ImageResourcesManager
	{
		public Icon MainAppIcon { get; private set; }
		public Image MainAppRibbonLogo { get; private set; }
		public Image FloaterLogo { get; private set; }

		#region Common Ribbon Resources
		public Image RibbonOutputImage { get; private set; }
		public Image RibbonPreviewImage { get; private set; }
		#endregion

		#region Main Menu
		public Image MainMenuOutputPdfImage { get; private set; }
		public Image MainMenuEmailImage { get; private set; }
		public Image MainMenuSlideSettingsImage { get; private set; }
		public Image MainMenuHelpImage { get; private set; }
		public Image MainMenuExitImage { get; private set; }
		#endregion

		#region Qat Menu
		public Image QatFloaterImage { get; private set; }
		public Image QatHelpImage { get; private set; }
		#endregion

		public void Load()
		{
			var resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "form_icon.ico");
			if (File.Exists(resourceFile))
				MainAppIcon = new Icon(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "app_logo.png");
			if (File.Exists(resourceFile))
				MainAppRibbonLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "branding_image.png");
			if (File.Exists(resourceFile))
				FloaterLogo = Image.FromFile(resourceFile);

			#region Common Ribbon Resources
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "ribbon_output.png");
			if (File.Exists(resourceFile))
				RibbonOutputImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "ribbon_preview.png");
			if (File.Exists(resourceFile))
				RibbonPreviewImage = Image.FromFile(resourceFile);
			#endregion

			#region Main Menu
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "filetab_savepdf.png");
			if (File.Exists(resourceFile))
				MainMenuOutputPdfImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "filetab_sendemail.png");
			if (File.Exists(resourceFile))
				MainMenuEmailImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "filetab_preferences.png");
			if (File.Exists(resourceFile))
				MainMenuSlideSettingsImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "filetab_help.png");
			if (File.Exists(resourceFile))
				MainMenuHelpImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "filetab_exit.png");
			if (File.Exists(resourceFile))
				MainMenuExitImage = Image.FromFile(resourceFile);
			#endregion

			#region Qat Menu
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "titlebar_floater.png");
			if (File.Exists(resourceFile))
				QatFloaterImage = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(ResourceManager.Instance.ImageResourcesFolder.LocalPath, "titlebar_help.png");
			if (File.Exists(resourceFile))
				QatHelpImage = Image.FromFile(resourceFile);
			#endregion
		}
	}
}
