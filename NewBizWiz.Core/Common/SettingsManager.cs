using System;
using System.IO;
using System.Text;
using System.Xml;

namespace Asa.Core.Common
{
	public class SettingsManager
	{
		private static SettingsManager _instance;

		private SettingsManager()
		{
			SelectedWizard = String.Empty;
			DashboardName = "6 Minute Seller";
			DashboardCode = String.Empty;
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
		public string DashboardCode { get; set; }

		public void LoadSharedSettings()
		{
			LoadDashdoardCode();

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

		private void LoadDashdoardCode()
		{
			var document = new XmlDocument();
			document.Load(ResourceManager.Instance.DashboardCodeFile.LocalPath);
			var node = document.SelectSingleNode(@"/Settings/dashboard/DashboardCode");
			if (node != null)
			{
				DashboardCode = node.InnerText.Trim().ToLower();
			}
		}
	}
}