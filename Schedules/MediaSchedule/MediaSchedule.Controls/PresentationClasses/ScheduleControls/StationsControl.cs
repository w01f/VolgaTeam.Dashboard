using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using Asa.Core.MediaSchedule;

namespace Asa.MediaSchedule.Controls.PresentationClasses.ScheduleControls
{
	public partial class StationsControl : UserControl
	{
		private readonly List<Station> _stations = new List<Station>();

		public StationsControl()
		{
			InitializeComponent();
		}

		public bool HasChanged { get; set; }
		public EventHandler<EventArgs> Changed;

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
			if (Changed != null)
				Changed(this, EventArgs.Empty);
		}

		private void repositoryItemCheckEdit_CheckedChanged(object sender, EventArgs e)
		{
			gridViewItems.CloseEditor();
		}
	}
}