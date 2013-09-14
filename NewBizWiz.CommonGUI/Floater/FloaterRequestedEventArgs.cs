using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewBizWiz.CommonGUI.Floater
{
	public class FloaterRequestedEventArgs : EventArgs
	{
		public Action AfterShow { get; set; }
	}
}
