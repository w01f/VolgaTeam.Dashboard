using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses
{
	public partial class StationsControl : UserControl
	{
		private readonly List<Station> _stations = new List<Station>();

		public StationsControl()
		{
			InitializeComponent();
		}

		public bool HasChanged { get; set; }

		public void LoadData(Schedule schedule)
		{
			_stations.Clear();
			_stations.AddRange(schedule.Stations);
			gridControlItems.DataSource = new BindingList<Station>(_stations);
			HasChanged = false;
		}

		public Station[] GetData()
		{
			return _stations.ToArray();
		}

		private void gridViewItems_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			HasChanged = true;
		}

		private void repositoryItemCheckEdit_CheckedChanged(object sender, EventArgs e)
		{
			gridViewItems.CloseEditor();
		}
	}
}