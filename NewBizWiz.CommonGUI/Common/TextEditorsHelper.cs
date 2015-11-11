using System;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Asa.CommonGUI.Common
{
	public static class TextEditorsHelper
	{
		private static bool enter;
		private static bool needSelect;

		public static void Editor_Enter(object sender, EventArgs e)
		{
			enter = true;
		}

		public static void Editor_MouseUp(object sender, MouseEventArgs e)
		{
			if (needSelect)
			{
				(sender as BaseEdit).SelectAll();
			}
			ResetEnterFlag();
		}

		public static void Editor_MouseDown(object sender, MouseEventArgs e)
		{
			needSelect = enter;
		}

		private static void ResetEnterFlag()
		{
			enter = false;
		}
	}
}
