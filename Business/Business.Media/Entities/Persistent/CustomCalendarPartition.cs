using System.ComponentModel.DataAnnotations.Schema;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Enums;
using Asa.Business.Media.Entities.NonPersistent.Calendar;
using Asa.Business.Media.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.Persistent
{
	public class CustomCalendarPartition : MediaPartition, IMediaPartition<CustomCalendar>
	{
		#region Nonpersistent Properties
		private CustomCalendar _content;
		[NotMapped, JsonIgnore]
		public CustomCalendar Content
		{
			get
			{
				if (_content == null)
					_content = SettingsContainer.CreateInstance<CustomCalendar>(this, ContentEncoded);
				return _content;
			}
			set { _content = value; }
		}
		#endregion

		public CustomCalendarPartition()
		{
			Type = SchedulePartitionType.CustomCalendar;
		}

		public override void BeforeSave()
		{
			ContentEncoded = Content.Serialize();
		}
	}
}
