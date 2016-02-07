using System.ComponentModel.DataAnnotations.Schema;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Enums;
using Asa.Business.Media.Entities.NonPersistent.Option;
using Asa.Business.Media.Interfaces;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.Persistent
{
	public class OptionsPartition : MediaPartition, IMediaPartition<OptionsContent>
	{
		#region Nonpersistent Properties
		private OptionsContent _content;
		[NotMapped, JsonIgnore]
		public OptionsContent Content
		{
			get
			{
				if (_content == null)
					_content = SettingsContainer.CreateInstance<OptionsContent>(this, ContentEncoded);
				return _content;
			}
			set { _content = value; }
		}
		#endregion

		public OptionsPartition()
		{
			Type = SchedulePartitionType.Options;
		}

		public override void BeforeSave()
		{
			ContentEncoded = Content.Serialize();
		}
	}
}
