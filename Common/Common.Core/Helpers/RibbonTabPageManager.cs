using System.Collections.Generic;
using System.Xml;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.Core.Objects.TabPages;

namespace Asa.Common.Core.Helpers
{
	public class RibbonTabPageManager
	{
		private readonly StorageFile _contentFile;

		public List<TabPageConfig> RibbonTabPageSettings { get; private set; }

		public RibbonTabPageManager(StorageFile contentFile)
		{
			_contentFile = contentFile;
			RibbonTabPageSettings = new List<TabPageConfig>();
			LoadConfig();
		}

		private void LoadConfig()
		{
			RibbonTabPageSettings.Clear();
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
					RibbonTabPageSettings.Add(tabPageConfig);
			}
			RibbonTabPageSettings.Sort((x, y) => x.Order.CompareTo(y.Order));
		}
	}
}
