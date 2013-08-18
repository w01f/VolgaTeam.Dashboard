using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace NewBizWiz.Core.Common
{
	public class ListManager
	{
		private static readonly ListManager _instance = new ListManager();
		private const string AdvertisersFileName = @"Advertisers.xml";
		private const string DecisionMakersFileName = @"DecisionMakers.xml";
		public const string DefaultBigLogoFileName = @"Default.png";
		public const string DefaultSmallLogoFileName = @"Default2.png";
		public const string DefaultTinyLogoFileName = @"Default3.png";
		private string LocalListFolder { get; set; }

		private ListManager()
		{
			Advertisers = new List<string>();
			DecisionMakers = new List<string>();
			Images = new List<ImageSource>();

			LocalListFolder = Path.Combine(String.Format(@"{0}\newlocaldirect.com\sync\Outgoing", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)), "AppID-" + SettingsManager.Instance.AppID.ToString(), @"User_lists");
			if (!Directory.Exists(LocalListFolder))
				Directory.CreateDirectory(LocalListFolder);

			string imageFolderPath = String.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Artwork\PRINT\", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			string folderPath = Path.Combine(imageFolderPath, "Big Logos");
			if (Directory.Exists(folderPath))
				BigImageFolder = new DirectoryInfo(folderPath);

			folderPath = Path.Combine(imageFolderPath, "Small Logos");
			if (Directory.Exists(folderPath))
				SmallImageFolder = new DirectoryInfo(folderPath);

			folderPath = Path.Combine(imageFolderPath, "Tiny Logos");
			if (Directory.Exists(folderPath))
				TinyImageFolder = new DirectoryInfo(folderPath);

			folderPath = Path.Combine(imageFolderPath, "Xtra Tiny Logos");
			if (Directory.Exists(folderPath))
				XtraTinyImageFolder = new DirectoryInfo(folderPath);

			LoadAdvertisers();
			LoadDecisionMakers();
			LoadImages();
		}

		public static ListManager Instance
		{
			get { return _instance; }
		}
		public DirectoryInfo BigImageFolder { get; set; }
		public DirectoryInfo SmallImageFolder { get; set; }
		public DirectoryInfo TinyImageFolder { get; set; }
		public DirectoryInfo XtraTinyImageFolder { get; set; }

		public List<string> Advertisers { get; set; }
		public List<string> DecisionMakers { get; set; }
		public List<ImageSource> Images { get; set; }

		private void LoadAdvertisers()
		{
			Advertisers.Clear();
			string listPath = Path.Combine(LocalListFolder, AdvertisersFileName);
			if (File.Exists(listPath))
			{
				var document = new XmlDocument();
				document.Load(listPath);

				XmlNode node = document.SelectSingleNode(@"/Advertisers");
				if (node != null)
				{
					foreach (XmlNode childeNode in node.ChildNodes)
					{
						if (!Advertisers.Contains(childeNode.InnerText))
							Advertisers.Add(childeNode.InnerText);
					}
				}
			}
		}

		private void LoadDecisionMakers()
		{
			DecisionMakers.Clear();
			string listPath = Path.Combine(LocalListFolder, DecisionMakersFileName);
			if (File.Exists(listPath))
			{
				var document = new XmlDocument();
				document.Load(listPath);

				XmlNode node = document.SelectSingleNode(@"/DecisionMakers");
				if (node != null)
				{
					foreach (XmlNode childeNode in node.ChildNodes)
					{
						if (!DecisionMakers.Contains(childeNode.InnerText))
							DecisionMakers.Add(childeNode.InnerText);
					}
				}
			}
		}

		private void LoadImages()
		{
			Images.Clear();
			foreach (FileInfo bigImageFile in BigImageFolder.GetFiles("*.png"))
			{
				string imageFileName = Path.GetFileNameWithoutExtension(bigImageFile.FullName);
				string imageFileExtension = Path.GetExtension(bigImageFile.FullName);

				string smallImageFilePath = Path.Combine(SmallImageFolder.FullName, string.Format("{0}2{1}", new string[] { imageFileName, imageFileExtension }));
				string tinyImageFilePath = Path.Combine(TinyImageFolder.FullName, string.Format("{0}3{1}", new string[] { imageFileName, imageFileExtension }));
				string xtraTinyImageFilePath = Path.Combine(XtraTinyImageFolder.FullName, string.Format("{0}4{1}", new string[] { imageFileName, imageFileExtension }));
				if (File.Exists(smallImageFilePath) && File.Exists(tinyImageFilePath) && File.Exists(xtraTinyImageFilePath))
				{
					ImageSource imageSource = new ImageSource();
					imageSource.BigImage = new Bitmap(bigImageFile.FullName);
					imageSource.SmallImage = new Bitmap(smallImageFilePath);
					imageSource.TinyImage = new Bitmap(tinyImageFilePath);
					imageSource.XtraTinyImage = new Bitmap(xtraTinyImageFilePath);
					this.Images.Add(imageSource);
				}
			}
		}

		public void SaveAdvertisers()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<Advertisers>");
			foreach (string advertiser in Advertisers)
				xml.AppendLine(@"<Advertiser>" + advertiser.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Advertiser>");
			xml.AppendLine(@"</Advertisers>");

			string userConfigurationPath = Path.Combine(LocalListFolder, AdvertisersFileName);
			using (var sw = new StreamWriter(userConfigurationPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		public void SaveDecisionMakers()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<DecisionMakers>");
			foreach (string decisionMaker in DecisionMakers)
				xml.AppendLine(@"<DecisionMaker>" + decisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
			xml.AppendLine(@"</DecisionMakers>");

			string userConfigurationPath = Path.Combine(LocalListFolder, DecisionMakersFileName);
			using (var sw = new StreamWriter(userConfigurationPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}
	}

	public class NameCodePair
	{
		public NameCodePair()
		{
			Name = string.Empty;
			Code = string.Empty;
		}

		public string Name { get; set; }
		public string Code { get; set; }

		public string Serialize()
		{
			var xml = new StringBuilder();

			xml.Append(@"<NameCodePair ");
			xml.Append("Name = \"" + Name.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.Append("Code = \"" + Code.Replace(@"&", "&#38;").Replace("\"", "&quot;") + "\" ");
			xml.AppendLine(@"/>");

			return xml.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlAttribute attribute in node.Attributes)
				switch (attribute.Name)
				{
					case "Name":
						Name = attribute.Value;
						break;
					case "Code":
						Code = attribute.Value;
						break;
				}
		}
	}
}
