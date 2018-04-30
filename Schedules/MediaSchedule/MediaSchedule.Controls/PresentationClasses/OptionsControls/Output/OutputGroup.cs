using System;
using System.Linq;
using Asa.Common.GUI.OutputSelector;

namespace Asa.Media.Controls.PresentationClasses.OptionsControls.Output
{
	public class OutputGroup : IDisposable, Common.GUI.OutputSelector.IOutputItem
	{
		public IOutputContainer OutputContainer { get; private set; }
		public string DisplayName { get; set; }
		public bool IsCurrent { get; set; }
		public OutputConfiguration[] Configurations { get; set; }

		public ISlideItem[] SlideItems
		{
			get => Configurations;
			set => Configurations = value.OfType<OutputConfiguration>().ToArray();
		}

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
