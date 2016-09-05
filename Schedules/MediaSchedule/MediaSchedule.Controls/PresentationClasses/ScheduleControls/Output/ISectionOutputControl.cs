using System.Collections.Generic;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Output
{
	public interface ISectionOutputControl
	{
		IEnumerable<ScheduleSectionOutputItem> GetAvailableOutputItems();
	}
}
