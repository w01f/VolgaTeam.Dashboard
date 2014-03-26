using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraTab;
using NewBizWiz.Core.OnlineSchedule;

namespace NewBizWiz.OnlineSchedule.Controls.PresentationClasses
{
	//public partial class DigitalProductInfoGroup : UserControl
	public partial class DigitalProductInfoGroup : XtraTabPage
	{
		public List<ProductInfo> DataSourse { get; private set; }

		public DigitalProductInfoGroup(IEnumerable<ProductInfo> productInfoRecords)
		{
			InitializeComponent();
			DataSourse = new List<ProductInfo>();
			DataSourse.AddRange(productInfoRecords);
			gridControl.DataSource = DataSourse;
		}

		public void CloseEditors()
		{
			gridView.CloseEditor();
		}

		private void gridView_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if (gridView.FocusedColumn != gridColumnPhrase) return;
			var productInfo = gridView.GetFocusedRow() as ProductInfo;
			if (productInfo == null) return;
			e.Cancel = !productInfo.Selected;
		}

		private void gridView_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
		{
			if (e.Column != gridColumnPhrase) return;
			var productInfo = gridView.GetRow(e.RowHandle) as ProductInfo;
			if (productInfo == null) return;
			if (!productInfo.Selected) return;
			e.Appearance.ForeColor = Color.DarkBlue;
			e.Appearance.Font = new Font(e.Appearance.Font.Name, e.Appearance.Font.Size, FontStyle.Bold);
		}

		private void repositoryItemCheckEdit_CheckedChanged(object sender, System.EventArgs e)
		{
			gridView.PostEditor();
		}
	}
}
