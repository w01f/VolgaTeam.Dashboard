using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;
using Asa.Core.Common;

namespace Asa.Core.Interop
{
	public class WordHelper
	{
		private bool _isFirstLaunch;
		private Application _wordObject;

		public bool Connect()
		{
			try
			{
				_wordObject =
					Marshal.GetActiveObject("Word.Application") as Application;
				_isFirstLaunch = false;
			}
			catch
			{
				_wordObject = null;
			}
			if (_wordObject == null)
			{
				try
				{
					_wordObject = new Application();
					_isFirstLaunch = true;
				}
				catch
				{
					return false;
				}
			}
			if (_wordObject != null)
			{
				_wordObject.Visible = false;
				_wordObject.DisplayAlerts = WdAlertLevel.wdAlertsNone;
				return true;
			}
			else
				return false;
		}

		public void Disconnect()
		{
			if (_isFirstLaunch)
			{
				Process[] proc = Process.GetProcessesByName("WINWORD");
				if (proc.GetLength(0) > 0)
					proc[0].Kill();
			}
			Utilities.Instance.ReleaseComObject(_wordObject);
			GC.Collect();
		}

		public void ConvertToHtml(string oldFileName, string newFileName)
		{
			try
			{
				MessageFilter.Register();
				Document document = _wordObject.Documents.Open(FileName: oldFileName);
				document.SaveAs(FileName: newFileName, FileFormat: WdSaveFormat.wdFormatHTML);
				(document).Close(SaveChanges: false);
			}
			catch {}
			finally
			{
				MessageFilter.Revoke();
			}
		}
	}
}