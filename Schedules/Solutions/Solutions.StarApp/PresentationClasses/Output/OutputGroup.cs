using System;

namespace Asa.Solutions.StarApp.PresentationClasses.Output
{
	public class OutputGroup : IDisposable
	{
		public IStarAppSlideContainer OutputContainer { get; private set; }
		public string Name { get; set; }
		public bool IsCurrent { get; set; }
		public OutputConfiguration[] Configurations { get; set; }

		public OutputGroup(IStarAppSlideContainer outputContainer)
		{
			OutputContainer = outputContainer;
		}

		public void Dispose()
		{
			OutputContainer = null;
		}
	}
}
