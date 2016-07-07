using Asa.Common.Core.Enums;
using Asa.Media.Controls.PresentationClasses.Digital.Settings;

namespace Asa.Media.Controls.PresentationClasses.Digital.ContentEditors
{
	interface IDigitalSection
	{
		DigitalSectionType SectionType { get; }
		SlideType SlideType { get; }
		string HelpTag { get; }
	}
}
