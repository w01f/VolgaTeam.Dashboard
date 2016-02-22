using Asa.Business.Media.Entities.NonPersistent.Section.Content;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	public interface IContentSettingsControl : ISettingsDataControl
	{
		void LoadContentData(ProgramScheduleContent content);
	}
}
