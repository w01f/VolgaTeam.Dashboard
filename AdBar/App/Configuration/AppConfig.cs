using System;
using System.Drawing;
using DevComponents.DotNetBar;

namespace Asa.Bar.App.Configuration
{
	public class AppConfig
	{
		public float VirtualDpi { get; private set; }

		public float FontSize { get; private set; }
		public int Width { get; private set; }
		public int Height { get; private set; }
		public int CollapsedHeight { get; private set; }

		public int MaxShortButtons { get; private set; }
		public int MultiHorizontalPadding { get; private set; }
		public int MultiVerticalPadding { get; private set; }
		public bool DisableNonAvailableButtons { get; private set; }

		public eSuperTabStyle TabStyle { get; private set; }
		public eStyle ManagerStyle { get; private set; }

		public Color AccentColor { get; private set; }
		public Color TextColor { get; private set; }
		public Color RibbonBarTextColor { get; private set; }
		public Color RibbonBarTextHoverColor { get; private set; }
		public Color SplashBackColor { get; private set; }
		public Color SplashBorderColor { get; private set; }
		public Color SplashTextColor { get; private set; }

		public Color FloaterBackColor { get; private set; }
		public Color FloaterBorderColor { get; private set; }

		public int UpdateWindowInterval { get; private set; }
		public int CheckProcessesInterval { get; private set; }

		public bool ShowUndockButton { get; private set; }
		public bool ShowDockRegularButton { get; private set; }
		public bool ShowDockFloaterButton { get; private set; }
		public bool ShowFloaterWhenUndock { get; private set; }
		public bool ShowExpandedWhenUndock { get; private set; }

		public AppConfig()
		{
			Reset();
		}

