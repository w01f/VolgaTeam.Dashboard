using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AdScheduleBuilder.OutputClasses.OutputControls
{
    public partial class OutputSnapshotContainer : UserControl
    {
        private List<PublicationSnapshotControl> _snapshots = new List<PublicationSnapshotControl>();

        public OutputSnapshotContainer()
        {
            InitializeComponent();
        }

        private void ResizeColumns()
        {
            double width = this.Width;
            double columnWidth = width/2;
            pnLeftColumn.Width = (int)columnWidth;
            pnRightColumn.Width = (int)columnWidth;
            this.Refresh();
        }

        private void UpdateSize()
        { 
            int bottom = 0;
            foreach (var snapshot in _snapshots)
            {
                if (snapshot.Bottom > bottom)
                    bottom = snapshot.Bottom;
            }
            this.Height = bottom;
        }

        public void UpdateSnapshots(BusinessClasses.Schedule schedule)
        {
            _snapshots.Clear();
            pnLeftColumn.Controls.Clear();
            pnRightColumn.Controls.Clear();
            int columnIndex = 0;
            foreach (var publication in schedule.Publications)
            {
                if (publication.Inserts.Count > 0)
                {
                    PublicationSnapshotControl snapshot = new PublicationSnapshotControl(publication);
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

        public void UpdateColumns(BusinessClasses.Schedule schedule)
        {
            foreach (var snaphot in _snapshots)
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
