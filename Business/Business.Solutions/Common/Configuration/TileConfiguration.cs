using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;

namespace Asa.Business.Solutions.Common.Configuration
{
	public class TileConfiguration
	{
		public const string ImageResourcesSubFolderName = "Images";
		public const string FileResourcesSubFolderName = "Files";

		public List<TileGroup> Groups { get; }

		private TileConfiguration()
		{
			Groups = new List<TileGroup>();
		}

		public static TileConfiguration FromFile(string configFilePath)
		{
			var config = new TileConfiguration();

			var resourceFolderPath = Path.Combine(Path.GetDirectoryName(configFilePath), "Resources");

			var document = new XmlDocument();
			document.Load(configFilePath);

			var groupNodes = document.SelectNodes("//Config/Tiles/Group")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
			foreach (var groupNode in groupNodes)
				config.Groups.Add(TileGroup.FromXml(groupNode, resourceFolderPath));

			return config;
		}

		public static Size ParseSizeConfiguration(XmlNode configNode)
		{
			if (configNode == null)
				return Size.Empty;

			return new Size(
				Int32.Parse(configNode.SelectSingleNode("./Width")?.InnerText ?? "0"),
				Int32.Parse(configNode.SelectSingleNode("./Height")?.InnerText ?? "0")
			);
		}

		public static Point ParseImageIdentConfiguration(XmlNode configNode)
		{
			if (configNode == null)
				return Point.Empty;

			return new Point(
				Int32.Parse(configNode.SelectSingleNode("./Left")?.InnerText ?? "0"),
				Int32.Parse(configNode.SelectSingleNode("./Top")?.InnerText ?? "0")
			);
		}

		public static Font ParseFontConfiguration(XmlNode configNode)
		{
			if (configNode == null)
				return null;

			var fontFamily = configNode.SelectSingleNode("./Family")?.InnerText ?? "Arial";
			var fontSize = Int32.Parse(configNode.SelectSingleNode("./Size")?.InnerText ?? "10");

			var fontStyle = FontStyle.Regular;
			var fontStyleNodes = configNode.SelectNodes("./Style")?.OfType<XmlNode>() ?? new XmlNode[] { };
			foreach (var fontStyleNode in fontStyleNodes)
			{
				if (String.Equals(fontStyleNode.InnerText, "bold", StringComparison.OrdinalIgnoreCase))
					fontStyle |= FontStyle.Bold;

				if (String.Equals(fontStyleNode.InnerText, "italic", StringComparison.OrdinalIgnoreCase))
					fontStyle |= FontStyle.Italic;
			}

			return new Font(fontFamily, fontSize, fontStyle, GraphicsUnit.Point);
		}

		public static ContentAlignment? ParseContentAlignment(XmlNode configNode)
		{
			if (configNode == null)
				return null;

			foreach (var c in Enum.GetNames(typeof(ContentAlignment)))
				if (String.Equals(configNode.InnerText, c, StringComparison.OrdinalIgnoreCase))
					return (ContentAlignment)Enum.Parse(typeof(ContentAlignment), c);

			return null;
		}

		public static Padding ParsePaddingConfiguration(XmlNode configNode)
		{
			if (configNode == null)
				return Padding.Empty;

			return new Padding(
				Int32.Parse(configNode.SelectSingleNode("./Left")?.InnerText ?? "0"),
				Int32.Parse(configNode.SelectSingleNode("./Right")?.InnerText ?? "0"),
				Int32.Parse(configNode.SelectSingleNode("./Top")?.InnerText ?? "0"),
				Int32.Parse(configNode.SelectSingleNode("./Bottom")?.InnerText ?? "0")
			);
		}
	}

	public class TileGroup
	{
		public string Title { get; private set; }
		public Color ForeColor { get; private set; }
		public Font Font { get; private set; }
		public Padding Padding { get; private set; }

		public List<TileItem> Items { get; }

		private TileGroup()
		{
			Items = new List<TileItem>();
		}

