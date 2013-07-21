using System;
using System.Windows.Forms;
using NewBizWiz.Core.Common;

namespace NewBizWiz.Reset
{
	public class AppManager
	{
		private static readonly AppManager _instance = new AppManager();

		private AppManager()
		{
			HelpManager = new HelpManager(String.Format(@"{0}\newlocaldirect.com\app\HelpUrls\SetupHelp.xml", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)));
		}

		public HelpManager HelpManager { get; private set; }

		public static AppManager Instance
		{
			get { return _instance; }
		}

		public void RunForm()
		{
			Application.Run(new FormMain());
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