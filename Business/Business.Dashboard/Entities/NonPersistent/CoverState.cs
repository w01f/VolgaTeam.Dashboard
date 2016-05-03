using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Asa.Business.Dashboard.Dictionaries;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Dashboard.Entities.NonPersistent
{
	public class CoverState : BaseSlideState
	{
		public bool AddAsPageOne { get; set; }
		public bool UseGenericCover { get; set; }
		public bool ShowPresentationDate { get; set; }

		public string SlideHeader { get; set; }
		public string Advertiser { get; set; }
		public string DecisionMaker { get; set; }
		public DateTime PresentationDate { get; set; }
		public Quote Quote { get; set; }

		public CoverState()
		{
			SaveFolder = new StorageDirectory(AppProfileManager.Instance.AppSaveFolder.RelativePathParts.Merge(new[] { "cover" }));
			TemplatesFolder = new StorageDirectory(AppProfileManager.Instance.AppSaveFolder.RelativePathParts.Merge(new[] { "cover", "templates" }));

			ShowPresentationDate = false;
			AddAsPageOne = true;
			UseGenericCover = false;

			SlideHeader = string.Empty;
			Advertiser = string.Empty;
			DecisionMaker = string.Empty;
			PresentationDate = DateTime.MinValue;
			Quote = new Quote();
		}

		protected override String FileNamePrefix => "cover";

		protected override string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(base.Serialize());
			result.AppendLine(@"<AddAsPageOne>" + AddAsPageOne + @"</AddAsPageOne>");
			result.AppendLine(@"<UseGenericCover>" + UseGenericCover + @"</UseGenericCover>");
			result.AppendLine(@"<ShowPresentationDate>" + ShowPresentationDate + @"</ShowPresentationDate>");
			result.AppendLine(@"<SlideHeader>" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
			result.AppendLine(@"<Advertiser>" + Advertiser.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Advertiser>");
			result.AppendLine(@"<DecisionMaker>" + DecisionMaker.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</DecisionMaker>");
			result.AppendLine(@"<PresentationDate>" + PresentationDate + @"</PresentationDate>");
			result.AppendLine(@"<Quote>" + Quote.Serialize() + @"</Quote>");

			return result.ToString();
		}

		protected override void Deserialize(XmlNode node)
		{
			base.Deserialize(node);
			PresentationDate = DateTime.MinValue;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
				switch (childNode.Name)
				{
					case "AddAsPageOne":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							AddAsPageOne = tempBool;
						break;
					case "UseGenericCover":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							UseGenericCover = tempBool;
						break;
					case "ShowPresentationDate":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							ShowPresentationDate = tempBool;
						break;
					case "SlideHeader":
						SlideHeader = childNode.InnerText;
						break;
					case "Advertiser":
						Advertiser = childNode.InnerText;
						break;
					case "DecisionMaker":
						DecisionMaker = childNode.InnerText;
						break;
					case "PresentationDate":
						DateTime tempDateTime;
						if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
							PresentationDate = tempDateTime;
						break;
					case "Quote":
						Quote.Deserialize(childNode);
						break;
				}
			}
		}

		public void Load(string filePath)
		{
			if (!File.Exists(filePath)) return;
			var document = new XmlDocument();
			document.Load(filePath);
			var node = document.SelectSingleNode(@"/CoverState");
			if (node != null)
				Deserialize(node);
		}

		public async Task Save(string fileName = "")
		{
			var file = GetSaveFile(fileName);
			using (var sw = new StreamWriter(file.LocalPath, false))
			{
				sw.Write("<CoverState>" + Serialize() + " </CoverState>");
				sw.Flush();
			}
			//await file.Upload();
		}
	}
}
