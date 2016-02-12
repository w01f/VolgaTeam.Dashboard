using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Dashboard.Entities.NonPersistent
{
	public class TargetCustomersState : BaseSlideState
	{
		public string SlideHeader { get; set; }
		public List<string> Demo { get; set; }
		public List<string> Income { get; set; }
		public List<string> Geographic { get; set; }

		public TargetCustomersState()
		{
			SaveFolder = new StorageDirectory(AppProfileManager.Instance.AppSaveFolder.RelativePathParts.Merge(new[] { "target customer" }));
			TemplatesFolder = new StorageDirectory(AppProfileManager.Instance.AppSaveFolder.RelativePathParts.Merge(new[] { "target customer", "templates" }));

			SlideHeader = string.Empty;
			Demo = new List<string>();
			Income = new List<string>();
			Geographic = new List<string>();
		}

		protected override string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(base.Serialize());
			result.AppendLine(@"<SlideHeader>" + SlideHeader.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SlideHeader>");
			result.AppendLine(@"<Demo>");
			foreach (string demo in Demo)
				result.AppendLine(@"<Value>" + demo.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Value>");
			result.AppendLine(@"</Demo>");
			result.AppendLine(@"<Income>");
			foreach (string income in Income)
				result.AppendLine(@"<Value>" + income.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Value>");
			result.AppendLine(@"</Income>");
			result.AppendLine(@"<Geographic>");
			foreach (string geographic in Geographic)
				result.AppendLine(@"<Value>" + geographic.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Value>");
			result.AppendLine(@"</Geographic>");
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
					case "Demo":
						Demo.Clear();
						foreach (XmlNode valueNode in childNode.ChildNodes)
							Demo.Add(valueNode.InnerText);
						break;
					case "Income":
						Income.Clear();
						foreach (XmlNode valueNode in childNode.ChildNodes)
							Income.Add(valueNode.InnerText);
						break;
					case "Geographic":
						Geographic.Clear();
						foreach (XmlNode valueNode in childNode.ChildNodes)
							Geographic.Add(valueNode.InnerText);
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
				node = document.SelectSingleNode(@"/TargetCustomersState");
				if (node != null)
					Deserialize(node);
			}
		}

		public async Task Save(string fileName = "")
		{
			var file = GetSaveFile(fileName);
			using (var sw = new StreamWriter(file.LocalPath, false))
			{
				sw.Write("<TargetCustomersState>" + Serialize() + " </TargetCustomersState> ");
				sw.Flush();
			}
			//await file.Upload();
		}
	}
}
