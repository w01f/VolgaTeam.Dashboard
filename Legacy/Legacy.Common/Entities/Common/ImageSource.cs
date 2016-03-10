using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Xml;

namespace Asa.Legacy.Common.Entities.Common
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
	}
}