		public void Load()
		{
			var configContent = ConfigHelper.GetTextFromFile(ResourceManager.Instance.AppConfigFile.LocalPath);
			Height = (Int32)((0.5 + (0.5 * VirtualDpi)) * float.Parse(ConfigHelper.GetValueRegex("<height>(.*)</height>", configContent)));
			Width = (Int32)((0.5 + (0.5 * VirtualDpi)) * float.Parse(ConfigHelper.GetValueRegex("<width>(.*)</width>", configContent)));
			MultiHorizontalPadding = Int32.Parse(ConfigHelper.GetValueRegex("<multihorizontalpadding>(.*)</multihorizontalpadding>", configContent));
			MultiVerticalPadding = Int32.Parse(ConfigHelper.GetValueRegex("<multiverticalpadding>(.*)</multiverticalpadding>", configContent));
			DisableNonAvailableButtons = ConfigHelper.GetValueRegex("<grayoutnonapproveduser>(.*)</grayoutnonapproveduser>", configContent) != "false";
			MaxShortButtons = Int32.Parse(ConfigHelper.GetValueRegex("<maxshortbuttons>(.*)</maxshortbuttons>", configContent));
			UpdateWindowInterval = Int32.Parse(ConfigHelper.GetValueRegex("<checktime>(.*)</checktime>", configContent));
			CheckProcessesInterval = Int32.Parse(ConfigHelper.GetValueRegex("<inactivitychecktime>(.*)</inactivitychecktime>", configContent));
			ShowUndockButton = ConfigHelper.GetValueRegex("<ShowUndockArrow>(.*)</ShowUndockArrow>", configContent) == "true";
			ShowDockRegularButton = ConfigHelper.GetValueRegex("<ShowDockArrowRibbon>(.*)</ShowDockArrowRibbon>", configContent) == "true";
			ShowDockFloaterButton = ConfigHelper.GetValueRegex("<ShowDockArrowFloater>(.*)</ShowDockArrowFloater>", configContent) == "true";
			ShowFloaterWhenUndock = ConfigHelper.GetValueRegex("<UndockedDefaultMode>(.*)</UndockedDefaultMode>", configContent) == "collapsed";
			ShowExpandedWhenUndock = ConfigHelper.GetValueRegex("<UndockedDefaultMode>(.*)</UndockedDefaultMode>", configContent) == "expanded";

			TabStyle = (eSuperTabStyle)(Enum.GetValues(typeof(eSuperTabStyle)).GetValue(Math.Min(5, Math.Max(0, Int32.Parse(ConfigHelper.GetValueRegex("<theme>(.*)</theme>", configContent))))));
			ManagerStyle = (eStyle)(Enum.GetValues(typeof(eStyle)).GetValue(Math.Min(10, Math.Max(0, Int32.Parse(ConfigHelper.GetValueRegex("<subtheme>(.*)</subtheme>", configContent))))));

			AccentColor = ColorTranslator.FromHtml(ConfigHelper.GetValueRegex("<accent>(.*)</accent>", configContent) ?? "#D2691E");
			TextColor = ColorTranslator.FromHtml(ConfigHelper.GetValueRegex("<textcolor>(.*)</textcolor>", configContent) ?? "#000000");
			RibbonBarTextColor = ColorTranslator.FromHtml(ConfigHelper.GetValueRegex("<grouptextcolor>(.*)</grouptextcolor>", configContent) ?? "#000000");
			RibbonBarTextHoverColor = ColorTranslator.FromHtml(ConfigHelper.GetValueRegex("<grouptexthovercolor>(.*)</grouptexthovercolor>", configContent) ?? "#000000");
			SplashBackColor = ColorTranslator.FromHtml(ConfigHelper.GetValueRegex("<AppSplashBackColor>(.*)</AppSplashBackColor>", configContent) ?? "#228B22");
			SplashBorderColor = ColorTranslator.FromHtml(ConfigHelper.GetValueRegex("<AppSplashBorderColor>(.*)</AppSplashBorderColor>", configContent) ?? "#228B22");
			SplashTextColor = ColorTranslator.FromHtml(ConfigHelper.GetValueRegex("<AppSplashTextColor>(.*)</AppSplashTextColor>", configContent) ?? "#ffffff");
			FloaterBackColor = ColorTranslator.FromHtml(ConfigHelper.GetValueRegex("<UndockedCollapsedBackColor>(.*)</UndockedCollapsedBackColor>", configContent) ?? "#228B22");
			FloaterBorderColor = ColorTranslator.FromHtml(ConfigHelper.GetValueRegex("<UndockedCollapsedBorderColor>(.*)</UndockedCollapsedBorderColor>", configContent) ?? "#228B22");

			if ((!AppManager.Instance.Settings.UserSettings.DefaultAccentColor.HasValue || AppManager.Instance.Settings.UserSettings.DefaultAccentColor.Value.Color != AccentColor) ||
				(!AppManager.Instance.Settings.UserSettings.DefaultTextColor.HasValue || AppManager.Instance.Settings.UserSettings.DefaultTextColor.Value.Color != TextColor) ||
				(!AppManager.Instance.Settings.UserSettings.DefaultRibbonBarTextColor.HasValue || AppManager.Instance.Settings.UserSettings.DefaultRibbonBarTextColor.Value != RibbonBarTextColor) ||
				(!AppManager.Instance.Settings.UserSettings.DefaultRibbonBarHoverTextColor.HasValue || AppManager.Instance.Settings.UserSettings.DefaultRibbonBarHoverTextColor.Value != RibbonBarTextHoverColor))
			{
				AppManager.Instance.Settings.UserSettings.DefaultAccentColor = AccentColor;
				AppManager.Instance.Settings.UserSettings.UserAccentColor = null;
				AppManager.Instance.Settings.UserSettings.DefaultTextColor = TextColor;
				AppManager.Instance.Settings.UserSettings.UserTextColor = null;
				AppManager.Instance.Settings.UserSettings.DefaultRibbonBarTextColor = RibbonBarTextColor;
				AppManager.Instance.Settings.UserSettings.DefaultRibbonBarHoverTextColor = RibbonBarTextHoverColor;
				AppManager.Instance.Settings.UserSettings.Save();
			}

			if (AppManager.Instance.Settings.UserSettings.DefaultShowFloaterWhenUndock != ShowFloaterWhenUndock)
			{
				AppManager.Instance.Settings.UserSettings.DefaultShowFloaterWhenUndock = ShowFloaterWhenUndock;
				AppManager.Instance.Settings.UserSettings.ShowFloaterWhenUndock = ShowFloaterWhenUndock;
				AppManager.Instance.Settings.UserSettings.Save();
			}

			if (AppManager.Instance.Settings.UserSettings.DefaultAlwaysExpanded != ShowExpandedWhenUndock)
			{
				AppManager.Instance.Settings.UserSettings.DefaultAlwaysExpanded = ShowExpandedWhenUndock;
				AppManager.Instance.Settings.UserSettings.AlwaysExpanded = ShowExpandedWhenUndock;
				AppManager.Instance.Settings.UserSettings.Save();
			}
		}

		private void Reset()
		{
			using (var graphics = Graphics.FromHwnd(IntPtr.Zero))
			{
				VirtualDpi = 1 / (96 / graphics.DpiX);
			}

			FontSize = VirtualDpi > 1 ? 9.25f : 8.25f;
			Height = 200;
			CollapsedHeight = (Int32)(22 * VirtualDpi) - (VirtualDpi >= 2 ? 1 : 0) + (VirtualDpi > 1 && VirtualDpi < 1.5 ? 2 : 0);

			MaxShortButtons = 4;
			MultiHorizontalPadding = 40;
			MultiVerticalPadding = 40;
		}
	}
}
