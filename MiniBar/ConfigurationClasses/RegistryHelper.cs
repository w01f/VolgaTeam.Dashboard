using System;
using Microsoft.Win32;

namespace MiniBar.ConfigurationClasses
{
	public static class RegistryHelper
	{
		public static IntPtr MinibarHandle
		{
			get
			{
				var result = 0;
				var key = Registry.CurrentUser.OpenSubKey(@"Software\NewBizWiz", RegistryKeyPermissionCheck.ReadSubTree);
				if (key != null)
				{
					object value = key.GetValue("MinibarHandle", false);
					if (value != null)
						int.TryParse(value.ToString(), out result);
				}
				return new IntPtr(result);
			}
			set
			{
				RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software", RegistryKeyPermissionCheck.ReadWriteSubTree).CreateSubKey("NewBizWiz", RegistryKeyPermissionCheck.ReadWriteSubTree);
				if (key != null)
					key.SetValue("MinibarHandle", value.ToInt32(), RegistryValueKind.DWord);
			}
		}

		public static bool ShowHidden
		{
			get
			{
				var result = false;
				var key = Registry.CurrentUser.OpenSubKey(@"Software\NewBizWiz", RegistryKeyPermissionCheck.ReadSubTree);
				if (key != null)
				{
					object value = key.GetValue("ShowMinibarHidden", false);
					if (value != null)
					{
						int tempInt = 0;
						int.TryParse(value.ToString(), out tempInt);
						result = tempInt == 1;
					}
				}
				return result;
			}
			set
			{
				var key = Registry.CurrentUser.OpenSubKey(@"Software", RegistryKeyPermissionCheck.ReadWriteSubTree).CreateSubKey("NewBizWiz", RegistryKeyPermissionCheck.ReadWriteSubTree);
				if (key != null)
					key.SetValue("ShowMinibarHidden", value, RegistryValueKind.DWord);
			}
		}

		public static bool ShowFloat
		{
			get
			{
				var result = false;
				var key = Registry.CurrentUser.OpenSubKey(@"Software\NewBizWiz", RegistryKeyPermissionCheck.ReadSubTree);
				if (key != null)
				{
					object value = key.GetValue("ShowMinibarFloat", false);
					if (value != null)
					{
						int tempInt = 0;
						int.TryParse(value.ToString(), out tempInt);
						result = tempInt == 1;
					}
				}
				return result;
			}
			set
			{
				var key = Registry.CurrentUser.OpenSubKey(@"Software", RegistryKeyPermissionCheck.ReadWriteSubTree).CreateSubKey("NewBizWiz", RegistryKeyPermissionCheck.ReadWriteSubTree);
				if (key != null)
					key.SetValue("ShowMinibarFloat", value, RegistryValueKind.DWord);
			}
		}
	}
}