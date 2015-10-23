using System;
using Microsoft.Win32;

namespace Asa.Core.Common
{
	public static class RegistryHelper
	{
		public static bool MaximizeMainForm
		{
			get
			{
				bool result = false;
				RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\NewBizWiz", RegistryKeyPermissionCheck.ReadSubTree);
				if (key != null)
				{
					object value = key.GetValue("MaximizeMainForm", false);
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
				RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software", RegistryKeyPermissionCheck.ReadWriteSubTree).CreateSubKey("NewBizWiz", RegistryKeyPermissionCheck.ReadWriteSubTree);
				if (key != null)
					key.SetValue("MaximizeMainForm", value, RegistryValueKind.DWord);
			}
		}

		public static IntPtr MainFormHandle
		{
			get
			{
				int result = 0;
				RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software\NewBizWiz", RegistryKeyPermissionCheck.ReadSubTree);
				if (key != null)
				{
					object value = key.GetValue("MainFormHandle", false);
					if (value != null)
						int.TryParse(value.ToString(), out result);
				}
				return new IntPtr(result);
			}
			set
			{
				RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Software", RegistryKeyPermissionCheck.ReadWriteSubTree).CreateSubKey("NewBizWiz", RegistryKeyPermissionCheck.ReadWriteSubTree);
				if (key != null)
					key.SetValue("MainFormHandle", value.ToInt32(), RegistryValueKind.DWord);
			}
		}
	}
}