using System;
using Asa.Common.Core.Enums;

namespace Asa.Solutions.Dashboard.PresentationClasses
{
	public class DashboardSlideChangedEventArgs : EventArgs
	{
		public SlideType SlideType { get; set; }
	}
}
