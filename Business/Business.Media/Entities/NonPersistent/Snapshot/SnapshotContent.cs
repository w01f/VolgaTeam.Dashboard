using System.Collections.Generic;
using System.Linq;
using Asa.Business.Media.Entities.NonPersistent.Common;

namespace Asa.Business.Media.Entities.NonPersistent.Snapshot
{
	public class SnapshotContent : MediaScheduleContent
	{
		public List<Snapshot> Snapshots { get; private set; }
		public SnapshotSummary SnapshotSummary { get; private set; }

		public SnapshotContent()
		{
			Snapshots = new List<Snapshot>();
			SnapshotSummary = new SnapshotSummary(this);
		}

		public override void Dispose()
		{
			Snapshots.ForEach(s => s.Dispose());
			Snapshots.Clear();

			SnapshotSummary.Dispose();
			SnapshotSummary = null;
			
			base.Dispose();
		}

		protected override void AfterCreate()
		{
			base.AfterCreate();
			foreach (var snapshot in Snapshots)
			{
				snapshot.Parent = this;
				snapshot.AfterCreate();
			}
			RebuildSnapshotIndexes();
		}

		public void ChangeSnapshotPosition(int position, int newPosition)
		{
			if (position < 0 || position >= Snapshots.Count) return;
			var snapshot = Snapshots[position];
			snapshot.Index = newPosition - 0.5;
			RebuildSnapshotIndexes();
		}

		public void RebuildSnapshotIndexes()
		{
			var i = 0;
			foreach (var snapshot in Snapshots.OrderBy(o => o.Index))
			{
				snapshot.Index = i;
				i++;
			}
			Snapshots.Sort((x, y) => x.Index.CompareTo(y.Index));
		}
	}
}
