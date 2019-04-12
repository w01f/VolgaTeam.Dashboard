using System.IO;
using System.Xml;

namespace Asa.Common.Core.Objects.FormStyle
{
	public class StartFormTextConfiguration
	{
		public string ConnectText { get; private set; }
		public string VersionText { get; private set; }
		public string InitialLoadingText { get; private set; }
		public string RefreshText { get; private set; }
		public string LaunchText { get; private set; }

		public void Load(string settingsFilePath)
		{
			if (!File.Exists(settingsFilePath)) return;
			var document = new XmlDocument();
			document.Load(settingsFilePath);
			var node = document.SelectSingleNode(@"/Config");
			if (node != null)
				foreach (XmlNode childNode in node.ChildNodes)
				{
					switch (childNode.Name)
					{
						case "ConnectText":
							ConnectText = childNode.InnerText;
							break;
						case "VersionText":
							VersionText = childNode.InnerText;
							break;
						case "FirstTimeText":
							InitialLoadingText = childNode.InnerText;
							break;
						case "RefreshText":
							RefreshText = childNode.InnerText;
							break;
						case "LaunchText":
							LaunchText = childNode.InnerText;
							break;
					}
				}
		}
	}
}
