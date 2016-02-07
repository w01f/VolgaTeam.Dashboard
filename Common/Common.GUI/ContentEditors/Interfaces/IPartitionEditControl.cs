using System;
using Asa.Business.Common.Entities.NonPersistent.Schedule;

namespace Asa.Common.GUI.ContentEditors.Interfaces
{
	public interface IPartitionEditControl<TChangeInfo> : IContentEditControl<TChangeInfo>
		where TChangeInfo : BaseScheduleChangeInfo
	{
		event EventHandler<EventArgs> ThemeChanged;
		void OnOuterThemeChanged();
	}
}