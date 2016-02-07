using System.ComponentModel.DataAnnotations.Schema;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Enums;
using Asa.Business.Media.Entities.NonPersistent.Snapshot;
using Asa.Business.Media.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.Persistent
{
	public class SnapshotPartition : MediaPartition, IMediaPartition<SnapshotContent>
	{
		#region Nonpersistent Properties
		private SnapshotContent _content;
		[NotMapped, JsonIgnore]
		public SnapshotContent Content
		{
			get
			{
				if (_content == null)
					_content = SettingsContainer.CreateInstance<SnapshotContent>(this, ContentEncoded);
				return _content;
			}
			set { _content = value; }
		}
		#endregion

		public SnapshotPartition()
		{
			Type = SchedulePartitionType.Snapshots;
		}

		public override void BeforeSave()
		{
			ContentEncoded = Content.Serialize();
		}
	}
}
