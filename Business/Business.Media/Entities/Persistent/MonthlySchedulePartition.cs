using System.ComponentModel.DataAnnotations.Schema;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Enums;
using Asa.Business.Media.Entities.NonPersistent.Section.Content;
using Asa.Business.Media.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.Persistent
{
	public class MonthlySchedulePartition : MediaPartition, IMediaPartition<MonthlyScheduleContent>
	{
		#region Nonpersistent Properties
		private MonthlyScheduleContent _content;
		[NotMapped, JsonIgnore]
		public MonthlyScheduleContent Content
		{
			get
			{
				if (_content == null)
					_content = SettingsContainer.CreateInstance<MonthlyScheduleContent>(this, ContentEncoded);
				return _content;
			}
			set => _content = value;
		}
		#endregion

		public MonthlySchedulePartition()
		{
			Type = SchedulePartitionType.MonthlySchedule;
		}

		public override void BeforeSave()
		{
			ContentEncoded = Content.Serialize();
		}
	}
}
