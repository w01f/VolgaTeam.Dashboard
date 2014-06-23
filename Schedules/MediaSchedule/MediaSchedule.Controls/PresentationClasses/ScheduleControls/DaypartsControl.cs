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
		private readonly List<Daypart> dayparts = new List<Daypart>();

		public DaypartsControl()
		{
			InitializeComponent();
		}

		public bool HasChanged { get; set; }

		public void LoadData(Schedule schedule)
		{
			dayparts.Clear();
			dayparts.AddRange(schedule.Dayparts);
			gridControlItems.DataSource = new BindingList<Daypart>(dayparts);
			HasChanged = false;
		}

		public Daypart[] GetData()
		{
			return dayparts.ToArray();
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