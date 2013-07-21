using System.Collections.Generic;
using System.IO;
using System.Xml;
using CalendarBuilder.ConfigurationClasses;
using CalendarBuilder.InteropClasses;

namespace CalendarBuilder.BusinessClasses
{
	public class SuccessModelsManager
	{
		private static readonly SuccessModelsManager _instance = new SuccessModelsManager();

		private SuccessModelsManager()
		{
			SuccessModels = new List<SuccessModel>();
		}

		public List<SuccessModel> SuccessModels { get; set; }

		public static SuccessModelsManager Instance
		{
			get { return _instance; }
		}

		public void Load()
		{
			SuccessModels.Clear();
			if (Directory.Exists(SettingsManager.Instance.SuccessModelsPath))
			{
				var fileNames = new List<string>();
				fileNames.AddRange(Directory.GetFiles(SettingsManager.Instance.SuccessModelsPath, "*.xml"));
				fileNames.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x, y));
				int i = 0;
				foreach (string fileName in fileNames)
				{
					var successModel = new SuccessModel(fileName);
					successModel.Index = i;
					if (!string.IsNullOrEmpty(successModel.Name))
					{
						SuccessModels.Add(successModel);
						i++;
					}
				}
			}
		}
	}

	public class SuccessModel
	{
		public SuccessModel(string fileName)
		{
			Name = string.Empty;
			Link = string.Empty;
			Description = string.Empty;
			Load(fileName);
		}

		public int Index { get; set; }
		public string Name { get; set; }
		public string Link { get; set; }
		public string Description { get; set; }

		private void Load(string fileName)
		{
			XmlNode node = null;
			if (File.Exists(fileName))
			{
				var document = new XmlDocument();
				document.Load(fileName);
				node = document.SelectSingleNode(@"/Link/LinkText");
				if (node != null)
					Name = node.InnerText.Trim();
				node = document.SelectSingleNode(@"/Link/LinkURL");
				if (node != null)
					Link = node.InnerText.Trim();
				node = document.SelectSingleNode(@"/Link/Overview");
				if (node != null)
					Description = node.InnerText.Trim();
			}
		}
	}
}