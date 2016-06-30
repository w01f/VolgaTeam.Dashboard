using Asa.Media.Controls.PresentationClasses.Digital.Settings;

namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	interface IDigitalSection
	{
		DigitalSectionType SectionType { get; }
		string HelpTag { get; }
	}
}
