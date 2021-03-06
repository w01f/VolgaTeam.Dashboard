﻿using System.Diagnostics;
using System.IO;
using System.Linq;
using Asa.Bar.App.Configuration;

namespace Asa.Bar.App.BarItems
{
	class SfxShortcut : TabGroupItem
	{
		private string _sfxFilePath;

		protected override LinkType Type => LinkType.Sfx;

		public SfxShortcut(string configPath) : base(configPath) { }

		protected override void Init(string configContent)
		{
			base.Init(configContent);
			var sfxFileName = ConfigHelper.GetValuesRegex("<path>(.*)</path>", configContent).FirstOrDefault();
			_sfxFilePath = Path.Combine(Path.GetDirectoryName(_configPath), sfxFileName);
		}

		protected override void OpenLinkInternal()
		{
			try
			{
				Process.Start(_sfxFilePath);
			}
			catch { }
		}
	}
}
