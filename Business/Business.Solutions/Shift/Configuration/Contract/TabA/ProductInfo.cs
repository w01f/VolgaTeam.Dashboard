using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;

namespace Asa.Business.Solutions.Shift.Configuration.Contract.TabA
{
	public class ProductInfo
	{
		private bool _isConfigured;
		private readonly string _sourceFilePath;

		public string ProductId { get; }
		public string Title { get; }

		public List<ListDataItem> MemoPopup1Items { get; }
		public TextEditorConfiguration MemoPopup1Configuration { get; set; }

		public List<ListDataItem> Combo1Items { get; }
		public TextEditorConfiguration Combo1Configuration { get; set; }

		public List<ListDataItem> Combo2Items { get; }
		public TextEditorConfiguration Combo2Configuration { get; set; }

		public List<ListDataItem> Combo3Items { get; }
		public TextEditorConfiguration Combo3Configuration { get; set; }

		public ProductButtonConfiguration ButtonConfiguration { get; set; }
		public FormListConfiguration FormListConfiguration { get; set; }

		private ProductInfo(string title, string sourceFile)
		{
			Title = title;

			_sourceFilePath = sourceFile;

			ProductId = Path.GetFileNameWithoutExtension(_sourceFilePath);

			MemoPopup1Items = new List<ListDataItem>();
			MemoPopup1Configuration = TextEditorConfiguration.Empty();

			Combo1Items = new List<ListDataItem>();
			Combo1Configuration = TextEditorConfiguration.Empty();

			Combo2Items = new List<ListDataItem>();
			Combo2Configuration = TextEditorConfiguration.Empty();

			Combo3Items = new List<ListDataItem>();
			Combo3Configuration = TextEditorConfiguration.Empty();

			ButtonConfiguration = ProductButtonConfiguration.Empty();
			FormListConfiguration = FormListConfiguration.Empty();
		}

		public static ProductInfo FromFile(string title, string filePath)
		{
			var productInfo = new ProductInfo(title, filePath);
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

			foreach (var node in configNode.SelectNodes("./MediaPlan/Description/Item")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
				MemoPopup1Items.Add(ListDataItem.FromXml(node));

			foreach (var node in configNode.SelectNodes("./MediaPlan/SizeSpecs/Item")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
				Combo1Items.Add(ListDataItem.FromXml(node));

			foreach (var node in configNode.SelectNodes("./MediaPlan/Quantity/Item")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
				Combo2Items.Add(ListDataItem.FromXml(node));

			foreach (var node in configNode.SelectNodes("./MediaPlan/Investment/Item")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { })
				Combo3Items.Add(ListDataItem.FromXml(node));

			_isConfigured = true;
		}
	}
}
