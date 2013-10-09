using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using DevComponents.DotNetBar;
using vbAccelerator.Components.Shell;

namespace NewBizWiz.Core.Common
{
	public abstract class NBWLink
	{
		public static string ManifestFileName = "Manifest.xml";

		protected NBWLink(DirectoryInfo rootFolder)
		{
			Order = 9999;
			TabOrder = 1;
			RootFolder = rootFolder;
		}

		public DirectoryInfo RootFolder { get; set; }
		public string Title { get; set; }
		public int TabOrder { get; set; }
		public int Order { get; set; }
		public Image Image { get; set; }
		public Image DisabledImage { get; set; }
		public string Icon { get; set; }
		public string AccessCode { get; set; }

		public ButtonItem AppButton { get; set; }
		public event EventHandler<EventArgs> OnCreateShorcut;
		public event EventHandler<EventArgs> OnRun;

		public abstract string LinkPath { get; }

		public virtual bool IsConfigured
		{
			get { return !String.IsNullOrEmpty(LinkPath) && Image != null && Order != 999; }
		}

		public virtual IEnumerable<BaseItem> Controls
		{
			get
			{
				return new[] { AppButton };
			}
		}

		private void AppButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (!AppButton.Enabled)
				MessageBox.Show("This link REQUIRES a Different slide background…\nChoose a different slide background on the Minibar PowerPoint tab…", "This App REQUIRES a Different slide background…", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void AppButton_Click(object sender, EventArgs e)
		{
			if (OnRun != null)
				OnRun(sender, e);
		}

		public void CreateShortcut()
		{
			if (OnCreateShorcut != null)
				OnCreateShorcut(this, EventArgs.Empty);
		}

		public void Load(XmlDocument manifest)
		{
			if (!RootFolder.Exists) return;
			Init(manifest);
			InitControls();
		}


		protected virtual void Init(XmlDocument manifest)
		{
			XmlNode node;
			int tempInt;
			node = manifest.SelectSingleNode(@"/Manifest/Title");
			if (node != null)
				Title = node.InnerText;
			node = manifest.SelectSingleNode(@"/Manifest/Image");
			if (node != null && File.Exists(Path.Combine(RootFolder.FullName, node.InnerText)))
				Image = new Bitmap(Path.Combine(RootFolder.FullName, node.InnerText));
			node = manifest.SelectSingleNode(@"/Manifest/Icon");
			if (node != null && File.Exists(Path.Combine(RootFolder.FullName, node.InnerText)))
				Icon = Path.Combine(RootFolder.FullName, node.InnerText);
			node = manifest.SelectSingleNode(@"/Manifest/Order");
			if (node != null)
				Order = Int32.TryParse(node.InnerText, out tempInt) ? tempInt : 9999;
			node = manifest.SelectSingleNode(@"/Manifest/AppsTab");
			if (node != null)
				TabOrder = Int32.TryParse(node.InnerText.ToLower().Replace("apps", ""), out tempInt) ? tempInt : 1;
			node = manifest.SelectSingleNode(@"/Manifest/Password");
			if (node != null)
				AccessCode = node.InnerText;
		}

		protected virtual void InitControls()
		{
			if (!IsConfigured) return;
			AppButton = new ButtonItem
			{
				Visible = true,
				Image = Image,
				DisabledImage = DisabledImage,
				Tag = LinkPath
			};
			AppButton.Click += AppButton_Click;
			AppButton.MouseDown += AppButton_MouseDown;
		}

		public abstract void CreateShorcut();
		public abstract void Run();

		public static NBWLink CreateLink(DirectoryInfo rootFolder)
		{
			if (!rootFolder.Exists) return null;
			if (!File.Exists(Path.Combine(rootFolder.FullName, ManifestFileName))) return null;
			var manifest = new XmlDocument();
			manifest.Load(Path.Combine(rootFolder.FullName, ManifestFileName));
			var nodeLinkType = manifest.SelectSingleNode(@"/Manifest/LinkType");
			if (nodeLinkType == null) return null;
			NBWLinkType linkType;
			if (!Enum.TryParse(nodeLinkType.InnerText, true, out linkType)) return null;
			NBWLink link = null;
			switch (linkType)
			{
				case NBWLinkType.App:
					link = new NBWApplication(rootFolder);
					break;
				case NBWLinkType.Url:
					link = new NBWUrl(rootFolder);
					break;
				case NBWLinkType.SimpleFile:
					link = new NBWSimpleFile(rootFolder);
					break;
				case NBWLinkType.SyncedFile:
					link = new NBWSyncedFile(rootFolder);
					break;
			}
			if (link != null)
				link.Load(manifest);
			return link;
		}
	}

