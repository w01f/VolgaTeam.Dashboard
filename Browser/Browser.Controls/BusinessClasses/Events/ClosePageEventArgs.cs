using System;
using Asa.Browser.Controls.Controls.WebPage;

namespace Asa.Browser.Controls.BusinessClasses.Events
{
	public class ClosePageEventArgs : EventArgs
	{
		public WebKitPage Page { get; set; }
		public bool NeedReleasePage { get; set; }
	}
}
