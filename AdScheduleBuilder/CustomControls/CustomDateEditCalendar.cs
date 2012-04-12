using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace AdScheduleBuilder.CustomControls
{
    public partial class CustomDateEditCalendar : DevExpress.XtraEditors.Controls.DateEditCalendar
    {
        public CustomDateEditCalendar(): base(null,DateTime.Now)
        {
            InitializeComponent();
        }
    }
}
