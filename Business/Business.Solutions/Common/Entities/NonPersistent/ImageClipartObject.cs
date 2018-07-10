using System.Drawing;
using System.IO;
using Asa.Business.Solutions.Common.Enums;
using Asa.Common.Core.Objects.Images;

namespace Asa.Business.Solutions.Common.Entities.NonPersistent
{
	public class ImageClipartObject : ClipartObject
	{
		public override ClipartObjectType Type => ClipartObjectType.Image;

		public Image Image { get; set; }

		public static ImageClipartObject FromFile(string filePath)
		{
			var tempFile = Path.GetTempFileName();
			File.Copy(filePath, tempFile, true);
			return new ImageClipartObject
			{
				Name = Path.GetFileNameWithoutExtension(filePath),
				Image = Image.FromFile(filePath)
			};
		}

		public static ImageClipartObject FromImageSource(ImageSource imageSource)
		{
			return new ImageClipartObject
			{
				Name = imageSource.Name,
				Image = imageSource.OriginalImage ?? imageSource.BigImage
			};
		}

		public static ImageClipartObject FromImage(Image image)
		{
			return new ImageClipartObject
			{
				Image = image
			};
		}
	}
}
