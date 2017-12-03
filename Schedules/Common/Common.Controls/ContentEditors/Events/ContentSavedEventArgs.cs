using System;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Schedules.Common.Controls.ContentEditors.Enums;

namespace Asa.Schedules.Common.Controls.ContentEditors.Events
{
	public class ContentSavedEventArgs<TChangeInfo> : EventArgs
		where TChangeInfo : BaseScheduleChangeInfo
	{
		public string[] Source { get; }
		public TChangeInfo ChangeInfo { get; }
		public ContentSavingReason SavingReason { get; }

		public ContentSavedEventArgs(string[] source, TChangeInfo changeInfo)
		{
			Source = source;
			ChangeInfo = changeInfo;
		}

		public ContentSavedEventArgs(string source, TChangeInfo changeInfo, ContentSavingReason savingReason)
		{
			Source = new[] { source };
			ChangeInfo = changeInfo;
			SavingReason = savingReason;
		}
	}
}
