using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Asa.Bar.App.Configuration;
using Asa.Bar.App.Forms;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Bar.App.BarItems
{
	class LocalFileShortcut : TabGroupItem
	{
		private string _path;

		protected override LinkType Type
		{
			get { return LinkType.LocalFile; }
		}

		public LocalFileShortcut(string configPath) : base(configPath) { }

		protected override void Init(string configContent)
		{
			base.Init(configContent);
			_path = ConfigHelper.GetValuesRegex("<path>(.*)</path>", configContent).FirstOrDefault();
		}

		protected override void OpenLinkInternal()
		{
			var buffer = new byte[1024 * 1024]; // 1MB buffer

			var sourceFile = new FileInfo(_path);
			var fileName = Path.GetFileName(_path);
			var destinationPath = Path.Combine(ResourceManager.Instance.TempFolder.LocalPath, fileName);
			var destinationFile = new FileInfo(destinationPath);

			if (!sourceFile.Exists)
			{
				PopupMessageHelper.Instance.ShowWarning(String.Format("You do not have a network connection to this file.{0}Connect to your network, and then try again.", Environment.NewLine));
				return;
			}

			if (!(destinationFile.Exists && destinationFile.LastWriteTime >= sourceFile.LastWriteTime && destinationFile.Length == sourceFile.Length))
			{
				if (File.Exists(destinationPath))
					File.Delete(destinationPath);
				FormProgress.SetTitle("Loading file...", true);
				FormProgress.ShowProgress();
				var thread = new Thread(() =>
				{
					using (var source = new FileStream(_path, FileMode.Open, FileAccess.Read))
					{
						var fileLength = source.Length;
						using (var dest = new FileStream(destinationPath, FileMode.CreateNew, FileAccess.Write))
						{
							decimal totalBytes = 0;
							int currentBlockSize;

							while ((currentBlockSize = source.Read(buffer, 0, buffer.Length)) > 0)
							{
								totalBytes += currentBlockSize;
								dest.Write(buffer, 0, currentBlockSize);
								FileStorageManager.Instance.ShowDownloadProgress(new FileProcessingProgressEventArgs(fileName, fileLength, totalBytes));
							}
						}
					}
				});
				thread.Start();
				while (thread.IsAlive)
					Application.DoEvents();
				FormProgress.CloseProgress();
			}
			try
			{
				Process.Start(destinationPath);
			}
			catch { }
		}
	}
}
