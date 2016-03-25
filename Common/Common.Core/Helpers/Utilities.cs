using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;
using Application = Microsoft.Office.Interop.PowerPoint.Application;

namespace Asa.Common.Core.Helpers
{
	public static class Utilities
	{
		public static bool IsWindows10()
		{
			var reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion");

			string productName = (string)reg.GetValue("ProductName");

			return productName.StartsWith("Windows 10");
		}

		public static void MakeFolderAvailable(DirectoryInfo folder)
		{
			try
			{
				foreach (var subFolder in folder.GetDirectories())
					MakeFolderAvailable(subFolder);
				foreach (FileInfo file in folder.GetFiles())
					if (File.Exists(file.FullName))
						File.SetAttributes(file.FullName, FileAttributes.Normal);
			}
			catch { }
		}

		public static void DeleteFolder(string folderPath, string filter = "")
		{
			DeleteFolder(new DirectoryInfo(folderPath), filter);
		}

		public static void DeleteFolder(DirectoryInfo folder, string filter = "")
		{
			try
			{
				if (!folder.Exists) return;
				MakeFolderAvailable(folder);
				foreach (var subFolder in folder.GetDirectories())
					DeleteFolder(subFolder, filter);
				foreach (var file in folder.GetFiles())
				{
					try
					{
						if (File.Exists(file.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
							File.Delete(file.FullName);
					}
					catch
					{
						try
						{
							Thread.Sleep(100);
							if (File.Exists(file.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
								File.Delete(file.FullName);
						}
						catch { }
					}
				}
				try
				{
					if (Directory.Exists(folder.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
						Directory.Delete(folder.FullName, false);
				}
				catch
				{
					try
					{
						Thread.Sleep(100);
						if (Directory.Exists(folder.FullName) && (folder.Name.Contains(filter) || string.IsNullOrEmpty(filter)))
							Directory.Delete(folder.FullName, false);
					}
					catch { }
				}
			}
			catch { }
		}

		public static void ReleaseComObject(object o)
		{
			try
			{
				Marshal.FinalReleaseComObject(o);
				o = null;
			}
			catch { }
		}

		public static void ActivateForm(IntPtr handle, bool maximized, bool topMost)
		{
			WinAPIHelper.ShowWindow(handle, maximized ? WindowShowStyle.ShowMaximized : WindowShowStyle.ShowNormal);
			uint lpdwProcessId;
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
			WinAPIHelper.SetForegroundWindow(handle);
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
			if (topMost)
				WinAPIHelper.MakeTopMost(handle);
			else
				WinAPIHelper.MakeNormal(handle);
		}

		public static void MinimizeForm(IntPtr handle)
		{
			var form = Control.FromHandle(handle) as Form;
			if (form != null)
				form.WindowState = FormWindowState.Minimized;
		}

		public static void ActivatePowerPoint(Application powerPoint)
		{
			if (powerPoint == null) return;
			var powerPointHandle = new IntPtr(powerPoint.HWND);
			WinAPIHelper.ShowWindow(powerPointHandle, WindowShowStyle.ShowMaximized);
			uint lpdwProcessId;
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
			WinAPIHelper.SetForegroundWindow(powerPointHandle);
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
		}

		public static void ActivateTaskbar()
		{
			var taskBarHandle = WinAPIHelper.FindWindow("Shell_traywnd", "");
			WinAPIHelper.ShowWindow(taskBarHandle, WindowShowStyle.Show);
			uint lpdwProcessId;
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
			WinAPIHelper.SetForegroundWindow(taskBarHandle);
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
		}
	}
}
