using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.IntegratedSolution
{
	public class IntegratedSolutionTabAInfo : ShiftChildTabInfo
	{
		public override bool IsRegularChildTab => true;

		public List<ProductInfo> Products { get; }
		public List<ProductOutputCondition> OutputConditions { get; }

		public IntegratedSolutionTabAInfo() : base(ShiftChildTabType.A)
		{
			Products = new List<ProductInfo>();
			OutputConditions = new List<ProductOutputCondition>();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			RightLogo = resourceManager.LogoTab9SubARightFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubARightFile.LocalPath)
				: null;
			FooterLogo = resourceManager.LogoTab9SubAFooterFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubAFooterFile.LocalPath)
				: null;
			BackgroundLogo = resourceManager.LogoTab9SubABackgroundFile.ExistsLocal()
				? Image.FromFile(resourceManager.LogoTab9SubABackgroundFile.LocalPath)
				: null;

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

			if (resourceManager.DataIntegratedSolutionPartAOutputConditionsFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(resourceManager.DataIntegratedSolutionPartAOutputConditionsFile.LocalPath);

				var outputConditionNodes = document.SelectNodes("//SlideOutputRules/SourceOutput")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var outputConditionNode in outputConditionNodes)
					OutputConditions.Add(ProductOutputCondition.FromXml(outputConditionNode));
			}
		}
	}
}
