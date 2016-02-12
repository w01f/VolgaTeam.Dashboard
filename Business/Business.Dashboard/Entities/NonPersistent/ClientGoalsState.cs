using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Dashboard.Entities.NonPersistent
{
	public class ClientGoalsState : BaseSlideState
	{
		public string SlideHeader { get; set; }
		public string Goal1 { get; set; }
		public string Goal2 { get; set; }
		public string Goal3 { get; set; }
		public string Goal4 { get; set; }
		public string Goal5 { get; set; }

		public ClientGoalsState()
			: base()
		{
			SaveFolder = new StorageDirectory(AppProfileManager.Instance.AppSaveFolder.RelativePathParts.Merge(new[] { "needs analysis" }));
			TemplatesFolder = new StorageDirectory(AppProfileManager.Instance.AppSaveFolder.RelativePathParts.Merge(new[] { "needs analysis", "templates" }));

			SlideHeader = string.Empty;
			Goal1 = string.Empty;
			Goal2 = string.Empty;
			Goal3 = string.Empty;
			Goal4 = string.Empty;
			Goal5 = string.Empty;
		}

		protected override string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(base.Serialize());
			result.AppendLine(@"<SlideHeader>" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
			result.AppendLine(@"<Goal1>" + Goal1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal1>");
			result.AppendLine(@"<Goal2>" + Goal2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal2>");
			result.AppendLine(@"<Goal3>" + Goal3.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal3>");
			result.AppendLine(@"<Goal4>" + Goal4.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal4>");
			result.AppendLine(@"<Goal5>" + Goal5.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Goal5>");
			return result.ToString();
		}

		protected override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "SlideHeader":
						SlideHeader = childNode.InnerText;
						break;
					case "Goal1":
						Goal1 = childNode.InnerText;
						break;
					case "Goal2":
						Goal2 = childNode.InnerText;
						break;
					case "Goal3":
						Goal3 = childNode.InnerText;
						break;
					case "Goal4":
						Goal4 = childNode.InnerText;
						break;
					case "Goal5":
						Goal5 = childNode.InnerText;
						break;
				}
			}
		}

		public void Load(string filePath)
		{
			if (File.Exists(filePath))
			{
				var document = new XmlDocument();
				document.Load(filePath);
				var node = document.SelectSingleNode(@"/ClientGoalsState");
				if (node != null)
					Deserialize(node);
			}
		}

		public async Task Save(string fileName = "")
		{
			var file = GetSaveFile(fileName);
			using (var sw = new StreamWriter(file.LocalPath, false))
			{
				sw.Write("<ClientGoalsState>" + Serialize() + " </ClientGoalsState> ");
				sw.Flush();
			}
			//await file.Upload();
		}
	}
}
