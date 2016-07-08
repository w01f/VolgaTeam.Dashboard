using System.Collections.Generic;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Output
{
	public interface IOutputItem
	{
		IList<OutputConfiguration> GetOutputConfigurations();
	}
}
