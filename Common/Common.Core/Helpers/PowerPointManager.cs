using System;
using System.Diagnostics;
using System.Linq;
using Asa.Common.Core.Enums;
using Asa.Common.Core.OfficeInterops;

namespace Asa.Common.Core.Helpers
{
	public class PowerPointManager<TProcessor> where TProcessor : PowerPointProcessor
	{
		public TProcessor Processor { get; }

		public SettingsSourceEnum SettingsSource { get; private set; }

		public PowerPointManager()
		{
			Processor = Activator.CreateInstance<TProcessor>();
		}

		public void Init()
		{
			SlideFormatParser.LoadAvailableFormats();
			if (Processor.Connect())
			{
				SettingsSource = SettingsSourceEnum.PowerPoint;
				SlideSettingsManager.Instance.GetActiveSettings(Processor);
			}
			else
			{
				KillPowerPoint();
				SettingsSource = SettingsSourceEnum.Application;
				SlideSettingsManager.Instance.GetDefaultSettings();
			}
		}

		public void RunPowerPointLoader()
		{
			KillPowerPoint();

			var launcherTemplatePath = SlideSettingsManager.Instance.GetLauncherTemplatePath();

			var process = new Process
			{
				StartInfo =
				{
					FileName = launcherTemplatePath,
					UseShellExecute = true,
					WindowStyle = ProcessWindowStyle.Maximized
				}
			};
			process.Start();
		}

		public bool IsPowerPointMultipleInstances()
		{
			if (Process.GetProcesses().Count(p => p.ProcessName.ToUpper().Contains("POWERPNT")) > 1)
				return true;
			try
			{
				if (!Processor.Connect(false))
					return false;
				return Processor.PowerPointObject.Presentations.Count > 1;
			}
			catch
			{
				return false;
			}
		}

		private void KillPowerPoint()
		{
			try
			{
				Process.GetProcesses().Where(p => p.ProcessName.ToUpper().Contains("POWERPNT")).ToList().ForEach(p => p.Kill());
			}
			catch
			{
			}
		}
	}
}
