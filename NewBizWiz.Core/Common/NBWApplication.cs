using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using DevComponents.DotNetBar;

namespace NewBizWiz.Core.Common
{
	public class NBWApplication
	{
		public const string NBWApplicationManifestFileName = "Manifest.xml";

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
					DisabledImage = Utilities.Instance.MakeGrayscale(Image as Bitmap);

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

		public event EventHandler<EventArgs> OnCreateShorcut;
		public event EventHandler<EventArgs> OnRun;

		private void AppButton_MouseDown(object sender, MouseEventArgs e)
		{
			if (!AppButton.Enabled)
				MessageBox.Show("This App REQUIRES a Different slide background…\nChoose a different slide background on the Minibar PowerPoint tab…", "This App REQUIRES a Different slide background…", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

		public void LoadManifest()
		{
			XmlNode node;
			bool tempBool;
			if (File.Exists(Path.Combine(RootFolder.FullName, NBWApplicationManifestFileName)))
			{
				var document = new XmlDocument();
				document.Load(Path.Combine(RootFolder.FullName, NBWApplicationManifestFileName));
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
