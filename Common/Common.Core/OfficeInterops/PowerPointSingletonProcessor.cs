using Microsoft.Office.Core;

namespace Asa.Common.Core.OfficeInterops
{
	public class PowerPointSingletonProcessor : PowerPointProcessor
	{
		public override bool Connect(bool forceNewObject = false)
		{
			var result = base.Connect(forceNewObject);
			if (!result)
				return false;
			result = GetActivePresentation() != null;
			if (result)
				PowerPointObject.Visible = MsoTriState.msoCTrue;
			return result;
		}
	}
}
