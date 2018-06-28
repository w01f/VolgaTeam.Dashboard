using System.IO;
using Asa.Business.Solutions.Common.Entities.NonPersistent;

namespace Asa.Solutions.Common.PresentationClasses
{
	static class SolutionToggleHelper
	{
		public const int ButtonHeight = 40;
		public const int ButtonPadding = 70;
		public const int ButtonMaxWidth = 350;


		public static ISolutionToggle Create(BaseSolutionInfo solutionInfo, int buttonWidth)
		{
			return File.Exists(solutionInfo.ToggleImagePath) ?
				(ISolutionToggle)new SolutionImageToggle(solutionInfo, buttonWidth) :
				new SolutionButtonToggle(solutionInfo);
		}

	}
}
