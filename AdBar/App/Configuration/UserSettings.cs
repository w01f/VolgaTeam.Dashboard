using System;
using System.Drawing;
using System.Xml.Serialization;
using Asa.Common.Core.Helpers;

namespace Asa.Bar.App.Configuration
{
	[Serializable]
	public class UserSettings
	{
		public string SelectedBrowser { get; set; }
		public int PreferedMonitor { get; set; }
		public bool LoadAtStartup { get; set; }
		public bool UseDockedStyle { get; set; }
		public bool DefaultAlwaysExpanded { get; set; }
		public bool AlwaysExpanded { get; set; }
		public bool DefaultShowFloaterWhenUndock { get; set; }
		public bool ShowFloaterWhenUndock { get; set; }
		public int? FormLocationLeft { get; set; }
		public int? FormLocationTop { get; set; }
		public int? FloaterLocationLeft { get; set; }
		public int? FloaterLocationTop { get; set; }

		public ColorEx? DefaultAccentColor { get; set; }
		public ColorEx? DefaultTextColor { get; set; }
		public ColorEx? DefaultRibbonBarTextColor { get; set; }
		public ColorEx? DefaultRibbonBarHoverTextColor { get; set; }

		public ColorEx? UserAccentColor { get; set; }
		public ColorEx? UserTextColor { get; set; }

		public DateTime? LastPatchUpdate { get; set; }

		[XmlIgnore]
		public Color AccentColor
		{
			get => UserAccentColor ?? DefaultAccentColor ?? Color.Chocolate;
			set
			{
				if (DefaultAccentColor != value)
					UserAccentColor = value;
			}
		}

		[XmlIgnore]
		public Color TextColor
		{
			get => UserTextColor ?? DefaultTextColor ?? Color.Chocolate;
			set
			{
				if (DefaultTextColor != value)
					UserTextColor = value;
			}
		}

		public UserSettings()
		{
			Reset();
		}

		public static UserSettings Load()
		{
			return SettingsSerializeHelper.Load<UserSettings>(ResourceManager.Instance.AppSettingsFile);
		}

		public void Save()
		{
			this.Save(ResourceManager.Instance.AppSettingsFile);
		}

		private void Reset()
		{
			UserAccentColor = null;
			UserTextColor = null;
			PreferedMonitor = 0;
			SelectedBrowser = null;
			LoadAtStartup = true;
			UseDockedStyle = true;
			AlwaysExpanded = false;
			ShowFloaterWhenUndock = false;
			FormLocationLeft = null;
			FormLocationTop = null;
			FloaterLocationLeft = null;
			FloaterLocationTop = null;
		}
	}
}
