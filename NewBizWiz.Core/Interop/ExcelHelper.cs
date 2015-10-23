using System;
using System.Diagnostics;
using Microsoft.Office.Interop.Excel;
using Asa.Core.Common;

namespace Asa.Core.Interop
{
	public class ExcelHelper
	{
		protected Application _excelObject;

		public bool Connect()
		{
			bool result = false;
			try
			{
				_excelObject = new Application();
				_excelObject.Visible = false;
				_excelObject.DisplayAlerts = false;
				result = true;
			}
			catch
			{
				_excelObject = null;
			}
			return result;
		}

		public void Disconnect()
		{
			if (_excelObject != null)
			{
				foreach (Workbook workbook in _excelObject.Workbooks)
					workbook.Close();
				uint processId = 0;
				WinAPIHelper.GetWindowThreadProcessId(new IntPtr(_excelObject.Hwnd), out processId);
				Process.GetProcessById((int)processId).Kill();
			}
			Utilities.Instance.ReleaseComObject(_excelObject);
			GC.Collect();
		}

		public void ConvertToHtml(string oldFileName, string newFileName)
		{
			try
			{
				MessageFilter.Register();
				Workbook workbook = _excelObject.Workbooks.Open(Filename: oldFileName, ReadOnly: true);
				workbook.SaveAs(Filename: newFileName, FileFormat: XlFileFormat.xlHtml);
				workbook.Close(SaveChanges: false);
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