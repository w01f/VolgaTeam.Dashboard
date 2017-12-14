using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using Asa.Common.Core.Configuration;

namespace Asa.Common.Core.Helpers
{
	public class PopupMessageHelper
	{
		public static PopupMessageHelper Instance { get; } = new PopupMessageHelper();

		private string _title;

		public string Title
		{
			get => _title ?? SettingsManager.Instance.DashboardName;
			set => _title = value;
		}

		public Form MainForm { get; set; }

		private PopupMessageHelper() { }

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
			using (new CenterWinDialog(MainForm))
			{
				MessageBox.Show(MainForm, text, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}

		private DialogResult ShowWarningQuestion(string text, string title, params object[] args)
		{
			using (new CenterWinDialog(MainForm))
			{
				return MessageBox.Show(MainForm, String.Format(text, args), title, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
			}
		}

		private void ShowInformation(string text, string title)
		{
			using (new CenterWinDialog(MainForm))
			{
				MessageBox.Show(MainForm, text, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}

	class CenterWinDialog : IDisposable
	{
		private int mTries = 0;
		private Form mOwner;

		public CenterWinDialog(Form owner)
		{
			mOwner = owner;
			owner?.BeginInvoke(new MethodInvoker(findDialog));
		}

		private void findDialog()
		{
			// Enumerate windows to find the message box
			if (mTries < 0) return;
			EnumThreadWndProc callback = new EnumThreadWndProc(checkWindow);
			if (EnumThreadWindows(GetCurrentThreadId(), callback, IntPtr.Zero))
			{
				if (++mTries < 10) mOwner?.BeginInvoke(new MethodInvoker(findDialog));
			}
		}
		private bool checkWindow(IntPtr hWnd, IntPtr lp)
		{
			if (mOwner == null) return false;
			// Checks if <hWnd> is a dialog
			StringBuilder sb = new StringBuilder(260);
			GetClassName(hWnd, sb, sb.Capacity);
			if (sb.ToString() != "#32770") return true;
			// Got it
			Rectangle frmRect = new Rectangle(mOwner.Location, mOwner.Size);
			RECT dlgRect;
			GetWindowRect(hWnd, out dlgRect);
			MoveWindow(hWnd,
				frmRect.Left + (frmRect.Width - dlgRect.Right + dlgRect.Left) / 2,
				frmRect.Top + (frmRect.Height - dlgRect.Bottom + dlgRect.Top) / 2,
				dlgRect.Right - dlgRect.Left,
				dlgRect.Bottom - dlgRect.Top, true);
			return false;
		}
		public void Dispose()
		{
			mTries = -1;
		}

		// P/Invoke declarations
		private delegate bool EnumThreadWndProc(IntPtr hWnd, IntPtr lp);
		[DllImport("user32.dll")]
		private static extern bool EnumThreadWindows(int tid, EnumThreadWndProc callback, IntPtr lp);
		[DllImport("kernel32.dll")]
		private static extern int GetCurrentThreadId();
		[DllImport("user32.dll")]
		private static extern int GetClassName(IntPtr hWnd, StringBuilder buffer, int buflen);
		[DllImport("user32.dll")]
		private static extern bool GetWindowRect(IntPtr hWnd, out RECT rc);
		[DllImport("user32.dll")]
		private static extern bool MoveWindow(IntPtr hWnd, int x, int y, int w, int h, bool repaint);
		private struct RECT { public int Left; public int Top; public int Right; public int Bottom; }
	}
}
