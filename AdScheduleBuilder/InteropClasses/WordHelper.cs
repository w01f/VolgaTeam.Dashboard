using System;
using System.Diagnostics;
using Word = Microsoft.Office.Interop.Word;

namespace AdScheduleBuilder.InteropClasses
{
    public partial class WordHelper
    {
        private Word.Application _wordObject;
        private bool _isFirstLaunch = false;

        public WordHelper()
        {
        }

        public bool Connect()
        {
            try
            {
                _wordObject =
                    System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application") as Word.Application;
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

                    _wordObject = new Word.Application();
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
                _wordObject.DisplayAlerts = Microsoft.Office.Interop.Word.WdAlertLevel.wdAlertsNone;
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
            AppManager.ReleaseComObject(_wordObject); 
            GC.Collect();
        }

        public void ConvertToHtml(string oldFileName, string newFileName)
        {
            try
            {
                MessageFilter.Register();
                Word.Document document = _wordObject.Documents.Open(FileName: oldFileName);
                document.SaveAs(FileName: newFileName, FileFormat: Word.WdSaveFormat.wdFormatHTML);
                ((Word._Document)document).Close(SaveChanges: false);
            }
            catch
            {
            }
            finally
            {
                MessageFilter.Revoke();
            }
        }
    }
}
