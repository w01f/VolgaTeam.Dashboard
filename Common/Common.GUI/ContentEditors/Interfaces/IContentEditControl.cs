using System;
using Asa.Business.Common.Entities.NonPersistent.Schedule;
using Asa.Common.GUI.ContentEditors.Events;

namespace Asa.Common.GUI.ContentEditors.Interfaces
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