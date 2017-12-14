using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace Asa.Browser.Single.Configuration
{
	class ResourceManager
	{
		public Image BrowserNavigationBack { get; private set; }
		public Image BrowserNavigationForward { get; private set; }
		public Image BrowserNavigationRefresh { get; private set; }
		public Image BrowserExternalChrome { get; private set; }
		public Image BrowserExternalFirefox { get; private set; }
		public Image BrowserExternalIE { get; private set; }
		public Image BrowserExternalEdge { get; private set; }
		public Image BrowserPowerPointAddSlide { get; private set; }
		public Image BrowserPowerPointAddSlides { get; private set; }
		public Image BrowserPowerPointPrint { get; private set; }
		public Image BrowserVideoAdd { get; private set; }
		public Image BrowserYoutubeAdd { get; private set; }
		public Image BrowserUrlCopy { get; private set; }
		public Image BrowserUrlEmail { get; private set; }
		public Image BrowserFloater { get; private set; }

		public static ResourceManager Instance { get; } = new ResourceManager();

		private ResourceManager() { }

		public void LoadResources()
		{
			var appFileName = Process.GetCurrentProcess().MainModule.FileName;
			var resorceFolderPath = Path.Combine(Path.GetDirectoryName(appFileName), "Resources");
			var resourceFile = Path.Combine(resorceFolderPath, "eo_left.png");
			if (File.Exists(resourceFile))
				BrowserNavigationBack = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(resorceFolderPath, "eo_right.png");
			if (File.Exists(resourceFile))
				BrowserNavigationForward = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(resorceFolderPath, "eo_refresh.png");
			if (File.Exists(resourceFile))
				BrowserNavigationRefresh = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(resorceFolderPath, "eo_chrome.png");
			if (File.Exists(resourceFile))
				BrowserExternalChrome = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(resorceFolderPath, "eo_firefox.png");
			if (File.Exists(resourceFile))
				BrowserExternalFirefox = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(resorceFolderPath, "eo_ie.png");
			if (File.Exists(resourceFile))
				BrowserExternalIE = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(resorceFolderPath, "eo_edge.png");
			if (File.Exists(resourceFile))
				BrowserExternalEdge = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(resorceFolderPath, "eo_add_slide.png");
			if (File.Exists(resourceFile))
				BrowserPowerPointAddSlide = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(resorceFolderPath, "eo_add_all.png");
			if (File.Exists(resourceFile))
				BrowserPowerPointAddSlides = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(resorceFolderPath, "eo_printer.png");
			if (File.Exists(resourceFile))
				BrowserPowerPointPrint = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(resorceFolderPath, "eo_video.png");
			if (File.Exists(resourceFile))
				BrowserVideoAdd = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(resorceFolderPath, "eo_youtube.png");
			if (File.Exists(resourceFile))
				BrowserYoutubeAdd = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(resorceFolderPath, "eo_copy.png");
			if (File.Exists(resourceFile))
				BrowserUrlCopy = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(resorceFolderPath, "eo_email.png");
			if (File.Exists(resourceFile))
				BrowserUrlEmail = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(resorceFolderPath, "eo_floater.png");
			if (File.Exists(resourceFile))
				BrowserFloater = Image.FromFile(resourceFile);
		}
	}
}
