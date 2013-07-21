using System;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using NewBizWiz.Core.Common;

namespace NewBizWiz.SyncPC
{
	public class AppManager
	{
		public const string SyncProcessFileName = "adSync5.exe";
		public const string SyncSettingsFileName = "syncfile.xml";
		public const string ConfigFileName = "config.txt";

		private static readonly AppManager _instance = new AppManager();

		private AppManager()
		{
			HelpManager = new HelpManager(String.Format(@"{0}\newlocaldirect.com\app\HelpUrls\SetupHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)));
		}

		public string SettingsFolderPath { get; set; }
		public HelpManager HelpManager { get; set; }

		public static AppManager Instance
		{
			get { return _instance; }
		}

		public bool OpenSettingForm()
		{
			bool result = false;
			if (!string.IsNullOrEmpty(SettingsFolderPath))
				result = FormMain.Instance.ShowDialog() == DialogResult.OK;
			return result;
		}

		public bool IsConfigured(string xml)
		{
			bool result = true;

			var document = new XmlDocument();
			document.LoadXml(xml);

			XmlNode node = document.SelectSingleNode(@"/Settings/MediaProperty/Path");
			string syncFolderPath = string.Empty;
			if (node != null)
				syncFolderPath = node.InnerText.Replace("\"", "");
			if (Directory.Exists(syncFolderPath))
			{
				result = result & Directory.Exists(Path.Combine(syncFolderPath, "Incoming"));
				string testFilePath = Path.Combine(syncFolderPath, "Incoming", "test.txt");
				try
				{
					using (TextWriter tw = new StreamWriter(testFilePath))
					{
						tw.WriteLine(string.Empty);
						tw.Close();
					}
					File.Delete(testFilePath);
					result = result & true;
				}
				catch
				{
					result = false;
				}
				result = result & Directory.Exists(Path.Combine(syncFolderPath, "Outgoing"));
				result = result & Directory.Exists(Path.Combine(syncFolderPath, "Outgoing", "applications"));
				result = result & Directory.Exists(Path.Combine(syncFolderPath, "Outgoing", "gallery"));
				result = result & Directory.Exists(Path.Combine(syncFolderPath, "Outgoing", "libraries"));
				result = result & Directory.Exists(Path.Combine(syncFolderPath, "Outgoing", "slides"));
				result = result & Directory.Exists(Path.Combine(syncFolderPath, "Outgoing", "update"));
			}
			else
				result = false;

			return result;
		}

		public void ShowWarning(string text)
		{
			MessageBox.Show(text, "Minibar", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		public DialogResult ShowWarningQuestion(string text)
		{
			return MessageBox.Show(text, "Minibar", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		}

		public void ShowInformation(string text)
		{
			MessageBox.Show(text, "Minibar", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}