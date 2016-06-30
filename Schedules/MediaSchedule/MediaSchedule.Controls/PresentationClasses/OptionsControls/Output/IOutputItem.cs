using System.Collections.Generic;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Output
{
	public interface IOutputItem
	{
		IList<OutputConfiguration> GetOutputConfigurations();
	}
}
