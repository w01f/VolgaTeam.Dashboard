using System;

namespace Asa.Media.Controls.PresentationClasses.SnapshotControls.Output
{
	public class OutputGroup : IDisposable
	{
		public IOutputContainer OutputContainer { get; private set; }
		public string Name { get; set; }
		public bool IsCurrent { get; set; }
		public OutputConfiguration[] Configurations { get; set; }

		public OutputGroup(IOutputContainer outputContainer)
		{
			OutputContainer = outputContainer;
		}

		public void Dispose()
		{
			OutputContainer = null;
		}
	}
}
