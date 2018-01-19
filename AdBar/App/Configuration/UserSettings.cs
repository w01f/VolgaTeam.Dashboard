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
		public int? FormLocationLeft { get; set; }
		public int? FormLocationTop { get; set; }

		public ColorEx? DefaultAccentColor { get; set; }

		public ColorEx? UserAccentColor { get; set; }

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
			PreferedMonitor = 0;
			SelectedBrowser = null;
			LoadAtStartup = true;
			UseDockedStyle = true;
		}
	}
}
