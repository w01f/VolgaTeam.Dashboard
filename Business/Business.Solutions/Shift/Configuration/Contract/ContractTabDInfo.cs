using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;
using Asa.Business.Solutions.Common.Configuration;
using Asa.Business.Solutions.Shift.Configuration.Contract.TabD;
using Asa.Business.Solutions.Shift.Enums;

namespace Asa.Business.Solutions.Shift.Configuration.Contract
{
	public class ContractTabDInfo : ShiftChildTabInfo
	{
		public override bool IsRegularChildTab => true;

		public Image Clipart2Image => _resourceManager.GraphicResources?.Tab15_D_Clipart2;
		public ClipartConfiguration Clipart2Configuration { get; private set; }
		public Image Clipart3Image => _resourceManager.GraphicResources?.Tab15_D_Clipart3;
		public ClipartConfiguration Clipart3Configuration { get; private set; }

		public List<UserData> UserList { get; }

		public TextEditorConfiguration EditorNameConfiguration { get; set; }
		public TextEditorConfiguration EditorDescriptionConfiguration { get; set; }
		public TextEditorConfiguration EditorEmailConfiguration { get; set; }
		public TextEditorConfiguration EditorPhoneConfiguration { get; set; }

		public ContractTabDInfo() : base(ShiftChildTabType.D, ShiftTopTabType.Contract)
		{
			Clipart2Configuration = new ClipartConfiguration();
			Clipart3Configuration = new ClipartConfiguration();

			UserList = new List<UserData>();

			EditorNameConfiguration = TextEditorConfiguration.Empty();
			EditorDescriptionConfiguration = TextEditorConfiguration.Empty();
			EditorEmailConfiguration = TextEditorConfiguration.Empty();
			EditorPhoneConfiguration = TextEditorConfiguration.Empty();
		}

		public override void LoadData(XmlNode configNode, ResourceManager resourceManager)
		{
			base.LoadData(configNode, resourceManager);

			if (_resourceManager.DataContractPartDFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(_resourceManager.DataContractPartDFile.LocalPath);

				var node = document.SelectSingleNode(@"/SHIFT15D");
				if (node == null) return;

				var userNodes = node.SelectNodes("./Users/User")?.OfType<XmlNode>().ToArray() ?? new XmlNode[] { };
				foreach (var userNode in userNodes)
					UserList.Add(UserData.FromXml(userNode, resourceManager.ClipartTab15DUsersFolder));

				Clipart2Configuration = ClipartConfiguration.FromXml(node, "SHIFT15DClipart2");
				Clipart3Configuration = ClipartConfiguration.FromXml(node, "SHIFT15DClipart3");

				CommonEditorConfiguration = TextEditorConfiguration.FromXml(node);
				EditorNameConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT15DHeader");
				EditorDescriptionConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT15DLINKTEXT1");
				EditorEmailConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT15DLINKTEXT2");
				EditorPhoneConfiguration = TextEditorConfiguration.FromXml(node, "SHIFT15DLINKTEXT3");
			}
		}
	}
}
