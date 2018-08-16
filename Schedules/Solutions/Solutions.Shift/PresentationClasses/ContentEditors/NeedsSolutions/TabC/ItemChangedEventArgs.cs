using System;
using Asa.Business.Solutions.Shift.Configuration.NeedsSolutions;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.NeedsSolutions.TabC
{
	public class ItemChangedEventArgs : EventArgs
	{
		public bool Checked { get; set; }
		public SolutionsItemInfo ItemInfo { get; set; }
	}
}
