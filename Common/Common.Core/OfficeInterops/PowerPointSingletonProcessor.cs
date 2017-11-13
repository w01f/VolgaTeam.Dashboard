using Microsoft.Office.Core;

namespace Asa.Common.Core.OfficeInterops
{
	public abstract class PowerPointSingletonProcessor : PowerPointProcessor
	{
		public override bool Connect(bool force = false)
		{
			var result = base.Connect(force);
			if (!result)
				return false;
			result = GetActivePresentation(force) != null;
			if (result)
				PowerPointObject.Visible = MsoTriState.msoCTrue;
			return result;
		}
	}
}
