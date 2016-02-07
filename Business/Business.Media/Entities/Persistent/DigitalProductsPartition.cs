using System.ComponentModel.DataAnnotations.Schema;
using Asa.Business.Common.Entities.NonPersistent.Common;
using Asa.Business.Common.Enums;
using Asa.Business.Media.Entities.NonPersistent.Digital;
using Asa.Business.Media.Interfaces;
using Asa.Business.Online.Entities.NonPersistent;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.Persistent
{
	class DigitalProductsPartition : MediaPartition, IMediaPartition<DigitalProductsContent>
	{
		#region Nonpersistent Properties
		private DigitalProductsContent _content;
		[NotMapped, JsonIgnore]
		public DigitalProductsContent Content
		{
			get
			{
				if (_content == null)
					_content = SettingsContainer.CreateInstance<DigitalMediaProductsContent>(this, ContentEncoded);
				return _content;
			}
			set { _content = value; }
		}
		#endregion

		public DigitalProductsPartition()
		{
			Type = SchedulePartitionType.DigitalProducts;
		}

		public override void BeforeSave()
		{
			ContentEncoded = Content.Serialize();
		}
	}
}
