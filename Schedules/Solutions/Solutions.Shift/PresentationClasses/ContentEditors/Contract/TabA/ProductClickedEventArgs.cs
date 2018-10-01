using System;
using Asa.Business.Solutions.Shift.Configuration.Contract;
using Asa.Business.Solutions.Shift.Configuration.Contract.TabA;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.Contract.TabA
{
	public class ProductClickedEventArgs : EventArgs
	{
		public ProductInfo ItemInfo { get; set; }
	}
}
