using System;
using System.IO;
using System.Xml;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.Core.OfficeInterops;

namespace Asa.Common.Core.Helpers
{
	public class SlideSettingsManager
	{
		public SlideSettings SlideSettings { get; private set; }
		public event EventHandler<SlideSettingsChangingEventArgs> SettingsChanging;
		public event EventHandler<EventArgs> SettingsChanged;

		public static SlideSettingsManager Instance { get; } = new SlideSettingsManager();

		public SlideSettingsManager()
		{
			SlideSettings = new SlideSettings();
		}

		public void ApplySettings(SlideSettings newSettings, PowerPointProcessor powerPointProcessor)
		{
			var args = new SlideSettingsChangingEventArgs();
			SettingsChanging?.Invoke(this, args);
			if (args.Cancel) return;
			SlideSettings = newSettings;
			powerPointProcessor.SetSlideSettings(SlideSettings);
			SettingsChanged?.Invoke(this, EventArgs.Empty);
		}

		public void GetDefaultSettings()
		{
			if (!ResourceManager.Instance.DefaultSlideSettingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(ResourceManager.Instance.DefaultSlideSettingsFile.LocalPath);
			var node = document.SelectSingleNode(@"/Settings/Size");
			if (node != null)
				SlideSettings = SlideSettings.ReadFromString(node.InnerText.Trim());
		}

		public void GetActiveSettings(PowerPointProcessor powerPointProcessor)
		{
			SlideSettings = powerPointProcessor.GetActiveSlideSettings() ?? SlideSettings;
		}

		public string GetLauncherTemplatePath()
		{
			var launcherTemplate = new StorageFile(ResourceManager.Instance.LauncherTemplatesFolder.RelativePathParts.Merge(Instance.SlideSettings.LauncherTemplateName));
			if (!launcherTemplate.ExistsLocal())
				throw new FileNotFoundException(String.Format("There is no {0} found", launcherTemplate.Name));

			return launcherTemplate.LocalPath;
		}
	}
}
