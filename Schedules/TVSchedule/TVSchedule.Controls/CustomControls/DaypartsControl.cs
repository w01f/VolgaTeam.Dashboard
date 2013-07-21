using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace TVScheduleBuilder.CustomControls
{
    public partial class DaypartsControl : UserControl
    {
        private List<BusinessClasses.Daypart> dayparts = new List<BusinessClasses.Daypart>();
        public bool HasChanged { get; set; }

        public DaypartsControl()
        {
            InitializeComponent();
        }

        public void LoadData(BusinessClasses.Schedule schedule)
        {
            dayparts.Clear();
            dayparts.AddRange(schedule.Dayparts);
            gridControlItems.DataSource = new BindingList<BusinessClasses.Daypart>(dayparts);
            this.HasChanged = false;
        }

        public BusinessClasses.Daypart[] GetData()
        {
            return dayparts.ToArray();
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
