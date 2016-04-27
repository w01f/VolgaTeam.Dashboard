using System.Collections.Generic;
using Asa.Common.Core.Enums;

namespace Asa.Common.Core.Objects.Output
{
	class SlideFormat
	{
		public SlideFormatEnum Id { get; set; }
		public SlideSize DefaultSize { get; set; }
		public List<SlideSize> SupportedSizes { get; private set; }

		public SlideFormat()
		{
			SupportedSizes = new List<SlideSize>();
		}
	}
}
