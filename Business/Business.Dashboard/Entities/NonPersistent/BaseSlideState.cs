using System;
using System.IO;
using System.Text;
using System.Xml;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Business.Dashboard.Entities.NonPersistent
{
	public abstract class BaseSlideState
	{
		public bool IsNewSolution { get; set; }

		public StorageDirectory SaveFolder { get; protected set; }
		public StorageDirectory TemplatesFolder { get; protected set; }

		protected BaseSlideState()
		{
			IsNewSolution = true;
		}

		protected virtual string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<IsNewSolution>" + IsNewSolution + @"</IsNewSolution>");
			return result.ToString();
		}

		protected virtual void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "IsNewSolution":
						bool tempBool;
						if (bool.TryParse(childNode.InnerText, out tempBool))
							IsNewSolution = tempBool;
						break;
				}
			}
		}

		protected StorageFile GetSaveFile(string fileName = "")
		{
			StorageFile file;
			if (String.IsNullOrEmpty(fileName))
			{
				var now = DateTime.Now;
				fileName = "cover-" + now.ToString("MMddyy") + "-" + now.ToString("hmmsstt");
				file = new StorageFile(SaveFolder.RelativePathParts.Merge(String.Format("{0}.xml", fileName)));
			}
			else
				file = new StorageFile(TemplatesFolder.RelativePathParts.Merge(String.Format("{0}.xml", fileName)));

			file.AllocateParentFolder();

			return file;
		}

		public bool AllowToLoad()
		{
			var result = false;
			var filesFolderPath = SaveFolder.LocalPath;
			if (Directory.Exists(filesFolderPath))
				result = Directory.GetFiles(filesFolderPath, "*.xml").Length > 0;
			var templatesFolderPath = TemplatesFolder.LocalPath;
			if (Directory.Exists(templatesFolderPath))
				result |= Directory.GetFiles(templatesFolderPath, "*.xml").Length > 0;
			return result;
		}
	}
}
