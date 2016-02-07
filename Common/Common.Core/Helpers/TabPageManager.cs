using System.Collections.Generic;
using System.Xml;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.Core.Objects.TabPages;

namespace Asa.Common.Core.Helpers
{
	public class TabPageManager
	{
		private readonly StorageFile _contentFile;

		public TabPageManager(StorageFile contentFile)
		{
			_contentFile = contentFile;
			TabPageSettings = new List<TabPageConfig>();
			LoadHelpLinks();
		}

		public List<TabPageConfig> TabPageSettings { get; private set; }

		private void LoadHelpLinks()
		{
			TabPageSettings.Clear();
			if (!_contentFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(_contentFile.LocalPath);
			var node = document.SelectSingleNode(@"/Root");
			if (node == null) return;
			foreach (XmlNode childNode in node.ChildNodes)
			{
				var tabPageConfig = new TabPageConfig();
				tabPageConfig.Deserialize(childNode);
				if (tabPageConfig.Visible)
					TabPageSettings.Add(tabPageConfig);
			}
			TabPageSettings.Sort((x, y) => x.Order.CompareTo(y.Order));
		}
	}
}
