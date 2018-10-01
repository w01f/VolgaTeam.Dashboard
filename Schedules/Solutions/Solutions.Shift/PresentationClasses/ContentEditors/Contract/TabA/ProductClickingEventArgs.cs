using System;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Contract.TabA
{
	public class ProductClickingEventArgs : EventArgs
	{
		public bool Cancel { get; set; }
	}
}
