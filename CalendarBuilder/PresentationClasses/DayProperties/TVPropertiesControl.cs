using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CalendarBuilder.PresentationClasses.DayProperties
{
    public partial class TVPropertiesControl : UserControl
    {
        private bool _alowToSave = false;
        private BusinessClasses.CalendarDay _day = null;

        [Browsable(true)]
        [Category("Action")]
        public event EventHandler PropertiesChanged;

        public TVPropertiesControl()
        {
            InitializeComponent();
        }

        public void LoadData(BusinessClasses.CalendarDay day)
        {
            _day = day;
            _alowToSave = false;

            _alowToSave = true;
        }

        public void SaveData()
        {
            if (_day != null)
            {
            }
        }
    }
}
