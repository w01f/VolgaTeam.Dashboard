using System;
using Asa.Business.Solutions.Shift.Configuration.NeedsSolutions;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.NeedsSolutions.TabA
{
	public class ItemChangedEventArgs : EventArgs
	{
		public bool Checked { get; set; }
		public NeedsItemInfo ItemInfo { get; set; }
	}
}
