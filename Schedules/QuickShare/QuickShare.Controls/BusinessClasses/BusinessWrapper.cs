using System.IO;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.Core.QuickShare;
using NewBizWiz.MediaSchedule.Controls.BusinessClasses;

namespace NewBizWiz.QuickShare.Controls.BusinessClasses
{
	public class BusinessWrapper
	{
		private static readonly BusinessWrapper _instance = new BusinessWrapper();

		private BusinessWrapper()
		{
			OutputManager = new OutputManager();
			PackageManager = new PackageManager();
			HelpManager = new HelpManager(MediaMetaData.Instance.SettingsManager.HelpLinksPath);
			ThemeManager = new ThemeManager(Path.Combine(SettingsManager.Instance.ThemeCollectionPath, SettingsManager.Instance.SlideMasterFolder));
			ActivityManager = new ActivityManager(MediaMetaData.Instance.SettingsManager.ActivityTrackName);
		}

		public static BusinessWrapper Instance
		{
			get { return _instance; }
		}

		public PackageManager PackageManager { get; private set; }
		public HelpManager HelpManager { get; private set; }
		public OutputManager OutputManager { get; private set; }
		public TabPageManager TabPageManager { get; private set; }
		public ThemeManager ThemeManager { get; private set; }
		public ActivityManager ActivityManager { get; private set; }
	}
}
