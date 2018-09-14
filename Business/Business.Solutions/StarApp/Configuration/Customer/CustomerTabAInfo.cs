using System;
using System.Drawing;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.StarApp.Enums;

namespace Asa.Business.Solutions.StarApp.Configuration.Customer
{
	public class CustomerTabAInfo : StarTabWithHeaderInfo
	{
		public override StarChildTabType TabType => StarChildTabType.A;

		public Image Clipart1Image => _resourceManager.GraphicResources?.Tab4_A_Clipart1;
		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab4_A_Clipart2;

		public ClipartConfiguration Clipart1Configuration { get; private set; }
		public ClipartConfiguration Clipart2Configuration { get; private set; }

		public CustomerTabAInfo() : base(StarTopTabType.Customer)
		{
			Clipart1Configuration = new ClipartConfiguration();
			Clipart2Configuration = new ClipartConfiguration();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (!resourceManager.DataCustomerPartAFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(resourceManager.DataCustomerPartAFile.LocalPath);

			var node = document.SelectSingleNode(@"/CP04A");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var item = ListDataItem.FromXml(childNode);
				switch (childNode.Name)
				{
					case "CP04AHeader":
						if (!String.IsNullOrEmpty(item.Value))
							HeadersItems.Add(item);
						break;
				}
			}

			Clipart1Configuration = ClipartConfiguration.FromXml(node, "CP04AClipart1");
			Clipart2Configuration = ClipartConfiguration.FromXml(node, "CP04AClipart2");
		}
	}
}
