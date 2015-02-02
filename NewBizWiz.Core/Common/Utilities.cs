using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using DevExpress.XtraEditors;
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

		private string _title;

		public string Title
		{
			get { return _title ?? SettingsManager.Instance.DashboardName; }
			set { _title = value; }
		}

		public void ShowWarning(string text)
		{
			MessageBox.Show(text, Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public DialogResult ShowWarningQuestion(string text, params object[] args)
		{
			return MessageBox.Show(String.Format(text, args), Title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		}

		public void ShowInformation(string text)
		{
			MessageBox.Show(text, Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
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
			var assemblyTypes = new List<Type>();
			foreach (var assembly in assemblies)
			{
				assemblyTypes.Clear();
				try
				{
					assemblyTypes.AddRange(assembly.GetTypes());
				}
				catch { continue; }
				if (!assemblyTypes.Any()) continue;
				var targetObject = FindControlInTypes(baseType, intendedClass, assemblyTypes, parameters);
				if (targetObject == null) continue;
				return targetObject;
			}
			return null;
		}
		#endregion

		public void PutImageToClipboard(Image imageData)
		{
			if (imageData == null) return;
			using (var stream = new MemoryStream())
			{
				imageData.Save(stream, ImageFormat.Png);
				var data = new DataObject("PNG", stream);
				Clipboard.Clear();
				Clipboard.SetDataObject(data, true);
			}
		}

		public Image GetImageFormClipboard()
		{
			if (!Clipboard.ContainsData("PNG")) return null;
			return Image.FromStream((Stream)Clipboard.GetData("PNG"));
		}
	}

	public static class Extensions
	{
		public static Point GetCenter(this Rectangle control)
		{
			return new Point(control.X + (control.Width / 2), control.Y + (control.Height / 2));
		}

		public static Point GetOffset(this Point point, int x, int y)
		{
			return new Point(point.X + x, point.Y + y);
		}

		public static TextGroup Join(this IEnumerable<ITextItem> textItems, string separator = "", string borderLeft = "", string borderRight = "")
		{
			var textGroup = new TextGroup(separator, borderLeft, borderRight);
			textGroup.Items.AddRange(textItems);
			return textGroup;
		}

		public static Image Resize(this Image image, Size size)
		{
			var originalWidth = image.Width;
			var originalHeight = image.Height;
			var percentWidth = (float)size.Width / originalWidth;
			var percentHeight = (float)size.Height / originalHeight;
			var percent = percentHeight < percentWidth ? percentHeight : percentWidth;
			var newWidth = (int)(originalWidth * percent);
			var newHeight = (int)(originalHeight * percent);
			Image newImage = new Bitmap(newWidth, newHeight);
			using (var graphicsHandle = Graphics.FromImage(newImage))
			{
				graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphicsHandle.DrawImage(image, 0, 0, newWidth, newHeight);
			}
			return newImage;
		}

		public static Image DrawBorder(this Image image)
		{
			const int borderWidth = 1;
			var originalWidth = image.Width;
			var originalHeight = image.Height;
			var newWidth = (originalWidth + borderWidth * 4);
			var newHeight = (originalHeight + borderWidth * 4);
			Image newImage = new Bitmap(newWidth, newHeight);
			using (var graphicsHandle = Graphics.FromImage(newImage))
			using (var pen = new Pen(Color.DimGray, borderWidth))
			{
				graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphicsHandle.DrawImage(image, borderWidth, borderWidth);
				graphicsHandle.DrawRectangle(pen, 0, 0, originalWidth, originalHeight);
			}
			return newImage;
		}

		public static Byte[] ToByteArray(this Image image)
		{
			var bmp = image as Bitmap;
			if (bmp == null) return null;
			var m = new MemoryStream();
			bmp.Save(m, GetImageFormat(bmp));
			return m.ToArray();
		}

		public static Image ToImage(this Byte[] array)
		{
			return Image.FromStream(new MemoryStream(array));
		}

		public static ImageFormat GetImageFormat(Bitmap bitmap)
		{
			var img = bitmap.RawFormat;
			if (img.Equals(ImageFormat.Jpeg))
				return ImageFormat.Jpeg;
			if (img.Equals(ImageFormat.Bmp))
				return ImageFormat.Bmp;
			if (img.Equals(ImageFormat.Png))
				return ImageFormat.Png;
			if (img.Equals(ImageFormat.Emf))
				return ImageFormat.Emf;
			if (img.Equals(ImageFormat.Exif))
				return ImageFormat.Exif;
			if (img.Equals(ImageFormat.Gif))
				return ImageFormat.Gif;
			if (img.Equals(ImageFormat.Icon))
				return ImageFormat.Icon;
			if (img.Equals(ImageFormat.MemoryBmp))
				return ImageFormat.MemoryBmp;
			if (img.Equals(ImageFormat.Tiff))
				return ImageFormat.Tiff;
			return ImageFormat.Wmf;
		}

		public static XmlNode GetXmlNode(this XElement element)
		{
			using (var xmlReader = element.CreateReader())
			{
				var xmlDoc = new XmlDocument();
				xmlDoc.Load(xmlReader);
				return xmlDoc;
			}
		}

		public static bool Compare(this Image firstImage, Image secondImage)
		{
			var firstBitmap = firstImage as Bitmap;
			var secondBitmap = secondImage as Bitmap;
			if (firstBitmap == null || secondBitmap == null) return false;
			for (var x = 0; x < firstBitmap.Width; x++)
			{
				if (x >= secondBitmap.Width) return false;
				for (var y = 0; y < firstBitmap.Height; y++)
				{
					if (y >= secondBitmap.Height) return false;
					if (firstBitmap.GetPixel(x, y) != secondBitmap.GetPixel(x, y)) return false;
				}
			}
			return true;
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