using System;
using System.Collections.Generic;
using DevComponents.DotNetBar;

namespace Asa.Bar.Plugins.Core
{
	public interface IAdBarControl
	{
		string ControlName { get; }
		IEnumerable<RibbonBar> RibbonBars { get; }
		event EventHandler<AdBarControlStateEventArgs> StateChanged;
		void UpdateControl(IAdBarControl raisedBy, object[] stateParameters);
	}

	public class AdBarControlStateEventArgs : EventArgs
	{
		public object[] StateParameters { get; set; }
	}
}
