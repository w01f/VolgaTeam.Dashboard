using System;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Schedules.Common.Controls.ContentEditors.Events;

namespace Asa.Schedules.Common.Controls.ContentEditors.Interfaces
{
	public interface IContentEditControl<TChangeInfo> : IContentControl
		where TChangeInfo : BaseScheduleChangeInfo
	{
		event EventHandler<ContentSavedEventArgs<TChangeInfo>> ContentChanged;
		void OnRelatedContentChanged(TChangeInfo lastChangeInfo);
		void Saving(ContentSavingEventArgs savingArgs);
		void Save(ContentSavingEventArgs savingArgs);
	}
}