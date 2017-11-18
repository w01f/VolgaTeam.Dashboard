using System;
using System.Windows.Forms;
using Asa.Browser.Controls.Controls;
using Asa.Common.Core.OfficeInterops;
using Asa.Common.GUI.Floater;
using Asa.Media.Controls.BusinessClasses.Managers;

namespace Asa.Media.Controls.PresentationClasses.Browser
{
	class MediaSiteContainer : SiteContainerControl, IMediaSite
	{
		public override PowerPointSingletonProcessor PowerPointSingleton
			=> BusinessObjects.Instance.PowerPointManager.Processor;

		public override Form MainForm => Controller.Instance.FormMain;

		public MediaSiteContainer()
		{
			buttonItemFloater.Visible = false;
		}

		public override void ShowFloater(FloaterRequestedEventArgs args)
		{
			Controller.Instance.ShowFloater(args.AfterShow);
		}

		public override bool CheckPowerPointRunning(Action afterRun)
		{
			return Controller.Instance.CheckPowerPointRunning(afterRun);
		}
	}
}
