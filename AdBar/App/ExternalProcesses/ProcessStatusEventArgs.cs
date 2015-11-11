using System;

namespace Asa.Bar.App.ExternalProcesses
{
	class ProcessStatusEventArgs : EventArgs
	{
		public BarVsProcessStatus Status { get; private set; }

		public ProcessStatusEventArgs(BarVsProcessStatus status)
		{
			Status = status;
		}
	}
}
