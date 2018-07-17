using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Asa.Common.Core.Helpers;
using Microsoft.Office.Interop.Outlook;
using Exception = System.Exception;

namespace Asa.Common.Core.OfficeInterops
{
	public class OutlookHelper
	{
		private static readonly OutlookHelper instance = new OutlookHelper();
		private Application _outlookObject;

		private OutlookHelper() { }

		public static OutlookHelper Instance => instance;

		public bool Open()
		{
			try
			{
				_outlookObject =
					Marshal.GetActiveObject("Application") as Application;
			}
			catch
			{
				_outlookObject = null;
			}
			if (_outlookObject == null)
			{
				try
				{
					_outlookObject = new Application();
					var folder = (_outlookObject.GetNamespace("MAPI")).GetDefaultFolder(OlDefaultFolders.olFolderInbox);
					folder.Display();
					_outlookObject.Explorers.Add(folder, OlFolderDisplayMode.olFolderDisplayNormal);
				}
				catch
				{
					return false;
				}
			}
			return true;
		}

		public void Close()
		{
			Utilities.ReleaseComObject(_outlookObject);
		}

		public void CreateMessage(string subject, string attachmentPath)
		{
			try
			{
				var handle = IntPtr.Zero;
				var processList = Process.GetProcesses();
				foreach (var process in processList.Where(x => x.ProcessName.ToLower().Contains("outlook")).Where(process => process.MainWindowHandle.ToInt32() != 0))
				{
					handle = process.MainWindowHandle;
					break;
				}
				Utilities.ActivateForm(handle, true, false);
				var mi = (MailItem)_outlookObject.CreateItem(OlItemType.olMailItem);
				mi.Attachments.Add(attachmentPath, OlAttachmentType.olByValue, 1, Path.GetFileNameWithoutExtension(attachmentPath));
				mi.Subject = subject;
				mi.Display(false);
				var count = 100000;
				handle = IntPtr.Zero;
				while (handle == IntPtr.Zero && count > 0)
				{
					handle = WinAPIHelper.FindWindow(string.Empty, subject + "- Message (HTML)");
					count--;
					System.Windows.Forms.Application.DoEvents();
				}
				if (handle != IntPtr.Zero)
					Utilities.ActivateForm(handle, true, false);
			}
			catch (Exception e)
			{
				PopupMessageHelper.Instance.ShowWarning(e.Message);
			}
		}

		public void CreateMessage(string[] attachmentPaths)
		{
			try
			{
				var mi = (MailItem)_outlookObject.CreateItem(OlItemType.olMailItem);
				foreach (string attachmentPath in attachmentPaths)
					mi.Attachments.Add(attachmentPath, OlAttachmentType.olByValue, 1, Path.GetFileNameWithoutExtension(attachmentPath));
				mi.Display(true);
			}
			catch (Exception e)
			{
				PopupMessageHelper.Instance.ShowWarning(e.Message);
			}
		}

		public void CreateMessage(string attachment)
		{
			try
			{
				var mi = (MailItem)_outlookObject.CreateItem(OlItemType.olMailItem);
				mi.Attachments.Add(attachment, OlAttachmentType.olByValue, 1, "Attachment");
				mi.Display(new object());
				_outlookObject = null;
			}
			catch (Exception e)
			{
				PopupMessageHelper.Instance.ShowWarning(e.Message);
			}
		}

		public void CreateMessage(FileInfo[] attachment)
		{
			try
			{
				var mi = (MailItem)_outlookObject.CreateItem(OlItemType.olMailItem);
				foreach (FileInfo file in attachment)
					mi.Attachments.Add(file.FullName, OlAttachmentType.olByValue, 1, "Attachment");
				mi.Display(new object());
				_outlookObject = null;
			}
			catch (Exception e)
			{
				PopupMessageHelper.Instance.ShowWarning(e.Message);
			}
		}

		public void CreateMessageToBilly()
		{
			var mi = (MailItem)_outlookObject.CreateItem(OlItemType.olMailItem);
			mi.Recipients.Add("billy@newlocaldirect.com");
			mi.Subject = "";
			mi.Display(new object());
			_outlookObject = null;
		}
	}
}
