using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;

namespace Asa.Core.Common
{
	public class SpecialLinkButton
	{
		public string Name { get; set; }
		public string Type { get; set; }
		public string Tooltip { get; set; }
		public Image Logo { get; set; }
		public List<string> Paths { get; private set; }

		public SpecialLinkButton()
		{
			Name = String.Empty;
			Type = String.Empty;
			Paths = new List<string>();
		}

		public void Open()
		{
			try
			{
				var process = new Process();
				if (Type == "URL")
				{
					var path = Paths.FirstOrDefault();
					if (path != null)
					{
						process.StartInfo.Arguments = path;
						process.StartInfo.FileName = "iexplore.exe";
						foreach (var browser in OnlineSchedule.ListManager.Instance.SpecialLinkBrowsers)
						{
							if (browser == "Chrome" && Utilities.Instance.ChromeInstalled)
							{
								process.StartInfo.FileName = "chrome.exe";
								break;
							}
							if (browser == "FF" && Utilities.Instance.FirefoxInstalled)
							{
								process.StartInfo.FileName = "firefox.exe";
								break;
							}
						}
					}
				}
				else if (Paths.Any(p => p.Contains("%SpecialApps%")))
				{
					process.StartInfo.FileName = Paths.First().Replace("%SpecialApps%",ResourceManager.Instance.SpecialAppsFolder.LocalPath);
				}
				else
					process.StartInfo.FileName = Paths.FirstOrDefault(p => File.Exists(p));
				process.Start();
			}
			catch { }
		}
	}
}
