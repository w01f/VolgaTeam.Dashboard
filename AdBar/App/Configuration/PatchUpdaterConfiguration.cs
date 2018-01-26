using System;
using System.IO;
using System.Xml;

namespace Asa.Bar.App.Configuration
{
	public class PatchUpdaterConfiguration
	{
		public bool RunUpdater { get; private set; }
		public bool ShowUpdaterInfo { get; private set; }
		public string UpdaterText { get; private set; }
		public DateTime PatchDate { get; private set; }

		public void Load()
		{
			if (!ResourceManager.Instance.PatchUpdaterConfigFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(ResourceManager.Instance.PatchUpdaterConfigFile.LocalPath);

			RunUpdater = Boolean.Parse(document.SelectSingleNode(@"/SFXUpdater/Enabled")?.InnerText ?? "false");
			ShowUpdaterInfo = document.SelectSingleNode(@"/SFXUpdater/Mode")?.InnerText?.ToLower() == "confirm";
			UpdaterText = document.SelectSingleNode(@"/SFXUpdater/ConfirmText")?.InnerText;

			if (ResourceManager.Instance.UpdaterConfigFile.ExistsLocal())
				PatchDate = new FileInfo(ResourceManager.Instance.UpdaterConfigFile.LocalPath).LastWriteTime;
		}
	}
}
