using System.Collections.Generic;
using System.ComponentModel;
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
            gridControlItems.DataSource = new BindingList<BusinessClasses.Station>(_stations);
            this.HasChanged = false;
        }

        public BusinessClasses.Station[] GetData()
        {
            return _stations.ToArray();
        }

        private void gridViewItems_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            this.HasChanged = true;
        }

        private void repositoryItemCheckEdit_CheckedChanged(object sender, System.EventArgs e)
        {
            gridViewItems.CloseEditor();
        }
    }
}
