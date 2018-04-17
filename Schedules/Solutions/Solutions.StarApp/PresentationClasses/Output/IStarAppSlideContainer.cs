using System.Collections.Generic;
using Asa.Common.Core.Enums;
using Asa.Common.GUI.Preview;

namespace Asa.Solutions.StarApp.PresentationClasses.Output
{
	public interface IStarAppSlideContainer
	{
		bool ReadyForOutput { get; }
		SlideType SlideType { get; }
		string OutputName { get; }
		OutputGroup GetOutputGroup();
		void GenerateOutput(IList<OutputConfiguration> configurations);
		IList<PreviewGroup> GeneratePreview(IList<OutputConfiguration> configurations);
	}
}
