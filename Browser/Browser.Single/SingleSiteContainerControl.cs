using System;
using System.Drawing;
using System.Windows.Forms;
using Asa.Browser.Controls.Controls;
using Asa.Browser.Single.Configuration;
using Asa.Browser.Single.InteropClasses;
using Asa.Common.Core.OfficeInterops;
using Asa.Common.GUI.Floater;

namespace Asa.Browser.Single
{
	class SingleSiteContainerControl : SiteBundleControl
	{
		public override PowerPointSingletonProcessor PowerPointSingleton => new BrowserPowerPointSingleton();
		public override Form MainForm => FormMain.Instance;
		public override Image SplashLogo => AppSettingsManager.Instance.SplashLogo;

		public override void ShowFloater(FloaterRequestedEventArgs args)
		{
			AppManager.Instance.ShowFloater(args.AfterShow, args.AfterBack);
		}

		public override bool CheckPowerPointRunning(Action afterRun)
		{
			return BrowserPowerPointSingleton.Instance.CheckPowerPointRunning(afterRun);
		}

		public override void UpdateMainStatusBarInfo() { }
	}
}
