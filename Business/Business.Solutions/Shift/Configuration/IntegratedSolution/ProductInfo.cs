using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Solutions.Shift.Configuration.IntegratedSolution
{
	public class ProductInfo
	{
		public const string PositioningId = "positioning";
		public const string ResearchId = "research";
		public const string StyleId = "style";

		private bool _isConfigured;
		private readonly string _sourceFilePath;
		private readonly StorageDirectory _imagesFolder;

		public string ProductId { get; }
		public string Title { get; }

		public List<ListDataItem> HeaderItems { get; }
		public TextEditorConfiguration HeaderConfiguration { get; set; }

		public List<ListDataItem> Combo1Items { get; }
		public TextEditorConfiguration Combo1Configuration { get; set; }

		public PositioningInfo Positioning { get; set; }
		public ResearchInfo Research { get; set; }
		public StyleInfo Style { get; set; }

		private ProductInfo(string title, string sourceFile, StorageDirectory imagesFolder)
		{
			Title = title;

			_sourceFilePath = sourceFile;

			ProductId = Path.GetFileNameWithoutExtension(_sourceFilePath);

			_imagesFolder = imagesFolder;

			HeaderItems = new List<ListDataItem>();
			HeaderConfiguration = TextEditorConfiguration.Empty();

			Combo1Items = new List<ListDataItem>();
			Combo1Configuration = TextEditorConfiguration.Empty();

			Positioning = PositioningInfo.Empty();
			Research = ResearchInfo.Empty();
			Style = StyleInfo.Empty();
		}

		public static ProductInfo FromFile(string title, string filePath, StorageDirectory sharedImagesFolder)
		{
			var productInfo = new ProductInfo(title, filePath, sharedImagesFolder);
			return productInfo;
		}

		public void LoadData()
		{
			if (_isConfigured)
				return;

			if (!File.Exists(_sourceFilePath))
				return;

			var document = new XmlDocument();
			document.Load(_sourceFilePath);

			var configNode = document.SelectSingleNode("//Settings");

			if (configNode == null)
				return;

			foreach (var node in configNode.SelectNodes("./SolutionHeader")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
				HeaderItems.Add(ListDataItem.FromXml(node));

			foreach (var node in configNode.SelectNodes("./SolutionCombo1")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
				Combo1Items.Add(ListDataItem.FromXml(node));

			foreach (var node in configNode.SelectNodes("./Toggles/Toggle")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
			{
				var id = node.Attributes?.OfType<XmlAttribute>()
					.FirstOrDefault(a => String.Equals(a.Name, "Id", StringComparison.OrdinalIgnoreCase))?.Value?.ToLower();
				switch (id)
				{
					case PositioningId:
						Positioning = PositioningInfo.FromXml(node);
						break;
					case ResearchId:
						Research = ResearchInfo.FromXml(node);
						break;
					case StyleId:
						Style = StyleInfo.FromXml(node, _imagesFolder, ProductId);
						break;
				}
			}

			HeaderConfiguration = TextEditorConfiguration.FromXml(configNode, "SolutionHeader");
			Combo1Configuration = TextEditorConfiguration.FromXml(configNode, "SolutionCombo1");

			_isConfigured = true;
		}
	}
}
