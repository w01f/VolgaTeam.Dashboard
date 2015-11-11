using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Asa.Bar.App.Common;
using Asa.Bar.App.Configuration;

namespace Asa.Bar.App.BarItems
{
	abstract class TabGroupItem
	{
		protected readonly string _configPath;
		protected abstract LinkType Type { get; }
		public string Title { get; private set; }
		public Image Image { get; private set; }
		public string Tooltip { get; private set; }
		public string Password { get; private set; }
		public bool UserGranted { get; private set; }

		protected TabGroupItem(string configPath)
		{
			_configPath = configPath;
			var configContent = ConfigHelper.GetTextFromFile(_configPath);
			Init(configContent);
		}

		protected virtual void Init(string configContent)
		{

			//var t = new TabGroupItem(,
			//	type == "url" ? LinkType.Url : (type == "exe" ? LinkType.Exe : LinkType.Any), configFile);

			Title = ConfigHelper.GetValueRegex("<title>(.*?)</title>", configContent);

			var imagePath = Path.ChangeExtension(_configPath, "png");
			Image = File.Exists(imagePath) ? Image.FromFile(imagePath) : null;

			Tooltip = ConfigHelper.GetValueRegex("<tooltip>(.*?)</tooltip>", configContent);

			Password = ConfigHelper.GetValueRegex("<pincode>(.*?)</pincode>", configContent);

			UserGranted = !configContent.Contains("approveduser") ||
						  ConfigHelper.GetValuesRegex("<approveduser>(.*?)</approveduser>", configContent)
						  .Any(user => user.Equals(Environment.UserName, StringComparison.OrdinalIgnoreCase));
		}

		public static TabGroupItem Create(string configPath)
		{
			var configContent = ConfigHelper.GetTextFromFile(configPath);
			var typeTag = ConfigHelper.GetValueRegex("<type>(.*)</type>", configContent);
			switch (typeTag)
			{
				case "url":
					return new UrlShortcut(configPath);
				case "exe":
					return new ExecutableShortcut(configPath);
				case "sfx":
					return new SfxShortcut(configPath);
				default:
					throw new ArgumentOutOfRangeException("Link type is not recognized");
			}
		}

		public void Open()
		{
			var stotedCursor = Cursor.Current;
			Cursor.Current = Cursors.WaitCursor;
			OpenLinkInternal();
			Cursor.Current = stotedCursor;
		}

		protected abstract void OpenLinkInternal();

		private static void Start(string path, string args = "", bool useShellExecute = false)
		{
			try
			{
				Process.Start(new ProcessStartInfo(path, args) { UseShellExecute = useShellExecute });
			}
			catch
			{
			}
		}
	}
}
