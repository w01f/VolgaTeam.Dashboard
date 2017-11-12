using System;
using Asa.Common.Core.Enums;

namespace Asa.Solutions.Common.Common
{
	public class SelectedSlideTypeChanged : EventArgs
	{
		public SlideType SlideType { get; set; }
	}
}
