using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Asa.Business.Common.Interfaces;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Interfaces;
using Asa.Common.Core.Objects.Themes;

namespace Asa.Business.Dashboard.Configuration
{
	public class SettingsManager : IThemeSettingsContainer, IBaseSettingsContainer
	{
		private ThemeSaveHelper _themeSaveHelper;

		private SettingsManager() { }

		public ThemeManager ThemeManager { get; private set; }
		public SlideManager SlideManager { get; private set; }

		public string SalesRep { get; set; }

		public bool ApplyThemeForAllSlideTypes { get; set; }

		public static SettingsManager Instance { get; } = new SettingsManager();

		public Theme GetSelectedTheme(SlideType slideType)
		{
			return _themeSaveHelper.GetSelectedTheme(slideType);
		}

		public String GetSelectedThemeName(SlideType slideType)
		{
			return GetSelectedTheme(slideType).Name;
		}

		public void SetSelectedTheme(SlideType slideType, string themeName, bool applyForAllSlidesTypes)
		{
			_themeSaveHelper.SetSelectedTheme(slideType, themeName, applyForAllSlidesTypes);
		}

		public async Task LoadSettings()
		{
			Asa.Common.Core.Configuration.SettingsManager.Instance.LoadSharedSettings();

			ThemeManager = new ThemeManager();
			ThemeManager.Load();
			SlideSettingsManager.Instance.SettingsChanged += (o, e) => ThemeManager.Load();
			InitThemeHelper(ThemeManager);

			SlideManager = new SlideManager();
			SlideManager.LoadSlides(Asa.Common.Core.Configuration.ResourceManager.Instance.SlideMastersFolder);

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
			node = document.SelectSingleNode(@"/Settings/ApplyThemeForAllSlideTypes");
			if (node != null)
			{
				bool tempBool;
				if (Boolean.TryParse(node.InnerText, out tempBool))
					ApplyThemeForAllSlideTypes = tempBool;
			}
			_themeSaveHelper.Deserialize(document.SelectNodes(@"//DashboardSettings/SelectedTheme").OfType<XmlNode>());
		}

		public void InitThemeHelper(ThemeManager themeManager)
		{
			_themeSaveHelper = new ThemeSaveHelper(
				themeManager,
				new[]
				{
					SlideType.Cleanslate,
					SlideType.Cover,
					SlideType.LeadoffStatement,
					SlideType.ClientGoals,
					SlideType.TargetCustomers,
					SlideType.SimpleSummary,
				}
			);
		}

		public void SaveSettings()
		{
			var xml = new StringBuilder();
			xml.AppendLine(@"<DashboardSettings>");
			if (!String.IsNullOrEmpty(SalesRep))
				xml.AppendLine(@"<SalesRep>" + SalesRep.Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</SalesRep>");
			xml.AppendLine(@"<ApplyThemeForAllSlideTypes>" + ApplyThemeForAllSlideTypes + @"</ApplyThemeForAllSlideTypes>");
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
