using System;
using Asa.Common.GUI.Preview;

namespace Asa.Online.Controls.PresentationClasses.Products
{
	public interface IDigitalOutputItem
	{
		string SlideName { get; }
		int SlidesCount { get; }
		void GenerateOutput();
		PreviewGroup GeneratePreview();
	}
}
