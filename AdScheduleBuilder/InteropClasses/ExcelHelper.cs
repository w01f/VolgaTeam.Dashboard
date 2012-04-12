using System;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;

namespace AdScheduleBuilder.InteropClasses
{
    public partial class ExcelHelper
    {
        private Excel.Application _excelObject;

        public ExcelHelper()
        {
        }

        public bool Connect()
        {
            bool result = false;
            try
            {
                _excelObject = new Excel.Application();
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
                foreach (Excel.Workbook workbook in _excelObject.Workbooks)
                    workbook.Close();
                uint processId = 0;
                WinAPIHelper.GetWindowThreadProcessId(new IntPtr(_excelObject.Hwnd), out processId);
                Process.GetProcessById((int)processId).Kill();
            }
            AppManager.ReleaseComObject(_excelObject);
            GC.Collect();
        }

        public void ConvertToHtml(string oldFileName, string newFileName)
        {
            try
            {
                MessageFilter.Register();
                Excel.Workbook workbook = _excelObject.Workbooks.Open(Filename: oldFileName, ReadOnly: true);
                workbook.SaveAs(Filename: newFileName, FileFormat: Excel.XlFileFormat.xlHtml);
                workbook.Close(SaveChanges: false);
            }
            catch
            {
            }
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
                default :
                    return string.Empty;
            }
        }
    }
}
