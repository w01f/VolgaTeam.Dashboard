using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml;
using Asa.Common.Core.Configuration;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Objects.Output;
using Asa.Common.Core.Objects.RemoteStorage;
using Asa.Common.Core.OfficeInterops;

namespace Asa.Common.Core.Helpers
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
			if (_powerPointHelper.Connect(false))
			{
				SettingsSource = SettingsSourceEnum.PowerPoint;
				GetActiveSettings();
			}
			else
			{
				KillPowerPoint();
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
			KillPowerPoint();

			var launcherTemplate = new StorageFile(ResourceManager.Instance.LauncherTemplatesFolder.RelativePathParts.Merge(SlideSettings.LauncherTemplateName));
			if (!launcherTemplate.ExistsLocal())
				throw new FileNotFoundException(String.Format("There is no {0} found", launcherTemplate.Name));

			var process = new Process();
			process.StartInfo.FileName = launcherTemplate.LocalPath;
			process.StartInfo.UseShellExecute = true;
			process.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
			process.Start();
		}

		private void KillPowerPoint()
		{
			Process.GetProcesses().Where(p => p.ProcessName.ToUpper().Contains("POWERPNT")).ToList().ForEach(p => p.Kill());
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
}
