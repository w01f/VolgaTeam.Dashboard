﻿using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Interfaces;
using Asa.Common.Core.Objects.RemoteStorage;
using Newtonsoft.Json;

namespace Asa.Common.Core.Objects.Images
{
	public class ImageSource : IJsonCloneable<ImageSource>
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

		public Guid Identifier { get; set; }
		public bool IsDefault { get; set; }
		public Image OriginalImage { get; set; }
		public Image BigImage { get; set; }
		public Image SmallImage { get; set; }
		public Image TinyImage { get; set; }
		public Image XtraTinyImage { get; set; }
		public string Name { get; set; }
		public string FileName { get; set; }
		[JsonIgnore]
		public string OutputFilePath { get; set; }
		[JsonIgnore]
		public bool ContainsData => XtraTinyImage != null;

		public ImageSource()
		{
			Identifier = Guid.NewGuid();
		}

		public void Dispose()
		{
			OriginalImage?.Dispose();
			OriginalImage = null;
			BigImage?.Dispose();
			BigImage = null;
			SmallImage?.Dispose();
			SmallImage = null;
			TinyImage?.Dispose();
			TinyImage = null;
			XtraTinyImage?.Dispose();
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
				imageSource.OriginalImage = (Image)image.Clone();
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
			var jsonContent = new Regex("<ImageSource>(.*?)</ImageSource>", RegexOptions.IgnoreCase)
				.Match(encoded).Groups
				.OfType<Capture>()
				.ElementAtOrDefault(1)?.Value;
			if (jsonContent != null)
				encoded = jsonContent;
			var imageSource = CloneHelper.Deserialize<ImageSource, ImageSource>(encoded);
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

		public void AfterClone(ImageSource source, bool fullClone = true) { }
	}
}
