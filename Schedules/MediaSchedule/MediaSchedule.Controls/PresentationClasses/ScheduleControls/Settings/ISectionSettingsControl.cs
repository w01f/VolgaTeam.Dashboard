using Asa.Business.Media.Entities.NonPersistent.Section.Content;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Settings
{
	public interface ISectionSettingsControl : ISettingsDataControl
	{
		void LoadSectionData(ScheduleSection sectionData);
	}
}
