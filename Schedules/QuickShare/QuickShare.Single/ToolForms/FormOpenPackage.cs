using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevExpress.XtraBars;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using NewBizWiz.Core.Common;
using NewBizWiz.Core.MediaSchedule;
using NewBizWiz.Core.QuickShare;

namespace NewBizWiz.QuickShare.Single
{
	public partial class FormOpenPackage : MetroForm
	{
		private ShortPackage[] _packageList;

		public FormOpenPackage()
		{
			InitializeComponent();
		}

		public string PackageName { get; set; }

		public void LoadPackages()
		{
			_packageList = PackageManager.GetShortPackageList();
			gridControlPackages.Visible = true;
			repositoryItemComboBoxStatus.Items.Clear();
			repositoryItemComboBoxStatus.Items.AddRange(MediaMetaData.Instance.ListManager.Statuses);
			gridControlPackages.DataSource = new BindingList<ShortPackage>(_packageList);
			if (gridViewPackages.RowCount > 0)
				gridViewPackages.FocusedRowHandle = 0;
		}

		private void FormOpenPackage_Load(object sender, EventArgs e)
		{
			LoadPackages();
		}

		private void barLargeButtonItemOpen_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (gridViewPackages.FocusedRowHandle != GridControl.InvalidRowHandle)
			{
				PackageName = _packageList[gridViewPackages.GetFocusedDataSourceRowIndex()].ShortFileName;
				DialogResult = DialogResult.OK;
				Close();
			}
			else
				Utilities.Instance.ShowWarning("Please select package in list");
		}

		private void barLargeButtonItemDelete_ItemClick(object sender, ItemClickEventArgs e)
		{
			if (Utilities.Instance.ShowWarningQuestion("Delete this Package?") == DialogResult.Yes)
			{
				string fileName = _packageList[gridViewPackages.GetFocusedDataSourceRowIndex()].FullFileName;
				try
				{
					if (File.Exists(fileName))
						File.Delete(fileName);
				}
				catch
				{
					Utilities.Instance.ShowWarning("Couldn't delete selected package.");
				}
				LoadPackages();
			}
		}

		private void barLargeButtonItemExit_ItemClick(object sender, ItemClickEventArgs e)
		{
			DialogResult = DialogResult.None;
			Close();
		}

		private void gridViewSchedules_RowClick(object sender, RowClickEventArgs e)
		{
			if (e.Clicks == 2)
				barLargeButtonItemOpen_ItemClick(null, null);
		}

		private void gridViewSchedules_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			ShortPackage schedule = _packageList[gridViewPackages.GetDataSourceRowIndex(e.RowHandle)];
			schedule.Save();
		}

		private void repositoryItemComboBoxStatus_Closed(object sender, ClosedEventArgs e)
		{
			gridViewPackages.CloseEditor();
		}
	}
}