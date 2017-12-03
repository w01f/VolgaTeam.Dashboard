using System.Collections.Generic;
using Asa.Common.GUI.Preview;

namespace Asa.Solutions.Dashboard.PresentationClasses.Output
{
	public interface IDashboardSlide
	{
		IEnumerable<DashboardSlideInfo> GetSlideInfo();
		void GenerateOutput(DashboardSlideInfo slideInfo);
		PreviewGroup GeneratePreview(DashboardSlideInfo slideInfo);
	}
}
