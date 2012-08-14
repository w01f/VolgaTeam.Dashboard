using System.Collections.Generic;
using System.Windows.Forms;

namespace TVScheduleBuilder.CustomControls
{
    public partial class DaypartsControl : UserControl
    {
        private List<BusinessClasses.Daypart> _dayparts = new List<BusinessClasses.Daypart>();
        public bool HasChanged { get; set; }

        public DaypartsControl()
        {
            InitializeComponent();
        }

        public void LoadData(BusinessClasses.Schedule schedule)
        {
            _dayparts.Clear();
            _dayparts.AddRange(schedule.Dayparts);
            checkedListBoxControl.Items.Clear();
            foreach (BusinessClasses.Daypart daypart in _dayparts)
                checkedListBoxControl.Items.Add(daypart.Code, daypart.Name, daypart.Available ? CheckState.Checked : CheckState.Unchecked, true);
            this.HasChanged = false;
        }

        public BusinessClasses.Daypart[] GetData()
        {
            return _dayparts.ToArray();
        }

        private void checkedListBoxControl_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            _dayparts[e.Index].Available = e.State == CheckState.Checked;
            this.HasChanged = true;
        }
    }
}
