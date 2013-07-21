using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using DevComponents.DotNetBar;
using NewBizWiz.MiniBar.ToolForms;
using vbAccelerator.Components.Shell;

namespace NewBizWiz.MiniBar.BusinessClasses
{
	public class NBWApplicationsManager
	{
		private static readonly NBWApplicationsManager _instance = new NBWApplicationsManager();

		private NBWApplicationsManager()
		{
			NBWApplications = new List<NBWApplication>();
			var nbwApplicationsRoot = new DirectoryInfo(SettingsManager.Instance.NBWApplicationsRootPath);
			if (nbwApplicationsRoot.Exists)
			{
				foreach (DirectoryInfo nbwApplicationRoot in nbwApplicationsRoot.GetDirectories())
				{
					var nbwApplication = new NBWApplication(nbwApplicationRoot);
					if (nbwApplication.IsConfigured)
						NBWApplications.Add(nbwApplication);
				}
				NBWApplications.Sort((x, y) => x.Order.CompareTo(y.Order));
			}
		}

		public List<NBWApplication> NBWApplications { get; set; }

		public static NBWApplicationsManager Instance
		{
			get { return _instance; }
		}
	}

	public class NBWApplication
	{
		public NBWApplication(DirectoryInfo rootFolder)
		{
			Title = string.Empty;
			Executable = string.Empty;
			Order = 9999;
			RootFolder = rootFolder;
			SlideTemplatesPath = string.Empty;

			if (RootFolder.Exists)
			{
				LoadManifest();

				if (File.Exists(Path.Combine(RootFolder.FullName, "disabled.png")))
					DisabledImage = new Bitmap(Path.Combine(RootFolder.FullName, "disabled.png"));
				else if (Image != null)
					DisabledImage = AppManager.Instance.MakeGrayscale(Image as Bitmap);

				IsConfigured = !string.IsNullOrEmpty(Title) && !string.IsNullOrEmpty(Executable) && Image != null && DisabledImage != null && Order != 999;
			}

			AppLabel = new LabelItem();
			AppLabel.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, ((204)));
			AppLabel.Text = Title;
			AppLabel.Click += AppButton_Click;

			AppButton = new ButtonItem();
			AppButton.Image = Image;
			AppButton.DisabledImage = DisabledImage;
			AppButton.Tag = Executable;
			AppButton.Click += AppButton_Click;
			AppButton.MouseDown += AppButton_MouseDown;

			DisabledButton = new ButtonItem();
			DisabledButton.Visible = false;
			DisabledButton.Image = DisabledImage;
		}

		public bool IsConfigured { get; set; }
		public DirectoryInfo RootFolder { get; set; }
		public string Title { get; set; }
		public string Executable { get; set; }
		public Image Image { get; set; }
		public Image DisabledImage { get; set; }
		public string Icon { get; set; }
		public int Order { get; set; }
		public bool UseWizard { get; set; }
		public bool UseSlideTemplates { get; set; }
		public string SlideTemplatesPath { get; set; }
		public string AccessCode { get; set; }
		public string TooltipOnDisable { get; set; }

		public LabelItem AppLabel { get; private set; }
		public ButtonItem AppButton { get; set; }
		public ButtonItem DisabledButton { get; set; }

		private void AppButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (!AppButton.Enabled)
				MessageBox.Show("This App REQUIRES a Different slide background…\nChoose a different slide background on the Minibar PowerPoint tab…", "This App REQUIRES a Different slide background…", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		private void AppButton_Click(object sender, EventArgs e)
		{
			string executablePath = Executable;
			if (File.Exists(Executable))
			{
				bool allowAccess = true;
				if (!string.IsNullOrEmpty(AccessCode))
				{
					allowAccess = false;
					using (var form = new FormAppCode())
					{
						var result = DialogResult.OK;
						while (result == DialogResult.OK)
						{
							result = form.ShowDialog();

							if (result == DialogResult.OK)
							{
								if (form.Code.Equals(AccessCode))
								{
									allowAccess = true;
									break;
								}
								else
									AppManager.Instance.ShowWarning("Incorrect Access Code.\nTry again");
							}
						}
					}
				}
				if (allowAccess)
					Process.Start(Executable);
			}
			ServiceDataManager.Instance.WriteActivity();
		}

		public void CreateShortcut()
		{
			string executablePath = Executable;
			if (File.Exists(Executable))
			{
				bool allowAccess = true;
				if (!string.IsNullOrEmpty(AccessCode))
				{
					allowAccess = false;
					using (var form = new FormAppCode())
					{
						var result = DialogResult.OK;
						while (result == DialogResult.OK)
						{
							result = form.ShowDialog();

							if (result == DialogResult.OK)
							{
								if (form.Code.Equals(AccessCode))
								{
									allowAccess = true;
									break;
								}
								else
									AppManager.Instance.ShowWarning("Incorrect Access Code.\nTry again");
							}
						}
					}
				}
				if (allowAccess)
				{
					using (var shortcut = new ShellLink())
					{
						shortcut.Target = Executable;
						shortcut.WorkingDirectory = Path.GetDirectoryName(Executable);
						shortcut.Description = Title.Replace("\n", " ").Replace("\r", string.Empty);
						shortcut.DisplayMode = ShellLink.LinkDisplayMode.edmNormal;
						if (File.Exists(Icon))
							shortcut.IconPath = Icon;
						shortcut.IconIndex = 0;
						shortcut.Save(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), Title.Replace("\n", " ").Replace("\r", string.Empty) + ".lnk"));
					}
				}
			}
		}

		public void LoadManifest()
		{
			XmlNode node;
			bool tempBool;
			if (File.Exists(Path.Combine(RootFolder.FullName, SettingsManager.NBWApplicationManifestFileName)))
			{
				var document = new XmlDocument();
				document.Load(Path.Combine(RootFolder.FullName, SettingsManager.NBWApplicationManifestFileName));
				node = document.SelectSingleNode(@"/Manifest/Title");
				if (node != null)
					Title = node.InnerText;
				node = document.SelectSingleNode(@"/Manifest/Executable");
				if (node != null)
					if (File.Exists(Path.Combine(RootFolder.FullName, node.InnerText)))
						Executable = Path.Combine(RootFolder.FullName, node.InnerText);
				node = document.SelectSingleNode(@"/Manifest/Image");
				if (node != null)
				{
					if (File.Exists(Path.Combine(RootFolder.FullName, node.InnerText)))
						Image = new Bitmap(Path.Combine(RootFolder.FullName, node.InnerText));
				}
				node = document.SelectSingleNode(@"/Manifest/Icon");
				if (node != null)
					if (File.Exists(Path.Combine(RootFolder.FullName, node.InnerText)))
						Icon = Path.Combine(RootFolder.FullName, node.InnerText);
				node = document.SelectSingleNode(@"/Manifest/SlideTemplatesPath");
				if (node != null)
				{
					UseSlideTemplates = true;
					SlideTemplatesPath = node.InnerText;
				}
				node = document.SelectSingleNode(@"/Manifest/UseWizard");
				if (node != null)
				{
					if (bool.TryParse(node.InnerText, out tempBool))
						UseWizard = tempBool;
				}
				node = document.SelectSingleNode(@"/Manifest/Order");
				if (node != null)
				{
					int tempInt = 9999;
					int.TryParse(node.InnerText, out tempInt);
					Order = tempInt;
				}
				node = document.SelectSingleNode(@"/Manifest/password");
				if (node != null)
					AccessCode = node.InnerText;
			}
		}
	}
}