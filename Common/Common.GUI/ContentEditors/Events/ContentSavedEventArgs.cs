using System;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Common.GUI.ContentEditors.Enums;

namespace Asa.Common.GUI.ContentEditors.Events
{
	public class ContentSavedEventArgs<TChangeInfo> : EventArgs
		where TChangeInfo : BaseScheduleChangeInfo
	{
		public string[] Source { get; private set; }
		public TChangeInfo ChangeInfo { get; private set; }
		public ContentSavingReason SavingReason { get; private set; }

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
