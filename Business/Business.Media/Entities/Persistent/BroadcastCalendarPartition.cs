using System.ComponentModel.DataAnnotations.Schema;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Enums;
using Asa.Business.Media.Entities.NonPersistent.Calendar;
using Asa.Business.Media.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.Persistent
{
	public class BroadcastCalendarPartition : MediaPartition, IMediaPartition<BroadcastCalendar>
	{
		#region Nonpersistent Properties
		private BroadcastCalendar _content;

		[NotMapped, JsonIgnore]
		public BroadcastCalendar Content
		{
			get
			{
				if (_content == null)
					_content = SettingsContainer.CreateInstance<BroadcastCalendar>(this, ContentEncoded);
				return _content;
			}
			set => _content = value;
		}
		#endregion

		public BroadcastCalendarPartition()
		{
			Type = SchedulePartitionType.BroadcastCalendar;
		}

		public override void BeforeSave()
		{
			ContentEncoded = Content.Serialize();
		}
	}
}
