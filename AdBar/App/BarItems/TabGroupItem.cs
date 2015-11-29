using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Asa.Bar.App.Configuration;

namespace Asa.Bar.App.BarItems
{
	abstract class TabGroupItem
	{
		private int _splashDelay;
		private string _splashText;

		protected readonly string _configPath;
		protected abstract LinkType Type { get; }
		public string Title { get; private set; }
		public Image Image { get; private set; }
		public string Tooltip { get; private set; }
		public string Password { get; private set; }
		public bool UserGranted { get; private set; }

		private bool SplashEnabled
		{
			get { return !String.IsNullOrEmpty(_splashText); }
		}

		protected TabGroupItem(string configPath)
		{
			_configPath = configPath;
			var configContent = ConfigHelper.GetTextFromFile(_configPath);
			Init(configContent);
		}

		protected virtual void Init(string configContent)
		{
			Title = ConfigHelper.GetValueRegex("<title>(.*?)</title>", configContent);

			var imagePath = Path.ChangeExtension(_configPath, "png");
			Image = File.Exists(imagePath) ? Image.FromFile(imagePath) : null;

			Tooltip = ConfigHelper.GetValueRegex("<tooltip>(.*?)</tooltip>", configContent);

			Password = ConfigHelper.GetValueRegex("<pincode>(.*?)</pincode>", configContent);

			_splashText = ConfigHelper.GetValueRegex("<splash_text>(.*?)</splash_text>", configContent);
			if (SplashEnabled)
				_splashDelay = Int32.Parse(ConfigHelper.GetValueRegex("<splash_delay>(.*)</splash_delay>", configContent));

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
			ShowSplash();
			OpenLinkInternal();
			Cursor.Current = stotedCursor;
		}

		protected abstract void OpenLinkInternal();

		private void ShowSplash()
		{
			if (!SplashEnabled) return;
			var formSplash = new FormSplash();
			formSplash.laTitle.Text = _splashText;
			formSplash.Shown += async (o, args) =>
			{
				await Task.Run(() => Thread.Sleep(_splashDelay * 1000));
				formSplash.Close();
			};
			formSplash.Show();
			GC.KeepAlive(formSplash);
		}
	}
}
