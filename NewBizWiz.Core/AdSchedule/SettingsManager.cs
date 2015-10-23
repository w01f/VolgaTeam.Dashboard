using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Asa.Core.Common;

namespace Asa.Core.AdSchedule
{
	public class SettingsManager
	{
		private static SettingsManager _instance;
		private ThemeSaveHelper _themeSaveHelper;

		private SettingsManager() { }

		public static SettingsManager Instance
		{
			get
			{
				if (_instance == null)
					_instance = new SettingsManager();
				return _instance;
			}
		}

		public void LoadSettings()
		{
			Common.SettingsManager.Instance.LoadSharedSettings();

			if (!Common.ResourceManager.Instance.AppSettingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(Common.ResourceManager.Instance.AppSettingsFile.LocalPath);
			_themeSaveHelper.Deserialize(document.SelectNodes(@"//LocalSettings/SelectedTheme").OfType<XmlNode>());
		}

		public void SaveSettings()
		{
			var xml = new StringBuilder();
			xml.AppendLine(@"<LocalSettings>");
			xml.AppendLine(_themeSaveHelper.Serialize());
			xml.AppendLine(@"</LocalSettings>");
			using (var sw = new StreamWriter(Common.ResourceManager.Instance.AppSettingsFile.LocalPath, false))
			{
				sw.Write(xml);
				sw.Flush();
			}
		}

		public void InitThemeHelper(ThemeManager themeManager)
		{
			_themeSaveHelper = new ThemeSaveHelper(themeManager);
		}

		public string GetSelectedTheme(SlideType slideType)
		{
			return _themeSaveHelper.GetSelectedTheme(slideType).Name;
		}

		public void SetSelectedTheme(SlideType slideType, string themeName)
		{
			_themeSaveHelper.SetSelectedTheme(slideType, themeName);
		}
	}
}