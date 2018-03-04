using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Asa.Common.Core.Helpers
{
	public static class ClipboardHelper
	{
		public static void PutPngToClipboard(Image imageData)
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

		public static Image GetPngFormClipboard()
		{
			if (!Clipboard.ContainsData("PNG")) return null;
			return Image.FromStream((Stream)Clipboard.GetData("PNG"));
		}

		public static Image GetImageFormClipboard()
		{
			if (Clipboard.GetDataObject() == null) return null;
			if (Clipboard.GetDataObject().GetDataPresent(DataFormats.Dib))
			{
				var dib = ((MemoryStream)Clipboard.GetData(DataFormats.Dib)).ToArray();
				var width = BitConverter.ToInt32(dib, 4);
				var height = BitConverter.ToInt32(dib, 8);
				var bpp = BitConverter.ToInt16(dib, 14);
				if (bpp == 32)
				{
					var gch = GCHandle.Alloc(dib, GCHandleType.Pinned);
					Bitmap bmp = null;
					try
					{
						var ptr = new IntPtr((long)gch.AddrOfPinnedObject() + 40);
						bmp = new Bitmap(width, height, width * 4, PixelFormat.Format32bppArgb, ptr);
						bmp.RotateFlip(RotateFlipType.Rotate180FlipX);
						return new Bitmap(bmp);
					}
					finally
					{
						gch.Free();
						bmp?.Dispose();
					}
				}
			}
			return Clipboard.ContainsImage() ? Clipboard.GetImage() : null;
		}
	}
}
