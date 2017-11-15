using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Asa.Browser.Controls.BusinessClasses.Objects;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Media.Controls.BusinessClasses.Managers
{
	public class BrowserManager
	{
		public string RibbonBarTitle { get; private set; }
		public string SiteListTitle { get; private set; }
		public List<SiteSettings> Sites { get; }

		public BrowserManager()
		{
			Sites = new List<SiteSettings>();
		}

		public void Init(StorageFile settingsFile)
		{
			if (!settingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(settingsFile.LocalPath);

			RibbonBarTitle = document.SelectSingleNode(@"//Root/Group1Labels/Group1")?.InnerText ?? "adSALESapps.com";
			SiteListTitle = document.SelectSingleNode(@"//Root/Group1Labels/ComboHeader")?.InnerText ?? "Sites";

			foreach (var siteNode in document.SelectNodes(@"//Root/Site").OfType<XmlNode>())
			{
				var siteSettings = new SiteSettings();
				siteSettings.BaseUrl = siteNode.SelectSingleNode("./Url")?.InnerText;
				siteSettings.Title = siteNode.SelectSingleNode("./ComboName")?.InnerText ?? siteSettings.BaseUrl;
				if (!String.IsNullOrEmpty(siteSettings.BaseUrl))
					Sites.Add(siteSettings);
			}
		}
	}
}
