using System;
using System.Drawing;

namespace NewBizWiz.CommonGUI.Floater
{
	public class FloaterRequestedEventArgs : EventArgs
	{
		public Image Logo { get; set; }
		public Action AfterShow { get; set; }
	}
}
