﻿using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Asa.Common.Core.Helpers;
using Microsoft.Office.Interop.Excel;

namespace Asa.Common.Core.OfficeInterops
{
	public class ExcelHelper
	{
		private static readonly ExcelHelper _instance = new ExcelHelper();

		public Application ExcelObject { get; private set; }

		private bool _isOpened;

		private ExcelHelper() { }

		public static ExcelHelper Instance => _instance;

		public bool IsOpened
		{
			get
			{
				var proc = Process.GetProcessesByName("EXCEL");
				if (!(proc.GetLength(0) > 0))
				{
					ExcelObject = null;
					_isOpened = false;
				}
				return _isOpened;
			}
		}

		public bool Connect()
		{
			bool result = false;
			try
			{
				ExcelObject = new Application();
				ExcelObject.Visible = false;
				ExcelObject.DisplayAlerts = false;
				result = true;
			}
			catch
			{
				ExcelObject = null;
			}
			return result;
		}


		public void Disconnect()
		{
			if (ExcelObject != null)
			{
				foreach (Workbook workbook in ExcelObject.Workbooks)
					workbook.Close();
				uint processId;
				WinAPIHelper.GetWindowThreadProcessId(new IntPtr(ExcelObject.Hwnd), out processId);
				Process.GetProcessById((int)processId).Kill();
			}
			Utilities.ReleaseComObject(ExcelObject);
			GC.Collect();
		}


		public void Print(FileInfo file)
		{
			var workBook = ExcelObject.Workbooks.Open(file.FullName);
			ExcelObject.Visible = true;
			var processList = Process.GetProcesses();
			foreach (var process in processList.Where(x => x.ProcessName.ToLower().Contains("excel")))
				if (process.MainWindowHandle.ToInt32() != 0)
					Utilities.ActivateForm(process.MainWindowHandle, true, false);
			workBook.Application.Dialogs[XlBuiltInDialog.xlDialogPrint].Show();
		}

		public bool ConvertToPDF(string originalFileName, string pdfFileName)
		{
			var result = false;
			try
			{
				MessageFilter.Revoke();
				var workbook = ExcelObject.Workbooks.Open(originalFileName);
				workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, pdfFileName, XlFixedFormatQuality.xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
				result = true;
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
			return result;
		}

		public void ConvertToHtml(string oldFileName, string newFileName)
		{
			try
			{
				MessageFilter.Register();
				var workbook = ExcelObject.Workbooks.Open(oldFileName, ReadOnly: true);
				workbook.SaveAs(newFileName, XlFileFormat.xlHtml);
				workbook.Close(false);
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public static string GetColumnLetterByIndex(int index)
		{
			switch (index)
			{
				case 0:
					return "A";
				case 1:
					return "B";
				case 2:
					return "C";
				case 3:
					return "D";
				case 4:
					return "E";
				case 5:
					return "F";
				case 6:
					return "G";
				case 7:
					return "H";
				case 8:
					return "I";
				case 9:
					return "J";
				case 10:
					return "K";
				case 11:
					return "L";
				default:
					return string.Empty;
			}
		}
	}
}
