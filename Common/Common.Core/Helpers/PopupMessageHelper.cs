using System;
using System.Windows.Forms;
using Asa.Common.Core.Configuration;

namespace Asa.Common.Core.Helpers
{
	public class PopupMessageHelper
	{
		private static readonly PopupMessageHelper _instance = new PopupMessageHelper();
		private PopupMessageHelper() { }
		public static PopupMessageHelper Instance => _instance;

		private string _title;

		public string Title
		{
			get { return _title ?? SettingsManager.Instance.DashboardName; }
			set { _title = value; }
		}

		public void ShowWarning(string text)
		{
			ShowWarning(text, Title);
		}

		public DialogResult ShowWarningQuestion(string text, params object[] args)
		{
			return ShowWarningQuestion(text, Title, args);
		}

		public void ShowInformation(string text)
		{
			ShowInformation(text, Title);
		}

		private void ShowWarning(string text, string title)
		{
			MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
		}

		private DialogResult ShowWarningQuestion(string text, string title, params object[] args)
		{
			return MessageBox.Show(String.Format(text, args), title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
		}

		private void ShowInformation(string text, string title)
		{
			MessageBox.Show(text, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
