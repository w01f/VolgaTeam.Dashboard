using System.Collections.Generic;
using Asa.Solutions.StarApp.PresentationClasses.Output;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Output
{
	public interface IOutputItem
	{
		IList<OutputConfiguration> GetOutputConfigurations();
	}
}
