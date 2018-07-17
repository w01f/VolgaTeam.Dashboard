using Asa.Common.GUI.Preview;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Output
{
	public interface IOutputContainer
	{
		string OutputName { get; }
		OutputGroup GetOutputGroup();
	}
}
