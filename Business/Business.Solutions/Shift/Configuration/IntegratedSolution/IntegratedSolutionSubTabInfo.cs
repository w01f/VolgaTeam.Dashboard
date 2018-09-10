using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.IntegratedSolution
{
	public abstract class IntegratedSolutionSubTabInfo : ShiftChildTabInfo
	{
		public override bool IsRegularChildTab => true;

		public List<ProductInfo> Products { get; }
		public List<ProductOutputCondition> OutputConditions { get; }

		protected IntegratedSolutionSubTabInfo(ShiftChildTabType tabType) : base(tabType, ShiftTopTabType.IntegratedSolution)
		{
			Products = new List<ProductInfo>();
			OutputConditions = new List<ProductOutputCondition>();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (resourceManager.DataSolutionsCommonFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataSolutionsCommonFile.LocalPath);

				var itemInfoNodes = document.SelectNodes("//Products/Product")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var productNode in itemInfoNodes)
				{
					var fileName = productNode.SelectSingleNode("./ProductFile")?.InnerText;
					if (!String.IsNullOrWhiteSpace(fileName))
					{
						var filePath = Path.Combine(Asa.Common.Core.Configuration.ResourceManager.Instance.DictionariesFolder.LocalPath,
							fileName);
						if (File.Exists(filePath))
							Products.Add(ProductInfo.FromFile(
								productNode.SelectSingleNode("./Name")?.InnerText,
								filePath,
								resourceManager.ClipartTab9SharedFolder));
					}
				}
			}

			if (resourceManager.DataIntegratedSolutionOutputConditionsFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataIntegratedSolutionOutputConditionsFile.LocalPath);

				var outputConditionNodes = document.SelectNodes("//SlideOutputRules/SourceOutput")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var outputConditionNode in outputConditionNodes)
					OutputConditions.Add(ProductOutputCondition.FromXml(outputConditionNode));
			}
		}
	}
}
