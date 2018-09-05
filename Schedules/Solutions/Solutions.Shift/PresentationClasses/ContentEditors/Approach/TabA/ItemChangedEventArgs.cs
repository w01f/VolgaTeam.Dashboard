using System;
using Asa.Business.Solutions.Shift.Configuration.Approach;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Approach.TabA
{
	public class ItemChangedEventArgs : EventArgs
	{
		public bool Checked { get; set; }
		public ApproachItemInfo ItemInfo { get; set; }
	}
}
