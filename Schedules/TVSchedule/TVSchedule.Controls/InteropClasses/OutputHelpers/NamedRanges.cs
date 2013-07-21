using System.Collections.Generic;
using Excel = Microsoft.Office.Interop.Excel;

namespace TVScheduleBuilder.InteropClasses
{
    public class NamedRanges
    {
        private Excel._Worksheet _baseWorksheet = null;
        public List<string> Items { get; private set; }

        public Excel.Range this[string rangeName]
        {
            get
            {
                if (this.Items.Contains(rangeName) || this.Items.Count == 0)
                    return _baseWorksheet.Range[rangeName];
                else
                    return null;
            }
        }

        public void SetValue(string rangeName, object value)
        {
            if (this.Items.Contains(rangeName) || this.Items.Count == 0)
                _baseWorksheet.Range[rangeName].Value = value;
        }

        public NamedRanges(Excel._Worksheet baseWorksheet)
        {
            _baseWorksheet = baseWorksheet;
            this.Items = new List<string>();
        }
    }
}
