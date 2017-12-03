using System;
using System.Drawing;

namespace Asa.Common.Core.Helpers
{
	public static class ColorHelper
	{
		public static string ToHex(this Color target)
		{
			return String.Format("#{0:X2}{1:X2}{2:X2}", target.R, target.G, target.B);
		}

		public static Color GetNegativeColor(this Color target)
		{
			var a = 1 - (0.299 * target.R + 0.587 * target.G + 0.114 * target.B) / 255;
			var d = a < 0.5 ? 0 : 255;
			return Color.FromArgb(d, d, d);
		}
	}
}
