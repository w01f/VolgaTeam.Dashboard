using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Dashboard.Entities.NonPersistent
{
	public class LeadoffStatementState : BaseSlideState
	{
		
		public bool ShowStatement1 { get; set; }
		public bool ShowStatement2 { get; set; }
		public bool ShowStatement3 { get; set; }
		public string SlideHeader { get; set; }
		public string Statement1 { get; set; }
		public string Statement2 { get; set; }
		public string Statement3 { get; set; }

		public LeadoffStatementState()
		{
			SaveFolder = new StorageDirectory(AppProfileManager.Instance.AppSaveFolder.RelativePathParts.Merge(new[] { "intro slide" }));
			TemplatesFolder = new StorageDirectory(AppProfileManager.Instance.AppSaveFolder.RelativePathParts.Merge(new[] { "intro slide", "templates" }));

			ShowStatement1 = true;
			ShowStatement2 = false;
			ShowStatement3 = false;
			SlideHeader = string.Empty;
			Statement1 = string.Empty;
			Statement2 = string.Empty;
			Statement3 = string.Empty;
		}

		protected override string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(base.Serialize());
			result.AppendLine(@"<ShowStatement1>" + ShowStatement1 + @"</ShowStatement1>");
			result.AppendLine(@"<ShowStatement2>" + ShowStatement2 + @"</ShowStatement2>");
			result.AppendLine(@"<ShowStatement3>" + ShowStatement3 + @"</ShowStatement3>");
			result.AppendLine(@"<SlideHeader>" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
			result.AppendLine(@"<Statement1>" + Statement1.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Statement1>");
			result.AppendLine(@"<Statement2>" + Statement2.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Statement2>");
			result.AppendLine(@"<Statement3>" + Statement3.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Statement3>");
			return result.ToString();
		}

		protected override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
				switch (childNode.Name)
				{
					case "ShowStatement1":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowStatement1 = tempBool;
						break;
					case "ShowStatement2":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowStatement2 = tempBool;
						break;
					case "ShowStatement3":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowStatement3 = tempBool;
						break;
					case "SlideHeader":
						SlideHeader = childNode.InnerText;
						break;
					case "Statement1":
						Statement1 = childNode.InnerText;
						break;
					case "Statement2":
						Statement2 = childNode.InnerText;
						break;
					case "Statement3":
						Statement3 = childNode.InnerText;
						break;
				}
			}
		}

		public void Load(string filePath)
		{
			XmlNode node;
			if (File.Exists(filePath))
			{
				var document = new XmlDocument();
				document.Load(filePath);
				node = document.SelectSingleNode(@"/LeadoffStatementState");
				if (node != null)
					Deserialize(node);
			}
		}

		public async Task Save(string fileName = "")
		{
			var file = GetSaveFile(fileName);
			using (var sw = new StreamWriter(file.LocalPath, false))
			{
				sw.Write("<LeadoffStatementState>" + Serialize() + " </LeadoffStatementState> ");
				sw.Flush();
			}
			//await file.Upload();
		}
	}
}
