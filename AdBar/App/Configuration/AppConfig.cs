using System;
using System.Drawing;
using DevComponents.DotNetBar;

namespace Asa.Bar.App.Configuration
{
	public class AppConfig
	{
		public float VirtualDpi { get; private set; }

		public int CollapsedHeight { get; private set; }
		public int UncollapsedHeight { get; private set; }
		public int Width { get; private set; }

		public int MaxShortButtons { get; private set; }
		public int MultiHorizontalPadding { get; private set; }
		public int MultiVerticalPadding { get; private set; }
		public bool DisableNonAvailableButtons { get; private set; }

		public eSuperTabStyle TabStyle { get; private set; }
		public eStyle ManagerStyle { get; private set; }
		public Color AccentColor { get; private set; }

		public int UpdateWindowInterval { get; private set; }
		public int CheckProcessesInterval { get; private set; }

		public AppConfig()
		{
			Reset();
		}

		public void Load()
		{
			using (var graphics = Graphics.FromHwnd(IntPtr.Zero))
			{
				VirtualDpi = 1 / (96 / graphics.DpiX);
			}

			var configContent = ConfigHelper.GetTextFromFile(ResourceManager.Instance.AppConfigFile.LocalPath);
			UncollapsedHeight = (Int32)((0.5 + (0.5 * VirtualDpi)) * float.Parse(ConfigHelper.GetValueRegex("<height>(.*)</height>", configContent)));
			CollapsedHeight = (Int32)(((0.25 + (0.75 * VirtualDpi))) * float.Parse(ConfigHelper.GetValueRegex("<collapsedheight>(.*)</collapsedheight>", configContent)));

			Width = (Int32)((0.5 + (0.5 * VirtualDpi)) * float.Parse(ConfigHelper.GetValueRegex("<width>(.*)</width>", configContent)));
			MultiHorizontalPadding = Int32.Parse(ConfigHelper.GetValueRegex("<multihorizontalpadding>(.*)</multihorizontalpadding>", configContent));
			MultiVerticalPadding = Int32.Parse(ConfigHelper.GetValueRegex("<multiverticalpadding>(.*)</multiverticalpadding>", configContent));
			DisableNonAvailableButtons = ConfigHelper.GetValueRegex("<grayoutnonapproveduser>(.*)</grayoutnonapproveduser>", configContent) != "false";
			MaxShortButtons = Int32.Parse(ConfigHelper.GetValueRegex("<maxshortbuttons>(.*)</maxshortbuttons>", configContent));
			UpdateWindowInterval = Int32.Parse(ConfigHelper.GetValueRegex("<checktime>(.*)</checktime>", configContent));
			CheckProcessesInterval = Int32.Parse(ConfigHelper.GetValueRegex("<inactivitychecktime>(.*)</inactivitychecktime>", configContent));

			TabStyle = (eSuperTabStyle)(Enum.GetValues(typeof(eSuperTabStyle)).GetValue(Math.Min(5, Math.Max(0, Int32.Parse(ConfigHelper.GetValueRegex("<theme>(.*)</theme>", configContent))))));
			ManagerStyle = (eStyle)(Enum.GetValues(typeof(eStyle)).GetValue(Math.Min(10, Math.Max(0, Int32.Parse(ConfigHelper.GetValueRegex("<subtheme>(.*)</subtheme>", configContent))))));

			var colorConfig = ConfigHelper.GetValueRegex("<accent>(.*)</accent>", configContent);
			AccentColor = !String.IsNullOrEmpty(colorConfig) ? Color.FromName(colorConfig) : Color.Chocolate;
			if (AppManager.Instance.Settings.UserSettings.AccentColor == Color.Transparent)
			{
				AppManager.Instance.Settings.UserSettings.AccentColor = AccentColor;
				AppManager.Instance.Settings.UserSettings.Save();
			}
		}

		private void Reset()
		{
			CollapsedHeight = 24;
			UncollapsedHeight = 200;
			MaxShortButtons = 4;
			MultiHorizontalPadding = 40;
			MultiVerticalPadding = 40;
		}
	}
}