	public class NBWApplication : NBWLink
	{
		private string _executablePath = String.Empty;

		public bool UseWizard { get; set; }
		public bool UseSlideTemplates { get; set; }
		public string SlideTemplatesPath { get; set; }

		public override string LinkPath
		{
			get { return _executablePath; }
		}

		public override bool IsConfigured
		{
			get { return base.IsConfigured && DisabledImage != null && File.Exists(LinkPath); }
		}

		protected internal NBWApplication(DirectoryInfo rootFolder)
			: base(rootFolder)
		{
			SlideTemplatesPath = String.Empty;
		}

		protected override void Init(XmlDocument manifest)
		{
			base.Init(manifest);

			XmlNode node;
			bool tempBool;
			node = manifest.SelectSingleNode(@"/Manifest/Executable");
			if (node != null && File.Exists(Path.Combine(RootFolder.FullName, node.InnerText)))
				_executablePath = Path.Combine(RootFolder.FullName, node.InnerText);
			node = manifest.SelectSingleNode(@"/Manifest/SlideTemplatesPath");
			if (node != null)
			{
				UseSlideTemplates = true;
				SlideTemplatesPath = node.InnerText;
			}
			node = manifest.SelectSingleNode(@"/Manifest/UseWizard");
			if (node != null)
			{
				if (Boolean.TryParse(node.InnerText, out tempBool))
					UseWizard = tempBool;
			}

			if (File.Exists(Path.Combine(RootFolder.FullName, "disabled.png")))
				DisabledImage = new Bitmap(Path.Combine(RootFolder.FullName, "disabled.png"));
			else if (Image != null)
				DisabledImage = Utilities.Instance.MakeGrayscale(Image as Bitmap);
		}

		public override void CreateShorcut()
		{
			using (var shortcut = new ShellLink())
			{
				shortcut.Target = LinkPath;
				shortcut.Description = Title.Replace("\n", " ").Replace("\r", string.Empty);
				shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
				if (File.Exists(Icon))
					shortcut.IconPath = Icon;
				shortcut.IconIndex = 0;
				shortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Title.Replace("\n", " ").Replace("\r", string.Empty) + ".lnk"));
			}
		}

