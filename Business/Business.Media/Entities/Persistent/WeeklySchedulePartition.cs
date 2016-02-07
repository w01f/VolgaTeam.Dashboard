using System.ComponentModel.DataAnnotations.Schema;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Enums;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.Persistent
{
	public class WeeklySchedulePartition : MediaPartition, IMediaPartition<WeeklyScheduleContent>
	{
		#region Nonpersistent Properties
		private WeeklyScheduleContent _content;
		[NotMapped, JsonIgnore]
		public WeeklyScheduleContent Content
		{
			get
			{
				if (_content == null)
					_content = SettingsContainer.CreateInstance<WeeklyScheduleContent>(this, ContentEncoded);
				return _content;
			}
			set { _content = value; }
		}
		#endregion

		public WeeklySchedulePartition()
		{
			Type = SchedulePartitionType.WeeklySchedule;
		}

		public override void BeforeSave()
		{
			ContentEncoded = Content.Serialize();
		}
	}
}
