using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace NewBizWiz.Core.Common
{
	public class ImageSource
	{
		public Image BigImage { get; set; }
		public Image SmallImage { get; set; }
		public Image TinyImage { get; set; }
		public Image XtraTinyImage { get; set; }

		public bool ContainsData
		{
			get { return XtraTinyImage != null; }
		}

		private string _encodedBigImage;
		public string EncodedBigImage
		{
			get
			{
				if (String.IsNullOrEmpty(_encodedBigImage))
				{
					TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
					_encodedBigImage = Convert.ToBase64String((byte[])converter.ConvertTo(BigImage, typeof(byte[])));
				}
				return _encodedBigImage;
			}
		}

		public string Serialize()
		{
			TypeConverter converter = TypeDescriptor.GetConverter(typeof(Bitmap));
			var result = new StringBuilder();
			result.Append("<BigImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(BigImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</BigImage>");
			result.Append("<SmallImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(SmallImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</SmallImage>");
			result.Append("<TinyImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(TinyImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</TinyImage>");
			result.Append("<XtraTinyImage>" + Convert.ToBase64String((byte[])converter.ConvertTo(XtraTinyImage, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + "</XtraTinyImage>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
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
				}
			}
		}

		public ImageSource Clone()
		{
			var result = new ImageSource();
			result.BigImage = BigImage;
			result.SmallImage = SmallImage;
			result.TinyImage = TinyImage;
			result.XtraTinyImage = XtraTinyImage;
			return result;
		}
	}
}
