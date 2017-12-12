using System;
using System.Collections.Generic;
using Asa.Schedules.Common.Controls.ContentEditors.Enums;

namespace Asa.Schedules.Common.Controls.ContentEditors.Events
{
	public class ContentSavingEventArgs : EventArgs
	{
		public ContentSavingReason SavingReason { get; set; }
		public bool RequreScheduleInfoValidation { get; set; }
		public bool Cancel { get; set; }
		public List<string> ErrorMessages { get; set; }

		public ContentSavingEventArgs()
		{
			ErrorMessages = new List<string>();
		}
	}
}
