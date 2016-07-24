using System;
using System.Diagnostics;
using System.Linq;
using Asa.Bar.App.Configuration;
using Asa.Common.Core.Extensions;
using Asa.Common.Core.Helpers;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Bar.App.BarItems
{
	class SyncFileShortcut : TabGroupItem
	{
		private StorageFile _file;

		protected override LinkType Type => LinkType.SyncFile;

		public SyncFileShortcut(string configPath) : base(configPath) { }

		protected override void Init(string configContent)
		{
			base.Init(configContent);
			var path = ConfigHelper.GetValuesRegex("<path>(.*)</path>", configContent).FirstOrDefault();
			_file = new StorageFile(ResourceManager.Instance.SyncFilesFolder.RelativePathParts.Merge(path.Split(Convert.ToChar(@"\"))));
		}

		protected override void OpenLinkInternal()
		{
			if (!_file.ExistsLocal())
			{
				PopupMessageHelper.Instance.ShowWarning("File not found");
				return;
			}
			try
			{
				Process.Start(_file.LocalPath);
			}
			catch { }
		}
	}
}
