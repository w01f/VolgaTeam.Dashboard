using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Microsoft.Office.Interop.PowerPoint;
using NewBizWiz.Core.Interop;
using Application = Microsoft.Office.Interop.PowerPoint.Application;
using Point = System.Drawing.Point;

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
			if (powerPoint == null) return;
			var powerPointHandle = new IntPtr(powerPoint.HWND);
			WinAPIHelper.ShowWindow(powerPointHandle, WindowShowStyle.ShowMaximized);
			uint lpdwProcessId;
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
			WinAPIHelper.SetForegroundWindow(powerPointHandle);
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
		}

		public void ActivateTaskbar()
		{
			var taskBarHandle = WinAPIHelper.FindWindow("Shell_traywnd", "");
			WinAPIHelper.ShowWindow(taskBarHandle, WindowShowStyle.Show);
			uint lpdwProcessId;
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), true);
			WinAPIHelper.SetForegroundWindow(taskBarHandle);
			WinAPIHelper.AttachThreadInput(WinAPIHelper.GetCurrentThreadId(), WinAPIHelper.GetWindowThreadProcessId(WinAPIHelper.GetForegroundWindow(), out lpdwProcessId), false);
		}

		public void ReleaseComObject(object o)
		{
			try
			{
				Marshal.FinalReleaseComObject(o);
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
			var g = Graphics.FromImage(newBitmap);

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

		public bool IsSmallScreen
		{
			get
			{
				return Screen.PrimaryScreen.Bounds.Width <= 1024;
			}
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

		#region Internet Browser Support
		private bool _chromeDefinded;
		private bool _firefoxDefinded;
		private bool _operaDefinded;
		private bool _chromeInstalled;
		private bool _firefoxInstalled;
		private bool _operaInstalled;

		public bool ChromeInstalled
		{
			get
			{
				if (!_chromeDefinded)
				{
					try
					{
						var process = new Process
						{
							StartInfo =
							{
								FileName = "chrome.exe",
								CreateNoWindow = true,
								WindowStyle = ProcessWindowStyle.Hidden
							}
						};
						process.Start();
						process.Kill();
						_chromeInstalled = true;
					}
					catch
					{
						_chromeInstalled = false;
					}
					_chromeDefinded = true;
				}
				return _chromeInstalled;
			}
		}

		public bool FirefoxInstalled
		{
			get
			{
				if (!_firefoxDefinded)
				{
					try
					{
						var process = new Process
						{
							StartInfo =
							{
								FileName = "firefox.exe",
								CreateNoWindow = true,
								WindowStyle = ProcessWindowStyle.Hidden
							}
						};
						process.Start();
						process.Kill();
						_firefoxInstalled = true;
					}
					catch
					{
						_firefoxInstalled = false;
					}
					_firefoxDefinded = true;
				}
				return _firefoxInstalled;
			}
		}

		public bool OperaInstalled
		{
			get
			{
				if (!_operaDefinded)
				{
					try
					{
						var process = new Process
						{
							StartInfo =
							{
								FileName = "opera.exe",
								CreateNoWindow = true,
								WindowStyle = ProcessWindowStyle.Hidden
							}
						};
						process.Start();
						process.Kill();
						_operaInstalled = true;
					}
					catch
					{
						_operaInstalled = false;
					}
					_operaDefinded = true;
				}
				return _operaInstalled;
			}
		}
		#endregion

		#region Reflection Support
		private static readonly Dictionary<string, Type> _sTypesDictionary = new Dictionary<string, Type>();

		private object FindControlInTypes(Type baseType, Type intendedClass, IEnumerable<Type> assemblyTypes, object[] parameters)
		{
			var lKey = baseType.FullName + intendedClass.FullName;
			if (_sTypesDictionary.ContainsKey(lKey))
				return Activator.CreateInstance(_sTypesDictionary[lKey], parameters);

			foreach (var type in assemblyTypes)
			{
				if (type != baseType && !type.IsSubclassOf(baseType) && (!baseType.IsInterface || type.GetInterface(baseType.Name) == null)) continue;
				var attrs = type.GetCustomAttributes(typeof(IntendForClassAttribute), false);
				foreach (IntendForClassAttribute attr in attrs)
				{
					if (attr.BusinessObjectClass != intendedClass && !intendedClass.IsSubclassOf(attr.BusinessObjectClass)) continue;
					_sTypesDictionary.Add(lKey, type);
					return Activator.CreateInstance(type, parameters);
				}
			}
			return null;
		}

		public object GetControlInstance(Type baseType, Type intendedClass, params object[] parameters)
		{
			var assemblies = AppDomain.CurrentDomain.GetAssemblies();
			return assemblies.Select(assembly =>
				FindControlInTypes(baseType, intendedClass, assembly.GetTypes(), parameters)).FirstOrDefault(control => control != null);
		}
		#endregion
	}

	public static class Extensions
	{
		public static Point GetCenter(this Rectangle control)
		{
			return new Point(control.X + (control.Width / 2), control.Y + (control.Height / 2));
		}

		public static TextGroup Join(this IEnumerable<ITextItem> textItems, string separator = "", string borderLeft = "", string borderRight = "")
		{
			var textGroup = new TextGroup(separator, borderLeft, borderRight);
			textGroup.Items.AddRange(textItems);
			return textGroup;
		}
	}

	[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
	public sealed class IntendForClassAttribute : Attribute
	{
		public Type BusinessObjectClass { get; private set; }

		public IntendForClassAttribute(Type businessObjectClass)
		{
			BusinessObjectClass = businessObjectClass;
		}
	}
}