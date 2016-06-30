using System.Collections.Generic;
using Asa.Common.GUI.Preview;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Output
{
	public interface IOutputContainer
	{
		string OutputName { get; }
		OutputGroup GetOutputGroup();
		void GenerateOutput(IList<OutputConfiguration> configurations);
		IList<PreviewGroup> GeneratePreview(IList<OutputConfiguration> configurations);
	}
}
