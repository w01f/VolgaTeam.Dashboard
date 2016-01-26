using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Asa.Bar.App.Common;
using Asa.Bar.App.Configuration;
using Asa.Bar.App.Forms;
using Asa.Core.Common;
using Asa.Core.Interop;
using ResourceManager = Asa.Bar.App.Configuration.ResourceManager;

namespace Asa.Bar.App.BarItems
{
	class CloudFileShortcut : TabGroupItem
	{
		private StorageFile _file;

		protected override LinkType Type
		{
			get { return LinkType.CloudFile; }
		}

		public CloudFileShortcut(string configPath) : base(configPath) { }

		protected override void Init(string configContent)
		{
			base.Init(configContent);
			var path = ConfigHelper.GetValuesRegex("<path>(.*)</path>", configContent).FirstOrDefault();
			_file = new StorageFile(ResourceManager.Instance.CloudFilesFolder.RelativePathParts.Merge(path.Split(Convert.ToChar(@"\"))));
		}

		protected override void OpenLinkInternal()
		{
			if (FileStorageManager.Instance.UseLocalMode)
			{
				Utilities.Instance.ShowWarning("You are not connected to the internet. Check your web connection and try again.");
				return;
			}
			FormProgress.SetTitle("Loading file...", true);
			FormProgress.ShowProgress();
			var thread = new Thread(() => AsyncHelper.RunSync(async () =>
			{
				if (await _file.Exists(true, true))
					await _file.Download(true);
			}));
			thread.Start();
			while (thread.IsAlive)
				Application.DoEvents();
			FormProgress.CloseProgress();
			if (_file.ExistsLocal())
			{
				try
				{
					Process.Start(_file.LocalPath);
				}
				catch { }
			}
			else
				Utilities.Instance.ShowWarning("File not found");
		}
	}
}
