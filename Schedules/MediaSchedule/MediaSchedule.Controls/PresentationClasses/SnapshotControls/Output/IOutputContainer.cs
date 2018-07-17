using Asa.Common.GUI.Preview;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Output
{
	public interface IOutputContainer
	{
		string OutputName { get; }
		OutputGroup GetOutputGroup();
	}
}
