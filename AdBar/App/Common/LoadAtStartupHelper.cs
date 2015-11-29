using System.Windows.Forms;
using Microsoft.Win32;

namespace Asa.Bar.App.Common
{
	static class LoadAtStartupHelper
	{
		private const string StartupRoot = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
		private const string AppKey = "adSalesApps";

		public static bool IsLoadAtStartupEnabled()
		{
			return GetRegistryKey().GetValue(AppKey) != null;
		}

		public static void SetLoadAtStartup()
		{
			GetRegistryKey().SetValue(AppKey, Application.ExecutablePath);
		}

		public static void UnsetLoadAtStartup()
		{
			if(!IsLoadAtStartupEnabled()) return;
			GetRegistryKey().DeleteValue(AppKey);
		}

		private static RegistryKey GetRegistryKey()
		{
			return Registry.CurrentUser.OpenSubKey(StartupRoot, true);
		}
	}
}
