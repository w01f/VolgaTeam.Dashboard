using Asa.Common.GUI.Preview;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.ContentEditors
{
	public interface ISectionOutputControl
	{
		bool ReadyForOutput { get; }
		void GenerateOutput();
		PreviewGroup GeneratePreview();
	}
}
