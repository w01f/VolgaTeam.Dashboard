using System;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.IntegratedSolution
{
	public class ProductOutputCondition
	{
		public string TemplateName { get; private set; }

		public ProductLayoutType LayoutType { get; private set; }

		public bool ConditionPositioningTab1MultiPopup1 { get; private set; }
		public bool ConditionPositioningTab1MultiPopup2 { get; private set; }
		public bool ConditionPositioningTab2 { get; private set; }
		public bool ConditionResearchTab1 { get; private set; }

		public static ProductOutputCondition FromXml(XmlNode configNode)
		{
			var outputCondition = Empty();
			if (configNode != null)
			{

				foreach (var attribute in configNode.Attributes?.OfType<XmlAttribute>() ?? new XmlAttribute[] { })
				{
					switch (attribute.Name)
					{
						case "File":
							outputCondition.TemplateName = attribute.Value;
							break;
						case "Layout":
							switch (attribute.Value)
							{
								case "left":
									outputCondition.LayoutType = ProductLayoutType.Left;
									break;
								case "right":
									outputCondition.LayoutType = ProductLayoutType.Right;
									break;
							}
							break;
						case "Toggle1Tab1Multibox1":
							outputCondition.ConditionPositioningTab1MultiPopup1 = Boolean.Parse(attribute.Value);
							break;
						case "Toggle1Tab1Multibox2":
							outputCondition.ConditionPositioningTab1MultiPopup2 = Boolean.Parse(attribute.Value);
							break;
						case "Toggle1Tab2ComboMerge1":
							outputCondition.ConditionPositioningTab2 = Boolean.Parse(attribute.Value);
							break;
						case "Toggle2BundleLines":
							outputCondition.ConditionResearchTab1 = Boolean.Parse(attribute.Value);
							break;
					}
				}
			}
			return outputCondition;
		}

		public static ProductOutputCondition Empty()
		{
			return new ProductOutputCondition();
		}
	}
}
