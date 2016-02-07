using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace Asa.Common.Core.Extensions
{
	public static class GraphicExtensions
	{
		private static ImageFormat GetImageFormat(Image image)
		{
			try
			{
				var img = image.RawFormat;
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
			catch
			{
				return ImageFormat.Png;
			}
		}

		public static ImageCodecInfo GetEncoder(Image image)
		{
			var format = GetImageFormat(image);
			var codecs = ImageCodecInfo.GetImageDecoders();
			var encoder = codecs.FirstOrDefault(codec => codec.FormatID == format.Guid) ?? codecs.FirstOrDefault();
			return encoder;
		}

		public static EncoderParameters GetEncoderParameters()
		{
			var encoderParameters = new EncoderParameters(1);
			encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 0L); ;
			return encoderParameters;
		}

		public static Bitmap MakeGrayscale(this Bitmap original)
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
			bmp.Save(m, GetImageFormat((Image)bmp.Clone()));
			return m.ToArray();
		}

		public static Image ToImage(this Byte[] array)
		{
			return Image.FromStream(new MemoryStream(array));
		}

		public static Point GetCenter(this Rectangle control)
		{
			return new Point(control.X + (control.Width / 2), control.Y + (control.Height / 2));
		}

		public static Point GetOffset(this Point point, int x, int y)
		{
			return new Point(point.X + x, point.Y + y);
		}

		public static Image ResizeImage(int newSize, Image originalImage)
		{
			if (originalImage.Width <= newSize)
				newSize = originalImage.Width;

			var newHeight = originalImage.Height * newSize / originalImage.Width;

			if (newHeight > newSize)
			{
				newSize = originalImage.Width * newSize / originalImage.Height;
				newHeight = newSize;
			}
			return originalImage.GetThumbnailImage(newSize, newHeight, null, IntPtr.Zero);
		}
	}
}
