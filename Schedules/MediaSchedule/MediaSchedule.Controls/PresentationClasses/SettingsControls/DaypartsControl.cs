using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Asa.Business.Media.Entities.NonPersistent.Schedule;
using DevExpress.XtraGrid.Views.Base;

namespace Asa.Media.Controls.PresentationClasses.SettingsControls
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

		public void LoadData(MediaScheduleSettings scheduleSettings)
		{
			_dayparts.Clear();
			_dayparts.AddRange(scheduleSettings.Dayparts);
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
			Changed?.Invoke(this, EventArgs.Empty);
		}

		private void repositoryItemCheckEdit_CheckedChanged(object sender, EventArgs e)
		{
			gridViewItems.CloseEditor();
		}
	}
}