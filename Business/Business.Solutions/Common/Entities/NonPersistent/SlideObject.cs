using System.Collections.Generic;
using Asa.Common.Core.Enums;
using Asa.Common.Core.Objects.Slides;

namespace Asa.Business.Solutions.Common.Entities.NonPersistent
{
	public class SlideObject
	{
		public Dictionary<SlideFormatEnum, string> SourceSlideMasters { get; } = new Dictionary<SlideFormatEnum, string>();

		public void SaveSlideMaster(SlideMaster slideMaster)
		{

		}
	}
}
