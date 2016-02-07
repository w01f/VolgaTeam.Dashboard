using System;
using System.Collections.Generic;
using Asa.Common.GUI.ContentEditors.Enums;

namespace Asa.Common.GUI.ContentEditors.Events
{
	public class ContentSavingEventArgs : EventArgs
	{
		public ContentSavingReason SavingReason { get; set; }
		public bool Cancel { get; set; }
		public List<string> ErrorMessages { get; set; }

		public ContentSavingEventArgs()
		{
			ErrorMessages = new List<string>();
		}
	}
}
