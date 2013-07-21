using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NewBizWiz.Core.AdSchedule;

namespace NewBizWiz.AdSchedule.Controls.PresentationClasses.OutputClasses.OutputControls
{
	public partial class OutputSnapshotContainer : UserControl
	{
		private readonly List<PublicationSnapshotControl> _snapshots = new List<PublicationSnapshotControl>();

		public OutputSnapshotContainer()
		{
			InitializeComponent();
		}

		private void ResizeColumns()
		{
			double width = Width;
			double columnWidth = width / 2;
			pnLeftColumn.Width = (int)columnWidth;
			pnRightColumn.Width = (int)columnWidth;
			Refresh();
		}

		private void UpdateSize()
		{
			int bottom = 0;
			foreach (PublicationSnapshotControl snapshot in _snapshots)
			{
				if (snapshot.Bottom > bottom)
					bottom = snapshot.Bottom;
			}
			Height = bottom;
		}

		public void UpdateSnapshots(Schedule schedule)
		{
			_snapshots.Clear();
			pnLeftColumn.Controls.Clear();
			pnRightColumn.Controls.Clear();
			int columnIndex = 0;
			foreach (PrintProduct publication in schedule.PrintProducts)
			{
				if (publication.Inserts.Count > 0)
				{
					var snapshot = new PublicationSnapshotControl(publication);
					if (columnIndex == 0)
					{
						pnLeftColumn.Controls.Add(snapshot);
						snapshot.BringToFront();
						columnIndex++;
					}
					else
					{
						pnRightColumn.Controls.Add(snapshot);
						snapshot.BringToFront();
						columnIndex = 0;
					}
					_snapshots.Add(snapshot);
				}
			}
			UpdateColumns(schedule);
		}

		public void UpdateColumns(Schedule schedule)
		{
			foreach (PublicationSnapshotControl snaphot in _snapshots)
				snaphot.UpdateToggles();
			UpdateSize();
		}

		private void OutputSnapshotContainer_SizeChanged(object sender, EventArgs e)
		{
			ResizeColumns();
		}

		private void OutputSnapshotContainer_Load(object sender, EventArgs e)
		{
			ResizeColumns();
		}
	}
}