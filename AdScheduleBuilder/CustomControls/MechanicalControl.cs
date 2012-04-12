using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AdScheduleBuilder.CustomControls
{
    [System.ComponentModel.ToolboxItem(false)]
    //public partial class MechanicalControl : UserControl
    public partial class MechanicalControl : DevExpress.XtraTab.XtraTabPage
    {
        private BusinessClasses.MechanicalType _mechanicalType = null;

        public MechanicalControl(BusinessClasses.MechanicalType mechanicalType)
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this._mechanicalType = mechanicalType;
            this.Text = mechanicalType.Name.Replace("&", "&&");
            UpdateList();
        }

        private void UpdateList()
        {
            gridControlMechanicals.DataSource = _mechanicalType.Items;
        }

        private void gridViewMechanicals_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            bool selected = false;
            object value = gridViewMechanicals.GetRowCellValue(e.RowHandle, gridColumnSelected);
            if (value != null)
                bool.TryParse(value.ToString(), out selected);
            for (int i = 0; i < gridViewMechanicals.RowCount; i++)
                gridViewMechanicals.SetRowCellValue(i, gridColumnSelected, false);
            gridViewMechanicals.SetRowCellValue(e.RowHandle, gridColumnSelected, !selected);
        }
    }
}
