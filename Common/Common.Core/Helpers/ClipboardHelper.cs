using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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
			if (!Clipboard.ContainsImage()) return null;
			return Clipboard.GetImage();
		}
	}
}
