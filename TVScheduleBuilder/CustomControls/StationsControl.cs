using System.Collections.Generic;
using System.Windows.Forms;

namespace TVScheduleBuilder.CustomControls
{
    public partial class StationsControl : UserControl
    {
        private List<BusinessClasses.Station> _stations = new List<BusinessClasses.Station>();
        public bool HasChanged { get; set; }

        public StationsControl()
        {
            InitializeComponent();
        }

        public void LoadData(BusinessClasses.Schedule schedule)
        {
            _stations.Clear();
            _stations.AddRange(schedule.Stations);
            checkedListBoxControl.Items.Clear();
            foreach (BusinessClasses.Station station in _stations)
                checkedListBoxControl.Items.Add(station.Name, station.Name, station.Available ? CheckState.Checked : CheckState.Unchecked, true);
            this.HasChanged = false;
        }

        public BusinessClasses.Station[] GetData()
        {
            return _stations.ToArray();
        }

        private void checkedListBoxControl_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            _stations[e.Index].Available = e.State == CheckState.Checked;
            this.HasChanged = true;
        }
    }
}