		public override void Run()
		{
			try
			{
				var process = new Process
				{
					StartInfo =
					{
						FileName = LinkPath,
					}
				};
				process.Start();
			}
			catch { }
		}
	}

	public class NBWUrl : NBWLink
	{
		private Uri _url;
		private BrowserType _browserType;

		private string BrowserPath
		{
			get
			{
				switch (_browserType)
				{
					case BrowserType.Chrome:
						return "chrome.exe";
					case BrowserType.Firefox:
						return "firefox.exe";
					case BrowserType.Opera:
						return "opera.exe";
					case BrowserType.Safari:
						return "safari.exe";
					case BrowserType.IE:
						return "iexplore.exe";
					default:
						return String.Empty;
				}
			}
		}

		public override string LinkPath
		{
			get { return _url == null ? String.Empty : _url.AbsoluteUri; }
		}

		protected internal NBWUrl(DirectoryInfo rootFolder)
			: base(rootFolder)
		{
			_browserType = BrowserType.Default;
		}

		protected override void Init(XmlDocument manifest)
		{
			base.Init(manifest);

			XmlNode node;
			node = manifest.SelectSingleNode(@"/Manifest/Url");
			if (node != null)
				Uri.TryCreate(node.InnerText, UriKind.Absolute, out _url);

			foreach (var browserNode in manifest.SelectNodes(@"/Manifest/Browser").OfType<XmlNode>())
			{
				BrowserType temp;
				if (Enum.TryParse(browserNode.InnerText, true, out temp))
				{
					switch (temp)
					{
						case BrowserType.Chrome:
							if (Utilities.Instance.ChromeInstalled)
								_browserType = temp;
							break;
						case BrowserType.Firefox:
							if (Utilities.Instance.FirefoxInstalled)
								_browserType = temp;
							break;
						case BrowserType.Opera:
							if (Utilities.Instance.OperaInstalled)
								_browserType = temp;
							break;
					}
					_browserType = temp;
				}
				if (_browserType != BrowserType.Default) break;
			}
		}

		public override void CreateShorcut()
		{
			if (_browserType != BrowserType.Default && !String.IsNullOrEmpty(LinkPath))
				using (var shortcut = new ShellLink())
				{
					shortcut.Target = LinkPath;
					shortcut.Description = Title.Replace("\n", " ").Replace("\r", string.Empty);
					shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
					if (File.Exists(Icon))
						shortcut.IconPath = Icon;
					shortcut.IconIndex = 0;
					shortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Title.Replace("\n", " ").Replace("\r", string.Empty) + ".lnk"));
				}
			else
				using (var writer = new StreamWriter(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Title.Replace("\n", " ").Replace("\r", string.Empty) + ".url")))
				{
					writer.WriteLine("[InternetShortcut]");
					writer.WriteLine("URL={0}", LinkPath);
					writer.WriteLine("IconFile=\"{0}\"", Icon);
					writer.WriteLine("IconIndex=0");
					writer.Flush();
				}
		}

		public override void Run()
		{
			if (_browserType != BrowserType.Default && !String.IsNullOrEmpty(BrowserPath))
				try
				{
					var process = new Process
					{
						StartInfo =
						{
							FileName = BrowserPath,
							Arguments = LinkPath,
						}
					};
					process.Start();
				}
				catch { }
			else
				try
				{
					var process = new Process
					{
						StartInfo =
						{
							FileName = LinkPath,
						}
					};
					process.Start();
				}
				catch { }
		}
	}

	public class NBWSimpleFile : NBWLink
	{
		private string _path;

		public override string LinkPath
		{
			get { return _path; }
		}

		public override bool IsConfigured
		{
			get { return base.IsConfigured && File.Exists(LinkPath); }
		}

		protected internal NBWSimpleFile(DirectoryInfo rootFolder) : base(rootFolder) { }

		protected override void Init(XmlDocument manifest)
		{
			base.Init(manifest);

			XmlNode node;
			node = manifest.SelectSingleNode(@"/Manifest/Path");
			if (node != null)
				_path = node.InnerText;
		}

		public override void CreateShorcut()
		{
			using (var shortcut = new ShellLink())
			{
				shortcut.Target = LinkPath;
				shortcut.Description = Title.Replace("\n", " ").Replace("\r", string.Empty);
				shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
				if (File.Exists(Icon))
					shortcut.IconPath = Icon;
				shortcut.IconIndex = 0;
				shortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Title.Replace("\n", " ").Replace("\r", string.Empty) + ".lnk"));
			}
		}

		public override void Run()
		{
			try
			{
				var process = new Process
				{
					StartInfo =
					{
						FileName = LinkPath,
					}
				};
				process.Start();
			}
			catch { }
		}
	}

	public class NBWSyncedFile : NBWLink
	{
		private string _path;

		public override string LinkPath
		{
			get { return _path; }
		}

		public override bool IsConfigured
		{
			get { return base.IsConfigured && File.Exists(LinkPath); }
		}

		protected internal NBWSyncedFile(DirectoryInfo rootFolder) : base(rootFolder) { }

		protected override void Init(XmlDocument manifest)
		{
			base.Init(manifest);

			XmlNode node;
			node = manifest.SelectSingleNode(@"/Manifest/FileName");
			if (node != null && File.Exists(Path.Combine(RootFolder.FullName, node.InnerText)))
				_path = Path.Combine(RootFolder.FullName, node.InnerText);
		}

		public override void CreateShorcut()
		{
			using (var shortcut = new ShellLink())
			{
				shortcut.Target = LinkPath;
				shortcut.Description = Title.Replace("\n", " ").Replace("\r", string.Empty);
				shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
				if (File.Exists(Icon))
					shortcut.IconPath = Icon;
				shortcut.IconIndex = 0;
				shortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Title.Replace("\n", " ").Replace("\r", string.Empty) + ".lnk"));
			}
		}

		public override void Run()
		{
			try
			{
				var process = new Process
				{
					StartInfo =
					{
						FileName = LinkPath,
					}
				};
				process.Start();
			}
			catch { }
		}
	}
}
