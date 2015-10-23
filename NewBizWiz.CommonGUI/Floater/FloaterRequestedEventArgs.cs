﻿using System;
using System.Drawing;

namespace Asa.CommonGUI.Floater
{
	public class FloaterRequestedEventArgs : EventArgs
	{
		public Image Logo { get; set; }
		public Action AfterShow { get; set; }
	}
}
