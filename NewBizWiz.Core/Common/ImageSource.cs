using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Asa.Core.Common
{
	public class ImageSource
	{
		public const string ImageFileExtension = ".png";

		public const string BigImageFolderName = "Big Logos";
		public const string SmallImageFolderName = "Small Logos";
		public const string TinyImageFolderName = "Tiny Logos";
		public const string XtraTinyImageFolderName = "xtra tiny logos";

		public const decimal BigHeight = 146;
		public const decimal SmallHeight = 75;
		public const decimal TinyHeight = 58;
		public const decimal XtraTinyHeight = 41;
		public const decimal BigWidth = 321;
		public const decimal SmallWidth = 164;
		public const decimal TinyWidth = 128;
		public const decimal XtraTinyWidth = 90;

		public bool IsDefault { get; set; }
		public Image BigImage { get; set; }
		public Image SmallImage { get; set; }
		public Image TinyImage { get; set; }
		public Image XtraTinyImage { get; set; }
		public string Name { get; set; }
		public string FileName { get; set; }
		public string OutputFilePath { get; set; }

		public bool ContainsData
		{
			get { return XtraTinyImage != null; }
		}

		private string _encodedBigImage;
		public string EncodedBigImage
		{
			get
			{
				if (!String.IsNullOrEmpty(_encodedBigImage)) return _encodedBigImage;
				var converter = TypeDescriptor.GetConverter(typeof(Bitmap));
				_encodedBigImage = Convert.ToBase64String((byte[])converter.ConvertTo(BigImage, typeof(byte[]))).Trim();
				return _encodedBigImage;
			}
		}

		public string Serialize()
		{
			var converter = TypeDescriptor.GetConverter(typeof(Bitmap));
			var result = new StringBuilder();
			result.Append("<IsDefault>" + IsDefault + "</IsDefault>");
			result.Append("<BigImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(BigImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</BigImage>");
			result.Append("<SmallImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(SmallImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</SmallImage>");
			result.Append("<TinyImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(TinyImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</TinyImage>");
			result.Append("<XtraTinyImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(XtraTinyImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</XtraTinyImage>");
			if (!String.IsNullOrEmpty(Name))
				result.Append("<Name>" + Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</Name>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "IsDefault":
						bool tempBool;
						if (bool.TryParse(childNode.InnerText, out tempBool))
							IsDefault = tempBool;
						break;
					case "BigImage":
						if (string.IsNullOrEmpty(childNode.InnerText))
							BigImage = null;
						else
						{
							BigImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
							_encodedBigImage = childNode.InnerText;
						}
						break;
					case "SmallImage":
						if (string.IsNullOrEmpty(childNode.InnerText))
							SmallImage = null;
						else
							SmallImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
					case "TinyImage":
						if (string.IsNullOrEmpty(childNode.InnerText))
							TinyImage = null;
						else
							TinyImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
					case "XtraTinyImage":
						if (string.IsNullOrEmpty(childNode.InnerText))
							TinyImage = null;
						else
							XtraTinyImage = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
					case "Name":
						Name = childNode.InnerText;
						break;
				}
			}
		}

		public void Deserialize(string content)
		{
			var document = new XmlDocument();
			document.LoadXml(String.Format("<Source>{0}</Source>", content));
			Deserialize(document.SelectSingleNode("Source"));
		}

		public ImageSource Clone()
		{
			var result = new ImageSource();
			result.IsDefault = IsDefault;
			if (BigImage != null && SmallImage != null && TinyImage != null && XtraTinyImage != null)
			{
				result.BigImage = BigImage.Clone() as Image;
				result.SmallImage = SmallImage.Clone() as Image;
				result.TinyImage = TinyImage.Clone() as Image;
				result.XtraTinyImage = XtraTinyImage.Clone() as Image;
			}
			result.Name = Name;
			return result;
		}

		public void Dispose()
		{
			if (BigImage != null)
				BigImage.Dispose();
			BigImage = null;
			if (SmallImage != null)
				SmallImage.Dispose();
			SmallImage = null;
			if (TinyImage != null)
				TinyImage.Dispose();
			TinyImage = null;
			if (XtraTinyImage != null)
				XtraTinyImage.Dispose();
			XtraTinyImage = null;
		}

		public void PrepareOutputFile()
		{
			OutputFilePath = Path.GetTempFileName();
			BigImage.Save(OutputFilePath);
		}

		public static ImageSource FromImage(Image image)
		{
			var imageSource = new ImageSource();
			if (image != null)
			{
				imageSource.BigImage = image.Resize(new Size((Int32)BigWidth, (Int32)BigHeight));
				imageSource.SmallImage = image.Resize(new Size((Int32)SmallWidth, (Int32)SmallHeight));
				imageSource.TinyImage = image.Resize(new Size((Int32)TinyWidth, (Int32)TinyHeight));
				imageSource.XtraTinyImage = image.Resize(new Size((Int32)XtraTinyWidth, (Int32)XtraTinyHeight));
			};
			return imageSource;
		}

		public static ImageSource FromString(string encoded)
		{
			if (String.IsNullOrEmpty(encoded)) return null;
			var imageSource = new ImageSource();
			imageSource.Deserialize(encoded);
			return imageSource;
		}

		public static ImageSource FromFolder(StorageDirectory folder, string imageName)
		{
			var bigImageFile = new StorageFile(folder.RelativePathParts.Merge(new[] { BigImageFolderName, String.Format("{0}{1}", imageName, ImageFileExtension) }));
			var smallImageFile = new StorageFile(folder.RelativePathParts.Merge(new[] { SmallImageFolderName, String.Format("{0}2{1}", imageName, ImageFileExtension) }));
			var tinyImageFile = new StorageFile(folder.RelativePathParts.Merge(new[] { TinyImageFolderName, String.Format("{0}3{1}", imageName, ImageFileExtension) }));
			var xtraTinyImageFile = new StorageFile(folder.RelativePathParts.Merge(new[] { XtraTinyImageFolderName, String.Format("{0}4{1}", imageName, ImageFileExtension) }));

			if (!bigImageFile.ExistsLocal() ||
				!smallImageFile.ExistsLocal() ||
				!tinyImageFile.ExistsLocal() ||
				!xtraTinyImageFile.ExistsLocal())
				return null;

			var imageSource = new ImageSource();
			imageSource.Name = imageName.ToLower().StartsWith("!default") ? imageName.Substring(1) : imageName;
			imageSource.FileName = bigImageFile.LocalPath;
			imageSource.IsDefault = imageName.ToLower().Contains("default");
			imageSource.BigImage = new Bitmap(bigImageFile.LocalPath);
			imageSource.SmallImage = new Bitmap(smallImageFile.LocalPath);
			imageSource.TinyImage = new Bitmap(tinyImageFile.LocalPath);
			imageSource.XtraTinyImage = new Bitmap(xtraTinyImageFile.LocalPath);

			return imageSource;
		}
	}

	public class ImageSourceGroup
	{
		private readonly StorageDirectory _root;

		public string Name { get; set; }
		public int Order { get; set; }

		public bool IsDefault
		{
			get { return Order < 0; }
		}

		public List<ImageSource> Images { get; private set; }

		public EventHandler<EventArgs> OnDataChanged;

		public ImageSourceGroup(StorageDirectory root)
		{
			_root = root;
			Images = new List<ImageSource>();
		}

		public void LoadImages()
		{
			Images.Clear();

			var bigImageFolder = new StorageDirectory(_root.RelativePathParts.Merge(ImageSource.BigImageFolderName));
			if (!bigImageFolder.ExistsLocal()) return;


			foreach (var file in bigImageFolder.GetLocalFiles().Where(file => file.Extension == ImageSource.ImageFileExtension))
			{
				var imageSource = ImageSource.FromFolder(_root, Path.GetFileNameWithoutExtension(file.LocalPath));
				if (imageSource != null)
					Images.Add(imageSource);
			}
		}
	}
}