		public static TileGroup FromXml(XmlNode configNode, string resourceFolderPath)
		{
			var config = new TileGroup();

			config.Title = configNode.SelectSingleNode("./Title")?.InnerText;
			config.ForeColor = ColorTranslator.FromHtml(configNode.SelectSingleNode("./ForeColor")?.InnerText);
			config.Font = TileConfiguration.ParseFontConfiguration(configNode.SelectSingleNode("./Font"));
			config.Padding = TileConfiguration.ParsePaddingConfiguration(configNode.SelectSingleNode("./Padding"));

			var itemNodes = configNode.SelectNodes("./Item")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
			foreach (var itemNode in itemNodes)
				config.Items.Add(TileItem.FromXml(itemNode, resourceFolderPath));

			return config;
		}
	}

	public abstract class TileItem
	{
		public string Title { get; private set; }
		public string Tooltip { get; private set; }
		public string ImagePath { get; private set; }
		public Size Size { get; private set; }
		public Point ImageIdent { get; private set; }
		public Color ForeColor { get; private set; }
		public eMetroTileColor BackColor { get; private set; }
		public Font Font { get; private set; }
		public ContentAlignment? TextAlignment { get; private set; }

		public abstract string GetExecutablePath();

		public static TileItem FromXml(XmlNode configNode, string resourceFolderPath)
		{
			TileItem config;

			var url = configNode.SelectSingleNode("./Url")?.InnerText;
			if (!String.IsNullOrWhiteSpace(url))
			{
				config = new UrlItem { Url = url };
			}
			else
			{
				var fileItem = new FileItem();

				var fileName = configNode.SelectSingleNode("./FileName")?.InnerText;
				if (!String.IsNullOrWhiteSpace(fileName))
				{
					fileItem.FilePath = Path.Combine(resourceFolderPath, TileConfiguration.FileResourcesSubFolderName, fileName);
				}
				else
				{
					var pathNodes = configNode.SelectNodes("./FilePath")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
					foreach (var pathNode in pathNodes)
					{
						if (!File.Exists(pathNode.InnerText)) continue;

						fileItem.FilePath = pathNode.InnerText;
						break;
					}
				}

				config = fileItem;
			}

			config.Title = configNode.SelectSingleNode("./Title")?.InnerText;
			config.Tooltip = configNode.SelectSingleNode("./Tooltip")?.InnerText;
			config.Size = TileConfiguration.ParseSizeConfiguration(configNode.SelectSingleNode("./Size"));
			config.ImageIdent = TileConfiguration.ParseImageIdentConfiguration(configNode.SelectSingleNode("./Position"));
			config.ForeColor = ColorTranslator.FromHtml(configNode.SelectSingleNode("./ForeColor")?.InnerText);
			config.BackColor = (eMetroTileColor)Enum.GetValues(typeof(eMetroTileColor)).GetValue(
				Int32.Parse(configNode.SelectSingleNode("./BackColor")?.InnerText ?? "0"));
			config.Font = TileConfiguration.ParseFontConfiguration(configNode.SelectSingleNode("./Font"));
			config.TextAlignment = TileConfiguration.ParseContentAlignment(configNode.SelectSingleNode("./TextAlignment"));

			var imageName = configNode.SelectSingleNode("./ImageFileName")?.InnerText;
			if (!String.IsNullOrWhiteSpace(imageName) && File.Exists(Path.Combine(resourceFolderPath, TileConfiguration.ImageResourcesSubFolderName, imageName)))
				config.ImagePath = Path.Combine(resourceFolderPath, TileConfiguration.ImageResourcesSubFolderName, imageName);

			return config;
		}
	}

	class UrlItem : TileItem
	{
		public string Url { get; set; }

		public override String GetExecutablePath()
		{
			return Url;
		}
	}

	class FileItem : TileItem
	{
		public string FilePath { get; set; }

		public override String GetExecutablePath()
		{
			return FilePath;
		}
	}
}
