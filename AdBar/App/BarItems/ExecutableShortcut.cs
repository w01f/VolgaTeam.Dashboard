using System;
using System.Diagnostics;
using System.Linq;
using Asa.Bar.App.Common;
using Asa.Bar.App.Configuration;

namespace Asa.Bar.App.BarItems
{
	class ExecutableShortcut:TabGroupItem
	{
		private string _executablePath;

		protected override LinkType Type => LinkType.Exe;

		public ExecutableShortcut(string configPath) : base(configPath) {}

		protected override void Init(string configContent)
		{
			base.Init(configContent);
			_executablePath = ConfigHelper.GetValuesRegex("<path>(.*)</path>", configContent).FirstOrDefault();

			if (_executablePath.Contains("%SpecialApps%"))
				_executablePath = _executablePath.Replace("%SpecialApps%", ResourceManager.Instance.SpecialAppsFolder.LocalPath);
			else
				_executablePath = Environment.ExpandEnvironmentVariables(_executablePath);

		}

		protected override void OpenLinkInternal()
		{
			try
			{
				Process.Start(_executablePath);
			}
			catch { }
		}
	}
}
