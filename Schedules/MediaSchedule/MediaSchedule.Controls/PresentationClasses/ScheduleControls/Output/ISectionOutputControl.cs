using System.Collections.Generic;
using Asa.Common.GUI.Preview;

namespace Asa.Media.Controls.PresentationClasses.ScheduleControls.Output
{
	public interface ISectionOutputControl
	{
		IList<OutputItem> GetOutputItems();
	}
}
