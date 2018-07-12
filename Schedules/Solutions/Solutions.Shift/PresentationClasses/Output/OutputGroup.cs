using System;
using System.Linq;
using Asa.Common.GUI.OutputSelector;

namespace Asa.Solutions.Shift.PresentationClasses.Output
{
	public class OutputGroup : IDisposable, IOutputItem
	{
		public IShiftSlideContainer OutputContainer { get; private set; }
		public string DisplayName { get; set; }
		public bool IsCurrent { get; set; }
		public OutputConfiguration[] Configurations { get; set; }

		public ISlideItem[] SlideItems
		{
			get => Configurations;
			set => Configurations = value.OfType<OutputConfiguration>().ToArray();
		}

		public OutputGroup(IShiftSlideContainer outputContainer)
		{
			OutputContainer = outputContainer;
		}

		public void Dispose()
		{
			OutputContainer = null;
		}
	}
}
