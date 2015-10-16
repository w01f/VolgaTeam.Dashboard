using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml;
using NewBizWiz.Core.Interop;

namespace NewBizWiz.Core.Common
{
	public class PowerPointManager
	{
		private static readonly PowerPointManager _instance = new PowerPointManager();
		private IPowerPointHelper _powerPointHelper;

		public static PowerPointManager Instance
		{
			get { return _instance; }
		}

		public SettingsSourceEnum SettingsSource { get; private set; }
		public SlideSettings SlideSettings { get; private set; }

		public event EventHandler<SlideSettingsChangingEventArgs> SettingsChanging;
		public event EventHandler<EventArgs> SettingsChanged;

		private PowerPointManager()
		{
			SlideSettings = new SlideSettings();
		}

		public void Init(IPowerPointHelper powerPointHelper)
		{
			_powerPointHelper = powerPointHelper;
			if (_powerPointHelper.IsLinkedWithApplication)
			{
				SettingsSource = SettingsSourceEnum.PowerPoint;
				GetActiveSettings();
			}
			else
			{
				SettingsSource = SettingsSourceEnum.Application;
				GetDefaultSettings();
			}
		}

		public void ApplySettings(SlideSettings newSettings)
		{
			var args = new SlideSettingsChangingEventArgs();
			if (SettingsChanging != null)
				SettingsChanging(this, args);
			if (args.Cancel) return;
			SlideSettings = newSettings;
			_powerPointHelper.SetSlideSettings(SlideSettings);
			if (SettingsChanged != null)
				SettingsChanged(this, EventArgs.Empty);
		}

		public void RunPowerPointLoader()
		{
			var launcherTemplate = new StorageFile(ResourceManager.Instance.LauncherTemplatesFolder.RelativePathParts.Merge(SlideSettings.LauncherTemplateName));
			if (!launcherTemplate.ExistsLocal())
				throw new FileNotFoundException(String.Format("There is no {0} found", launcherTemplate.Name));

			var process = new Process();
			process.StartInfo.FileName = launcherTemplate.LocalPath;
			process.StartInfo.UseShellExecute = true;
			process.Start();
		}

		private void GetDefaultSettings()
		{
			if (!ResourceManager.Instance.DefaultSlideSettingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(ResourceManager.Instance.DefaultSlideSettingsFile.LocalPath);
			var node = document.SelectSingleNode(@"/Settings/Size");
			if (node != null)
				SlideSettings = SlideSettings.ReadFromString(node.InnerText.Trim());
		}

		private void GetActiveSettings()
		{
			SlideSettings = _powerPointHelper.GetSlideSettings() ?? SlideSettings;
		}
	}

	public enum SettingsSourceEnum
	{
		PowerPoint,
		Application
	}

	public enum SlideOrientationEnum
	{
		Landscape,
		Portrait
	}

	public class SlideSettings
	{
		public double SizeHeght { get; set; }
		public double SizeWidth { get; set; }
		public SlideOrientationEnum Orientation { get; set; }

		public string SizeFormatted
		{
			get
			{
				switch (Orientation)
				{
					case SlideOrientationEnum.Landscape:
						if (SizeWidth == 10 && SizeHeght == 7.5)
							return "4 x 3";
						if (SizeWidth == 10.75 && SizeHeght == 8.25)
							return "5 x 4";
						if (SizeWidth == 13 && SizeHeght == 7.32)
							return "16 x 9";
						return "4 x 3";
					case SlideOrientationEnum.Portrait:
						if (SizeWidth == 7.5 && SizeHeght == 10)
							return "3 x 4";
						if (SizeWidth == 8.25 && SizeHeght == 10.75)
							return "4 x 5";
						if (SizeWidth == 7.32 && SizeHeght == 13)
							return "9 x 16";
						return "4 x 3";
					default:
						return "4 x 3";
				}
			}
		}

		public string SlideFolder
		{
			get
			{
				switch (Orientation)
				{
					case SlideOrientationEnum.Landscape:
						if (SizeWidth == 10 && SizeHeght == 7.5)
							return "Slides43";
						if (SizeWidth == 10.75 && SizeHeght == 8.25)
							return "Slides54";
						if (SizeWidth == 13 && SizeHeght == 7.32)
							return "Slides169";
						return "Slides43";
					case SlideOrientationEnum.Portrait:
						if (SizeWidth == 7.5 && SizeHeght == 10)
							return "Slides34";
						if (SizeWidth == 8.25 && SizeHeght == 10.75)
							return "Slides45";
						if (SizeWidth == 7.32 && SizeHeght == 13)
							return "Slides916";
						return "Slides43";
					default:
						return "Slides43";
				}
			}
		}

		public string SlideMasterFolder
		{
			get { return SizeFormatted.Replace(" ", ""); }
		}

		public string LauncherTemplateName
		{
			get { return String.Format("adSALESapps{0}.potx", SlideFolder.Replace("Slides", "")); }
		}

		public SlideSettings()
		{
			Orientation = SlideOrientationEnum.Landscape;
			SizeWidth = 10;
			SizeHeght = 7.5;
		}

		public bool IsEqual(SlideSettings target)
		{
			return target.SizeWidth == SizeWidth && target.SizeHeght == SizeHeght && target.Orientation == Orientation;
		}

		public static SlideSettings ReadFromString(string size)
		{
			switch (size)
			{
				case "4x3":
					return new SlideSettings
					{
						Orientation = SlideOrientationEnum.Landscape,
						SizeWidth = 10,
						SizeHeght = 7.5
					};
				case "3x4":
					return new SlideSettings
					{
						Orientation = SlideOrientationEnum.Portrait,
						SizeWidth = 7.5,
						SizeHeght = 10
					};
				case "5x4":
					return new SlideSettings
					{
						Orientation = SlideOrientationEnum.Landscape,
						SizeWidth = 10.75,
						SizeHeght = 8.25
					};
				case "4x5":
					return new SlideSettings
					{
						Orientation = SlideOrientationEnum.Portrait,
						SizeWidth = 8.25,
						SizeHeght = 10.75
					};
				case "16x9":
					return new SlideSettings
					{
						Orientation = SlideOrientationEnum.Landscape,
						SizeWidth = 13,
						SizeHeght = 7.32
					};
			}
			throw new ArgumentOutOfRangeException("Can't parse slide configuration");
		}

		public static IEnumerable<SlideSettings> GetAvailableConfigurations()
		{
			return new[]
			{
				ReadFromString("4x3"),
				ReadFromString("16x9"),
				ReadFromString("3x4"),
			};
		}
	}

	public class SlideSettingsChangingEventArgs : EventArgs
	{
		public bool Cancel { get; set; }

		public SlideSettingsChangingEventArgs()
		{
			Cancel = false;
		}
	}
}
