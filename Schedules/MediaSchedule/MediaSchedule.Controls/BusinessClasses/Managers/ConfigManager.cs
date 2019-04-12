using System;
using System.Xml;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Media.Controls.BusinessClasses.Managers
{
	public class ConfigManager
	{
		public bool UserQuickSchedules { get; private set; }

		public void LoadConfig(StorageFile configFile)
		{
			if (!configFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(configFile.LocalPath);

			UserQuickSchedules = Boolean.Parse(document.SelectSingleNode(@"//Config/Gitrdun")?.InnerText ?? "false");
		}
	}
}