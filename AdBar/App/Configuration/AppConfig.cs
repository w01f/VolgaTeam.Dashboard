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
		public Color SplashBackColor { get; private set; }
		public Color SplashBorderColor { get; private set; }
		public Color SplashTextColor { get; private set; }

		public int UpdateWindowInterval { get; private set; }
		public int CheckProcessesInterval { get; private set; }

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

			TabStyle = (eSuperTabStyle)(Enum.GetValues(typeof(eSuperTabStyle)).GetValue(Math.Min(5, Math.Max(0, Int32.Parse(ConfigHelper.GetValueRegex("<theme>(.*)</theme>", configContent))))));
			ManagerStyle = (eStyle)(Enum.GetValues(typeof(eStyle)).GetValue(Math.Min(10, Math.Max(0, Int32.Parse(ConfigHelper.GetValueRegex("<subtheme>(.*)</subtheme>", configContent))))));

			AccentColor = ColorTranslator.FromHtml(ConfigHelper.GetValueRegex("<accent>(.*)</accent>", configContent) ?? "#D2691E");
			TextColor = ColorTranslator.FromHtml(ConfigHelper.GetValueRegex("<textcolor>(.*)</textcolor>", configContent) ?? "#000000");
			SplashBackColor = ColorTranslator.FromHtml(ConfigHelper.GetValueRegex("<AppSplashBackColor>(.*)</AppSplashBackColor>", configContent) ?? "#228B22");
			SplashBorderColor = ColorTranslator.FromHtml(ConfigHelper.GetValueRegex("<AppSplashBorderColor>(.*)</AppSplashBorderColor>", configContent) ?? "#228B22");
			SplashTextColor = ColorTranslator.FromHtml(ConfigHelper.GetValueRegex("<AppSplashTextColor>(.*)</AppSplashTextColor>", configContent) ?? "#ffffff");

			if (!AppManager.Instance.Settings.UserSettings.DefaultAccentColor.HasValue || AppManager.Instance.Settings.UserSettings.DefaultAccentColor.Value.Color != AccentColor)
			{
				AppManager.Instance.Settings.UserSettings.DefaultAccentColor = AccentColor;
				AppManager.Instance.Settings.UserSettings.UserAccentColor = null;
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
