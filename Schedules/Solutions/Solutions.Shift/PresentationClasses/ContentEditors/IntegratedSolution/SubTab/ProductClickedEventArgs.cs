using System;
using Asa.Business.Solutions.Shift.Configuration.IntegratedSolution;

namespace Asa.Solutions.Shift.PresentationClasses.ContentEditors.IntegratedSolution.SubTab
{
	public class ProductClickedEventArgs : EventArgs
	{
		public ProductInfo ItemInfo { get; set; }
	}
}
