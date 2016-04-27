using System;
using Asa.Common.Core.Enums;

namespace Asa.Common.Core.Objects.Output
{
	public class SlideSize
	{
		public decimal Width { get; set; }
		public decimal Height { get; set; }

		public SlideOrientationEnum Orientation => Width > Height ? SlideOrientationEnum.Landscape : SlideOrientationEnum.Portrait;
	}
}
