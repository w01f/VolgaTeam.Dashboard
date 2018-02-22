using System;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Xml;
using Asa.Common.Core.Objects.RemoteStorage;

namespace Asa.Media.Controls.BusinessClasses.Managers
{
	public class ApplicationIdleManager
	{
		private static readonly Timer IdleTimer = new Timer();

		private Form _mainForm;

		public bool Enabled { get; private set; }
		public int InactivityMinutesTimeout { get; private set; }
		public bool SaveOnClose { get; private set; }

		public EventHandler<EventArgs> BeforeCloseOnTimerExpired;

		public ApplicationIdleManager()
		{
			Enabled = false;
		}

		public void LoadSettings(StorageFile settingsFile)
		{
			if (!settingsFile.ExistsLocal()) return;
			var document = new XmlDocument();
			document.Load(settingsFile.LocalPath);

			Enabled = Boolean.Parse(document.SelectSingleNode(@"//config/EnableAutoClose")?.InnerText ?? "false");
			InactivityMinutesTimeout = Int32.Parse(document.SelectSingleNode(@"//config/InactivityTime")?.InnerText ?? "0");
			SaveOnClose = Boolean.Parse(document.SelectSingleNode(@"//config/SaveOnClose")?.InnerText ?? "false");
		}

		public void LinkToApplication(Form mainForm)
		{
			if (!Enabled || InactivityMinutesTimeout == 0) return;
			_mainForm = mainForm;

			var limf = new LeaveIdleMessageFilter();
			Application.AddMessageFilter(limf);
			Application.Idle += OnApplicationIdle;
			IdleTimer.Interval = InactivityMinutesTimeout * 60 * 1000;
			IdleTimer.Tick += OnTimerExpired;
			IdleTimer.Start();
			_mainForm.Closed += OnMainFormClosed;
		}

		private void OnApplicationIdle(Object sender, EventArgs e)
		{
			if (!IdleTimer.Enabled)
				IdleTimer.Start();
		}

		private void OnTimerExpired(object sender, EventArgs e)
		{
			if (IdleTimer.Enabled)
				IdleTimer.Stop();
			Application.Idle -= OnApplicationIdle;

			BeforeCloseOnTimerExpired?.Invoke(_mainForm, EventArgs.Empty);

			_mainForm.Close();
		}

		private void OnMainFormClosed(Object sender, EventArgs e)
		{
			IdleTimer.Stop();
			Application.Idle -= OnApplicationIdle;
		}

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		internal class LeaveIdleMessageFilter : IMessageFilter
		{
			const int WM_NCLBUTTONDOWN = 0x00A1;
			const int WM_NCLBUTTONUP = 0x00A2;
			const int WM_NCRBUTTONDOWN = 0x00A4;
			const int WM_NCRBUTTONUP = 0x00A5;
			const int WM_NCMBUTTONDOWN = 0x00A7;
			const int WM_NCMBUTTONUP = 0x00A8;
			const int WM_NCXBUTTONDOWN = 0x00AB;
			const int WM_NCXBUTTONUP = 0x00AC;
			const int WM_KEYDOWN = 0x0100;
			const int WM_KEYUP = 0x0101;
			const int WM_MOUSEMOVE = 0x0200;
			const int WM_LBUTTONDOWN = 0x0201;
			const int WM_LBUTTONUP = 0x0202;
			const int WM_RBUTTONDOWN = 0x0204;
			const int WM_RBUTTONUP = 0x0205;
			const int WM_MBUTTONDOWN = 0x0207;
			const int WM_MBUTTONUP = 0x0208;
			const int WM_XBUTTONDOWN = 0x020B;
			const int WM_XBUTTONUP = 0x020C;

			// The Messages array must be sorted due to use of Array.BinarySearch
			static readonly int[] Messages = {WM_NCLBUTTONDOWN,
				WM_NCLBUTTONUP, WM_NCRBUTTONDOWN, WM_NCRBUTTONUP, WM_NCMBUTTONDOWN,
				WM_NCMBUTTONUP, WM_NCXBUTTONDOWN, WM_NCXBUTTONUP, WM_KEYDOWN, WM_KEYUP,
				WM_LBUTTONDOWN, WM_LBUTTONUP, WM_RBUTTONDOWN, WM_RBUTTONUP,
				WM_MBUTTONDOWN, WM_MBUTTONUP, WM_XBUTTONDOWN, WM_XBUTTONUP};

			public bool PreFilterMessage(ref Message m)
			{
				if (m.Msg == WM_MOUSEMOVE)  // mouse move is high volume
					return false;
				if (!IdleTimer.Enabled)     // idling?
					return false;           // No
				if (Array.BinarySearch(Messages, m.Msg) >= 0)
					IdleTimer.Stop();
				return false;
			}
		}
	}
}
