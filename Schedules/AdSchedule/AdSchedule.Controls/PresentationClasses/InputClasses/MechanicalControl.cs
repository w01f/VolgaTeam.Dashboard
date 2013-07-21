using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using NewBizWiz.Core.AdSchedule;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.InputClasses
{
	[ToolboxItem(false)]
	//public partial class MechanicalControl : UserControl
	public partial class MechanicalControl : XtraTabPage
	{
		private readonly MechanicalType _mechanicalType;

		public MechanicalControl(MechanicalType mechanicalType)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			_mechanicalType = mechanicalType;
			Text = mechanicalType.Name.Replace("&", "&&");
			UpdateList();
		}

		private void UpdateList()
		{
			gridControlMechanicals.DataSource = _mechanicalType.Items;
		}

		private void gridViewMechanicals_RowClick(object sender, RowClickEventArgs e)
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