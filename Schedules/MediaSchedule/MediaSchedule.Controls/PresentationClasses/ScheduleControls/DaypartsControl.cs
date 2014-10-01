using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using NewBizWiz.Core.MediaSchedule;

namespace NewBizWiz.MediaSchedule.Controls.PresentationClasses.ScheduleControls
{
	public partial class DaypartsControl : UserControl
	{
		private readonly List<Daypart> _dayparts = new List<Daypart>();

		public DaypartsControl()
		{
			InitializeComponent();
		}

		public bool HasChanged { get; set; }
		public EventHandler<EventArgs> Changed;

		public void LoadData(Schedule schedule)
		{
			_dayparts.Clear();
			_dayparts.AddRange(schedule.Dayparts);
			gridControlItems.DataSource = new BindingList<Daypart>(_dayparts);
			HasChanged = false;
		}

		public Daypart[] GetData()
		{
			return _dayparts.ToArray();
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