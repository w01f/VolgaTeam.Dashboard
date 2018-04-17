using System.Collections.Generic;
using Asa.Common.GUI.Preview;

namespace Asa.Solutions.StarApp.PresentationClasses.Output
{
	public interface IStarAppSlide
	{
		string OutputName { get; }
		IList<OutputConfiguration> GetOutputConfigurations();
		void GenerateOutput();
		PreviewGroup GeneratePreview();
	}
}
