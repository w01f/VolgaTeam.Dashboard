using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Asa.Core.Interop
{
	public enum WM
	{
		WM_KEYUP = 0x0101,
		WM_KEYDOWN = 0x0100,
		WM_LBUTTONDOWN = 0x0201,
		WM_LBUTTONUP = 0x0202
	}

	public struct WINDOWPLACEMENT
	{
		public int flags;
		public int length;
		public Point ptMaxPosition;
		public Point ptMinPosition;
		public Rectangle rcNormalPosition;
		public int showCmd;
	}

	/// <summary>Enumeration of the different ways of showing a window using 
	/// ShowWindow</summary>
	public enum WindowShowStyle : uint
	{
		/// <summary>Hides the window and activates another window.</summary>
		/// <remarks>See SW_HIDE</remarks>
		Hide = 0,
		/// <summary>Activates and displays a window. If the window is minimized 
		/// or maximized, the system restores it to its original size and 
		/// position. An application should specify this flag when displaying 
		/// the window for the first time.</summary>
		/// <remarks>See SW_SHOWNORMAL</remarks>
		ShowNormal = 1,
		/// <summary>Activates the window and displays it as a minimized window.</summary>
		/// <remarks>See SW_SHOWMINIMIZED</remarks>
		ShowMinimized = 2,
		/// <summary>Activates the window and displays it as a maximized window.</summary>
		/// <remarks>See SW_SHOWMAXIMIZED</remarks>
		ShowMaximized = 3,
		/// <summary>Maximizes the specified window.</summary>
		/// <remarks>See SW_MAXIMIZE</remarks>
		Maximize = 3,
		/// <summary>Displays a window in its most recent size and position. 
		/// This value is similar to "ShowNormal", except the window is not 
		/// actived.</summary>
		/// <remarks>See SW_SHOWNOACTIVATE</remarks>
		ShowNormalNoActivate = 4,
		/// <summary>Activates the window and displays it in its current size 
		/// and position.</summary>
		/// <remarks>See SW_SHOW</remarks>
		Show = 5,
		/// <summary>Minimizes the specified window and activates the next 
		/// top-level window in the Z order.</summary>
		/// <remarks>See SW_MINIMIZE</remarks>
		Minimize = 6,
		/// <summary>Displays the window as a minimized window. This value is 
		/// similar to "ShowMinimized", except the window is not activated.</summary>
		/// <remarks>See SW_SHOWMINNOACTIVE</remarks>
		ShowMinNoActivate = 7,
		/// <summary>Displays the window in its current size and position. This 
		/// value is similar to "Show", except the window is not activated.</summary>
		/// <remarks>See SW_SHOWNA</remarks>
		ShowNoActivate = 8,
		/// <summary>Activates and displays the window. If the window is 
		/// minimized or maximized, the system restores it to its original size 
		/// and position. An application should specify this flag when restoring 
		/// a minimized window.</summary>
		/// <remarks>See SW_RESTORE</remarks>
		Restore = 9,
		/// <summary>Sets the show state based on the SW_ value specified in the 
		/// STARTUPINFO structure passed to the CreateProcess function by the 
		/// program that started the application.</summary>
		/// <remarks>See SW_SHOWDEFAULT</remarks>
		ShowDefault = 10,
		/// <summary>Windows 2000/XP: Minimizes a window, even if the thread 
		/// that owns the window is hung. This flag should only be used when 
		/// minimizing windows from a different thread.</summary>
		/// <remarks>See SW_FORCEMINIMIZE</remarks>
		ForceMinimized = 11
	}


	public class WinAPIHelper
	{
		#region Public constants
		public const Byte BSF_IGNORECURRENTTASK = 2; //this ignores the current app Hex 2
		public const Byte BSF_POSTMESSAGE = 16; //This posts the message Hex 10
		public const Byte BSM_APPLICATIONS = 8; //This tells the windows message to just go to applications Hex 8


		private const UInt32 SWP_NOSIZE = 0x0001;
		private const UInt32 SWP_NOMOVE = 0x0002;
		private const UInt32 SWP_NOZORDER = 0x0004;
		private const UInt32 SWP_NOREDRAW = 0x0008;
		private const UInt32 SWP_NOACTIVATE = 0x0010;
		private const UInt32 SWP_FRAMECHANGED = 0x0020; /* The frame changed: send WM_NCCALCSIZE */
		private const UInt32 SWP_SHOWWINDOW = 0x0040;
		private const UInt32 SWP_HIDEWINDOW = 0x0080;
		private const UInt32 SWP_NOCOPYBITS = 0x0100;
		private const UInt32 SWP_NOOWNERZORDER = 0x0200; /* Don't do owner Z ordering */
		private const UInt32 SWP_NOSENDCHANGING = 0x0400; /* Don't send WM_WINDOWPOSCHANGING */

		public const int WM_APP = 0x8000;

		public const UInt32 TOPMOST_FLAGS = SWP_NOMOVE | SWP_NOSIZE;
		private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
		private static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
		private static readonly IntPtr HWND_TOP = new IntPtr(0);
		private static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

		public static readonly UInt32 WM_NCLBUTTONDOWN = 0xA1;
		public static readonly IntPtr HTCAPTION = new IntPtr(0x2);
		#endregion

		#region API imports
		[DllImport("USER32.DLL", EntryPoint = "BroadcastSystemMessageA", SetLastError = true,
			CharSet = CharSet.Unicode, ExactSpelling = true,
			CallingConvention = CallingConvention.StdCall)]
		public static extern int BroadcastSystemMessage(Int32 dwFlags, ref Int32 pdwRecipients, int uiMessage, int wParam, int lParam);


		[DllImport("USER32.DLL", EntryPoint = "RegisterWindowMessageA", SetLastError = true,
			CharSet = CharSet.Unicode, ExactSpelling = true,
			CallingConvention = CallingConvention.StdCall)]
		public static extern int RegisterWindowMessage(String pString);

		[DllImport("user32.dll")]
		public static extern bool ShowWindow(IntPtr hWnd, WindowShowStyle nCmdShow);

		[DllImport("user32.dll")]
		public static extern IntPtr FindWindow(string className, string windowName);

		[DllImport("user32.dll")]
		public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

		[DllImport("user32.dll")]
		public static extern int SetForegroundWindow(IntPtr wnd);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetActiveWindow(IntPtr hWnd);

		[DllImport("user32.dll")]
		public static extern IntPtr GetForegroundWindow();

		[DllImport("user32.dll", SetLastError = true)]
		public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

		[DllImport("user32.dll")]
		public static extern IntPtr SetFocus(IntPtr hWnd);

		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool PostMessage(IntPtr hWnd, uint Msg, uint wParam, uint lParam);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool BringWindowToTop(IntPtr hWnd);

		[DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
		public static extern bool SystemParametersInfo(uint uiAction, uint uiParam, uint pvParam, uint fWinIni);

		[DllImport("User32.dll", EntryPoint = "ShowWindowAsync")]
		public static extern bool ShowWindowAsync(IntPtr hWnd, WindowShowStyle cmdShow);

		[DllImport("user32.dll")]
		public static extern bool AttachThreadInput(uint idAttach, uint idAttachTo, bool fAttach);

		[DllImport("kernel32.dll")]
		public static extern uint GetCurrentThreadId();

		[DllImport("uxtheme", ExactSpelling = true)]
		public static extern Int32 DrawThemeParentBackground(IntPtr hWnd, IntPtr hdc, ref Rectangle pRect);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsIconic(IntPtr hWnd);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool IsWindowVisible(IntPtr hWnd);

		[DllImport("user32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetWindowPlacement(IntPtr hWnd, out WINDOWPLACEMENT lpwndpl);

		[DllImport("kernel32.dll", SetLastError = true, CallingConvention = CallingConvention.Winapi)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool IsWow64Process([In] IntPtr hProcess, [Out] out bool wow64Process);

		[DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
		public static extern int StrCmpLogicalW(String x, String y);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		public static extern bool GetIconInfo(IntPtr hIcon, ref IconInfo pIconInfo);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr CreateIconIndirect([In] ref IconInfo icon);

		[DllImport("user32.dll")]
		public static extern int GetSystemMetrics(SystemMetric smIndex);

		[DllImport("User32.dll")]
		public static extern bool ReleaseCapture();
		#endregion

		public static void MakeTopMost(IntPtr handle)
		{
			SetWindowPos(handle, HWND_TOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
		}

		public static void MakeNormal(IntPtr handle)
		{
			SetWindowPos(handle, HWND_NOTOPMOST, 0, 0, 0, 0, TOPMOST_FLAGS);
		}

		public static void MakeTop(IntPtr handle)
		{
			SetWindowPos(handle, HWND_TOP, 0, 0, 0, 0, TOPMOST_FLAGS);
		}

		public static void MakeBottom(IntPtr handle)
		{
			SetWindowPos(handle, HWND_BOTTOM, 0, 0, 0, 0, TOPMOST_FLAGS);
		}

		public static bool InternalCheckIsWow64()
		{
			if ((Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor >= 1) ||
				Environment.OSVersion.Version.Major >= 6)
			{
				using (Process p = Process.GetCurrentProcess())
				{
					bool retVal;
					if (!IsWow64Process(p.Handle, out retVal))
					{
						return false;
					}
					return retVal;
				}
			}
			else
			{
				return false;
			}
		}
	}

	public struct IconInfo
	{
		public bool IsIcon;
		public int xHotspot;
		public int yHotspot;
		public IntPtr MaskBitmap;
		public IntPtr ColorBitmap;
	}

	public enum SystemMetric
	{
		SM_CXSCREEN = 0,  // 0x00
		SM_CYSCREEN = 1,  // 0x01
		SM_CXVSCROLL = 2,  // 0x02
		SM_CYHSCROLL = 3,  // 0x03
		SM_CYCAPTION = 4,  // 0x04
		SM_CXBORDER = 5,  // 0x05
		SM_CYBORDER = 6,  // 0x06
		SM_CXDLGFRAME = 7,  // 0x07
		SM_CXFIXEDFRAME = 7,  // 0x07
		SM_CYDLGFRAME = 8,  // 0x08
		SM_CYFIXEDFRAME = 8,  // 0x08
		SM_CYVTHUMB = 9,  // 0x09
		SM_CXHTHUMB = 10, // 0x0A
		SM_CXICON = 11, // 0x0B
		SM_CYICON = 12, // 0x0C
		SM_CXCURSOR = 13, // 0x0D
		SM_CYCURSOR = 14, // 0x0E
		SM_CYMENU = 15, // 0x0F
		SM_CXFULLSCREEN = 16, // 0x10
		SM_CYFULLSCREEN = 17, // 0x11
		SM_CYKANJIWINDOW = 18, // 0x12
		SM_MOUSEPRESENT = 19, // 0x13
		SM_CYVSCROLL = 20, // 0x14
		SM_CXHSCROLL = 21, // 0x15
		SM_DEBUG = 22, // 0x16
		SM_SWAPBUTTON = 23, // 0x17
		SM_CXMIN = 28, // 0x1C
		SM_CYMIN = 29, // 0x1D
		SM_CXSIZE = 30, // 0x1E
		SM_CYSIZE = 31, // 0x1F
		SM_CXSIZEFRAME = 32, // 0x20
		SM_CXFRAME = 32, // 0x20
		SM_CYSIZEFRAME = 33, // 0x21
		SM_CYFRAME = 33, // 0x21
		SM_CXMINTRACK = 34, // 0x22
		SM_CYMINTRACK = 35, // 0x23
		SM_CXDOUBLECLK = 36, // 0x24
		SM_CYDOUBLECLK = 37, // 0x25
		SM_CXICONSPACING = 38, // 0x26
		SM_CYICONSPACING = 39, // 0x27
		SM_MENUDROPALIGNMENT = 40, // 0x28
		SM_PENWINDOWS = 41, // 0x29
		SM_DBCSENABLED = 42, // 0x2A
		SM_CMOUSEBUTTONS = 43, // 0x2B
		SM_SECURE = 44, // 0x2C
		SM_CXEDGE = 45, // 0x2D
		SM_CYEDGE = 46, // 0x2E
		SM_CXMINSPACING = 47, // 0x2F
		SM_CYMINSPACING = 48, // 0x30
		SM_CXSMICON = 49, // 0x31
		SM_CYSMICON = 50, // 0x32
		SM_CYSMCAPTION = 51, // 0x33
		SM_CXSMSIZE = 52, // 0x34
		SM_CYSMSIZE = 53, // 0x35
		SM_CXMENUSIZE = 54, // 0x36
		SM_CYMENUSIZE = 55, // 0x37
		SM_ARRANGE = 56, // 0x38
		SM_CXMINIMIZED = 57, // 0x39
		SM_CYMINIMIZED = 58, // 0x3A
		SM_CXMAXTRACK = 59, // 0x3B
		SM_CYMAXTRACK = 60, // 0x3C
		SM_CXMAXIMIZED = 61, // 0x3D
		SM_CYMAXIMIZED = 62, // 0x3E
		SM_NETWORK = 63, // 0x3F
		SM_CLEANBOOT = 67, // 0x43
		SM_CXDRAG = 68, // 0x44
		SM_CYDRAG = 69, // 0x45
		SM_SHOWSOUNDS = 70, // 0x46
		SM_CXMENUCHECK = 71, // 0x47
		SM_CYMENUCHECK = 72, // 0x48
		SM_SLOWMACHINE = 73, // 0x49
		SM_MIDEASTENABLED = 74, // 0x4A
		SM_MOUSEWHEELPRESENT = 75, // 0x4B
		SM_XVIRTUALSCREEN = 76, // 0x4C
		SM_YVIRTUALSCREEN = 77, // 0x4D
		SM_CXVIRTUALSCREEN = 78, // 0x4E
		SM_CYVIRTUALSCREEN = 79, // 0x4F
		SM_CMONITORS = 80, // 0x50
		SM_SAMEDISPLAYFORMAT = 81, // 0x51
		SM_IMMENABLED = 82, // 0x52
		SM_CXFOCUSBORDER = 83, // 0x53
		SM_CYFOCUSBORDER = 84, // 0x54
		SM_TABLETPC = 86, // 0x56
		SM_MEDIACENTER = 87, // 0x57
		SM_STARTER = 88, // 0x58
		SM_SERVERR2 = 89, // 0x59
		SM_MOUSEHORIZONTALWHEELPRESENT = 91, // 0x5B
		SM_CXPADDEDBORDER = 92, // 0x5C
		SM_DIGITIZER = 94, // 0x5E
		SM_MAXIMUMTOUCHES = 95, // 0x5F

		SM_REMOTESESSION = 0x1000, // 0x1000
		SM_SHUTTINGDOWN = 0x2000, // 0x2000
		SM_REMOTECONTROL = 0x2001, // 0x2001


		SM_CONVERTABLESLATEMODE = 0x2003,
		SM_SYSTEMDOCKED = 0x2004,
	}
}