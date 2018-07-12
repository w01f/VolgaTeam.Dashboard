using System.Collections.Generic;
using Asa.Common.GUI.Preview;

namespace Asa.Solutions.Shift.PresentationClasses.Output
{
	public interface IShiftSlide
	{
		string OutputName { get; }
		IList<OutputConfiguration> GetOutputConfigurations();
		void GenerateOutput();
		PreviewGroup GeneratePreview();
	}
}
