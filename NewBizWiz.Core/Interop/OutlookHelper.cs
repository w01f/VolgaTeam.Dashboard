﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Outlook;
using NewBizWiz.Core.Common;
using Exception = System.Exception;

namespace NewBizWiz.Core.Interop
{
	public class OutlookHelper
	{
		private static readonly OutlookHelper instance = new OutlookHelper();
		private Application _outlookObject;

		private OutlookHelper() { }

		public static OutlookHelper Instance
		{
			get { return instance; }
		}

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
					MAPIFolder folder = (_outlookObject.GetNamespace("MAPI")).GetDefaultFolder(OlDefaultFolders.olFolderInbox);
					folder.Display();
					Explorer explorer = _outlookObject.Explorers.Add(folder, OlFolderDisplayMode.olFolderDisplayNormal);
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
			Utilities.Instance.ReleaseComObject(_outlookObject);
		}

		public void CreateMessage(string subject, string attachmentPath)
		{
			try
			{
				IntPtr handle = IntPtr.Zero;
				Process[] processList = Process.GetProcesses();
				foreach (Process process in processList.Where(x => x.ProcessName.ToLower().Contains("outlook")))
				{
					if (process.MainWindowHandle.ToInt32() != 0)
					{
						handle = process.MainWindowHandle;
						break;
					}
				}
				Utilities.Instance.ActivateForm(handle, true, false);
				var mi = (MailItem)_outlookObject.CreateItem(OlItemType.olMailItem);
				mi.Attachments.Add(attachmentPath, OlAttachmentType.olByValue, 1, "Attachment");
				mi.Subject = subject;
				mi.Display(true);
				int count = 100000;
				handle = IntPtr.Zero;
				while (handle == IntPtr.Zero && count > 0)
				{
					handle = WinAPIHelper.FindWindow(string.Empty, subject + "- Message (HTML)");
					count--;
					System.Windows.Forms.Application.DoEvents();
				}
				if (handle != IntPtr.Zero)
					Utilities.Instance.ActivateForm(handle, true, true);
			}
			catch (Exception e)
			{
				Utilities.Instance.ShowWarning(e.Message);
			}
		}

		public void CreateMessage(string[] attachmentPaths)
		{
			try
			{
				var mi = (MailItem)_outlookObject.CreateItem(OlItemType.olMailItem);
				foreach (string attachmentPath in attachmentPaths)
					mi.Attachments.Add(attachmentPath, OlAttachmentType.olByValue, 1, "Attachment");
				mi.Display(true);
			}
			catch (Exception e)
			{
				Utilities.Instance.ShowWarning(e.Message);
			}
		}

		public void CreateMessage(string attachment)
		{
			try
			{
				MailItem mi = (MailItem)_outlookObject.CreateItem(OlItemType.olMailItem);
				mi.Attachments.Add(attachment, OlAttachmentType.olByValue, 1, "Attachment");
				mi.Display(new object());
				_outlookObject = null;
			}
			catch (Exception e)
			{
				Utilities.Instance.ShowWarning(e.Message);
			}
		}

		public void CreateMessage(FileInfo[] attachment)
		{
			try
			{
				MailItem mi = (MailItem)_outlookObject.CreateItem(OlItemType.olMailItem);
				foreach (FileInfo file in attachment)
					mi.Attachments.Add(file.FullName, OlAttachmentType.olByValue, 1, "Attachment");
				mi.Display(new object());
				_outlookObject = null;
			}
			catch (Exception e)
			{
				Utilities.Instance.ShowWarning(e.Message);
			}
		}

		public void CreateMessageToBilly()
		{
			MailItem mi = (MailItem)_outlookObject.CreateItem(OlItemType.olMailItem);
			mi.Recipients.Add("billy@newlocaldirect.com");
			mi.Subject = "";
			mi.Display(new object());
			_outlookObject = null;

		}
	}
}