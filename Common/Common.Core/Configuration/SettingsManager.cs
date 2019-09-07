using System;
using System.IO;
using System.Text;
using System.Xml;
using Asa.Common.Core.Helpers;

namespace Asa.Common.Core.Configuration
{
	public class SettingsManager
	{
		private static SettingsManager _instance;

		private SettingsManager()
		{
			SelectedWizard = String.Empty;
			DashboardName = "adSALESapps.com";
		}

		public static SettingsManager Instance
		{
			get
			{
				if (_instance == null)
					_instance = new SettingsManager();
				return _instance;
			}
		}

		public string SelectedWizard { get; set; }
		public string DashboardName { get; set; }

		public void LoadSharedSettings()
		{
			if (ResourceManager.Instance.SharedSettingsFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(ResourceManager.Instance.SharedSettingsFile.LocalPath);

				var node = document.SelectSingleNode(@"/SharedSettings/SelectedWizard");
				if (node != null)
					SelectedWizard = node.InnerText;
			}
			else if (ResourceManager.Instance.DefaultSlideSettingsFile.ExistsLocal())
			{
				var document = new XmlDocument();
				document.Load(ResourceManager.Instance.DefaultSlideSettingsFile.LocalPath);

				var node = document.SelectSingleNode(@"/Settings/PackName");
				if (node != null)
					SelectedWizard = node.InnerText;
			}

			MasterWizardManager.Instance.SetMasterWizard();
		}

		public void SaveSharedSettings()
		{
			var xml = new StringBuilder();

			xml.AppendLine(@"<SharedSettings>");
			xml.AppendLine(@"<SelectedWizard>" + SelectedWizard.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SelectedWizard>");
			xml.AppendLine(@"</SharedSettings>");

			using (var sw = new StreamWriter(ResourceManager.Instance.SharedSettingsFile.LocalPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}
	}
}
