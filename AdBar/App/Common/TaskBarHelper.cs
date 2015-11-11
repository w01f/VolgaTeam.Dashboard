using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Asa.Bar.App.Common
{
	class TaskBarHelper
	{
		private enum ABE : uint
		{
			Left = 0,
			Top = 1,
			Right = 2,
			Bottom = 3
		}

		private enum ABM : uint
		{
			New = 0x00000000,
			Remove = 0x00000001,
			QueryPos = 0x00000002,
			SetPos = 0x00000003,
			GetState = 0x00000004,
			GetTaskbarPos = 0x00000005,
			Activate = 0x00000006,
			GetAutoHideBar = 0x00000007,
			SetAutoHideBar = 0x00000008,
			WindowPosChanged = 0x00000009,
			SetState = 0x0000000A,
		}

		private static class ABS
		{
			public const int Autohide = 0x0000001;
			public const int AlwaysOnTop = 0x0000002;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct APPBARDATA
		{
			public uint cbSize;
			public IntPtr hWnd;
			public readonly uint uCallbackMessage;
			public readonly ABE uEdge;
			public RECT rc;
			public readonly int lParam;
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct RECT
		{
			public readonly int left;
			public readonly int top;
			public readonly int right;
			public readonly int bottom;
		}

		private static class Shell32
		{
			[DllImport("shell32.dll", SetLastError = true)]
			public static extern IntPtr SHAppBarMessage(ABM dwMessage, [In] ref APPBARDATA pData);
		}

		public sealed class Taskbar
		{
			private const string ClassName = "Shell_TrayWnd", SecondaryClassName = "Shell_SecondaryTrayWnd";

			public Taskbar(bool secondary)
			{
				Handle = User32.FindWindow(!secondary ? ClassName : SecondaryClassName, null);

				if (Handle == IntPtr.Zero)
					return;

				var data = new APPBARDATA { cbSize = (uint)Marshal.SizeOf(typeof(APPBARDATA)), hWnd = Handle };
				IntPtr result = Shell32.SHAppBarMessage(ABM.GetTaskbarPos, ref data);

				if (result == IntPtr.Zero)
					throw new InvalidOperationException();

				Position = (TaskbarPosition)data.uEdge;
				Bounds = Rectangle.FromLTRB(data.rc.left, data.rc.top, data.rc.right, data.rc.bottom);

				data.cbSize = (uint)Marshal.SizeOf(typeof(APPBARDATA));
				result = Shell32.SHAppBarMessage(ABM.GetState, ref data);
				int state = result.ToInt32();
				AlwaysOnTop = (state & ABS.AlwaysOnTop) == ABS.AlwaysOnTop;
				AutoHide = (state & ABS.Autohide) == ABS.Autohide;

				var lpRect = new RECT();
				User32.GetWindowRect(Handle, ref lpRect);
				VisibleBounds = Rectangle.FromLTRB(lpRect.left, lpRect.top, lpRect.right, lpRect.bottom);

				/*Debug.WriteLine(this.Bounds);
                Debug.WriteLine(lpRect.top + "-------------------");*/
			}

			public Rectangle VisibleBounds { get; private set; }

			public Rectangle Bounds { get; private set; }

			public TaskbarPosition Position { get; private set; }

			public Point Location
			{
				get { return VisibleBounds.Location; }
			}

			public Size Size
			{
				get { return Bounds.Size; }
			}

			//Always returns false under Windows 7
			public bool AlwaysOnTop { get; private set; }
			public bool AutoHide { get; private set; }

			public IntPtr Handle { get; private set; }
		}

		internal enum TaskbarPosition
		{
			Unknown = -1,
			Left,
			Top,
			Right,
			Bottom,
		}

		private static class User32
		{
			[DllImport("user32.dll", SetLastError = true)]
			public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

			[DllImport("user32.dll")]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);
		}
	}
}