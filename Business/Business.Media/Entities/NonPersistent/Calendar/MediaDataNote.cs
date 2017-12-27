using System.Drawing;
using Asa.Business.Calendar.Entities.NonPersistent;
using Asa.Business.Media.Enums;
using Asa.Common.Core.Interfaces;
using Asa.Common.Core.Objects.Output;
using Newtonsoft.Json;

namespace Asa.Business.Media.Entities.NonPersistent.Calendar
{
	public class MediaDataNote : CalendarNote
	{
		public TextGroup MediaData { get; set; }
		public bool EditedByUser { get; private set; }
		public bool Splitted { get; set; }

		public BroadcastCalendar ParentBroadcastCalendar => (BroadcastCalendar)Parent;

		public override ITextItem Note
		{
			get => _note ?? (ParentBroadcastCalendar.DataSourceType == BroadcastDataTypeEnum.Schedule || ParentBroadcastCalendar.DataSourceType == BroadcastDataTypeEnum.Snapshots ? MediaData : null);
			set
			{
				if (!MediaData.IsEqual(value) || !(ParentBroadcastCalendar.DataSourceType == BroadcastDataTypeEnum.Schedule || ParentBroadcastCalendar.DataSourceType == BroadcastDataTypeEnum.Snapshots))
					_note = value;
				EditedByUser = EditedByUser || _note != null;
			}
		}

		public override Color BackgroundColor
		{
			get => _backgroundColor;
			set
			{
				_backgroundColor = value;
				EditedByUser = EditedByUser || _backgroundColor != DefaultBackgroundColor;
			}
		}

		[JsonConstructor]
		private MediaDataNote() { }

		public MediaDataNote(BroadcastCalendar parent) : base(parent)
		{
			EditedByUser = !(ParentBroadcastCalendar.DataSourceType == BroadcastDataTypeEnum.Schedule ||
							 ParentBroadcastCalendar.DataSourceType == BroadcastDataTypeEnum.Snapshots);
		}

		public void Reset()
		{
			_note = null;
			_backgroundColor = DefaultBackgroundColor;
			EditedByUser = false;
		}
	}
}
