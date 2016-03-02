using System.Drawing;

namespace AdSalesBrowser
{
	static class Utils
	{
		public static Point GetCenter(this Rectangle control)
		{
			return new Point(control.X + (control.Width / 2), control.Y + (control.Height / 2));
		}
	}
}
