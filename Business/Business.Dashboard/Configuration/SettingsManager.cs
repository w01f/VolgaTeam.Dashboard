using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.Themes;

namespace Asa.Business.Dashboard.Configuration
{
	public class SettingsManager
	{
		private ThemeSaveHelper _themeSaveHelper;

		private SettingsManager() { }

		public ThemeManager ThemeManager { get; private set; }
		public SlideManager SlideManager { get; private set; }

		public string SalesRep { get; set; }

		public static SettingsManager Instance { get; } = new SettingsManager();

		public Theme GetSelectedTheme(SlideType slideType)
		{
			return _themeSaveHelper.GetSelectedTheme(slideType);
		}

		public void SetSelectedTheme(SlideType slideType, string themeName)
		{
			_themeSaveHelper.SetSelectedTheme(slideType, themeName);
		}

		public async Task LoadSettings()
		{
			Asa.Common.Core.Configuration.SettingsManager.Instance.LoadSharedSettings();

			ThemeManager = new ThemeManager();
			ThemeManager.Load();
			PowerPointManager.Instance.SettingsChanged += (o, e) => ThemeManager.Load();
			_themeSaveHelper = new ThemeSaveHelper(ThemeManager);

			SlideManager = new SlideManager();
			SlideManager.Load();

			LoadDashboardSettings();
		}

		public void LoadDashboardSettings()
		{
			if (!Asa.Common.Core.Configuration.ResourceManager.Instance.AppSettingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(Asa.Common.Core.Configuration.ResourceManager.Instance.AppSettingsFile.LocalPath);
			var node = document.SelectSingleNode(@"/DashboardSettings/SalesRep");
			if (node != null)
				SalesRep = node.InnerText;
			_themeSaveHelper.Deserialize(document.SelectNodes(@"//DashboardSettings/SelectedTheme").OfType<XmlNode>());
		}

		public void SaveDashboardSettings()
		{
			var xml = new StringBuilder();
			xml.AppendLine(@"<DashboardSettings>");
			if (!String.IsNullOrEmpty(SalesRep))
				xml.AppendLine(@"<SalesRep>" + SalesRep.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SalesRep>");
			xml.AppendLine(_themeSaveHelper.Serialize());
			xml.AppendLine(@"</DashboardSettings>");
			using (var sw = new StreamWriter(Asa.Common.Core.Configuration.ResourceManager.Instance.AppSettingsFile.LocalPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}
	}
}
