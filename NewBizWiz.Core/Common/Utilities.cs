using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using NewBizWiz.Core.Interop;
using Application = Microsoft.Office.Interop.PowerPoint.Application;

namespace NewBizWiz.Core.Common
{
	public class Utilities
	{
		private static readonly Utilities _instance = new Utilities();
		private Utilities() { }
		public static Utilities Instance
		{
			get { return _instance; }
		}

		public void ShowWarning(string text)
		{
			MessageBox.Show(text, SettingsManager.Instance.DashboardName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public DialogResult ShowWarningQuestion(string text)
		{
			return MessageBox.Show(text, SettingsManager.Instance.DashboardName, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		}

		public void ShowInformation(string text)
		{
			MessageBox.Show(text, SettingsManager.Instance.DashboardName, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		public void ActivateForm(IntPtr handle, bool maximized, bool topMost)
		{
			WinAPIHelper.ShowWindow(handle, maximized ? WindowShowStyle.ShowMaximized : WindowShowStyle.ShowNormal);
			uint lpdwProcessId = 0;
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
			WinAPIHelper.SetForegroundWindow(handle);
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
			if (topMost)
				WinAPIHelper.MakeTopMost(handle);
			else
				WinAPIHelper.MakeNormal(handle);
		}

		public void MinimizeForm(IntPtr handle)
		{
			var form = Control.FromHandle(handle) as Form;
			if (form != null)
				form.WindowState = FormWindowState.Minimized;
		}

		public void ActivatePowerPoint(Application powerPoint)
		{
			if (powerPoint != null)
			{
				var powerPointHandle = new IntPtr(powerPoint.HWND);
				WinAPIHelper.ShowWindow(powerPointHandle, WindowShowStyle.ShowMaximized);
				uint lpdwProcessId = 0;
				WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
				WinAPIHelper.SetForegroundWindow(powerPointHandle);
				WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
			}
		}

		public void ActivateMiniBar()
		{
			IntPtr minibarHandle = RegistryHelper.MinibarHandle;
			if (minibarHandle.ToInt32() == 0)
			{
				Process[] processList = Process.GetProcesses();
				foreach (Process process in processList.Where(x => x.ProcessName.Contains("MiniBar")))
				{
					if (process.MainWindowHandle.ToInt32() != 0)
					{
						minibarHandle = process.MainWindowHandle;
						break;
					}
				}
			}
			if (minibarHandle.ToInt32() != 0)
			{
				uint lpdwProcessId = 0;
				WinAPIHelper.MakeTopMost(minibarHandle);
				WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
				WinAPIHelper.SetForegroundWindow(minibarHandle);
				WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
			}
		}

		public void ReleaseComObject(object o)
		{
			try
			{
				Marshal.ReleaseComObject(o);
				o = null;
			}
			catch { }
		}

		public string GetLetterByDigit(int digit)
		{
			switch (digit)
			{
				case 1:
					return "A";
				case 2:
					return "B";
				case 3:
					return "C";
				case 4:
					return "D";
				case 5:
					return "E";
				case 6:
					return "F";
				default:
					return "";
			}
		}

		public Bitmap MakeGrayscale(Bitmap original)
		{
			var newBitmap = new Bitmap(original.Width, original.Height);

			//get a graphics object from the new image
			Graphics g = Graphics.FromImage(newBitmap);

			//create the grayscale ColorMatrix
			var colorMatrix = new ColorMatrix(
				new[]
				{
					new[] { .3f, .3f, .3f, 0, 0 },
					new[] { .59f, .59f, .59f, 0, 0 },
					new[] { .11f, .11f, .11f, 0, 0 },
					new float[] { 0, 0, 0, 1, 0 },
					new float[] { 0, 0, 0, 0, 1 }
				});

			//create some image attributes
			var attributes = new ImageAttributes();

			//set the color matrix attribute
			attributes.SetColorMatrix(colorMatrix);

			//draw the original image on the new image
			//using the grayscale color matrix
			g.DrawImage(original, new Rectangle(0, 0, original.Width, original.Height),
						0, 0, original.Width, original.Height, GraphicsUnit.Pixel, attributes);

			//dispose the Graphics object
			g.Dispose();
			return newBitmap;
		}

		#region Select All in Editor Handlers
		private bool enter;
		private bool needSelect;

		public void Editor_Enter(object sender, EventArgs e)
		{
			enter = true;
		}

		public void Editor_MouseUp(object sender, MouseEventArgs e)
		{
			if (needSelect)
			{
				(sender as BaseEdit).SelectAll();
			}
			ResetEnterFlag();
		}

		public void Editor_MouseDown(object sender, MouseEventArgs e)
		{
			needSelect = enter;
		}

		private void ResetEnterFlag()
		{
			enter = false;
		}
		#endregion

		#region Picture Box Clicks Habdlers
		/// <summary>
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		public void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		public void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion
	}
}