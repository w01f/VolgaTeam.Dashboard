using System;
using System.Collections.Generic;

namespace Asa.Solutions.StarApp.PresentationClasses.Output
{
	public class LoadAllEventArgs : EventArgs
	{
		public List<OutputGroup> OutputItems { get; }

		public LoadAllEventArgs()
		{
			OutputItems = new List<OutputGroup>();
		}
	}
}
