using System;
using System.Linq;
using Asa.Common.GUI.OutputSelector;
using Asa.Online.Controls.PresentationClasses.Products;

namespace Asa.Media.Controls.PresentationClasses.Digital.Output
{
	public class OutputGroup : IOutputItem
	{
		public string DisplayName { get; set; }
		public bool IsCurrent { get; set; }
		public IDigitalOutputItem[] OutputItems { get; set; }

		public ISlideItem[] SlideItems
		{
			get => OutputItems;
			set => OutputItems = value.OfType<IDigitalOutputItem>().ToArray();
		}
	}
}
